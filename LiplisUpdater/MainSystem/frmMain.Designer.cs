namespace LiplisUpdater
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.ヘルプToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.prg = new System.Windows.Forms.ProgressBar();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.grpNew = new System.Windows.Forms.GroupBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblNewVer = new System.Windows.Forms.Label();
            this.lblNewVerTitle = new System.Windows.Forms.Label();
            this.grpNow = new System.Windows.Forms.GroupBox();
            this.lblNowVer = new System.Windows.Forms.Label();
            this.lblNowVerTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.grpNew.SuspendLayout();
            this.grpNow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fsmiFile,
            this.ヘルプToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(464, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fsmiFile
            // 
            this.fsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEnd});
            this.fsmiFile.Name = "fsmiFile";
            this.fsmiFile.Size = new System.Drawing.Size(85, 22);
            this.fsmiFile.Text = "ファイル(&F)";
            // 
            // tsmiEnd
            // 
            this.tsmiEnd.Name = "tsmiEnd";
            this.tsmiEnd.Size = new System.Drawing.Size(100, 22);
            this.tsmiEnd.Text = "終了";
            this.tsmiEnd.Click += new System.EventHandler(this.tsmiEnd_Click);
            // 
            // ヘルプToolStripMenuItem
            // 
            this.ヘルプToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiVersion});
            this.ヘルプToolStripMenuItem.Name = "ヘルプToolStripMenuItem";
            this.ヘルプToolStripMenuItem.Size = new System.Drawing.Size(66, 22);
            this.ヘルプToolStripMenuItem.Text = "ヘルプ(&)";
            // 
            // tsmiVersion
            // 
            this.tsmiVersion.Name = "tsmiVersion";
            this.tsmiVersion.Size = new System.Drawing.Size(263, 22);
            this.tsmiVersion.Text = "Liplis Update Toolバージョン情報";
            this.tsmiVersion.Click += new System.EventHandler(this.tsmiVersion_Click);
            // 
            // prg
            // 
            this.prg.Location = new System.Drawing.Point(12, 248);
            this.prg.Name = "prg";
            this.prg.Size = new System.Drawing.Size(440, 23);
            this.prg.TabIndex = 2;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnUpdate.Location = new System.Drawing.Point(12, 176);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(440, 39);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "アップデート";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblFile
            // 
            this.lblFile.Location = new System.Drawing.Point(12, 226);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(440, 16);
            this.lblFile.TabIndex = 5;
            this.lblFile.Text = "ファイル表示";
            // 
            // grpNew
            // 
            this.grpNew.Controls.Add(this.btnCheck);
            this.grpNew.Controls.Add(this.lblNewVer);
            this.grpNew.Controls.Add(this.lblNewVerTitle);
            this.grpNew.Location = new System.Drawing.Point(12, 122);
            this.grpNew.Name = "grpNew";
            this.grpNew.Size = new System.Drawing.Size(440, 48);
            this.grpNew.TabIndex = 15;
            this.grpNew.TabStop = false;
            this.grpNew.Text = "最新版情報";
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(6, 17);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(64, 23);
            this.btnCheck.TabIndex = 17;
            this.btnCheck.Text = "確認";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lblNewVer
            // 
            this.lblNewVer.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNewVer.Location = new System.Drawing.Point(186, 17);
            this.lblNewVer.Name = "lblNewVer";
            this.lblNewVer.Size = new System.Drawing.Size(238, 24);
            this.lblNewVer.TabIndex = 16;
            this.lblNewVer.Text = "XXXXXXX";
            // 
            // lblNewVerTitle
            // 
            this.lblNewVerTitle.AutoSize = true;
            this.lblNewVerTitle.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNewVerTitle.Location = new System.Drawing.Point(79, 17);
            this.lblNewVerTitle.Name = "lblNewVerTitle";
            this.lblNewVerTitle.Size = new System.Drawing.Size(100, 24);
            this.lblNewVerTitle.TabIndex = 15;
            this.lblNewVerTitle.Text = "バージョン";
            // 
            // grpNow
            // 
            this.grpNow.Controls.Add(this.lblNowVer);
            this.grpNow.Controls.Add(this.lblNowVerTitle);
            this.grpNow.Controls.Add(this.lblTitle);
            this.grpNow.Controls.Add(this.picIcon);
            this.grpNow.Location = new System.Drawing.Point(12, 29);
            this.grpNow.Name = "grpNow";
            this.grpNow.Size = new System.Drawing.Size(440, 87);
            this.grpNow.TabIndex = 16;
            this.grpNow.TabStop = false;
            this.grpNow.Text = "インストール情報";
            // 
            // lblNowVer
            // 
            this.lblNowVer.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNowVer.Location = new System.Drawing.Point(186, 50);
            this.lblNowVer.Name = "lblNowVer";
            this.lblNowVer.Size = new System.Drawing.Size(238, 24);
            this.lblNowVer.TabIndex = 14;
            this.lblNowVer.Text = "XXXXXXX";
            // 
            // lblNowVerTitle
            // 
            this.lblNowVerTitle.AutoSize = true;
            this.lblNowVerTitle.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNowVerTitle.Location = new System.Drawing.Point(79, 50);
            this.lblNowVerTitle.Name = "lblNowVerTitle";
            this.lblNowVerTitle.Size = new System.Drawing.Size(100, 24);
            this.lblNowVerTitle.TabIndex = 13;
            this.lblNowVerTitle.Text = "バージョン";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.Location = new System.Drawing.Point(79, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(154, 24);
            this.lblTitle.TabIndex = 12;
            this.lblTitle.Text = "Liplis Windows";
            // 
            // picIcon
            // 
            this.picIcon.Image = ((System.Drawing.Image)(resources.GetObject("picIcon.Image")));
            this.picIcon.Location = new System.Drawing.Point(6, 14);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(64, 64);
            this.picIcon.TabIndex = 11;
            this.picIcon.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 283);
            this.Controls.Add(this.grpNow);
            this.Controls.Add(this.grpNew);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.prg);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "リプリス アップデートツール";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpNew.ResumeLayout(false);
            this.grpNew.PerformLayout();
            this.grpNow.ResumeLayout(false);
            this.grpNow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiEnd;
        private System.Windows.Forms.ProgressBar prg;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.GroupBox grpNew;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblNewVer;
        private System.Windows.Forms.Label lblNewVerTitle;
        private System.Windows.Forms.GroupBox grpNow;
        private System.Windows.Forms.Label lblNowVer;
        private System.Windows.Forms.Label lblNowVerTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.ToolStripMenuItem ヘルプToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiVersion;
    }
}

