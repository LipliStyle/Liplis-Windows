//=======================================================================
//  ClassName : CusCtlTabControl
//  概要      : カスタムタブ
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System.Windows.Forms;

namespace Liplis.Control
{
    public class CusCtlTabPage : TabPage
    {
        public CusCtlTabPage()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
    }
}
