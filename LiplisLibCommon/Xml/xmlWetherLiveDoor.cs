//=======================================================================
//  ClassName : xmlWetherYahoo
//  概要      : ヤフー天気
//
//  SatelliteServer
//  Copyright(c) 2009-2012 sachin.Sachin
//=======================================================================
using System.Collections.Generic;
using System.Xml;
using Liplis.Msg;
using System;
using System.Text.RegularExpressions;
using Liplis.Common;

namespace Liplis.Xml
{
    public class xmlWetherLiveDoor : xmlWetherBase
    {
        ///==========================
        /// リスト
        public string description                   { get; set; }

        /// <summary>
        /// RssListControllerコンストラクタ
        /// 設定ファイルを読み込む
        /// </summary>
        #region xmlWetherYahoo
        public xmlWetherLiveDoor(string url)
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
            title = rXLMSStr(xmlDoc.SelectNodes(wthLiveDoor.TITLE));
            link = rXLMSStr(xmlDoc.SelectNodes(wthLiveDoor.LINK));
            description = rXLMSStr(xmlDoc.SelectNodes(wthLiveDoor.DESCRIPTION));
            lastBuildDate = rXLMSStr(xmlDoc.SelectNodes(wthLiveDoor.LASTBUILDDATE));
            readXmlList(xmlDoc.SelectNodes(wthLiveDoor.TITLE_LIST), titleList);
            readXmlList(xmlDoc.SelectNodes(wthLiveDoor.LINK_LIST), linkList);
            readXmlList(xmlDoc.SelectNodes(wthLiveDoor.DESCRIPTION_LIST), descriptionList);
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
                    DateTime d = DateTime.Now;

                    if (arytitle.Length == 10)
                    {
                        string[] dayAry = arytitle[9].Remove(0, arytitle[9].IndexOf("月") + 1).Split('日');
                        string[] tmpAry = arytitle[6].Split('/');

                        Regex r = new Regex(@"[0-9]+℃", RegexOptions.IgnoreCase);

                        //次のように一致する対象をすべて検索することもできる
                        MatchCollection mc = r.Matches(descriptionList[cnt]);

                        int max = int.Parse(mc[0].ToString().Replace("℃", ""));
                        int min = int.Parse(mc[1].ToString().Replace("℃", ""));

                        string date = dateString(arytitle[9]);

                        //メッセージ生成
                        msgList.Add(new msgWetherDescription(date, arytitle[3],0, LpsLiplisUtil.getYobi(date), arytitle[5], max, min, 0,"", linkList[cnt]));
                        Console.WriteLine(arytitle[3] + " : " + linkList[cnt]);
                    }
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
            DateTime d = DateTime.Now;

            return d.Year + "/" + day.Remove(day.IndexOf("("), day.Length - day.IndexOf("(")).Replace("月", "/").Replace("日", "") + " 00:00:00";
        }
        #endregion


    }

    public class wthLiveDoor
    {
        //登録RSS
        public const string TITLE = "/rss/channel/title";
        public const string LINK = "/rss/channel/link";
        public const string LASTBUILDDATE = "/rss/channel/lastBuildDate";
        public const string DESCRIPTION = "/rss/channel/description";
        public const string TITLE_LIST = "/rss/channel/item/title";
        public const string LINK_LIST = "/rss/channel/item/link";
        public const string DESCRIPTION_LIST = "/rss/channel/item/description";
        public const string PUBDATE_LIST = "/rss/channel/item/pubDate";
    }
}
