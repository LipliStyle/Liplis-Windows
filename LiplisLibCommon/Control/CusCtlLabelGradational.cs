//=======================================================================
//  ClassName : CusCtlLabelGradational
//  概要      : グラデーション背景を設定できるカスタムラベル
//
//  Liplis2.0
//  Copyright(c) 2010-2012 LipliStyle.Sachin
//=======================================================================
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Liplis.Control
{
    public class CusCtlLabelGradational : System.Windows.Forms.Label
    {
        #region コンストラクター
        public CusCtlLabelGradational()
        {
            base.BackColor = Color.Transparent;
            this._GradientMode = LinearGradientMode.Horizontal;
        }
        #endregion

        #region OnPaintBackground
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if ((this.BackColor.A < 255) || (this.BackColor2.A < 255))
            {
                base.OnPaintBackground(pevent);
            }

            using (LinearGradientBrush lgb = new LinearGradientBrush(this.ClientRectangle, this.BackColor, this.BackColor2, this.GradientMode))
            {
                pevent.Graphics.FillRectangle(lgb, this.ClientRectangle);
            }
        }
        #endregion


        #region BackColor

        private Color _BackColor;
        public new Color BackColor
        {
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

        #region BackColor2

        private Color _BackColor2;
        [Category("表示")]
        [Description("コンポーネントの背景色のグラデーションの終了色です。")]
        public Color BackColor2
        {
            get {
                if (this._BackColor2 != Color.Empty) {
                    return this._BackColor2;
                }
                if (this.Parent != null) {
                    return this.Parent.BackColor;
                }

                return System.Windows.Forms.Control.DefaultBackColor;
            }
            set {
                this._BackColor2 = value;
                this.Invalidate();
            }
        }

        public void ResetBackColor2() {
            this.BackColor2 = Color.Empty;
        }

        private Boolean ShouldSerializeBackColor2() {
            return this._BackColor2 != Color.Empty;
        }

        #endregion

        private LinearGradientMode _GradientMode;
        [Category("表示")]
        [DefaultValue(typeof(LinearGradientMode), "Horizontal")]
        [Description("コンポーネントの背景色のグラデーションの方向です。")]
        public LinearGradientMode GradientMode
        {
            get { return this._GradientMode; }
            set {
                this._GradientMode = value;
                this.Invalidate();
            }
        }
    }
}