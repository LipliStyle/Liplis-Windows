//=======================================================================
//  ClassName : ObjLpsApiMsg
//  概要      : APIメッセージ
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Liplis.Common;
using Liplis.Web;
using Liplis.Xml;

namespace Liplis.Msg
{
    class ObjLpsNewsSum : XmlReadList
    {
        ///==========================
        /// プロパティ
        public new string url           { get; set; }
        public string title             { get; set; }
        public string source            { get; set; }
        public string converted         { get; set; }
        public string befor             { get; set; }
        public string jpgUrl            { get; set; }
        public List<string> nameList    { get; set; }
        public List<int> emotionList    { get; set; }
        public List<int> pointList      { get; set; }
        public List<int> posList        { get; set; }
        private ObjTone tone;

        ///==========================
        /// 名前空間
        XmlNamespaceManager nsManager;

        public ObjLpsNewsSum()
        {
        }

        /// <summary>
        /// RssListControllerコンストラクタ
        /// 設定ファイルを読み込む
        /// </summary>
        #region ObjLpsNewsSum
        public ObjLpsNewsSum(string xml, ObjTone tone)
        {
            try
            {
                this.tone = tone;
                xmlDoc = new XmlDocument();

                nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
                nsManager.AddNamespace("p", "http://Clalis.Api");


                initList();

                xmlDoc.Load(new StringReader(xml));
                readResult();

            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// リストの初期化
        /// </summary>
        #region initList
        private void initList()
        {
            url         = "";
            title       = "";
            source      = "";
            jpgUrl      = "";
            nameList    = new List<string>();
            emotionList = new List<int>();
            pointList   = new List<int>();
            posList     = new List<int>();
        }
        #endregion

        /// <summary>
        /// 設定読込
        /// </summary>
        #region readResult
        public void readResult()
        {
            url = rXLMSStr(xmlDoc.SelectNodes(LiplisDefine.CLALIS_V20_SUMNEWS_URL, nsManager));
            title = rXLMSStr(xmlDoc.SelectNodes(LiplisDefine.CLALIS_V20_SUMNEWS_TITLE, nsManager));
            jpgUrl = rXLMSStr(xmlDoc.SelectNodes(LiplisDefine.CLALIS_V20_SUMNEWS_JPG_URL, nsManager));
            source = rXLMSStr(xmlDoc.SelectNodes(LiplisDefine.CLALIS_V20_SUMNEWS_RESULT, nsManager));

            //ソースが空でタイトルが入っていたらタイトルを入れておく
            //2011/11/26
            if (source.Equals("") && !title.Equals("")){source = title　+ ",0,0,0;";}

            itemConvert(source);

            //デモ用データの生成
            //#test
            //createDemoData();
        }
        #endregion

        /// <summary>
        /// itemConvert
        /// ストリングをメッセージすりとに変換する
        /// </summary>
        /// <param name="xml">取得データ</param>
        /// <returns>MsgShortNews</returns>
        #region itemConvert
        private void itemConvert(string xml)
        {
            int idx = 0;

            //トーンコンバート
            converted = tone.convert(getSource(xml));	

            try
            {
                string[] wordList = xml.Split(';');

                //全文取得と割り振り
                if (wordList.Length > 1)
                {
                    for (idx = 0; idx < wordList.Length; idx++)
                    {
                        string[] bufList = wordList[idx].Split(',');
                        try
                        {
                            nameList.Add(bufList[0]);
                            emotionList.Add(int.Parse(bufList[1]));
                            pointList.Add(int.Parse(bufList[2]));
                            posList.Add(int.Parse(bufList[3]));
                        }
                        catch
                        {
                            if (bufList.Length > 0)
                            {
                                //nameList.Add(bufList[0]);
                            }
                            else
                            {
                                nameList.Add("");
                            }
                            emotionList.Add(0);
                            pointList.Add(0);
                            posList.Add(0);
                        }
                    }
                }
            }
            catch
            {
                LpsLogControllerCus.d("itemConvert");
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        #region getSource
        private string getSource(string source)
        {
            StringBuilder result = new StringBuilder();
            int idx = 0;
            try
            {
                string[] wordList = source.Split(';');

                if (wordList.Length > 1)
                {
                    for (idx = 0; idx < wordList.Length; idx++)
                    {
                        string[] bufList = wordList[idx].Split(',');
                        try
                        {
                            result.Append(bufList[0]);
                        }
                        catch
                        {
                            result.Append("");
                        }
                    }
                }

            }
            catch
            {

            }

            return result.ToString();
        }
        #endregion

        /// <summary>
        /// でもデータの作成メソッド
        /// </summary>
        #region createDemoData
        private void createDemoData()
        {
            //デモ用データ作成
            System.IO.File.AppendAllText(LpsPathController.getAppPath() + "\\log\\liplis.dmo" + "", title.Replace("*", "＊") + "*" + url + "*" + source.Replace("*", "＊") + Environment.NewLine, Encoding.GetEncoding(932));

            StringBuilder res = new StringBuilder();
            StringBuilder res2 = new StringBuilder();

            MsgShortNews r = LiplisApiCus.getShortNews("http://liplis.mine.nu/xml/Tone/LiplisKudo.xml");

            int idx = 0;

            res.Append("<string>");
            foreach (string name in r.nameList)
            {
                res.Append(name + "," + r.emotionList[idx] + "," + r.pointList[idx] + ";");
                res2.Append(name);
                idx++;
            }
            res.Append("</string>" + Environment.NewLine);
            res2.Append(Environment.NewLine);

            System.IO.File.AppendAllText(LpsPathController.getAppPath() + "\\log\\demo.xml",res.ToString(), Encoding.GetEncoding(932));
            System.IO.File.AppendAllText(LpsPathController.getAppPath() + "\\log\\test.txt", res2.ToString(), Encoding.GetEncoding(932));
        }
        #endregion

    }
}
