using Liplis.Control;
namespace Liplis.Activity
{
    partial class ActivityTalk
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityTalk));
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShow = new System.Windows.Forms.ToolStripMenuItem();
            this.lnkLbl = new System.Windows.Forms.LinkLabel();
            this.cmst = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmitTitleCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmitLinkCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmitShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmitDogaDown = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmitMp3Down = new System.Windows.Forms.ToolStripMenuItem();
            this.mHTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wbTalk = new System.Windows.Forms.WebBrowser();
            this.btnNext = new System.Windows.Forms.PictureBox();
            this.btnUrlCopy = new System.Windows.Forms.PictureBox();
            this.btnWebJump = new System.Windows.Forms.PictureBox();
            this.btnTweet = new System.Windows.Forms.PictureBox();
            this.lblEmotion = new System.Windows.Forms.Label();
            this.lblPoint = new System.Windows.Forms.Label();
            this.btnTell = new System.Windows.Forms.PictureBox();
            this.cms.SuspendLayout();
            this.cmst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUrlCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnWebJump)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTweet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTell)).BeginInit();
            this.SuspendLayout();
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopy,
            this.tsmiShow});
            this.cms.Name = "cms";
            resources.ApplyResources(this.cms, "cms");
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            resources.ApplyResources(this.tsmiCopy, "tsmiCopy");
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiShow
            // 
            this.tsmiShow.Name = "tsmiShow";
            resources.ApplyResources(this.tsmiShow, "tsmiShow");
            this.tsmiShow.Click += new System.EventHandler(this.tsmiShow_Click);
            // 
            // lnkLbl
            // 
            this.lnkLbl.BackColor = System.Drawing.Color.Transparent;
            this.lnkLbl.ContextMenuStrip = this.cmst;
            resources.ApplyResources(this.lnkLbl, "lnkLbl");
            this.lnkLbl.Name = "lnkLbl";
            this.lnkLbl.UseWaitCursor = true;
            this.lnkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClickEvent);
            // 
            // cmst
            // 
            this.cmst.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmitTitleCopy,
            this.tsmitLinkCopy,
            this.tsmitShow,
            this.tsmitDogaDown,
            this.tsmitMp3Down,
            this.mHTToolStripMenuItem});
            this.cmst.Name = "cmst";
            resources.ApplyResources(this.cmst, "cmst");
            this.cmst.Opening += new System.ComponentModel.CancelEventHandler(this.cmst_Opening);
            // 
            // tsmitTitleCopy
            // 
            this.tsmitTitleCopy.Name = "tsmitTitleCopy";
            resources.ApplyResources(this.tsmitTitleCopy, "tsmitTitleCopy");
            this.tsmitTitleCopy.Click += new System.EventHandler(this.tsmitTitleCopy_Click);
            // 
            // tsmitLinkCopy
            // 
            this.tsmitLinkCopy.Name = "tsmitLinkCopy";
            resources.ApplyResources(this.tsmitLinkCopy, "tsmitLinkCopy");
            this.tsmitLinkCopy.Click += new System.EventHandler(this.tsmitLinkCopy_Click);
            // 
            // tsmitShow
            // 
            this.tsmitShow.Name = "tsmitShow";
            resources.ApplyResources(this.tsmitShow, "tsmitShow");
            this.tsmitShow.Click += new System.EventHandler(this.tsmitShow_Click);
            // 
            // tsmitDogaDown
            // 
            this.tsmitDogaDown.Name = "tsmitDogaDown";
            resources.ApplyResources(this.tsmitDogaDown, "tsmitDogaDown");
            // 
            // tsmitMp3Down
            // 
            this.tsmitMp3Down.Name = "tsmitMp3Down";
            resources.ApplyResources(this.tsmitMp3Down, "tsmitMp3Down");
            // 
            // mHTToolStripMenuItem
            // 
            this.mHTToolStripMenuItem.Name = "mHTToolStripMenuItem";
            resources.ApplyResources(this.mHTToolStripMenuItem, "mHTToolStripMenuItem");
            // 
            // wbTalk
            // 
            resources.ApplyResources(this.wbTalk, "wbTalk");
            this.wbTalk.Name = "wbTalk";
            this.wbTalk.ScrollBarsEnabled = false;
            this.wbTalk.Url = new System.Uri("http://m", System.UriKind.Absolute);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.TabStop = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnUrlCopy
            // 
            this.btnUrlCopy.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnUrlCopy, "btnUrlCopy");
            this.btnUrlCopy.Name = "btnUrlCopy";
            this.btnUrlCopy.TabStop = false;
            this.btnUrlCopy.Click += new System.EventHandler(this.btnUrlCopy_Click);
            // 
            // btnWebJump
            // 
            this.btnWebJump.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnWebJump, "btnWebJump");
            this.btnWebJump.Name = "btnWebJump";
            this.btnWebJump.TabStop = false;
            this.btnWebJump.Click += new System.EventHandler(this.btnWebJump_Click);
            // 
            // btnTweet
            // 
            this.btnTweet.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnTweet, "btnTweet");
            this.btnTweet.Name = "btnTweet";
            this.btnTweet.TabStop = false;
            this.btnTweet.Click += new System.EventHandler(this.btnTweet_Click);
            // 
            // lblEmotion
            // 
            this.lblEmotion.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblEmotion, "lblEmotion");
            this.lblEmotion.Name = "lblEmotion";
            // 
            // lblPoint
            // 
            this.lblPoint.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblPoint, "lblPoint");
            this.lblPoint.Name = "lblPoint";
            // 
            // btnTell
            // 
            this.btnTell.BackColor = System.Drawing.Color.Transparent;
            this.btnTell.Image = global::Liplis.Properties.Resources.ico_talk_tell;
            resources.ApplyResources(this.btnTell, "btnTell");
            this.btnTell.Name = "btnTell";
            this.btnTell.TabStop = false;
            this.btnTell.Click += new System.EventHandler(this.btnTell_Click);
            // 
            // ActivityTalk
            // 
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = global::Liplis.Properties.Resources.window;
            this.Controls.Add(this.btnTell);
            this.Controls.Add(this.lblPoint);
            this.Controls.Add(this.lblEmotion);
            this.Controls.Add(this.btnTweet);
            this.Controls.Add(this.btnWebJump);
            this.Controls.Add(this.btnUrlCopy);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.wbTalk);
            this.Controls.Add(this.lnkLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ActivityTalk";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivityTalk_FormClosing);
            this.Load += new System.EventHandler(this.WinTalk_Load);
            this.MouseEnter += new System.EventHandler(this.ActivityTalk_MouseEnter);
            this.cms.ResumeLayout(false);
            this.cmst.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUrlCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnWebJump)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTweet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTell)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiShow;
        private System.Windows.Forms.ContextMenuStrip cmst;
        private System.Windows.Forms.ToolStripMenuItem tsmitTitleCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmitLinkCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmitShow;
        private System.Windows.Forms.ToolStripMenuItem tsmitDogaDown;
        private System.Windows.Forms.ToolStripMenuItem tsmitMp3Down;
        private System.Windows.Forms.ToolStripMenuItem mHTToolStripMenuItem;
        protected System.Windows.Forms.LinkLabel lnkLbl;
        protected System.Windows.Forms.WebBrowser wbTalk;
        private System.Windows.Forms.PictureBox btnNext;
        private System.Windows.Forms.PictureBox btnUrlCopy;
        private System.Windows.Forms.PictureBox btnWebJump;
        private System.Windows.Forms.PictureBox btnTweet;
        private System.Windows.Forms.Label lblEmotion;
        private System.Windows.Forms.Label lblPoint;
        private System.Windows.Forms.PictureBox btnTell;
    }
}
