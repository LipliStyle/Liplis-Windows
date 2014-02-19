//=======================================================================
//  ClassName : WetherInfoYahoo
//  概要      : ウェザーインフォヤフー
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin.Sachin
//=======================================================================
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Liplis.Common;
using Liplis.Msg;
using Liplis.Xml;

namespace Liplis.Web.WetherInfo
{
    public class WetherInfoLiveDoor
    {
        ///=============================
        /// URL
        private const string LIVEDOOR_WTH_URL = "http://weather.livedoor.com/weather_hacks/rss_feed_list.html";
        private const string LIVEDOOR_WTH_RSS = "http://weather.livedoor.com/forecast/rss/";

        ///=============================
        /// プロパティ
        public List<string> nameLiset { get; set; }
        public List<string> wetherUrlList { get; set; }
        public List<string> warnUrlList { get; set; }

        public List<xmlWetherBase> wetherXmlList { get; set; }


        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WetherInfoLiveDoor
        public WetherInfoLiveDoor()
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
        }
        #endregion

        /// <summary>
        /// URLリストのコレクト
        /// </summary>
        #region collectUrlList
        public void collectUrlList()
        {
            //LinqでURL抽出
            var q = from Match m in Regex.Matches(new HtmlParser().getHtmlSource(LIVEDOOR_WTH_URL), @"href=""(?<url>.*?)""")
                    where m.Success
                    select m.Groups["url"];

            //結果を得る
            foreach (var x in q)
            {
                string url = x.ToString();
                if (url.StartsWith(LIVEDOOR_WTH_RSS))
                {
                    wetherUrlList.Add(url);
                    wetherXmlList.Add(new xmlWetherLiveDoor(url));
                }
            }
            wetherUrlList.Sort();

            foreach (xmlWetherLiveDoor yfo in wetherXmlList)
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
