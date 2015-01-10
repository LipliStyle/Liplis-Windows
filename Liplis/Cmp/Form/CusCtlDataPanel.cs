//=======================================================================
//  ClassName : CusCtlDataPanel
//  概要      : カスタムデータパネル(サムネイルあり)
//
//  Liplis2.3
//  2013/06/20 Liplis2.3.0 UI変更
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Liplis.Activity;
using Liplis.Common;
using Liplis.Control;
using Liplis.Fct;
using Liplis.Msg;

namespace Liplis.Cmp.Form
{
    public class CusCtlDataPanel : CusCtlPanel
    {
        ///=====================================
        /// lips
        protected Liplis.MainSystem.Liplis lips;
        protected ObjSetting os;

        ///=====================================
        /// 構成要素
        protected CusCtlPictureBox picChar;
        protected CusCtlPictureBox pic;
        protected CusCtlLinkLabel lnkLbl;
        protected CusCtlLabel lblPoint;
        protected CusCtlLabel lblEmotion;

        protected System.Windows.Forms.PictureBox btnUrlCopy;
        protected System.Windows.Forms.PictureBox btnWebJump;
        protected System.Windows.Forms.PictureBox btnTweet;
        

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
        #region DataPanel
        public CusCtlDataPanel(Liplis.MainSystem.Liplis lips,ObjSetting os, string url, string title, string discription, string jpgPath, int newsEmotion, int newsPoint, Bitmap charBody, EventHandler enter, IContainer components)
        {
            this.lips = lips;
            this.os = os;
            initCms(components);
            initDataPanel(url, title, discription, jpgPath, newsEmotion, newsPoint, charBody, enter);
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
        protected void initDataPanel(string url, string title, string discription, string jpgPath, int newsEmotion, int newsPoint, Bitmap charBody, EventHandler enter)
        {
            //要素の取得
            this.url         = url;
            this.title       = title;
            this.discription = discription;;
            this.jpgPath     = jpgPath;

            //初期化
            //this.lblText    = new CusCtlLabel();
            this.lnkLbl     = new CusCtlLinkLabel();
            this.pic        = new CusCtlPictureBox();
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
            this.Controls.Add(this.pic);
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
            this.lnkLbl.Size = new System.Drawing.Size(290, 12);
            this.lnkLbl.TabIndex = 1;
            this.lnkLbl.TabStop = true;
            this.lnkLbl.Text = title;
            this.lnkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlLbl_LinkClicked);
            this.lnkLbl.MouseEnter += new System.EventHandler(enter);


            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(380, 12);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(65, 50);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.Click += new System.EventHandler(this.pic_Click);
            this.pic.MouseEnter += new System.EventHandler(enter);

            try
            {
                //イメージ
                if (!jpgPath.Equals(""))
                {
                    this.pic.Image = new Bitmap(new Bitmap(jpgPath), new Size(65, 50));
                }
                else
                {
                    this.pic.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.TRANSE);
                }
            }
            catch
            {

            }
            

            // 
            // picChar
            // 
            //int wid = 50 * (lips.Width/lips.Height);
            this.picChar.Location = new System.Drawing.Point(3, 11);
            this.picChar.Name = "picChar";
            this.picChar.Size = new System.Drawing.Size(75, 75);
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

        /// <summary>
        /// /スモールアイコンの初期化
        /// </summary>
        #region initSmallIcon
        protected void initSmallIcon()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityTalk));
            // 
            // btnUrlCopy
            // 
            this.btnUrlCopy.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnUrlCopy, "btnUrlCopy");
            this.btnUrlCopy.Size = new System.Drawing.Size(20, 20);
            this.btnUrlCopy.Location = new System.Drawing.Point(230, 40);
            this.btnUrlCopy.Name = "btnUrlCopy";
            this.btnUrlCopy.TabStop = false;
            this.btnUrlCopy.Click += new System.EventHandler(this.btnUrlCopy_Click);
            // 
            // btnWebJump
            // 
            this.btnWebJump.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnWebJump, "btnWebJump");
            this.btnWebJump.Size = new System.Drawing.Size(20, 20);
            this.btnWebJump.Location = new System.Drawing.Point(255, 40);
            this.btnWebJump.Name = "btnWebJump";
            this.btnWebJump.TabStop = false;
            this.btnWebJump.Click += new System.EventHandler(this.btnWebJump_Click);
            // 
            // btnTweet
            // 
            this.btnTweet.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnTweet, "btnTweet");
            this.btnTweet.Size = new System.Drawing.Size(20, 20);
            this.btnTweet.Location = new System.Drawing.Point(280, 40);
            this.btnTweet.Name = "btnTweet";
            this.btnTweet.TabStop = false;
            this.btnTweet.Click += new System.EventHandler(this.btnTweet_Click);
        }
        #endregion

        /// <summary>
        /// スモールアイコンOFF
        /// </summary>
        #region smallIconOff
        public void smallIconOff()
        {
            btnUrlCopy.Visible = false;
            btnWebJump.Visible = false;
            btnTweet.Visible = false;
        }
        #endregion

        /// <summary>
        /// CMSの初期化
        /// </summary>
        /// <param name="components"></param>
        #region initCms
        protected void initCms(IContainer components)
        {
            this.cms           = new ContextMenuStrip(components);
            this.tsmiCopyTitle = new ToolStripMenuItem();
            this.tsmiCopyUrl   = new ToolStripMenuItem();
            this.tsmiCopyNews  = new ToolStripMenuItem();
            this.tsmiJumpNews  = new ToolStripMenuItem();
            this.cms.SuspendLayout();
            this.cms.ResumeLayout(false);

            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyTitle,
            this.tsmiCopyUrl,
            this.tsmiCopyNews,
            this.tsmiJumpNews,
            }
            );
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(173, 114);
            this.cms.Opening += new CancelEventHandler(cms_Opening);
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


        /// <summary>
        /// cmst_Opening
        /// cmstがオープンされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region cmst_Opening
        private void cms_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        
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

        /// <summary>
        /// 次の話題
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void btnNext_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_NEXT, "");
        }
        #endregion

        /// <summary>
        /// URLコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        protected void btnUrlCopy_Click(object sender, EventArgs e)
        {
            LpsLiplisUtil.setDataToClipBord(url);
        }
        #endregion

        /// <summary>
        /// URLジャンプ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        protected void btnWebJump_Click(object sender, EventArgs e)
        {
            linkJump();
        }
        #endregion

        /// <summary>
        /// ツイート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        protected void btnTweet_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_TWEET, lnkLbl.Text);
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
            //lblText.Dispose();
            lnkLbl.Dispose();
            pic.Image.Dispose();
            pic.Image = null;
            pic.Dispose();
            picChar.Image.Dispose();
            picChar.Image = null;
            picChar.Dispose();
            this.Dispose();
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
                        return "うれしい！";
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
                    return "ふつう";
            }
        }
        #endregion
    }
}
