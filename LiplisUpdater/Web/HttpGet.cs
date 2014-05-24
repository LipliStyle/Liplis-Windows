//=======================================================================
//  ClassName : HttpGet
//  概要      : HttpGetし、情報を取得する
//
//  Liplisちゃんシステム      
//  Copyright(c) 2010-2010 sachin.Sachin
//
//  Update : 2011/07/17 ver0.3 作成
//=======================================================================
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using Liplis.Common;

namespace Liplis.Web
{
    public static class HttpGet
    {
        public const int WEB_GET_TIMEOUT = 30000;
        public const string WEB_GET_METHOD = "GET";
        private const string WEB_POST_CONTENT_TYPE = "application/x-www-form-urlencoded";

        ///====================================================================
        ///
        ///                        パブリックメソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// ポストを送信する
        /// (UTF-8のみ)
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string getHtmlGet(string url)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequest(url);

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            //受信したデータを表示する
            return getWebResponse(req);
        }
        public static string getHtmlGet(string url, string proxy)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequestProxy(url, proxy);

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            //受信したデータを表示する
            return getWebResponse(req);
        }
        public static string getHtmlGet(string url, Encoding enc)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequest(url);

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            //受信したデータを表示する
            return getWebResponse(req, enc);
        }
        public static TextReader getHtmlGetTR(string url)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequest(url);

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            //受信したデータを表示する
            return getWebResponseStreamReader(req);
        }
        public static Stream getHtmlGetSt(string url)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequest(url);

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            //受信したデータを表示する
            return getWebResponseStream(req);
        }
        public static Stream getHtmlGetStProxy(string url, string proxy)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequestProxy(url, proxy);
            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            //受信したデータを表示する
            return getWebResponseStream(req);
        }

        /// <summary>
        /// use
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string getHtmlGetUseToken(string url, string authToken)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequest(url);

            //トークンの適用
            req.Headers.Add("Authorization", authToken);

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            //受信したデータを表示する
            return getWebResponse(req);
        }
        public static string getHtmlGetUseToken(string url, string proxy, string authToken)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequestProxy(url, proxy);

            //トークンの適用
            req.Headers.Add("Authorization", authToken);

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            //受信したデータを表示する
            return getWebResponse(req);
        }
        public static string getHtmlGetUA(string url,string userAgent)
        {
            //ウェブリクエストの取得
            HttpWebRequest req = getWebRequest(url);

            req.UserAgent = userAgent;

            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            //受信したデータを表示する
            return getWebResponse(req);
        }
        

        ///====================================================================
        ///
        ///                       GETの実行メソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// getWebRequest
        /// ウェブリクエストを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static HttpWebRequest getWebRequest(string url)
        {
            // リクエストの作成
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = WEB_GET_TIMEOUT;
            req.Method = WEB_GET_METHOD;
            req.ContentType = WEB_POST_CONTENT_TYPE;
            return req;
        }
        private static HttpWebRequest getWebRequestProxy(string url, string proxy)
        {
            // リクエストの作成
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Proxy = new WebProxy(proxy);
            req.Timeout = WEB_GET_TIMEOUT;
            req.Method = WEB_GET_METHOD;
            req.ContentType = WEB_POST_CONTENT_TYPE;
            return req;
        }

        /// <summary>
        /// getWebResponse
        /// ウェブレスポンスを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string getWebResponse(HttpWebRequest req)
        {
            using (Stream resStream = req.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(resStream, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        private static string getWebResponse(HttpWebRequest req, Encoding enc)
        {
            using (Stream resStream = req.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(resStream, enc))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        private static StreamReader getWebResponseStreamReader(HttpWebRequest req)
        {
            try
            {
                using (Stream resStream = req.GetResponse().GetResponseStream())
                {
                    return new StreamReader(resStream, Encoding.UTF8);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            
        }
        private static Stream getWebResponseStream(HttpWebRequest req)
        {
            using (Stream resStream = req.GetResponse().GetResponseStream())
            {
                return resStream;
            }
        }

        /// <summary>
        /// getWebRequest
        /// ウェブリクエストを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static void sendPostRequest(HttpWebRequest req, byte[] pramData)
        {
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(pramData, 0, pramData.Length);
            }
        }

    }
}
