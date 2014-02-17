namespace Liplis.Widget.WidRss
{
    partial class _WidgetRssBase
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
            this.pnlRss = new Liplis.Control.CusCtlFlowLayoutPanel();
            this.lblTitle = new Liplis.Control.CusCtlLabel();
            this.timUpdate = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pnlRss
            // 
            this.pnlRss.AutoScroll = true;
            this.pnlRss.BackColor = System.Drawing.Color.Transparent;
            this.pnlRss.Location = new System.Drawing.Point(4, 19);
            this.pnlRss.Name = "pnlRss";
            this.pnlRss.Size = new System.Drawing.Size(152, 140);
            this.pnlRss.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.Location = new System.Drawing.Point(5, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 12);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "title";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WidgetRss12Base_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WidgetRss12Base_MouseMove);
            // 
            // timUpdate
            // 
            this.timUpdate.Interval = 1000;
            this.timUpdate.Tick += new System.EventHandler(this.timUpdate_Tick);
            // 
            // WidgetRssBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(160, 160);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlRss);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetRssBase";
            this.Text = "Form2";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WidgetRss12Base_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WidgetRss12Base_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Control.CusCtlFlowLayoutPanel pnlRss;
        private Control.CusCtlLabel lblTitle;
        private System.Windows.Forms.Timer timUpdate;

    }
}