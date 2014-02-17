namespace Liplis.Widget.WidSys
{
    partial class WidgetSysParent
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
            this.btnClose = new Liplis.Control.CusCtlButton();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(133, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 10);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "cusCtlButton1";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // WidgetSysParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(160, 160);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetSysParent";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.WidgetSysParent_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bar_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private Control.CusCtlButton btnClose;
    }
}