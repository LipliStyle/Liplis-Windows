//=======================================================================
//  ClassName : objTone
//  概要      : トーンリスト
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin.Sachin
//=======================================================================
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using Liplis.Common;

namespace Liplis.Xml
{
    public class XmlTone : XmlReadList
    {
        ///=============================
        /// XMLファイル定義
        public const string SETTING_SKIN_FILE_NAME = "skin.xml";
        public const string SETTING_TONE_XML_FILE = "tone.xml";

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
        #region XmlTone
        public XmlTone()
        {
            try
            {
                xmlDoc = new XmlDocument();
                initList();

                //キャッシュファイルの取得
                xmlFilePath = LpsPathController.getAppPath() + "\\" + SETTING_TONE_XML_FILE;

                readXml();
                readResult();

            }
            catch (System.Exception err)
            {
                lc.writingLog("WindowListController : コンストラクター : " + SETTING_TONE_XML_FILE + "が存在しないため作成します\n" + err);
                createDefault();
            }
        }
        public XmlTone(string toneUrl)
        {
            try
            {
                initList();

                //キャッシュファイルの取得
                xmlFilePath = toneUrl;

                readXml();
                readResult();

            }
            catch (System.Exception err)
            {
                lc.writingLog("WindowListController : コンストラクター : " + SETTING_TONE_XML_FILE + "が存在しないため作成します\n" + err);
                createDefault();
            }
        }

        #endregion

        /// <summary>
        /// リストの初期化
        /// </summary>
        #region initList
        private void initList()
        {
            nameList = new List<string>();
            typeList = new List<string>();
            beforList = new List<string>();
            afterList = new List<string>();
        }
        #endregion

        /// <summary>
        /// readResult
        /// 設定読込
        /// </summary>
        #region readResult
        public void readResult()
        {
            readXmlList(xmlDoc.SelectNodes(tnxp.NAME), nameList);
            readXmlList(xmlDoc.SelectNodes(tnxp.TYPE), typeList);
            readXmlList(xmlDoc.SelectNodes(tnxp.BEFOR), beforList);
            readXmlList(xmlDoc.SelectNodes(tnxp.AFTER), afterList);
        }
        #endregion

        /// <summary>
        /// saveSettings
        /// リードオンリー
        /// </summary>
        #region saveSettings
        public void saveSettings()
        {

        }
        #endregion

        /// <summary>
        /// convert
        /// 語尾変換
        /// </summary>
        /// <param name="target"></param>
        #region convert
        public string convert(string target)
        {
            string result = target;
            //lc.writingLog(target + "\n");
            int cnt = 0;
            try
            {
                //語尾変換
                foreach (string str in nameList)
                {
                    System.Windows.Forms.Application.DoEvents();
                    if (cnt <= afterList.Count)
                    {
                        if (afterList[cnt] != "")
                        {
                            Regex r = new Regex(beforList[cnt]);
                            result = r.Replace(result, afterList[cnt]);
                            r = null;
                        }
                    }
                    cnt++;
                }
                //lc.writingLog(result + "\n");
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
        public void createDefault()
        {
            initList();
        }
        #endregion

    }

    #region tnxp
    public class tnxp
    {
        //登録RSS
        public const string NAME = "/tone/toneDiscription/name";
        public const string TYPE = "/tone/toneDiscription/type";
        public const string BEFOR = "/tone/toneDiscription/befor";
        public const string AFTER = "/tone/toneDiscription/after";
    }
    #endregion


}
