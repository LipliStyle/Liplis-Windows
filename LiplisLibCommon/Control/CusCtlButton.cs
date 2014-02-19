//=======================================================================
//  ClassName : CustomPanel
//  概要      : カスタムボタン
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin.Sachin
//=======================================================================
using System.Windows.Forms;

namespace Liplis.Control
{
    public class CusCtlButton : Button
    {
        public CusCtlButton() : base()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
    }
}
