//=======================================================================
//  ClassName : TransparentLabel
//  概要      : 透過ラベル
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin.Sachin
//=======================================================================
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Liplis.Control
{
    public class TransparentLabel : System.Windows.Forms.Label
    {

        private void UpdateRegion()
        {
            // コントロールの ClientSize と同じ大きさの Bitmap クラスを生成します。
            Bitmap foregroundBitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            // 文字列などの背景以外の部分を描画します。
            using (Graphics g = Graphics.FromImage(foregroundBitmap))
            {
                this.DrawForeground(g);
            }

            int w = foregroundBitmap.Width;
            int h = foregroundBitmap.Height;

            Rectangle rect = new Rectangle(0, 0, w, h);
            Region region = new Region(rect);

            // できた Bitmap クラスからピクセルの色情報を取得します。
            BitmapData bd = foregroundBitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int stride = bd.Stride;
            int bytes = stride * h;
            byte[] bgraValues = new byte[bytes];
            Marshal.Copy(bd.Scan0, bgraValues, 0, bytes);
            foregroundBitmap.UnlockBits(bd);
            foregroundBitmap.Dispose();

            // 描画された部分だけの領域を作成します。
            int line;
            for (int y = 0; y < h; y++)
            {
                line = stride * y;
                for (int x = 0; x < w; x++)
                {
                    // アルファ値が 0 は背景
                    if (bgraValues[line + x * 4 + 3] == 0)
                    {
                        region.Exclude(new Rectangle(x, y, 1, 1));
                    }
                }
            }

            // Region に描画された領域を設定します。
            this.Region = region;
        }

        private void DrawForeground(Graphics g)
        {
            using (SolidBrush sb = new SolidBrush(this.ForeColor))
            {
                Rectangle r = new Rectangle(
                    this.Padding.Left,
                    this.Padding.Top,
                    this.ClientRectangle.Width - this.Padding.Left - this.Padding.Right,
                    this.ClientRectangle.Height - this.Padding.Top - this.Padding.Bottom
                );

                g.DrawString(this.Text, this.Font, sb, r);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if ((this.DesignMode == false) && (this.BackColor == Color.Transparent))
            {
                this.UpdateRegion();
            }
            base.OnTextChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if ((this.DesignMode == false) && (this.BackColor == Color.Transparent))
            {
                this.UpdateRegion();
            }
            base.OnSizeChanged(e);
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            if ((this.DesignMode == false) && (this.BackColor == Color.Transparent))
            {
                this.UpdateRegion();
            }
            base.OnPaddingChanged(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (this.BackColor != Color.Transparent)
            {
                base.OnPaintBackground(pevent);
            }
            else
            {
                if (this.DesignMode == true)
                {
                    base.OnPaintBackground(pevent);
                }
            }
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.BackColor != Color.Transparent)
            {
                base.OnPaint(e);
            }
            else
            {
                // UpdateRegion メソッドで描画したものと同じ内容を描画します
                this.DrawForeground(e.Graphics);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
