//=======================================================================
//  ClassName : CharPanel
//  概要      : キャラクターパネル
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Liplis.Common;
using System.Drawing;
using Liplis.Fct;
using Liplis.Msg;

namespace Liplis.Cmp.Form
{
    public class CusCtlRssPanel : Panel
    {
        ///=====================================
        /// 構成要素
        private Label lblText;
        private Label lnkLbl;
        private PictureBox pic;

        ///=====================================
        /// URL
        private string title       = "";
        private string url         = "";
        private string description = "";
        private string jpgPath     = "";
        private Bitmap defaultImage;
        private bool select        = false;

        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region CharPanel
        public CusCtlRssPanel(string title, string url,  string description, string jpgPath, Bitmap defaultImage)
        {
            this.title = url;
            this.url          = url;
            this.description  = description;
            this.jpgPath      = jpgPath;
            this.defaultImage = defaultImage;
            initDataPanel();
        }
        #endregion


        /// <summary>
        /// キャラパネルの初期化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="discription"></param>
        /// <param name="cat"></param>
        /// <param name="jpgPath"></param>
        /// <param name="enter"></param>
        /// <param name="cmst"></param>
        /// <param name="cms"></param>
        #region initDataPanel
        private void initDataPanel()
        {
            //初期化
            this.lblText = new System.Windows.Forms.Label();
            this.lnkLbl = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();

            // 
            // panel
            // 
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lnkLbl);
            this.Controls.Add(this.pic);
            this.Location = new System.Drawing.Point(3, 3);
            this.Name = "panel";
            this.Size = new System.Drawing.Size(590, 125);
            this.TabIndex = 0;
            this.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.MouseLeave += new System.EventHandler(this.mouseLeave);

            if (!select)
            {
                this.BackColor = Color.FromArgb(50, 255, 255, 255);
            }
            else
            {
                this.BackColor = Color.FromArgb(100, 255, 228, 225);
            }


            // 
            // lnkLbl
            // 
            this.lnkLbl.Location = new System.Drawing.Point(108, 17);
            this.lnkLbl.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lnkLbl.Name = "lnkLbl";
            this.lnkLbl.Size = new System.Drawing.Size(439, 16);
            this.lnkLbl.TabIndex = 1;
            this.lnkLbl.Text = title;
            this.lnkLbl.DoubleClick += new System.EventHandler(this.doubleClick);
            this.lnkLbl.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.lnkLbl.MouseLeave += new System.EventHandler(this.mouseLeave);

            // 
            // lblText
            // 
            this.lblText.Location = new System.Drawing.Point(108, 33);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(439, 88);
            this.lblText.Text = description;
            this.lblText.TabIndex = 2;
            this.lblText.ForeColor = Color.Black;
            this.lblText.DoubleClick += new System.EventHandler(this.doubleClick);
            this.lblText.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.lblText.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(2, 17);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(50, 50);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.DoubleClick += new System.EventHandler(this.doubleClick);
            this.pic.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.pic.MouseLeave += new System.EventHandler(this.mouseLeave);

            //イメージ
            if (!jpgPath.Equals(""))
            {
                this.pic.Image = new Bitmap(new Bitmap(jpgPath), new Size(50, 50));
            }
            else
            {
                this.pic.Image = defaultImage;
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                           onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// doubleClick
        /// ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region doubleClick
        private void doubleClick(object sender, EventArgs e)
        {
            
        }
        #endregion

        /// <summary>
        /// mouseEnter
        /// マウスエンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region mouseEnter
        private void mouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(150, 240, 255, 255);
        }
        #endregion

        /// <summary>
        /// mouseLeave
        /// マウスリーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region mouseLeave
        private void mouseLeave(object sender, EventArgs e)
        {
            if (!select)
            {
                this.BackColor = Color.FromArgb(50, 255, 255, 255);
            }
            else
            {
                this.BackColor = Color.FromArgb(100, 255, 228, 225);
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                           onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// 破棄処理
        /// </summary>
        #region dispose
        public void dispose()
        {
            lblText.Dispose();
            lnkLbl.Dispose();
            pic.Image.Dispose();
            pic.Image = null;
            pic.Dispose();
        }
        #endregion

        ///====================================================================
        ///
        ///                           処理メソッド
        ///                         
        ///====================================================================


        /// <summary>
        /// linkJump
        /// リンクにジャンプする
        /// </summary>
        #region linkJump
        private void linkJump()
        {
            try
            {

            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
        }
        #endregion

        /// <summary>
        /// doProcess
        /// ブラウザを呼び出す
        /// </summary>
        #region doProcess
        private void doProcess()
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception fileNotFoundErr)
            {
                Console.Write(fileNotFoundErr);
            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
        }
        #endregion


    }
}
