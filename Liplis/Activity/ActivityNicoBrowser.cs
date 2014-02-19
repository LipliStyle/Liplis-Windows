//=======================================================================
//  ClassName : NicoBrowserWindow
//  概要      : ニコブラウザウインドウ
//
//  Liplis3.0
//  2013/07/06 Liplis3.0.4 ニコニコサムネイル最新API対応
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.MainSystem;

namespace Liplis.Activity
{
    /// <summary>
    /// NicoBrowserWindow
    /// ローカルにhtmlファイルを生成し、ニコニコ動画の情報を表示する
    /// </summary>
    public partial class ActivityNicoBrowser : BaseSystem
    {
        ///=====================================
        ///URL
        string url;

        ///=====================================
        /// フラグ
        private bool flgEnd = false;

        ///=====================================
        /// デリゲート
        private static LpsDelegate.dlgI7ToVoid reqSetLocation;

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public ActivityNicoBrowser()
        {
            InitializeComponent();
            initSettingWindow();
        }
        #endregion

        /// <summary>
        /// 画面の初期化
        /// </summary>
        #region initSettingWindow
        private void initSettingWindow()
        {
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.wbNico.ScrollBarsEnabled = false;
            this.Opacity = 1;
            this.initDelegate();
            this.wbNico.ScriptErrorsSuppressed = true;
        }
        #endregion

        /// <summary>
        /// initDelegate
        /// delegateの初期化
        /// </summary>
        #region initDelegate
        private void initDelegate()
        {
            //セットロケーションデリゲート
            reqSetLocation = new LpsDelegate.dlgI7ToVoid(dlgSetLocation);
        }
        #endregion

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region NicoBrowserWindow_Load
        private void NicoBrowserWindow_Load(object sender, EventArgs e)
        {
            timEnd.Interval = 30000;
        }
        #endregion

        /// <summary>
        /// クリエイトニコインフォ
        /// </summary>
        #region createNicoInfo
        public void createNicoInfo(string url)
        {
            this.url = url;
            string htmlStr = "";
            string fixFileName;
            string[] splitStr;

            splitStr = url.Split('/');
            url = splitStr[splitStr.Length - 1];

            fixFileName = LpsPathController.getTempPath() + "nico.html";

            //ファイル読込
            using (StreamWriter fixFile = new StreamWriter(@fixFileName, false, System.Text.Encoding.GetEncoding(932)))
            {
                //  2013/07/06 Liplis3.0.4 ニコニコサムネイル最新API対応(旧ソース削除)
                //HTML作成
                htmlStr = htmlStr + "<HTML>\n";
                htmlStr = htmlStr + "<HEAD>\n";
                htmlStr = htmlStr + "</HEAD>\n";
                htmlStr = htmlStr + "	<BODY>\n";
                htmlStr = htmlStr + "   <iframe src=http://www.nicovideo.jp/thumb/" + url + " width=300 height=180  scrolling=no border=0 frameborder=0></iframe>\n";
                htmlStr = htmlStr + "   </BODY>\n";
                htmlStr = htmlStr + "</HTML>\n";

                fixFile.Write(htmlStr);
            }

            try
            {
                wbNico.Navigate(fixFileName);
                wbNico.Refresh();
            }
            catch
            {

            }
        }
        #endregion

        /// <summary>
        /// ショウする
        /// </summary>
        #region doShow
        public void doShow()
        {
            this.createNicoInfo(this.url);
        }
        #endregion

        /// <summary>
        /// イメージをセットする
        /// </summary>
        /// <param name="path"></param>
        #region setImage
        public void setImage(string url)
        {
            if (LpsLiplisUtil.domainCheck(url, LpsDefineMost.URL_NICO_DOMAIN))
            {
                this.url = url;
                Invoke(new LpsDelegate.dlgVoidToVoid(doThread));
                this.Show();
                this.Refresh();
            }
            else
            {
                this.Hide();
            }
        }
        #endregion

        /// <summary>
        /// エンドタイマー
        /// </summary>
        #region timEnd_Tick
        private void timEnd_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// btnClose_Click
        /// 閉じる
        /// </summary>
        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion
        

        ///====================================================================
        ///
        ///                          onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// onDelete
        /// </summary>
        #region Dispose
        public void dispose()
        {
            flgEnd = true;
            this.Close();
        }
        #endregion

        /// <summary>
        /// ActivityNicoBrowser_FormClosing
        /// フォームクロージングキャンセラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityNicoBrowser_FormClosing
        private void ActivityNicoBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            //エンドフラグが有効でなければ、ハイドさせる
            if (!flgEnd)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                       ウインドウ制御
        ///                         
        ///====================================================================

        /// <summary>
        /// setLocation
        /// 座標のセット
        /// </summary>
        #region setLocation
        public void setLocation(int liplisX, int liplisY, int liplisWidth, int liplisHieght, int talkWidth, int talkHeight, int direction)
        {
            reqSetLocation(liplisX, liplisY, liplisWidth, liplisHieght, talkWidth, talkHeight, direction);
        }
        #endregion


        /// <summary>
        /// dlgSetLocation
        /// </summary>
        #region dlgSetLocation
        private void dlgSetLocation(int liplisX, int liplisY, int liplisWidth, int liplisHieght, int talkWidth, int talkHeigh, int direction)
        {
            int ftLocX = this.Left;
            int ftLocY = this.Top;
            int targetX = liplisX - this.Width;
            int targetY = liplisY;
            int moveValX = 0;
            int moveValY = 0;
            int cnt = 1;

            //シフト
            shiftPos(ref targetX, ref targetY, liplisX, liplisY, liplisWidth, liplisHieght, talkWidth, talkHeigh, direction);

            //移動量の算出
            moveValX = targetX - this.Left;
            moveValY = targetY - this.Top;


            //加速度的移動
            while (targetX != this.Left)
            {
                System.Threading.Thread.Sleep(5);
                System.Windows.Forms.Application.DoEvents();

                if (cnt > 100)
                {
                    this.Left = targetX;
                    this.Top = targetY;
                    break;
                }

                this.Left = ftLocX + (moveValX * cnt / 100);
                this.Top = ftLocY + (moveValY * cnt / 100);
                cnt = cnt * 2;
            }
        }
        #endregion

        /// <summary>
        /// 表示座標をチェック、シフトする
        /// </summary>
        #region shiftPos
        private void shiftPos(ref int locationX, ref int locationY, int liplisX, int liplisY, int liplisWidth, int liplisHieght, int talkWidth, int talkHeigh, int direction)
        {
            int cnt = 0;
            int talkMoveLeft = locationX;
            int talkMoveTop = locationY;

            try
            {
                while (!checkPos(talkMoveLeft, talkMoveTop))
                {
                    switch (cnt)
                    {
                        case 0:
                            talkMoveLeft = liplisX - talkWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth / 2 - this.Width / 2;
                            locationY = talkMoveTop + talkHeigh + 5;

                            break;
                        case 1:
                            talkMoveLeft = liplisX + liplisWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth / 2 - this.Width / 2;
                            locationY = talkMoveTop + talkHeigh + 5;
                            break;
                        case 2:
                            talkMoveLeft = liplisX + liplisWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth / 2 - this.Width / 2;
                            locationY = talkMoveTop + talkHeigh + 5;
                            break;
                        default:
                            talkMoveLeft = liplisX + talkWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth / 2 - this.Width / 2;
                            locationY = talkMoveTop + talkHeigh + 5;
                            return;
                    }
                    cnt++;
                    if (cnt > 3)
                    {
                        if (direction == 0)
                        {
                            talkMoveLeft = liplisX - talkWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth / 2 - this.Width / 2;
                            locationY = talkMoveTop + talkHeigh + 5;
                        }
                        else
                        {
                            talkMoveLeft = liplisX + liplisWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth / 2 - this.Width / 2;
                            locationY = talkMoveTop + talkHeigh + 5;
                        }
                        return;
                    }
                }
                if (cnt == 0)
                {
                    talkMoveLeft = liplisX - talkWidth;
                    talkMoveTop = liplisY;

                    locationX = talkMoveLeft + talkWidth / 2 - this.Width / 2;
                    locationY = talkMoveTop + talkHeigh + 5;
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }

        }
        #endregion

        ///====================================================================
        ///
        ///                       ウェブロードスレッド
        ///                         
        ///====================================================================

        /// <summary>
        /// doThread
        /// スレッドを実行する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region doThread
        public void doThread()
        {
            //画像作成するスレッドを生成
            Thread imgThread = new Thread(new ThreadStart(doShow));

            //WebBrowserはシングルスレッドアパートメントモードでのみ実行可能なのでスレッドのモードを設定して実行する
            imgThread.SetApartmentState(ApartmentState.STA);

            //スレッドスタート
            imgThread.Start();

            //デッドロック防止のため、コメント

            ////スレッド終了を待つ
            //imgThread.Join();

            ////スレッドを終了
            //imgThread.Abort();

            return;
        }
        #endregion

    }
}
