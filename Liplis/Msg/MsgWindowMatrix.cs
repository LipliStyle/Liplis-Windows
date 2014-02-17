//=======================================================================
//  ClassName : MsgShortNews
//  概要      : ショートニュースメッセージ
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using Liplis.Common;
using Liplis.Fct;
using System.Windows.Forms;

namespace Liplis.Msg
{
    [Serializable]
    public class MsgWindowMatrix
    {
        public string windowId  { get; set; }
        public string title     { get; set; }
        public int kbn          { get; set; }
        public string url       { get; set; }
        public int sizeKbn      { get; set; }
        public Rectangle rect   { get; set; }
        public Bitmap icon      { get; set; }
        public DataGridViewRow dgvCtl { get; set; }

        /// <summary>
        /// MsgWindowMatrix
        /// コンストラクター
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="wid"></param>
        /// <param name="hi"></param>
        #region MsgWindowMatrix
        public MsgWindowMatrix(string windowId, string title, int kbn, string url,int sizeKbn, Bitmap ico, int x, int y, int wid, int hi)
        {
            this.windowId = windowId;
            this.title    = title;
            this.kbn      = kbn;
            this.url      = url;
            this.rect     = new Rectangle(x, y, wid, hi);
            this.icon     = ico;
        }
        public MsgWindowMatrix(string title, int kbn, string url, int sizeKbn, Bitmap icon)
        {
            this.title    = title;
            this.windowId = LpsLiplisUtil.getName(10);
            this.kbn      = kbn;
            this.url      = url;
            this.sizeKbn  = sizeKbn;
            this.rect     = new Rectangle(0, 0, 0, 0);
            this.icon     = icon;
            this.dgvCtl = null;
        }
        #endregion

    }
}
