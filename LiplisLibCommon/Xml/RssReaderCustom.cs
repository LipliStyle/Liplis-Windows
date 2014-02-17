//=======================================================================
//  ClassName : RssReader
//  概要      : RSSを読み込むクラス
//              RSS1.0系、RSS ATOM、RSS2.0系に対応
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Liplis.Web;
using System.Net;

namespace Liplis.Xml
{
    public class RssReaderCustom : XmlLinq, IDisposable
    {
        ///============================
        /// RSS基本情報
        #region RssBasicData
        public string title { get; set; }
        public string rssUri { get; set; }
        public string rssUpdate { get; set; }
        public string userAgent { get; set; }
        #endregion

        ///============================
        ///リンクラベルコレクション
        #region dataList
        public List<string> urlList { get; set; }
        public List<string> urlTitleList { get; set; }
        public List<string> discriptionList { get; set; }
        public List<string> dateList { get; set; }
        #endregion

        ///============================
        ///定数
        #region property
        protected string rssBufPath = "rssBuf.xml";
        protected const int def = 0;
        protected const int rdf = 1;
        protected const int atm = 2;
        protected const int vr2 = 3;
        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public RssReaderCustom(string rssUri)
        {
            init(rssUri);
        }
        public RssReaderCustom(string rssUri, string userAgent)
        {
            initUA(rssUri, userAgent);
        }
        public RssReaderCustom(string rssUri, string userAgent, string fileName)
        {
            init(rssUri, fileName);
        }

        #endregion

        /// <summary>
        /// 初期化メソッド
        /// </summary>
        #region 初期化
        private void init(string rssUri)
        {
            try
            {
                urlList = new List<string>();
                urlTitleList = new List<string>();
                discriptionList = new List<string>();
                dateList = new List<string>();
                this.rssUri = rssUri;
                xmlFilePath = rssUri;
                createList();
            }
            catch (System.Exception err)
            {
                //ダウンロード失敗時、スルーするかメッセージを表示するようにする
                //lc.writingLog("RssReader : init\n" + err);
                throw err;
            }
        }
        private void init(string rssUri, string fileName)
        {
            rssBufPath = fileName;
            init(rssUri);
        }
        private void initUA(string rssUri, string userAgent)
        {
            try
            {
                urlList = new List<string>();
                urlTitleList = new List<string>();
                discriptionList = new List<string>();
                dateList = new List<string>();
                this.rssUri = rssUri;
                xmlFilePath = rssUri;
                this.userAgent = userAgent;
                createListUA();
            }
            catch (System.Exception err)
            {
                //ダウンロード失敗時、スルーするかメッセージを表示するようにする
                //lc.writingLog("RssReader : init\n" + err);
                throw err;
            }
        }
        #endregion



        /// <summary>
        /// UrlとTitleをセットする
        /// 取得に失敗した場合、成功フラグをオフにする
        /// </summary>
        #region createList
        public void createList()
        {
            try
            {
                switch (interpretationRssType())
                {
                    case rdf:
                        createUrlListRdf();
                        break;
                    case atm:
                        createUrlListAtom();
                        break;
                    case vr2:
                        createUrlListV2();
                        break;
                    default:
                        break;
                }

                //空のリストが合った場合は補完する
                checkList();
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createList\n" + err);
            }
        }
        public void createListUA()
        {
            try
            {
                switch (interpretationRssTypeUA())
                {
                    case rdf:
                        createUrlListRdf();
                        break;
                    case atm:
                        createUrlListAtom();
                        break;
                    case vr2:
                        createUrlListV2();
                        break;
                    default:
                        break;
                }

                //空のリストが合った場合は補完する
                checkList();
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createList\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// Rssタイプを判定する
        /// </summary>
        #region rssタイプの判別
        public int interpretationRssType()
        {
            try
            {
                try { readXml(); }
                catch { return def; }
                if (xmlDoc == null) { return def; }
                if (getRootElements().IndexOf("RDF") > 0) { return rdf; }
                if (getRootElements().IndexOf("feed") > 0) { return atm; }
                if (getRootElements().Equals("rss")) { return vr2; }
                return def;
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : interpretationRssType\n" + err);
                return def;
            }
        }
        public int interpretationRssTypeUA()
        {
            try
            {
                try { readXmlUA(); }
                catch { return def; }
                if (xmlDoc == null) { return def; }
                if (getRootElements().IndexOf("RDF") > 0) { return rdf; }
                if (getRootElements().IndexOf("feed") > 0) { return atm; }
                if (getRootElements().Equals("rss")) { return vr2; }
                return def;
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : interpretationRssType\n" + err);
                return def;
            }
        }
        #endregion

        /// <summary>
        /// Rss1.0のRssからURLリストを生成する
        /// </summary>
        #region createUrlListRdf
        public void createUrlListRdf()
        {
            createTitleList("item");
            createUrlListRdf("item");
            createTitle("channel");
            createDiscriptionList("item");
            createUpdateList("item");
        }
        #endregion

        /// <summary>
        /// RssAtomのRssからURLリストを生成する
        /// </summary>
        #region createUrlListAtom
        public void createUrlListAtom()
        {
            createTitleList("entry");
            createUrlListAtom("entry");
            createTitle("feed");
            createDiscriptionListAtom("entry");
            createUpdateListAtom("entry");
        }
        #endregion

        /// <summary>
        /// Rss2.0のRssからURLリストを生成する
        /// </summary>
        #region createUrlListV2
        public void createUrlListV2()
        {
            createTitleListV2("item");
            createUrlListV2("item");
            createTitleV2("channel");
            createDiscriptionListV2("item");
            createUpdateListV2("item");
        }
        #endregion

        /// <summary>
        /// ディスポーズ
        /// </summary>
        #region Dispose
        public void Dispose()
        {
            urlList.Clear();
            urlTitleList.Clear();
            discriptionList.Clear();
            dateList.Clear();
        }
        #endregion

        ///====================================================================
        ///
        ///                        タイトルリスト
        ///                         
        ///====================================================================

        /// <summary>
        /// タイトルリストを生成する
        /// RSSの形式により、アイテムタグが異なるので、合ったものを指定
        /// </summary>
        #region createTitleList
        private void createTitleList(string itemTag)
        {
            try
            {
                int byt;
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                XNamespace xns = getXmlns();

                //クエリーを投げる
                var query = from xperson in xmlDoc.Descendants(xns + itemTag)
                            select xperson.Element(xns + "title").Value;

                //結果リストに格納
                foreach (string title in query)
                {

                    System.Windows.Forms.Application.DoEvents();
                    string bufStr = title;
                    //バイト数を取得する
                    byt = sjisEnc.GetByteCount(title);
                    urlTitleList.Add(bufStr);
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createTitleList\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// タイトルリストを生成する(RSS2.0版)
        /// </summary>
        #region createTitleListV2
        private void createTitleListV2(string itemTag)
        {
            try
            {
                int byt;
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");

                //クエリーを投げる
                var query = from xperson in xmlDoc.Descendants(itemTag)
                            select xperson.Element("title").Value;

                //結果リストに格納
                foreach (string title in query)
                {

                    System.Windows.Forms.Application.DoEvents();
                    string bufStr = title;
                    //バイト数を取得する
                    byt = sjisEnc.GetByteCount(title);
                    urlTitleList.Add(bufStr);
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createTitleListV2\n" + err);
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                          URLリスト
        ///                         
        ///====================================================================

        /// <summary>
        /// URLリストを生成する(RDF版)
        /// </summary>
        #region createUrlListRdf
        private void createUrlListRdf(string itemTag)
        {
            try
            {
                XNamespace xns = getXmlns();
                var query = from xperson in xmlDoc.Descendants(xns + itemTag)
                            select xperson.Element(xns + "link").Value;

                //結果リストに格納
                foreach (string link in query)
                {

                    System.Windows.Forms.Application.DoEvents();
                    urlList.Add(link);
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createUrlListRdf\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// URLリストを生成する(Atom版)
        /// </summary>
        #region createUrlListAtom
        private void createUrlListAtom(string itemTag)
        {
            try
            {
                XNamespace xns = getXmlns();
                var query = from xperson in xmlDoc.Descendants(xns + itemTag)
                            select xperson.Element(xns + "link").Attribute("href").Value;

                //結果リストに格納
                foreach (string link in query)
                {

                    System.Windows.Forms.Application.DoEvents();
                    urlList.Add(link);
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createUrlListAtom\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// URLリストを生成する(Ver2.0版)
        /// </summary>
        #region createUrlListV2
        private void createUrlListV2(string itemTag)
        {
            try
            {
                var query = from xperson in xmlDoc.Descendants(itemTag)
                            select xperson.Element("link").Value;

                //結果リストに格納
                foreach (string link in query)
                {

                    System.Windows.Forms.Application.DoEvents();
                    urlList.Add(link);
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createUrlListV2\n" + err);
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                            title
        ///                         
        ///====================================================================

        /// <summary>
        /// タイトルを生成する
        /// RSSの形式により、アイテムタグが異なるので、合ったものを指定
        /// </summary>
        #region createTitle
        private void createTitle(string mainRoot)
        {
            try
            {
                XNamespace xns = getXmlns();
                var query = from xperson in xmlDoc.Descendants(xns + mainRoot)
                            select xperson.Element(xns + "title").Value;
                foreach (var title in query)
                {

                    System.Windows.Forms.Application.DoEvents();
                    this.title = title;
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createTitle\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// タイトルを生成する(Ver2.0版)
        /// </summary>
        #region createTitleV2
        private void createTitleV2(string mainRoot)
        {
            try
            {
                var query = from xperson in xmlDoc.Descendants(mainRoot)
                            select xperson.Element("title").Value;
                foreach (var title in query)
                {

                    System.Windows.Forms.Application.DoEvents();
                    this.title = title;
                }

            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createTitleV2\n" + err);
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                          discripthion
        ///                         
        ///====================================================================

        /// <summary>
        /// ディくりぷしょんリストを生成する
        /// RSSの形式により、アイテムタグが異なるので、合ったものを指定
        /// </summary>
        #region createDiscriptionList
        private void createDiscriptionList(string itemTag)
        {
            try
            {
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                XNamespace xns = getXmlns();

                //クエリーを投げる
                var query = from xperson in xmlDoc.Descendants(xns + itemTag)
                            select xperson.Element(xns + "description").Value;

                try
                {
                    //結果リストに格納
                    foreach (string description in query)
                    {

                        System.Windows.Forms.Application.DoEvents();
                        discriptionList.Add(description);
                    }
                }
                catch (Exception err)
                {
                    discriptionList = new List<string>();
                    lc.writingLog("RssReader : createDiscriptionList\n" + err + "\n" + url + "\n");
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createDiscriptionList\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// ディスクリプションリストを生成する(RSS2.0版)
        /// </summary>
        #region createDiscriptionListV2
        private void createDiscriptionListV2(string itemTag)
        {
            try
            {
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");

                //クエリーを投げる
                var query = from xperson in xmlDoc.Descendants(itemTag)
                            select xperson.Element("description").Value;

                try
                {
                    //結果リストに格納
                    foreach (string description in query)
                    {

                        System.Windows.Forms.Application.DoEvents();
                        discriptionList.Add(description);
                    }
                }
                catch (Exception)
                {
                    //lc.writingLog("V2ディスクリプション作成失敗①\n" + rssUri + "\n");
                    createDiscriptionListV2_content(itemTag);
                    //lc.writingLog("RssReader : createDiscriptionListV2\n" + err + "\n" + url + "\n");
                }

            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createDiscriptionListV2\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// ディスクリプションリストを生成する(RSS2.0 Content版)
        /// </summary>
        #region createDiscriptionListV2_content
        private void createDiscriptionListV2_content(string itemTag)
        {
            try
            {
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");

                //名前空間の取得
                XNamespace xns = getXmlns();

                //クエリーを投げる
                var query = from xperson in xmlDoc.Descendants(itemTag)
                            select xperson.Element(xns + "encoded").Value;

                try
                {
                    //結果リストに格納
                    foreach (string description in query)
                    {

                        System.Windows.Forms.Application.DoEvents();
                        discriptionList.Add(description);
                    }
                }
                catch (Exception)
                {
                    //lc.writingLog("V2ディスクリプション作成失敗②\n" + rssUri + "\n");
                    //lc.writingLog("RssReader : createDiscriptionListV2_content\n" + err + "\n" + url + "\n");
                    createDisctiptionListEmpty();
                }

            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createDiscriptionListV2\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// ディスクリプションリストを生成する(Atom版)
        /// </summary>
        #region createDiscriptionListAtom
        private void createDiscriptionListAtom(string itemTag)
        {
            try
            {
                XNamespace xns = getXmlns();
                var query = from xperson in xmlDoc.Descendants(xns + itemTag)
                            select xperson.Element(xns + "content").Value;

                try
                {
                    //結果リストに格納
                    foreach (string link in query)
                    {

                        System.Windows.Forms.Application.DoEvents();
                        discriptionList.Add(link);
                    }
                }
                catch (Exception)
                {
                    //lc.writingLog("RssReader : createDiscriptionListAtom\n" + err + "\n" + url + "\n");
                    //lc.writingLog("V2ディスクリプション作成失敗③\n" + rssUri + "\n");
                    //lc.writingLog("V2 結局空を作成\n" + rssUri + "\n");
                    //空のディスクリプションリストを作成
                    createDisctiptionListEmpty();
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createDiscriptionListAtom\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// 空のディスクリプションリストを作成する
        /// </summary>
        #region createDisctiptionListEmpty
        private void createDisctiptionListEmpty()
        {
            try
            {
                discriptionList = new List<string>();
                foreach (string title in urlTitleList)
                {

                    System.Windows.Forms.Application.DoEvents();
                    discriptionList.Add("");
                }
            }
            catch
            {
                discriptionList = new List<string>();
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                        更新日リスト
        ///                         
        ///====================================================================

        /// <summary>
        /// 更新日リストを生成する
        /// RSSの形式により、アイテムタグが異なるので、合ったものを指定
        /// </summary>
        #region createUpdateList
        private void createUpdateList(string itemTag)
        {
            try
            {
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                XNamespace xns = getXmlns();
                XNamespace xnsDc = getNameSpase("dc");

                //クエリーを投げる
                var query = from xperson in xmlDoc.Descendants(xns + itemTag)
                            select xperson.Element(xnsDc + "date").Value;

                try
                {
                    //結果リストに格納
                    foreach (string description in query)
                    {

                        System.Windows.Forms.Application.DoEvents();
                        dateList.Add(description);
                    }
                }
                catch
                {
                    dateList = new List<string>(urlList.Count);
                    foreach (string str in urlList)
                    {
                        dateList.Add("");
                    }
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createDiscriptionList\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// 更新日リストを生成する(RSS2.0版)
        /// </summary>
        #region createUpdateListV2
        private void createUpdateListV2(string itemTag)
        {
            try
            {
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");

                //クエリーを投げる
                var query = from xperson in xmlDoc.Descendants(itemTag)
                            select xperson.Element("pubDate").Value;

                try
                {
                    //結果リストに格納
                    foreach (string description in query)
                    {

                        System.Windows.Forms.Application.DoEvents();
                        dateList.Add(description);
                    }
                }
                catch
                {
                    dateList = new List<string>(urlList.Count);
                    foreach (string str in urlList)
                    {
                        dateList.Add("");
                    }
                }

            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createDiscriptionListV2\n" + err);
            }
        }
        #endregion

        /// <summary>
        /// 更新日リストを生成する(Atom版)
        /// </summary>
        #region createUpdateListAtom
        private void createUpdateListAtom(string itemTag)
        {
            try
            {
                XNamespace xns = getXmlns();
                var query = from xperson in xmlDoc.Descendants(xns + itemTag)
                            select xperson.Element(xns + "issued").Value;

                try
                {
                    //結果リストに格納
                    foreach (string link in query)
                    {

                        System.Windows.Forms.Application.DoEvents();
                        dateList.Add(link);
                    }
                }
                catch
                {
                    createUpdateListAtom2(itemTag);
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createUpdateListAtom\n" + err);
            }
        }
        private void createUpdateListAtom2(string itemTag)
        {
            try
            {
                XNamespace xns = getXmlns();
                var query = from xperson in xmlDoc.Descendants(xns + itemTag)
                            select xperson.Element(xns + "updated").Value;

                try
                {
                    //結果リストに格納
                    foreach (string link in query)
                    {

                        System.Windows.Forms.Application.DoEvents();
                        dateList.Add(link);
                    }
                }
                catch
                {
                    dateList = new List<string>(urlList.Count);
                    foreach (string str in urlList)
                    {
                        dateList.Add("");
                    }
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("RssReader : createUpdateListAtom2\n" + err);
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                        リストを補完する
        ///                         
        ///====================================================================
        /// <summary>
        /// ディスクリプションを補完する
        /// </summary>
        #region checkList
        private void checkList()
        {
            if (discriptionList.Count <= 0)
            {
                discriptionList.Clear();
                foreach (string title in urlTitleList)
                {

                    System.Windows.Forms.Application.DoEvents();
                    discriptionList.Add(title);
                }
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                        プロパティ
        ///                         
        ///====================================================================
        /// <summary>
        /// レスポンスこーど
        /// </summary>
        public int response
        {
            set{this.responseCode = value;}
            get{return this.responseCode; }
        }

        ///====================================================================
        ///
        ///                        汎用メソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// URLからタイトルを引く
        /// </summary>
        #region checkList
        public string getTitleFromUrl(string url)
        {
            try
            {
                int idx = urlList.IndexOf(url);
                if(idx >= 0)
                {
                    return urlTitleList[idx];
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        #endregion

        /// <summary>
        /// タイトルからURLを引く
        /// </summary>
        #region checkList
        public string getUrlFromTitle(string title)
        {
            try
            {
                int idx = urlTitleList.IndexOf(title);
                if(idx >= 0)
                {
                    return urlList[idx];
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                        ろーどXML
        ///                         
        ///====================================================================

        /// <summary>
        /// readXml
        /// XMLを読み込む
        /// </summary>
        protected void readXmlUA()
        {
            xmlDoc = new XDocument();

            try
            {
                //指定したXMLファイルの読み込み
                xmlDoc = XDocument.Load(convertStream(HttpGet.getHtmlGetUA(xmlFilePath,this.userAgent)));

                responseCode = 0;
            }
            catch (System.Xml.XmlException)
            {

                //XML例外 XMLが読み込めませんでした。
                lc.writingLog("XML例外 XMLが読み込めませんでした。書式に問題がある可能性があります。\n");
                responseCode = 1;
            }
            catch (WebException ex)
            {
                //HTTPプロトコルエラーかどうか調べる
                if (ex.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    //HttpWebResponseを取得
                    HttpWebResponse errres = (HttpWebResponse)ex.Response;
                    //応答ステータスコードを表示する
                    lc.writingLog("Web例外 " + errres.StatusCode + ":" + errres.StatusDescription + Environment.NewLine + "接続先はココ→" + xmlFilePath + "\n");
                    Console.WriteLine("{0}:{1}", errres.StatusCode, errres.StatusDescription);
                    responseCode = 10;
                }
                else
                {
                    //XML例外 XMLが読み込めませんでした。
                    lc.writingLog("Web例外 XMLが読み込めませんでした。サーバーが存在しない可能性があります。\n接続先はココ→" + xmlFilePath + "\n");
                    responseCode = 11;
                }
            }
            catch (System.Exception)
            {
                //XMLその他例外 XMLが読み込めませんでした。
                lc.writingLog("XMLその他例外ファイルにアクセスできません。ファイルが開かれている可能性があります。\n" + xmlFilePath + "\n");
                responseCode = 12;
            }
        }


    }
}
