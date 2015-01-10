namespace Liplis.Activity
{
    partial class ActivityChat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityChat));
            this.spl = new System.Windows.Forms.SplitContainer();
            this.flp = new Liplis.Control.CusCtlFlowLayoutPanel();
            this.txtTell = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.spl.Panel1.SuspendLayout();
            this.spl.Panel2.SuspendLayout();
            this.spl.SuspendLayout();
            this.SuspendLayout();
            // 
            // spl
            // 
            this.spl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spl.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spl.Location = new System.Drawing.Point(0, 0);
            this.spl.Name = "spl";
            this.spl.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spl.Panel1
            // 
            this.spl.Panel1.Controls.Add(this.flp);
            // 
            // spl.Panel2
            // 
            this.spl.Panel2.Controls.Add(this.txtTell);
            this.spl.Panel2.Controls.Add(this.btnSend);
            this.spl.Size = new System.Drawing.Size(480, 480);
            this.spl.SplitterDistance = 437;
            this.spl.TabIndex = 84;
            // 
            // flp
            // 
            this.flp.AutoScroll = true;
            this.flp.BackColor = System.Drawing.Color.Transparent;
            this.flp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flp.Location = new System.Drawing.Point(0, 0);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(480, 437);
            this.flp.TabIndex = 83;
            this.flp.WrapContents = false;
            // 
            // txtTell
            // 
            this.txtTell.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTell.Location = new System.Drawing.Point(60, 0);
            this.txtTell.Multiline = true;
            this.txtTell.Name = "txtTell";
            this.txtTell.Size = new System.Drawing.Size(420, 39);
            this.txtTell.TabIndex = 1;
            this.txtTell.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTell_KeyDown);
            this.txtTell.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTell_KeyUp);
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSend.Location = new System.Drawing.Point(0, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(60, 39);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "発言";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ActivityChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 480);
            this.Controls.Add(this.spl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActivityChat";
            this.Text = "ActivityChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivityLog_FormClosing);
            this.spl.Panel1.ResumeLayout(false);
            this.spl.Panel2.ResumeLayout(false);
            this.spl.Panel2.PerformLayout();
            this.spl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.SplitContainer spl;
        private Control.CusCtlFlowLayoutPanel flp;
        private System.Windows.Forms.TextBox txtTell;
        private System.Windows.Forms.Button btnSend;


    }
}