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

namespace Liplis.Widget.WidRss
{
    public partial class _WidgetRssParent : WidgetBaseParent
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
        #region WidgetRssParent
        public _WidgetRssParent(ObjWidgetSetting o, ActivityWidget a, WidgetBaseSetting s)
           : base(o, a, s)
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(s.size.Width * 160, s.size.Height * 160);
            this.g.Size = new System.Drawing.Size(s.size.Width * 160, s.size.Height * 160 + 6);
            this.g.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bar_MouseMove);
            this.g.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bar_MouseDown);
            WidgetRssSetting ss = (WidgetRssSetting)s;
            this.url = ss.url;
            this.interval = ss.interval;
        }
        #endregion

        /// <summary>
        /// ベースフォームをインスタンスかするメソッドを定義する
        /// </summary>
        #region loadBaseForm
        protected override void loadBaseForm()
        {
            WidgetRssSetting ss = (WidgetRssSetting)s;
            f2 = new WidgetRssBase(o,ss);
            WidgetRssBase f = (WidgetRssBase)f2;
            f.update();
            f.initTimer();
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

        private void frm12Parent_Load(object sender, EventArgs e)
        {
            parentFormInit();
            initBackGroundLabel();
        }
    }
}
