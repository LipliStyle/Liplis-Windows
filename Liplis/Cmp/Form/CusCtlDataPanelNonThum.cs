﻿//=======================================================================
//  ClassName : CusCtlDataPanelNonThum
//  概要      : カスタムデータパネル(サムネイルなし)
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.ComponentModel;
using System.Drawing;
using Liplis.Activity;
using Liplis.Control;
using Liplis.Msg;

namespace Liplis.Cmp.Form
{
    public class CusCtlDataPanelNonThum : CusCtlDataPanel
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
        public CusCtlDataPanelNonThum(Liplis.MainSystem.Liplis lips, ObjSetting os, string url, string title, string discription, int newsEmotion, int newsPoint, Bitmap charBody, EventHandler enter, IContainer components)
        {
            this.lips = lips;
            this.os = os;
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
            //this.lblText    = new CusCtlLabel();
            this.lnkLbl     = new CusCtlLinkLabel();
            this.picChar    = new CusCtlPictureBox();
            this.lblEmotion = new CusCtlLabel();
            this.lblPoint   = new CusCtlLabel();
            this.btnUrlCopy = new System.Windows.Forms.PictureBox();
            this.btnWebJump = new System.Windows.Forms.PictureBox();
            this.btnTweet = new System.Windows.Forms.PictureBox();

            // 
            // panel
            // 
            this.BackColor = Color.Azure;
            //this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnUrlCopy);
            this.Controls.Add(this.btnWebJump);
            this.Controls.Add(this.btnTweet);

            this.Controls.Add(this.lnkLbl);
            this.Controls.Add(this.lblPoint);
            this.Controls.Add(this.lblEmotion);
            this.Controls.Add(this.picChar);
            this.Location = new System.Drawing.Point(3, 3);
            this.Name = "panel";
            this.Size = new System.Drawing.Size(445, 60);
            this.TabIndex = 0;
            this.MouseEnter += new System.EventHandler(enter);

            // 
            // lnkLbl
            // 
            this.lnkLbl.ContextMenuStrip = cms;
            this.lnkLbl.Location = new System.Drawing.Point(85, 14);
            this.lnkLbl.Name = "lnkLbl";
            this.lnkLbl.Size = new System.Drawing.Size(355, 12);
            this.lnkLbl.TabIndex = 1;
            this.lnkLbl.TabStop = true;
            this.lnkLbl.Text = title;
            this.lnkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlLbl_LinkClicked);
            this.lnkLbl.MouseEnter += new System.EventHandler(enter);

            // 
            // lblText
            // 
            //this.lblText.ContextMenuStrip = cms;
            //this.lblText.Location = new System.Drawing.Point(11, 23);
            //this.lblText.Name = "lblText";
            //this.lblText.Size = new System.Drawing.Size(470, 88);
            //this.lblText.Text = discription;
            //this.lblText.TabIndex = 2;
            //this.lblText.Click += new System.EventHandler(this.lblText_Click);
            //this.lblText.MouseEnter += new System.EventHandler(enter);

            // 
            // picChar
            // 
            this.picChar.Location = new System.Drawing.Point(3, 11);
            this.picChar.Name = "picChar";
            this.picChar.Size = new System.Drawing.Size(75,50);
            this.picChar.TabIndex = 3;
            this.picChar.TabStop = false;
            this.picChar.Image = charBody;
            // 
            // lblEmotion
            // 
            this.lblEmotion.BackColor = Color.FromArgb(220, 229, 242, 247);
            this.lblEmotion.Location = new System.Drawing.Point(122, 40);
            this.lblEmotion.Name = "lblEmotion";
            this.lblEmotion.Size = new System.Drawing.Size(100, 15);
            this.lblEmotion.TabIndex = 4;
            this.lblEmotion.Text = getEmotion(newsEmotion, newsPoint);
            this.lblEmotion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPoint
            // 
            this.lblPoint.BackColor = Color.FromArgb(220, 229, 242, 247);
            this.lblPoint.Location = new System.Drawing.Point(80, 40);
            this.lblPoint.Name = "lblPoint";
            this.lblPoint.Size = new System.Drawing.Size(40, 15);
            this.lblPoint.TabIndex = 5;
            this.lblPoint.Text = newsPoint.ToString();
            this.lblPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            //スモールアイコンの初期化
            initSmallIcon();
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
            //lblText.Dispose();
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
