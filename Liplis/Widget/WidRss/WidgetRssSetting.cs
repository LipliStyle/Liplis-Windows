//=======================================================================
//  ClassName : WidgetBrowserSetting
//  概要      : ウィジェットブラウザセッティング
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;

namespace Liplis.Widget.WidRss
{
    [Serializable]
    public class WidgetRssSetting : WidgetBaseSetting
    {
        ///=====================================
        /// オブジェクト
        public string url { get; set; }
        public int interval { get; set; }

        /// <summary>
        /// WidgetBrowserSetting
        /// ブラウザ設定
        /// </summary>
        /// <param name="title">タイトル</param>
        /// <param name="kbn">区分</param>
        /// <param name="sizeKbn">サイズ区分</param>
        /// <param name="icon">アイコン</param>
        /// <param name="url">URL</param>
        #region WidgetRssSetting
        public WidgetRssSetting(string title, int kbn, Size size, string url, int interval)
            :base(title,kbn,size)
        {
            this.url = url;
            this.interval = interval;
        }
        #endregion
    }
}
