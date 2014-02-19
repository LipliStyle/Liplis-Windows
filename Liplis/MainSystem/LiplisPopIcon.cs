//=======================================================================
//  ClassName : LiplisPopIcon
//  概要      : ポップアイコン
//
//  Liplis2.1
//  Copyright(c) 2010-2012 LipliStyle.Sachin
//=======================================================================
using System;
using System.Drawing;
using CircularMenu;
using Liplis.Common;
using Liplis.Msg;


namespace Liplis.MainSystem
{
    public class LiplisPopIcon : IDisposable
    {
        ///==========================
        /// プロパティ
        private int wid = 0;
        private int hi = 0;
        private Liplis lips;

        //=====================================
        /// フラグ
        public bool flgActive { get; set; }// アイコン表示フラグ

        ///=============================
        /// 透過色
        protected Color TRANS_COLOR = Color.FromArgb(255, 254, 240, 254);

        //=====================================
        // cmp
        private CircularMenuPopup cmp;

        //=====================================
        // ステータス
        private int sleepMode    = 0;   //0:起床状態 1:おやすみ状態

        //=====================================
        // mo
        //MenuOption moBrw;
        //MenuOption moWid;
        MenuOption moEnd;
        MenuOption moMin;
        MenuOption moChr;
        MenuOption moRss;
        MenuOption moMen;
        MenuOption moNex;
        MenuOption moSlp;
        MenuOption moLog;
        MenuOption moBat;
        //MenuOption moDwn;

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public LiplisPopIcon(Liplis lips, int sleepMode)
        {
            this.lips = lips;
            this.sleepMode = sleepMode;
            this.cmp = lips.cmp;
            this.wid = lips.Width;
            this.hi = lips.Height + LiplisDefine.LPS_ICON_DIF;            //Liplisと56ずらず(12, アイコン32, 12)
            this.InitializeComponent();
        }
        #endregion

        /// <summary>
        /// InitializeComponent
        /// </summary>
        #region InitializeComponent
        public void InitializeComponent()
        {
            ObjWindowFile owf                   = lips.getOwf();
            ObjBattery ob                       = lips.getObtry();

            //moBrw = createMenuOption(owf.ico_brw, new System.EventHandler(this.btnRssBrw_Click), true);
            //moWid = createMenuOption(owf.ico_wid, new System.EventHandler(this.btnWidget_Clickbtn), true);
            moEnd = createMenuOption(owf.ico_pow, new System.EventHandler(this.btnEnd_Click), true);
            moMin = createMenuOption(owf.ico_tray, new System.EventHandler(this.btnMinimize_Click), true);
            moChr = createMenuOption(owf.ico_char, new System.EventHandler(this.btnChar_Click), true);
            moRss = createMenuOption(owf.ico_rss, new System.EventHandler(this.btnRss_Click), true);
            moMen = createMenuOption(owf.ico_setting, new System.EventHandler(this.btnMenu_Clickbtn), true);
            moNex = createMenuOption(owf.ico_next, new System.EventHandler(this.btnNext_Click), false);
            moLog = createMenuOption(owf.ico_log, new System.EventHandler(this.btnLog_Click), true);
            moBat = createMenuOption(ob.nowBatteryImage, new System.EventHandler(this.btnBat_Click), true);
            //moDwn = createMenuOption(owf.ico_down, new System.EventHandler(this.btnDown_Clickbtn), true);

            if (sleepMode == 1)
            {
                moSlp = createMenuOption(owf.ico_waikUp, new System.EventHandler(this.btnSleep_Click), true);
            }
            else
            {
                moSlp = createMenuOption(owf.ico_zzz, new System.EventHandler(this.btnSleep_Click), true);
            }
        }


        #endregion

        /// <summary>
        /// createMenuOption
        /// メニューオプションを生成する
        /// </summary>
        #region createMenuOption
        private MenuOption createMenuOption(Bitmap bmp, EventHandler eh, bool clickClose)
        {
            MenuOption opt = new MenuOption();
            //初期化
            opt.DisabledImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            opt.DisabledImage.TransparencyKey        = TRANS_COLOR;
            opt.HoverImage.DropShadow.ShadowColor    = System.Drawing.Color.Black;
            opt.HoverImage.TransparencyKey           = TRANS_COLOR;
            opt.Image.DropShadow.ShadowColor         = System.Drawing.Color.Black;
            opt.Image.TransparencyKey                = TRANS_COLOR;
            opt.PressedImage.DropShadow.ShadowColor  = System.Drawing.Color.Black;
            opt.PressedImage.TransparencyKey         = TRANS_COLOR;

            //アイコンのサイズ調整
            //Liplis2.1.0 改善
            Bitmap fixIcon = fixIconSize(bmp);

            //アイコン設定
            opt.DisabledImage.Image = fixIcon;
            opt.HoverImage.Image    = fixIcon;
            opt.Image.Image         = fixIcon;
            opt.PressedImage.Image  = fixIcon;

            //イベントの追加
            opt.Click += eh;

            //クリッククローズの設定
            opt.m_click_close = clickClose;

            //コントロールに追加
            this.cmp.MenuOptions.Add(opt);

            return opt;
        }

        /// <summary>
        /// アイコンサイズの調整
        /// Liplis2.1.0 改善
        /// </summary>
        #region fixIconSize
        private Bitmap fixIconSize(Bitmap bmp)
        {
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(32, 32);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            using (Graphics g = Graphics.FromImage(canvas))
            {
                //補間方法として高品質双三次補間
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //画像を縮小して描画する
                g.DrawImage(bmp, 0, 0, 32, 32);
                //補間方法として高品質双三次補間を指定する
            }
            return canvas;
        }
        #endregion

        /// <summary>
        /// メニューオプションを作成する
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="eh"></param>
        /// <param name="clickClose"></param>
        /// <returns></returns>
        #region createMenuOptionNonAdd
        private MenuOption createMenuOptionNonAdd(Bitmap bmp, EventHandler eh, bool clickClose)
        {
            MenuOption opt = new MenuOption();
            //初期化
            opt.DisabledImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            opt.DisabledImage.TransparencyKey = TRANS_COLOR;
            opt.HoverImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            opt.HoverImage.TransparencyKey = TRANS_COLOR;
            opt.Image.DropShadow.ShadowColor = System.Drawing.Color.Black;
            opt.Image.TransparencyKey = TRANS_COLOR;
            opt.PressedImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            opt.PressedImage.TransparencyKey = TRANS_COLOR;

            //アイコン設定
            opt.DisabledImage.Image = bmp;
            opt.HoverImage.Image = bmp;
            opt.Image.Image = bmp;
            opt.PressedImage.Image = bmp;

            //イベントの追加
            opt.Click += eh;

            //クリッククローズの設定
            opt.m_click_close = clickClose;

            return opt;
        }
        #endregion
        #endregion

        /// <summary>
        /// クリックイベント
        /// </summary>
        #region btnNext_Click
        private void btnNext_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_NEXT, "");
        }
        #endregion
        #region btnSleep_Click
        private void btnSleep_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_SLEEP, "");
        }
        #endregion
        #region btnRssBrw_Click
        private void btnRssBrw_Click(object sender, EventArgs e)
        {
            //lips.onRecive(LiplisDefine.LM_RSS_READ, "");sddss
            lips.onRecive(LiplisDefine.LM_RSS_BRW, "");
        }
        #endregion
        #region btnLog_Click
        private void btnLog_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_LOG, "");
        }
        #endregion
        #region btnEnd_Click
        private void btnEnd_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_END, "");
        }
        #endregion
        #region btnMinimize_Click
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_MINIMIZE, "Icon");
        }
        #endregion
        #region btnRss_Click
        private void btnRss_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_RSS, "");
        }
        #endregion
        #region btnChar_Click
        private void btnChar_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_CHAR, "");
        }
        #endregion
        #region btnMenu_Clickbtn
        private void btnMenu_Clickbtn(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_SETTING, "");
        }
        #endregion
        #region btnWidget_Clickbtn
        private void btnWidget_Clickbtn(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_WIDGET, "");
        }
        #endregion
        #region btnDown_Clickbtn
        private void btnDown_Clickbtn(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_DOWN, "");
        }
        #endregion
        #region btnBat_Click
        private void btnBat_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_BATTERY, "");
        }
        #endregion

        /// <summary>
        /// Dispose
        /// </summary>
        #region Dispose
        public void Dispose()
        {
            cmp.Dispose();
            moEnd.Dispose();
            moMin.Dispose();
            moChr.Dispose();
            moRss.Dispose();
            moMen.Dispose();
            moNex.Dispose();
            moSlp.Dispose();
            moLog.Dispose();

            cmp = null;
            moEnd = null;
            moMin = null;
            moChr = null;
            moRss = null;
            moMen = null;
            moNex = null;
            moSlp = null;
            moLog = null;
        }
        #endregion

    }
}
