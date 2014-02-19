//=======================================================================
//  ClassName : CustomTextBox
//  extends   : TextBox
//  概要      : チャットオブジェクト
//
//  Liplis2.3
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Liplis.Common;

namespace Liplis.Cmp.Form
{
    [Designer("")]
    [DesignTimeVisible(true)]
    [ToolboxItem(true)]
    public class CustomTextBox : TextBox
    {
        ///==========================
        /// 内容
        public PictureBox pictureBox { get; set; }
        public Point p { get; set; }


        /// <summary>
        /// コンストラクター
        /// </summary>
        #region CustomTextBox
        public CustomTextBox()
            : base()
        {
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            this.Controls.Add(pictureBox);
        }
        #endregion

        /// <summary>
        /// イニシャルコンポーネント
        /// </summary>
        #region InitializeComponent
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// WndProc
        /// オーバーライド
        /// </summary>
        /// <param name="m">Message</param>
        #region WndProc
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case LpsWindowsApiDefine.WM_PAINT:

                    Bitmap bmpCaptured =
                      new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
                    Bitmap bmpResult =
                      new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
                    Rectangle r =
                      new Rectangle(0, 0, this.ClientRectangle.Width,
                      this.ClientRectangle.Height);

                    CaptureWindow(this, ref bmpCaptured);
                    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                    this.BackColor = Color.Transparent;

                    ImageAttributes imgAttrib = new ImageAttributes();

                    ColorMap[] colorMap = new ColorMap[1];

                    colorMap[0] = new ColorMap();

                    colorMap[0].OldColor = Color.White;

                    colorMap[0].NewColor = Color.Transparent;

                    imgAttrib.SetRemapTable(colorMap);

                    Graphics g = Graphics.FromImage(bmpResult);

                    g.DrawImage(bmpCaptured, r, 0, 0, this.ClientRectangle.Width,
                        this.ClientRectangle.Height, GraphicsUnit.Pixel, imgAttrib);

                    g.Dispose();

                    pictureBox.Image = (Image)bmpResult.Clone();
                    break;

                case LpsWindowsApiDefine.WM_HSCROLL:

                case LpsWindowsApiDefine.WM_VSCROLL:

                    this.Invalidate(); // repaint

                    // if you use scrolling then add these two case statements


                    break;

            }


            //switch (m.WParam.ToInt32())
            //{
            //    case WindowsApiDefine.WM_LBUTTONDOWN:
            //        p = new Point(Cursor.Position.X, Cursor.Position.Y);
            //        break;
            //}
            base.WndProc(ref m);
        }
        #endregion

        /// <summary>
        /// CaptureWindow
        /// ウインドウキャプチャー
        /// </summary>
        /// <param name="control"></param>
        /// <param name="bitmap"></param>
        #region CaptureWindow
        private static void CaptureWindow(System.Windows.Forms.Control control, ref Bitmap bitmap)
        {
            Graphics g = Graphics.FromImage(bitmap);
            //int i = (int)(WindowsApiDefine.PRF_CLIENT | WindowsApiDefine.PRF_ERASEBKGND);
            IntPtr iPtr = new IntPtr(14);
            IntPtr hdc = g.GetHdc();
            LpsWindowsApi.SendMessage(control.Handle, LpsWindowsApiDefine.WM_PRINT, hdc, iPtr);
            g.ReleaseHdc(hdc);
            g.Dispose();
        }
        #endregion
    }


}
