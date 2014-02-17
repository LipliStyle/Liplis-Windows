namespace Liplis.Widget.WidBrowser
{
    partial class WidgetBrowser11Parent
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
            this.btnClose = new Liplis.Control.CusCtlButton();
            this.wb = new System.Windows.Forms.WebBrowser();
            this.timUpdate = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(211, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 10);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "cusCtlButton1";
            this.btnClose.UseVisualStyleBackColor = false;

            // 
            // wb
            // 
            this.wb.Location = new System.Drawing.Point(5, 20);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.ScrollBarsEnabled = false;
            this.wb.Size = new System.Drawing.Size(231, 156);
            this.wb.TabIndex = 4;
            // 
            // timUpdate
            // 
            this.timUpdate.Tick += new System.EventHandler(this.timUpdate_Tick);
            // 
            // WidgetBrowser11Parent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(240, 180);
            this.Controls.Add(this.wb);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetBrowser11Parent";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmParent_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Control.CusCtlButton btnClose;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.Timer timUpdate;
    }
}