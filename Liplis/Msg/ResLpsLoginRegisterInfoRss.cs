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
    class ResLpsLoginRegisterInfoRss
    {
        ///=============================
        ///プロパティ
        public List<RegisterRsUserInfo> rsslist { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsLoginRegisterInfoRss
        public ResLpsLoginRegisterInfoRss()
        {
            this.rsslist = new List<RegisterRsUserInfo>();
        }
        public ResLpsLoginRegisterInfoRss(List<RegisterRsUserInfo> rsslist)
        {
            this.rsslist = rsslist;
        }
        #endregion
    }
}
