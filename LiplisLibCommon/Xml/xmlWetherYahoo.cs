//=======================================================================
//  ClassName : xmlWetherYahoo
//  概要      : ヤフー天気
//
//  SatelliteServer
//  Copyright(c) 2009-2012 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Xml;
using Liplis.Msg;
using Liplis.Common;

namespace Liplis.Xml
{
    public class xmlWetherYahoo : xmlWetherBase
    {
        /// <summary>
        /// RssListControllerコンストラクタ
        /// 設定ファイルを読み込む
        /// </summary>
        #region xmlWetherYahoo
        public xmlWetherYahoo(string url)
        {
            try
            {
                xmlDoc = new XmlDocument();
                initList();

                //キャッシュファイルの取得
                xmlFilePath = url;

                readXml();
                readResult();
                loadToMsg();

            }
            catch (System.Exception err)
            {
                lc.writingLog("xmlWetherYahoo : コンストラクター : " + url + "が存在しないため作成します\n" + err);
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
            titleList       = new List<string>();
            linkList        = new List<string>();
            descriptionList = new List<string>();
            pubDateList     = new List<string>();
            msgList         = new List<msgWetherDescription>();
        }
        #endregion

        /// <summary>
        /// 設定読込
        /// </summary>
        #region readResult
        public void readResult()
        {
            title = rXLMSStr(xmlDoc.SelectNodes(wthYahoo.TITLE));
            link = rXLMSStr(xmlDoc.SelectNodes(wthYahoo.LINK));
            lastBuildDate = rXLMSStr(xmlDoc.SelectNodes(wthYahoo.LASTBUILDDATE));
            readXmlList(xmlDoc.SelectNodes(wthYahoo.TITLE_LIST), titleList);
            readXmlList(xmlDoc.SelectNodes(wthYahoo.LINK_LIST), linkList);
            readXmlList(xmlDoc.SelectNodes(wthYahoo.DESCRIPTION_LIST), descriptionList);
            readXmlList(xmlDoc.SelectNodes(wthLiveDoor.PUBDATE_LIST), pubDateList);
        }
        #endregion


        /// <summary>
        /// メッセージにロードする
        /// </summary>
        #region loadToMsg
        private void loadToMsg()
        {
            int cnt = 0;

            foreach (string title in titleList)
            {
                try
                {
                    string[] arytitle = title.Split(' ');
                    string[] day = arytitle[1].Split('日');
                    string[] tmp = arytitle[6].Split('/');
                    int max = int.Parse(tmp[0].Replace("℃",""));
                    int min = int.Parse(tmp[1].Replace("℃",""));
                    string date = dateString(day[0]);

                    //レギオンの設定

                    //メッセージ生成
                    msgList.Add(new msgWetherDescription(date, arytitle[2], 0, LpsLiplisUtil.getYobi(date), arytitle[4], max, min, 0, "", linkList[cnt]));

                    Console.WriteLine(arytitle[4] + " : " + linkList[cnt]);
                }
                catch
                {

                }
                finally
                {
                    cnt++;
                }
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


        /// <summary>
        /// dateString
        /// デートストリングを生成する
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        #region dateString
        private string dateString(string day)
        {
            string result = "";

            int dayInt = int.Parse(day);
            DateTime d = DateTime.Now;

            //1日判定
            if (dayInt == 1)
            {
                //システムデイトと指定デイトが違った場合は来月と判断する
                if (d.Day != dayInt)
                {
                    result = d.AddDays(1).ToString("yyyy/MM/dd 00:00:00");
                }
                else
                {
                    result = d.ToString("yyyy/MM/dd 00:00:00");
                }
            }
            else
            {
                result = d.Year + "/" + d.Month + "/" + day + " 00:00:00";
            }


            return result;
        }
        #endregion

    }

    public class wthYahoo
    {
        //登録RSS
        public const string TITLE = "/rss/channel/title";
        public const string LINK = "/rss/channel/link";
        public const string LASTBUILDDATE = "/rss/channel/lastBuildDate";
        public const string TITLE_LIST = "/rss/channel/item/title";
        public const string LINK_LIST = "/rss/channel/item/link";
        public const string DESCRIPTION_LIST = "/rss/channel/item/description";
    }
}
