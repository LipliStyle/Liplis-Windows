//=======================================================================
//  ClassName : MimePart
//  概要      : Mht用マイムパート
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin.Sachin
//=======================================================================
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Win32;

namespace Liplis.Web.MhtGenerator
{
    /// <summary>
    /// MIME に格納されるファイルを処理します。
    /// </summary>
    public class MimePart
    {
        private string contentType;
        private string location;
        private byte[] data;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public MimePart(string url)
        {
            this.Init();
            this.Read(url);
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        private void Init()
        {
            this.contentType = null;
            this.location = null;
            this.data = null;
        }

        #region ReadOnly Properties

        /*
	public string ContentType
	{
		get
		{
			return this.contentType;
		}
	}

	public string Location
	{
		get
		{
			return this.location;
		}
	}

	public byte[] Data
	{
		get
		{
			return this.data;
		}
	}
	*/

        #endregion

        protected void Read(string url)
        {
            this.location = url;
            if (url.StartsWith("http://"))
            {
                this.ReadHttp(url);
            }
            else if (File.Exists(url))
            {
                this.ReadLocalFile(url);
            }
        }

        private void ReadLocalFile(string url)
        {
            RegistryKey regClsRoot = Registry.ClassesRoot.OpenSubKey(Path.GetExtension(url));
            this.contentType = regClsRoot.GetValue("Content Type") as string;

            Stream str = new FileStream(url, FileMode.Open, FileAccess.Read);
            byte[] bytes = new byte[str.Length];
            str.Read(bytes, 0, Convert.ToInt32(str.Length));
            str.Close();
            this.data = bytes;
        }

        private void ReadHttp(string url)
        {
            try
            {
                HttpWebRequest req = null;
                //2回まで再試行
                for (int i = 0; req == null && i < 3; i++)
                {
                    try
                    {
                        req = WebRequest.Create(url) as HttpWebRequest;
                    }
                    catch
                    {
                        req = null;
                        System.Diagnostics.Debug.WriteLine(this, "HTTP接続再試行しています。");
                        //0.5秒待つ
                        System.Threading.Thread.Sleep(500);
                    }
                }
                if (req == null)
                {
                    System.Diagnostics.Trace.WriteLine(this, "HTTP接続に失敗しました。");
                    return;
                }

                using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
                {
                    if (res == null)
                    {
                        return;
                    }

                    System.Diagnostics.Debug.WriteLine(this, "HTTPデータを読み取っています...");
                    this.contentType = res.ContentType;

                    /*
                    Stream str = res.GetResponseStream();
                    byte[] bytes = new byte[str.Length];
                    str.Read(bytes, 0, Convert.ToInt32(str.Length));
                    str.Close();
                    this.data = bytes;
                    */
                    Stream str = res.GetResponseStream();

                    int b;
                    MemoryStream ms = new MemoryStream();
                    while ((b = str.ReadByte()) != -1)
                    {
                        ms.WriteByte((byte)b);
                    }
                    ms.Close();
                    str.Close();

                    this.data = ms.ToArray();
                }
            }
            catch { }
        }

        public void Write(TextWriter textWriter)
        {
            if (this.contentType == null || !this.contentType.StartsWith("text/"))
            {
                this.WriteBinary(textWriter);
            }
            else
            {
                this.WriteText(textWriter);
            }
        }

        private void WriteBinary(TextWriter textWriter)
        {
            textWriter.WriteLine("Content-Type: " + this.contentType);
            textWriter.WriteLine("Content-Transfer-Encoding: base64");
            textWriter.WriteLine("Content-Location: " + new Uri(this.location));
            textWriter.WriteLine();

            int len = this.data.Length;
            for (int p = 0; p < len; p += 57)
            {
                int len2 = len - p;
                if (len2 > 57) len2 = 57;
                textWriter.WriteLine(Convert.ToBase64String(
                    this.data, p, len2));
            }
        }

        private void WriteText(TextWriter textWriter)
        {
            textWriter.WriteLine("Content-Type: " + this.contentType);
            textWriter.WriteLine("Content-Transfer-Encoding: quoted-printable");
            textWriter.WriteLine("Content-Location: " + new Uri(this.location));
            textWriter.WriteLine();

            char ch = '\0', prev;
            foreach (byte b in this.data)
            {
                prev = ch;
                ch = (char)b;
                if (ch == '\r')
                {
                    textWriter.WriteLine();
                }
                else if (ch == '\n')
                {
                    if (prev != '\r') textWriter.WriteLine();
                }
                else if (ch == '=')
                {
                    textWriter.Write("=3D");
                }
                else if (ch > 127)
                {
                    textWriter.Write("={0:X2}", b);
                }
                else
                {
                    textWriter.Write(ch);
                }
            }
        }

        public ArrayList ParseHtml()
        {
            ArrayList ret;
            object target;

            ret = new ArrayList();

            using (MemoryStream ms = new MemoryStream(this.data, false))
            using (XmlParser xp = new XmlParser(ms))
            {
                xp.Dec = Encoding.GetEncoding("windows-1250").GetDecoder();
                while (xp.Read())
                {
                    target = null;
                    if ((xp.Name == "img" || xp.Name == "img/") && xp.Attributes.Contains("src"))
                    {
                        target = xp.Attributes["src"];
                    }
                    else if ((xp.Name == "link" || xp.Name == "link/") && xp.Attributes.Contains("href") && ((string)xp.Attributes["href"]).EndsWith(".css"))
                    {
                        target = xp.Attributes["href"];
                    }
                    if (target != null)
                    {
                        this.AddObject(ret, target.ToString());
                    }
                }
            }
            return ret;
        }

        private void AddObject(ArrayList arrayList, string url)
        {
            StringBuilder sb;
            string url2;
            string target;

            sb = new StringBuilder();
            foreach (char ch in url)
            {
                if (ch != '\r' && ch != '\n')
                {
                    sb.Append(ch);
                }
            }
            url2 = sb.ToString();
            foreach (object obj in arrayList)
            {
                //すでに登録済み
                if ((obj as MimePart).location == url2)
                {
                    return;
                }
            }

            if (this.location.StartsWith("http://"))
            {
                //HTTPから取得
                if (url2.StartsWith("http://"))
                {
                    target = url2;
                }
                else
                {
                    target = new Uri(new Uri(this.location), url2).AbsoluteUri;
                }
            }
            else if (File.Exists(new FileInfo(Path.GetDirectoryName(this.location) + "\\" + url2).FullName))
            {
                //ローカルファイルから取得
                target = new FileInfo(Path.GetDirectoryName(this.location) + "\\" + url2).FullName;
            }
            else
            {
                //取得不可能
                return;
            }

            MimePart mo = new MimePart(target);
            if (mo.data == null)
            {
                //取得できなかった場合
                return;
            }

            arrayList.Add(mo);
        }
    }
}
