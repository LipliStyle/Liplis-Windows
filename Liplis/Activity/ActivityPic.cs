//=======================================================================
//  ClassName : ActivityPic
//  概要      : ピクチャーアクティビティ
//
//  Liplis2.3
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Liplis.Common;

namespace Liplis.Activity
{
    public partial class ActivityPic : Liplis.MainSystem.BaseSystem
    {
        ///============================
        /// プロパティ
        public string url { get; set; }

        ///=====================================
        /// フラグ
        private bool flgEnd = false;

        ///============================
        /// デリゲート
        #region デリゲート
        private static LpsDelegate.dlgI7ToVoid reqSetLocation;
        #endregion

        ///====================================================================
        ///
        ///                          onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ActivityPic
        public ActivityPic()
        {
            InitializeComponent();

            //デリゲートの初期化
            initDelegate();

            this.Opacity = 1;
        }
        #endregion

        /// <summary>
        /// initDelegate
        /// delegateの初期化
        /// </summary>
        #region initDelegate
        private void initDelegate()
        {
            //セットロケーションデリゲート
            reqSetLocation = new LpsDelegate.dlgI7ToVoid(dlgSetLocation);
        }
        #endregion

        ///====================================================================
        ///
        ///                          onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// onDelete
        /// </summary>
        #region Dispose
        public void dispose()
        {
            flgEnd = true;
            if (pic != null)
            {
                if (pic.Image != null)
                {
                    this.pic.Image.Dispose();
                    this.pic.Image = null;
                }
            }
            this.Close();
        }
        #endregion

        /// <summary>
        /// ActivityPic_FormClosing
        /// フォームクロージングキャンセラー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityPic_FormClosing
        private void ActivityPic_FormClosing(object sender, FormClosingEventArgs e)
        {
            //エンドフラグが有効でなければ、ハイドさせる
            if (!flgEnd)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                       ウインドウ制御
        ///                         
        ///====================================================================

        /// <summary>
        /// setLocation
        /// 座標のセット
        /// </summary>
        #region setLocation
        public void setLocation(int liplisX, int liplisY, int liplisWidth, int liplisHieght, int talkWidth, int talkHeight, int direction)
        {
            reqSetLocation(liplisX, liplisY, liplisWidth, liplisHieght, talkWidth, talkHeight, direction);
        }
        #endregion

        /// <summary>
        /// 通常化
        /// </summary>
        #region onNormalize
        public void onNormalize()
        {
            this.Show();
        }
        #endregion

        /// <summary>
        /// 最小化
        /// </summary>
        #region onMinimize
        public void onMinimize()
        {
            this.Hide();
        }
        #endregion

        /// <summary>
        /// イメージをセットする
        /// </summary>
        /// <param name="path"></param>
        #region setImage
        public void setImage(string path, string url)
        {
            if (!path.Equals(""))
            {
                if (LpsPathControllerCus.checkFileExist(path))
                {
                    //以前設定されていた画像をセット
                    if (this.pic.Image != null)
                    {
                        this.pic.Image.Dispose();
                        this.pic.Image = null;
                    }

                    //ビットマップのセット
                    using(Bitmap b = new Bitmap(path))
                    {
                        this.pic.Image = new Bitmap(b, new Size(130, 100));
                    }
                   
                    //セット
                    this.url = url;
                    this.Show();
                    this.Refresh();
                }
                else
                {
                    this.Hide();
                }
            }
            else
            {
                this.Hide();
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                          onRecive
        ///                         
        ///====================================================================


        /// <summary>
        /// ActivityPic_Load
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityPic_Load
        private void ActivityPic_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
        }
        #endregion
        
        /// <summary>
        /// pic_Click
        /// ピクチャークリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region pic_Click
        private void pic_Click(object sender, EventArgs e)
        {

        }
        #endregion

        ///====================================================================
        ///
        ///                       デリゲート
        ///                         
        ///====================================================================

        /// <summary>
        /// dlgSetLocation
        /// </summary>
        #region dlgSetLocation
        private void dlgSetLocation(int liplisX, int liplisY, int liplisWidth, int liplisHieght, int talkWidth, int talkHeigh, int direction)
        {
            int ftLocX = this.Left;
            int ftLocY = this.Top;
            int targetX = liplisX - this.Width;
            int targetY = liplisY;
            int moveValX = 0;
            int moveValY = 0;
            int cnt = 1;

            //シフト
            shiftPos(ref targetX, ref targetY, liplisX, liplisY, liplisWidth, liplisHieght, talkWidth,  talkHeigh, direction);

            //移動量の算出
            moveValX = targetX - this.Left;
            moveValY = targetY - this.Top;


            //加速度的移動
            while (targetX != this.Left)
            {
                System.Threading.Thread.Sleep(5);
                System.Windows.Forms.Application.DoEvents();

                if (cnt > 100)
                {
                    this.Left = targetX;
                    this.Top = targetY;
                    break;
                }

                this.Left = ftLocX + (moveValX * cnt / 100);
                this.Top = ftLocY + (moveValY * cnt / 100);
                cnt = cnt * 2;
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                          その他メソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// 表示座標をチェック、シフトする
        /// </summary>
        #region shiftPos
        private void shiftPos(ref int locationX, ref int locationY, int liplisX, int liplisY, int liplisWidth, int liplisHieght, int talkWidth, int talkHeigh, int direction)
       {
            int cnt = 0;
            int talkMoveLeft = locationX;
            int talkMoveTop = locationY;

            try
            {
                while (!checkPos(talkMoveLeft, talkMoveTop))
                {
                    switch (cnt)
                    {
                        case 0:
                            talkMoveLeft = liplisX - talkWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth/2 - this.Width/2;
                            locationY = talkMoveTop + talkHeigh + 20;

                            break;
                        case 1:
                            talkMoveLeft = liplisX + liplisWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth/2 - this.Width/2;
                            locationY = talkMoveTop + talkHeigh + 20;
                            break;
                        case 2:
                            talkMoveLeft = liplisX + liplisWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth/2 - this.Width/2;
                            locationY = talkMoveTop + talkHeigh + 20;
                            break;
                        default:
                            talkMoveLeft = liplisX + talkWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth/2 - this.Width/2;
                            locationY = talkMoveTop + talkHeigh + 20;
                            return;
                    }
                    cnt++;
                    if (cnt > 3)
                    {
                        if (direction == 0)
                        {
                            talkMoveLeft = liplisX - talkWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth/2 - this.Width/2;
                            locationY = talkMoveTop + talkHeigh + 20;
                        }
                        else
                        {
                            talkMoveLeft = liplisX + liplisWidth;
                            talkMoveTop = liplisY;

                            locationX = talkMoveLeft + talkWidth/2 - this.Width/2;
                            locationY = talkMoveTop + talkHeigh + 20;
                        }
                        return;
                    }
                }
                if (cnt == 0)
                {
                    talkMoveLeft = liplisX - talkWidth;
                    talkMoveTop = liplisY;

                    locationX = talkMoveLeft + talkWidth / 2 - this.Width / 2;
                    locationY = talkMoveTop + talkHeigh + 20;
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }

        }
        #endregion
    }
}
