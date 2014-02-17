//=======================================================================
//  ClassName : DataPanel
//  概要      : データパネル
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Control;

namespace Liplis.Cmp.Form
{
    public class CusCtlDataPanel : CusCtlPanel
    {
        ///=====================================
        /// 構成要素
        protected CusCtlPictureBox picChar;
        protected CusCtlPictureBox pic;
        protected CusCtlLinkLabel lnkLbl;
        protected CusCtlLabel lblText;
        protected CusCtlLabel lblPoint;
        protected CusCtlLabel lblEmotion;
        

        protected System.Windows.Forms.ContextMenuStrip cms;
        protected System.Windows.Forms.ToolStripMenuItem tsmiCopyTitle;
        protected System.Windows.Forms.ToolStripMenuItem tsmiCopyUrl;
        protected System.Windows.Forms.ToolStripMenuItem tsmiCopyNews;
        protected System.Windows.Forms.ToolStripMenuItem tsmiJumpNews;

        ///=====================================
        /// プロパティ
        public string url           { get; set; }
        public string title         { get; set; }
        public string discription   { get; set; }
        public string jpgPath       { get; set; }

        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region CusCtlDataPanel
        public CusCtlDataPanel(string url, string title, string discription, string jpgPath, int newsEmotion, int newsPoint, Bitmap charBody, IContainer components)
        {
            initCms(components);
            initDataPanel(url, title, discription, jpgPath, newsEmotion, newsPoint, charBody);
        }
        public CusCtlDataPanel()
        {
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
        #region initDataPanel
        protected void initDataPanel(string url, string title, string discription, string jpgPath, int newsEmotion, int newsPoint, Bitmap charBody)
        {
            //要素の取得
            this.url         = url;
            this.title       = title;
            this.discription = discription;;
            this.jpgPath     = jpgPath;

            //初期化
            this.lblText    = new CusCtlLabel();
            this.lnkLbl     = new CusCtlLinkLabel();
            this.pic        = new CusCtlPictureBox();
            this.picChar    = new CusCtlPictureBox();
            this.lblEmotion = new CusCtlLabel();
            this.lblPoint   = new CusCtlLabel();

            // 
            // panel
            // 
            this.BackColor = Color.FromArgb(50,255,255,255);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lnkLbl);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.lblPoint);
            this.Controls.Add(this.lblEmotion);
            this.Controls.Add(this.picChar);
            this.Location = new System.Drawing.Point(3, 3);
            this.Name = "panel";
            this.Size = new System.Drawing.Size(590, 120);
            this.TabIndex = 0;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // 
            // lnkLbl
            // 
            this.lnkLbl.ContextMenuStrip = cms;
            this.lnkLbl.Location = new System.Drawing.Point(148, 11);
            this.lnkLbl.Name = "lnkLbl";
            this.lnkLbl.Size = new System.Drawing.Size(333, 12);
            this.lnkLbl.TabIndex = 1;
            this.lnkLbl.TabStop = true;
            this.lnkLbl.Text = title;
            this.lnkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlLbl_LinkClicked);

            // 
            // lblText
            // 
            this.lblText.ContextMenuStrip = cms;
            this.lblText.Location = new System.Drawing.Point(148, 23);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(333, 88);
            this.lblText.Text = discription;
            this.lblText.TabIndex = 2;
            this.lblText.Click += new System.EventHandler(this.lblText_Click);


            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(12, 11);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(130, 100);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.Click += new System.EventHandler(this.pic_Click);

            //イメージ
            if (!jpgPath.Equals(""))
            {
                this.pic.Image = new Bitmap (new Bitmap(jpgPath), new Size(130,100));
            }
            else
            {
                this.pic.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.TRANSE);
            }

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

        /// <summary>
        /// CMSの初期化
        /// </summary>
        /// <param name="components"></param>
        #region initCms
        protected void initCms(IContainer components)
        {
            this.cms = new System.Windows.Forms.ContextMenuStrip(components);
            this.tsmiCopyTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyNews = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiJumpNews = new System.Windows.Forms.ToolStripMenuItem();
            this.cms.SuspendLayout();
            this.cms.ResumeLayout(false);

            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyTitle,
            this.tsmiCopyUrl,
            this.tsmiCopyNews,
            this.tsmiJumpNews});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(173, 114);
            // 
            // tsmiCopyTitle
            // 
            this.tsmiCopyTitle.Name = "tsmiCopyTitle";
            this.tsmiCopyTitle.Size = new System.Drawing.Size(172, 22);
            this.tsmiCopyTitle.Text = "タイトルをコピー";
            this.tsmiCopyTitle.Click += new System.EventHandler(this.tsmiCopyTitle_Click);
            // 
            // tsmiCopyUrl
            // 
            this.tsmiCopyUrl.Name = "tsmiCopyUrl";
            this.tsmiCopyUrl.Size = new System.Drawing.Size(172, 22);
            this.tsmiCopyUrl.Text = "URLをコピー";
            this.tsmiCopyUrl.Click += new System.EventHandler(this.tsmiCopyUrl_Click);
            // 
            // tsmiCopyNews
            // 
            this.tsmiCopyNews.Name = "tsmiCopyNews";
            this.tsmiCopyNews.Size = new System.Drawing.Size(172, 22);
            this.tsmiCopyNews.Text = "記事をコピー";
            this.tsmiCopyNews.Click += new System.EventHandler(this.tsmiCopyNews_Click);
            // 
            // tsmiJumpNews
            // 
            this.tsmiJumpNews.Name = "tsmiJumpNews";
            this.tsmiJumpNews.Size = new System.Drawing.Size(172, 22);
            this.tsmiJumpNews.Text = "記事にジャンプ";
            this.tsmiJumpNews.Click += new System.EventHandler(this.tsmiJumpNews_Click);
        }
        #endregion

        ///====================================================================
        ///
        ///                           onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// lnlLbl_LinkClicked
        /// リンクラベルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region lnlLbl_LinkClicked
        protected void lnlLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    linkJump();
                }
                catch (System.Exception err)
                {
                    LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                }
            }
            
        }
        #endregion
        
        /// <summary>
        /// pic_Click
        /// ピクチャークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region pic_Click
        protected void pic_Click(object sender, EventArgs e)
        {
            linkJump();
        }
        #endregion

        /// <summary>
        /// lblText_Click
        /// テキストクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region lblText_Click
        protected void lblText_Click(object sender, EventArgs e)
        {
            //linkJump();
        }
        #endregion

        /// <summary>
        /// tsmiCopyTitle_Click
        /// タイトルコピークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiCopyTitle_Click
        private void tsmiCopyTitle_Click(object sender, EventArgs e)
        {
            LpsLiplisUtil.setDataToClipBord(this.title);
        }
        #endregion

        
        /// <summary>
        /// tsmiCopyUrl_Click
        /// URLコピークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region lblText_Click
        private void tsmiCopyUrl_Click(object sender, EventArgs e)
        {
            LpsLiplisUtil.setDataToClipBord(this.url);
        }
        #endregion

        
        /// <summary>
        /// tsmiCopyNews_Click
        /// ニュースコピークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region lblText_Click
        private void tsmiCopyNews_Click(object sender, EventArgs e)
        {
            LpsLiplisUtil.setDataToClipBord(this.discription);
        }
        #endregion


        /// <summary>
        /// tsmiJumpNews_Click
        /// ニュースジャンプクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region lblText_Click
        private void tsmiJumpNews_Click(object sender, EventArgs e)
        {
            linkJump();
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
        public virtual void dispose()
        {
            lblText.Dispose();
            lnkLbl.Dispose();
            pic.Image.Dispose();
            pic.Image = null;
            pic.Dispose();
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


        /// <summary>
        /// linkJump
        /// リンクにジャンプする
        /// </summary>
        #region linkJump
        protected void linkJump()
        {
            try
            {
                new LpsDelegate.dlgVoidToVoid(doProcess).BeginInvoke(null, null);
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
        protected void doProcess()
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


        /// <summary>
        /// getEmotion
        /// エモーションを取得する
        /// </summary>
        /// <returns></returns>
        #region getEmotion
        protected string getEmotion(int newsEmotion, int newsPoint)
        {
            switch(newsEmotion)
            {
                case 1:
                    if (newsPoint >= 0)
                    {
                        return "うれしい";
                    }
                    else
                    {
                        return "かなしい";
                    }
                case 2: 
                    if (newsPoint >= 0)
                    {
                        return "好き";
                    }
                    else
                    {
                        return "きらい・・・";
                    }
                case 3:
                    if (newsPoint >= 0)
                    {
                        return "安心";
                    }
                    else
                    {
                        return "不安・・・";
                    }
                case 4:
                    if (newsPoint >= 0)
                    {
                        return "快感！";
                    }
                    else
                    {
                        return "きもちわるい・・・";
                    }
                case 5:
                    if (newsPoint >= 0)
                    {
                        return "びっくり！";
                    }
                    else
                    {
                        return "びっくり！";
                    }
                case 6:
                    if (newsPoint >= 0)
                    {
                        return "怒り！";
                    }
                    else
                    {
                        return "・・・";
                    }
                case 7:
                    if (newsPoint >= 0)
                    {
                        return "興味津々！";
                    }
                    else
                    {
                        return "ふーん";
                    }
                case 8:
                    if (newsPoint >= 0)
                    {
                        return "尊敬！";
                    }
                    else
                    {
                        return "残念です・・・";
                    }
                case 9:
                    if (newsPoint >= 0)
                    {
                        return "冷静";
                    }
                    else
                    {
                        return "あせあせ";
                    }
                case 10:
                    if (newsPoint >= 0)
                    {
                        return "えっへん！";
                    }
                    else
                    {
                        return "はずかしい・・・";
                    }
                default:
                    return "";
            }
        }
        #endregion
    }
}
