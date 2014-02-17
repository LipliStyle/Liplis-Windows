//=======================================================================
//  ClassName : CustomTextBox
//  概要      : カスタムテキストボックス
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Liplis.Common;


namespace Liplis.Control
{
    public class CusCtlWebBrowser : WebBrowser
    {
        public CusCtlWebBrowser()
            : base()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
    }
}
