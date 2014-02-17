//=======================================================================
//  ClassName : DataPanel
//  概要      : データパネル
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
using System.ComponentModel;
using Liplis.Control;

namespace Liplis.Cmp.Form
{
    public class DataPanelNonThum : DataPanel
    {
        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region DataPanelNonThum
        public DataPanelNonThum(string url, string title, string discription, int newsEmotion, int newsPoint, Bitmap charBody, EventHandler enter, IContainer components)
        {
            initCms(components);
            initDataPanelNonThum(url, title, discription, newsEmotion, newsPoint, charBody, enter);
        }
        #endregion


        /// <summary>
        /// データパネルの初期化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        /// <param name="discription"></param>
        /// <param name="cat"></param>
        /// <param name="jpgPath"></param>
        /// <param name="enter"></param>
        /// <param name="cmst"></param>
        /// <param name="cms"></param>
        #region initDataPanelNonThum
        private void initDataPanelNonThum(string url, string title, string discription, int newsEmotion, int newsPoint, Bitmap charBody, EventHandler enter)
        {
            //要素の取得
            this.url         = url;
            this.title       = title;
            this.discription = discription;;
            this.jpgPath     = jpgPath;

            //初期化
            this.lblText    = new CusCtlLabel();
            this.lnkLbl     = new CusCtlLinkLabel();
            this.picChar    = new CusCtlPictureBox();
            this.lblEmotion = new CusCtlLabel();
            this.lblPoint   = new CusCtlLabel();


            // 
            // panel
            // 
            this.BackColor = Color.FromArgb(50,255,255,255);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lnkLbl);
            this.Controls.Add(this.lblPoint);
            this.Controls.Add(this.lblEmotion);
            this.Controls.Add(this.picChar);
            this.Location = new System.Drawing.Point(3, 3);
            this.Name = "panel";
            this.Size = new System.Drawing.Size(590, 120);
            this.TabIndex = 0;
            this.MouseEnter += new System.EventHandler(enter);

            // 
            // lnkLbl
            // 
            this.lnkLbl.ContextMenuStrip = cms;
            this.lnkLbl.Location = new System.Drawing.Point(11, 11);
            this.lnkLbl.Name = "lnkLbl";
            this.lnkLbl.Size = new System.Drawing.Size(470, 12);
            this.lnkLbl.TabIndex = 1;
            this.lnkLbl.TabStop = true;
            this.lnkLbl.Text = title;
            this.lnkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlLbl_LinkClicked);
            this.lnkLbl.MouseEnter += new System.EventHandler(enter);

            // 
            // lblText
            // 
            this.lblText.ContextMenuStrip = cms;
            this.lblText.Location = new System.Drawing.Point(11, 23);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(470, 88);
            this.lblText.Text = discription;
            this.lblText.TabIndex = 2;
            this.lblText.Click += new System.EventHandler(this.lblText_Click);
            this.lblText.MouseEnter += new System.EventHandler(enter);

            // 
            // picChar
            // 
            this.picChar.Location = new System.Drawing.Point(487, 11);
            this.picChar.Name = "picChar";
            this.picChar.Size = new System.Drawing.Size(100, 100);
            this.picChar.TabIndex = 3;
            this.picChar.TabStop = false;
            this.picChar.Image = charBody;
            // 
            // lblEmotion
            // 
            this.lblEmotion.BackColor = Color.FromArgb(220, 229, 242, 247);
            this.lblEmotion.Location = new System.Drawing.Point(487, 81);
            this.lblEmotion.Name = "lblEmotion";
            this.lblEmotion.Size = new System.Drawing.Size(100, 15);
            this.lblEmotion.TabIndex = 4;
            this.lblEmotion.Text = getEmotion(newsEmotion, newsPoint);
            this.lblEmotion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPoint
            // 
            this.lblPoint.BackColor = Color.FromArgb(220, 229, 242, 247);
            this.lblPoint.Location = new System.Drawing.Point(487, 96);
            this.lblPoint.Name = "lblPoint";
            this.lblPoint.Size = new System.Drawing.Size(100, 15);
            this.lblPoint.TabIndex = 5;
            this.lblPoint.Text = newsPoint.ToString();
            this.lblPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
        public override void dispose()
        {
            lblText.Dispose();
            lnkLbl.Dispose();
            picChar.Image.Dispose();
            picChar.Image = null;
            picChar.Dispose();
        }
        #endregion

        ///====================================================================
        ///
        ///                           処理メソッド
        ///                         
        ///====================================================================

    }
}
