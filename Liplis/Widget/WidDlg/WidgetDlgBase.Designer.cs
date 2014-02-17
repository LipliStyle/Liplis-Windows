namespace Liplis.Widget.WidDlg
{
    partial class WidgetDlgBase
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
            this.g = new System.Windows.Forms.GroupBox();
            this.lblWidDlgRate = new Liplis.Control.CusCtlLabel();
            this.prg = new System.Windows.Forms.ProgressBar();
            this.lblWidDlgTitle = new Liplis.Control.CusCtlLabel();
            this.g.SuspendLayout();
            this.SuspendLayout();
            // 
            // g
            // 
            this.g.BackColor = System.Drawing.Color.Transparent;
            this.g.Controls.Add(this.lblWidDlgRate);
            this.g.Controls.Add(this.prg);
            this.g.Controls.Add(this.lblWidDlgTitle);
            this.g.Location = new System.Drawing.Point(0, -6);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(160, 46);
            this.g.TabIndex = 1;
            this.g.TabStop = false;
            // 
            // lblWidDlgRate
            // 
            this.lblWidDlgRate.Location = new System.Drawing.Point(4, 29);
            this.lblWidDlgRate.Name = "lblWidDlgRate";
            this.lblWidDlgRate.Size = new System.Drawing.Size(46, 12);
            this.lblWidDlgRate.TabIndex = 17;
            this.lblWidDlgRate.Text = "999.99%";
            this.lblWidDlgRate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblWidDlgRate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.lblWidDlgRate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // prg
            // 
            this.prg.Location = new System.Drawing.Point(56, 29);
            this.prg.Name = "prg";
            this.prg.Size = new System.Drawing.Size(98, 12);
            this.prg.TabIndex = 30;
            this.prg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.prg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // lblWidDlgTitle
            // 
            this.lblWidDlgTitle.Location = new System.Drawing.Point(5, 11);
            this.lblWidDlgTitle.Name = "lblWidDlgTitle";
            this.lblWidDlgTitle.Size = new System.Drawing.Size(149, 12);
            this.lblWidDlgTitle.TabIndex = 16;
            this.lblWidDlgTitle.Text = "XXXXXXXXXX";
            this.lblWidDlgTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.lblWidDlgTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // WidgetDlgBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 40);
            this.Controls.Add(this.g);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetDlgBase";
            this.Text = "Form2";
            this.g.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox g;
        private Control.CusCtlLabel lblWidDlgRate;
        private Control.CusCtlLabel lblWidDlgTitle;
        private System.Windows.Forms.ProgressBar prg;

    }
}