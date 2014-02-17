//=======================================================================
//  ClassName : WebClientEx
//  概要      : WebClientEx
//
//  WebClientEx
//  Copyright(c) 2009-2011 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Liplis.Web
{
    public class WebClientCookieCoustom : WebClient
    {
        private CookieContainer cookieContainer;

        public CookieContainer CookieContainer
        {
            get
            {
                return cookieContainer;
            }
            set
            {
                cookieContainer = value;
            }
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest webRequest = base.GetWebRequest(uri);

            if (webRequest is HttpWebRequest)
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)webRequest;
                httpWebRequest.CookieContainer = this.cookieContainer;
            }

            return webRequest;
        }
    }
}
