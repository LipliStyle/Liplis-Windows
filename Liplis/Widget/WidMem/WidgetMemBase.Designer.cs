namespace Liplis.Widget.WidMem
{
    partial class WidgetMemBase
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
            this.lblWidMemRateSl = new Liplis.Control.CusCtlLabel();
            this.lblWidMemRateMax = new Liplis.Control.CusCtlLabel();
            this.lblWidMemRateVal = new Liplis.Control.CusCtlLabel();
            this.lblWidMemTitle = new Liplis.Control.CusCtlLabel();
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
            this.g.Controls.Add(this.lblWidMemRateSl);
            this.g.Controls.Add(this.lblWidMemRateMax);
            this.g.Controls.Add(this.lblWidMemRateVal);
            this.g.Controls.Add(this.lblWidMemTitle);
            this.g.Controls.Add(this.picWidCpuIco);
            this.g.Location = new System.Drawing.Point(0, -6);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(160, 46);
            this.g.TabIndex = 1;
            this.g.TabStop = false;
            // 
            // lblWidMemRateSl
            // 
            this.lblWidMemRateSl.Location = new System.Drawing.Point(80, 9);
            this.lblWidMemRateSl.Name = "lblWidMemRateSl";
            this.lblWidMemRateSl.Size = new System.Drawing.Size(10, 12);
            this.lblWidMemRateSl.TabIndex = 31;
            this.lblWidMemRateSl.Text = "/";
            this.lblWidMemRateSl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.lblWidMemRateSl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // lblWidMemRateMax
            // 
            this.lblWidMemRateMax.Location = new System.Drawing.Point(91, 9);
            this.lblWidMemRateMax.Name = "lblWidMemRateMax";
            this.lblWidMemRateMax.Size = new System.Drawing.Size(47, 12);
            this.lblWidMemRateMax.TabIndex = 30;
            this.lblWidMemRateMax.Text = "999.99%";
            this.lblWidMemRateMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWidMemRateMax.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.lblWidMemRateMax.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // lblWidMemRateVal
            // 
            this.lblWidMemRateVal.Location = new System.Drawing.Point(35, 9);
            this.lblWidMemRateVal.Name = "lblWidMemRateVal";
            this.lblWidMemRateVal.Size = new System.Drawing.Size(47, 12);
            this.lblWidMemRateVal.TabIndex = 17;
            this.lblWidMemRateVal.Text = "999.99%";
            this.lblWidMemRateVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWidMemRateVal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.lblWidMemRateVal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // lblWidMemTitle
            // 
            this.lblWidMemTitle.AutoSize = true;
            this.lblWidMemTitle.Location = new System.Drawing.Point(3, 9);
            this.lblWidMemTitle.Name = "lblWidMemTitle";
            this.lblWidMemTitle.Size = new System.Drawing.Size(29, 12);
            this.lblWidMemTitle.TabIndex = 16;
            this.lblWidMemTitle.Text = "Mem";
            this.lblWidMemTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.lblWidMemTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // picWidCpuIco
            // 
            this.picWidCpuIco.BackColor = System.Drawing.Color.Transparent;
            this.picWidCpuIco.Image = global::Liplis.Properties.Resources.widMem24;
            this.picWidCpuIco.Location = new System.Drawing.Point(5, 21);
            this.picWidCpuIco.Name = "picWidCpuIco";
            this.picWidCpuIco.Size = new System.Drawing.Size(24, 24);
            this.picWidCpuIco.TabIndex = 29;
            this.picWidCpuIco.TabStop = false;
            this.picWidCpuIco.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.picWidCpuIco.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // WidgetMemBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 40);
            this.Controls.Add(this.g);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetMemBase";
            this.Text = "Form2";
            this.g.ResumeLayout(false);
            this.g.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWidCpuIco)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox g;
        private Control.CusCtlLabel lblWidMemRateVal;
        private Control.CusCtlLabel lblWidMemTitle;
        private System.Windows.Forms.Timer timSystemInfo;
        private Control.CusCtlPictureBox picWidCpuIco;
        private Control.CusCtlLabel lblWidMemRateSl;
        private Control.CusCtlLabel lblWidMemRateMax;

    }
}