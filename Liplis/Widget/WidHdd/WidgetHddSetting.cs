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

namespace Liplis.Widget.WidHdd
{
    [Serializable]
    public class WidgetHddSetting: WidgetBaseSetting
    {
        ///=====================================
        /// プロパティ
        public string drive { get; set; }

        /// <summary>
        /// WidgetMemSetting
        /// メモリ設定
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="kbn">区分</param>
        /// <param name="sizeKbn">サイズ区分</param>
        /// <param name="icon">アイコン</param>
        /// <param name="url">URL</param>
        #region WidgetHddSetting
        public WidgetHddSetting(string title, int kbn, Size size, string drive)
            :base(title,kbn,size)
        {
            this.drive = drive;
        }
        #endregion

    }
}
