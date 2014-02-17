namespace Liplis.Activity
{
    partial class ActivityTwitterActivation
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
            this.lblSetsumei = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSendPin = new System.Windows.Forms.Button();
            this.txtPin = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblSetsumei
            // 
            this.lblSetsumei.Location = new System.Drawing.Point(0, 9);
            this.lblSetsumei.Name = "lblSetsumei";
            this.lblSetsumei.Size = new System.Drawing.Size(284, 35);
            this.lblSetsumei.TabIndex = 34;
            this.lblSetsumei.Text = "表示されたブラウザで連携処理を行なって下さい。\r\nPINコードが表示されたら、テキストボックスに入力してください。";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(152, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 23);
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSendPin
            // 
            this.btnSendPin.Location = new System.Drawing.Point(12, 67);
            this.btnSendPin.Name = "btnSendPin";
            this.btnSendPin.Size = new System.Drawing.Size(120, 23);
            this.btnSendPin.TabIndex = 32;
            this.btnSendPin.Text = "PINコード送信";
            this.btnSendPin.UseVisualStyleBackColor = true;
            this.btnSendPin.Click += new System.EventHandler(this.btnSendPin_Click);
            // 
            // txtPin
            // 
            this.txtPin.Location = new System.Drawing.Point(12, 39);
            this.txtPin.Name = "txtPin";
            this.txtPin.Size = new System.Drawing.Size(260, 19);
            this.txtPin.TabIndex = 30;
            this.txtPin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPin_KeyDown);
            // 
            // ActivityTwitterActivation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 99);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSendPin);
            this.Controls.Add(this.txtPin);
            this.Controls.Add(this.lblSetsumei);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ActivityTwitterActivation";
            this.Text = "ツイッター認証";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ActivityTwitterActivation_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSetsumei;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSendPin;
        private System.Windows.Forms.TextBox txtPin;
    }
}