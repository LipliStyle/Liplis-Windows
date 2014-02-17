﻿//=======================================================================
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
    public partial class WidgetSysParent : WidgetBaseParent
    {
        int interfaceNum = 0;

        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region frmParent
        public WidgetSysParent(ObjWidgetSetting setting, ActivityRssReader arr, MsgWindowMatrix mwm, int interfaceNum)
            :base(setting, arr, mwm)
        {
            InitializeComponent();
            this.interfaceNum = interfaceNum;
        }
        #endregion

        /// <summary>
        /// ベースフォームをインスタンスかするメソッドを定義する
        /// </summary>
        #region loadBaseForm
        protected override void loadBaseForm()
        {
            f2 = new WidgetSysBase(interfaceNum);
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

        private void WidgetSysParent_Load(object sender, EventArgs e)
        {
            parentFormInit();
            initBarLabel(this.bar);
            initBackGroundLabel();
        }

 

    }
}
