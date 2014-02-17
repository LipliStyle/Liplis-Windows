namespace Liplis.Activity
{
    partial class ActivityVersion
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lbl = new System.Windows.Forms.Label();
            this.picVar = new System.Windows.Forms.PictureBox();
            this.Liplis = new System.Windows.Forms.Label();
            this.linkSite = new System.Windows.Forms.LinkLabel();
            this.linkMail = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnClose = new Liplis.Control.CusCtlButton();
            ((System.ComponentModel.ISupportInitialize)(this.picVar)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.Location = new System.Drawing.Point(21, 55);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(112, 24);
            this.lbl.TabIndex = 98;
            this.lbl.Text = "ご意見などありましたらご連絡下さい。";
            // 
            // picVar
            // 
            this.picVar.Image = global::Liplis.Properties.Resources.version;
            this.picVar.InitialImage = global::Liplis.Properties.Resources.version;
            this.picVar.Location = new System.Drawing.Point(178, 11);
            this.picVar.Name = "picVar";
            this.picVar.Size = new System.Drawing.Size(100, 140);
            this.picVar.TabIndex = 97;
            this.picVar.TabStop = false;
            // 
            // Liplis
            // 
            this.Liplis.AutoSize = true;
            this.Liplis.BackColor = System.Drawing.Color.Transparent;
            this.Liplis.ForeColor = System.Drawing.Color.Black;
            this.Liplis.Location = new System.Drawing.Point(6, 17);
            this.Liplis.Name = "Liplis";
            this.Liplis.Size = new System.Drawing.Size(32, 12);
            this.Liplis.TabIndex = 96;
            this.Liplis.Text = "Liplis";
            // 
            // linkSite
            // 
            this.linkSite.AutoSize = true;
            this.linkSite.BackColor = System.Drawing.Color.Transparent;
            this.linkSite.ForeColor = System.Drawing.Color.Black;
            this.linkSite.Location = new System.Drawing.Point(6, 130);
            this.linkSite.Name = "linkSite";
            this.linkSite.Size = new System.Drawing.Size(140, 12);
            this.linkSite.TabIndex = 95;
            this.linkSite.TabStop = true;
            this.linkSite.Text = "サイト : http://liplis.mine.nu";
            // 
            // linkMail
            // 
            this.linkMail.AutoSize = true;
            this.linkMail.BackColor = System.Drawing.Color.Transparent;
            this.linkMail.ForeColor = System.Drawing.Color.Black;
            this.linkMail.Location = new System.Drawing.Point(6, 107);
            this.linkMail.Name = "linkMail";
            this.linkMail.Size = new System.Drawing.Size(142, 12);
            this.linkMail.TabIndex = 94;
            this.linkMail.TabStop = true;
            this.linkMail.Text = "メール : sachin@ma.tnc.ne.jp";
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.ForeColor = System.Drawing.Color.Black;
            this.lblVersion.Location = new System.Drawing.Point(44, 16);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(121, 12);
            this.lblVersion.TabIndex = 93;
            this.lblVersion.Text = "Version";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(105, 155);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 99;
            this.btnClose.Text = "close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ActivityVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 182);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.picVar);
            this.Controls.Add(this.Liplis);
            this.Controls.Add(this.linkSite);
            this.Controls.Add(this.linkMail);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ActivityVersion";
            this.Text = "バージョン情報";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivitySetting_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picVar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.PictureBox picVar;
        private System.Windows.Forms.Label Liplis;
        private System.Windows.Forms.LinkLabel linkSite;
        private System.Windows.Forms.LinkLabel linkMail;
        private System.Windows.Forms.Label lblVersion;
        private Control.CusCtlButton btnClose;
    }
}