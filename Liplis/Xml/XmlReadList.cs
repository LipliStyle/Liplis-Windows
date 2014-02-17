//=======================================================================
//  ClassName : XmlReadList
//  概要      : XMLを読み込むクラス
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using Liplis.Common;

namespace Liplis.Xml
{
    public class XmlReadList : XmlMost
    {
        ///=============================
        ///クラス
        protected XmlDocument xmlDoc;
        protected XmlNodeList list;
        protected XmlNamespaceManager nsmgr;

        /// <summary>
        /// readXml
        /// XMLを読み込む
        /// </summary>
        #region readXml
        protected override void readXml()
        {
            xmlDoc = new XmlDocument();

            try
            {
                //指定したXMLファイルの読み込み
                xmlDoc.Load(xmlFilePath);
            }
            catch (System.Xml.XmlException err)
            {
                //XML例外 XMLが読み込めませんでした。
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "が読み込めません");

                //エラーを呼び出し元に移管
                throw err;
            }
            catch (System.Exception)
            {
                //XMLその他例外 XMLが読み込めませんでした。
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " アクセス例外\n");
            }
        }
        #endregion


        /// <summary>
        /// readXmlFromXmlstring
        /// xmlのstringからデータを読み込む
        /// </summary>
        #region readXmlFromXmlstring
        protected void readXmlFromXmlstring(string xml)
        {
            xmlDoc = new XmlDocument();

            try
            {
                //指定したXMLファイルの読み込み
                xmlDoc.LoadXml(xml);
            }
            catch (System.Xml.XmlException err)
            {
                //XML例外 XMLが読み込めませんでした。
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "が読み込めません");

                //エラーを呼び出し元に移管
                throw err;
            }
            catch (System.Exception)
            {
                //XMLその他例外 XMLが読み込めませんでした。
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " アクセス例外\n");
            }
        }
        #endregion

        /// <summary>
        /// readXmlList
        /// XMLノードリストを読み込む
        /// </summary>
        /// <param name="pXmlList">XMLノードリスト</param>
        /// <param name="pList">対象リスト</param>
        #region readXmlList
        protected void readXmlList(XmlNodeList pXmlList, List<string> pList)
        {
            try
            {
                //リストを初期化する
                pList.Clear();

                //読み込んだノードリストを対象のリストに格納する
                foreach (XmlNode node in pXmlList)
                {
                    pList.Add(node.InnerText);
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        protected List<string> readXmlList(XmlNodeList pXmlList)
        {
            try
            {
                //リストを初期化する
                List<string> resultList = new List<string>();

                //読み込んだノードリストを対象のリストに格納する
                foreach (XmlNode node in pXmlList)
                {
                    resultList.Add(node.InnerText);
                }
                return resultList;
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return null;
            }
        }
        #endregion


        /// <summary>
        /// readXmlList
        /// XMLノードリストをINTリストに読み込む
        /// </summary>
        /// <param name="pXmlList"></param>
        /// <param name="pList"></param>
        #region readXmlListInt
        protected void readXmlListInt(XmlNodeList pXmlList, List<int> pList)
        {
            try
            {
                //リストを初期化する
                pList.Clear();

                //読み込んだノードリストを対象のリストに格納する
                foreach (XmlNode node in pXmlList)
                {
                    pList.Add(Int16.Parse(node.InnerText));
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// readXmlListTriByte
        /// 要素を指定バイト数に制限し、XMLノードリストを読み込む
        /// </summary>
        /// <param name="pXmlList">XMLノードリスト</param>
        /// <param name="pList">対象リスト</param>
        /// /// <param name="seigen">制限バイト数</param>
        #region readXmlListTriByte
        protected void readXmlListTriByte(XmlNodeList pXmlList, List<string> pList, int seigen)
        {
            try
            {
                string bufStr;
                int byt;
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                //リストを初期化する
                pList.Clear();

                //制限文字数以上であれば、切り詰めてリストに格納する
                foreach (XmlNode node in pXmlList)
                {
                    //バッファに取得しておく
                    bufStr = node.InnerText;

                    //バイト数を取得する
                    byt = sjisEnc.GetByteCount(bufStr);
                    //指定バイトまで切り詰める
                    if (byt > seigen)
                    {
                        while (byt > seigen)
                        {
                            bufStr = bufStr.Substring(0, bufStr.Length - 1);
                            byt = sjisEnc.GetByteCount(bufStr);
                        }

                    }
                    //リスト書き込み
                    pList.Add(bufStr);
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// readAttributeList
        /// n番目の属性を取得する
        /// </summary>
        /// <param name="pXmlList">XMLノードリスト</param>
        /// <param name="pList">格納するリスト</param>
        /// <param name="n">インデックス</param>
        #region readAttributeList
        protected void readAttributeList(XmlNodeList pXmlList, List<string> pList, int n)
        {
            try
            {
                //リストを初期化する
                pList.Clear();

                //読み込んだノードリストを対象のリストに格納する
                foreach (XmlNode node in pXmlList)
                {
                    pList.Add(node.Attributes[n].InnerText);
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        protected List<string> readAttributeList(XmlNodeList pXmlList, int n)
        {
            List<string> pList = new List<string>();
            try
            {
                //リストを初期化する
                pList.Clear();

                //読み込んだノードリストを対象のリストに格納する
                foreach (XmlNode node in pXmlList)
                {
                    pList.Add(node.Attributes[n].InnerText);
                }
                return pList;
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return null;
            }
        }
        #endregion

        /// <summary>
        /// readAttributeStr
        /// n番目の属性をストリングとして取得する
        /// </summary>
        /// <param name="pXmlList">XMLノードリスト</param>
        /// <param name="n">インデックス</param>
        /// <returns></returns>
        #region readAttributeStr
        protected string readAttributeStr(XmlNodeList pXmlList, int n)
        {
            try
            {
                //読み込んだノードリストを対象のリストに格納する
                foreach (XmlNode node in pXmlList)
                {
                    return node.Attributes[n].InnerText;
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
            return "";
        }
        #endregion

        /// <summary>
        /// readAttributeInt
        /// n番目の属性をイントとして取得する
        /// </summary>
        /// <param name="pXmlList">XMLノードリスト</param>
        /// <param name="n">インデックス</param>
        /// <returns>値</returns>
        #region readAttributeInt
        protected int readAttributeInt(XmlNodeList pXmlList, int n)
        {
            int put = 0;
            try
            {
                //読み込んだノードリストを対象のリストに格納する
                foreach (XmlNode node in pXmlList)
                {
                    if (int.TryParse(node.Attributes[n].InnerText, out put))
                    {
                        return int.Parse(node.Attributes[n].InnerText);
                    }
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
            return 0;
        }
        #endregion

        /// <summary>
        /// ただ一つの設定を読み込む(string)
        /// 値が取得できない、空の場合にはdefaultを返す。
        /// </summary>
        /// <param name="pXmlList">XMLノードリスト</param>
        /// <returns>値</returns>
        #region rXLMSStr
        protected string rXLMSStr(XmlNodeList pXmlList)
        {
            string result = "";

            try
            {
                foreach (XmlNode node in pXmlList)
                {
                    result = node.InnerText;
                }
                return result;
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return result;
            }
        }
        #endregion

        /// <summary>
        /// ただ一つの設定を読み込む(int)
        /// 値が取得できない、空の場合には0を返す。
        /// </summary>
        /// <param name="pXmlList">XMLノードリスト</param>
        /// <returns>値</returns>
        #region rXLMSInt
        protected int rXLMSInt(XmlNodeList pXmlList)
        {
            int result = 0;
            try
            {
                foreach (XmlNode node in pXmlList)
                {
                    try { result = int.Parse(node.InnerText); }
                    catch (System.Exception err) { LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "XML読込エラー 読込失敗。デフォルトを読み込みます。readXmlListMonoSettingInt\n" + err); }
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 一つの設定項目に値を設定する。
        /// スラッシュ区切りのXpath指定
        /// </summary>
        /// <param name="pTargetTag">対象タグ</param>
        /// <param name="pValue">値</param>
        #region wXLMSStr
        protected void wXLMSStr(string pTargetTag, string pValue)
        {
            string[] result;

            try
            {
                //XMLライタの宣言
                System.Xml.XmlWriterSettings st = new System.Xml.XmlWriterSettings();
                st.Encoding = System.Text.Encoding.UTF8;    //文字コードを指定する
                st.Indent = true;                           //インデントを指定する
                st.IndentChars = ("\t");                    //インデントにタブを指定

                System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(xmlFilePath, st);


                //pTargetTagの解析
                result = pTargetTag.Split('/');

                foreach (string path in result)
                {
                    writer.WriteStartElement(path);
                }

                //書き込み内容の設定
                writer.WriteValue(pValue);

                //終了処理
                writer.WriteEndElement();
                writer.Close();
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// すべての属性を消し、素タグにする
        /// </summary>
        /// <param name="str">対象タグ名</param>
        /// <returns>素タグにした結果</returns>
        #region killAttribute
        protected string killAttribute(string str)
        {
            string[] buf;
            string result = str;

            try
            {
                if (str.IndexOf("<") > 0)
                {
                    buf = str.Split(' ');
                    if (buf.Length > 1)
                    {
                        result = buf[0] + ">";
                    }
                    else
                    {
                        result = buf[0];
                    }
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
            return result;
        }
        #endregion

        /// <summary>
        /// リストを配列に格納する
        /// </summary>
        /// <param name="pList">対象リスト</param>
        /// <param name="strCollection">配列</param>
        #region listToMatrix
        public void listToMatrix(List<string> pList, string[] strCollection)
        {
            try
            {
                int i = 0;
                foreach (string title in pList)
                {
                    strCollection[i] = title;
                    i++;
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion
    }
}
