namespace Liplis.Widget
{
    partial class WidgetCpu02Base
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
            this.g = new System.Windows.Forms.GroupBox();
            this.cusCtlLabel1 = new Liplis.Control.CusCtlLabel();
            this.lblWidCpuTitle = new Liplis.Control.CusCtlLabel();
            this.timSystemInfo = new System.Windows.Forms.Timer(this.components);
            this.g.SuspendLayout();
            this.SuspendLayout();
            // 
            // g
            // 
            this.g.Controls.Add(this.cusCtlLabel1);
            this.g.Controls.Add(this.lblWidCpuTitle);
            this.g.Location = new System.Drawing.Point(0, -6);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(160, 46);
            this.g.TabIndex = 1;
            this.g.TabStop = false;
            // 
            // cusCtlLabel1
            // 
            this.cusCtlLabel1.AutoSize = true;
            this.cusCtlLabel1.Location = new System.Drawing.Point(36, 9);
            this.cusCtlLabel1.Name = "cusCtlLabel1";
            this.cusCtlLabel1.Size = new System.Drawing.Size(29, 12);
            this.cusCtlLabel1.TabIndex = 17;
            this.cusCtlLabel1.Text = "999%";
            // 
            // lblWidCpuTitle
            // 
            this.lblWidCpuTitle.AutoSize = true;
            this.lblWidCpuTitle.Location = new System.Drawing.Point(2, 9);
            this.lblWidCpuTitle.Name = "lblWidCpuTitle";
            this.lblWidCpuTitle.Size = new System.Drawing.Size(28, 12);
            this.lblWidCpuTitle.TabIndex = 16;
            this.lblWidCpuTitle.Text = "CPU";
            // 
            // timSystemInfo
            // 
            this.timSystemInfo.Interval = 1000;
            this.timSystemInfo.Tick += new System.EventHandler(this.timSystemInfo_Tick);
            // 
            // WidgetCpu02Base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 40);
            this.Controls.Add(this.g);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetCpu02Base";
            this.Text = "Form2";
            this.g.ResumeLayout(false);
            this.g.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox g;
        private Control.CusCtlLabel cusCtlLabel1;
        private Control.CusCtlLabel lblWidCpuTitle;
        private System.Windows.Forms.Timer timSystemInfo;

    }
}