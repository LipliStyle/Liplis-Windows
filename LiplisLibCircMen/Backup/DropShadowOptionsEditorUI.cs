using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PixelEffects;

namespace CircularMenu
{
    /// <summary>
    /// Defines a dialog that can edit <see cref="DropShadowOptions"/> objects.
    /// </summary>
	public class DropShadowOptionsEditorUi : System.Windows.Forms.Form
	{
        private DropShadowOptions m_options = new DropShadowOptions();
        private bool m_needsRepaint = false;
        private long m_lastRepaintTime = DateTime.Today.Ticks;

        private Bitmap m_iconExample = null;

        private System.Windows.Forms.GroupBox m_gbExample;
        private System.Windows.Forms.PictureBox m_pbShadow;
        private System.Windows.Forms.PictureBox m_pbMechanics;
        private System.Windows.Forms.Label m_lblColor;
        private System.Windows.Forms.Label m_lblBlurRadius;
        private System.Windows.Forms.Label m_lblOpacityStep;
        private System.Windows.Forms.Label m_lblMaxOpacity;
        private System.Windows.Forms.Label m_lblXOffset;
        private System.Windows.Forms.Label m_lblYOffset;
        private System.Windows.Forms.ComboBox m_cboColor;
        private System.Windows.Forms.Button m_cmdChooseColor;
        private System.Windows.Forms.HScrollBar m_hsbBlurRadius;
        private System.Windows.Forms.Label m_lblBlurValue;
        private System.Windows.Forms.Label m_lblOpacityStepVal;
        private System.Windows.Forms.HScrollBar m_hsbOpacityStep;
        private System.Windows.Forms.Label m_lblMaxOpacityVal;
        private System.Windows.Forms.HScrollBar m_hsbMaxOpacity;
        private System.Windows.Forms.Label m_lblXOffsetVal;
        private System.Windows.Forms.HScrollBar m_hsbXOffset;
        private System.Windows.Forms.Label m_lblYOffsetVal;
        private System.Windows.Forms.HScrollBar m_hsbYOffset;
        private System.Windows.Forms.Button m_cmdCancel;
        private System.Windows.Forms.Button m_cmdOk;
        private System.Windows.Forms.CheckBox m_chkSame;
        private System.Windows.Forms.Timer m_repainter;
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Initializes a new editor for a default set of options.
        /// </summary>
		public DropShadowOptionsEditorUi()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            
            // Add colors...
            m_cboColor.Items.Add( Color.White );
            m_cboColor.Items.Add( Color.LightGray );
            m_cboColor.Items.Add( Color.Gray );
            m_cboColor.Items.Add( Color.DarkGray );
            m_cboColor.Items.Add( Color.Black );
            m_cboColor.Items.Add( Color.DarkRed );
            m_cboColor.Items.Add( Color.Red );
            m_cboColor.Items.Add( Color.DarkGreen );
            m_cboColor.Items.Add( Color.Green );
            m_cboColor.Items.Add( Color.LightGreen );
            m_cboColor.Items.Add( Color.DarkBlue );
            m_cboColor.Items.Add( Color.Blue );
            m_cboColor.Items.Add( Color.LightBlue );
            m_cboColor.Items.Add( Color.Yellow );
            m_cboColor.Items.Add( Color.LightYellow );

            m_cboColor.Items.Add( Color.AliceBlue );
            m_cboColor.Items.Add( Color.Navy );
            m_cboColor.Items.Add( Color.Pink );
            m_cboColor.Items.Add( Color.Aquamarine );
            m_cboColor.Items.Add( Color.SeaGreen );

            // Show our default example...
            RefreshDisplaySettings();
		}

        /// <summary>
        /// Initializes a new editor and assigns it to edit the provided set of
        /// options, which cannot be null.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided options set is null.
        /// </exception>
        public DropShadowOptionsEditorUi( DropShadowOptions options ) 
            : this()
        {
            Options = options;
        }

        /// <summary>
        /// Defines the set of options currently being edited by this dialog.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        public DropShadowOptions Options 
        {
            get { return m_options; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "Options",
                        "The options property cannot be null"
                        );

                else 
                {
                    m_options = value;
                    RefreshDisplaySettings();
                }
            }
        }

        /// <summary>
        /// Synchronizes the GUI setting elements to reflect the current values
        /// of the options object, and repaints the examples.
        /// </summary>
        public void RefreshDisplaySettings() 
        {
            // Color...
            if( !m_cboColor.Items.Contains( m_options.ShadowColor ) )
                m_cboColor.Items.Add( m_options.ShadowColor );

            m_cboColor.SelectedItem = m_options.ShadowColor;

            // Blur radius...
            if( 
                m_options.BlurRadius >= m_hsbBlurRadius.Minimum && 
                m_options.BlurRadius <= m_hsbBlurRadius.Maximum 
                )
                m_hsbBlurRadius.Value = m_options.BlurRadius;

            // Opacity step...
            if(
                m_options.OpacityStep >= m_hsbOpacityStep.Minimum &&
                m_options.OpacityStep <= m_hsbOpacityStep.Maximum
                )
                m_hsbOpacityStep.Value = m_options.OpacityStep;

            // Maximum opacity.
            m_hsbMaxOpacity.Value = m_options.MaximumOpacity;

            // X offset.
            if(
                m_options.ShadowOffsetX >= m_hsbXOffset.Minimum &&
                m_options.ShadowOffsetX <= m_hsbXOffset.Maximum 
                )
                m_hsbXOffset.Value = m_options.ShadowOffsetX;

            // Y offset.
            if(
                m_options.ShadowOffsetY >= m_hsbYOffset.Minimum &&
                m_options.ShadowOffsetY <= m_hsbYOffset.Maximum 
                )
                m_hsbYOffset.Value = m_options.ShadowOffsetY;

            // Indicate values. 
            ShowNumbers();

            // Redraw our examples. 
            DrawIconExample();
            DrawLayoutExample();
        }

        /// <summary>
        /// Repaints the iconic example (shows an actual image and its drop
        /// shadow).  This method can take a long time.
        /// </summary>
        public void DrawIconExample() 
        {
            m_pbShadow.Refresh();
        }

        /// <summary>
        /// Repaints the layout example (showing only the sample regions).
        /// This method is fast, compared with <see cref="DrawIconExample"/>.
        /// </summary>
        public void DrawLayoutExample() 
        {
            m_pbMechanics.Refresh();
        }

        /// <summary>
        /// Displays the current settings in all of the numeric labels.
        /// </summary>
        private void ShowNumbers() 
        {
            m_lblBlurValue.Text = "(" + m_options.BlurRadius + ")";
            m_lblOpacityStepVal.Text = "(" + m_options.OpacityStep + ")";
            m_lblMaxOpacityVal.Text = "(" + m_options.MaximumOpacity + ")";
            m_lblXOffsetVal.Text = "(" + m_options.ShadowOffsetX + ")";
            m_lblYOffsetVal.Text = "(" + m_options.ShadowOffsetY + ")";
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
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DropShadowOptionsEditorUi));
            this.m_gbExample = new System.Windows.Forms.GroupBox();
            this.m_pbMechanics = new System.Windows.Forms.PictureBox();
            this.m_pbShadow = new System.Windows.Forms.PictureBox();
            this.m_lblColor = new System.Windows.Forms.Label();
            this.m_lblBlurRadius = new System.Windows.Forms.Label();
            this.m_lblOpacityStep = new System.Windows.Forms.Label();
            this.m_lblMaxOpacity = new System.Windows.Forms.Label();
            this.m_lblXOffset = new System.Windows.Forms.Label();
            this.m_lblYOffset = new System.Windows.Forms.Label();
            this.m_cboColor = new System.Windows.Forms.ComboBox();
            this.m_cmdChooseColor = new System.Windows.Forms.Button();
            this.m_hsbBlurRadius = new System.Windows.Forms.HScrollBar();
            this.m_lblBlurValue = new System.Windows.Forms.Label();
            this.m_lblOpacityStepVal = new System.Windows.Forms.Label();
            this.m_hsbOpacityStep = new System.Windows.Forms.HScrollBar();
            this.m_lblMaxOpacityVal = new System.Windows.Forms.Label();
            this.m_hsbMaxOpacity = new System.Windows.Forms.HScrollBar();
            this.m_lblXOffsetVal = new System.Windows.Forms.Label();
            this.m_hsbXOffset = new System.Windows.Forms.HScrollBar();
            this.m_lblYOffsetVal = new System.Windows.Forms.Label();
            this.m_hsbYOffset = new System.Windows.Forms.HScrollBar();
            this.m_cmdCancel = new System.Windows.Forms.Button();
            this.m_cmdOk = new System.Windows.Forms.Button();
            this.m_chkSame = new System.Windows.Forms.CheckBox();
            this.m_repainter = new System.Windows.Forms.Timer(this.components);
            this.m_gbExample.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_gbExample
            // 
            this.m_gbExample.Controls.Add(this.m_pbMechanics);
            this.m_gbExample.Controls.Add(this.m_pbShadow);
            this.m_gbExample.Location = new System.Drawing.Point(8, 8);
            this.m_gbExample.Name = "m_gbExample";
            this.m_gbExample.Size = new System.Drawing.Size(112, 232);
            this.m_gbExample.TabIndex = 0;
            this.m_gbExample.TabStop = false;
            this.m_gbExample.Text = "Example";
            // 
            // m_pbMechanics
            // 
            this.m_pbMechanics.Location = new System.Drawing.Point(8, 128);
            this.m_pbMechanics.Name = "m_pbMechanics";
            this.m_pbMechanics.Size = new System.Drawing.Size(96, 96);
            this.m_pbMechanics.TabIndex = 1;
            this.m_pbMechanics.TabStop = false;
            this.m_pbMechanics.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintLayoutExample);
            // 
            // m_pbShadow
            // 
            this.m_pbShadow.Location = new System.Drawing.Point(8, 24);
            this.m_pbShadow.Name = "m_pbShadow";
            this.m_pbShadow.Size = new System.Drawing.Size(96, 96);
            this.m_pbShadow.TabIndex = 0;
            this.m_pbShadow.TabStop = false;
            this.m_pbShadow.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintShadowExample);
            // 
            // m_lblColor
            // 
            this.m_lblColor.Location = new System.Drawing.Point(128, 16);
            this.m_lblColor.Name = "m_lblColor";
            this.m_lblColor.Size = new System.Drawing.Size(100, 16);
            this.m_lblColor.TabIndex = 1;
            this.m_lblColor.Text = "&Color:";
            // 
            // m_lblBlurRadius
            // 
            this.m_lblBlurRadius.Location = new System.Drawing.Point(128, 40);
            this.m_lblBlurRadius.Name = "m_lblBlurRadius";
            this.m_lblBlurRadius.Size = new System.Drawing.Size(100, 16);
            this.m_lblBlurRadius.TabIndex = 4;
            this.m_lblBlurRadius.Text = "&Blur Radius:";
            // 
            // m_lblOpacityStep
            // 
            this.m_lblOpacityStep.Location = new System.Drawing.Point(128, 64);
            this.m_lblOpacityStep.Name = "m_lblOpacityStep";
            this.m_lblOpacityStep.Size = new System.Drawing.Size(100, 16);
            this.m_lblOpacityStep.TabIndex = 7;
            this.m_lblOpacityStep.Text = "&Opacity Step:";
            // 
            // m_lblMaxOpacity
            // 
            this.m_lblMaxOpacity.Location = new System.Drawing.Point(128, 88);
            this.m_lblMaxOpacity.Name = "m_lblMaxOpacity";
            this.m_lblMaxOpacity.Size = new System.Drawing.Size(100, 16);
            this.m_lblMaxOpacity.TabIndex = 10;
            this.m_lblMaxOpacity.Text = "&Maximum Opacity:";
            // 
            // m_lblXOffset
            // 
            this.m_lblXOffset.Location = new System.Drawing.Point(128, 112);
            this.m_lblXOffset.Name = "m_lblXOffset";
            this.m_lblXOffset.Size = new System.Drawing.Size(100, 16);
            this.m_lblXOffset.TabIndex = 13;
            this.m_lblXOffset.Text = "&X Offset:";
            // 
            // m_lblYOffset
            // 
            this.m_lblYOffset.Location = new System.Drawing.Point(128, 136);
            this.m_lblYOffset.Name = "m_lblYOffset";
            this.m_lblYOffset.Size = new System.Drawing.Size(100, 16);
            this.m_lblYOffset.TabIndex = 16;
            this.m_lblYOffset.Text = "&Y Offset:";
            // 
            // m_cboColor
            // 
            this.m_cboColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.m_cboColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboColor.Location = new System.Drawing.Point(232, 16);
            this.m_cboColor.Name = "m_cboColor";
            this.m_cboColor.Size = new System.Drawing.Size(144, 21);
            this.m_cboColor.TabIndex = 2;
            this.m_cboColor.SelectedIndexChanged += new System.EventHandler(this.SelectedColorChanged);
            this.m_cboColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawColorItem);
            // 
            // m_cmdChooseColor
            // 
            this.m_cmdChooseColor.Location = new System.Drawing.Point(384, 16);
            this.m_cmdChooseColor.Name = "m_cmdChooseColor";
            this.m_cmdChooseColor.Size = new System.Drawing.Size(40, 23);
            this.m_cmdChooseColor.TabIndex = 3;
            this.m_cmdChooseColor.Text = "...";
            this.m_cmdChooseColor.Click += new System.EventHandler(this.ChooseColor);
            // 
            // m_hsbBlurRadius
            // 
            this.m_hsbBlurRadius.Location = new System.Drawing.Point(232, 40);
            this.m_hsbBlurRadius.Maximum = 20;
            this.m_hsbBlurRadius.Name = "m_hsbBlurRadius";
            this.m_hsbBlurRadius.Size = new System.Drawing.Size(144, 17);
            this.m_hsbBlurRadius.TabIndex = 5;
            this.m_hsbBlurRadius.Scroll += new System.Windows.Forms.ScrollEventHandler(this.BlurRadiusScrolling);
            // 
            // m_lblBlurValue
            // 
            this.m_lblBlurValue.Location = new System.Drawing.Point(384, 40);
            this.m_lblBlurValue.Name = "m_lblBlurValue";
            this.m_lblBlurValue.Size = new System.Drawing.Size(40, 16);
            this.m_lblBlurValue.TabIndex = 6;
            this.m_lblBlurValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // m_lblOpacityStepVal
            // 
            this.m_lblOpacityStepVal.Location = new System.Drawing.Point(384, 64);
            this.m_lblOpacityStepVal.Name = "m_lblOpacityStepVal";
            this.m_lblOpacityStepVal.Size = new System.Drawing.Size(40, 16);
            this.m_lblOpacityStepVal.TabIndex = 9;
            this.m_lblOpacityStepVal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // m_hsbOpacityStep
            // 
            this.m_hsbOpacityStep.Location = new System.Drawing.Point(232, 64);
            this.m_hsbOpacityStep.Maximum = 255;
            this.m_hsbOpacityStep.Minimum = 1;
            this.m_hsbOpacityStep.Name = "m_hsbOpacityStep";
            this.m_hsbOpacityStep.Size = new System.Drawing.Size(144, 17);
            this.m_hsbOpacityStep.TabIndex = 8;
            this.m_hsbOpacityStep.Value = 1;
            this.m_hsbOpacityStep.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OpacityStepScrolling);
            // 
            // m_lblMaxOpacityVal
            // 
            this.m_lblMaxOpacityVal.Location = new System.Drawing.Point(384, 88);
            this.m_lblMaxOpacityVal.Name = "m_lblMaxOpacityVal";
            this.m_lblMaxOpacityVal.Size = new System.Drawing.Size(40, 16);
            this.m_lblMaxOpacityVal.TabIndex = 12;
            this.m_lblMaxOpacityVal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // m_hsbMaxOpacity
            // 
            this.m_hsbMaxOpacity.Location = new System.Drawing.Point(232, 88);
            this.m_hsbMaxOpacity.Maximum = 255;
            this.m_hsbMaxOpacity.Minimum = 1;
            this.m_hsbMaxOpacity.Name = "m_hsbMaxOpacity";
            this.m_hsbMaxOpacity.Size = new System.Drawing.Size(144, 17);
            this.m_hsbMaxOpacity.TabIndex = 11;
            this.m_hsbMaxOpacity.Value = 1;
            this.m_hsbMaxOpacity.Scroll += new System.Windows.Forms.ScrollEventHandler(this.MaximumOpacityScrolling);
            // 
            // m_lblXOffsetVal
            // 
            this.m_lblXOffsetVal.Location = new System.Drawing.Point(384, 112);
            this.m_lblXOffsetVal.Name = "m_lblXOffsetVal";
            this.m_lblXOffsetVal.Size = new System.Drawing.Size(40, 16);
            this.m_lblXOffsetVal.TabIndex = 15;
            this.m_lblXOffsetVal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // m_hsbXOffset
            // 
            this.m_hsbXOffset.Location = new System.Drawing.Point(232, 112);
            this.m_hsbXOffset.Maximum = 20;
            this.m_hsbXOffset.Minimum = -10;
            this.m_hsbXOffset.Name = "m_hsbXOffset";
            this.m_hsbXOffset.Size = new System.Drawing.Size(144, 17);
            this.m_hsbXOffset.TabIndex = 14;
            this.m_hsbXOffset.Scroll += new System.Windows.Forms.ScrollEventHandler(this.XOffsetScrolling);
            // 
            // m_lblYOffsetVal
            // 
            this.m_lblYOffsetVal.Location = new System.Drawing.Point(384, 136);
            this.m_lblYOffsetVal.Name = "m_lblYOffsetVal";
            this.m_lblYOffsetVal.Size = new System.Drawing.Size(40, 16);
            this.m_lblYOffsetVal.TabIndex = 18;
            this.m_lblYOffsetVal.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // m_hsbYOffset
            // 
            this.m_hsbYOffset.Location = new System.Drawing.Point(232, 136);
            this.m_hsbYOffset.Maximum = 20;
            this.m_hsbYOffset.Minimum = -10;
            this.m_hsbYOffset.Name = "m_hsbYOffset";
            this.m_hsbYOffset.Size = new System.Drawing.Size(144, 17);
            this.m_hsbYOffset.TabIndex = 17;
            this.m_hsbYOffset.Scroll += new System.Windows.Forms.ScrollEventHandler(this.YOffsetScrolling);
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdCancel.Location = new System.Drawing.Point(248, 208);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Size = new System.Drawing.Size(88, 32);
            this.m_cmdCancel.TabIndex = 19;
            this.m_cmdCancel.Text = "Cancel";
            // 
            // m_cmdOk
            // 
            this.m_cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_cmdOk.Location = new System.Drawing.Point(344, 208);
            this.m_cmdOk.Name = "m_cmdOk";
            this.m_cmdOk.Size = new System.Drawing.Size(88, 32);
            this.m_cmdOk.TabIndex = 20;
            this.m_cmdOk.Text = "OK";
            // 
            // m_chkSame
            // 
            this.m_chkSame.Location = new System.Drawing.Point(232, 168);
            this.m_chkSame.Name = "m_chkSame";
            this.m_chkSame.Size = new System.Drawing.Size(192, 24);
            this.m_chkSame.TabIndex = 21;
            this.m_chkSame.Text = "Keep X and Y offsets the &same";
            this.m_chkSame.CheckedChanged += new System.EventHandler(this.LockedChanged);
            // 
            // m_repainter
            // 
            this.m_repainter.Enabled = true;
            this.m_repainter.Interval = 1000;
            this.m_repainter.Tick += new System.EventHandler(this.TestRepaint);
            // 
            // DropShadowOptionsEditorUi
            // 
            this.AcceptButton = this.m_cmdOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_cmdCancel;
            this.ClientSize = new System.Drawing.Size(440, 248);
            this.Controls.Add(this.m_chkSame);
            this.Controls.Add(this.m_cmdOk);
            this.Controls.Add(this.m_cmdCancel);
            this.Controls.Add(this.m_lblYOffsetVal);
            this.Controls.Add(this.m_hsbYOffset);
            this.Controls.Add(this.m_lblXOffsetVal);
            this.Controls.Add(this.m_hsbXOffset);
            this.Controls.Add(this.m_lblMaxOpacityVal);
            this.Controls.Add(this.m_hsbMaxOpacity);
            this.Controls.Add(this.m_lblOpacityStepVal);
            this.Controls.Add(this.m_hsbOpacityStep);
            this.Controls.Add(this.m_lblBlurValue);
            this.Controls.Add(this.m_hsbBlurRadius);
            this.Controls.Add(this.m_cmdChooseColor);
            this.Controls.Add(this.m_cboColor);
            this.Controls.Add(this.m_lblYOffset);
            this.Controls.Add(this.m_lblXOffset);
            this.Controls.Add(this.m_lblMaxOpacity);
            this.Controls.Add(this.m_lblOpacityStep);
            this.Controls.Add(this.m_lblBlurRadius);
            this.Controls.Add(this.m_lblColor);
            this.Controls.Add(this.m_gbExample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DropShadowOptionsEditorUi";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Drop Shadow Options";
            this.m_gbExample.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        /// <summary>
        /// Renders an example of the actual icon.
        /// </summary>
        private void PaintShadowExample(
            object sender, 
            System.Windows.Forms.PaintEventArgs e
            )
        {
            // Get the example icon to draw.
            if( m_iconExample == null )
                m_iconExample = Effects.GetAssemblyImageResource(
                    System.Reflection.Assembly.GetExecutingAssembly(),
                    "CircularMenu.DefaultIcon.ico"
                    );

            // Use the options to take care of it.
            Bitmap example = m_options.GetImageWithDropShadow( m_iconExample );

            // Center the example.
            int left = (m_pbShadow.Width / 2) - (example.Width / 2);
            int top = (m_pbShadow.Height / 2) - (example.Height / 2);

            e.Graphics.DrawImage(
                example,
                left,
                top
                );

            // Draw ourselves a nice border.
            e.Graphics.DrawRectangle(
                new Pen( Color.FromArgb( 128, SystemColors.ControlDark ) ),
                0, 0, m_pbShadow.Width - 1, m_pbShadow.Height - 1
                );
        }

        /// <summary>
        /// Renders an example of the shadow region compared with the image
        /// region.
        /// </summary>
        private void PaintLayoutExample(
            object sender, 
            System.Windows.Forms.PaintEventArgs e
            )
        {
            // Calculate metrics...
            Size imageSize = new Size( 32, 32 );
            Size shadowSize = new Size(
                imageSize.Width + m_options.BlurRadius * 2,
                imageSize.Height + m_options.BlurRadius * 2
                );

            int width = 0, height = 0;
            int imageX = 0, imageY = 0;
            int dsX = 0, dsY = 0;

            int x = m_options.ShadowOffsetX;
            int y = m_options.ShadowOffsetY;

            if( x >= 0 )
            {
                width = x + shadowSize.Width;
                dsX = x;
            }
            else 
            {
                width = Math.Max(
                    shadowSize.Width,
                    imageSize.Width + (-x)
                    );
                imageX = -x;
            }

            if( y >= 0 )
            {
                height = y + shadowSize.Height;
                dsY = y;
            }
            else 
            {
                height = Math.Max(
                    shadowSize.Height,
                    imageSize.Height + (-y)
                    );
                imageY = -y;
            }

            // Where's our physical origin?
            int left = (m_pbMechanics.Width / 2) - (width / 2);
            int top = (m_pbMechanics.Height / 2) - (height / 2);

            // Render the shadow box.
            e.Graphics.FillRectangle(
                SystemBrushes.ControlDark,
                left + dsX,
                top + dsY,
                shadowSize.Width,
                shadowSize.Height
                );

            // Render the image box.
            e.Graphics.FillRectangle(
                SystemBrushes.Window,
                left + imageX,
                top + imageY,
                imageSize.Width,
                imageSize.Height
                );

            e.Graphics.DrawRectangle(
                SystemPens.ControlText,
                left + imageX,
                top + imageY,
                imageSize.Width,
                imageSize.Height
                );

            // Draw the axis lines.
            Pen axisPen = new Pen( Color.Red );
            axisPen.DashStyle = DashStyle.Dot;

            e.Graphics.DrawLine(
                axisPen,
                left + imageX,
                0,
                left + imageX,
                m_pbMechanics.Height
                );

            e.Graphics.DrawLine(
                axisPen,
                0,
                top + imageY,
                m_pbMechanics.Width,
                top + imageY
                );

            // Now put a border around the whole thing.
            e.Graphics.DrawRectangle(
                new Pen( Color.FromArgb( 128, SystemColors.ControlDark ) ),
                0, 0, m_pbMechanics.Width - 1, m_pbMechanics.Height - 1
                );
        }

        /// <summary>
        /// Indicates that the user selected a new color.
        /// </summary>
        private void SelectedColorChanged( object sender, System.EventArgs e )
        {
            m_options.ShadowColor = (Color)m_cboColor.SelectedItem;

            m_lastRepaintTime = DateTime.Now.Ticks;
            m_needsRepaint = true;
        }

        /// <summary>
        /// Updates the blur radius.
        /// </summary>
        private void BlurRadiusScrolling( 
            object sender, 
            System.Windows.Forms.ScrollEventArgs e 
            )
        {
            m_options.BlurRadius = m_hsbBlurRadius.Value;
            ShowNumbers();
            DrawLayoutExample();

            m_lastRepaintTime = DateTime.Now.Ticks;
            m_needsRepaint = true;
        }

        /// <summary>
        /// Updates the opacity step.
        /// </summary>
        private void OpacityStepScrolling(
            object sender, 
            System.Windows.Forms.ScrollEventArgs e
            )
        {
            m_options.OpacityStep = m_hsbOpacityStep.Value;
            ShowNumbers();

            m_lastRepaintTime = DateTime.Now.Ticks;
            m_needsRepaint = true;
        }

        /// <summary>
        /// Updates the maximum opacity.
        /// </summary>
        private void MaximumOpacityScrolling(
            object sender, 
            System.Windows.Forms.ScrollEventArgs e
            )
        {
            m_options.MaximumOpacity = m_hsbMaxOpacity.Value;
            ShowNumbers();

            m_lastRepaintTime = DateTime.Now.Ticks;
            m_needsRepaint = true;
        }

        /// <summary>
        /// Updates the x offset value.
        /// </summary>
        private void XOffsetScrolling(
            object sender, 
            System.Windows.Forms.ScrollEventArgs e
            )
        {
            m_options.ShadowOffsetX = m_hsbXOffset.Value;
            if( m_chkSame.Checked ) 
            {
                m_hsbYOffset.Value = m_options.ShadowOffsetX;
                m_options.ShadowOffsetY = m_options.ShadowOffsetX;
            }

            ShowNumbers();
            DrawLayoutExample();

            m_lastRepaintTime = DateTime.Now.Ticks;
            m_needsRepaint = true;
        }

        /// <summary>
        /// Updates the y offset value.
        /// </summary>
        private void YOffsetScrolling(
            object sender, 
            System.Windows.Forms.ScrollEventArgs e
            )
        {
            m_options.ShadowOffsetY = m_hsbYOffset.Value;
            if( m_chkSame.Checked ) 
            {
                m_hsbXOffset.Value = m_options.ShadowOffsetY;
                m_options.ShadowOffsetX = m_options.ShadowOffsetY;
            }

            ShowNumbers();
            DrawLayoutExample();

            m_lastRepaintTime = DateTime.Now.Ticks;
            m_needsRepaint = true;
        }

        /// <summary>
        /// Determines if the icon example should be painted.
        /// </summary>
        private void TestRepaint( object sender, System.EventArgs e )
        {
            if( m_needsRepaint && DateTime.Now.Ticks >= m_lastRepaintTime + 1000000 )
            {
                m_lastRepaintTime = DateTime.Now.Ticks;
                m_needsRepaint = false;
                DrawIconExample();
            }
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

            e.Graphics.FillRectangle(
                new SolidBrush( (Color)m_cboColor.Items[ e.Index ] ),
                e.Bounds.Left + 2, e.Bounds.Top + 2,
                e.Bounds.Height - 4,
                e.Bounds.Height - 4
                );

            e.Graphics.DrawString(
                ((Color)m_cboColor.Items[ e.Index ]).Name,
                m_cboColor.Font,
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
        /// Selects a color directly from the color selection panel.
        /// </summary>
        private void ChooseColor( object sender, System.EventArgs e )
        {
            ColorDialog d = new ColorDialog();

            d.AllowFullOpen = true;
            d.AnyColor = true;
            d.Color = m_options.ShadowColor;
            d.FullOpen = true;
            d.ShowHelp = false;
            
            if( d.ShowDialog( this ) == DialogResult.OK ) 
            {
                if( !m_cboColor.Items.Contains( d.Color ) )
                    m_cboColor.Items.Add( d.Color );

                m_cboColor.SelectedItem = d.Color;
            }
        }

        /// <summary>
        /// Checks to see if the x and y offsets are identical.
        /// </summary>
        private void LockedChanged( object sender, System.EventArgs e )
        {
            if( m_chkSame.Checked ) 
            {
                m_hsbYOffset.Value = m_hsbXOffset.Value;
                m_options.ShadowOffsetY = m_hsbXOffset.Value;

                ShowNumbers();
                DrawLayoutExample();
                DrawIconExample();
            }
        }
	}
}
