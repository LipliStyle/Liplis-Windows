namespace Liplis.Activity
{
    partial class ActivityTalkMini
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
            this.lnkLblMsg = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lnkLblMsg
            // 
            this.lnkLblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lnkLblMsg.Location = new System.Drawing.Point(4, 4);
            this.lnkLblMsg.Name = "lnkLblMsg";
            this.lnkLblMsg.Size = new System.Drawing.Size(192, 76);
            this.lnkLblMsg.TabIndex = 0;
            this.lnkLblMsg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblMsg_LinkClicked);
            // 
            // ActivityTalkMini
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Liplis.Properties.Resources.windowMini;
            this.ClientSize = new System.Drawing.Size(200, 85);
            this.Controls.Add(this.lnkLblMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ActivityTalkMini";
            this.Text = "ActivityTalkMini";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivityTalkMini_FormClosing);
            this.Load += new System.EventHandler(this.ActivityTalkMini_Load);
            this.MouseEnter += new System.EventHandler(this.ActivityTalkMini_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkLblMsg;
    }
}