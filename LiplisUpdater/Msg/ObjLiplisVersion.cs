//=======================================================================
//  ClassName : ObjLiplisVersion
//  概要      : バージョン
//
//  Liplis2.3
//  2013/06/20 Liplis2.3.0 UI変更
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Reflection;
using System.Xml;
using Liplis.Common;
using Liplis.Xml;

namespace Liplis.Msg
{
    public class ObjLiplisVersion : XmlReadList
    {
        ///=============================
        /// バージョン 追加
        public string skinVersion;
        public string liplisMinVersion;
        public string versionUrl;
        public string skinUrl;

        ///=============================
        /// バージョンチェック有無 Noralisでこのフラグを操作する。
        private bool flgCheckOn = false;

        /// <summary>
		/// RssListControllerコンストラクタ
		/// 設定ファイルを読み込む
		/// </summary>
		#region ObjLiplisVersion
		public ObjLiplisVersion(string loadSkin)
		{
			try
			{
				xmlDoc = new XmlDocument();

				//キャッシュファイルの取得
                xmlFilePath = LpsPathControllerCus.getSkinPath() + loadSkin + "\\define\\version.xml";

				//body.xmlの存在チェック
				if (LpsPathControllerCus.checkFileExist(xmlFilePath))
				{
					//xmlの読み込み
					readXml();
					readResult();
				}
				else
				{
					loadDefault();
				}

			}
			catch (System.Exception err)
			{
				LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
			}
		}
        public ObjLiplisVersion(string loadSkin, string url)
        {
            try
            {
                downLoadXml("tmp.xml", url);
                readXml();
                readResult();
            }
            catch (System.Exception err)
            {
                loadDefault();
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        public ObjLiplisVersion()
		{
			loadDefault();
		}
		#endregion

        /// <summary>
        /// デフォルトロード
        /// </summary>
        #region loadDefault
        private void loadDefault()
        {
            skinVersion = "";
            liplisMinVersion = "";
            versionUrl = "";
            skinUrl = "";
        }
        #endregion

        /// <summary>
		/// readResult
		/// 設定読込
		/// </summary>
		#region readResult
		public void readResult()
		{
            skinVersion = rXLMSStr(xmlDoc.SelectNodes(LiplisDefine.VERSION_SKIN));
            liplisMinVersion = rXLMSStr(xmlDoc.SelectNodes(LiplisDefine.VERSION_MIN));
            versionUrl = rXLMSStr(xmlDoc.SelectNodes(LiplisDefine.VERSION_URL));
            skinUrl = rXLMSStr(xmlDoc.SelectNodes(LiplisDefine.VERSION_SKIN_URL));
		}
		#endregion

        /// <summary>
        /// チェックオンを取得する
        /// </summary>
        public bool getFlgCheckOn()
        {
            return this.flgCheckOn;
        }

    }
}
