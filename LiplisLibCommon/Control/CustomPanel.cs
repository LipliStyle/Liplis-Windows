//=======================================================================
//  ClassName : CustomPanel
//  概要      : カスタムパネル
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using Liplis.Common;


namespace Liplis.Control
{
    public class CustomPanel : Panel
    {
        public CustomPanel()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.HorizontalScroll.Visible = false;
        }
    }
}
