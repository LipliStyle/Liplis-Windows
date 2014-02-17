//=======================================================================
//  ClassName : XmlLinq
//  概要      : Linqコントローラー
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using Liplis.Common;
using System.IO;
using Liplis.Web;
using System.Text;

namespace Liplis.Xml
{
    public class XmlLinq : XmlMost
    {
        ///=============================
        ///クラス
        protected XDocument xmlDoc;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public XmlLinq() { }


        /// <summary>
        /// readXml
        /// XMLを読み込む
        /// </summary>
        #region readXml
        protected override void readXml()
        {
            xmlDoc = new XDocument();

            try
            {
                //指定したXMLファイルの読み込み
                xmlDoc = XDocument.Load(convertStream(HttpGet.getHtmlGet(xmlFilePath)));
            }
            catch (System.Xml.XmlException)
            {
                //XML例外 XMLが読み込めませんでした。
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "XML例外 XMLが読み込めませんでした。書式に問題がある可能性があります。\n");
            }
            catch (System.Net.WebException)
            {
                //XML例外 XMLが読み込めませんでした。
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Web例外 XMLが読み込めませんでした。サーバーが存在しない可能性があります。\n接続先はココ→" + xmlFilePath + "\n");
            }
            catch (System.Exception)
            {
                //XMLその他例外 XMLが読み込めませんでした。
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "XMLその他例外ファイルにアクセスできません。ファイルが開かれている可能性があります。\n" + xmlFilePath + "\n");
            }
        }
        #endregion

        /// <summary>
        /// ルートエレメントを返す
        /// </summary>
        /// <returns>ルートエレメント</returns>
        #region getRootElements
        public string getRootElements()
        {
            try
            {
                return xmlDoc.Root.Name.ToString();
            }
            catch (Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return "";
            }
        }
        #endregion

        /// <summary>
        /// デフォルトネームスペースを取得する
        /// </summary>
        /// <returns>ネームスペース</returns>
        #region getXmlns
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
            catch (Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
            return "";
        }
        #endregion

        /// <summary>
        /// 対象のネームスペースを取得する
        /// </summary>
        /// <param name="name">タグ名</param>
        /// <returns>XNamespace</returns>
        #region getNameSpase
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
            catch (Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
            return "";
        }
        #endregion

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
            catch (Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return null;
            }
        }
        #endregion
    }
}
