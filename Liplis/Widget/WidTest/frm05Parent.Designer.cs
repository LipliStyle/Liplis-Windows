namespace Liplis.Widget
{
    partial class frm05Parent
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
            this.bar = new System.Windows.Forms.Label();
            this.btnClose = new Liplis.Control.CusCtlButton();
            this.SuspendLayout();
            // 
            // bar
            // 
            this.bar.BackColor = System.Drawing.Color.RoyalBlue;
            this.bar.ForeColor = System.Drawing.Color.Black;
            this.bar.Location = new System.Drawing.Point(3, 4);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(154, 10);
            this.bar.TabIndex = 2;
            this.bar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bar_MouseDown);
            this.bar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bar_MouseMove);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(132, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 10);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "cusCtlButton1";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // frm05Parent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(160, 80);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.bar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm05Parent";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmParent_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label bar;
        private Control.CusCtlButton btnClose;
    }
}