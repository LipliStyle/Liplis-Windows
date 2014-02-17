using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CircularMenu
{
	/// <summary>
	/// Summary description for MenuOptionImageEditorUI.
	/// </summary>
	public class MenuOptionImageEditorUI : System.Windows.Forms.Form
	{
        private System.Windows.Forms.GroupBox m_gbPreview;
        private System.Windows.Forms.PictureBox m_pbPreview;
        private System.Windows.Forms.Label m_lblBaseImage;
        private System.Windows.Forms.PictureBox m_picImage;
        private System.Windows.Forms.Button m_cmdBrowse;
        private System.Windows.Forms.Label m_lblTransparencyKey;
        private System.Windows.Forms.ComboBox m_cboTransparencyKey;
        private System.Windows.Forms.Label m_lblDropShadow;
        private System.Windows.Forms.CheckBox m_chkUseDs;
        private System.Windows.Forms.Button m_cmdSettings;
        private System.Windows.Forms.Label m_lblMaxOpacity;
        private System.Windows.Forms.TrackBar m_tbMaxOpacity;
        private System.Windows.Forms.Button m_cmdOK;
        private System.Windows.Forms.Button m_cmdCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <summary>
        /// Restricts processing of events.
        /// </summary>
        private bool m_ignoreEvents = false;

        /// <summary>
        /// The variable being edited.
        /// </summary>
        private MenuOptionImage m_edit = new MenuOptionImage();

        /// <summary>
        /// Initializes a new image editor and edits a default menu option
        /// image.
        /// </summary>
		public MenuOptionImageEditorUI()
		{
			InitializeComponent();

            // Add colors...
            m_cboTransparencyKey.Items.Add( Color.Empty );
            m_cboTransparencyKey.Items.Add( Color.White );
            m_cboTransparencyKey.Items.Add( Color.LightGray );
            m_cboTransparencyKey.Items.Add( Color.FromArgb( 192, 192, 192 ) );
            m_cboTransparencyKey.Items.Add( Color.DarkGray );
            m_cboTransparencyKey.Items.Add( Color.Gray );
            m_cboTransparencyKey.Items.Add( Color.Black );
            m_cboTransparencyKey.Items.Add( Color.FromArgb( 255, 0, 0 ) );
            m_cboTransparencyKey.Items.Add( Color.FromArgb( 0, 255, 0 ) );
            m_cboTransparencyKey.Items.Add( Color.FromArgb( 0, 0, 255 ) );
            m_cboTransparencyKey.Items.Add( Color.Magenta );
            m_cboTransparencyKey.Items.Add( Color.Cyan );

            OptionImage = OptionImage;
		}

        /// <summary>
        /// Initializes a new image editor and edits the provided menu option.
        /// </summary>
        public MenuOptionImageEditorUI( MenuOptionImage edit ) : this()
        {
            OptionImage = edit;
        }

        /// <summary>
        /// The image being edited.  Setting this to null will be ignored.
        /// </summary>
        public MenuOptionImage OptionImage 
        {
            get { return m_edit; }
            set 
            {
                if( value != null ) 
                {
                    m_edit = value;

                    // Refresh our controls. 
                    m_ignoreEvents = true;

                    if( !m_cboTransparencyKey.Items.Contains( m_edit.TransparencyKey ) )
                        m_cboTransparencyKey.Items.Add( m_edit.TransparencyKey );

                    m_cboTransparencyKey.SelectedItem = m_edit.TransparencyKey;

                    m_chkUseDs.Checked = m_edit.UseDropShadow;

                    m_tbMaxOpacity.Value = m_edit.MaximumOpacity;

                    m_ignoreEvents = false;

                    // Redraw. 
                    m_picImage.Refresh();
                    m_pbPreview.Refresh();
                }
            }
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_gbPreview = new System.Windows.Forms.GroupBox();
            this.m_pbPreview = new System.Windows.Forms.PictureBox();
            this.m_lblBaseImage = new System.Windows.Forms.Label();
            this.m_picImage = new System.Windows.Forms.PictureBox();
            this.m_cmdBrowse = new System.Windows.Forms.Button();
            this.m_lblTransparencyKey = new System.Windows.Forms.Label();
            this.m_cboTransparencyKey = new System.Windows.Forms.ComboBox();
            this.m_lblDropShadow = new System.Windows.Forms.Label();
            this.m_chkUseDs = new System.Windows.Forms.CheckBox();
            this.m_cmdSettings = new System.Windows.Forms.Button();
            this.m_lblMaxOpacity = new System.Windows.Forms.Label();
            this.m_tbMaxOpacity = new System.Windows.Forms.TrackBar();
            this.m_cmdOK = new System.Windows.Forms.Button();
            this.m_cmdCancel = new System.Windows.Forms.Button();
            this.m_gbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_tbMaxOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // m_gbPreview
            // 
            this.m_gbPreview.Controls.Add(this.m_pbPreview);
            this.m_gbPreview.Location = new System.Drawing.Point(8, 8);
            this.m_gbPreview.Name = "m_gbPreview";
            this.m_gbPreview.Size = new System.Drawing.Size(104, 104);
            this.m_gbPreview.TabIndex = 0;
            this.m_gbPreview.TabStop = false;
            this.m_gbPreview.Text = "Preview";
            // 
            // m_pbPreview
            // 
            this.m_pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pbPreview.Location = new System.Drawing.Point(8, 16);
            this.m_pbPreview.Name = "m_pbPreview";
            this.m_pbPreview.Size = new System.Drawing.Size(88, 80);
            this.m_pbPreview.TabIndex = 0;
            this.m_pbPreview.TabStop = false;
            this.m_pbPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintPreview);
            // 
            // m_lblBaseImage
            // 
            this.m_lblBaseImage.Location = new System.Drawing.Point(120, 16);
            this.m_lblBaseImage.Name = "m_lblBaseImage";
            this.m_lblBaseImage.Size = new System.Drawing.Size(100, 24);
            this.m_lblBaseImage.TabIndex = 1;
            this.m_lblBaseImage.Text = "Base Image:";
            // 
            // m_picImage
            // 
            this.m_picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_picImage.Location = new System.Drawing.Point(232, 16);
            this.m_picImage.Name = "m_picImage";
            this.m_picImage.Size = new System.Drawing.Size(24, 24);
            this.m_picImage.TabIndex = 2;
            this.m_picImage.TabStop = false;
            this.m_picImage.Paint += new System.Windows.Forms.PaintEventHandler(this.ShowBaseImage);
            // 
            // m_cmdBrowse
            // 
            this.m_cmdBrowse.Location = new System.Drawing.Point(264, 16);
            this.m_cmdBrowse.Name = "m_cmdBrowse";
            this.m_cmdBrowse.Size = new System.Drawing.Size(104, 24);
            this.m_cmdBrowse.TabIndex = 3;
            this.m_cmdBrowse.Text = "&Browse...";
            this.m_cmdBrowse.Click += new System.EventHandler(this.BrowseForImage);
            // 
            // m_lblTransparencyKey
            // 
            this.m_lblTransparencyKey.Location = new System.Drawing.Point(120, 48);
            this.m_lblTransparencyKey.Name = "m_lblTransparencyKey";
            this.m_lblTransparencyKey.Size = new System.Drawing.Size(100, 24);
            this.m_lblTransparencyKey.TabIndex = 4;
            this.m_lblTransparencyKey.Text = "&Transparency Key:";
            // 
            // m_cboTransparencyKey
            // 
            this.m_cboTransparencyKey.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.m_cboTransparencyKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboTransparencyKey.ItemHeight = 18;
            this.m_cboTransparencyKey.Location = new System.Drawing.Point(232, 48);
            this.m_cboTransparencyKey.Name = "m_cboTransparencyKey";
            this.m_cboTransparencyKey.Size = new System.Drawing.Size(136, 24);
            this.m_cboTransparencyKey.TabIndex = 5;
            this.m_cboTransparencyKey.SelectedIndexChanged += new System.EventHandler(this.TransparencyKeyChanged);
            this.m_cboTransparencyKey.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawColorItem);
            // 
            // m_lblDropShadow
            // 
            this.m_lblDropShadow.Location = new System.Drawing.Point(120, 80);
            this.m_lblDropShadow.Name = "m_lblDropShadow";
            this.m_lblDropShadow.Size = new System.Drawing.Size(100, 24);
            this.m_lblDropShadow.TabIndex = 6;
            this.m_lblDropShadow.Text = "&Use Drop Shadow:";
            // 
            // m_chkUseDs
            // 
            this.m_chkUseDs.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_chkUseDs.Location = new System.Drawing.Point(232, 80);
            this.m_chkUseDs.Name = "m_chkUseDs";
            this.m_chkUseDs.Size = new System.Drawing.Size(24, 24);
            this.m_chkUseDs.TabIndex = 7;
            this.m_chkUseDs.CheckedChanged += new System.EventHandler(this.UseDropShadowChanged);
            // 
            // m_cmdSettings
            // 
            this.m_cmdSettings.Location = new System.Drawing.Point(264, 80);
            this.m_cmdSettings.Name = "m_cmdSettings";
            this.m_cmdSettings.Size = new System.Drawing.Size(104, 24);
            this.m_cmdSettings.TabIndex = 8;
            this.m_cmdSettings.Text = "&Settings...";
            this.m_cmdSettings.Click += new System.EventHandler(this.ChangeDropShadowSettings);
            // 
            // m_lblMaxOpacity
            // 
            this.m_lblMaxOpacity.Location = new System.Drawing.Point(120, 112);
            this.m_lblMaxOpacity.Name = "m_lblMaxOpacity";
            this.m_lblMaxOpacity.Size = new System.Drawing.Size(100, 24);
            this.m_lblMaxOpacity.TabIndex = 9;
            this.m_lblMaxOpacity.Text = "&Maximum Opacity:";
            // 
            // m_tbMaxOpacity
            // 
            this.m_tbMaxOpacity.LargeChange = 15;
            this.m_tbMaxOpacity.Location = new System.Drawing.Point(232, 112);
            this.m_tbMaxOpacity.Maximum = 255;
            this.m_tbMaxOpacity.Name = "m_tbMaxOpacity";
            this.m_tbMaxOpacity.Size = new System.Drawing.Size(136, 45);
            this.m_tbMaxOpacity.TabIndex = 10;
            this.m_tbMaxOpacity.TickFrequency = 26;
            this.m_tbMaxOpacity.Value = 255;
            this.m_tbMaxOpacity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MaximumOpacityChanged);
            this.m_tbMaxOpacity.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MaximumOpacityChanged);
            // 
            // m_cmdOK
            // 
            this.m_cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_cmdOK.Location = new System.Drawing.Point(168, 176);
            this.m_cmdOK.Name = "m_cmdOK";
            this.m_cmdOK.Size = new System.Drawing.Size(96, 32);
            this.m_cmdOK.TabIndex = 11;
            this.m_cmdOK.Text = "OK";
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdCancel.Location = new System.Drawing.Point(272, 176);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Size = new System.Drawing.Size(96, 32);
            this.m_cmdCancel.TabIndex = 12;
            this.m_cmdCancel.Text = "Cancel";
            // 
            // MenuOptionImageEditorUI
            // 
            this.AcceptButton = this.m_cmdOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_cmdCancel;
            this.ClientSize = new System.Drawing.Size(378, 216);
            this.ControlBox = false;
            this.Controls.Add(this.m_cmdCancel);
            this.Controls.Add(this.m_cmdOK);
            this.Controls.Add(this.m_tbMaxOpacity);
            this.Controls.Add(this.m_lblMaxOpacity);
            this.Controls.Add(this.m_cmdSettings);
            this.Controls.Add(this.m_chkUseDs);
            this.Controls.Add(this.m_lblDropShadow);
            this.Controls.Add(this.m_cboTransparencyKey);
            this.Controls.Add(this.m_lblTransparencyKey);
            this.Controls.Add(this.m_cmdBrowse);
            this.Controls.Add(this.m_picImage);
            this.Controls.Add(this.m_lblBaseImage);
            this.Controls.Add(this.m_gbPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuOptionImageEditorUI";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Menu Option Image";
            this.m_gbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_tbMaxOpacity)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        /// <summary>
        /// Paints the base image for this option.
        /// </summary>
        private void ShowBaseImage(
            object sender, 
            System.Windows.Forms.PaintEventArgs e
            )
        {
            e.Graphics.DrawImage(
                m_edit.Image,
                e.ClipRectangle
                );
        }

        /// <summary>
        /// Paints the preview image.
        /// </summary>
        private void PaintPreview(
            object sender, 
            System.Windows.Forms.PaintEventArgs e
            )
        {
            // Paint the checkered background.
            Color foreground = Color.White;
            Color background = Color.FromArgb( 225, 225, 225 );
            bool even = false;

            for( int x = 0; x <= m_pbPreview.Width; x += 7 )
            {
                bool colEven = false;
                for( int y = 0; y <= m_pbPreview.Height; y += 7 )
                {
                    Brush brush;
                    if( even && colEven || !even && !colEven )
                        brush = new SolidBrush( foreground );
                    else
                        brush = new SolidBrush( background );

                    e.Graphics.FillRectangle(
                        brush,
                        x,
                        y,
                        7,
                        7
                        );

                    colEven = !colEven;
                }

                even = !even;
            }

            // Paint the image.
            Bitmap image = m_edit.CachedImage;
            int imgx = (m_pbPreview.Width / 2) - (image.Width / 2);
            int imgy = (m_pbPreview.Height / 2) - (image.Height / 2);

            e.Graphics.DrawImage(
                image,
                imgx,
                imgy
                );
        }

        /// <summary>
        /// Renders an item in the color combo box.
        /// </summary>
        private void DrawColorItem(
            object sender, 
            System.Windows.Forms.DrawItemEventArgs e
            )
        {
            Color textColor = SystemColors.WindowText;
            e.DrawBackground();

            switch( e.State ) 
            {
                case DrawItemState.Focus: e.DrawFocusRectangle(); break;

                case DrawItemState.HotLight:
                    e.Graphics.FillRectangle(
                        SystemBrushes.Highlight,
                        e.Bounds
                        );

                    textColor = SystemColors.WindowText;
                    break;

                case DrawItemState.Selected:
                    e.Graphics.FillRectangle(
                        SystemBrushes.Highlight,
                        e.Bounds
                        );

                    textColor = SystemColors.WindowText;
                    break;
            }

            Color current = (Color)m_cboTransparencyKey.Items[ e.Index ];
            Color drawColor;
            string name;
            
            if( !Color.Empty.Equals( current ) )
            {
                drawColor = current;
                name = current.Name;
            }
            else 
            {
                drawColor = Color.White;
                name = "(None)";
            }

            e.Graphics.FillRectangle(
                new SolidBrush( drawColor ),
                e.Bounds.Left + 2, e.Bounds.Top + 2,
                e.Bounds.Height - 4,
                e.Bounds.Height - 4
                );

            e.Graphics.DrawString(
                name,
                m_cboTransparencyKey.Font,
                new SolidBrush( textColor ),
                new Rectangle(
                    e.Bounds.Left + 4 + (e.Bounds.Height - 4),
                    e.Bounds.Top + 2,
                    e.Bounds.Width - (4 + (e.Bounds.Height - 2)),
                    e.Bounds.Height - 4
                    )
                );
        }

        /// <summary>
        /// Fired when the transparency key changes.
        /// </summary>
        private void TransparencyKeyChanged(
            object sender, 
            System.EventArgs e
            )
        {
            if( !m_ignoreEvents )
            {
                m_edit.TransparencyKey = 
                    (Color)m_cboTransparencyKey.SelectedItem;

                m_pbPreview.Refresh();
            }
        }

        /// <summary>
        /// Fired when the user selects or deselects the drop shadow
        /// check box.
        /// </summary>
        private void UseDropShadowChanged(
            object sender, 
            System.EventArgs e
            )
        {
            if( !m_ignoreEvents ) 
            {
                m_edit.UseDropShadow = m_chkUseDs.Checked;
                m_pbPreview.Refresh();
            }
        }

        /// <summary>
        /// Called after the user finishes scrolling the maximum opacity
        /// slider.
        /// </summary>
        private void MaximumOpacityChanged(
            object sender, 
            System.Windows.Forms.KeyEventArgs e
            )
        {
            if( !m_ignoreEvents ) 
            {
                m_edit.MaximumOpacity = (byte)m_tbMaxOpacity.Value;
                m_pbPreview.Refresh();
            }
        }

        /// <summary>
        /// Allows the user to edit the drop shadow settings.
        /// </summary>
        private void ChangeDropShadowSettings(
            object sender, 
            System.EventArgs e
            )
        {
            DropShadowOptionsEditorUi d = new DropShadowOptionsEditorUi(
                (DropShadowOptions)m_edit.DropShadow.Clone()
                );

            if( d.ShowDialog( this ) == DialogResult.OK ) 
            {
                m_edit.DropShadow = d.Options;
                m_pbPreview.Refresh();
            }
        }

        /// <summary>
        /// Allows the user to browse for an image to use as the base image.
        /// </summary>
        private void BrowseForImage(
            object sender, 
            System.EventArgs e
            )
        {
            OpenFileDialog d = new OpenFileDialog();

            d.AddExtension = true;
            d.CheckFileExists = true;
            d.CheckPathExists = true;
            d.Filter = "All image formats|*.bmp;*.jpg;*.jpeg;*.gif;*.ico";
            d.ShowHelp = false;
            d.ShowReadOnly = false;
            d.Title = "Select an Image";
            d.ValidateNames = true;
            
            if( d.ShowDialog( this ) == DialogResult.OK ) 
            {
                try 
                {
                    m_edit.Image = new Bitmap( d.FileName );
                    m_picImage.Refresh();
                    m_pbPreview.Refresh();
                }
                catch 
                {
                    MessageBox.Show(
                        this,
                        "Could not load the image.",
                        "Browse For Image",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                        );
                }
            }
        }

        /// <summary>
        /// Called after the user finishes scrolling the maximum opacity
        /// slider.
        /// </summary>
        private void MaximumOpacityChanged(
            object sender, 
            System.Windows.Forms.MouseEventArgs e
            )
        {
            if( !m_ignoreEvents ) 
            {
                m_edit.MaximumOpacity = (byte)m_tbMaxOpacity.Value;
                m_pbPreview.Refresh();
            }
        }
	}
}
