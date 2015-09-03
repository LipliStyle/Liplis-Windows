//=======================================================================
//  ClassName : WebSummary
//  概要      : ウェブサマリー
//　　　　　　　ウェブサイトの要約を返す
//
//  SatelliteServer
//  Copyright(c) 2009-2012 sachin.Sachin
//　2012/08/17 シングルスレッド実行メソッドを追加
//             それに伴い、インスタンス化用コンストラクター、ワークプロパティを追加
//=======================================================================
using System;
using Liplis.Control;
using NReadability;
using System.Threading;

namespace Liplis.Web
{
    public class WebSummary
    {
        ///=============================
        ///URL,結果
        #region 
        public  string url{get;set;}
        public string result {get;set;}
        #endregion
        

        /// <summary>
        /// デフォルトコンストラクター
        /// </summary>
        #region WebSummary
        public WebSummary()
        {

        }
        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WebSummary
        public WebSummary(string url)
        {
            this.url = url;
        }
        #endregion


        /// <summary>
        /// URLトランスコーダー
        /// スタティック(静的にURLを指定して使用)
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>日本語抽出</returns>
        #region transeForJapa
        [STAThread]
        public static string transeForJapa(string url)
        {
            bool mainContentExtracted;

            //結果
            string result = "";
            string source = "";
            string title = "";

            //トランスコーダー
            NReadabilityTranscoder nReadabilityTranscoder = new NReadabilityTranscoder();
            //パーサー
            HtmlParser hp = new HtmlParser();
            
            //仮想ブラウザ
            using (NonDispBrowser nb = new NonDispBrowser())
            {
                //HTMLの取得
                source = hp.getHtmlSource(url);

                try
                {
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
                              //まずは要約データからボディの取得を試みる
                    nb.NavigateAndWaitFromSource(hp.getHtmlPlainTextFromSourceWB(nReadabilityTranscoder.Transcode(source, out mainContentExtracted)));
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
                    title = nb.Document.Title;
                    result = nb.Document.Body.InnerText.Replace(title, "");

                    if (result != "") { return result; }

                    result = HtmlParser.htmlGomiRegularRemove(HtmlParser.htmlTagRegularRemove(source));
                }
                catch
                {

                }
            }


            //結果を返す
            return result;

            //return hp.getHtmlPlainTextFromSource(nReadabilityTranscoder.Transcode(getHtmlSource(url), out mainContentExtracted));
        }
        [STAThread]
        public void transeForJapaSta()
        {
            result = transeForJapa(this.url);
        }
        #endregion


        /// <summary>
        /// doThread
        /// スレッドを実行する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region transeForJapaStaDo
        public void transeForJapaStaDo()
        {
            //画像作成するスレッドを生成
            Thread imgThread = new Thread(new ThreadStart(transeForJapaSta));

            //WebBrowserはシングルスレッドアパートメントモードでのみ実行可能なのでスレッドのモードを設定して実行する
            imgThread.SetApartmentState(ApartmentState.STA);

            //スレッドスタート
            imgThread.Start();

            //スレッド終了を待つ
            imgThread.Join();

            //スレッドを終了
            imgThread.Abort();

            return;
        }
        #endregion


        /// <summary>
        /// getTitle
        /// タイトルを取得する
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>タイトルを取得する</returns>
        #region getTitle
        [STAThread]
        public static string getTitle(string url)
        {
            bool mainContentExtracted;

            //結果
            string source = "";
            string title = "";

            //トランスコーダー
            NReadabilityTranscoder nReadabilityTranscoder = new NReadabilityTranscoder();
            //パーサー
            HtmlParser hp = new HtmlParser();

            //仮想ブラウザ
            NonDispBrowser nb = new NonDispBrowser();
            //HTMLの取得
            source = hp.getHtmlSource(url);

            try
            {
#pragma warning disable CS0618 // 型またはメンバーが古い形式です
                nb.NavigateAndWaitFromSource(hp.getHtmlPlainTextFromSourceWB(nReadabilityTranscoder.Transcode(source, out mainContentExtracted)));
#pragma warning restore CS0618 // 型またはメンバーが古い形式です
                title = nb.Document.Title;
            }
            catch
            {

            }
            finally
            {
                //確実に破棄
                nb.Dispose();
            }

            //結果を返す
            return title;

            //return hp.getHtmlPlainTextFromSource(nReadabilityTranscoder.Transcode(getHtmlSource(url), out mainContentExtracted));
        }
        #endregion
    }
}
