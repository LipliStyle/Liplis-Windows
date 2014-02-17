//=======================================================================
//  ClassName : frmParent
//  概要      : こちらに透過するオブジェクトを置く
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Windows.Forms;
using Liplis.Activity;
using Liplis.Msg;
using Liplis.Widget.WidRss;
using Liplis.Xml;
using Liplis.Common;
using Liplis.Widget.WidBrw;
using System.Drawing;

namespace Liplis.Widget
{
    public partial class WidgetBrowserParent : WidgetBaseParent
    {
        private string url;
        private int interval;
        private bool autoUpdate;

        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WidgetBrowserParent
        public WidgetBrowserParent(ObjWidgetSetting o, ActivityRssReader a, WidgetBaseSetting s)
           : base(o, a, s)
        {
            WidgetBrowserSetting ss = (WidgetBrowserSetting)s;                                      //設定メッセージを変換しておく
            InitializeComponent();
            this.Size                = new Size(s.size.Width * 160, s.size.Height * 160);
            this.g.Size              = new Size(s.size.Width * 160, s.size.Height * 160 + 6);
            this.wb.Size             = new Size(s.size.Width * 160 - 8, s.size.Height * 160 - 20);
            this.g.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WidgetRssParent_MouseDoubleClick);
            this.wb.ScriptErrorsSuppressed = true;

            this.url                 = ss.url;
            this.interval            = ss.interval;
            this.autoUpdate          = ss.autoUpdate;
            this.updateBrowser();
            this.initTimer();
        }
        #endregion

        /// <summary>
        /// タイマーを更新する
        /// </summary>
        #region initTimer
        internal void initTimer()
        {
            this.timUpdate.Interval = this.interval;
            this.timUpdate.Start();
        }
        #endregion

        /// <summary>
        /// ベースフォームをインスタンスかするメソッドを定義する
        /// </summary>
        #region loadBaseForm
        protected override void loadBaseForm()
        {
            WidgetBrowserSetting ss = (WidgetBrowserSetting)s;
            f2 = new WidgetBrowserBase(o, ss);
        }
        #endregion

        ///====================================================================
        ///
        ///                             onRecive
        ///                         
        ///====================================================================
        
        #region イベント
        private void bar_MouseMove(object sender, MouseEventArgs e){ mouseMoveWidget(e); }
        private void bar_MouseDown(object sender, MouseEventArgs e){ mouseDown(e); }
        #endregion

        /// <summary>
        /// frm12Parent_Load
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region frm12Parent_Load
        private void frm12Parent_Load(object sender, EventArgs e)
        {
            parentFormInit();
            initBackGroundLabel();
        }
        #endregion

        /// <summary>
        /// WidgetRssParent_MouseDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region WidgetRssParent_MouseDoubleClick
        private void WidgetRssParent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// timUpdate_Tick
        /// タイムティック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region timUpdate_Tick
        private void timUpdate_Tick(object sender, EventArgs e)
        {
            if (this.autoUpdate)
            {
                updateBrowser();
            }
        }
        #endregion

        /// <summary>
        /// updateBrowser
        /// URLを更新して読み直す
        /// </summary>
        #region update
        internal void updateBrowser()
        {
            this.wb.Visible = false;
            this.wb.Navigate(this.url);
            this.wb.Visible = true;
        }
        #endregion

    }
}
