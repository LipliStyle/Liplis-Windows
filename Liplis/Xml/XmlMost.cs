//=======================================================================
//  ClassName : XmlMost
//  概要      : XMLコントローラの規定クラス
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================

using System;
using System.Net;
using System.Reflection;
using Liplis.Common;

namespace Liplis.Xml
{
    public abstract class XmlMost
    {
        ///=============================
        ///クラス
        protected LiplisLog lc;

        ///=============================
        ///キャッシュファイルパス
        protected string xmlFilePath;
        protected string url;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public XmlMost()
        {
            lc = new LiplisLog();
        }

        /// <summary>
        /// downLoadXml
        /// XMLをダウンロードする
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <param name="url">URL</param>
        #region downLoadXml
        public void downLoadXml(string fileName, string pUrl)
        {
            try
            {
                WebClient wc = new WebClient();
                xmlFilePath = LpsPathControllerCus.getXmlPath() + fileName;
                url = pUrl;

                wc.DownloadFile(pUrl, xmlFilePath);
            }
            catch (System.Net.WebException)
            {
                //XML例外 XMLが読み込めませんでした。
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Web例外 XMLが読み込めませんでした。サーバーが存在しない可能性があります。" + Environment.NewLine);
            }
            catch (System.Exception err)
            {
                //ここでスルーするか
                //LiplisLog.writingLog("XmlMost : downLoadXml\n" + err);
                throw err;  //下層に投げる
            }
        }
        #endregion

        /// <summary>
        /// readXml
        /// XMLを読み込む
        /// </summary>
        protected abstract void readXml();

    }
}
