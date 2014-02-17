namespace Liplis.Activity
{
    partial class ActivityRssRegistAddCat
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
            this.btnCatAdd = new System.Windows.Forms.Button();
            this.txtCat = new System.Windows.Forms.TextBox();
            this.lblCat = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCatAdd
            // 
            this.btnCatAdd.Location = new System.Drawing.Point(8, 31);
            this.btnCatAdd.Name = "btnCatAdd";
            this.btnCatAdd.Size = new System.Drawing.Size(86, 23);
            this.btnCatAdd.TabIndex = 13;
            this.btnCatAdd.Text = "追加";
            this.btnCatAdd.UseVisualStyleBackColor = true;
            this.btnCatAdd.Click += new System.EventHandler(this.btnCatAdd_Click);
            // 
            // txtCat
            // 
            this.txtCat.BackColor = System.Drawing.SystemColors.Info;
            this.txtCat.Location = new System.Drawing.Point(52, 6);
            this.txtCat.Name = "txtCat";
            this.txtCat.Size = new System.Drawing.Size(137, 19);
            this.txtCat.TabIndex = 12;
            this.txtCat.TextChanged += new System.EventHandler(this.txtCat_TextChanged);
            this.txtCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCat_KeyDown);
            // 
            // lblCat
            // 
            this.lblCat.AutoSize = true;
            this.lblCat.Location = new System.Drawing.Point(7, 9);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(39, 12);
            this.lblCat.TabIndex = 11;
            this.lblCat.Text = "カテゴリ";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(102, 31);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ActivityRssRegistAddCat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 62);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCatAdd);
            this.Controls.Add(this.txtCat);
            this.Controls.Add(this.lblCat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ActivityRssRegistAddCat";
            this.Text = "カテゴリ追加";
            this.Load += new System.EventHandler(this.ActivityRssRegistAddCat_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ActivityRssRegistAddCat_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCatAdd;
        private System.Windows.Forms.TextBox txtCat;
        private System.Windows.Forms.Label lblCat;
        private System.Windows.Forms.Button btnCancel;
    }
}