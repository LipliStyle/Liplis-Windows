namespace Liplis.Widget.WidBrw
{
    partial class WidgetBrwParent
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
            this.g = new Liplis.Control.CusCtlGroupBox();
            this.wb = new Liplis.Control.CusCtlWebBrowser();
            this.timUpdate = new System.Windows.Forms.Timer(this.components);
            this.g.SuspendLayout();
            this.SuspendLayout();
            // 
            // g
            // 
            this.g.BackColor = System.Drawing.Color.Transparent;
            this.g.Controls.Add(this.wb);
            this.g.Location = new System.Drawing.Point(0, -6);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(160, 166);
            this.g.TabIndex = 0;
            this.g.TabStop = false;
            this.g.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bar_MouseDown);
            this.g.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bar_MouseMove);
            // 
            // wb
            // 
            this.wb.Location = new System.Drawing.Point(4, 22);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.Size = new System.Drawing.Size(152, 140);
            this.wb.TabIndex = 3;
            // 
            // timUpdate
            // 
            this.timUpdate.Interval = 1000;
            this.timUpdate.Tick += new System.EventHandler(this.timUpdate_Tick);
            // 
            // WidgetBrowserParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(160, 160);
            this.Controls.Add(this.g);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetBrowserParent";
            this.Load += new System.EventHandler(this.frm12Parent_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WidgetRssParent_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bar_MouseMove);
            this.g.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Control.CusCtlGroupBox g;
        private Control.CusCtlWebBrowser wb;
        private System.Windows.Forms.Timer timUpdate;


    }
}