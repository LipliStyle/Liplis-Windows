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

namespace Liplis.Widget.WidSys
{
    public partial class WidgetSysParent : WidgetBaseParent
    {
        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WidgetSysParent
        public WidgetSysParent(ObjWidgetSetting setting, ActivityWidget arr, WidgetBaseSetting s)
            :base(setting, arr, s)
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
            WidgetSysSetting s2 = (WidgetSysSetting)s;
            f2 = new WidgetSysBase(s2.interfaceNum);
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

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WidgetSysParent_Load(object sender, EventArgs e)
        {
            parentFormInit();
            initBackGroundLabel();
        }

    }
}
