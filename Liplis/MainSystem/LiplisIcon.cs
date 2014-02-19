//=======================================================================
//  ClassName : LiplisIcon
//  概要      : アイコン
//
//  Liplis3.0
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Msg;

namespace Liplis.MainSystem
{
    public partial class LiplisIcon : BaseSystem
    {
        ///==========================
        /// プロパティ
        protected int wid = 0;
        protected int hi  = 0;
        protected Liplis lips;

        //=====================================
        /// フラグ
        public bool flgActive { get; set; }// アイコン表示フラグ
        protected bool flgEnd = false;


        ///====================================================================
        ///
        ///                          onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public LiplisIcon(Liplis lips)
            : base()
        {
            this.lips = lips;
            this.wid = lips.Width;
            this.hi = lips.Height + LiplisDefine.LPS_ICON_DIF;            //Liplisと56ずらず(12, アイコン32, 12)
            InitializeComponent();
            initBtnImage();
        }
        #endregion

        /// <summary>
        /// initBtnImage
        /// ボタンイメージの設定
        /// </summary>
        #region コンストラクター
        public void initBtnImage()
        {
            //画像設定
            ObjWindowFile owf      = lips.getOwf();
            ObjBattery ob          = lips.getObtry();
            this.btnNext.Image     = owf.ico_next;
            this.btnSleep.Image    = owf.ico_zzz;
            this.btnMenu.Image     = owf.ico_setting;
            this.btnEnd.Image      = owf.ico_pow;
            this.btnMinimize.Image = owf.ico_tray;
            this.btnBattery.Image      = ob.nowBatteryImage;
            this.btnChar.Image     = owf.ico_char;
            this.btnLog.Image      = owf.ico_log;
        }
        #endregion

        ///====================================================================
        ///
        ///                              onDelete
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
        /// onDelete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityRss_FormClosing
        protected void LiplisIcon_FormClosing(object sender, FormClosingEventArgs e)
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
        ///                          onLoad
        ///                         
        ///====================================================================

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region LiplisIcon_Load
        protected void LiplisIcon_Load(object sender, EventArgs e)
        {
            initLiplisIcon();
        }
        #endregion

        /// <summary>
        /// initLiplisIcon
        /// 初期化処理
        /// ☆Miniオーバーライド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region initLiplisIcon
        protected virtual void initLiplisIcon()
        {
            //画面サイズの調整と
            setWindowProperty(FctCreateBackground.createTransParentBgi(this.wid,this.hi));

            //サーフェスの設定
            this.btnNext.Top        = this.Height - 124;
            this.btnNext.Left       = this.Width / 5 - 16;
            this.btnSleep.Top       = this.Height - 124;
            this.btnSleep.Left      = this.Width / 5 * 2 - 16;
            this.btnMinimize.Top    = this.Height - 124;
            this.btnMinimize.Left   = this.Width / 5 * 3 - 16;
            this.btnEnd.Top         = this.Height - 124;
            this.btnEnd.Left        = this.Width / 5 * 4 - 16;

            this.btnMenu.Top = this.Height - 80;
            this.btnMenu.Left = this.Width / 5  - 16;
            this.btnLog.Top = this.Height - 80;
            this.btnLog.Left = this.Width / 5 * 2 - 16;
            this.btnChar.Top = this.Height - 80;
            this.btnChar.Left = this.Width / 5 * 3 - 16;
            this.btnBattery.Top = this.Height - 80;
            this.btnBattery.Left = this.Width / 5 * 4 - 16;

        }
        #endregion

        ///====================================================================
        ///
        ///                             onRecive
        ///                         
        ///====================================================================
        #region btnNext_Click
        protected void btnNext_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_NEXT, "");
        }
        #endregion
        #region btnSleep_Click
        protected void btnSleep_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_SLEEP, "");
        }
        #endregion
        #region btnMenu_Click
        protected void btnMenu_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_SETTING, "");
        }
        #endregion
        #region btnLog_Click
        protected void btnLog_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_LOG, "");
        }
        #endregion
        #region btnEnd_Click
        protected void btnEnd_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_END, "");
        }
        #endregion
        #region btnMinimize_Click
        protected void btnMinimize_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_MINIMIZE, "Icon");
        }
        #endregion
        #region btnRss_Click
        protected void btnRss_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_BATTERY, "");
        }
        #endregion
        #region btnChar_Click
        protected void btnChar_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_CHAR, "");
        }
        #endregion
        #region btnBrw_Click
        protected void btnBrw_Click(object sender, EventArgs e)
        {
            //lips.onRecive(LiplisDefine.LM_RSS_READ, "");
            lips.onRecive(LiplisDefine.LM_RSS_BRW, "");
        }
        #endregion
        #region btnWid_Click
        protected void btnWid_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_WIDGET, "");
        }
        #endregion
        #region btnDown_Click
        protected void btnDown_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_DOWN, "");
        }
        #endregion


        #region LiplisIcon_DragDrop
        protected void LiplisIcon_DragDrop(object sender, DragEventArgs e)
        {
            Invoke(new LpsDelegate.dlgListStrToVoid(lips.ar.dropDataCheckLiplis), FctDragData.getDropTextList(e));
        }
        #endregion
        #region LiplisIcon_DragEnter
        protected void LiplisIcon_DragEnter(object sender, DragEventArgs e)
        {
            FctDragData.getDrag(e);
        }
        #endregion
        #region onNormalize
        public void onNormalize()
        {
            this.Show();
        }
        #endregion
        #region onMinimize
        public void onMinimize()
        {
            this.Hide();
        }
        #endregion
        #region LiplisIcon_MouseDown
        protected void LiplisIcon_MouseDown(object sender, MouseEventArgs e)
        {
            lips.Liplis_MouseDown(sender, e);
        }
        #endregion
        #region LiplisIcon_MouseMove
        protected void LiplisIcon_MouseMove(object sender, MouseEventArgs e)
        {
            lips.Liplis_MouseMove(sender, e);
        }
        #endregion
        #region LiplisIcon_MouseUp
        protected void LiplisIcon_MouseUp(object sender, MouseEventArgs e)
        {
            lips.mouseUp(sender, e);
        }
        #endregion


        ///====================================================================
        ///
        ///                            アイコン変更
        ///                         
        ///====================================================================

        ///====================================================================
        ///
        ///                       パブリックメソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// 背景を設定する
        /// </summary>
        #region setSurface
        public void setSurface(int left, int top, int width, int height)
        {
            this.Left = left;
            this.Top = top - 56;
            this.Width = width;
            this.Height = height + 56;
        }
        #endregion

        /// <summary>
        /// setSleepIcon
        /// スリープアイコンを設定する
        /// </summary>
        #region setSleepIcon
        public void setSleepIcon()
        {
            this.btnSleep.Image = lips.getOwf().ico_zzz;
            this.Refresh();
        }
        #endregion

        /// <summary>
        /// setWaikUpIcon
        /// ウェイクアップアイコンを設定する
        /// </summary>
        #region setWaikUpIcon
        public void setWaikUpIcon()
        {
            this.btnSleep.Image = lips.getOwf().ico_waikUp;
            this.Refresh();
        }
        #endregion

        /// <summary>
        /// setWaikUpIcon
        /// ウェイクアップアイコンを設定する
        /// </summary>
        #region setBatteryIcon
        public void setBatteryIcon()
        {
            this.btnBattery.Image = lips.getObtry().nowBatteryImage;
            this.Refresh();
        }
        #endregion



    }
}
