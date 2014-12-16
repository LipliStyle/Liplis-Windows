namespace Liplis.Activity
{
    partial class ActivityTell
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
            this.lstLog = new System.Windows.Forms.ListBox();
            this.pnl = new System.Windows.Forms.Panel();
            this.trc = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.spl = new System.Windows.Forms.SplitContainer();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.pnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trc)).BeginInit();
            this.spl.Panel1.SuspendLayout();
            this.spl.Panel2.SuspendLayout();
            this.spl.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 12;
            this.lstLog.Location = new System.Drawing.Point(0, 19);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(475, 87);
            this.lstLog.TabIndex = 1;
            this.lstLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ActivityTell_MouseDown);
            this.lstLog.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActivityTell_MouseMove);
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.trc);
            this.pnl.Controls.Add(this.label1);
            this.pnl.Controls.Add(this.btnClose);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(475, 19);
            this.pnl.TabIndex = 3;
            this.pnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ActivityTell_MouseDown);
            this.pnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActivityTell_MouseMove);
            // 
            // trc
            // 
            this.trc.AutoSize = false;
            this.trc.Location = new System.Drawing.Point(340, 0);
            this.trc.Maximum = 100;
            this.trc.Name = "trc";
            this.trc.Size = new System.Drawing.Size(104, 22);
            this.trc.TabIndex = 3;
            this.trc.Scroll += new System.EventHandler(this.trc_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Liplis会話ウインドウ";
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Location = new System.Drawing.Point(452, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 19);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // spl
            // 
            this.spl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spl.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spl.Location = new System.Drawing.Point(0, 106);
            this.spl.Name = "spl";
            // 
            // spl.Panel1
            // 
            this.spl.Panel1.Controls.Add(this.btnSend);
            // 
            // spl.Panel2
            // 
            this.spl.Panel2.Controls.Add(this.txtSendData);
            this.spl.Size = new System.Drawing.Size(475, 19);
            this.spl.SplitterDistance = 62;
            this.spl.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Location = new System.Drawing.Point(0, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(62, 19);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "送信";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtSendData
            // 
            this.txtSendData.BackColor = System.Drawing.Color.Gray;
            this.txtSendData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSendData.ForeColor = System.Drawing.Color.White;
            this.txtSendData.Location = new System.Drawing.Point(0, 0);
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(409, 19);
            this.txtSendData.TabIndex = 0;
            this.txtSendData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSendData_KeyDown);
            // 
            // ActivityTell
            // 
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(475, 125);
            this.ControlBox = false;
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.spl);
            this.Name = "ActivityTell";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivityTell_FormClosing);
            this.Load += new System.EventHandler(this.ActivityTell_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ActivityTell_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActivityTell_MouseMove);
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trc)).EndInit();
            this.spl.Panel1.ResumeLayout(false);
            this.spl.Panel2.ResumeLayout(false);
            this.spl.Panel2.PerformLayout();
            this.spl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.SplitContainer spl;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trc;

    }
}