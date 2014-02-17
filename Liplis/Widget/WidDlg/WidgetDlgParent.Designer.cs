namespace Liplis.Widget.WidDlg
{
    partial class WidgetDlgParent
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
            this.SuspendLayout();
            // 
            // WidgetDlgParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(160, 40);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetDlgParent";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmParent_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WidgetCpuParent_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WidgetCpuParent_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion



    }
}