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

namespace Liplis.Widget
{
    [Serializable]
    public class WidgetBaseSetting
    {
        ///=====================================
        /// オブジェクト
        public string           windowId  { get; set; }
        public string           title     { get; set; }
        public int              kbn       { get; set; }
        public Size             size      { get; set; }
        public Rectangle        rect      { get; set; }

        /// <summary>
        /// MsgWindowMatrix
        /// コンストラクター
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="wid"></param>
        /// <param name="hi"></param>
        #region MsgWindowMatrix
        public WidgetBaseSetting(string title, int kbn, Size size)
        {
            this.title    = title;
            this.windowId = LpsLiplisUtil.getName(10);
            this.kbn      = kbn;
            this.size     = size;
            this.rect     = new Rectangle(0, 0, 0, 0);
        }
        #endregion

        /// <summary>
        /// getSizeStr
        /// サイズをストリングで取得する
        /// </summary>
        /// <returns></returns>
        #region getSizeStr
        public string getSizeStr()
        {
            return size.Height + "×" + size.Width;
        }

        #endregion

    }
}
