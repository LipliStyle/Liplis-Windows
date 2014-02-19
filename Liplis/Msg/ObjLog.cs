//=======================================================================
//  ClassName : ObjLog
//  概要      : ログオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Msg
{
    public class ObjLog
    {
        ///============================
        /// RSS情報
        public string title { get; set; }
        public string url { get; set; }
        public string cat { get; set; }
        public string jpgUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        #region ObjRss
        public ObjLog()
        {

        }
        public ObjLog(string title, string url, string cat, string jpgUrl)
        {
            this.title = title;
            this.url = url;
            this.cat = cat;
            this.jpgUrl = jpgUrl;
        }
        #endregion
    }
}
