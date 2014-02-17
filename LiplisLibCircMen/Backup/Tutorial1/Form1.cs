using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Tutorial1
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        private CircularMenu.CircularMenuPopup circularMenuPopup1;
        private CircularMenu.MenuOption menuOption1;
        private CircularMenu.MenuOption menuOption2;
        private CircularMenu.MenuOption menuOption3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
            this.circularMenuPopup1 = new CircularMenu.CircularMenuPopup();
            this.menuOption1 = new CircularMenu.MenuOption();
            this.menuOption2 = new CircularMenu.MenuOption();
            this.menuOption3 = new CircularMenu.MenuOption();
            // 
            // circularMenuPopup1
            // 
            this.circularMenuPopup1.ClosingAnimation.FrameImageEffect = ((CircularMenu.IFrameModifier)(resources.GetObject("resource.FrameImageEffect")));
            this.circularMenuPopup1.ClosingAnimation.LayoutAnimator = ((CircularMenu.IFrameLayoutManager)(resources.GetObject("resource.LayoutAnimator")));
            this.circularMenuPopup1.MenuOptions.Add(this.menuOption1);
            this.circularMenuPopup1.MenuOptions.Add(this.menuOption2);
            this.circularMenuPopup1.MenuOptions.Add(this.menuOption3);
            this.circularMenuPopup1.OpeningAnimation.FrameImageEffect = ((CircularMenu.IFrameModifier)(resources.GetObject("resource.FrameImageEffect1")));
            this.circularMenuPopup1.OpeningAnimation.FramesToRender = 30;
            this.circularMenuPopup1.OpeningAnimation.LayoutAnimator = ((CircularMenu.IFrameLayoutManager)(resources.GetObject("resource.LayoutAnimator1")));
            this.circularMenuPopup1.Radius = 25;
            this.circularMenuPopup1.ToolTip.BackgroundColor = System.Drawing.SystemColors.Info;
            this.circularMenuPopup1.ToolTip.BackgroundOpacity = ((System.Byte)(175));
            this.circularMenuPopup1.ToolTip.BorderColor = System.Drawing.SystemColors.InfoText;
            this.circularMenuPopup1.ToolTip.BorderOpacity = ((System.Byte)(255));
            this.circularMenuPopup1.ToolTip.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.circularMenuPopup1.ToolTip.ForegroundColor = System.Drawing.SystemColors.InfoText;
            this.circularMenuPopup1.ToolTip.ForegroundOpacity = ((System.Byte)(255));
            this.circularMenuPopup1.ToolTipOverride = null;
            // 
            // menuOption1
            // 
            this.menuOption1.DisabledImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption1.DisabledImage.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image")));
            this.menuOption1.DisabledImage.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.menuOption1.HoverImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption1.HoverImage.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image1")));
            this.menuOption1.HoverImage.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.menuOption1.Image.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption1.Image.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image2")));
            this.menuOption1.Image.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.menuOption1.PressedImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption1.PressedImage.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image3")));
            this.menuOption1.PressedImage.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.menuOption1.Click += new System.EventHandler(this.menuOption1_Click);
            // 
            // menuOption2
            // 
            this.menuOption2.DisabledImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption2.DisabledImage.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image4")));
            this.menuOption2.DisabledImage.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.menuOption2.HoverImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption2.HoverImage.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image5")));
            this.menuOption2.HoverImage.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.menuOption2.Image.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption2.Image.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image6")));
            this.menuOption2.Image.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.menuOption2.PressedImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption2.PressedImage.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image7")));
            this.menuOption2.PressedImage.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            // 
            // menuOption3
            // 
            this.menuOption3.DisabledImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption3.DisabledImage.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image8")));
            this.menuOption3.DisabledImage.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.menuOption3.HoverImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption3.HoverImage.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image9")));
            this.menuOption3.HoverImage.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.menuOption3.Image.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption3.Image.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image10")));
            this.menuOption3.Image.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            this.menuOption3.PressedImage.DropShadow.ShadowColor = System.Drawing.Color.Black;
            this.menuOption3.PressedImage.Image = ((System.Drawing.Bitmap)(resources.GetObject("resource.Image11")));
            this.menuOption3.PressedImage.TransparencyKey = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        private void Form1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if( e.Button == MouseButtons.Right )
                circularMenuPopup1.Popup( this, e.X, e.Y );
        }

        private void menuOption1_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog( this );
        }
	}
}
