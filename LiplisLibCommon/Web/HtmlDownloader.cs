//=======================================================================
//  ClassName : HtmlDownloder
//  概要      : HTMLファイルの取得
//
//  Liplisシステム      
//  Copyright(c) 2010-2011 sachin. All Rights Reserved. 
//=======================================================================;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Liplis.Web
{
    public class HtmlDownloader
    {
        /// <summary>
        /// HTMLファイルを保存する
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        #region downLoadSaveFile
        public static bool downLoadSaveFile(string url, string fileName)
        {
            string status = "";
            try
            {
                //WebRequestの作成
                HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(url);
                webreq.Timeout = 5000;

                HttpWebResponse webres = null;
                try
                {
                    //サーバーからの応答を受信するためのWebResponseを取得
                    webres = (HttpWebResponse)webreq.GetResponse();

                    //応答したURIを表示する
                    //Console.WriteLine(webres.ResponseUri);
                    //応答ステータスコードを表示する
                    status = webres.StatusCode + " " + webres.StatusDescription;

                    //シュトリームに出力
                    Encoding sjis = Encoding.GetEncoding("Shift-JIS");
                    StreamWriter w = new StreamWriter(fileName, false, sjis);
                    w.Write(webres.GetResponseStream());

                    w.Close();

                }
                catch (System.Net.WebException ex)
                {
                    //HTTPプロトコルエラーかどうか調べる
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        //HttpWebResponseを取得
                        HttpWebResponse errres = (HttpWebResponse)ex.Response;
                        //応答したURIを表示する
                        Console.WriteLine(errres.ResponseUri);
                        //応答ステータスコードを表示する
                        status = errres.StatusCode + " " + errres.StatusDescription;

                        return false;
                    }
                    else
                    {
                        status = ex.Message;

                        return false;
                    }

                }
                finally
                {
                    //閉じる
                    if (webres != null)
                        webres.Close();
                }





                return true;
            }
            catch
            {

                return false;
            }
            finally
            {

            }
        }
        public static bool downLoadSaveFile(string url, string fileName, string proxy)
        {
            WebClient wc = new WebClient();
            try
            {
                wc.Proxy = new WebProxy(proxy, true, null);
                wc.DownloadFile(url, fileName);
                return true;
            }
            catch
            {

                return false;
            }
            finally
            {
                wc.Dispose();
            }

        }
        #endregion

        /// <summary>
        /// HTMLファイルを保存する
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        #region downLoadString
        public string downLoadString(string url)
        {
            WebClient wc = new WebClient();
            try
            {
                return  wc.DownloadString(url);;
            }
            catch
            {

                return "";
            }
            finally
            {
                wc.Dispose();
            }
        }
        public string downLoadString(string url,string proxy)
        {
            WebClient wc = new WebClient();
            try
            {
                wc.Proxy = new WebProxy(proxy, true, null);
                return wc.DownloadString(url); ;
            }
            catch
            {

                return "";
            }
            finally
            {
                wc.Dispose();
            }
        }
        #endregion
    }
}
