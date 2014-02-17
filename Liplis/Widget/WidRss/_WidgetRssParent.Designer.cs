namespace Liplis.Widget.WidRss
{
    partial class _WidgetRssParent
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
            this.g = new Liplis.Control.CusCtlGroupBox();
            this.SuspendLayout();
            // 
            // g
            // 
            this.g.BackColor = System.Drawing.Color.Transparent;
            this.g.Location = new System.Drawing.Point(0, -6);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(160, 166);
            this.g.TabIndex = 0;
            this.g.TabStop = false;
            this.g.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bar_MouseDown);
            this.g.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bar_MouseMove);
            // 
            // WidgetRssParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(160, 160);
            this.Controls.Add(this.g);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetRssParent";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frm12Parent_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bar_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private Control.CusCtlGroupBox g;


    }
}