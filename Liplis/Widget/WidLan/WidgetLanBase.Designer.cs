namespace Liplis.Widget.WidLan
{
    partial class WidgetLanBase
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
            this.lblWidMemTitle = new Liplis.Control.CusCtlLabel();
            this.lblWidLanRateSnd = new Liplis.Control.CusCtlLabel();
            this.lblWidLanRateRcv = new Liplis.Control.CusCtlLabel();
            this.g.SuspendLayout();
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
            this.g.Controls.Add(this.lblWidLanRateSnd);
            this.g.Controls.Add(this.lblWidLanRateRcv);
            this.g.Controls.Add(this.lblWidMemTitle);
            this.g.Location = new System.Drawing.Point(0, -6);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(160, 46);
            this.g.TabIndex = 1;
            this.g.TabStop = false;
            // 
            // lblWidMemTitle
            // 
            this.lblWidMemTitle.AutoSize = true;
            this.lblWidMemTitle.Location = new System.Drawing.Point(3, 9);
            this.lblWidMemTitle.Name = "lblWidMemTitle";
            this.lblWidMemTitle.Size = new System.Drawing.Size(23, 12);
            this.lblWidMemTitle.TabIndex = 16;
            this.lblWidMemTitle.Text = "Lan";
            // 
            // lblWidLanRateSnd
            // 
            this.lblWidLanRateSnd.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblWidLanRateSnd.Location = new System.Drawing.Point(3, 22);
            this.lblWidLanRateSnd.Name = "lblWidLanRateSnd";
            this.lblWidLanRateSnd.Size = new System.Drawing.Size(47, 12);
            this.lblWidLanRateSnd.TabIndex = 34;
            this.lblWidLanRateSnd.Text = "999.99KB";
            this.lblWidLanRateSnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWidLanRateRcv
            // 
            this.lblWidLanRateRcv.BackColor = System.Drawing.Color.Transparent;
            this.lblWidLanRateRcv.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblWidLanRateRcv.Location = new System.Drawing.Point(3, 34);
            this.lblWidLanRateRcv.Name = "lblWidLanRateRcv";
            this.lblWidLanRateRcv.Size = new System.Drawing.Size(47, 12);
            this.lblWidLanRateRcv.TabIndex = 33;
            this.lblWidLanRateRcv.Text = "999.99KB";
            this.lblWidLanRateRcv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // WidgetLanBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 40);
            this.Controls.Add(this.g);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetLanBase";
            this.Text = "Form2";
            this.g.ResumeLayout(false);
            this.g.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox g;
        private Control.CusCtlLabel lblWidMemTitle;
        private System.Windows.Forms.Timer timSystemInfo;
        private Control.CusCtlLabel lblWidLanRateSnd;
        private Control.CusCtlLabel lblWidLanRateRcv;

    }
}