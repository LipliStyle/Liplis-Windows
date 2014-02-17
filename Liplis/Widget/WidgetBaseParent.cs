//=======================================================================
//  ClassName : frmParent
//  概要      : こちらに透過するオブジェクトを置く
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using Liplis.Activity;
using Liplis.Common;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Ser;

namespace Liplis.Widget
{
    public partial class WidgetBaseParent : BaseSystem
    {
        ///=============================
        /// 設定
        protected ObjWidgetSetting o;
        protected WidgetBaseSetting s;
        protected ActivityWidget a;
        internal DataGridViewRow d { get; set; }

        ///=============================
        /// 設定
        protected WidgetBaseBase f2;

        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WidGetParentBase
        public WidgetBaseParent(ObjWidgetSetting o, ActivityWidget a, WidgetBaseSetting s)
        {
            //InitializeComponent();
            this.o                = o;
            this.a                = a;
            this.s                = s;
            this.initBaseForm();        //初期化
            this.loadBaseForm();        //子クラスでのロード
        }
        public WidgetBaseParent()
        {

        }
        #endregion

        /// <summary>
        /// initBaseForm
        /// ベースフォームをの初期化を行う
        /// </summary>
        #region initBaseForm
        protected virtual void initBaseForm()
        {
            this.ctrlCheckFlg = LpsLiplisUtil.boolToBit(o.ctlRock);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(s.rect.X, s.rect.Y);
            this.Opacity = o.opacity;                                                               // 半透明指定
            this.FormClosing += new FormClosingEventHandler(this.WidgetBaseParent_FormClosing);     //クロージングイベント
            this.DoubleClick += delegate { this.Close(); };                                         //GBのダブルクリック
            this.ShowInTaskbar = false;
        }
        #endregion


        /// <summary>
        /// ベースフォームをインスタンスかするメソッドを定義する
        /// </summary>
        protected virtual void loadBaseForm()
        {
            //必ず子クラスでオーバーラーイド
            MessageBox.Show("子クラスでオーバーライド！");
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

        ///====================================================================
        ///
        ///                             onLoad
        ///                         
        ///====================================================================

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region frmTranse_Load
        protected void frmTranse_Load(object sender, EventArgs e)
        {
            parentFormInit();
        }
        #endregion

        /// <summary>
        /// 親フォームの初期化
        /// </summary>
        #region parentFormInit
        protected void parentFormInit()
        {
            f2.ShowInTaskbar   = false; // タスクバーに表示させない
            f2.ControlBox      = false; // コントロールボックスを表示しない
            f2.FormBorderStyle = FormBorderStyle.None; // 枠線スタイル＝枠線なし
            f2.Size            = this.ClientSize; // Form2のサイズはForm1のクライアント領域のサイズ
            f2.StartPosition   = FormStartPosition.Manual; // Form2の初期位置はLocationで指定
            f2.Location        = this.PointToScreen(this.ClientRectangle.Location); // Form2の位置をForm1のクライアント領域にセット
            f2.f1 = this;

            // 背景を塗る（this.BackColorだとコントロールの背景も変わってしまうのでダメ）
            {
                // フォームと同じ大きさのBITMAPを作成
                Bitmap image = new Bitmap(f2.Width, f2.Height);

                // 透過色で塗りつぶす
                Graphics g = Graphics.FromImage(image);
                g.Clear(Color.Green); // 何色でも良いが他のコントロールの色と違うものにする

                // 背景に設定
                f2.BackgroundImage = image;
            }
            // 透過色の設定
            f2.TransparencyKey = Color.Green;

            // 半透明フォームをオーナーにする
            this.AddOwnedForm(f2);

            // 連動イベント
            this.FormClosing += delegate { try { f2.Close(); } catch { } }; // Closeに追従
            this.Move += delegate { f2.Location = this.PointToScreen(this.ClientRectangle.Location); }; // Moveに追従
            f2.Move += delegate { this.Location = f2.PointToScreen(f2.ClientRectangle.Location); }; // Moveに追従

            f2.Show();
        }
        #endregion



        /// <summary>
        /// initBackGroundLabel
        /// 背景の初期化
        /// </summary>
        #region initBackGroundLabel
        protected virtual void initBackGroundLabel()
        {
            // 切り抜く大きさのBitmapを準備
            Bitmap bmp = new Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // これを使って Graphicsを生成
            using (Graphics g = Graphics.FromImage(bmp))
            {
                //たてに白から黒へのグラデーションのブラシを作成
                //g.VisibleClipBoundsは表示クリッピング領域に外接する四角形
                using (LinearGradientBrush gb = new LinearGradientBrush(
                        g.VisibleClipBounds,
                        o.widgetColorUp,
                        o.widgetColorUnder,
                        LinearGradientMode.Vertical))
                {
                    //四角を描く
                    g.FillRectangle(gb, g.VisibleClipBounds);
                }
            }

            this.BackgroundImage = bmp;
        }
        #endregion

        ///// <summary>
        ///// initBarLabel
        ///// ラベルの初期化
        ///// </summary>
        #region initBarLabel
        //protected void initBarLabel(Label lbl)
        //{
        //    // 切り抜く大きさのBitmapを準備
        //    try
        //    {
        //        Bitmap bmp = new Bitmap(lbl.Width, lbl.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

        //        // これを使って Graphicsを生成
        //        using (Graphics g = Graphics.FromImage(bmp))
        //        {
        //            //たてに白から黒へのグラデーションのブラシを作成
        //            //g.VisibleClipBoundsは表示クリッピング領域に外接する四角形
        //            using (LinearGradientBrush gb = new LinearGradientBrush(
        //                    g.VisibleClipBounds,
        //                    o.widgetBarColorUp,
        //                    o.widgetBarColorUnder,
        //                    LinearGradientMode.Vertical))
        //            {
        //                //四角を描く
        //                g.FillRectangle(gb, g.VisibleClipBounds);
        //            }
        //        }

        //        lbl.BackgroundImage = bmp;
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }

        //}
        #endregion




        ///====================================================================
        ///
        ///                             onRecive
        ///                         
        ///====================================================================


        /// <summary>
        /// WndProcのオーバーライド
        /// </summary>
        #region WndProc
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
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnClose_Click
        private void WidgetBaseParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                a.owml.deleteSetting(s);
                a.widgetList.Remove(this);
                a.dgvWidgetManager.Rows.Remove(d);
                this.Close();
            }
            catch
            {

            }
            finally
            {
                
            }
        }
        #endregion



        ///====================================================================
        ///
        ///                          ムーブイベント
        ///                         
        ///====================================================================

        /// <summary>
        /// mouseMovePitatto
        /// </summary>
        #region マウスムーブイベント
        protected void Main_MouseDown(object sender, MouseEventArgs e) { mouseDown(e); }
        protected void Main_MouseMove(object sender, MouseEventArgs e) { mouseMove(e); }
        protected void lblTitle_MouseDown(object sender, MouseEventArgs e) { mouseDown(e); }
        protected void lblTitle_MouseMove(object sender, MouseEventArgs e) { mouseMoveWidget(e); }
        #endregion

        /// <summary>
        /// mouseMovePitatto
        /// </summary>
        /// <param name="e"></param>
        #region mouseMovePitatto
        protected override void mouseMoveWidget(MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    //コントロールロック
                    if (o.ctlRock && System.Windows.Forms.Control.ModifierKeys != Keys.Control) { return; }

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

                    ////他ウインドウとの衝突を計算
                    //foreach (WidgetBaseSetting m in a.owml.widgetList)
                    //{
                    //    if(m.Equals(this.s))
                    //    {
                    //        continue;
                    //    }

                    //    // 画面端の判定用（画面の端の位置に、吸着するサイズ分のRECTを定義する）
                    //    Rectangle rectLeftTop = new Rectangle(
                    //                                m.rect.X - gap.Width,
                    //                                m.rect.Y,
                    //                                gap.Width,
                    //                                m.rect.Height / 2);
                    //    Rectangle rectLeftBottom = new Rectangle(
                    //                                m.rect.X - gap.Width,
                    //                                m.rect.Y + m.rect.Height / 2,
                    //                                gap.Width,
                    //                                m.rect.Height / 2);
                    //    Rectangle rectTopLeft = new Rectangle(
                    //                                m.rect.X,
                    //                                m.rect.Y - gap.Height,
                    //                                m.rect.Width/2,
                    //                                gap.Height);
                    //    Rectangle rectTopRight = new Rectangle(
                    //                                m.rect.X + m.rect.Width/2,
                    //                                m.rect.Y - gap.Height,
                    //                                m.rect.Width / 2,
                    //                                gap.Height);
                    //    Rectangle rectRightTop = new Rectangle(
                    //                                m.rect.X + m.rect.Width,
                    //                                m.rect.Y,
                    //                                gap.Width,
                    //                                m.rect.Height / 2);
                    //    Rectangle rectRightBottom = new Rectangle(
                    //                                area.Width - gap.Width,
                    //                                m.rect.Y + m.rect.Height / 2,
                    //                                gap.Width,
                    //                                m.rect.Height / 2);
                    //    Rectangle rectBottomLeft = new Rectangle(
                    //                                m.rect.X,
                    //                                m.rect.Y + m.rect.Height,
                    //                                m.rect.Width / 2,
                    //                                gap.Height);
                    //    Rectangle rectBottomRight = new Rectangle(
                    //                                m.rect.X + m.rect.Width/2,
                    //                                m.rect.Y + m.rect.Height,
                    //                                m.rect.Width / 2,
                    //                                gap.Height);
                    //    // 衝突判定
                    //    // 判定用のRECTを自分のウィンドウの隅に重ねるように移動し、
                    //    // 画面端の判定用のRECTと衝突しているかチェックする。
                    //    // 衝突していた場合は、吸着させるように移動する

                    //    // 左端衝突判定
                    //    {
                    //        newRect = newPosition;
                    //        newRect.X = newPosition.Right - gap.Width;  // ウィンドウの右隅
                    //        newRect.Width = gap.Width;
                    //        newRect.Height = newRect.Height / 5; 

                    //        if (newRect.IntersectsWith(rectLeftTop))
                    //        {
                    //            // 左端に吸着させる
                    //            newPosition.X = m.rect.X - this.Width;
                    //            newPosition.Y = m.rect.Y;
                    //        }
                    //    }

                    //    // 右端衝突判定
                    //    {
                    //        newRect = newPosition;
                    //        newRect.Width = gap.Width;
                    //        newRect.Height = newRect.Height / 5; 

                    //        if (newRect.IntersectsWith(rectRightTop))
                    //        {
                    //            // 右端に吸着させる
                    //            newPosition.X = m.rect.X + m.rect.Width;
                    //            newPosition.Y = m.rect.Y;
                    //        }
                    //    }
                    //    // 上端衝突判定
                    //    {
                    //        newRect = newPosition;
                    //        newRect.Y = newPosition.Bottom - gap.Height; // ウィンドウの下端
                    //        newRect.Height = gap.Height;
                    //        newRect.Width = newRect.Width;

                    //        if (newRect.IntersectsWith(rectTopLeft))
                    //        {
                    //            // 下端に吸着させる
                    //            newPosition.Y = m.rect.Y - this.Height;
                    //            newPosition.X = m.rect.X;
                    //        }
                    //    }
                    //    // 下端衝突判定
                    //    {
                    //        newRect = newPosition;
                    //        newRect.Height = gap.Height;
                    //        newRect.Width = newRect.Width;

                    //        if (newRect.IntersectsWith(rectBottomLeft))
                    //        {
                    //            // 上端に吸着させる
                    //            newPosition.Y = m.rect.Y + m.rect.Height;
                    //            newPosition.X = m.rect.X;
                    //        }
                    //    }
                    //}

                    // 実際に移動させる
                    this.Left     = newPosition.Left;
                    this.Top      = newPosition.Top;
                    this.s.rect = newPosition;



                    //データグリッドを更新する
                    this.d.Cells[3].Value = this.Left.ToString();
                    this.d.Cells[4].Value = this.Top.ToString();

                    //自分を探して座標をセット
                    s.rect = new Rectangle(newPosition.X, newPosition.Y, this.Width, this.Height);
                    SerialWidgetList.saveObject(a.owml);                    
                }
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
        protected override void mouseDown(MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    if (o.ctlRock)
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
        #endregion

        /// <summary>
        /// setSaihaimen
        /// 最背面に配置
        /// 現在使用していない
        /// </summary>
        #region setSaihaimen
        protected void setSaihaimen()
        {
            // Program Manager のハンドルを取得する
            IntPtr hProgramManagerHandle = LpsWindowsApi.FindWindow(null, "Program Manager");

            // 正しく取得できた場合は、Program Manager を親ウィンドウに設定する
            if (!hProgramManagerHandle.Equals(0))
            {
                LpsWindowsApi.SetParent(this.Handle, hProgramManagerHandle);
            }
        }

        #endregion


        /// <summary>
        /// setOpacity
        /// オパシティをセットする
        /// </summary>
        #region setOpacity
        public void setOpacity()
        {
            this.Opacity = o.opacity;
        }
        #endregion
    }
}
