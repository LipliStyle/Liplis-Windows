using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CircularMenu
{
	/// <summary>
	/// Summary description for MenuAnimationEditorUI.
	/// </summary>
	public class MenuAnimationEditorUI : System.Windows.Forms.Form
	{
        private System.Windows.Forms.PictureBox m_picSample;
        private System.Windows.Forms.Label m_lblLayout;
        private System.Windows.Forms.ComboBox m_cboLayout;
        private System.Windows.Forms.ComboBox m_cboEffect;
        private System.Windows.Forms.Label m_lblEffect;
        private System.Windows.Forms.Button m_cmdPreview;
        private System.Windows.Forms.Label m_lblFrames;
        private System.Windows.Forms.NumericUpDown m_nudFrames;
        private System.Windows.Forms.Button m_cmdOk;
        private System.Windows.Forms.Button m_cmdCancel;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Timer m_tmrAnimate;

        private MenuAnimation m_animation;
        private FrameCollection m_frames = null;
        private int m_curFrame = 0;
        private MenuOptionCollection m_options;

        /// <summary>
        /// Creates a new editor dialog and edits the provided value.
        /// </summary>
        public MenuAnimationEditorUI( MenuAnimation edit ) : this()
        {
            Animation = edit;
        }

        /// <summary>
        /// Creates a new dialog and edits a default animation.
        /// </summary>
		public MenuAnimationEditorUI()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// Set up our options.
            m_cboLayout.Items.Add( new ComboItem( "Circular (no animation)", new CircularLayoutManager() ) );
            m_cboLayout.Items.Add( new ComboItem( "Radial burst", new StarburstLayoutManager() ) );
            m_cboLayout.Items.Add( new ComboItem( "Spin along perimeter", new SpinLayoutManager() ) );
            m_cboLayout.Items.Add( new ComboItem( "Unfold along perimeter", new PerimeterUnfoldLayoutManager() ) );
            m_cboLayout.Items.Add( new ComboItem( "Radial burst and spin", new SpinningStarburstLayoutManager() ) );
            m_cboLayout.Items.Add( new ComboItem( "Radial burst and unfold", new UnfoldingStarburstLayoutManager() ) );

            m_cboEffect.Items.Add( new ComboItem( "No effect", new NoOpFrameModifier() ) );
            m_cboEffect.Items.Add( new ComboItem( "Fade in", new FadeInFrameModifier() ) );
            m_cboEffect.Items.Add( new ComboItem( "Zoom in", new ZoomInFrameModifier() ) );
            m_cboEffect.Items.Add( new ComboItem( "Burn in", new BurnInFrameModifier() ) );
            m_cboEffect.Items.Add( new ComboItem( "Fade in and zoom in", new FadeInZoomFrameModifier() ) );

            // Get our sample options.
            m_options = new MenuOptionCollection();
            m_options.Add();
            m_options.Add();
            m_options.Add();
            m_options.Add();
            m_options.Add();
            m_options.Add();

            // Set our default animation.
            Animation = new ForwardMenuAnimation();
		}

        /// <summary>
        /// Defines the animation edited by this dialog.
        /// </summary>
        public MenuAnimation Animation 
        {
            get { return m_animation; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "Animation",
                        "Cannot edit a null animation"
                        );
                else
                {
                    m_animation = value;
                    
                    // Select the proper layout. 
                    ComboItem layout = 
                        new ComboItem( "Custom", m_animation.LayoutAnimator );

                    if( !m_cboLayout.Items.Contains( layout ) )
                        m_cboLayout.Items.Add( layout );

                    m_cboLayout.SelectedIndex = 
                        m_cboLayout.Items.IndexOf( layout );                    

                    // Select the proper effect.
                    ComboItem effect = 
                        new ComboItem( "Custom", m_animation.FrameImageEffect );

                    if( !m_cboEffect.Items.Contains( effect ) )
                        m_cboEffect.Items.Add( effect );

                    m_cboEffect.SelectedIndex = 
                        m_cboEffect.Items.IndexOf( effect );

                    // Show the number of frames.
                    if( m_animation.FramesToRender > m_nudFrames.Maximum )
                        m_nudFrames.Maximum = m_animation.FramesToRender;

                    m_nudFrames.Value = m_animation.FramesToRender;
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
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MenuAnimationEditorUI));
            this.m_picSample = new System.Windows.Forms.PictureBox();
            this.m_lblLayout = new System.Windows.Forms.Label();
            this.m_cboLayout = new System.Windows.Forms.ComboBox();
            this.m_cboEffect = new System.Windows.Forms.ComboBox();
            this.m_lblEffect = new System.Windows.Forms.Label();
            this.m_cmdPreview = new System.Windows.Forms.Button();
            this.m_lblFrames = new System.Windows.Forms.Label();
            this.m_nudFrames = new System.Windows.Forms.NumericUpDown();
            this.m_cmdOk = new System.Windows.Forms.Button();
            this.m_cmdCancel = new System.Windows.Forms.Button();
            this.m_tmrAnimate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_nudFrames)).BeginInit();
            this.SuspendLayout();
            // 
            // m_picSample
            // 
            this.m_picSample.Location = new System.Drawing.Point(8, 8);
            this.m_picSample.Name = "m_picSample";
            this.m_picSample.Size = new System.Drawing.Size(184, 184);
            this.m_picSample.TabIndex = 0;
            this.m_picSample.TabStop = false;
            // 
            // m_lblLayout
            // 
            this.m_lblLayout.Location = new System.Drawing.Point(200, 8);
            this.m_lblLayout.Name = "m_lblLayout";
            this.m_lblLayout.Size = new System.Drawing.Size(216, 16);
            this.m_lblLayout.TabIndex = 1;
            this.m_lblLayout.Text = "&Layout Style:";
            // 
            // m_cboLayout
            // 
            this.m_cboLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboLayout.Location = new System.Drawing.Point(200, 32);
            this.m_cboLayout.Name = "m_cboLayout";
            this.m_cboLayout.Size = new System.Drawing.Size(216, 21);
            this.m_cboLayout.TabIndex = 2;
            this.m_cboLayout.SelectedIndexChanged += new System.EventHandler(this.LayoutStyleChanged);
            // 
            // m_cboEffect
            // 
            this.m_cboEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboEffect.Location = new System.Drawing.Point(200, 88);
            this.m_cboEffect.Name = "m_cboEffect";
            this.m_cboEffect.Size = new System.Drawing.Size(216, 21);
            this.m_cboEffect.TabIndex = 4;
            this.m_cboEffect.SelectedIndexChanged += new System.EventHandler(this.EffectStyleChanged);
            // 
            // m_lblEffect
            // 
            this.m_lblEffect.Location = new System.Drawing.Point(200, 64);
            this.m_lblEffect.Name = "m_lblEffect";
            this.m_lblEffect.Size = new System.Drawing.Size(216, 16);
            this.m_lblEffect.TabIndex = 3;
            this.m_lblEffect.Text = "&Effect Style:";
            // 
            // m_cmdPreview
            // 
            this.m_cmdPreview.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_cmdPreview.Image = ((System.Drawing.Image)(resources.GetObject("m_cmdPreview.Image")));
            this.m_cmdPreview.Location = new System.Drawing.Point(160, 160);
            this.m_cmdPreview.Name = "m_cmdPreview";
            this.m_cmdPreview.Size = new System.Drawing.Size(32, 32);
            this.m_cmdPreview.TabIndex = 5;
            this.m_cmdPreview.Click += new System.EventHandler(this.RunPreview);
            // 
            // m_lblFrames
            // 
            this.m_lblFrames.Location = new System.Drawing.Point(200, 120);
            this.m_lblFrames.Name = "m_lblFrames";
            this.m_lblFrames.Size = new System.Drawing.Size(216, 16);
            this.m_lblFrames.TabIndex = 6;
            this.m_lblFrames.Text = "&Frames:";
            // 
            // m_nudFrames
            // 
            this.m_nudFrames.Increment = new System.Decimal(new int[] {
                                                                          5,
                                                                          0,
                                                                          0,
                                                                          0});
            this.m_nudFrames.Location = new System.Drawing.Point(200, 144);
            this.m_nudFrames.Maximum = new System.Decimal(new int[] {
                                                                        1000,
                                                                        0,
                                                                        0,
                                                                        0});
            this.m_nudFrames.Minimum = new System.Decimal(new int[] {
                                                                        1,
                                                                        0,
                                                                        0,
                                                                        0});
            this.m_nudFrames.Name = "m_nudFrames";
            this.m_nudFrames.Size = new System.Drawing.Size(216, 20);
            this.m_nudFrames.TabIndex = 7;
            this.m_nudFrames.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_nudFrames.ThousandsSeparator = true;
            this.m_nudFrames.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.m_nudFrames.Value = new System.Decimal(new int[] {
                                                                      1,
                                                                      0,
                                                                      0,
                                                                      0});
            this.m_nudFrames.Leave += new System.EventHandler(this.FramesToRenderChanged);
            // 
            // m_cmdOk
            // 
            this.m_cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_cmdOk.Location = new System.Drawing.Point(216, 208);
            this.m_cmdOk.Name = "m_cmdOk";
            this.m_cmdOk.Size = new System.Drawing.Size(96, 32);
            this.m_cmdOk.TabIndex = 8;
            this.m_cmdOk.Text = "OK";
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdCancel.Location = new System.Drawing.Point(320, 208);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Size = new System.Drawing.Size(96, 32);
            this.m_cmdCancel.TabIndex = 9;
            this.m_cmdCancel.Text = "Cancel";
            // 
            // m_tmrAnimate
            // 
            this.m_tmrAnimate.Interval = 30;
            this.m_tmrAnimate.Tick += new System.EventHandler(this.ShowCurrentFrame);
            // 
            // MenuAnimationEditorUI
            // 
            this.AcceptButton = this.m_cmdOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_cmdCancel;
            this.ClientSize = new System.Drawing.Size(426, 248);
            this.ControlBox = false;
            this.Controls.Add(this.m_cmdCancel);
            this.Controls.Add(this.m_cmdOk);
            this.Controls.Add(this.m_nudFrames);
            this.Controls.Add(this.m_lblFrames);
            this.Controls.Add(this.m_cmdPreview);
            this.Controls.Add(this.m_cboEffect);
            this.Controls.Add(this.m_lblEffect);
            this.Controls.Add(this.m_cboLayout);
            this.Controls.Add(this.m_lblLayout);
            this.Controls.Add(this.m_picSample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuAnimationEditorUI";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Menu Animation";
            ((System.ComponentModel.ISupportInitialize)(this.m_nudFrames)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        /// <summary>
        /// Initializes the animation preview.
        /// </summary>
        private void RunPreview( object sender, System.EventArgs e )
        {
            m_cmdPreview.Enabled = false;

            // Get the animation!
            if( m_frames == null ) 
            {
                this.Cursor = Cursors.WaitCursor;
             
                m_frames = m_animation.GetUncachedAnimation(
                    m_options,
                    50
                    );

                this.Cursor = Cursors.Default;
            }
            m_curFrame = 0;

            // Start the animation!
            m_tmrAnimate.Enabled = true;
        }

        /// <summary>
        /// Draws and renders the current frame.
        /// </summary>
        private void ShowCurrentFrame(
            object sender, 
            System.EventArgs e
            )
        {
            // Show the current frame.
            Bitmap frame = new Bitmap( m_picSample.Width, m_picSample.Height );
            Graphics g = Graphics.FromImage( frame );

            g.Clear( SystemColors.Control );
            g.DrawRectangle(
                SystemPens.ControlDark,
                0, 0,
                m_picSample.Width - 1, m_picSample.Height - 1
                );

            int xOffset = m_picSample.Width / 2;
            int yOffset = m_picSample.Height / 2;

            m_frames[ m_curFrame ].Render( g, xOffset, yOffset );

            g.Dispose();
            m_picSample.BackgroundImage = frame;

            // Advance to the next frame.
            m_curFrame++;
            if( m_curFrame >= m_frames.Count ) 
            {
                m_tmrAnimate.Enabled = false;
                m_curFrame = 0;
                m_cmdPreview.Enabled = true;
            }
        }

        /// <summary>
        /// The layout style changed.
        /// </summary>
        private void LayoutStyleChanged( 
            object sender, 
            System.EventArgs e 
            )
        {
            m_animation.LayoutAnimator = (IFrameLayoutManager)
                (m_cboLayout.SelectedItem as ComboItem).Value;

            m_animation.ClearAnimation();
            m_frames = null;
        }

        /// <summary>
        /// The effect style changed.
        /// </summary>
        private void EffectStyleChanged(
            object sender, 
            System.EventArgs e
            )
        {
            m_animation.FrameImageEffect = (IFrameModifier)
                (m_cboEffect.SelectedItem as ComboItem).Value;

            m_animation.ClearAnimation();
            m_frames = null;
        }

        /// <summary>
        /// The number of frames to render has changed.
        /// </summary>
        private void FramesToRenderChanged(
            object sender, 
            System.EventArgs e
            )
        {
            if( m_nudFrames.Value != m_animation.FramesToRender ) 
            {
                m_animation.FramesToRender = 
                    (int)m_nudFrames.Value;

                m_animation.ClearAnimation();
                m_frames = null;
            }
        }

        /// <summary>
        /// Stores a combo box item.
        /// </summary>
        private class ComboItem 
        {
            public string Name;
            public object Value;

            public ComboItem( string name, object value ) 
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return Name;
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }
            
            public override bool Equals( object obj )
            {
                if( obj == null || !(obj is ComboItem) )
                    return false;

                else if( Value == null )
                    return (obj as ComboItem).Value == null;

                else if( (obj as ComboItem).Value == null )
                    return false;

                else
                    return Value.GetType().Equals( (obj as ComboItem).Value.GetType() );
            }
        }
	}
}
