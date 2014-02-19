//=======================================================================
//  ClassName : ActivityTalkMini
//  概要      : トークウインドウミニ
//
//  Liplis3.0.5
//  2013/06/23 ver2.0.1 ミニバージョン
//  2013/08/31 ver3.0.5 ossも設定するように変更
//                      テキストカラー、リンクカラーが適用されるように変更
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Msg;

namespace Liplis.Activity
{
    public partial class ActivityTalkMini : ActivityTalk
    {
        ///============================
        /// ウインドウサイズ
        private new const int WIDTH = 200;
        private new const int HEIGHT = 85;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="lips"></param>
        /// <param name="os"></param>
        #region ActivityTalkMini
        public ActivityTalkMini(Liplis.MainSystem.Liplis lips, ObjSetting os, ObjSkinSetting oss)
        {
            this.lips = lips;
            this.os = os;
            this.oss = oss;         //スキン設定オブジェクト 2013/08/31 ver3.0.5
            this.StartPosition = FormStartPosition.Manual;

            InitializeComponent();
            initDelegate();
            detectContextMenu();
        }
        public ActivityTalkMini()
        {

        }
        #endregion

        /// <summary>
        /// initTalkWindow
        /// オーバーライド
        /// </summary>
        #region initTalkWindow
        protected override void initTalkWindow()
        {
            //一時背景の設定と透過の設定(透明で初期化)
            setWindowProperty(FctCreateFromResource.getTranse());

            //ウインドウサイズ設定 デザイナの設定を継承
            setSize(WIDTH, HEIGHT);

            this.Opacity = 1;

            this.ShowInTaskbar = false;

            //リンクに下線を
            this.lnkLblMsg.LinkBehavior = LinkBehavior.NeverUnderline;

            //フォントカラーの適用
            linkColor = LpsLiplisUtil.checkColor(oss.linkColor, Color.Blue);

            //黒なら補正
            if (linkColor.R == 0 && linkColor.G == 0 && linkColor.B == 0) { linkColor = Color.Blue; }

            //リンクカラー
            lnkLblMsg.LinkColor = linkColor;

            //テキストカラー
            txtColor = LpsLiplisUtil.checkColor(oss.textColor, Color.Black);
        }
        #endregion

        ///====================================================================
        ///
        ///                          イベントハンドラ
        ///                         
        ///====================================================================

        #region イベントハンドラ
        private void ActivityTalkMini_Load(object sender, EventArgs e)
        {
            base.WinTalk_Load(sender, e);
        }

        private void ActivityTalkMini_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.ActivityTalk_FormClosing(sender, e);
        }

        private void ActivityTalkMini_MouseEnter(object sender, EventArgs e)
        {
            base.ActivityTalk_MouseEnter(sender, e);
        }

        private void lnkLblMsg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
 
        ///====================================================================
        ///
        ///                           オーバーライド
        ///                         
        ///====================================================================

        /// <summary>
        /// setTextWindow
        /// </summary>
        #region setTextWindow
        protected override void setTextWindow(string str)
        {
            this.lnkLblMsg.Text = str;
        }
        #endregion

        /// <summary>
        /// dlgSetLocation
        /// </summary>
        #region dlgSetLocation
        protected override void dlgSetLocation(int liplisX, int liplisY, int liplisWidth, int liplisHieght, int direction)
        {
            int ftLocX = this.Left;
            int ftLocY = this.Top;
            int targetX = liplisX;
            int targetY = liplisY - this.Height;
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
        /// 表示座標をチェック、シフトする
        /// </summary>
        #region shiftPos
        protected override void shiftPos(ref int locationX, ref int locationY, int liplisX, int liplisY, int liplisWidth, int liplisHieght, int direction)
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
        /// dlgSetBackGround
        /// 背景を設定する
        /// </summary>
        #region dlgSetBackGround
        protected override void dlgSetBackGround()
        {
            //背景のセット
            this.BackgroundImage.Dispose();
            this.BackgroundImage = null;
            this.BackgroundImage = LpsLiplisUtil.reductionBitmap(new Bitmap(os.getWindowPath()),200,85);
        }
        #endregion

        #region WB使わないため、処理なしで上書き
        public override void detectContextMenu()
        {
            
        }
        protected override void ActivityTalk_MouseEnter(object sender, EventArgs e)
        {

        }
        #endregion




    }
}
