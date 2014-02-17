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

namespace Liplis.Widget
{
    public partial class frmParent : WidgetBaseParent
    {
        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region frmParent
        public frmParent(ObjWidgetSetting setting, ActivityWidget arr, WidgetBaseSetting mwm)
            : base(setting, arr, mwm)
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
            f2 = new WidgetBaseBase();
        }
        #endregion


        ///====================================================================
        ///
        ///                             onRecive
        ///                         
        ///====================================================================


        #region イベント
        private void bar_MouseMove(object sender, MouseEventArgs e) { mouseMoveWidget(e); }
        private void bar_MouseDown(object sender, MouseEventArgs e) { mouseDown(e); }

        #endregion

        private void frmParent_Load(object sender, EventArgs e)
        {
            parentFormInit();
            initBackGroundLabel();
        }


 

    }
}
