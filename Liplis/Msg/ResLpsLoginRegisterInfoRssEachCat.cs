//=======================================================================
//  Clalis3.1
//  ClassName : ResLpsLoginRegisterInfoRss
//  概要      : リプリス設定情報RSS
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Liplis.Msg
{
    public class ResLpsLoginRegisterInfoRssEachCat
    {
        ///=============================
        ///プロパティ
        public List<RegisterRsUserInfoCat> rsslist { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsLoginRegisterInfoRssEachCat
        public ResLpsLoginRegisterInfoRssEachCat()
        {
            this.rsslist = new List<RegisterRsUserInfoCat>();
        }
        public ResLpsLoginRegisterInfoRssEachCat(List<RegisterRsUserInfoCat> rsslist)
        {
            this.rsslist = rsslist;
        }
        #endregion
    }
}
