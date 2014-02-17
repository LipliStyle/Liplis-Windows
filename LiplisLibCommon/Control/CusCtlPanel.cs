//=======================================================================
//  ClassName : CustomPanel
//  概要      : カスタムパネル
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System.Windows.Forms;


namespace Liplis.Control
{
    public class CusCtlPanel : Panel
    {
        public CusCtlPanel() : base()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.HorizontalScroll.Visible = false;
        }
    }
}
