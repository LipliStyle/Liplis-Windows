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

namespace Liplis.Widget.WidCpu
{
    [Serializable]
    public class WidgetCpuSetting: WidgetBaseSetting
    {

        /// <summary>
        /// WidgetCpuSetting
        /// CPU設定
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="kbn">区分</param>
        /// <param name="sizeKbn">サイズ区分</param>
        /// <param name="icon">アイコン</param>
        /// <param name="url">URL</param>
        #region WidgetCpuSetting
        public WidgetCpuSetting(string title, int kbn, Size size)
            :base(title,kbn,size)
        {
        }
        #endregion

    }
}
