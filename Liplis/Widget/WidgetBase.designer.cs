namespace Liplis.Widget
{
    partial class WidgetBase
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
            this.pfMoni = new System.Diagnostics.PerformanceCounter();
            this.pfMoniTimer = new System.Windows.Forms.Timer(this.components);
            this.derayTimer = new System.Windows.Forms.Timer(this.components);
            this.bar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pfMoni)).BeginInit();
            this.SuspendLayout();
            // 
            // pfMoni
            // 
            this.pfMoni.CategoryName = "Processor";
            this.pfMoni.CounterName = "% Processor Time";
            // 
            // bar
            // 
            this.bar.BackColor = System.Drawing.Color.RoyalBlue;
            this.bar.ForeColor = System.Drawing.Color.Black;
            this.bar.Location = new System.Drawing.Point(2, 3);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(233, 10);
            this.bar.TabIndex = 1;
            // 
            // WidgetBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(240, 180);
            this.Controls.Add(this.bar);
            this.ForeColor = System.Drawing.Color.Lime;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WidgetBase";
            this.Text = "SubWindow";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pfMoni)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Diagnostics.PerformanceCounter pfMoni;
        private System.Windows.Forms.Timer pfMoniTimer;
        private System.Windows.Forms.Timer derayTimer;
        private System.Windows.Forms.Label bar;
    }
}