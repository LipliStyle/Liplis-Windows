//=======================================================================
//  ClassName : ObjLpsApiMsg
//  概要      : APIメッセージ
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Liplis.Xml;
using System.Reflection;
using Liplis.Common;
using System;

namespace Liplis.Msg
{
    class ObjLpsApiMsg : XmlReadList
    {
        public new string url { get; set; }
        public string createDate    { get; set; }
        public string source        { get; set; }
        public string convrted      { get; set; }
        public List<string> nameList { get; set; }
        public List<int> emotionList { get; set; }
        public List<int> pointList  { get; set; }


        ///==========================
        /// 名前空間
        XmlNamespaceManager nsManager;

        public ObjLpsApiMsg()
        {
        }

        /// <summary>
        /// RssListControllerコンストラクタ
        /// 設定ファイルを読み込む
        /// </summary>
        #region ObjLpsApiMsg
        public ObjLpsApiMsg(string xml)
        {
            try
            {
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
            url      = "";
            createDate  = "";
            source      = "";
            convrted    = "";
            nameList    = new List<string>();
            emotionList = new List<int>();
            pointList   = new List<int>();
        }
        #endregion

        /// <summary>
        /// 設定読込
        /// </summary>
        #region readResult
        public void readResult()
        {
            itemConvert(rXLMSStr(xmlDoc.SelectNodes(LiplisDefine.CLALIS_V20_SNEWS, nsManager)));
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
            int idx = 1;

            try
            {
                string[] wordList = xml.Split(';');

                this.url = wordList[0];

                if (wordList.Length > 1)
                {
                    for (idx = 1; idx < wordList.Length; idx++)
                    {
                        string[] bufList = wordList[idx].Split(',');
                        try
                        {
                            nameList.Add(bufList[0]);
                            emotionList.Add(int.Parse(bufList[1]));
                            pointList.Add(int.Parse(bufList[2]));
                        }
                        catch
                        {
                            if (bufList.Length > 0)
                            {
                                nameList.Add(bufList[0]);
                            }
                            else
                            {
                                nameList.Add("");
                            }
                            emotionList.Add(0);
                            pointList.Add(0);
                        }
                    }
                }

            }
            catch
            {

            }
        }
        #endregion

    }
}
