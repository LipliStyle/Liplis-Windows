namespace Liplis.MainSystem
{
    partial class LiplisTaskBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiplisTaskBar));
            this.icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.liplisContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmSleep = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMinimize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOutrangeRecovery = new System.Windows.Forms.ToolStripMenuItem();
            this.liplisContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // icon
            // 
            this.icon.ContextMenuStrip = this.liplisContext;
            this.icon.Icon = ((System.Drawing.Icon)(resources.GetObject("icon.Icon")));
            this.icon.Text = "Liplis";
            this.icon.Visible = true;
            // 
            // liplisContext
            // 
            this.liplisContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmOutrangeRecovery,
            this.tsmSleep,
            this.tsmMinimize,
            this.tsmLog,
            this.tsmSetting,
            this.tsmEnd});
            this.liplisContext.Name = "liplisContext";
            this.liplisContext.Size = new System.Drawing.Size(209, 158);
            // 
            // tsmSleep
            // 
            this.tsmSleep.Name = "tsmSleep";
            this.tsmSleep.Size = new System.Drawing.Size(208, 22);
            this.tsmSleep.Text = "お休み/復帰";
            this.tsmSleep.Click += new System.EventHandler(this.tsmSleep_Click);
            // 
            // tsmMinimize
            // 
            this.tsmMinimize.Name = "tsmMinimize";
            this.tsmMinimize.Size = new System.Drawing.Size(208, 22);
            this.tsmMinimize.Text = "最小化/戻す";
            this.tsmMinimize.Click += new System.EventHandler(this.tsmMinimize_Click);
            // 
            // tsmLog
            // 
            this.tsmLog.Name = "tsmLog";
            this.tsmLog.Size = new System.Drawing.Size(208, 22);
            this.tsmLog.Text = "ログ";
            this.tsmLog.Click += new System.EventHandler(this.tsmLog_Click);
            // 
            // tsmSetting
            // 
            this.tsmSetting.Name = "tsmSetting";
            this.tsmSetting.Size = new System.Drawing.Size(208, 22);
            this.tsmSetting.Text = "設定";
            this.tsmSetting.Click += new System.EventHandler(this.tsmSetting_Click);
            // 
            // tsmEnd
            // 
            this.tsmEnd.Name = "tsmEnd";
            this.tsmEnd.Size = new System.Drawing.Size(208, 22);
            this.tsmEnd.Text = "終了";
            this.tsmEnd.Click += new System.EventHandler(this.tsmEnd_Click);
            // 
            // tsmOutrangeRecovery
            // 
            this.tsmOutrangeRecovery.Name = "tsmOutrangeRecovery";
            this.tsmOutrangeRecovery.Size = new System.Drawing.Size(208, 22);
            this.tsmOutrangeRecovery.Text = "メインウインドウに復帰";
            this.tsmOutrangeRecovery.Click += new System.EventHandler(this.tsmOutrangeRecovery_Click);
            // 
            // LiplisTaskBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LiplisTaskBar";
            this.Text = "Liplis2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LiplisTaskBar_FormClosing);
            this.liplisContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon icon;
        private System.Windows.Forms.ContextMenuStrip liplisContext;
        private System.Windows.Forms.ToolStripMenuItem tsmMinimize;
        private System.Windows.Forms.ToolStripMenuItem tsmSetting;
        private System.Windows.Forms.ToolStripMenuItem tsmSleep;
        private System.Windows.Forms.ToolStripMenuItem tsmEnd;
        private System.Windows.Forms.ToolStripMenuItem tsmLog;
        private System.Windows.Forms.ToolStripMenuItem tsmOutrangeRecovery;
    }
}