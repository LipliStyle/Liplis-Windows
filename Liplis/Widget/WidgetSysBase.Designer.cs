namespace Liplis.Widget
{
    partial class WidgetSysBase
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
            this.lblCpuRate = new System.Windows.Forms.Label();
            this.prgCpuRate = new System.Windows.Forms.ProgressBar();
            this.lblMem = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.prgMem = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSentByte = new System.Windows.Forms.Label();
            this.lblReceiveByte = new System.Windows.Forms.Label();
            this.prgReciv = new System.Windows.Forms.ProgressBar();
            this.prgSend = new System.Windows.Forms.ProgressBar();
            this.timSystemInfo = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblCpuRate
            // 
            this.lblCpuRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCpuRate.AutoSize = true;
            this.lblCpuRate.BackColor = System.Drawing.Color.Transparent;
            this.lblCpuRate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCpuRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblCpuRate.Location = new System.Drawing.Point(52, 16);
            this.lblCpuRate.Name = "lblCpuRate";
            this.lblCpuRate.Size = new System.Drawing.Size(43, 16);
            this.lblCpuRate.TabIndex = 37;
            this.lblCpuRate.Text = "9.99%";
            this.lblCpuRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // prgCpuRate
            // 
            this.prgCpuRate.ForeColor = System.Drawing.Color.Lime;
            this.prgCpuRate.Location = new System.Drawing.Point(8, 34);
            this.prgCpuRate.Name = "prgCpuRate";
            this.prgCpuRate.Size = new System.Drawing.Size(225, 15);
            this.prgCpuRate.TabIndex = 38;
            // 
            // lblMem
            // 
            this.lblMem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMem.AutoSize = true;
            this.lblMem.BackColor = System.Drawing.Color.Transparent;
            this.lblMem.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblMem.Location = new System.Drawing.Point(52, 58);
            this.lblMem.Name = "lblMem";
            this.lblMem.Size = new System.Drawing.Size(92, 16);
            this.lblMem.TabIndex = 33;
            this.lblMem.Text = "9.99G/9.99G";
            this.lblMem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label6.Location = new System.Drawing.Point(7, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 32;
            this.label6.Text = "Mem";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label4.Location = new System.Drawing.Point(7, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 45;
            this.label4.Text = "Send";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label5.Location = new System.Drawing.Point(7, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 16);
            this.label5.TabIndex = 31;
            this.label5.Text = "CPU";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label3.Location = new System.Drawing.Point(100, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 44;
            this.label3.Text = "kB/s";
            // 
            // prgMem
            // 
            this.prgMem.ForeColor = System.Drawing.Color.Lime;
            this.prgMem.Location = new System.Drawing.Point(8, 77);
            this.prgMem.Name = "prgMem";
            this.prgMem.Size = new System.Drawing.Size(225, 15);
            this.prgMem.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label2.Location = new System.Drawing.Point(100, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 43;
            this.label2.Text = "kB/s";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.Location = new System.Drawing.Point(7, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 42;
            this.label1.Text = "Recv";
            // 
            // lblSentByte
            // 
            this.lblSentByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSentByte.AutoSize = true;
            this.lblSentByte.BackColor = System.Drawing.Color.Transparent;
            this.lblSentByte.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSentByte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblSentByte.Location = new System.Drawing.Point(52, 145);
            this.lblSentByte.Name = "lblSentByte";
            this.lblSentByte.Size = new System.Drawing.Size(48, 16);
            this.lblSentByte.TabIndex = 41;
            this.lblSentByte.Text = "XXXX";
            // 
            // lblReceiveByte
            // 
            this.lblReceiveByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReceiveByte.AutoSize = true;
            this.lblReceiveByte.BackColor = System.Drawing.Color.Transparent;
            this.lblReceiveByte.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblReceiveByte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblReceiveByte.Location = new System.Drawing.Point(52, 101);
            this.lblReceiveByte.Name = "lblReceiveByte";
            this.lblReceiveByte.Size = new System.Drawing.Size(48, 16);
            this.lblReceiveByte.TabIndex = 40;
            this.lblReceiveByte.Text = "XXXX";
            // 
            // prgReciv
            // 
            this.prgReciv.ForeColor = System.Drawing.Color.Lime;
            this.prgReciv.Location = new System.Drawing.Point(10, 121);
            this.prgReciv.Name = "prgReciv";
            this.prgReciv.Size = new System.Drawing.Size(223, 15);
            this.prgReciv.TabIndex = 35;
            // 
            // prgSend
            // 
            this.prgSend.ForeColor = System.Drawing.Color.Lime;
            this.prgSend.Location = new System.Drawing.Point(8, 164);
            this.prgSend.Name = "prgSend";
            this.prgSend.Size = new System.Drawing.Size(225, 15);
            this.prgSend.TabIndex = 34;
            // 
            // timSystemInfo
            // 
            this.timSystemInfo.Interval = 1000;
            this.timSystemInfo.Tick += new System.EventHandler(this.timSystemInfo_Tick);
            // 
            // WidgetSysBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 180);
            this.Controls.Add(this.lblCpuRate);
            this.Controls.Add(this.prgCpuRate);
            this.Controls.Add(this.lblMem);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.prgMem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSentByte);
            this.Controls.Add(this.lblReceiveByte);
            this.Controls.Add(this.prgReciv);
            this.Controls.Add(this.prgSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetSysBase";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCpuRate;
        private System.Windows.Forms.ProgressBar prgCpuRate;
        private System.Windows.Forms.Label lblMem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar prgMem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSentByte;
        private System.Windows.Forms.Label lblReceiveByte;
        private System.Windows.Forms.ProgressBar prgReciv;
        private System.Windows.Forms.ProgressBar prgSend;
        private System.Windows.Forms.Timer timSystemInfo;

    }
}