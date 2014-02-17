namespace Liplis.Widget
{
    partial class WidgetBaseBase
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
            this.pnlClose = new Liplis.Control.CusCtlPanel();
            this.SuspendLayout();
            // 
            // pnlClose
            // 
            this.pnlClose.BackgroundImage = global::Liplis.Properties.Resources.btnClose;
            this.pnlClose.Location = new System.Drawing.Point(220, 4);
            this.pnlClose.Name = "pnlClose";
            this.pnlClose.Size = new System.Drawing.Size(16, 16);
            this.pnlClose.TabIndex = 7;
            // 
            // WidgetBaseBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 180);
            this.Controls.Add(this.pnlClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetBaseBase";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private Control.CusCtlPanel pnlClose;

    }
}