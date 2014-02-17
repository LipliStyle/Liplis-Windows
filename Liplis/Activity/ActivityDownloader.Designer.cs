namespace Liplis.Activity
{
    partial class ActivityDownloader
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityDownloader));
            this.splDogaDlCenter = new System.Windows.Forms.SplitContainer();
            this.splDogaDlCenter2 = new System.Windows.Forms.SplitContainer();
            this.btnBrowser = new Liplis.Control.CusCtlButton();
            this.lblNicoMessage = new Liplis.Control.CusCtlLabel();
            this.btnNicoCheck = new Liplis.Control.CusCtlButton();
            this.btnNicoDl = new Liplis.Control.CusCtlButton();
            this.btnNicoMp3 = new Liplis.Control.CusCtlButton();
            this.txtNicoUrl = new System.Windows.Forms.TextBox();
            this.cusCtlLabel10 = new Liplis.Control.CusCtlLabel();
            this.cusCtlGroupBox1 = new Liplis.Control.CusCtlGroupBox();
            this.wbNico = new System.Windows.Forms.WebBrowser();
            this.splDogaDlCenter3 = new System.Windows.Forms.SplitContainer();
            this.btnStop = new Liplis.Control.CusCtlButton();
            this.lblKey = new Liplis.Control.CusCtlLabel();
            this.cusCtlPanel1 = new Liplis.Control.CusCtlPanel();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.rdoKey = new System.Windows.Forms.RadioButton();
            this.rdoTag = new System.Windows.Forms.RadioButton();
            this.lblSort = new Liplis.Control.CusCtlLabel();
            this.cboSort = new System.Windows.Forms.ComboBox();
            this.btnNicoSearchWord = new Liplis.Control.CusCtlButton();
            this.txtNicoSearchWord = new System.Windows.Forms.TextBox();
            this.cusCtlLabel11 = new Liplis.Control.CusCtlLabel();
            this.dgvNicoSearch = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDoga = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMp3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBrw = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvDownloader = new System.Windows.Forms.DataGridView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splDogaDlCenter.Panel1.SuspendLayout();
            this.splDogaDlCenter.Panel2.SuspendLayout();
            this.splDogaDlCenter.SuspendLayout();
            this.splDogaDlCenter2.Panel1.SuspendLayout();
            this.splDogaDlCenter2.Panel2.SuspendLayout();
            this.splDogaDlCenter2.SuspendLayout();
            this.cusCtlGroupBox1.SuspendLayout();
            this.splDogaDlCenter3.Panel1.SuspendLayout();
            this.splDogaDlCenter3.Panel2.SuspendLayout();
            this.splDogaDlCenter3.SuspendLayout();
            this.cusCtlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNicoSearch)).BeginInit();
            this.cms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownloader)).BeginInit();
            this.SuspendLayout();
            // 
            // splDogaDlCenter
            // 
            this.splDogaDlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splDogaDlCenter.Location = new System.Drawing.Point(0, 0);
            this.splDogaDlCenter.Name = "splDogaDlCenter";
            this.splDogaDlCenter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splDogaDlCenter.Panel1
            // 
            this.splDogaDlCenter.Panel1.Controls.Add(this.splDogaDlCenter2);
            // 
            // splDogaDlCenter.Panel2
            // 
            this.splDogaDlCenter.Panel2.Controls.Add(this.dgvDownloader);
            this.splDogaDlCenter.Size = new System.Drawing.Size(1008, 730);
            this.splDogaDlCenter.SplitterDistance = 371;
            this.splDogaDlCenter.TabIndex = 1;
            // 
            // splDogaDlCenter2
            // 
            this.splDogaDlCenter2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splDogaDlCenter2.Location = new System.Drawing.Point(0, 0);
            this.splDogaDlCenter2.Name = "splDogaDlCenter2";
            // 
            // splDogaDlCenter2.Panel1
            // 
            this.splDogaDlCenter2.Panel1.Controls.Add(this.btnBrowser);
            this.splDogaDlCenter2.Panel1.Controls.Add(this.lblNicoMessage);
            this.splDogaDlCenter2.Panel1.Controls.Add(this.btnNicoCheck);
            this.splDogaDlCenter2.Panel1.Controls.Add(this.btnNicoDl);
            this.splDogaDlCenter2.Panel1.Controls.Add(this.btnNicoMp3);
            this.splDogaDlCenter2.Panel1.Controls.Add(this.txtNicoUrl);
            this.splDogaDlCenter2.Panel1.Controls.Add(this.cusCtlLabel10);
            this.splDogaDlCenter2.Panel1.Controls.Add(this.cusCtlGroupBox1);
            // 
            // splDogaDlCenter2.Panel2
            // 
            this.splDogaDlCenter2.Panel2.Controls.Add(this.splDogaDlCenter3);
            this.splDogaDlCenter2.Size = new System.Drawing.Size(1008, 371);
            this.splDogaDlCenter2.SplitterDistance = 354;
            this.splDogaDlCenter2.TabIndex = 0;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Image = global::Liplis.Properties.Resources.Brw;
            this.btnBrowser.Location = new System.Drawing.Point(243, 32);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(48, 48);
            this.btnBrowser.TabIndex = 9;
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // lblNicoMessage
            // 
            this.lblNicoMessage.Location = new System.Drawing.Point(6, 323);
            this.lblNicoMessage.Name = "lblNicoMessage";
            this.lblNicoMessage.Size = new System.Drawing.Size(339, 48);
            this.lblNicoMessage.TabIndex = 8;
            // 
            // btnNicoCheck
            // 
            this.btnNicoCheck.Image = global::Liplis.Properties.Resources.check;
            this.btnNicoCheck.Location = new System.Drawing.Point(297, 32);
            this.btnNicoCheck.Name = "btnNicoCheck";
            this.btnNicoCheck.Size = new System.Drawing.Size(48, 48);
            this.btnNicoCheck.TabIndex = 7;
            this.btnNicoCheck.UseVisualStyleBackColor = true;
            this.btnNicoCheck.Click += new System.EventHandler(this.btnNicoCheck_Click);
            // 
            // btnNicoDl
            // 
            this.btnNicoDl.Image = global::Liplis.Properties.Resources.douga;
            this.btnNicoDl.Location = new System.Drawing.Point(7, 32);
            this.btnNicoDl.Name = "btnNicoDl";
            this.btnNicoDl.Size = new System.Drawing.Size(48, 48);
            this.btnNicoDl.TabIndex = 6;
            this.btnNicoDl.UseVisualStyleBackColor = true;
            this.btnNicoDl.Click += new System.EventHandler(this.btnNicoDl_Click);
            // 
            // btnNicoMp3
            // 
            this.btnNicoMp3.Image = global::Liplis.Properties.Resources.mp3Dl;
            this.btnNicoMp3.Location = new System.Drawing.Point(61, 32);
            this.btnNicoMp3.Name = "btnNicoMp3";
            this.btnNicoMp3.Size = new System.Drawing.Size(48, 48);
            this.btnNicoMp3.TabIndex = 5;
            this.btnNicoMp3.UseVisualStyleBackColor = true;
            this.btnNicoMp3.Click += new System.EventHandler(this.btnNicoMp3_Click);
            // 
            // txtNicoUrl
            // 
            this.txtNicoUrl.Location = new System.Drawing.Point(102, 7);
            this.txtNicoUrl.Name = "txtNicoUrl";
            this.txtNicoUrl.Size = new System.Drawing.Size(243, 19);
            this.txtNicoUrl.TabIndex = 4;
            // 
            // cusCtlLabel10
            // 
            this.cusCtlLabel10.AutoSize = true;
            this.cusCtlLabel10.Location = new System.Drawing.Point(5, 11);
            this.cusCtlLabel10.Name = "cusCtlLabel10";
            this.cusCtlLabel10.Size = new System.Drawing.Size(90, 12);
            this.cusCtlLabel10.TabIndex = 3;
            this.cusCtlLabel10.Text = "URLまたは動画ID";
            // 
            // cusCtlGroupBox1
            // 
            this.cusCtlGroupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cusCtlGroupBox1.Controls.Add(this.wbNico);
            this.cusCtlGroupBox1.Location = new System.Drawing.Point(3, 86);
            this.cusCtlGroupBox1.Name = "cusCtlGroupBox1";
            this.cusCtlGroupBox1.Size = new System.Drawing.Size(342, 234);
            this.cusCtlGroupBox1.TabIndex = 2;
            this.cusCtlGroupBox1.TabStop = false;
            this.cusCtlGroupBox1.Text = "動画情報";
            // 
            // wbNico
            // 
            this.wbNico.Location = new System.Drawing.Point(11, 15);
            this.wbNico.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbNico.Name = "wbNico";
            this.wbNico.ScrollBarsEnabled = false;
            this.wbNico.Size = new System.Drawing.Size(322, 213);
            this.wbNico.TabIndex = 2;
            this.wbNico.WebBrowserShortcutsEnabled = false;
            // 
            // splDogaDlCenter3
            // 
            this.splDogaDlCenter3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splDogaDlCenter3.Location = new System.Drawing.Point(0, 0);
            this.splDogaDlCenter3.Name = "splDogaDlCenter3";
            this.splDogaDlCenter3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splDogaDlCenter3.Panel1
            // 
            this.splDogaDlCenter3.Panel1.Controls.Add(this.btnStop);
            this.splDogaDlCenter3.Panel1.Controls.Add(this.lblKey);
            this.splDogaDlCenter3.Panel1.Controls.Add(this.cusCtlPanel1);
            this.splDogaDlCenter3.Panel1.Controls.Add(this.lblSort);
            this.splDogaDlCenter3.Panel1.Controls.Add(this.cboSort);
            this.splDogaDlCenter3.Panel1.Controls.Add(this.btnNicoSearchWord);
            this.splDogaDlCenter3.Panel1.Controls.Add(this.txtNicoSearchWord);
            this.splDogaDlCenter3.Panel1.Controls.Add(this.cusCtlLabel11);
            // 
            // splDogaDlCenter3.Panel2
            // 
            this.splDogaDlCenter3.Panel2.Controls.Add(this.dgvNicoSearch);
            this.splDogaDlCenter3.Size = new System.Drawing.Size(650, 371);
            this.splDogaDlCenter3.SplitterDistance = 65;
            this.splDogaDlCenter3.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(558, 35);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(89, 23);
            this.btnStop.TabIndex = 20;
            this.btnStop.Text = "検索停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(5, 40);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(53, 12);
            this.lblKey.TabIndex = 19;
            this.lblKey.Text = "検索方法";
            // 
            // cusCtlPanel1
            // 
            this.cusCtlPanel1.Controls.Add(this.rdoAll);
            this.cusCtlPanel1.Controls.Add(this.rdoKey);
            this.cusCtlPanel1.Controls.Add(this.rdoTag);
            this.cusCtlPanel1.Location = new System.Drawing.Point(63, 32);
            this.cusCtlPanel1.Name = "cusCtlPanel1";
            this.cusCtlPanel1.Size = new System.Drawing.Size(222, 24);
            this.cusCtlPanel1.TabIndex = 18;
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Location = new System.Drawing.Point(158, 4);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(47, 16);
            this.rdoAll.TabIndex = 18;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "両方";
            this.rdoAll.UseVisualStyleBackColor = true;
            this.rdoAll.CheckedChanged += new System.EventHandler(this.rdoAll_CheckedChanged);
            // 
            // rdoKey
            // 
            this.rdoKey.AutoSize = true;
            this.rdoKey.Location = new System.Drawing.Point(5, 4);
            this.rdoKey.Name = "rdoKey";
            this.rdoKey.Size = new System.Drawing.Size(71, 16);
            this.rdoKey.TabIndex = 16;
            this.rdoKey.TabStop = true;
            this.rdoKey.Text = "キーワード";
            this.rdoKey.UseVisualStyleBackColor = true;
            this.rdoKey.CheckedChanged += new System.EventHandler(this.rdoKey_CheckedChanged);
            // 
            // rdoTag
            // 
            this.rdoTag.AutoSize = true;
            this.rdoTag.Location = new System.Drawing.Point(99, 4);
            this.rdoTag.Name = "rdoTag";
            this.rdoTag.Size = new System.Drawing.Size(40, 16);
            this.rdoTag.TabIndex = 17;
            this.rdoTag.TabStop = true;
            this.rdoTag.Text = "タグ";
            this.rdoTag.UseVisualStyleBackColor = true;
            this.rdoTag.CheckedChanged += new System.EventHandler(this.rdoTag_CheckedChanged);
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new System.Drawing.Point(315, 39);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(32, 12);
            this.lblSort.TabIndex = 17;
            this.lblSort.Text = "ソート";
            // 
            // cboSort
            // 
            this.cboSort.FormattingEnabled = true;
            this.cboSort.Items.AddRange(new object[] {
            "コメントが新しい順",
            "コメントが古い順",
            "再生数が多い順",
            "再生数が少ない順",
            "コメント数が多い順",
            "コメント数が少ない順",
            "マイリスト数が多い順",
            "マイリスト数が少ない順",
            "投稿日が新しい順",
            "投稿日が古い順",
            "再生時間が長い順",
            "再生時間が短い順"});
            this.cboSort.Location = new System.Drawing.Point(353, 35);
            this.cboSort.Name = "cboSort";
            this.cboSort.Size = new System.Drawing.Size(173, 20);
            this.cboSort.TabIndex = 16;
            this.cboSort.SelectedIndexChanged += new System.EventHandler(this.cboSort_SelectedIndexChanged);
            // 
            // btnNicoSearchWord
            // 
            this.btnNicoSearchWord.Location = new System.Drawing.Point(558, 4);
            this.btnNicoSearchWord.Name = "btnNicoSearchWord";
            this.btnNicoSearchWord.Size = new System.Drawing.Size(89, 23);
            this.btnNicoSearchWord.TabIndex = 12;
            this.btnNicoSearchWord.Text = "キーワード検索";
            this.btnNicoSearchWord.UseVisualStyleBackColor = true;
            this.btnNicoSearchWord.Click += new System.EventHandler(this.btnNicoSearchWord_Click);
            // 
            // txtNicoSearchWord
            // 
            this.txtNicoSearchWord.Location = new System.Drawing.Point(66, 6);
            this.txtNicoSearchWord.Name = "txtNicoSearchWord";
            this.txtNicoSearchWord.Size = new System.Drawing.Size(486, 19);
            this.txtNicoSearchWord.TabIndex = 9;
            // 
            // cusCtlLabel11
            // 
            this.cusCtlLabel11.AutoSize = true;
            this.cusCtlLabel11.Location = new System.Drawing.Point(5, 9);
            this.cusCtlLabel11.Name = "cusCtlLabel11";
            this.cusCtlLabel11.Size = new System.Drawing.Size(57, 12);
            this.cusCtlLabel11.TabIndex = 8;
            this.cusCtlLabel11.Text = "検索ワード";
            // 
            // dgvNicoSearch
            // 
            this.dgvNicoSearch.AllowDrop = true;
            this.dgvNicoSearch.AllowUserToAddRows = false;
            this.dgvNicoSearch.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNicoSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNicoSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNicoSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column1});
            this.dgvNicoSearch.ContextMenuStrip = this.cms;
            this.dgvNicoSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNicoSearch.Location = new System.Drawing.Point(0, 0);
            this.dgvNicoSearch.MultiSelect = false;
            this.dgvNicoSearch.Name = "dgvNicoSearch";
            this.dgvNicoSearch.RowTemplate.Height = 21;
            this.dgvNicoSearch.Size = new System.Drawing.Size(650, 302);
            this.dgvNicoSearch.TabIndex = 2;
            this.dgvNicoSearch.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNicoSearch_CellClick);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "ID";
            this.Column2.Name = "Column2";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "タイトル";
            this.Column1.Name = "Column1";
            this.Column1.Width = 500;
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDoga,
            this.tsmiMp3,
            this.tsmiBrw});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(173, 92);
            // 
            // tsmiDoga
            // 
            this.tsmiDoga.Name = "tsmiDoga";
            this.tsmiDoga.Size = new System.Drawing.Size(172, 22);
            this.tsmiDoga.Text = "動画ダウンロード";
            this.tsmiDoga.Click += new System.EventHandler(this.tsmiDoga_Click);
            // 
            // tsmiMp3
            // 
            this.tsmiMp3.Name = "tsmiMp3";
            this.tsmiMp3.Size = new System.Drawing.Size(172, 22);
            this.tsmiMp3.Text = "MP3ダウンロード";
            this.tsmiMp3.Click += new System.EventHandler(this.tsmiMp3_Click);
            // 
            // tsmiBrw
            // 
            this.tsmiBrw.Name = "tsmiBrw";
            this.tsmiBrw.Size = new System.Drawing.Size(172, 22);
            this.tsmiBrw.Text = "ブラウザで表示";
            this.tsmiBrw.Click += new System.EventHandler(this.tsmiBrw_Click);
            // 
            // dgvDownloader
            // 
            this.dgvDownloader.AllowDrop = true;
            this.dgvDownloader.AllowUserToAddRows = false;
            this.dgvDownloader.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDownloader.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDownloader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDownloader.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvDownloader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDownloader.Location = new System.Drawing.Point(0, 0);
            this.dgvDownloader.MultiSelect = false;
            this.dgvDownloader.Name = "dgvDownloader";
            this.dgvDownloader.RowTemplate.Height = 21;
            this.dgvDownloader.Size = new System.Drawing.Size(1008, 355);
            this.dgvDownloader.TabIndex = 3;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "種類";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "ファイル名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.Width = 690;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "サイズ";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // ActivityDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.splDogaDlCenter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActivityDownloader";
            this.Text = "動画ダウンロードツール";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivityDownloader_FormClosing);
            this.splDogaDlCenter.Panel1.ResumeLayout(false);
            this.splDogaDlCenter.Panel2.ResumeLayout(false);
            this.splDogaDlCenter.ResumeLayout(false);
            this.splDogaDlCenter2.Panel1.ResumeLayout(false);
            this.splDogaDlCenter2.Panel1.PerformLayout();
            this.splDogaDlCenter2.Panel2.ResumeLayout(false);
            this.splDogaDlCenter2.ResumeLayout(false);
            this.cusCtlGroupBox1.ResumeLayout(false);
            this.splDogaDlCenter3.Panel1.ResumeLayout(false);
            this.splDogaDlCenter3.Panel1.PerformLayout();
            this.splDogaDlCenter3.Panel2.ResumeLayout(false);
            this.splDogaDlCenter3.ResumeLayout(false);
            this.cusCtlPanel1.ResumeLayout(false);
            this.cusCtlPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNicoSearch)).EndInit();
            this.cms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDownloader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splDogaDlCenter;
        private System.Windows.Forms.SplitContainer splDogaDlCenter2;
        private Control.CusCtlLabel lblNicoMessage;
        private Control.CusCtlButton btnNicoCheck;
        private Control.CusCtlButton btnNicoDl;
        private Control.CusCtlButton btnNicoMp3;
        private System.Windows.Forms.TextBox txtNicoUrl;
        private Control.CusCtlLabel cusCtlLabel10;
        private Control.CusCtlGroupBox cusCtlGroupBox1;
        private System.Windows.Forms.WebBrowser wbNico;
        private System.Windows.Forms.SplitContainer splDogaDlCenter3;
        private Control.CusCtlButton btnNicoSearchWord;
        private System.Windows.Forms.TextBox txtNicoSearchWord;
        private Control.CusCtlLabel cusCtlLabel11;
        private System.Windows.Forms.DataGridView dgvNicoSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        internal System.Windows.Forms.DataGridView dgvDownloader;
        private Control.CusCtlLabel lblSort;
        private System.Windows.Forms.ComboBox cboSort;
        private Control.CusCtlLabel lblKey;
        private Control.CusCtlPanel cusCtlPanel1;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.RadioButton rdoKey;
        private System.Windows.Forms.RadioButton rdoTag;
        private Control.CusCtlButton btnStop;
        private Control.CusCtlButton btnBrowser;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem tsmiDoga;
        private System.Windows.Forms.ToolStripMenuItem tsmiMp3;
        private System.Windows.Forms.ToolStripMenuItem tsmiBrw;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}