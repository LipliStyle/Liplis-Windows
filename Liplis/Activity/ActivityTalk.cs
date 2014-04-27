//=======================================================================
//  ClassName : WinTalk
//  概要      : トークウインドウ
//
//  Liplis3.0.5
//  2013/06/20 ver2.3.0 サイズ変更
//  2013/08/31 ver3.0.5 ossも設定するように変更
//                      テキストカラー、リンクカラーが適用されるように変更
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Fct;
using Liplis.MainSystem;
using Liplis.Msg;

namespace Liplis.Activity
{
    public partial class ActivityTalk : BaseSystem
    {
        ///============================
        /// ウインドウサイズ
        protected const int WIDTH = 320;
        protected const int HEIGHT = 140;

        ///============================
        /// プロパティ
        public string url { get; set; }
        public string title { get; set; }
        public string chatText { get; set; }

        ///============================
        /// フォントカラー
        protected Color linkColor = Color.Blue;
        protected Color txtColor = Color.Black;

        ///=====================================
        /// フラグ
        protected bool flgEnd     = false;
        ///=====================================
        /// リプリス
        protected MainSystem.Liplis lips;

        ///=====================================
        /// 設定
        protected ObjSetting os;
        protected ObjSkinSetting oss;       //2013/08/31 ver3.0.5 ossも設定するように変更

        ///============================
        /// デリゲート
        #region デリゲート
        protected static LpsDelegate.dlgS1ToVoid reqSetTextTotalkWindow;
        protected static LpsDelegate.dlgI5ToVoid reqSetLocation;
        protected static LpsDelegate.dlgVoidToVoid reqSetBackGround;
        protected static LpsDelegate.dlgI1ToVoid reqSetWindowMode;
        protected static LpsDelegate.dlgVoidToVoid reqCallBrowser;

        #endregion

        /// <summary>
        /// コンストラクター
        /// 2013/08/31 ver3.0.5 ossも設定するように変更
        /// </summary>
        #region ActivityTalk
        public ActivityTalk(Liplis.MainSystem.Liplis lips, ObjSetting os, ObjSkinSetting oss)
            : base()
        {
            //リプリス
            this.lips = lips;
            
            //設定オブジェクト
            this.os = os;

            //スキン設定オブジェクト 2013/08/31 ver3.0.5
            this.oss = oss;

            //コンポーネントの初期化
            InitializeComponent();

            //デリゲートの初期化
            initDelegate();

            //スタートポジションの設定を任せる
            this.StartPosition = FormStartPosition.Manual;

            //ホイールの初期化
            initWheel();

            detectContextMenu();
        }
        public ActivityTalk()
        {

        }
        #endregion

        /// <summary>
        /// initDelegate
        /// delegateの初期化
        /// </summary>
        #region initDelegate
        protected void initDelegate()
        {
            //セットテキストデリゲート
            reqSetTextTotalkWindow = new LpsDelegate.dlgS1ToVoid(dlgSetTextTotalkWindow);

            //セットロケーションデリゲート
            reqSetLocation = new LpsDelegate.dlgI5ToVoid(dlgSetLocation);

            //セットバックグラウンド
            reqSetBackGround = new LpsDelegate.dlgVoidToVoid(dlgSetBackGround);

            //セットウインドウモード
            reqSetWindowMode = new LpsDelegate.dlgI1ToVoid(dlgSetWindowMode);

            //コールブラウザ
            reqCallBrowser = new LpsDelegate.dlgVoidToVoid(dlgCallBrowser);
        }
        #endregion

        /// <summary>
        /// initWheel
        /// </summary>
        #region ホイールイベントのイニシャライズ
        protected void initWheel()
        {
            // 予め、コントロールにフォーカスを当ててやる必要があるので注意。
            //イニシャライズドコンポーネントで定義済みなのでかつあい
            //this.MouseEnter += new System.EventHandler(this.pnlIn_MouseEnter);
            // マウスのホイールを検出するイベントハンドラーを追加する。
            // MouseWheelイベントは、VS.NETのデザイナのイベント一覧に出てこないので、
            // 下記のように手動で追加する必要があります。
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pnlIn_MouseWheel);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// HTMLの初期化
        /// 2013/08/31 ver3.0.5 フォントカラーを設定するように変更
        /// </summary>
        #region initHtml
        protected void initHtml()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<html>");
            sb.Append("<head>");
            sb.Append("</head>");
            sb.Append("	<body style=\"background-repeat: no-repeat; background-position: -2px -16px; background-attachment:fixed; color: " + LpsLiplisUtil.convertColorCode(txtColor, "#000000") + "; \" background=\"" + os.getWindowPath() + "\">");
            sb.Append("	<div id=\"content\">");
            sb.Append("	    <div id=\"cntd\" style=\"font-size:12px;\">");
            sb.Append("	    </div>");
            sb.Append("	</div>");
            sb.Append("	</body>");
            sb.Append("</html>");


            string fname = LpsPathController.getJavascriptPath() + "index.htm";
            Encoding sjis = Encoding.GetEncoding("Shift-JIS");
            StreamWriter w = new StreamWriter(fname, false, sjis);
            w.Write(sb.ToString());
            w.Close();

            //this.wbTalk.DocumentText = sb.ToString();
            this.wbTalk.Navigate(fname);
        }
        #endregion


        ///====================================================================
        ///
        ///                          onLoad
        ///                         
        ///====================================================================

        /// <summary>
        /// initTalkWindow
        /// initTalkWindowの初期化
        /// ☆Miniオーバーライド
        /// </summary>
        #region initTalkWindow
        protected virtual void initTalkWindow()
        {
            //一時背景の設定と透過の設定(透明で初期化)
            setWindowProperty(FctCreateFromResource.getTranse());

            //ウインドウサイズ設定 デザイナの設定を継承
            setSize(WIDTH, HEIGHT);

            //オーパシティ
            this.Opacity = 1;

            //ショウタスクバー
            this.ShowInTaskbar = false;

            //フォントカラーの適用
            linkColor = LpsLiplisUtil.checkColor(oss.linkColor,Color.Blue);

            //黒なら補正
            if(linkColor.R == 0 && linkColor.G == 0 && linkColor.B == 0){linkColor = Color.Blue;}

            //リンクカラー
            lnkLbl.LinkColor = linkColor;

            //テキストカラー
            txtColor = LpsLiplisUtil.checkColor(oss.textColor, Color.Black);

        }
        #endregion

        ///====================================================================
        ///
        ///                          onRecive
        ///                         
        ///====================================================================
        

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region WinTalk_Load
        protected void WinTalk_Load(object sender, EventArgs e)
        {
            initTalkWindow();

            //背景の初期化
            dlgSetBackGround();
        }
        #endregion

        /// <summary>
        /// リンクラベルクリックイベント
        /// </summary>
        #region linkLabelClickEvent
        protected void linkLabelClickEvent(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    callBrowser();
                }
                catch (System.Exception err)
                {
                    LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                }
            }
        }
        #endregion

        /// <summary>
        /// リンクラベルクリックイベント
        /// </summary>
        #region linkLabelOtherClickEvent
        protected void linkLabelOtherClickEvent(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel l = (LinkLabel)sender;

            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    callBrowser((string)l.Tag);
                }
                catch (System.Exception err)
                {
                    LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                }
            }
        }
        #endregion


        /// <summary>
        /// pnlIn_MouseEnter
        /// マウスエンターイベント
        /// </summary>
        #region pnlIn_MouseEnter
        protected void pnlIn_MouseEnter(object sender, EventArgs e)
        {
            this.wbTalk.Focus();
        }
        #endregion

        /// <summary>
        /// pnlIn_MouseWheel
        /// マウスホイールイベント
        /// </summary>
        #region pnlIn_MouseWheel
        protected void pnlIn_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            wbTalk.Document.ActiveElement.ScrollTop = wbTalk.Document.ActiveElement.ScrollTop - (e.Delta / 30);
        }
        #endregion

        /// <summary>
        /// pnlIn_MouseEnter
        /// マウスエンターイベント
        /// ☆Miniオーバーライド
        /// </summary>
        #region ActivityTalk_MouseEnter
        protected virtual void ActivityTalk_MouseEnter(object sender, EventArgs e)
        {
            if (wbTalk != null)
            {
                this.wbTalk.Focus();
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void btnNext_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_NEXT, "");
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void btnUrlCopy_Click(object sender, EventArgs e)
        {
            LpsLiplisUtil.setDataToClipBord(url);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void btnWebJump_Click(object sender, EventArgs e)
        {
            callBrowser();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void btnTweet_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_TWEET, "");
        }
        #endregion

        ///====================================================================
        ///
        ///                          onReciveCms
        ///                         
        ///====================================================================

        /// <summary>
        /// tsmiCopy_Click
        /// 文章のコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiCopy_Click
        protected void tsmiCopy_Click(object sender, EventArgs e)
        {
            LpsLiplisUtil.setDataToClipBord(chatText);
        }
        #endregion

        /// <summary>
        /// tsmiShow_Click
        /// 記事の参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiShow_Click
        protected void tsmiShow_Click(object sender, EventArgs e)
        {
            try
            {
                callBrowser();
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// tsmitTitleCopy_Click
        /// タイトルのコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmitTitleCopy_Click
        protected void tsmitTitleCopy_Click(object sender, EventArgs e)
        {
            LpsLiplisUtil.setDataToClipBord(title);
        }
        #endregion

        /// <summary>
        /// tsmitLinkCopy_Click
        /// リンクkのコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmitLinkCopy_Click
        protected void tsmitLinkCopy_Click(object sender, EventArgs e)
        {
            LpsLiplisUtil.setDataToClipBord(url);
        }
        #endregion

        /// <summary>
        /// tsmitShow_Click
        /// 記事の参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmitShow_Click
        protected void tsmitShow_Click(object sender, EventArgs e)
        {
            try
            {
                callBrowser();
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// cmst_Opening
        /// cmstがオープンされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region cmst_Opening
        protected void cmst_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cmst.Items[5].Enabled = LpsRegularEx.checkUrl(this.url);

            cmst.Items[4].Enabled = cmst.Items[3].Enabled = LpsLiplisUtil.domainCheck(this.url, LpsDefineMost.URL_NICO_DOMAIN);
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
        /// ActivityTalk_FormClosing
        /// フォームクロージングキャンセラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityTalk_FormClosing
        protected void ActivityTalk_FormClosing(object sender, FormClosingEventArgs e)
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
        /// setTextTotalkWindow
        /// テキストのセット
        /// ☆Miniオーバーライド
        /// </summary>
        #region setTextTotalkWindow
        public virtual void setTextTotalkWindow(string msg)
        {
            //デリゲート経由でテキストを更新
            reqSetTextTotalkWindow(msg);
        }
        #endregion

        /// <summary>
        /// setLocation
        /// 座標のセット
        /// </summary>
        #region setLocation
        public void setLocation(int liplisX, int liplisY, int liplisWidth, int liplisHieght, int direction)
        {
            reqSetLocation(liplisX, liplisY, liplisWidth, liplisHieght,direction);
        }
        #endregion

        /// <summary>
        /// setBackGround
        /// 背景のセット
        /// </summary>
        #region setBackGround
        public void setBackGround()
        {
            reqSetBackGround();
        }
        #endregion

        /// <summary>
        /// setWindowMode
        /// ウインドウモードのセット
        /// </summary>
        #region setWindowMode
        public void setWindowMode(int mode)
        {
            Invoke(reqSetWindowMode,mode);
        }
        #endregion

        /// <summary>
        /// setWindowMode
        /// ウインドウモードのセット
        /// </summary>
        #region callBrowser
        public void callBrowser()
        {
            new LpsDelegate.dlgVoidToVoid(doCallBrowzer).BeginInvoke(null, null);
        }
        public void callBrowser(string url)
        {
            new LpsDelegate.dlgS1ToVoid(doCallBrowzer2).BeginInvoke(url, null, null);
        }
        #endregion

        /// <summary>
        /// activityInit
        /// アクティビティを初期化する
        /// </summary>
        #region activityInit
        public void activityInit()
        {
            new LpsDelegate.dlgVoidToVoid(dlgActivityInit).BeginInvoke(null, null);
        }
        #endregion

        /// <summary>
        /// 通常化
        /// </summary>
        #region onNormalize
        public void onNormalize()
        {
            this.Show();
        }
        #endregion

        /// <summary>
        /// 最小化
        /// </summary>
        #region onMinimize
        public void onMinimize()
        {
            this.Hide();
        }
        #endregion

        ///====================================================================
        ///
        ///                       デリゲート
        ///                         
        ///====================================================================
        
        /// <summary>
        /// setTextWindow
        /// ☆Miniオーバーライド
        /// </summary>
        #region setTextWindow
        protected virtual void setTextWindow(string str)
        {
            HtmlElement element = null;
            element = wbTalk.Document.GetElementById("cntd");
            element.InnerHtml = str;
            wbTalk.Document.Window.ScrollTo(0,99999);//WebBrowser
        }
        protected void dlgSetTextTotalkWindow(string msg)
        {
            setTextWindow(msg);
            this.Refresh();
        }
        #endregion

        /// <summary>
        /// dlgSetLocation
        /// ☆Miniオーバーライド
        /// </summary>
        #region dlgSetLocation
        protected virtual void dlgSetLocation(int liplisX, int liplisY, int liplisWidth, int liplisHieght, int direction)
        {
            int ftLocX = this.Left;
            int ftLocY = this.Top;
            int targetX = liplisX - this.Width;
            int targetY = liplisY;
            int moveValX = 0;
            int moveValY = 0;
            int cnt = 1;

            shiftPos(ref targetX, ref targetY, liplisX, liplisY, liplisWidth, liplisHieght, direction);

            moveValX = targetX - this.Left;
            moveValY = targetY - this.Top;

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
        /// dlgSetBackGround
        /// 背景を設定する
        /// ☆Miniオーバーライド
        /// </summary>
        #region dlgSetBackGround
        protected virtual void dlgSetBackGround()
        {
            //HTMLへのセット
            this.initHtml();

            //背景のセット
            this.BackgroundImage.Dispose();
            this.BackgroundImage = null;
            this.BackgroundImage = new Bitmap(os.getWindowPath());
        }
        #endregion

        /// <summary>
        /// dlgSetWindowMode
        /// ウインドウモードを設定する
        /// </summary>
        #region dlgSetWindowMode
        protected void dlgSetWindowMode(int mode)
        {
            if (mode == 0)
            {
                setModeTextOnly();
            }
            else if (mode == 1)
            {
                setModeWithUrl();
            }
            else
            {
                setModeTextOnly();
            }
        }
        #endregion

        /// <summary>
        /// ブラウザをコールする
        /// </summary>
        #region dlgCallBrowser
        protected void dlgCallBrowser()
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception fileNotFoundErr)
            {
                Console.Write(fileNotFoundErr);
                //lips.chatFixedSentence(ComDefine.err_BrowzerErr);
                //lips.expression = ComDefine.EXPRESSION_CRY;
            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
        }
        #endregion

        /// <summary>
        /// dlgActivityInit
        /// </summary>
        #region dlgActivityInit
        [STAThread]
        public void dlgActivityInit()
        {
            try
            {
                //if (LpsLiplisUtil.domainCheck(url, LpsDefineMost.URL_NICO_DOMAIN))
                //{
                //    anb = new ActivityNicoBrowser();
                //    Invoke(new LpsDelegate.dlgI2S1ToVoid(anb.activityInit), this.Location.X, this.Location.Y - 230,url);
                //}
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {

            }

        }
        #endregion

        ///====================================================================
        ///
        ///                          その他メソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// 表示座標をチェック、シフトする
        /// </summary>
        #region shiftPos
        protected virtual void shiftPos(ref int locationX, ref int locationY, int liplisX, int liplisY, int liplisWidth, int liplisHieght ,int direction)
        {
            int cnt = 0;

            try
            {
                while (!checkPos(locationX, locationY))
                {
                    switch (cnt)
                    {
                        case 0:
                            locationX = liplisX - this.Width;
                            locationY = liplisY;
                            break;
                        case 1:
                            locationX = liplisX + liplisWidth;
                            locationY = liplisY;
                            break;
                        case 2:
                            locationX = liplisX + liplisWidth;
                            locationY = liplisY;
                            break;
                        default:
                            locationX = liplisX + this.Width;
                            locationY = liplisY;
                            return;
                    }
                    cnt++;
                    if (cnt > 3)
                    {
                        if (direction == 0)
                        {
                            locationX = liplisX - this.Width;
                            locationY = liplisY;
                        }
                        else
                        {
                            locationX = liplisX + liplisWidth;
                            locationY = liplisY;
                        }
                        return;
                    }
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }

        }
        #endregion

        /// <summary>
        /// setModeTextOnly
        /// テキストオンリーモードに設定する
        /// </summary>
        #region setModeTextOnly
        protected void setModeTextOnly()
        {
            try
            {
                this.lnkLbl.Text = "";
                this.lnkLbl.Top = 4;
                this.lnkLbl.Left = 8;
                this.lnkLbl.Width = 304;
                this.lnkLbl.Height = 12;
                this.lnkLbl.Enabled = false;
                this.lnkLbl.Visible = false;
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// setModeWithUrl
        /// ウイズURLモードに設定する
        /// </summary>
        #region setModeWithUrl
        protected void setModeWithUrl()
        {
            try
            {
                this.lnkLbl.Text = title;
                this.lnkLbl.Top = 4;
                this.lnkLbl.Left = 8;
                this.lnkLbl.Width = 304;
                this.lnkLbl.Height = 12;
                this.lnkLbl.Enabled = true;
                this.lnkLbl.Visible = true;

            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// プロセス起動のスレッド
        /// </summary>
        #region doCallBrowzer
        protected void doCallBrowzer()
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception fileNotFoundErr)
            {
                Console.Write(fileNotFoundErr);
            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
        }
        #endregion

        /// <summary>
        /// プロセス起動のスレッド
        /// </summary>
        #region doCallBrowzer2
        protected void doCallBrowzer2(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception fileNotFoundErr)
            {
                Console.Write(fileNotFoundErr);
            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
        }
        #endregion

        #region コンテキストメニューの差し替え(廃止)
        public virtual void detectContextMenu()
        {
            if (wbTalk.Document != null)
            {
                wbTalk.Document.ContextMenuShowing += new HtmlElementEventHandler(Document_ContextMenuShowing);
            }
        }

        void Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
        {
            cmst.Show(e.MousePosition);
            e.ReturnValue = false;
        }
        #endregion


    }
}
