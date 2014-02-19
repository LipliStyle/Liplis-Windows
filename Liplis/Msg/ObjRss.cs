//=======================================================================
//  ClassName : ObjRss
//  概要      : RSSオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using Liplis.Xml;

namespace Liplis.Msg
{
    [Serializable]
    public class ObjRss
    {
        ///============================
        /// RSS情報
        public string title { get; set; }
        public string url { get; set; }
        public string cat { get; set; }
        public List<MsgShortNews> topicList { get; set; }


        /// <summary>
        /// 
        /// </summary>
        #region ObjRss
        public ObjRss()
        {

        }
        public ObjRss(string title, string url, string cat)
        {
            this.title = title;
            this.url = url;
            this.cat = cat;
            this.topicList = new List<MsgShortNews>();
        }
        #endregion

        /// <summary>
        /// updateTopicList
        /// 記事リストを更新する
        /// </summary>
        #region updateTopicList
        public void updateTopicList(RssReader rr)
        {
            int idx = 0;

            //更新件数チェック
            if (rr.urlList.Count <= 0)
            {
                return;
            }

            //ヌチェック
            if (topicList == null) { topicList = new List<MsgShortNews>(); }

            //URL1件目チェック
            if (this.topicList.Count > 0)
            {
                //1件目が同じなら未更新と判断
                if (this.topicList[0].url.Equals(rr.urlList[0]))
                {
                    return;
                }
            }

            List<MsgShortNews> newTopicList = new List<MsgShortNews>();

            //更新する
            foreach (string url in rr.urlList)
            {
                MsgShortNews n = new MsgShortNews();
                try
                {
                    n.url = url;
                    n.title = rr.urlTitleList[idx];
                }
                catch
                {
                    Console.WriteLine("ニュース収集エラー");
                }

                newTopicList.Add(n);

                idx++;
            }

            //更新完了
            topicList = newTopicList;
        }
        #endregion

    }
}
