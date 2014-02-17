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

namespace Liplis.Widget.WidLan
{
    [Serializable]
    public class WidgetLanSetting : WidgetBaseSetting
    {
        public int    interfaceNum { get; set; }
        public string interfaceName { get; set; }

        /// <summary>
        /// WidgetLanSetting
        /// メモリ設定
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="kbn">区分</param>
        /// <param name="sizeKbn">サイズ区分</param>
        /// <param name="icon">アイコン</param>
        /// <param name="url">URL</param>
        #region WidgetLanSetting
        public WidgetLanSetting(string title, int kbn, Size size, int interfaceNum, string interfaceName)
            :base(title,kbn,size)
        {
            this.interfaceNum = interfaceNum;
            this.interfaceName = interfaceName;
        }
        #endregion

    }
}
