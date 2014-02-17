//=======================================================================
//  ClassName : WidgetBase
//  概要      : ウィジェットベース
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Liplis.MainSystem;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Liplis.Widget
{
    public partial class WidgetBase : BaseSystem
    {
        // サーフェースの画像
        private System.Drawing.Bitmap surfaceBitmap;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public WidgetBase():base()
        {
            InitializeComponent();
            init();
            faidStart();
        }

        private void init()
        {
            // フォームの境界線を無くす
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
 
            // フォームの境界線を無くしたことで、メニューも閉じるボタンも表示されないので
            // 右クリック時コンテキストメニューにより終了できるようにする
            ContextMenuStrip cntmenu = new ContextMenuStrip();
            {
                ToolStripMenuItem newcontitem = new ToolStripMenuItem();
                newcontitem.Text = "終了(&Q)";
                newcontitem.Click += delegate
                {
                    // 閉じる
                    this.Close();
                };
                cntmenu.Items.Add(newcontitem);
            }
            this.ContextMenuStrip = cntmenu;  //フォームの右クリック
        }

        /// <summary>
        /// ドラッグ移動
        /// </summary>
        private void Main_MouseDown(object sender, MouseEventArgs e) { mouseDown(e); }
        private void Main_MouseMove(object sender, MouseEventArgs e) { mouseMove(e); }
        private void lblTitle_MouseDown(object sender, MouseEventArgs e) { mouseDown(e); }
        private void lblTitle_MouseMove(object sender, MouseEventArgs e) { mouseMove(e); }








        // Form作成時にCreateParams.ExStyleにWS_EX_LAYEREDを指定するため
        // Form.CreateParamsをオーバーライドする
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                const int WS_EX_LAYERED = 0x00080000;
 
                System.Windows.Forms.CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_LAYERED;
 
                return cp;
            }
        }
 
        // UpdateLayeredWindow関連のAPI定義
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DeleteObject(IntPtr hobject);
 
        public const byte AC_SRC_OVER = 0;
        public const byte AC_SRC_ALPHA = 1;
        public const int ULW_ALPHA = 2;
 
        // UpdateLayeredWindowで使うBLENDFUNCTION構造体の定義
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }
 
        // UpdateLayeredWindowを使うための定義
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int UpdateLayeredWindow(
            IntPtr hwnd,
            IntPtr hdcDst,
            [System.Runtime.InteropServices.In()]
            ref Point pptDst,
            [System.Runtime.InteropServices.In()]
            ref Size psize,
            IntPtr hdcSrc,
            [System.Runtime.InteropServices.In()]
            ref Point pptSrc,
            int crKey,
            [System.Runtime.InteropServices.In()]
            ref BLENDFUNCTION pblend,
            int dwFlags);
 
 
        // レイヤードウィンドウを設定する
        public void SetLayeredWindow(Bitmap srcBitmap)
        {
            // スクリーンのGraphicsと、hdcを取得
            Graphics g_sc = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr hdc_sc = g_sc.GetHdc();
 
            // BITMAPのGraphicsと、hdcを取得
            Graphics g_bmp = Graphics.FromImage(srcBitmap);
            IntPtr hdc_bmp = g_bmp.GetHdc();
 
            // BITMAPのhdcで、サーフェイスのBITMAPを選択する
            // このとき背景を無色透明にしておく
            IntPtr oldhbmp = SelectObject(hdc_bmp, srcBitmap.GetHbitmap(Color.FromArgb(0)));
 
            // BLENDFUNCTION を初期化
            BLENDFUNCTION blend = new BLENDFUNCTION();
            blend.BlendOp = AC_SRC_OVER;
            blend.BlendFlags = 0;
            blend.SourceConstantAlpha = 255;
            blend.AlphaFormat = AC_SRC_ALPHA;
 
            // ウィンドウ位置の設定
            Point pos = new Point(this.Left, this.Top);
 
            // サーフェースサイズの設定
            Size surfaceSize = new Size(this.Width, this.Height);
 
            // サーフェース位置の設定
            Point surfacePos = new Point(0, 0);
 
            // レイヤードウィンドウの設定
            UpdateLayeredWindow(
                this.Handle, hdc_sc, ref pos, ref surfaceSize,
                hdc_bmp, ref surfacePos, 0, ref blend, ULW_ALPHA);
 
            // 後始末
            DeleteObject(SelectObject(hdc_bmp, oldhbmp));
            g_sc.ReleaseHdc(hdc_sc);
            g_sc.Dispose();
            g_bmp.ReleaseHdc(hdc_bmp);
            g_bmp.Dispose();
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {
            // レイヤードウィンドウの設定
            {
                // サーフェイスのBITMAPを生成
                surfaceBitmap = new Bitmap(this.Width, this.Height);
 
                // BITMAPに描画するためのGraphicsを取得
                Graphics g = Graphics.FromImage(surfaceBitmap);
 
                // グラデーション描画（半透明）
                {
                    // 透明から紫色へのグラデーションを描画
                    System.Drawing.Drawing2D.LinearGradientBrush lgb =
                        new System.Drawing.Drawing2D.LinearGradientBrush(
                            this.ClientRectangle, Color.Transparent, Color.Violet,
                            System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
                    lgb.GammaCorrection = true; //ガンマ補正
                    g.FillRectangle(lgb, this.ClientRectangle);
                    lgb.Dispose();
                }
                // グラデーションの上に文字を重ねる（不透明）
                {
                    // アンチエリアス
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
 
                    // レンダリング品質
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
 
                    // 描画する文字
                    string text = "Aaあぁアァ亜宇";
 
                    // メイリオでの描画
                    {
                        Font font = new Font("Meiryo UI", 24, FontStyle.Bold);
                        Point pos = new Point(20, 20);
 
                        // GraphicsPathの生成
                        System.Drawing.Drawing2D.GraphicsPath gp =
                            new System.Drawing.Drawing2D.GraphicsPath();
 
                        // テキストをPathに変換
                        gp.AddString(text, font.FontFamily, (int)font.Style, font.SizeInPoints,
                            pos, StringFormat.GenericDefault);
 
                        // パスを移動させる
                        {
                            System.Drawing.Drawing2D.Matrix translateMatrix =
                                new System.Drawing.Drawing2D.Matrix();
                            translateMatrix.Translate(
                                pos.X - gp.GetBounds().X,
                                pos.Y - gp.GetBounds().Y);
                            gp.Transform(translateMatrix);
                            translateMatrix.Dispose();
                        }
 
                        // 文字の縁を描画
                        g.DrawPath(new Pen(Brushes.Blue, 3.0f), gp);
 
                        // 文字を塗る
                        g.FillPath(Brushes.White, gp);
 
                        // 終了
                        gp.Dispose();
                    }
                }
                // レイヤードウィンドウ指定
                SetLayeredWindow(surfaceBitmap);
            }
        }
    }
}
