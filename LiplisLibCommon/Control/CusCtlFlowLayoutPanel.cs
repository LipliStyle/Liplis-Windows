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
    public class CusCtlFlowLayoutPanel : FlowLayoutPanel
    {
        public CusCtlFlowLayoutPanel()
            : base()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
    }
}
