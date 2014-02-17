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

namespace Liplis.Widget
{
    public partial class WidgetRss11Parent : WidgetBaseParent
    {
        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WidgetRss11Parent
        public WidgetRss11Parent(ObjWidgetSetting setting, ActivityRssReader arr, WidgetBaseSetting mwm)
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
            f2 = new frmBase12();
        }
        #endregion

        ///====================================================================
        ///
        ///                             onRecive
        ///                         
        ///====================================================================
        
        #region イベント
        private void bar_MouseMove(object sender, MouseEventArgs e){ mouseMovePitatto(e); }
        private void bar_MouseDown(object sender, MouseEventArgs e){ mouseDown(e); }
        #endregion

        private void frm12Parent_Load(object sender, EventArgs e)
        {
            parentFormInit();
            initBackGroundLabel();
        }





 

    }
}
