//=======================================================================
//  ClassName : WidgetBrowserSetting
//  概要      : ウィジェットブラウザセッティング
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Liplis.Widget.WidBrw
{
    [Serializable]
    public class WidgetBrwSetting : WidgetBaseSetting
    {
        ///=====================================
        /// オブジェクト
        public string url        { get; set; }
        public int    interval   { get; set; }
        public bool   autoUpdate { get; set; }

        /// <summary>
        /// WidgetBrowserSetting
        /// ブラウザ設定
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="kbn">区分</param>
        /// <param name="sizeKbn">サイズ区分</param>
        /// <param name="icon">アイコン</param>
        /// <param name="url">URL</param>
        #region WidgetBrowserSetting
        public WidgetBrwSetting(string title, int kbn, Size size, string url, int interval, bool autoUpdate)
            : base(title, kbn, size)
        {
            this.url = url;
            this.interval = interval;
            this.autoUpdate = autoUpdate;
        }
        #endregion
    }
}
