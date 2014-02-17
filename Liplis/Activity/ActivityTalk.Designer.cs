﻿using Liplis.Control;
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
            this.cms.SuspendLayout();
            this.cmst.SuspendLayout();
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
            // ActivityTalk
            // 
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = global::Liplis.Properties.Resources.window;
            this.Controls.Add(this.wbTalk);
            this.Controls.Add(this.lnkLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ActivityTalk";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivityTalk_FormClosing);
            this.Load += new System.EventHandler(this.WinTalk_Load);
            this.MouseEnter += new System.EventHandler(this.ActivityTalk_MouseEnter);
            this.cms.ResumeLayout(false);
            this.cmst.ResumeLayout(false);
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
    }
}
