//=======================================================================
//  ClassName : frmParent
//  概要      : こちらに透過するオブジェクトを置く
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using Liplis.Activity;
using Liplis.Common;
using Liplis.MainSystem;
using Liplis.Msg;

namespace Liplis.Widget.WidBrowser
{
    public partial class WidgetBrowserParent11 : WidgetBaseParent
    {
        string url;
        int interval;

        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WidgetBrowserParent
        public WidgetBrowserParent11(ObjWidgetSetting setting, ActivityRssReader arr, WidgetBaseSetting mwm, string url, int interval)
            : base(setting, arr, mwm)
        {
            InitializeComponent();
            this.url = url;
            this.interval = interval;
            updateBrowser(url);

            this.wb.ScriptErrorsSuppressed = false;       //スクリプトエラーの無視

            initTimer();
        }
        #endregion

        /// <summary>
        /// ベースフォームをインスタンスかするメソッドを定義する
        /// </summary>
        #region loadBaseForm
        protected override void loadBaseForm()
        {
            f2 = new WidgetBaseBase();
        }
        #endregion


        /// <summary>
        /// タイマーを更新する
        /// </summary>
        #region initTimer
        protected void initTimer()
        {
            this.timUpdate.Interval = this.interval;
            this.timUpdate.Start();
        }
        #endregion

        ///====================================================================
        ///
        ///                             onRecive
        ///                         
        ///====================================================================


        #region イベント
        private void bar_MouseMove(object sender, MouseEventArgs e) { mouseMovePitatto(e); }
        private void bar_MouseDown(object sender, MouseEventArgs e) { mouseDown(e); }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            btnClose_Click(sender, e);
        }
        #endregion

        private void frmParent_Load(object sender, EventArgs e)
        {
            parentFormInit();
            initBarLabel(this.bar);
            initBackGroundLabel();
        }

        private void updateBrowser(string url)
        {
            this.wb.Visible = false;
            this.wb.Navigate(url);
            this.wb.Visible = true;
        }

        private void timUpdate_Tick(object sender, EventArgs e)
        {
            updateBrowser(url);
        }
 

    }
}
