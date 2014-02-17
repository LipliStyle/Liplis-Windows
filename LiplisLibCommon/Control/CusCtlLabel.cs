//=======================================================================
//  ClassName : CusCtlLabel
//  概要      : カスタムコントロールラベル
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Liplis.Control
{
    public class CusCtlLabel : Label
    {
        public CusCtlLabel()
            : base()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);      
        }


    }
}
