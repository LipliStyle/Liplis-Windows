//=======================================================================
//  Clalis3.1
//  ClassName : RegisterRsCatUserInfo
//  概要      : RSSカテゴリユーザー情報
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin.Sachin
//=======================================================================
using System.Collections.Generic;

namespace Liplis.Msg
{
    public class RegisterRsUserInfoCat
    {
        #region プロパティ
        ///=============================
        ///プロパティ
        public string cat { get; set; }
        public List<RegisterRsUserInfo> rsslist { get; set; }
        #endregion

        #region コンストラクター
        public RegisterRsUserInfoCat()
        {
            rsslist = new List<RegisterRsUserInfo>();
        }
        public RegisterRsUserInfoCat(string cat)
        {
            this.cat = cat;
            this.rsslist = new List<RegisterRsUserInfo>(); ;
        }
        public RegisterRsUserInfoCat(string cat, List<RegisterRsUserInfo> rsslist)
        {
            this.cat = cat;
            this.rsslist = rsslist;
        }
        #endregion
    }
}
