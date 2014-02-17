//=======================================================================
//  Clalis3.1
//  ClassName : RegistTwUserInfo
//  概要      : ツイッターユーザー情報
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin. All Rights Reserved. 
//=======================================================================

namespace Liplis.Msg
{
    public class RegisterTwUserInfo
    {
        #region プロパティ
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        #endregion

        #region コンストラクター
        public RegisterTwUserInfo()
        {
        }
        public RegisterTwUserInfo(string name, string url, string description, string iconUrl)
        {
            this.name = name;
            this.url = url;
            this.description = description;
            this.iconUrl = iconUrl;
        }
        #endregion
    }
}
