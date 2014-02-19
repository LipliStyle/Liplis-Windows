//=======================================================================
//  ClassName : XmlLinqClass
//  概要      : Linqを扱うためのクラス
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin.Sachin
//=======================================================================
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Liplis.Web;

namespace Liplis.Xml
{
    public class XmlLinq : XmlMost
    {
        ///=============================
        ///クラス
        protected XDocument xmlDoc;
        protected int responseCode;
        protected string sourceText;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public XmlLinq() { }
        public XmlLinq(XDocument xmlDoc) 
        {
            this.xmlDoc = xmlDoc;
            responseCode = 0;
        }


        /// <summary>
        /// readXml
        /// XMLを読み込む
        /// </summary>
        protected override void readXml()
        {
            xmlDoc = new XDocument();
            
            try
            {
                //ソースをテキストで取得
                sourceText = HttpGet.getHtmlGet(xmlFilePath);

                //指定したXMLファイルの読み込み
                xmlDoc = XDocument.Load(convertStream(sourceText));

                responseCode = 0;
            }
            catch (System.Xml.XmlException ex)
            {
                //リトライ
                if (reTry(ex.Message, xmlFilePath, "tmp.xml", false, 0))
                {
                    responseCode = 0;
                    return;
                }

                //XML例外 XMLが読み込めませんでした。
                lc.writingLog("XML例外 XMLが読み込めませんでした。書式に問題がある可能性があります。\n");
                responseCode = 1;
            }
            catch (WebException ex)
            {
                //HTTPプロトコルエラーかどうか調べる
                if (ex.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    //HttpWebResponseを取得
                    HttpWebResponse errres = (HttpWebResponse)ex.Response;
                    //応答ステータスコードを表示する
                    lc.writingLog("Web例外 " + errres.StatusCode + ":" + errres.StatusDescription + Environment.NewLine + "接続先はココ→" + xmlFilePath + "\n");
                    Console.WriteLine("{0}:{1}",errres.StatusCode, errres.StatusDescription);
                    responseCode = 10;
                }
                else
                {
                    //XML例外 XMLが読み込めませんでした。
                    lc.writingLog("Web例外 XMLが読み込めませんでした。サーバーが存在しない可能性があります。\n接続先はココ→" + xmlFilePath + "\n");
                    responseCode = 11;
                }
            }
            catch (System.Exception)
            {
                //XMLその他例外 XMLが読み込めませんでした。
                lc.writingLog("XMLその他例外ファイルにアクセスできません。ファイルが開かれている可能性があります。\n" + xmlFilePath + "\n");
                responseCode = 12;
            }
        }

        /// <summary>
        /// readXml
        /// XMLを読み込む
        /// </summary>
        protected void readXmlNonConvert()
        {
            xmlDoc = new XDocument();

            try
            {
                //指定したXMLファイルの読み込み
                xmlDoc = XDocument.Load(xmlFilePath);

                responseCode = 0;
            }
            catch (System.Xml.XmlException ex)
            {
                //リトライ
                if (reTry(ex.Message, xmlFilePath, "tmp.xml", false, 0))
                {
                    responseCode = 0;
                    return;
                }

                //XML例外 XMLが読み込めませんでした。
                lc.writingLog("XML例外 XMLが読み込めませんでした。書式に問題がある可能性があります。\n");
                responseCode = 1;
            }
            catch (WebException ex)
            {
                //HTTPプロトコルエラーかどうか調べる
                if (ex.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    //HttpWebResponseを取得
                    HttpWebResponse errres = (HttpWebResponse)ex.Response;
                    //応答ステータスコードを表示する
                    lc.writingLog("Web例外 " + errres.StatusCode + ":" + errres.StatusDescription + Environment.NewLine + "接続先はココ→" + xmlFilePath + "\n");
                    Console.WriteLine("{0}:{1}", errres.StatusCode, errres.StatusDescription);
                    responseCode = 10;
                }
                else
                {
                    //XML例外 XMLが読み込めませんでした。
                    lc.writingLog("Web例外 XMLが読み込めませんでした。サーバーが存在しない可能性があります。\n接続先はココ→" + xmlFilePath + "\n");
                    responseCode = 11;
                }
            }
            catch (System.Exception)
            {
                //XMLその他例外 XMLが読み込めませんでした。
                lc.writingLog("XMLその他例外ファイルにアクセスできません。ファイルが開かれている可能性があります。\n" + xmlFilePath + "\n");
                responseCode = 12;
            }
        }

        /// <summary>
        /// リトライする
        /// </summary>
        /// <param name="message">エラーメッセージ</param>
        /// <param name="url">URL</param>
        /// <param name="fileName">テンプ保存ファイル名</param>
        /// <param name="flgRetry">リトライフラグ</param>
        /// <param name="cntRetry">リトライカウント</param>
        /// <returns></returns>
        private bool reTry(string message, string url, string fileName, bool flgRetry, int cntRetry)
        {
            int idx = 0;
            int col = 0;
            int row = 0;
            string res = "";
            string line = "";
            try
            {
                if (!flgRetry)
                {
                    //無効文字があった場合、ファイルだうんろーどして除去
                    downLoadXml("tmp.xml", xmlFilePath);
                }

                //不正文字位置の特定
                row = getInvaildCodeRow(message);
                col = getInvaildCodeCol(message);

                //テキストファイル読み込み
                StreamReader sr = new StreamReader(this.xmlFilePath, Encoding.GetEncoding("UTF-8"));

                while ((line = sr.ReadLine()) != null)
                {
                    if (row == idx+1)
                    {
                        line.Remove(col, 1);
                    }
                    res += line;
                    idx++;
                }

                sr.Close();

                //不正文字の除去
                res = res.Replace(getInvaildCode(message), "");

                Console.Write(res.IndexOf(getInvaildCode(message)));

                xmlDoc = XDocument.Parse(res);

                //リトライしてみる
                //xmlDoc = XDocument.Load(xmlFilePath);

                return true;
            }
            catch (System.Xml.XmlException ex)
            {
                //10回以上ミスならエラーとする
                if (cntRetry > 10)
                {
                    return false;
                }

                //再書き込み
                StreamWriter w = new StreamWriter(this.xmlFilePath, false, Encoding.GetEncoding("UTF-8"));
                w.Write(line);
                w.Close();
                return reTry(ex.Message, url, fileName, true, cntRetry++);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 無効文字を取得する
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string getInvaildCode(string message)
        {
            string result = message.Substring(1,message.IndexOf('\'',0)+1);
            return result;
        }

        /// <summary>
        /// 無効文字行を取得する
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private int getInvaildCodeRow(string message)
        {
            int startIdx = message.IndexOf("行", 0) + 2;
            int endIdx = message.IndexOf("、",startIdx);
            string result = message.Substring(startIdx, endIdx - startIdx);
            return int.Parse(result);
        }

        /// <summary>
        /// 無効文字行を取得する
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private int getInvaildCodeCol(string message)
        {
            int startIdx = message.IndexOf("位置", 0) + 2;
            int endIdx = message.IndexOf("。", startIdx);
            string result = message.Substring(startIdx, endIdx - startIdx);
            return int.Parse(result);
        }

        /// <summary>
        /// ルートエレメントを返す
        /// </summary>
        /// <returns></returns>
        public string getRootElements()
        {
            try
            {
                return xmlDoc.Root.Name.ToString();
            }
            catch (System.Exception)
            {
                lc.writingLog("XmlLinq : getRootElements\n");
                return "";
            }
        }

        /// <summary>
        /// デフォルトネームスペースを取得する
        /// </summary>
        public XNamespace getXmlns()
        {
            try
            {
                var query = from xroot in xmlDoc.Elements() select xroot.Attribute("xmlns");
                foreach (string xmlns in query)
                {
                    return xmlns;
                }
            }
            catch (System.Exception)
            {
                lc.writingLog("XmlLinq : getXmlns\n");
            }
            return "";
        }

        /// <summary>
        /// 対象のネームスペースを取得する
        /// </summary>
        public XNamespace getNameSpase(string name)
        {
            try
            {
                var query = from xroot in xmlDoc.Elements() select xroot.Attribute(XNamespace.Xmlns + name);
                foreach (string xmlns in query)
                {
                    return xmlns;
                }
            }
            catch (System.Exception)
            {
                lc.writingLog("XmlLinq : getXmlns\n");
            }
            return "";
        }


        /// <summary>
        /// URLの内容をテキストリーダーにコンバートして返す
        /// </summary>
        /// <returns>ネームスペース</returns>
        #region convertStream
        public TextReader convertStream(string source)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < source.Length; i++)
                {
                    string subStr = source.Substring(i, 1);
                    char c = subStr[0];

                    if (!((0x00 <= c && c <= 0x1f) || (c == 0x7f)))
                    {
                        sb.Append(subStr);
                    }
                }

                return new StringReader(sb.ToString());
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
