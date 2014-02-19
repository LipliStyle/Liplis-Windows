//=======================================================================
//  ClassName : NonDispBrowser
//  概要      : webBrowzerコントロールのカスタマイズ
//
//  Liplisちゃんシステム      
//  Copyright(c) 2010-2010 sachin.Sachin
//=======================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Liplis.Web;
using System.Runtime.InteropServices;

namespace Liplis.Control
{
    public class NonDispBrowser : WebBrowser
    {

        protected bool done;

        // タイムアウト時間（10秒）
        TimeSpan timeout = new TimeSpan(0, 0, 10);
        [STAThread]
        protected override void OnDocumentCompleted(
                      WebBrowserDocumentCompletedEventArgs e)
        {
            //エラートラップ
            this.Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);

            // ページにフレームが含まれる場合にはフレームごとに
            // このメソッドが実行されるため実際のURLを確認する
            if (e.Url == this.Url)
            {
                done = true;
            }
        }
        [STAThread]
        protected override void OnNewWindow(CancelEventArgs e)
        {
            // ポップアップ・ウィンドウをキャンセル
            e.Cancel = true;
        }

        public NonDispBrowser()
        {
            // スクリプト・エラーを表示しない
            this.ScriptErrorsSuppressed = true;
            this.NewWindow += new CancelEventHandler(NonDispBrowser_NewWindow);
        }


        /// <summary>
        /// ナビゲイトして完了まで待つ
        /// </summary>
        /// <param name="url"></param>
        [STAThread]
        public bool NavigateAndWait(string url)
        {
            try
            {
                base.Navigate(url); // ページの移動

                done = false;
                DateTime start = DateTime.Now;

                while (done == false)
                {
                    if (DateTime.Now - start > timeout)
                    {
                        // タイムアウト
                        return false;
                    }
                    Application.DoEvents();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ナビゲイトして完了まで待つ
        /// </summary>
        /// <param name="url"></param>
        [STAThread]
        public bool NavigateAndWaitFromSource(string source)
        {
            try
            {
                base.DocumentText = source; // ページの移動

                done = false;
                DateTime start = DateTime.Now;


                Application.DoEvents();
                while (done == false)
                {
                    if (DateTime.Now - start > timeout)
                    {
                        // タイムアウト
                        return false;
                    }
                    Application.DoEvents();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Window_Error(object sender,
            HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = false;
        }

        /// <summary>
        /// Webブラウザ：新しいウィンドウが開かれたとき
        /// </summary>
        private void NonDispBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            // キャンセル
            e.Cancel = true;
        }
    }
}
