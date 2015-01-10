//=======================================================================
//  ClassName : ResLpsTalkResponse
//  概要      : おしゃべり応答
//
//  SatelliteServer
//  Copyright(c) 2009-2015 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;

namespace Liplis.Msg
{
    public class ResLpsChatResponse
    {
        ///=============================
        /// プロパティ
        public string title { get; set; }
        public string url { get; set; }
        public string jpgUrl { get; set; }
        public List<string> descriptionList { get; set; }
        public List<string> opList { get; set; }
        public bool already { get; set; }

        #region ResLpsTalkResponse
        public ResLpsChatResponse()
        {
            descriptionList = new List<string>();
            opList = new List<string>();
        }
        #endregion

        #region ResLpsTalkResponse(Int64 idx, string title, string url, string jpgUrl, List<string> descriptionList)
        public ResLpsChatResponse(Int64 idx, string title, string url, string jpgUrl, List<string> descriptionList, List<string> opList, bool already)
        {
            this.title = title;
            this.url = url;
            this.jpgUrl = jpgUrl;
            this.descriptionList = descriptionList;
            this.opList = opList;
            this.already = already;
        }
        #endregion
    }
}
