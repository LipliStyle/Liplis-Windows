namespace Liplis.Widget.WidCpu
{
    partial class WidgetCpuBase
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
            this.timSystemInfo = new System.Windows.Forms.Timer(this.components);
            this.g = new System.Windows.Forms.GroupBox();
            this.lblWidCpuRate = new Liplis.Control.CusCtlLabel();
            this.prg = new System.Windows.Forms.ProgressBar();
            this.lblWidCpuTitle = new Liplis.Control.CusCtlLabel();
            this.picWidCpuIco = new Liplis.Control.CusCtlPictureBox();
            this.g.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWidCpuIco)).BeginInit();
            this.SuspendLayout();
            // 
            // timSystemInfo
            // 
            this.timSystemInfo.Interval = 1000;
            this.timSystemInfo.Tick += new System.EventHandler(this.timSystemInfo_Tick);
            // 
            // g
            // 
            this.g.BackColor = System.Drawing.Color.Transparent;
            this.g.Controls.Add(this.lblWidCpuRate);
            this.g.Controls.Add(this.prg);
            this.g.Controls.Add(this.lblWidCpuTitle);
            this.g.Controls.Add(this.picWidCpuIco);
            this.g.Location = new System.Drawing.Point(0, -6);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(160, 46);
            this.g.TabIndex = 1;
            this.g.TabStop = false;
            // 
            // lblWidCpuRate
            // 
            this.lblWidCpuRate.Location = new System.Drawing.Point(37, 9);
            this.lblWidCpuRate.Name = "lblWidCpuRate";
            this.lblWidCpuRate.Size = new System.Drawing.Size(46, 12);
            this.lblWidCpuRate.TabIndex = 17;
            this.lblWidCpuRate.Text = "999.99%";
            this.lblWidCpuRate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblWidCpuRate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.lblWidCpuRate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // prg
            // 
            this.prg.Location = new System.Drawing.Point(83, 9);
            this.prg.Name = "prg";
            this.prg.Size = new System.Drawing.Size(75, 12);
            this.prg.TabIndex = 30;
            this.prg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.prg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // lblWidCpuTitle
            // 
            this.lblWidCpuTitle.AutoSize = true;
            this.lblWidCpuTitle.Location = new System.Drawing.Point(3, 9);
            this.lblWidCpuTitle.Name = "lblWidCpuTitle";
            this.lblWidCpuTitle.Size = new System.Drawing.Size(28, 12);
            this.lblWidCpuTitle.TabIndex = 16;
            this.lblWidCpuTitle.Text = "CPU";
            this.lblWidCpuTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.lblWidCpuTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // picWidCpuIco
            // 
            this.picWidCpuIco.BackColor = System.Drawing.Color.Transparent;
            this.picWidCpuIco.Image = global::Liplis.Properties.Resources.widCpu24;
            this.picWidCpuIco.Location = new System.Drawing.Point(5, 21);
            this.picWidCpuIco.Name = "picWidCpuIco";
            this.picWidCpuIco.Size = new System.Drawing.Size(24, 24);
            this.picWidCpuIco.TabIndex = 29;
            this.picWidCpuIco.TabStop = false;
            this.picWidCpuIco.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.picWidCpuIco.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // WidgetCpuBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 40);
            this.Controls.Add(this.g);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetCpuBase";
            this.Text = "Form2";
            this.g.ResumeLayout(false);
            this.g.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWidCpuIco)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox g;
        private Control.CusCtlLabel lblWidCpuRate;
        private Control.CusCtlLabel lblWidCpuTitle;
        private System.Windows.Forms.Timer timSystemInfo;
        private Control.CusCtlPictureBox picWidCpuIco;
        private System.Windows.Forms.ProgressBar prg;

    }
}