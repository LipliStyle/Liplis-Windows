//=======================================================================
//  ClassName : WidgetBaseSetting
//  概要      : ウィジェットベースセッティング
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using Liplis.Common;
using Liplis.Fct;
using System.Windows.Forms;
using Liplis.Widget;

namespace Liplis.Widget.WidSys
{
    [Serializable]
    public class WidgetSysSetting :WidgetBaseSetting
    {
        ///=====================================
        /// オブジェクト
        public int interfaceNum { get; set; }

        /// <summary>
        /// MsgWindowMatrix
        /// コンストラクター
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="wid"></param>
        /// <param name="hi"></param>
        #region WidgetSysSetting
        public WidgetSysSetting(string title, int kbn, Size size, int interfaceNum)
            : base(title, kbn, size)
        {
            this.interfaceNum = interfaceNum;
        }
        #endregion

    }
}
