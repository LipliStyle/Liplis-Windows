//=======================================================================
//  ClassName : ObjSetting
//  概要      : 設定オブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liplis.Xml;
using Liplis.Common;
using System.Reflection;

namespace Liplis.Msg
{
    public class ObjVersion : XmlSettingObject
    {
        ///=============================
        ///プロパティ
        public string version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ObjVersion(string versionFilePath)
        {
            //設定の取得
            setting = new SharedPreferences(versionFilePath);

            getPreferenceData();

        }

        /// <summary>
        /// getPreferenceData
        /// プリファレンスデータの取得
        /// </summary>
        #region getPreferenceData
        public override void getPreferenceData()
        {
            try
            {
                //メイン設定の読込
                version = setting.getString(LiplisDefine.PREFS_VERSION, Assembly.GetExecutingAssembly().GetName().Version.ToString());
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "readResult:設定の読込失敗" + Environment.NewLine + err);
            }

        }
        #endregion

        /// <summary>
        /// setPreferenceData
        /// セーブ
        /// </summary>
        #region setPreferenceData
        public override void setPreferenceData()
        {
            setting.setString(LiplisDefine.PREFS_VERSION, this.version);
            setting.saveSettings();
        }
        #endregion
    }
}
