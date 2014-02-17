//=======================================================================
//  ClassName : frmBase
//  概要      : こちらに透過しないオブジェクトを置く
//              親側からよば
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Windows.Forms;
using Liplis.MainSystem;

namespace Liplis.Widget
{
    public partial class WidgetBaseBase : BaseSystem
    {
        public WidgetBaseParent f1 { get; set; }

        public WidgetBaseBase()
        {
            this.Opacity = 1;
            this.ShowInTaskbar = false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                // 自身をアクティブフォーカスさせない
                case 0x21:  // WM_MOUSEACTIVATE
                m.Result = new IntPtr(3);   // MA_NOACTIVATE
                return;
            }
            base.WndProc(ref m);
        }

        protected virtual void endWidget(object sender, MouseEventArgs e)
        {
            f1.Close();
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOOLWINDOW = 0x00000080;

                // ExStyle に WS_EX_TOOLWINDOW ビットを立てる
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TOOLWINDOW;

                return cp;
            }
        }
    }
}
