//=======================================================================
//  ClassName : UsrCtlLoadingImage
//  概要      : ユーザーコントロールローディングオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2012 LipliStyle. All Rights Reserved. 
//=======================================================================
//LineColor
//ラインの色。
//Speed
//ラインが回転する速さ。範囲：-359～359。1以上で時計回り、-1以下で反時計回り、0で停止。
//LineWeightRatio
//ラインの太さの割合。範囲：0.0～1.0。画像で上から 0.25、1.0に設定されている。
//Sweep
//描画される弧の角度。範囲：1～359。
//Interval
//描画の更新間隔(ms)。範囲：1～
//IsSquare
//trueのとき、縦と横のサイズが等しくなる。falseのとき自由にサイズを設定できる

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Liplis.Control
{
    public partial class UsrCtlLoadingImage : UserControl
    {
        #region LineColor
        private Color _lineColor = Color.Blue;
        public Color LineColor
        {
            get { return this._lineColor; }
            set { this._lineColor = value; this.SetImages(); }
        }
        #endregion

        #region Speed
        [DefaultValue(10)]
        public int Speed
        {
            get { return this._speed; }
            set
            {
                if (value < -359 || value > 359) throw new ArgumentOutOfRangeException(); this._speed = value; this.SetImages();
            }
        }
        private int _speed = 10;
        #endregion

        #region Interval
        [DefaultValue(30)]
        public int Interval
        {
            get { return this._interval; }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(); this._interval = value; this.tim.Interval = value;
            }
        }
        private int _interval = 30;

        #endregion

        #region Sweep
        [DefaultValue(135)]
        public int Sweep
        {
            get { return this._sweep; }
            set
            {
                if (value < 1 || value > 359) throw new ArgumentOutOfRangeException(); this._sweep = value; this.SetImages();
            }
        }
        private int _sweep = 135;
        #endregion

        #region LineWeightRatio
        [DefaultValue(0.25f)]
        public float LineWeightRatio
        {
            get { return this._lineWeightRatio; }
            set
            {
                if (value < 0.0f || value > 1.0f) throw new ArgumentOutOfRangeException(); this._lineWeightRatio = value; this.SetImages();
            }
        }
        private float _lineWeightRatio = 0.25f;
        #endregion

        #region IsSquare
        [DefaultValue(true)]
        public bool IsSquare
        {
            get { return this._isSquare; }
            set
            {
                this._isSquare = value; if (this.IsSquare)
                    this.Width = this.Height;

                this.SetImages();
            }
        }
        private bool _isSquare = true;
        #endregion

        private Image[] Images;
        private int index;

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region LoadingImage
        public UsrCtlLoadingImage()
        {
            InitializeComponent();
            this.tim.Interval = this.Interval;
            this.Size = new Size(16, 16);
            this.SetImages();
        }
        #endregion

        /// <summary>
        /// イメージセット
        /// </summary>
        #region SetImages
        private void SetImages()
        {
            float w = this.LineWeightRatio / 2.0f * this.Width;
            float h = this.LineWeightRatio / 2.0f * this.Height;

            int count;
            if (this.Speed == 0)
                count = 1;
            else
                count = 360 / this.Speed * (this.Speed < 0 ? -1 : 1);

            int speedc = 0;

            if (this.Images != null)
                for (int i = 0; i < this.Images.Length; i++)
                    if (this.Images[i] != null)
                        this.Images[i].Dispose();

            this.Images = new Image[count];

            Pen p = new Pen(this.LineColor, w);

            for (int i = 0; i < count; i++)
            {
                this.Images[i] = new Bitmap(this.Width, this.Height);
                using (Graphics g = Graphics.FromImage(this.Images[i]))
                {
                    g.Clear(Color.Transparent);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.DrawArc(p, w / 2.0f, h / 2.0f, this.Width - w - 1f, this.Height - h - 1f, speedc, this.Sweep);
                }

                speedc += this.Speed;
            }

            this.index = 0;
            this.pic.Image = this.Images[0];
        }
        #endregion

        /// <summary>
        /// 回転処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tim_Tick
        private void tim_Tick(object sender, EventArgs e)
        {
            if (this.index + 1 >= this.Images.Length)
            {
                this.pic.Image = this.Images[0];
                this.index = 0;
            }
            else
            {
                this.pic.Image = this.Images[++index];
            }
        }
        #endregion

        /// <summary>
        /// サイズ変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region LoadingImage_SizeChanged
        private void LoadingImage_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width < 8)
                this.Width = 8;
            if (this.Height < 8)
                this.Height = 8;

            if (this.IsSquare)
                this.Width = this.Height;

            this.SetImages();
        }
        #endregion


    }
}
