//=======================================================================
//  ClassName : CurveBorderDraw
//  概要      : カスタムボタン
//
//
//Panel コントロールに独自の境界線を描画するサンプルを紹介します。
//
//Panel コントロールは既に境界線を表示する BorderStyle プロパティを持っているためそれを再定義し、独自の境界線スタイルとして使用しています。
//境界線のスタイルとして Pen クラスの DashStyle プロパティで描画できる破線を、全て描画できるようにしています。
//
//境界線の色を示す BorderColor プロパティと、境界線の幅を示す BorderWidth プロパティを追加しています。
//
//Panel コントロールはコンテナ機能を持っているため、Padding プロパティの値の最小値を BorderWidth プロパティの値になるようにしています。
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Liplis.Control
{
    public class CusCtlCurveBorderDrawLabel : System.Windows.Forms.Label
    {
        public CusCtlCurveBorderDrawLabel()
        {
            base.BackColor = Color.Transparent;
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._BorderColor = Color.Black;
            this._BorderStyle = Liplis.Control.BorderStyle.None;
            this._BorderWidth = 1;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent) {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e) {
            Rectangle ar = this.ClientRectangle;
            ar.X += ar.X + Convert.ToInt32(Math.Floor((double)this.BorderWidth / 2));
            ar.Y += ar.Y + Convert.ToInt32(Math.Floor((double)this.BorderWidth / 2));
            ar.Width -= (this.BorderWidth + 1);
            ar.Height -= (this.BorderWidth + 1);
            Rectangle lr = this.ClientRectangle;
            bool canArc = ((ar.Width > 0) && (ar.Height > 0));

            using (GraphicsPath gp = new GraphicsPath()) {
                gp.StartFigure();
                if ((this.RadiusTopRight > 0) && (canArc == true)) {
                    int w = this.RadiusTopRight > ar.Width ? ar.Width : this.RadiusTopRight;
                    int h = this.RadiusTopRight > ar.Height ? ar.Height : this.RadiusTopRight;
                    gp.AddArc(ar.Right - w, ar.Top, w, h, 270, 90);
                } else {
                    gp.AddLine(lr.Right, lr.Top, lr.Right, lr.Top);
                }
                if ((this.RadiusBottomRight > 0) && (canArc == true)) {
                    int w = this.RadiusBottomRight > ar.Width ? ar.Width : this.RadiusBottomRight;
                    int h = this.RadiusBottomRight > ar.Height ? ar.Height : this.RadiusBottomRight;
                    gp.AddArc(ar.Right - w, ar.Bottom - h, w, h, 0, 90);
                } else {
                    gp.AddLine(lr.Right, lr.Bottom, lr.Right, lr.Bottom);
                }
                if ((this.RadiusBottomLeft > 0) && (canArc == true)) {
                    int w = this.RadiusBottomLeft > ar.Width ? ar.Width : this.RadiusBottomLeft;
                    int h = this.RadiusBottomLeft > ar.Height ? ar.Height : this.RadiusBottomLeft;
                    gp.AddArc(ar.Left, ar.Bottom - h, w, h, 90, 90);
                } else {
                    gp.AddLine(lr.Left, lr.Bottom, lr.Left, lr.Bottom);
                }
                if ((this.RadiusTopLeft > 0) && (canArc == true)) {
                    int w = this.RadiusTopLeft > ar.Width ? ar.Width : this.RadiusTopLeft;
                    int h = this.RadiusTopLeft > ar.Height ? ar.Height : this.RadiusTopLeft;
                    gp.AddArc(ar.Left, ar.Top, w, h, 180, 90);
                } else {
                    gp.AddLine(lr.Left, lr.Top, lr.Left, lr.Top);
                }
                gp.CloseFigure();

                if (canArc == true) {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                }
                // 背景を描画
                using (SolidBrush sb = new SolidBrush(this.BackColor)) {
                    e.Graphics.FillPath(sb, gp);
                }
                // 境界線を描画
                if (this.BorderStyle != BorderStyle.None) {
                    using (Pen p = new Pen(this.BorderColor, this.BorderWidth)) {
                        p.DashStyle = this.ConvertToDashStyle(this.BorderStyle);
                        e.Graphics.DrawPath(p, gp);
                    }
                }
                if (canArc == true) {
                    e.Graphics.SmoothingMode = SmoothingMode.Default;
                }
            }

            base.OnPaint(e);
        }

        private DashStyle ConvertToDashStyle(Liplis.Control.BorderStyle style)
        {
            return (DashStyle)style - 1;
        }

        #region BackColor

        private Color _BackColor;
        public new Color BackColor {
            get {
                if (this._BackColor != Color.Empty) {
                    return this._BackColor;
                }

                if (this.Parent != null) {
                    return this.Parent.BackColor;
                }

                return System.Windows.Forms.Control.DefaultBackColor;
            }
            set {
                this._BackColor = value;
                this.Invalidate();
            }
        }

        public override void ResetBackColor() {
            this.BackColor = Color.Empty;
        }

        private Boolean ShouldSerializeBackColor() {
            return this._BackColor != Color.Empty;
        }

        #endregion

        private Color _BorderColor;
        [Category("表示")]
        [DefaultValue(typeof(Color), "Black")]
        [Description("コントロールの境界線色を取得または設定します。")]
        public Color BorderColor {
            get { return this._BorderColor; }
            set {
                this._BorderColor = value;
                this.Invalidate();
            }
        }

        private BorderStyle _BorderStyle;
        [Category("表示")]
        [DefaultValue(typeof(BorderStyle), "None")]
        [Description("コントロールの境界線スタイルを取得または設定します。")]
        public new BorderStyle BorderStyle {
            get { return this._BorderStyle; }
            set {
                this._BorderStyle = value;
                this.Invalidate();
            }
        }

        private int _BorderWidth;
        [Category("表示")]
        [DefaultValue(1)]
        [Description("コントロールの境界線の幅を取得または設定します。")]
        public int BorderWidth {
            get { return this._BorderWidth; }
            set {
                this._BorderWidth = value;
                this.Invalidate();
            }
        }

        private int _RadiusTopLeft;
        [Category("表示")]
        [DefaultValue(0)]
        [Description("コントロールの左上の角の半径を取得または設定します。")]
        public int RadiusTopLeft {
            get { return this._RadiusTopLeft; }
            set {
                this._RadiusTopLeft = value;
                this.Invalidate();
            }
        }

        private int _RadiusTopRight;
        [Category("表示")]
        [DefaultValue(0)]
        [Description("コントロールの右上の角の半径を取得または設定します。")]
        public int RadiusTopRight {
            get { return this._RadiusTopRight; }
            set {
                this._RadiusTopRight = value;
                this.Invalidate();
            }
        }

        private int _RadiusBottomLeft;
        [Category("表示")]
        [DefaultValue(0)]
        [Description("コントロールの左下の角の半径を取得または設定します。")]
        public int RadiusBottomLeft {
            get { return this._RadiusBottomLeft; }
            set {
                this._RadiusBottomLeft = value;
                this.Invalidate();
            }
        }

        private int _RadiusBottomRight;
        [Category("表示")]
        [DefaultValue(0)]
        [Description("コントロールの右下の角の半径を取得または設定します。")]
        public int RadiusBottomRight {
            get { return this._RadiusBottomRight; }
            set {
                this._RadiusBottomRight = value;
                this.Invalidate();
            }
        }
    }
}