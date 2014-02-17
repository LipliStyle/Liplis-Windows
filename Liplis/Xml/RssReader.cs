//=======================================================================
//  ClassName : RssReader
//  概要      : アールエスエスを読み取るインターフェースクラス
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Liplis.Xml;
using System.Reflection;
using Liplis.Common;

namespace Liplis.Xml
{
    public class RssReader : XmlLinq, IDisposable
    {
        ///============================
        /// RSS基本情報
        #region RssBasicData
        public string title { get; set; }
        public string rssUri { get; set; }
        public string rssUpdate { get; set; }
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
        protected const int def     = 0;
        protected const int rdf     = 1;
        protected const int atm     = 2;
        protected const int vr2     = 3;
        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="rssUri">RSS URL</param>
        #region コンストラクター
        public RssReader(string rssUri)
        {
            init(rssUri);
        }
        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="rssUri"></param>
        /// <param name="fileName"></param>
        #region コンストラクター
        public RssReader(string rssUri, string fileName)
        {
            init(rssUri, fileName);
        }

        #endregion

        /// <summary>
        /// 初期化メソッド
        /// </summary>
        /// <param name="rssUri">rssUri</param>
        #region 初期化
        private void init(string rssUri)
        {
            try
            {
                urlList         = new List<string>();
                urlTitleList    = new List<string>();
                discriptionList = new List<string>();
                dateList        = new List<string>();
                this.rssUri     = rssUri;
                xmlFilePath     = rssUri;
                createList();
            }
            catch (System.Exception err)
            {
                //ダウンロード失敗時、スルーするかメッセージを表示するようにする
                //LiplisLog.writingLog("RssReader : init\n" + err);
                throw err;
            }
        }
        private void init(string rssUri, string fileName)
        {
            rssBufPath = fileName;
            init(rssUri);
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
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// Rssタイプを判定する
        /// </summary>
        /// <returns></returns>
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
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
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
        /// <param name="itemTag">itemTag</param> 
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
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// タイトルリストを生成する(RSS2.0版)
        /// </summary>
        /// <param name="itemTag">itemTag</param> 
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
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
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
        /// <param name="itemTag">itemTag</param> 
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
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// URLリストを生成する(Atom版)
        /// </summary>
        /// <param name="itemTag">itemTag</param>
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
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// URLリストを生成する(Ver2.0版)
        /// </summary>
        /// <param name="itemTag">itemTag</param>  
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
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
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
        /// <param name="itemTag">mainRoot</param> 
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
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// タイトルを生成する(Ver2.0版)
        /// </summary>
        /// <param name="itemTag">mainRoot</param> 
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
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
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
        /// <param name="itemTag">itemTag</param> 
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
                    LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString() + Environment.NewLine + url);
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// ディスクリプションリストを生成する(RSS2.0版)
        /// </summary>
        /// <param name="itemTag">itemTag</param>  
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
                    //LiplisLog.writingLog("V2ディスクリプション作成失敗①\n" + rssUri + "\n");
                    createDiscriptionListV2_content(itemTag);
                    //LiplisLog.writingLog("RssReader : createDiscriptionListV2\n" + err + "\n" + url + "\n");
                }

            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// ディスクリプションリストを生成する(RSS2.0 Content版)
        /// </summary>
        /// <param name="itemTag">itemTag</param>  
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
                    //LiplisLog.writingLog("V2ディスクリプション作成失敗②\n" + rssUri + "\n");
                    //LiplisLog.writingLog("RssReader : createDiscriptionListV2_content\n" + err + "\n" + url + "\n");
                    createDisctiptionListEmpty();
                }

            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// ディスクリプションリストを生成する(Atom版)
        /// </summary>
        /// <param name="itemTag">itemTag</param> 
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
                    //LiplisLog.writingLog("RssReader : createDiscriptionListAtom\n" + err + "\n" + url + "\n");
                    //LiplisLog.writingLog("V2ディスクリプション作成失敗③\n" + rssUri + "\n");
                    //LiplisLog.writingLog("V2 結局空を作成\n" + rssUri + "\n");
                    //空のディスクリプションリストを作成
                    createDisctiptionListEmpty();
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
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
        /// <param name="itemTag">itemTag</param> 
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
                    dateList = new List<string>();
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 更新日リストを生成する(RSS2.0版)
        /// </summary>
        /// <param name="itemTag">itemTag</param> 
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
                    dateList = new List<string>();
                }

            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 更新日リストを生成する(Atom版)
        /// </summary>
        /// <param name="itemTag">itemTag</param> 
        #region createUpdateListAtom
        private void createUpdateListAtom(string itemTag)
        {
            try
            {
                XNamespace xns = getXmlns();
                var query = from xperson in xmlDoc.Descendants(xns + itemTag)
                            select xperson.Element(xns + "published").Value;

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
                    dateList = new List<string>();
                }
            }
            catch (System.Exception err)
            {
                LiplisLog.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
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
    }
}
