﻿//=======================================================================
//  ClassName : CusCtlPictureBox
//  概要      : カスタムコントロールピクチャーボック
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System.Windows.Forms;

namespace Liplis.Control
{
    public class CusCtlPictureBox : PictureBox
    {
        public CusCtlPictureBox()
            : base()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
    }
}
