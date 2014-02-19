//=======================================================================
//  ClassName : RssUrlExtraction
//  概要      : WEBサイトからRSSURLを抽出する
//
//  SatelliteServer
//  Copyright(c) 2009-2014 sachin.Sachin
//=======================================================================
using System.Windows.Forms;
using Liplis.Control;
using Liplis.Xml;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Liplis.Web
{
    public static class RssUrlExtraction
    {
        /// <summary>
        /// getRssUrlを抽出する
        /// rssURLが見つかったらURLを返す。見つからなかったら空を返す。
        /// </summary>
        /// <param name="targetUrl">ターゲットサイトURL</param>
        /// <returns>RSSURL</returns>
        public static List<string> getRssUrl(string targetUrl)
        {
            List<string> res = new List<string>();

            //パーサー
            HtmlParser hp = new HtmlParser();

            //HTMLの取得
            string source = hp.getHtmlSource(targetUrl);
            string[] lst = source.Split(Environment.NewLine.ToCharArray());

            foreach (string line in lst)
            {
                if (line.IndexOf("application/rss+xml") > 0)
                {
                    int startIdx = 0;
                    int endIdx = 0;
                    string str ="";
                    try
                    {
                        do
                        {
                            startIdx = line.IndexOf("href", endIdx);
                            if (startIdx != -1)
                            {
                                startIdx += 6;
                                endIdx = line.IndexOf("\"", startIdx);
                                str = line.Substring(startIdx, endIdx - startIdx);
                                res.Add(str);
                            }

                        } while (startIdx > 0);
                    }
                    catch
                    {

                    }

                }
            }


            //仮想ブラウザ
            //using (NonDispBrowser nb = new NonDispBrowser())
            //{
            //    //まずは要約データからボディの取得を試みる
            //    nb.NavigateAndWaitFromSource(hp.getHtmlPlainTextFromSourceWB(source));

            //    HtmlDocument doc = nb.Document;
            //    HtmlElementCollection links = doc.GetElementsByTagName("link");

            //    foreach (HtmlElement ht in links)
            //    {
            //        if (ht.GetAttribute("type") == "application/rss+xml")
            //        {
            //            //string title = ht.GetAttribute("title");
            //            string href = ht.GetAttribute("href");

            //            return href;
            //        }
            //    }

            //}




            return res;
        }

        /// <summary>
        /// RSSリーダーで返す
        /// </summary>
        /// <param name="targetUrl">ターゲットサイトURL</param>
        /// <returns>結果RSS</returns>
        public static RssReader getRss(string targetUrl)
        {
            List<string> urlList = getRssUrl(targetUrl);

            foreach (string url in urlList)
            {
                RssReader rr = new RssReaderEx(url);

                if (rr.title != null)
                {
                    return rr;
                }
            }

            return null;
        }
    }
}
