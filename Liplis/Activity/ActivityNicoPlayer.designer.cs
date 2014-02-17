namespace Liplis.Activity
{
    partial class ActivityNicoPlayer
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
            this.wbNico = new System.Windows.Forms.WebBrowser();
            this.timEnd = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wbNico
            // 
            this.wbNico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbNico.Location = new System.Drawing.Point(0, 0);
            this.wbNico.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbNico.Name = "wbNico";
            this.wbNico.ScrollBarsEnabled = false;
            this.wbNico.Size = new System.Drawing.Size(640, 480);
            this.wbNico.TabIndex = 0;
            this.wbNico.WebBrowserShortcutsEnabled = false;
            // 
            // timEnd
            // 
            this.timEnd.Tick += new System.EventHandler(this.timEnd_Tick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(599, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(42, 20);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ActivityNicoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.wbNico);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ActivityNicoPlayer";
            this.Text = "NicoBrowserWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivityNicoBrowser_FormClosing);
            this.Load += new System.EventHandler(this.NicoBrowserWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbNico;
        private System.Windows.Forms.Timer timEnd;
        private System.Windows.Forms.Button btnClose;
    }
}