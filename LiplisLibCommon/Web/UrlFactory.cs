//=======================================================================
//  ClassName : UrlFactory
//  概要      : URLを作成する
//
//  SatelliteServer
//  Copyright(c) 2009-2012 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Web
{
    public static class UrlFactory
    {

        /// <summary>
        /// YahooAPIURLを生成する
        /// </summary>
        /// <returns></returns>
        #region createGoogleUrl
        public static string createYahooApiUrl(string apiUrl, string name, string appId, string encode, string result)
        {
            return apiUrl + "?appid=" + appId + "&query=" + UrlEncoder.getUTF8(name) + "&results=" + result;
        }
        #endregion
    }
}
