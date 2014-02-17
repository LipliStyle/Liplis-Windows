namespace Liplis.Activity
{
    partial class ActivityRssBwrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityRssBwrowser));
            this.splRssCenter = new System.Windows.Forms.SplitContainer();
            this.tvRss = new System.Windows.Forms.TreeView();
            this.splRssRight = new System.Windows.Forms.SplitContainer();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.KIDOKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splRssBrowser = new System.Windows.Forms.SplitContainer();
            this.splRssBrowserCtl = new System.Windows.Forms.SplitContainer();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRssBrNext = new System.Windows.Forms.Button();
            this.btnRssBrBack = new System.Windows.Forms.Button();
            this.grp = new System.Windows.Forms.GroupBox();
            this.cboUrl = new System.Windows.Forms.ComboBox();
            this.rssWb = new System.Windows.Forms.WebBrowser();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRegist = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReload = new System.Windows.Forms.ToolStripMenuItem();
            this.splRssCenter.Panel1.SuspendLayout();
            this.splRssCenter.Panel2.SuspendLayout();
            this.splRssCenter.SuspendLayout();
            this.splRssRight.Panel1.SuspendLayout();
            this.splRssRight.Panel2.SuspendLayout();
            this.splRssRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.splRssBrowser.Panel1.SuspendLayout();
            this.splRssBrowser.Panel2.SuspendLayout();
            this.splRssBrowser.SuspendLayout();
            this.splRssBrowserCtl.Panel1.SuspendLayout();
            this.splRssBrowserCtl.Panel2.SuspendLayout();
            this.splRssBrowserCtl.SuspendLayout();
            this.grp.SuspendLayout();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splRssCenter
            // 
            this.splRssCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splRssCenter.Location = new System.Drawing.Point(0, 26);
            this.splRssCenter.Name = "splRssCenter";
            // 
            // splRssCenter.Panel1
            // 
            this.splRssCenter.Panel1.Controls.Add(this.tvRss);
            // 
            // splRssCenter.Panel2
            // 
            this.splRssCenter.Panel2.Controls.Add(this.splRssRight);
            this.splRssCenter.Size = new System.Drawing.Size(1008, 704);
            this.splRssCenter.SplitterDistance = 267;
            this.splRssCenter.TabIndex = 3;
            // 
            // tvRss
            // 
            this.tvRss.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRss.Location = new System.Drawing.Point(0, 0);
            this.tvRss.Name = "tvRss";
            this.tvRss.Size = new System.Drawing.Size(267, 704);
            this.tvRss.TabIndex = 3;
            this.tvRss.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRss_AfterSelect);
            // 
            // splRssRight
            // 
            this.splRssRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splRssRight.Location = new System.Drawing.Point(0, 0);
            this.splRssRight.Name = "splRssRight";
            this.splRssRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splRssRight.Panel1
            // 
            this.splRssRight.Panel1.Controls.Add(this.dgv);
            // 
            // splRssRight.Panel2
            // 
            this.splRssRight.Panel2.Controls.Add(this.splRssBrowser);
            this.splRssRight.Size = new System.Drawing.Size(737, 704);
            this.splRssRight.SplitterDistance = 237;
            this.splRssRight.TabIndex = 0;
            // 
            // dgv
            // 
            this.dgv.AllowDrop = true;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KIDOKU,
            this.TITLE,
            this.DATE,
            this.URL});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 21;
            this.dgv.Size = new System.Drawing.Size(737, 237);
            this.dgv.TabIndex = 1;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            // 
            // KIDOKU
            // 
            this.KIDOKU.HeaderText = "";
            this.KIDOKU.Name = "KIDOKU";
            this.KIDOKU.Width = 50;
            // 
            // TITLE
            // 
            this.TITLE.HeaderText = "記事タイトル";
            this.TITLE.Name = "TITLE";
            this.TITLE.ReadOnly = true;
            this.TITLE.Width = 500;
            // 
            // DATE
            // 
            this.DATE.HeaderText = "更新日";
            this.DATE.Name = "DATE";
            this.DATE.ReadOnly = true;
            // 
            // URL
            // 
            this.URL.HeaderText = "URL";
            this.URL.Name = "URL";
            this.URL.ReadOnly = true;
            this.URL.Width = 180;
            // 
            // splRssBrowser
            // 
            this.splRssBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splRssBrowser.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splRssBrowser.IsSplitterFixed = true;
            this.splRssBrowser.Location = new System.Drawing.Point(0, 0);
            this.splRssBrowser.Name = "splRssBrowser";
            this.splRssBrowser.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splRssBrowser.Panel1
            // 
            this.splRssBrowser.Panel1.Controls.Add(this.splRssBrowserCtl);
            // 
            // splRssBrowser.Panel2
            // 
            this.splRssBrowser.Panel2.Controls.Add(this.rssWb);
            this.splRssBrowser.Size = new System.Drawing.Size(737, 463);
            this.splRssBrowser.SplitterDistance = 37;
            this.splRssBrowser.TabIndex = 0;
            // 
            // splRssBrowserCtl
            // 
            this.splRssBrowserCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splRssBrowserCtl.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splRssBrowserCtl.Location = new System.Drawing.Point(0, 0);
            this.splRssBrowserCtl.Name = "splRssBrowserCtl";
            // 
            // splRssBrowserCtl.Panel1
            // 
            this.splRssBrowserCtl.Panel1.Controls.Add(this.btnStop);
            this.splRssBrowserCtl.Panel1.Controls.Add(this.btnUpdate);
            this.splRssBrowserCtl.Panel1.Controls.Add(this.btnRssBrNext);
            this.splRssBrowserCtl.Panel1.Controls.Add(this.btnRssBrBack);
            // 
            // splRssBrowserCtl.Panel2
            // 
            this.splRssBrowserCtl.Panel2.Controls.Add(this.grp);
            this.splRssBrowserCtl.Size = new System.Drawing.Size(737, 37);
            this.splRssBrowserCtl.SplitterDistance = 231;
            this.splRssBrowserCtl.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(180, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(48, 32);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "中止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(127, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(48, 32);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRssBrNext
            // 
            this.btnRssBrNext.Location = new System.Drawing.Point(58, 2);
            this.btnRssBrNext.Name = "btnRssBrNext";
            this.btnRssBrNext.Size = new System.Drawing.Size(48, 32);
            this.btnRssBrNext.TabIndex = 7;
            this.btnRssBrNext.Text = "進む";
            this.btnRssBrNext.UseVisualStyleBackColor = true;
            this.btnRssBrNext.Click += new System.EventHandler(this.btnRssBrNext_Click);
            // 
            // btnRssBrBack
            // 
            this.btnRssBrBack.Location = new System.Drawing.Point(4, 2);
            this.btnRssBrBack.Name = "btnRssBrBack";
            this.btnRssBrBack.Size = new System.Drawing.Size(48, 32);
            this.btnRssBrBack.TabIndex = 6;
            this.btnRssBrBack.Text = "戻る";
            this.btnRssBrBack.UseVisualStyleBackColor = true;
            this.btnRssBrBack.Click += new System.EventHandler(this.btnRssBrBack_Click);
            // 
            // grp
            // 
            this.grp.Controls.Add(this.cboUrl);
            this.grp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp.Location = new System.Drawing.Point(0, 0);
            this.grp.Name = "grp";
            this.grp.Size = new System.Drawing.Size(502, 37);
            this.grp.TabIndex = 0;
            this.grp.TabStop = false;
            // 
            // cboUrl
            // 
            this.cboUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboUrl.FormattingEnabled = true;
            this.cboUrl.Location = new System.Drawing.Point(3, 15);
            this.cboUrl.Name = "cboUrl";
            this.cboUrl.Size = new System.Drawing.Size(496, 20);
            this.cboUrl.TabIndex = 0;
            // 
            // rssWb
            // 
            this.rssWb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rssWb.Location = new System.Drawing.Point(0, 0);
            this.rssWb.MinimumSize = new System.Drawing.Size(20, 20);
            this.rssWb.Name = "rssWb";
            this.rssWb.Size = new System.Drawing.Size(737, 422);
            this.rssWb.TabIndex = 0;
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(1008, 26);
            this.msMenu.TabIndex = 4;
            this.msMenu.Text = "menuStrip1";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRegist,
            this.tsmiReload});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(44, 22);
            this.tsmFile.Text = "RSS";
            // 
            // tsmiRegist
            // 
            this.tsmiRegist.Name = "tsmiRegist";
            this.tsmiRegist.Size = new System.Drawing.Size(152, 22);
            this.tsmiRegist.Text = "登録";
            this.tsmiRegist.Click += new System.EventHandler(this.tsmiRegist_Click);
            // 
            // tsmiReload
            // 
            this.tsmiReload.Name = "tsmiReload";
            this.tsmiReload.Size = new System.Drawing.Size(152, 22);
            this.tsmiReload.Text = "再読み込み";
            this.tsmiReload.Click += new System.EventHandler(this.tsmiReload_Click);
            // 
            // ActivityRssBwrowser
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splRssCenter);
            this.Controls.Add(this.msMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActivityRssBwrowser";
            this.Text = "RSSブラウザ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivityRss_FormClosing);
            this.Load += new System.EventHandler(this.ActivityRss_Load);
            this.splRssCenter.Panel1.ResumeLayout(false);
            this.splRssCenter.Panel2.ResumeLayout(false);
            this.splRssCenter.ResumeLayout(false);
            this.splRssRight.Panel1.ResumeLayout(false);
            this.splRssRight.Panel2.ResumeLayout(false);
            this.splRssRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.splRssBrowser.Panel1.ResumeLayout(false);
            this.splRssBrowser.Panel2.ResumeLayout(false);
            this.splRssBrowser.ResumeLayout(false);
            this.splRssBrowserCtl.Panel1.ResumeLayout(false);
            this.splRssBrowserCtl.Panel2.ResumeLayout(false);
            this.splRssBrowserCtl.ResumeLayout(false);
            this.grp.ResumeLayout(false);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splRssCenter;
        private System.Windows.Forms.TreeView tvRss;
        private System.Windows.Forms.SplitContainer splRssRight;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn KIDOKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn URL;
        private System.Windows.Forms.SplitContainer splRssBrowser;
        private System.Windows.Forms.SplitContainer splRssBrowserCtl;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRssBrNext;
        private System.Windows.Forms.Button btnRssBrBack;
        private System.Windows.Forms.GroupBox grp;
        private System.Windows.Forms.ComboBox cboUrl;
        private System.Windows.Forms.WebBrowser rssWb;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiRegist;
        private System.Windows.Forms.ToolStripMenuItem tsmiReload;

    }
}