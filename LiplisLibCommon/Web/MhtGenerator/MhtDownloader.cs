//=======================================================================
//  ClassName : MhtDownloader
//  概要      : MHTダウンローダー
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Liplis.Web.MhtGenerator
{

    /// <summary>
    /// MIME 文書を管理します。
    /// </summary>
    public class MhtDownloader
    {
        /// <summary>
        /// MIMEObjectを格納する配列
        /// </summary>
        private ArrayList objects;
        private string Boundary;
        private string From;
        private string Subject;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public MhtDownloader(string url)
        {
            this.init();
            MimePart mo = new MimePart(url);
            this.AddMimeObject(mo);
            this.AddMimeObjects(mo.ParseHtml());
        }

        private void init()
        {
            this.objects = new ArrayList();

            // 本当はIDを振って重複チェックの要あり
            this.Boundary = "----=_NextPart_000_0000";

            this.From = "<MimeDocument>";
            this.Subject = "MimeDocument Generated Page";
        }

        /*
        private ArrayList Objects
        {
            get
            {
                return this.objects;
            }
        }
        */

        protected void AddMimeObject(MimePart mimeObject)
        {
            this.objects.Add(mimeObject);
        }

        protected void AddMimeObjects(ArrayList objects)
        {
            foreach (object obj in objects)
            {
                this.AddMimeObject(obj as MimePart);
            }
        }

        public void Write(TextWriter textWriter)
        {
            this.WriteHeader(textWriter);

            foreach (object obj in this.objects)
            {
                MimePart mo = obj as MimePart;
                mo.Write(textWriter);
                this.WriteBoundary(textWriter);
            }
        }

        public void Write(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default))
            {
                this.Write(sw);
            }
        }

        private void WriteHeader(TextWriter textWriter)
        {
            textWriter.WriteLine("From: " + this.From);
            textWriter.WriteLine("Subject: " + this.Subject);
            textWriter.WriteLine("MIME-Version: 1.0");
            textWriter.WriteLine("Content-Type: multipart/related;");
            textWriter.WriteLine("\tboundary=\"" + this.Boundary + "\";");
            this.WriteBoundary(textWriter);
        }

        private void WriteBoundary(TextWriter textWriter)
        {
            textWriter.WriteLine();
            textWriter.WriteLine("--{0}", this.Boundary);
        }

    }
}

