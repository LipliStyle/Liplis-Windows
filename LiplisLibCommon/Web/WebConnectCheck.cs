//=======================================================================
//  ClassName : webConnectCheck
//  概要      : Web接続チェッククラス
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin.Sachin
//=======================================================================
using System;
using System.Net;
using Liplis.Xml;

namespace Liplis.Web
{
    public class WebConnectCheck
    {
        /// <summary>
        /// ページが存在するかチェック
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool check(string url)
        {
            WebRequest.DefaultWebProxy = null; // プロキシ未使用を明示

            if (GetStatusCode(url) >= 400)
            { // 4xx、5xxはアクセス失敗とする
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 対象のサイトのステータスコードを返す
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        //public static HttpStatusCode GetStatusCode(string url)
        public static int GetStatusCode(string url)
        {
            HttpWebRequest req;
            HttpWebResponse res = null;
            HttpStatusCode statusCode;

            try
            {
                req = (HttpWebRequest)WebRequest.Create(url);
                res = (HttpWebResponse)req.GetResponse();
                statusCode = res.StatusCode;

            }
            catch (WebException ex)
            {

                res = (HttpWebResponse)ex.Response;

                if (res != null)
                {
                    statusCode = res.StatusCode;
                }
                else
                {
                    return 901;
                }
            }
            catch (System.UriFormatException)
            {
                //長杉
                return 902;
            }
            catch(System.Exception)
            {
                return 999;
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                }
            }

            //結果をintに変換して返す
            return (int)statusCode; ;
        }


        /// <summary>
        /// 対象のXMLが有効かどうか判断する
        /// </summary>
        /// <param name="url"></param>
        public static bool checkRss(string url)
        {
            string title = "";
            try
            {
                RssReader rr = new RssReader(url);
                title = rr.title;
                rr.Dispose();

                return title != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// リプリスのアップデートファイルチェック
        /// </summary>
        /// <returns></returns>
        public static bool checkLiplisUpdateFile()
        {
            return false;
        }

    }
}
