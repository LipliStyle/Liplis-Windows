namespace Liplis.Activity
{
    partial class ActivityRssRegist
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("カテゴリ");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityRssRegist));
            this.csmCat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCatAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCatFix = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCatDel = new System.Windows.Forms.ToolStripMenuItem();
            this.csmRss = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRssDel = new System.Windows.Forms.ToolStripMenuItem();
            this.spc = new System.Windows.Forms.SplitContainer();
            this.spc3 = new System.Windows.Forms.SplitContainer();
            this.tvRss = new System.Windows.Forms.TreeView();
            this.btnCatAdd = new System.Windows.Forms.Button();
            this.spc2 = new System.Windows.Forms.SplitContainer();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.txtSelCat = new System.Windows.Forms.TextBox();
            this.lblSelCat = new System.Windows.Forms.Label();
            this.btnRegist = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.rSSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.csmCat.SuspendLayout();
            this.csmRss.SuspendLayout();
            this.spc.Panel1.SuspendLayout();
            this.spc.Panel2.SuspendLayout();
            this.spc.SuspendLayout();
            this.spc3.Panel1.SuspendLayout();
            this.spc3.Panel2.SuspendLayout();
            this.spc3.SuspendLayout();
            this.spc2.Panel1.SuspendLayout();
            this.spc2.Panel2.SuspendLayout();
            this.spc2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // csmCat
            // 
            this.csmCat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCatAdd,
            this.tsmiCatFix,
            this.tsmiCatDel});
            this.csmCat.Name = "csmCat";
            this.csmCat.Size = new System.Drawing.Size(153, 70);
            // 
            // tsmiCatAdd
            // 
            this.tsmiCatAdd.Name = "tsmiCatAdd";
            this.tsmiCatAdd.Size = new System.Drawing.Size(152, 22);
            this.tsmiCatAdd.Text = "カテゴリ 追加";
            this.tsmiCatAdd.Click += new System.EventHandler(this.tsmiCatAdd_Click);
            // 
            // tsmiCatFix
            // 
            this.tsmiCatFix.Name = "tsmiCatFix";
            this.tsmiCatFix.Size = new System.Drawing.Size(152, 22);
            this.tsmiCatFix.Text = "カテゴリ 修正";
            this.tsmiCatFix.Click += new System.EventHandler(this.tsmiCatFix_Click);
            // 
            // tsmiCatDel
            // 
            this.tsmiCatDel.Name = "tsmiCatDel";
            this.tsmiCatDel.Size = new System.Drawing.Size(152, 22);
            this.tsmiCatDel.Text = "カテゴリ 削除";
            this.tsmiCatDel.Click += new System.EventHandler(this.tsmiCatDel_Click);
            // 
            // csmRss
            // 
            this.csmRss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRssDel});
            this.csmRss.Name = "csmRss";
            this.csmRss.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmiRssDel
            // 
            this.tsmiRssDel.Name = "tsmiRssDel";
            this.tsmiRssDel.Size = new System.Drawing.Size(124, 22);
            this.tsmiRssDel.Text = "RSS削除";
            this.tsmiRssDel.Click += new System.EventHandler(this.tsmiRssDel_Click);
            // 
            // spc
            // 
            this.spc.BackColor = System.Drawing.Color.White;
            this.spc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spc.Location = new System.Drawing.Point(0, 26);
            this.spc.Name = "spc";
            // 
            // spc.Panel1
            // 
            this.spc.Panel1.Controls.Add(this.spc3);
            // 
            // spc.Panel2
            // 
            this.spc.Panel2.Controls.Add(this.spc2);
            this.spc.Size = new System.Drawing.Size(784, 454);
            this.spc.SplitterDistance = 195;
            this.spc.TabIndex = 3;
            // 
            // spc3
            // 
            this.spc3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc3.Location = new System.Drawing.Point(0, 0);
            this.spc3.Name = "spc3";
            this.spc3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc3.Panel1
            // 
            this.spc3.Panel1.Controls.Add(this.tvRss);
            // 
            // spc3.Panel2
            // 
            this.spc3.Panel2.Controls.Add(this.btnCatAdd);
            this.spc3.Size = new System.Drawing.Size(195, 454);
            this.spc3.SplitterDistance = 419;
            this.spc3.TabIndex = 4;
            // 
            // tvRss
            // 
            this.tvRss.AllowDrop = true;
            this.tvRss.BackColor = System.Drawing.Color.White;
            this.tvRss.ContextMenuStrip = this.csmCat;
            this.tvRss.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRss.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tvRss.HideSelection = false;
            this.tvRss.Location = new System.Drawing.Point(0, 0);
            this.tvRss.Name = "tvRss";
            treeNode1.Name = "nodeCat";
            treeNode1.Text = "カテゴリ";
            this.tvRss.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvRss.Size = new System.Drawing.Size(195, 419);
            this.tvRss.TabIndex = 4;
            this.tvRss.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRss_AfterSelect);
            this.tvRss.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvRss_MouseDown);
            // 
            // btnCatAdd
            // 
            this.btnCatAdd.Location = new System.Drawing.Point(3, 3);
            this.btnCatAdd.Name = "btnCatAdd";
            this.btnCatAdd.Size = new System.Drawing.Size(189, 23);
            this.btnCatAdd.TabIndex = 8;
            this.btnCatAdd.Text = "カテゴリ追加";
            this.btnCatAdd.UseVisualStyleBackColor = true;
            this.btnCatAdd.Click += new System.EventHandler(this.btnCatAdd_Click);
            // 
            // spc2
            // 
            this.spc2.BackColor = System.Drawing.Color.White;
            this.spc2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc2.IsSplitterFixed = true;
            this.spc2.Location = new System.Drawing.Point(0, 0);
            this.spc2.Name = "spc2";
            this.spc2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc2.Panel1
            // 
            this.spc2.Panel1.Controls.Add(this.dgv);
            // 
            // spc2.Panel2
            // 
            this.spc2.Panel2.BackColor = System.Drawing.Color.White;
            this.spc2.Panel2.Controls.Add(this.chkDelete);
            this.spc2.Panel2.Controls.Add(this.txtSelCat);
            this.spc2.Panel2.Controls.Add(this.lblSelCat);
            this.spc2.Panel2.Controls.Add(this.btnRegist);
            this.spc2.Panel2.Controls.Add(this.txtUrl);
            this.spc2.Panel2.Controls.Add(this.lblUrl);
            this.spc2.Size = new System.Drawing.Size(585, 454);
            this.spc2.SplitterDistance = 386;
            this.spc2.TabIndex = 0;
            // 
            // dgv
            // 
            this.dgv.AllowDrop = true;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TITLE,
            this.URL});
            this.dgv.ContextMenuStrip = this.csmRss;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 21;
            this.dgv.Size = new System.Drawing.Size(585, 386);
            this.dgv.TabIndex = 0;
            this.dgv.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseDown);
            this.dgv.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgv_UserDeletingRow);
            this.dgv.DragDrop += new System.Windows.Forms.DragEventHandler(this.ActivityRss_DragDrop);
            this.dgv.DragEnter += new System.Windows.Forms.DragEventHandler(this.ActivityRss_DragEnter);
            // 
            // TITLE
            // 
            this.TITLE.HeaderText = "TITLE";
            this.TITLE.Name = "TITLE";
            this.TITLE.Width = 200;
            // 
            // URL
            // 
            this.URL.HeaderText = "URL";
            this.URL.Name = "URL";
            this.URL.Width = 180;
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(327, 8);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(111, 16);
            this.chkDelete.TabIndex = 9;
            this.chkDelete.Text = "削除時、確認する";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // txtSelCat
            // 
            this.txtSelCat.BackColor = System.Drawing.SystemColors.Info;
            this.txtSelCat.Location = new System.Drawing.Point(76, 8);
            this.txtSelCat.Name = "txtSelCat";
            this.txtSelCat.ReadOnly = true;
            this.txtSelCat.Size = new System.Drawing.Size(198, 19);
            this.txtSelCat.TabIndex = 8;
            // 
            // lblSelCat
            // 
            this.lblSelCat.AutoSize = true;
            this.lblSelCat.Location = new System.Drawing.Point(7, 11);
            this.lblSelCat.Name = "lblSelCat";
            this.lblSelCat.Size = new System.Drawing.Size(63, 12);
            this.lblSelCat.TabIndex = 7;
            this.lblSelCat.Text = "選択カテゴリ";
            // 
            // btnRegist
            // 
            this.btnRegist.Location = new System.Drawing.Point(372, 27);
            this.btnRegist.Name = "btnRegist";
            this.btnRegist.Size = new System.Drawing.Size(63, 31);
            this.btnRegist.TabIndex = 6;
            this.btnRegist.Text = "登録";
            this.btnRegist.UseVisualStyleBackColor = true;
            this.btnRegist.Click += new System.EventHandler(this.btnRegist_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.BackColor = System.Drawing.SystemColors.Info;
            this.txtUrl.Location = new System.Drawing.Point(76, 33);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(286, 19);
            this.txtUrl.TabIndex = 4;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(7, 36);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(27, 12);
            this.lblUrl.TabIndex = 1;
            this.lblUrl.Text = "URL";
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rSSToolStripMenuItem,
            this.twitterToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(784, 26);
            this.msMenu.TabIndex = 6;
            this.msMenu.Text = "Twitter";
            // 
            // rSSToolStripMenuItem
            // 
            this.rSSToolStripMenuItem.BackColor = System.Drawing.Color.Azure;
            this.rSSToolStripMenuItem.Name = "rSSToolStripMenuItem";
            this.rSSToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.rSSToolStripMenuItem.Text = "RSS";
            // 
            // twitterToolStripMenuItem
            // 
            this.twitterToolStripMenuItem.Name = "twitterToolStripMenuItem";
            this.twitterToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
            this.twitterToolStripMenuItem.Text = "Twitter";
            // 
            // ActivityRssRegist
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 480);
            this.Controls.Add(this.spc);
            this.Controls.Add(this.msMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMenu;
            this.Name = "ActivityRssRegist";
            this.Text = "話題管理ツール";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActivityRss_FormClosing);
            this.Load += new System.EventHandler(this.ActivityRss_Load);
            this.SizeChanged += new System.EventHandler(this.ActivityRssRegist_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ActivityRss_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ActivityRss_DragEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ActivityRss_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ActivityRss_MouseMove);
            this.csmCat.ResumeLayout(false);
            this.csmRss.ResumeLayout(false);
            this.spc.Panel1.ResumeLayout(false);
            this.spc.Panel2.ResumeLayout(false);
            this.spc.ResumeLayout(false);
            this.spc3.Panel1.ResumeLayout(false);
            this.spc3.Panel2.ResumeLayout(false);
            this.spc3.ResumeLayout(false);
            this.spc2.Panel1.ResumeLayout(false);
            this.spc2.Panel2.ResumeLayout(false);
            this.spc2.Panel2.PerformLayout();
            this.spc2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer spc;
        private System.Windows.Forms.SplitContainer spc2;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnRegist;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn TITLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn URL;
        private System.Windows.Forms.SplitContainer spc3;
        private System.Windows.Forms.TreeView tvRss;
        private System.Windows.Forms.Button btnCatAdd;
        private System.Windows.Forms.TextBox txtSelCat;
        private System.Windows.Forms.Label lblSelCat;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ContextMenuStrip csmCat;
        private System.Windows.Forms.ToolStripMenuItem tsmiCatFix;
        private System.Windows.Forms.ToolStripMenuItem tsmiCatDel;
        private System.Windows.Forms.ContextMenuStrip csmRss;
        private System.Windows.Forms.ToolStripMenuItem tsmiRssDel;
        private System.Windows.Forms.ToolStripMenuItem rSSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiCatAdd;
        private System.Windows.Forms.ToolStripMenuItem twitterToolStripMenuItem;
    }
}