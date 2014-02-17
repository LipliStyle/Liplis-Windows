//=======================================================================
//  ClassName : CusCtlLinkLabel
//  概要      : カスタムコントロールリンクラベル
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
    public class CusCtlLinkLabel : LinkLabel
    {
        public CusCtlLinkLabel()
            : base()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }


        private bool _AllowTransparency;
        /// <summary>
        /// 背景が透明なとき背面のコントロールを描画するかどうかを示す値取得または設定します。
        /// </summary>
        [Category("表示")]
        [DefaultValue(false)]
        [Description("背景が透明なとき背面のコントロールが表示されるどうかを示します。")]
        public bool AllowTransparency
        {
            get { return _AllowTransparency; }
            set
            {
                if (_AllowTransparency == value)
                {
                    return;
                }

                _AllowTransparency = value;

                this.Invalidate();
            }
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            // 背面のコントロールを描画しない Or 背景色が不透明なので背面のコントロールを描画する必要なし
            if ((this.AllowTransparency == false) || (this.BackColor.A == 255))
            {
                base.OnPaintBackground(pevent);
                return;
            }

            // 背面のコントロールを描画
            this.DrawParentWithBackControl(pevent);

            // 背景を描画
            this.DrawBackground(pevent);
        }

        /// <summary>
        /// コントロールの背景を描画します。
        /// </summary>
        /// <param name="pevent">描画先のコントロールに関連する情報</param>
        private void DrawBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            // 背景色
            using (SolidBrush sb = new SolidBrush(this.BackColor))
            {
                pevent.Graphics.FillRectangle(sb, this.ClientRectangle);
            }

            // 背景画像
            if (this.BackgroundImage != null)
            {
                this.DrawBackgroundImage(pevent.Graphics, this.BackgroundImage, this.BackgroundImageLayout);
            }
        }

        /// <summary>
        /// コントロールの背景画像を描画します。
        /// </summary>
        /// <param name="g">描画に使用するグラフィックス オブジェクト</param>
        /// <param name="img">描画する画像</param>
        /// <param name="layout">画像のレイアウト</param>
        private void DrawBackgroundImage(Graphics g, Image img, ImageLayout layout)
        {
            Size imgSize = img.Size;

            switch (layout)
            {
                case ImageLayout.None:
                    g.DrawImage(img, 0, 0, imgSize.Width, imgSize.Height);

                    break;
                case ImageLayout.Tile:
                    int xCount = Convert.ToInt32(Math.Ceiling((double)this.ClientRectangle.Width / (double)imgSize.Width));
                    int yCount = Convert.ToInt32(Math.Ceiling((double)this.ClientRectangle.Height / (double)imgSize.Height));
                    for (int x = 0; x <= xCount - 1; x++)
                    {
                        for (int y = 0; y <= yCount - 1; y++)
                        {
                            g.DrawImage(img, imgSize.Width * x, imgSize.Height * y, imgSize.Width, imgSize.Height);
                        }
                    }

                    break;
                case ImageLayout.Center:
                    {
                        int x = 0;
                        if (this.ClientRectangle.Width > imgSize.Width)
                        {
                            x = (int)Math.Floor((double)(this.ClientRectangle.Width - imgSize.Width) / 2.0);
                        }
                        int y = 0;
                        if (this.ClientRectangle.Height > imgSize.Height)
                        {
                            y = (int)Math.Floor((double)(this.ClientRectangle.Height - imgSize.Height) / 2.0);
                        }
                        g.DrawImage(img, x, y, imgSize.Width, imgSize.Height);

                        break;
                    }
                case ImageLayout.Stretch:
                    g.DrawImage(img, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);

                    break;
                case ImageLayout.Zoom:
                    {
                        double xRatio = (double)this.ClientRectangle.Width / (double)imgSize.Width;
                        double yRatio = (double)this.ClientRectangle.Height / (double)imgSize.Height;
                        double minRatio = Math.Min(xRatio, yRatio);

                        Size zoomSize = new Size(Convert.ToInt32(Math.Ceiling(imgSize.Width * minRatio)), Convert.ToInt32(Math.Ceiling(imgSize.Height * minRatio)));

                        int x = 0;
                        if (this.ClientRectangle.Width > zoomSize.Width)
                        {
                            x = (int)Math.Floor((double)(this.ClientRectangle.Width - zoomSize.Width) / 2.0);
                        }
                        int y = 0;
                        if (this.ClientRectangle.Height > zoomSize.Height)
                        {
                            y = (int)Math.Floor((double)(this.ClientRectangle.Height - zoomSize.Height) / 2.0);
                        }
                        g.DrawImage(img, x, y, zoomSize.Width, zoomSize.Height);

                        break;
                    }
            }
        }

        /// <summary>
        /// 親コントロールと背面にあるコントロールを描画します。
        /// </summary>
        /// <param name="pevent">描画先のコントロールに関連する情報</param>
        private void DrawParentWithBackControl(System.Windows.Forms.PaintEventArgs pevent)
        {
            // 親コントロールを描画
            this.DrawParentControl(this.Parent, pevent);

            // 親コントロールとの間のコントロールを親側から描画
            for (int i = this.Parent.Controls.Count - 1; i >= 0; i--)
            {
                System.Windows.Forms.Control c = this.Parent.Controls[i];
                if (c == this)
                {
                    break;
                }
                if (this.Bounds.IntersectsWith(c.Bounds) == false)
                {
                    continue;
                }

                this.DrawBackControl(c, pevent);
            }
        }

        /// <summary>
        /// 親コントロールを描画します。
        /// </summary>
        /// <param name="c">親コントロール</param>
        /// <param name="pevent">描画先のコントロールに関連する情報</param>
        private void DrawParentControl(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs pevent)
        {
            using (Bitmap bmp = new Bitmap(c.Width, c.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    using (PaintEventArgs p = new PaintEventArgs(g, c.ClientRectangle))
                    {
                        this.InvokePaintBackground(c, p);
                        this.InvokePaint(c, p);
                    }
                }

                int offsetX = this.Left + (int)Math.Floor((double)(this.Bounds.Width - this.ClientRectangle.Width) / 2.0);
                int offsetY = this.Top + (int)Math.Floor((double)(this.Bounds.Height - this.ClientRectangle.Height) / 2.0);
                pevent.Graphics.DrawImage(bmp, this.ClientRectangle, new Rectangle(offsetX, offsetY, this.ClientRectangle.Width, this.ClientRectangle.Height), GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// 背面のコントロールを描画します。
        /// </summary>
        /// <param name="c">背面のコントロール</param>
        /// <param name="pevent">描画先のコントロールに関連する情報</param>
        private void DrawBackControl(System.Windows.Forms.Control c, System.Windows.Forms.PaintEventArgs pevent)
        {
            using (Bitmap bmp = new Bitmap(c.Width, c.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                c.DrawToBitmap(bmp, new Rectangle(0, 0, c.Width, c.Height));

                int offsetX = (c.Left - this.Left) - (int)Math.Floor((double)(this.Bounds.Width - this.ClientRectangle.Width) / 2.0);
                int offsetY = (c.Top - this.Top) - (int)Math.Floor((double)(this.Bounds.Height - this.ClientRectangle.Height) / 2.0);
                pevent.Graphics.DrawImage(bmp, offsetX, offsetY, c.Width, c.Height);
            }
        }
    }
}
