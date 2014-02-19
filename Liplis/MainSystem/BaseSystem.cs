//=======================================================================
//  ClassName : BaseSystem
//  概要      : 
//
//  Liplis2.0
//  Copyright(c) 2010-2011 sachin.Sachin
//=======================================================================

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Liplis.Common;
using System.Reflection;

namespace Liplis.MainSystem
{
    /// <summary>
    /// BaseSystem
    /// ウインドウの基底クラス
    /// このシステムでウインドウを作成する場合、このクラスを継承して作成する。
    /// </summary>
    public partial class BaseSystem : Form
    {
        ///=============================
        ///マウスポインタ座標
        protected Point mousePoint;

        ///=============================
        /// 透過色
        protected Color TRANS_COLOR = Color.FromArgb(255,254, 240, 254);

        ///============================
        ///各種インターバル
        protected int autoEndInterval = LiplisDefine.autoEndInterval;
        protected int faidStartInterval = LiplisDefine.faidStartInterval;
        protected int faidEndInterval = LiplisDefine.faidEndInterval;

        ///============================
        /// フェードフラグ
        protected bool faidEndFlg;
        protected bool faidStatrFlg;

        ///============================
        /// コントロールキー押下チェック
        public bool ctrlKeyFlg { get; set; }
        public int ctrlCheckFlg { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region BaseSystem
        public BaseSystem()
        {
            this.dblbuffer();                               //ダブルバッファリング
            this.Opacity = 0;                               //デフォルトではオパシティ0
            this.StartPosition = FormStartPosition.Manual;  //開始位置をマニュアルで設定
            faidEndFlg = false;
            faidStatrFlg = false;
        }
        #endregion

        /// <summary>
        /// ダブルバッファリング設定
        /// </summary>
        #region dblbuffer
        protected void dblbuffer()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        #endregion

        /// <summary>
        /// ベースシステムの初期化
        /// </summary>
        #region initBaseSystem
        protected void initBaseSystem()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Opaque, true);
        }
        #endregion

        /// <summary>
        /// フェードスタート
        /// </summary>
        #region faidStart
        public virtual void faidStart()
        {
            try
            {
                //フェードスタート優先
                faidStatrFlg = true;

                //フェードエンドを待つ
                while (faidEndFlg)
                {
                    //既にオパシティがある場合は一旦下げておく
                    if (this.Opacity > 0)
                    {
                        for (int i = faidEndInterval; 0 <= i; i--)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                            this.Opacity = (double)i / faidEndInterval;
                            this.Refresh();
                        }
                    }
                }

                for (int i = 0; faidStartInterval >= i; i++)
                {
                    Thread.Sleep(1);
                    Application.DoEvents();
                    this.Opacity = (double)i / faidStartInterval;
                    this.Refresh();
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
            finally
            {
                faidStatrFlg = false;
            }
        }
        #endregion

        /// <summary>
        /// フェードイン
        /// </summary>
        #region faidIn
        public virtual void faidIn()
        {
            this.Visible = true;

            for (int i = 0; i <= 30; i++)
            {
                this.Opacity = (double)i / 30;
            }
        }
        public virtual void faidIn(double opc)
        {
            this.Visible = true;

            for (int i = 0; i <= 30 * opc; i++)
            {
                this.Opacity = (double)i / 30;
            }
        }
        #endregion

        /// <summary>
        /// フェードアウト
        /// </summary>
        #region faidOut
        public virtual void faidOut()
        {
            try
            {
                for (int i = 10; i > 0; i--)
                {
                    Thread.Sleep(1);
                    Application.DoEvents();
                    this.Opacity = (double)i / 10;
                }
                this.Refresh();
                Visible = false;
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// 背景を設定する
        /// </summary>
        /// <param name="bmpPath">ビットマップパス、もしくはビットマップインスタンス</param>
        #region setBgi
        public void setBgi(string bmpPath)
        {
            setBgi(new Bitmap(bmpPath));
        }
        public void setBgi(Bitmap bmp)
        {
            try
            {
                bmp.MakeTransparent(TRANS_COLOR);
                this.Size = bmp.Size;
                this.BackgroundImage = bmp;
                this.BackColor = TRANS_COLOR;
                this.TransparencyKey = TRANS_COLOR;
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// ウインドウ設定の初期化(透過設定等を行う)
        /// </summary>
        /// <param name="bmp">初期化用画像</param>
        #region setWindowProperty
        protected void setWindowProperty(Bitmap bmp)
        {
            //透明背景色の許可
            this.ShowInTaskbar = false;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.FormBorderStyle = FormBorderStyle.None;

            bmp.MakeTransparent(TRANS_COLOR);
            this.Size = bmp.Size;
            this.BackgroundImage = bmp;
            this.BackColor = TRANS_COLOR;
            this.TransparencyKey = TRANS_COLOR;
        }
        #endregion

        /// <summary>
        /// ロケーションを設定する
        /// </summary>
        /// <param name="locationX">locationX</param>
        /// <param name="locationY">locationY</param>
        #region setLocation
        public virtual void setLocation(int locationX, int locationY)
        {
            try
            {
                this.Location = new Point(locationX, locationY);
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// サイズを設定する
        /// </summary>
        /// <param name="widht">widht</param>
        /// <param name="height">height</param>
        #region (
        public virtual void setSize(int width, int height)
        {
            try
            {
                //this.Height = height;
                //this.Width = width;

                this.Size = new Size(width, height);
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 汎用マウスドラッグ移動
        /// マウスダウン編
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        #region mouseDown
        protected virtual void mouseDown(MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    if (ctrlCheckFlg == 1)
                    {
                        if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                        {
                            mousePoint = new Point(e.X, e.Y);
                        }
                    }
                    else
                    {
                        //位置を記憶する
                        mousePoint = new Point(e.X, e.Y);
                    }
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        internal void mouseDownOv(MouseEventArgs e)
        {
            mouseDown(e);
        }
        #endregion

        /// <summary>
        /// 汎用マウスドラッグ移動
        /// マウスムーブ編
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        #region mouseMove
        protected virtual void mouseMove(MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    //コントロールロック
                    if (ctrlCheckFlg == 1)
                    {
                        if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                        {
                            this.Left += e.X - mousePoint.X;
                            this.Top += e.Y - mousePoint.Y;
                        }
                    }
                    else
                    {
                        this.Left += e.X - mousePoint.X;
                        this.Top += e.Y - mousePoint.Y;
                    }
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        internal void mouseMoveOv(MouseEventArgs e)
        {
            mouseMove(e);
        }
        #endregion
        /// <summary>
        /// 汎用マウスドラッグ移動
        /// マウスムーブ編
        /// </summary>
        /// <param name="e">MouseEventArgs</param>
        #region mouseMovePitatto
        protected virtual void mouseMoveWidget(MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    //コントロールロック
                    if (ctrlCheckFlg == 1 && !ctrlKeyFlg){return;}

                    // 吸着するサイズ
                    Size gap = new Size(16, 16);

                    // 移動先のフォーム位置
                    Rectangle newPosition = new Rectangle(
                        this.Left + e.X - mousePoint.X,
                        this.Top + e.Y - mousePoint.Y,
                        this.Width,
                        this.Height);
                    // 判定用のRECT
                    Rectangle newRect = new Rectangle();

                    // 作業領域の取得（この作業領域の内側に吸着する）
                    Size area = new Size(
                        System.Windows.Forms.Screen.GetWorkingArea(this).Width,
                        System.Windows.Forms.Screen.GetWorkingArea(this).Height);

                    // 画面端の判定用（画面の端の位置に、吸着するサイズ分のRECTを定義する）
                    Rectangle rectLeft = new Rectangle(
                                                0,
                                                0,
                                                gap.Width,
                                                area.Height);
                    Rectangle rectTop = new Rectangle(
                                                0,
                                                0,
                                                area.Width,
                                                gap.Height);
                    Rectangle rectRight = new Rectangle(
                                                area.Width - gap.Width,
                                                0,
                                                gap.Width,
                                                area.Height);
                    Rectangle rectBottom = new Rectangle(
                                                0,
                                                area.Height - gap.Height,
                                                area.Width,
                                                gap.Height);
                    // 衝突判定
                    // 判定用のRECTを自分のウィンドウの隅に重ねるように移動し、
                    // 画面端の判定用のRECTと衝突しているかチェックする。
                    // 衝突していた場合は、吸着させるように移動する

                    // 左端衝突判定
                    {
                        newRect = newPosition;
                        newRect.Width = gap.Width;

                        if (newRect.IntersectsWith(rectLeft))
                        {
                            // 左端に吸着させる
                            newPosition.X = 0;
                        }
                    }
                    // 右端衝突判定
                    {
                        newRect = newPosition;
                        newRect.X = newPosition.Right - gap.Width;  // ウィンドウの右隅
                        newRect.Width = gap.Width;

                        if (newRect.IntersectsWith(rectRight))
                        {
                            // 右端に吸着させる
                            newPosition.X = area.Width - this.Width;
                        }
                    }
                    // 上端衝突判定
                    {
                        newRect = newPosition;
                        newRect.Height = gap.Height;

                        if (newRect.IntersectsWith(rectTop))
                        {
                            // 上端に吸着させる
                            newPosition.Y = 0;
                        }
                    }
                    // 下端衝突判定
                    {
                        newRect = newPosition;
                        newRect.Y = newPosition.Bottom - gap.Height; // ウィンドウの下端
                        newRect.Height = gap.Height;

                        if (newRect.IntersectsWith(rectBottom))
                        {
                            // 下端に吸着させる
                            newPosition.Y = area.Height - this.Height;
                        }
                    }

                    // 実際に移動させる
                    this.Left = newPosition.Left;
                    this.Top = newPosition.Top;

                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        internal virtual void mouseMoveWidgetOv(MouseEventArgs e)
        {
            mouseMoveWidget(e);
        }
        #endregion

        /// <summary>
        /// キー操作
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyEventArgs</param>
        #region BaseSystem_KeyDown
        protected void BaseSystem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                ctrlKeyFlg = true;
            }
        }
        #endregion

        /// <summary>
        /// キーアップ
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyEventArgs</param>
        #region BaseSystem_KeyUp
        protected void BaseSystem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                ctrlKeyFlg = false;
            }
        }
        #endregion

        /// <summary>
        /// 座標チェック
        /// 現在のフォームが有効領域内にあるかチェックする
        /// </summary>
        /// <param name="locationX">X</param>
        /// <param name="locationY">Y</param>
        /// <param name="defaultX">デフォルト座標X</param>
        /// <param name="defaultY">デフォルト座標Y</param>
        #region checkPos
        protected void checkPos(ref int locationX, ref int locationY, int defaultX, int defaultY)
        {
            int h, w;
            h = Screen.PrimaryScreen.Bounds.Height;
            w = Screen.PrimaryScreen.Bounds.Width;

            if (locationX < 0 || locationX > w) { locationX = defaultX; }
            if (locationY < 0 || locationY > h) { locationY = defaultY; }
        }

        //座標チェッカー
        protected bool checkPos(int locationX, int locationY)
        {
            int h, w;
            h = Screen.PrimaryScreen.Bounds.Height;
            w = Screen.PrimaryScreen.Bounds.Width;

            if (locationX < 0 || locationX > w) { return false; }
            if (locationY < 0 || locationY > h) { return false; }
            return true;
        }
        #endregion

        /// <summary>
        /// 色名、ARGB、RGBから色を引く
        /// </summary>
        /// <param name="colorStr">色指定(ARGB or RGB or 色名)</param>
        /// <returns>結果Colorクラス</returns>
        #region getColor
        protected Color getColor(string colorStr, Color DefaultColor)
        {
            string[] rgb = colorStr.Split(',');
            try
            {
                if (rgb.Length == 1)
                {
                    return Color.FromName(colorStr);
                }
                else if (rgb.Length == 3)
                {
                    try
                    {
                        return Color.FromArgb(255, int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
                    }
                    catch
                    {
                        return DefaultColor;
                    }
                }
                else if (rgb.Length == 4)
                {
                    try
                    {
                        return Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]), int.Parse(rgb[3]));
                    }
                    catch
                    {
                        return DefaultColor;
                    }
                }
                else
                {
                    return DefaultColor;
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return DefaultColor;
            }
        }
        #endregion

        /// <summary>
        /// 領域チェック
        /// </summary>
        /// <param name="r">領域レクト</param>
        /// <returns>範囲内/外</returns>
        #region territoryCheck
        protected bool territoryCheck(Rectangle r)
        {
            mousePoint = new Point(MousePosition.X, MousePosition.Y);

            if (r.Left < mousePoint.X && r.Left + r.Width > mousePoint.X)
            {
                if (r.Top < mousePoint.Y && r.Top + r.Height > mousePoint.Y)
                {
                    return true;
                }
            }

            return false;
        }
        protected bool territoryCheck()
        {
            return territoryCheck(new Rectangle(this.Left, this.Top, this.Width, this.Height));
        }
        #endregion

        /// <summary>
        /// ウインドウ状態チェック
        /// chekcWindowState
        /// </summary>
        /// <returns>ウインドウ状態</returns>
        #region chekcWindowState
        protected int chekcWindowState()
        {
            //自分自身のフォームの状態を調べる
            return (int)this.WindowState;
        }
        #endregion

        /// <summary>
        /// ctrlCheck
        /// コントロールチェック
        /// </summary>
        /// <returns></returns>
        #region ctrlCheck
        protected internal bool ctrlCheck()
        {
            return ctrlCheckFlg == 1 && System.Windows.Forms.Control.ModifierKeys != Keys.Control;
        }
        #endregion

        protected void restert()
        {
            this.Close();
            System.Diagnostics.Process.Start(Application.ExecutablePath);
        }

    }
}
