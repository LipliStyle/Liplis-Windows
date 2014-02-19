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
    public class RssReaderEx : RssReader
    {
        #region コンストラクター
        public RssReaderEx():base()
        {
           
        }
        public RssReaderEx(string rssUri)
            : base(rssUri)
        {

        }
        public RssReaderEx(string rssUri, string fileName)
            : base(rssUri, fileName)
        {

        }
        #endregion

        /// <summary>
        /// RssEXの処理を使う
        /// </summary>
        #region createUrlListEx
        protected override void createUrlListEx()
        {
            try
            {
                RssReaderNative rr = new RssReaderNative(this.url);
                this.title = rr.title;
                this.urlList = rr.urlList;
                urlTitleList = rr.urlTitleList;
                discriptionList = rr.discriptionList;
                dateList = rr.dateList;
                response = 0;
            }
            catch 
            {
                response = 12;
            }
        }
        #endregion

        /// <summary>
        /// ディスクリプションを補完する
        /// </summary>
        #region checkList
        protected override void recoveryTryFromEX2()
        {
            if (discriptionList.Count > 0)
            {
                if (discriptionList[0] == "")
                {
                    RssReaderNative rr = new RssReaderNative(this.url);

                    if (rr.discriptionList.Count > 0)
                    {
                        //空でなければEXを採用する
                        if (rr.discriptionList[0] != "")
                        {
                            this.title = rr.title;
                            this.urlList = rr.urlList;
                            this.urlTitleList = rr.urlTitleList;
                            this.discriptionList = rr.discriptionList;
                            this.dateList = rr.dateList;
                            response = 0;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
