//=======================================================================
//  ClassName : MsgSumNws
//  概要      : ニュースサマリーメッセージ
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Liplis.Msg
{
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class ResLpsSummaryNews2Json
    {
        ///=============================
        /// プロパティ
        public int news_id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string jpgUrl { get; set; }
        public List<string> descriptionList { get; set; }

        #region ResLpsSummaryNews2Json
        public ResLpsSummaryNews2Json()
        {
            descriptionList = new List<string>();
        }
        #endregion

        #region ResLpsSummaryNews2Json(int news_id, string title, string url, string jpgUrl, List<string> descriptionList)
        public ResLpsSummaryNews2Json(int news_id, string title, string url, string jpgUrl, List<string> descriptionList)
        {
            this.news_id = news_id;
            this.title = title;
            this.url = url;
            this.jpgUrl = jpgUrl;
            this.descriptionList = descriptionList;
        }
        #endregion
    }
}
