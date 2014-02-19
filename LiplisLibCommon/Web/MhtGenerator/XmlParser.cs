//=======================================================================
//  ClassName : XmlParser
//  概要      : Mht用XMLパーサー
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin.Sachin
//=======================================================================
using System;
using System.Collections;
using System.IO;
using System.Text;


namespace Liplis.Web.MhtGenerator
{
    /// <summary>
    /// XmlReader の簡易版です。
    /// </summary>
    public class XmlParser : IDisposable
    {
        private Stream stream;
        private string element;
        private string name;
        private Hashtable attr;
        private bool letter;
        private string data;
        private Decoder dec;

        public Decoder Dec
        {
            set
            {
                this.dec = value;
            }
        }

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public XmlParser(Stream stream)
        {
            this.stream = stream;
            this.element = this.name = this.data = "";
            this.attr = new Hashtable();
            this.dec = Encoding.Default.GetDecoder();
            this.letter = false;
        }

        public void Dispose()
        {
            if (stream != null)
            {
                stream = null;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public Hashtable Attributes
        {
            get
            {
                return this.attr;
            }
        }

        public string Data
        {
            get
            {
                return this.data;
            }
        }

        public bool Read()
        {
            MemoryStream data;
            MemoryStream element;
            int ch;
            int state;

            data = new MemoryStream();
            element = new MemoryStream();
            state = 0;

            while ((ch = stream.ReadByte()) != -1)
            {
                if ((state == 1 || state == 4) && ch == (int)'>')
                {
                    break;
                }
                else if (ch == (int)'<')
                {
                    if (state == 1)
                    {
                        data.WriteByte((byte)ch);
                        data.Write(element.ToArray(), 0, (int)element.Length);
                        element.SetLength(0);
                    }
                    else
                    {
                        state = 1;
                    }
                }
                else if (state >= 1)
                {
                    element.WriteByte((byte)ch);
                    if (element.Length == 3)
                    {
                        byte[] e = element.ToArray();
                        if (e[0] == (byte)'!'
                            && e[1] == (byte)'-'
                            && e[2] == (byte)'-')
                        {
                            state = 2;
                        }
                    }
                    if ((state == 2 || state == 3) && ch == (int)'-')
                    {
                        state++;
                    }
                    else if (state > 2)
                    {
                        state = 2;
                    }
                }
                else
                {
                    data.WriteByte((byte)ch);
                }
            }
            this.element = this.ConvertString(element.ToArray());
            this.name = "";
            this.attr.Clear();
            this.data = this.ConvertString(data.ToArray());
            data.Close();
            element.Close();
            if (this.element.Length < 1) return false;

            this.Parse();
            return true;
        }

        private string ConvertString(byte[] bytes)
        {
            char[] chars;

            if (bytes.Length < 1) return "";
            chars = new char[this.dec.GetCharCount(bytes, 0, bytes.Length)];
            this.dec.GetChars(bytes, 0, bytes.Length, chars, 0);
            return new String(chars);
        }

        private void Parse()
        {
            int xmlState;
            StringBuilder aname;
            StringBuilder adata;
            int len;
            int state;
            char ch;
            char quot;

            if (this.element.StartsWith("!--"))
            {
                this.name = "!--";
                string ad = this.element.Substring(3);
                if (ad.EndsWith("--")) ad = ad.Substring(0, ad.Length - 2);
                if (ad.Length > 0) this.attr["value"] = ad;
                return;
            }

            aname = new StringBuilder();
            adata = new StringBuilder();
            xmlState = state = 0;
            len = this.element.Length;
            quot = '\0';
            for (int i = 0; i <= len; i++)
            {
                ch = (i < len) ? this.element[i] : '\0';
                if (i == len
                    || (quot == '\0' && (ch == ' ' || ch == '\r' || ch == '\n' || ch == '\t'))
                    || (state == 4 && ch == quot))
                {
                    if (state == 1)
                    {
                        state = 2;
                    }
                    else if (state == 3)
                    {
                        aname.Remove(0, aname.Length);
                        state = 2;
                    }
                    else if (state == 4)
                    {
                        quot = '\0';
                        if (aname.Length > 0 && adata.Length > 0)
                        {
                            string an = this.letter ? aname.ToString()
                                : aname.ToString().ToLower();
                            this.attr[an] = adata.ToString();
                        }
                        if (aname.Length > 0) aname.Remove(0, aname.Length);
                        if (adata.Length > 0) adata.Remove(0, adata.Length);
                        state = 2;
                    }
                    continue;
                }
                if (state == 0)
                {
                    state = 1;
                }
                else if (state == 2)
                {
                    state = 3;
                }
                if (ch == '/')
                {
                    if (state == 1)
                    {
                        if (this.name.Length < 1)
                        {
                            xmlState = 1;
                        }
                        else
                        {
                            xmlState = 2;
                        }
                        continue;
                    }
                    else if (state == 3)
                    {
                        xmlState = 2;
                        continue;
                    }
                }
                if (ch == '?' && (state == 1 || state == 3))
                {
                    xmlState = 3;
                    continue;
                }
                if (state == 1)
                {
                    this.name += ch;
                }
                else if (state == 3)
                {
                    if (ch == '=')
                    {
                        state = 4;
                    }
                    else
                    {
                        aname.Append(ch);
                    }
                }
                else if (state == 4)
                {
                    if (quot == '\0' && (ch == '"' || ch == '\''))
                    {
                        quot = ch;
                    }
                    else
                    {
                        adata.Append(ch);
                    }
                }
            }
            if (!letter) this.name = this.name.ToLower();

            switch (xmlState)
            {
                case 1:
                    this.name = "/" + this.name;
                    break;
                case 2:
                    this.name += "/";
                    break;
                case 3:
                    this.name = "?" + this.name;
                    break;
            }
        }
    }
}
