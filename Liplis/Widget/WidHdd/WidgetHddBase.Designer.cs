namespace Liplis.Widget.WidHdd
{
    partial class WidgetHddBase
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
            this.lblWidHddVal = new Liplis.Control.CusCtlLabel();
            this.prg = new System.Windows.Forms.ProgressBar();
            this.lblWidHddTitle = new Liplis.Control.CusCtlLabel();
            this.picWidCpuIco = new Liplis.Control.CusCtlPictureBox();
            this.g.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWidCpuIco)).BeginInit();
            this.SuspendLayout();
            // 
            // timSystemInfo
            // 
            this.timSystemInfo.Interval = 10000;
            this.timSystemInfo.Tick += new System.EventHandler(this.timSystemInfo_Tick);
            // 
            // g
            // 
            this.g.BackColor = System.Drawing.Color.Transparent;
            this.g.Controls.Add(this.lblWidHddVal);
            this.g.Controls.Add(this.prg);
            this.g.Controls.Add(this.lblWidHddTitle);
            this.g.Controls.Add(this.picWidCpuIco);
            this.g.Location = new System.Drawing.Point(0, -6);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(160, 46);
            this.g.TabIndex = 1;
            this.g.TabStop = false;
            // 
            // lblWidHddVal
            // 
            this.lblWidHddVal.Location = new System.Drawing.Point(37, 9);
            this.lblWidHddVal.Name = "lblWidHddVal";
            this.lblWidHddVal.Size = new System.Drawing.Size(117, 12);
            this.lblWidHddVal.TabIndex = 17;
            this.lblWidHddVal.Text = "999999GB/999999GB";
            this.lblWidHddVal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.lblWidHddVal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // prg
            // 
            this.prg.Location = new System.Drawing.Point(36, 27);
            this.prg.Name = "prg";
            this.prg.Size = new System.Drawing.Size(120, 12);
            this.prg.TabIndex = 30;
            this.prg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.prg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // lblWidHddTitle
            // 
            this.lblWidHddTitle.AutoSize = true;
            this.lblWidHddTitle.Location = new System.Drawing.Point(3, 9);
            this.lblWidHddTitle.Name = "lblWidHddTitle";
            this.lblWidHddTitle.Size = new System.Drawing.Size(21, 12);
            this.lblWidHddTitle.TabIndex = 16;
            this.lblWidHddTitle.Text = "C:\\";
            this.lblWidHddTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.lblWidHddTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // picWidCpuIco
            // 
            this.picWidCpuIco.BackColor = System.Drawing.Color.Transparent;
            this.picWidCpuIco.Image = global::Liplis.Properties.Resources.widHdd24;
            this.picWidCpuIco.Location = new System.Drawing.Point(5, 21);
            this.picWidCpuIco.Name = "picWidCpuIco";
            this.picWidCpuIco.Size = new System.Drawing.Size(24, 24);
            this.picWidCpuIco.TabIndex = 29;
            this.picWidCpuIco.TabStop = false;
            this.picWidCpuIco.Click += new System.EventHandler(this.picWidCpuIco_Click);
            this.picWidCpuIco.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.picWidCpuIco.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            // 
            // WidgetHddBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 40);
            this.Controls.Add(this.g);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetHddBase";
            this.Text = "Form2";
            this.g.ResumeLayout(false);
            this.g.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWidCpuIco)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox g;
        private Control.CusCtlLabel lblWidHddVal;
        private Control.CusCtlLabel lblWidHddTitle;
        private System.Windows.Forms.Timer timSystemInfo;
        private Control.CusCtlPictureBox picWidCpuIco;
        private System.Windows.Forms.ProgressBar prg;

    }
}