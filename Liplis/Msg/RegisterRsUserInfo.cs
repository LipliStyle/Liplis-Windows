//=======================================================================
//  Clalis3.1
//  ClassName : RegisterRsUserInfo
//  概要      : RSSユーザー情報
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin. All Rights Reserved. 
//=======================================================================


namespace Liplis.Msg
{
    public class RegisterRsUserInfo
    {
        #region プロパティ
        public string url { get; set; }
        public string title { get; set; }
        public string cat { get; set; }
        #endregion

        #region コンストラクター
        public RegisterRsUserInfo()
        {
        }
        public RegisterRsUserInfo(string url, string title, string cat)
        {
            this.url = url;
            this.title = title;
            this.cat = cat;
        }
        #endregion
    }
}
