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
using Liplis.Control;

namespace Liplis.Widget.WidRss
{
    public partial class WidgetRssParent : WidgetBaseParent
    {
        string url;
        int interval;
        WidgetRssBase f;

        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WidgetRssParent
        public WidgetRssParent(ObjWidgetSetting o, ActivityWidget a, WidgetBaseSetting s)
           : base(o, a, s)
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(s.size.Width * 160, s.size.Height * 160);
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
            f = (WidgetRssBase)f2;
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

        /// <summary>
        /// ウインドウのマウスダウンイベント
        /// 投下のイベントをキャッチする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region WidgetRssParent_MouseDown
        private void WidgetRssParent_MouseDown(object sender, MouseEventArgs e)
        {
            CusCtlPanel p = new CusCtlPanel();
            CusCtlLinkLabel l = new CusCtlLinkLabel();

            int x = e.X - 12;
            int y = e.Y - 12;

            foreach (System.Windows.Forms.Control c in f.pnlRss.Controls)
            {
                if (c.Left <= x && x <= (c.Left + c.Width) &&
                c.Top <= y && y <= (c.Top + c.Height))
                {
                    if (c.GetType() == p.GetType())
                    {
                        foreach (System.Windows.Forms.Control cs in c.Controls)
                        {
                            if (cs.GetType() == l.GetType())
                            {
                                f.LinkLblClick(cs, e);
                            }
                        }
                    }
                }
            }
            mouseDown(e);
        }
        #endregion

        /// <summary>
        /// マウスムーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region WidgetRssParent_MouseMove
        private void WidgetRssParent_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMoveWidget(e);
        }
        #endregion





    }
}
