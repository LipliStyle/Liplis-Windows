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

namespace Liplis.Widget.WidLan
{
    public partial class WidgetLanParent : WidgetBaseParent
    {
        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WidgetLanParent
        public WidgetLanParent(ObjWidgetSetting o, ActivityWidget a, WidgetBaseSetting s)
            : base(o, a, s)
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// ベースフォームをインスタンスかするメソッドを定義する
        /// </summary>
        #region loadBaseForm
        protected override void loadBaseForm()
        {
            f2 = new WidgetLanBase(o, (WidgetLanSetting)s);
        }
        #endregion


        ///====================================================================
        ///
        ///                             onRecive
        ///                         
        ///====================================================================


        #region イベント
        private void WidgetCpuParent_MouseDown(object sender, MouseEventArgs e) { f2.mouseDownOv(e); }
        private void WidgetCpuParent_MouseMove(object sender, MouseEventArgs e) { f2.mouseMoveWidgetOv(e); }
        #endregion

        private void frmParent_Load(object sender, EventArgs e)
        {
            parentFormInit();
            initBackGroundLabel();
        }


    }
}
