//=======================================================================
//  ClassName : RssReader
//  概要      : RSSを読み込むクラス
//              RSS1.0系、RSS ATOM、RSS2.0系に対応
//
//  Liplisシステム      
//  Copyright(c) 2010-2013 sachin.Sachin
//=======================================================================
using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Liplis.Xml
{
    public class RssReaderNative : RssReader
    {
        public RssReaderNative(string url)
        {
            try
            {
                this.url = url;
                using (XmlReader rdr = XmlReader.Create(url))
                {
                    SyndicationFeed feed = SyndicationFeed.Load(rdr);
                    this.title = feed.Title.Text;
                    Console.WriteLine("description:" + feed.Description.Text);


                    foreach (SyndicationItem item in feed.Items)
                    {
                        if (item.Links != null) { urlList.Add((item.Links.Count > 0 ? item.Links[0].Uri.AbsoluteUri : "")); } else { urlList.Add(""); }
                        if (item.Title != null) { urlTitleList.Add(item.Title.Text); } else { urlTitleList.Add(""); }
                        if (item.Summary != null) { discriptionList.Add(item.Summary.Text); } else { discriptionList.Add(""); }
                        if (item.PublishDate != null) { dateList.Add(item.PublishDate.ToString("yyyy/MM/dd HH:mm:ss")); } else { dateList.Add(""); }
                    }
                }
            }
            catch
            {
                
            }
            
        }
    }
}
