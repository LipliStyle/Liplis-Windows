//=======================================================================
//  ClassName : ObjTone
//  概要      : トーン変換を行う
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Xml;

namespace Liplis.Msg
{
    [Serializable]
    public class ObjTone : XmlReadList
    {
        ///==========================
        /// リスト
        public List<string> nameList { get; set; }
        public List<string> typeList { get; set; }
        public List<string> beforList { get; set; }
        public List<string> afterList { get; set; }

        /// <summary>
        /// RssListControllerコンストラクタ
        /// 設定ファイルを読み込む
        /// </summary>
        #region ObjTone
        public ObjTone(string loadSkin)
        {
            try
            {
                xmlDoc = new XmlDocument();
                initList();

                //キャッシュファイルの取得
                xmlFilePath = LpsPathControllerCus.getToneDefinePath(loadSkin);

                //body.xmlの存在チェック
                if (LpsPathControllerCus.checkFileExist(xmlFilePath))
                {
                    //xmlの読み込み
                    readXml();
                    readResult();
                }
                else
                {
                    //リソースからデフォルトチャットファイルを読み込む
                    readXmlFromXmlstring(FctCreateFromResource.getResourceXml(LiplisDefine.TONE_RESOURCE));
                    readResult();
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, LpsPathControllerCus.getToneDefinePath(loadSkin) + "が存在しないため作成します" + Environment.NewLine + err);
                createDefault();
            }
        }
        public ObjTone()
        {
            initList();
        }
        #endregion

        /// <summary>
        /// リストの初期化
        /// </summary>
        #region initList
        protected void initList()
        {
            nameList = new List<string>();
            typeList = new List<string>();
            beforList = new List<string>();
            afterList = new List<string>();
        }
        #endregion

        /// <summary>
        /// 設定読込
        /// </summary>
        #region readResult
        protected void readResult()
        {
            readXmlList(xmlDoc.SelectNodes(LiplisDefine.TONE_NAME), nameList);
            readXmlList(xmlDoc.SelectNodes(LiplisDefine.TONE_TYPE), typeList);
            readXmlList(xmlDoc.SelectNodes(LiplisDefine.TONE_BEFOR), beforList);
            readXmlList(xmlDoc.SelectNodes(LiplisDefine.TONE_AFTER), afterList);
        }
        #endregion

        /// <summary>
        /// saveSettings
        /// リードオンリー
        /// </summary>
        #region saveSettings
        public virtual void saveSettings()
        {

        }
        #endregion

        /// <summary>
        /// 語尾変換
        /// </summary>
        /// <param name="target">対象文字列</param>
        /// <returns>結果文字列</returns>
        #region convert
        public string convert(string target)
        {
            string result = target;

            int cnt = 0;
            try
            {
                //語尾変換
                foreach (string str in nameList)
                {
                    Application.DoEvents();
                    if (cnt <= afterList.Count)
                    {
                        if (afterList[cnt] != "")
                        {
                            Regex r = new Regex(beforList[cnt]);
                            result = r.Replace(result, afterList[cnt]);
                        }
                    }
                    cnt++;
                }
                return result;
            }
            catch
            {
                return target;
            }
        }
        #endregion

        /// <summary>
        /// デフォルトファイルの作成
        /// </summary>
        #region createDefault
        protected void createDefault()
        {
            initList();
        }
        #endregion
    }
}
