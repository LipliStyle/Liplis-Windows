//=======================================================================
//  ClassName : WetherInfoYahoo
//  概要      : ウェザーインフォヤフー
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Liplis.Common;
using Liplis.Msg;
using Liplis.Xml;
using Liplis.Sql;
using System;

namespace Liplis.Web.WetherInfo
{
    public class WetherInfoYahoo
    {
        ///=============================
        /// URL
        private const string YAHOO_WTH_URL = "http://weather.yahoo.co.jp/weather/rss/";
        private const string YAHOO_WTH_RSS = "http://rss.weather.yahoo.co.jp/rss/days";
        private const string YAHOO_WTH_WARN = "http://rss.weather.yahoo.co.jp/rss/warn";

        ///=============================
        /// プロパティ
        public List<string> nameLiset{get;set;}
        public List<string> wetherUrlList { get; set; }
        public List<string> warnUrlList { get; set; }

        public List<xmlWetherBase> wetherXmlList { get; set; }
        public List<xmlWetherBase> warnXmlList { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WetherInfoYahoo
        public WetherInfoYahoo()
        {
            //リスト初期化
            initList();

            //URLリストを収集する
            collectUrlList();
        }
        #endregion

        /// <summary>
        /// リストの初期化
        /// </summary>
        #region initList
        public void initList()
        {
            nameLiset     = new List<string>();
            wetherUrlList = new List<string>();
            warnUrlList   = new List<string>();
            wetherXmlList = new List<xmlWetherBase>();
            warnXmlList = new List<xmlWetherBase>();
        }
        #endregion

        /// <summary>
        /// URLリストのコレクト
        /// </summary>
        #region collectUrlList
        public void collectUrlList()
        {
            //LinqでURL抽出
            var q = from Match m in Regex.Matches(new HtmlParser().getHtmlSource(YAHOO_WTH_URL), @"href=""(?<url>.*?)""")
                    where m.Success
                    select m.Groups["url"];

            //結果を得る
            foreach (var x in q)
            {
                string url = x.ToString();
                
                if (url.StartsWith(YAHOO_WTH_RSS))
                {
                    wetherUrlList.Add(url);
                    wetherXmlList.Add(new xmlWetherYahoo(url));
                }
                else if (url.StartsWith(YAHOO_WTH_WARN))
                {
                    warnUrlList.Add(url);
                    warnXmlList.Add(new xmlWetherYahoo(url));
                }
                Console.WriteLine(x);
            }
            wetherUrlList.Sort();

            foreach(xmlWetherYahoo yfo in wetherXmlList)
            {
                foreach(msgWetherDescription msg in yfo.msgList)
                {
                    if (nameLiset.IndexOf(msg.region) < 0)
                    {
                        nameLiset.Add(msg.region);
                    }
                }
            }            
        }
        #endregion

        /// <summary>
        /// テスト出力メソッド
        /// </summary>
        /// <param name="nameList"></param>
        /// <returns></returns>
        protected bool writeFile(List<string> nameList)
        {
            try
            {
                string filePath = LpsPathController.getAppPath() + "\\" + LpsLiplisUtil.getName(10) + ".txt";

                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
                {
                    foreach (string str in nameList)
                    {
                        w.WriteLine(str);
                    }

                    w.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
