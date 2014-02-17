using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CircularMenu
{
    /// <summary>
    /// Summary description for CircularMenuWindow.
    /// </summary>
    public class CircularMenuWindow : System.Windows.Forms.Form
    {
        ///============================
        /// 汎用デリゲート
        public delegate void dlgVoidToVoid();

        #region Static

        /// <summary>
        /// Gets and returns the screen containing the given pixel, or
        /// null if no such screen exists.
        /// </summary>
        public static Screen GetScreen( Point position ) 
        {
            foreach( Screen s in Screen.AllScreens )
                if( s.Bounds.Contains( position ) )
                    return s;
            
            return null;
        }

        #endregion

        #region Animation Fields

        /// <summary>
        /// The menu to animate.
        /// </summary>
        private CircularMenuPopup m_menu;

        /// <summary>
        /// Our animation.
        /// </summary>
        private FrameCollection m_animation;

        /// <summary>
        /// The central pixel in the menu animation.
        /// </summary>
        private int m_centerX, m_centerY;

        /// <summary>
        /// Our closing animation.
        /// </summary>
        private FrameCollection m_closeAnimation;

        /// <summary>
        /// The animation background.
        /// </summary>
        private Bitmap m_controlBackground;

        /// <summary>
        /// Determines if the control is done animating.
        /// </summary>
        private bool m_isReady;

        /// <summary>
        /// The region wherein the tool tip will be drawn.
        /// </summary>
        private Rectangle m_toolTipArea;

        /// <summary>
        /// The region that the final frame of the animation is fully
        /// contained within.
        /// </summary>
        private Rectangle m_finalFrameRegion;

        /// <summary>
        /// The region that the menu will occupy during use.
        /// </summary>
        private Rectangle m_menuRegion;

        /// <summary>
        /// The current frame to render.
        /// </summary>
        private Frame m_currentFrame = null;

        /// <summary>
        /// Our animator.
        /// </summary>
        private Animator m_animator = null;

        #endregion

        #region Tracking Fields

        /// <summary>
        /// The option selected by the most recently tracked mouse coordinates.
        /// null if the most recent selection is no option.
        /// </summary>
        private MenuOption m_lastHoverOption = null;

        /// <summary>
        /// The most recent known coordinate of the mouse, relative to the 
        /// upper-left hand corner of the window.
        /// </summary>
        private int m_mouseX;

        /// <summary>
        /// The most recent known coordinate of the mouse, relative to the 
        /// upper-left hand corner of the window.
        /// </summary>
        private int m_mouseY;

        /// <summary>
        /// The coordinate where the user depressed the mouse button.
        /// </summary>
        private int m_downX;

        /// <summary>
        /// The coordinate where the user depressed the mouse button.
        /// </summary>
        private int m_downY;

        /// <summary>
        /// Determines if the user is currently depressing the mouse button.
        /// </summary>
        private bool m_pressing = false;

        /// <summary>
        /// Determines the option the user pressed.
        /// </summary>
        private MenuOption m_pressedOption = null;

        /// <summary>
        /// Stores the option actually selected by the user.
        /// </summary>
        private MenuOption m_selectedOption = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new circular menu window for the provided menu.
        /// </summary>
        /// <param name="menu">
        /// The menu to animate.  Cannot be null.
        /// </param>
        /// <param name="position">
        /// The screen coordinates of central pixel of the animation.  The
        /// window will position itself around this point while ensuring
        /// that the entire menu can still be shown on the screen.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided menu is a null reference.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided menu does not have any visible options, or
        /// if the provided screen coordinate does not exist on any screen.
        /// </exception>
        public CircularMenuWindow( CircularMenuPopup menu, Point position )
        {
            InitializeComponent();
            if( menu == null ) throw new ArgumentNullException();

            // Set up our animation.
            #region ...

            m_menu = menu;
            MenuOptionCollection options = menu.MenuOptions;
            m_animation = menu.OpeningAnimation.GetAnimation( options, menu.Radius );
            m_closeAnimation = menu.ClosingAnimation.GetAnimation( options, menu.Radius );
            m_isReady = false;

            #endregion

            // Position ourselves.
            #region ...

            Screen s = GetScreen( position );
            if( s == null ) throw new ArgumentException(
                                "The provided point does not exist on any screen",
                                "position"
                                );

            Rectangle ourPos = new Rectangle(
                position.X - m_animation.FinalFrame.Bounds.Width / 2,
                position.Y - m_animation.FinalFrame.Bounds.Height / 2,
                m_animation.FinalFrame.Bounds.Width,
                m_animation.FinalFrame.Bounds.Height
                );

            #endregion

            // Adjust our boundaries to allow for specialized tool tip 
            // renderers.
            #region ...

            Rectangle extend = 
                new Rectangle( 0, 0, ourPos.Width, ourPos.Height );

            Rectangle rawToolTip = extend;

            m_centerX = ourPos.Width / 2;
            m_centerY = ourPos.Height / 2;

            if( m_menu.ActualRenderer is IExtraSpaceToolTipRenderer ) 
            {
                Bitmap temp = 
                    new Bitmap( this.Bounds.Width, this.Bounds.Height );

                using( Graphics tempG = Graphics.FromImage( temp ) ) 
                {
                    extend = (m_menu.ActualRenderer as IExtraSpaceToolTipRenderer ).GetMaximumRenderArea(
                        tempG,
                        new Point( m_centerX, m_centerY ),
                        ourPos.Size
                        );

                    rawToolTip = extend;
                }
                temp.Dispose();

                // Position "extend" on the screen.
                extend.X += ourPos.Left;
                extend.Y += ourPos.Top;

                // Incorporate "extend" into our region.
                extend = Rectangle.Union( extend, ourPos );
                m_centerX += (ourPos.Left - extend.Left);
                m_centerY += (ourPos.Top - extend.Top);

                ourPos = extend;
            }

            // Make sure we're on the screen.
            if( ourPos.Width > s.WorkingArea.Width ) 
                ourPos.Width = s.WorkingArea.Width;
            if( ourPos.Height > s.WorkingArea.Height )
                ourPos.Height = s.WorkingArea.Height;

            if( ourPos.Left < s.WorkingArea.Left )
                ourPos.X = s.WorkingArea.Left;
            if( ourPos.Top < s.WorkingArea.Top )
                ourPos.Y = s.WorkingArea.Top;

            if( ourPos.Right > s.WorkingArea.Right )
                ourPos.X = s.WorkingArea.Right - ourPos.Width;
            if( ourPos.Bottom > s.WorkingArea.Bottom )
                ourPos.Y = s.WorkingArea.Bottom - ourPos.Height;

            // We have it.
            m_finalFrameRegion = ourPos;

            // Set the tool tip.
            m_toolTipArea = rawToolTip;
            if( m_toolTipArea.X < 0 ) m_toolTipArea.X += (-m_toolTipArea.X );
            if( m_toolTipArea.Y < 0 ) m_toolTipArea.Y += (-m_toolTipArea.Y );

            #endregion

            // Get our overall boundaries.
            #region ...

            int centerX = ourPos.Left + m_centerX;
            int centerY = ourPos.Top + m_centerY;

            int left = ourPos.Left;
            int top = ourPos.Top;
            int right = ourPos.Right;
            int bottom = ourPos.Bottom;

            foreach( Frame f in m_animation ) 
            {
                int fleft = f.Bounds.Left + centerX;
                int ftop = f.Bounds.Top + centerY;
                int fright = f.Bounds.Right + centerX;
                int fbottom = f.Bounds.Bottom + centerY;

                if( fleft < left ) left = fleft;
                if( ftop < top ) top = ftop;
                if( fright > right ) right = fright;
                if( fbottom > bottom ) bottom = fbottom;
            }
            foreach( Frame f in m_closeAnimation ) 
            {
                int fleft = f.Bounds.Left + centerX;
                int ftop = f.Bounds.Top + centerY;
                int fright = f.Bounds.Right + centerX;
                int fbottom = f.Bounds.Bottom + centerY;

                if( fleft < left ) left = fleft;
                if( ftop < top ) top = ftop;
                if( fright > right ) right = fright;
                if( fbottom > bottom ) bottom = fbottom;
            }

            // We have to be at least 100x100.
            if( right - left < 75 )
            {
                left -= 50;
                right += 50;

                m_centerX += 50;
            }
            if( bottom - top < 75 )
            {
                top -= 50;
                bottom += 50;

                m_centerY += 50;
            }

            // Set our region. 
            m_menuRegion = Rectangle.FromLTRB( left, top, right, bottom );
            this.Bounds = m_menuRegion;

            #endregion

            // Create a bitmap to use.
            #region ...

            m_controlBackground = new Bitmap( this.Width, this.Height );

            #endregion

            // Start the animation.
            #region ...

            m_animator = new Animator( 
                this,
                m_animation,
                false
                );

            new Thread( new ThreadStart( m_animator.Start ) ).Start();

            #endregion
        }

        #endregion

        #region Painting

        /// <summary>
        /// Paints an individual frame.
        /// </summary>
        private void RepaintMenu()
        {
            // Are we disposing?
            if( Disposing ) return;

            // Get a graphics context to render to. 
            try 
            {
                Graphics g = Graphics.FromImage( m_controlBackground );
                g.Clear( Color.Transparent );

                if( m_isReady ) 
                {
                    // Paint the final frame. 
                    int x, y;
                    if( m_pressing ) 
                    {
                        if( m_pressedOption == m_lastHoverOption ) 
                        {
                            x = m_downX;
                            y = m_downY;
                        } 
                        else 
                        {
                            x = this.Width + 1;
                            y = this.Height + 1;
                        }
                    }
                    else 
                    {
                        x = m_mouseX;
                        y = m_mouseY;
                    }

                    // Paint the frame of the option currently selected or
                    // hovered over.
                    MenuOption opt = 
                        (m_pressedOption == null)?
                        m_lastHoverOption:
                        m_pressedOption;

                    if( opt != null && opt.ToolTip != null && opt.ToolTip != "" )
                        m_menu.ActualRenderer.Render(
                            g,
                            opt.ToolTip,
                            new Point( m_centerX, m_centerY ),
                            m_toolTipArea
                            );

                    else
                        m_menu.ActualRenderer.RenderEmpty(
                            g,
                            new Point( m_centerX, m_centerY ),
                            m_toolTipArea
                            );

                    // Paint the frame itself. 
                    m_animation.FinalFrame.Render(
                        g,
                        m_centerX,
                        m_centerY,
                        x,
                        y,
                        m_pressing
                        );
                }
                else 
                {
                    // Paint an intermittent frame.                
                    m_currentFrame.Render(
                        g,
                        m_centerX,
                        m_centerY
                        );
                }

                g.Dispose();
                SetBitmap( m_controlBackground );
            } 
            catch { /* Ignore errors */ }
        }

        /// <summary>
        /// Sets the bitmap to use for this form.
        /// </summary>
        private void SetBitmap( Bitmap bitmap )
        {
            // Collect some variables we'll use to render.
            IntPtr screenDc = GetDC( IntPtr.Zero );
            IntPtr memDc = CreateCompatibleDC( screenDc );
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try 
            {
                hBitmap = bitmap.GetHbitmap( Color.FromArgb(0) );  
                oldBitmap = SelectObject( memDc, hBitmap );

                W32Size size = new W32Size( bitmap.Width, bitmap.Height );
                W32Point pointSource = new W32Point( 0, 0 );
                W32Point topPos = new W32Point( this.Left, this.Top );
                
                BLENDFUNCTION blend = new BLENDFUNCTION();
                blend.BlendOp = 0x00; // AC_SRC_OVER
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = 255;
                blend.AlphaFormat = 0x01; // AC_SRC_ALPHA

                lock( this )
                    if( !Disposing )
                        UpdateLayeredWindow(
                            Handle, 
                            screenDc, 
                            ref topPos, 
                            ref size, 
                            memDc, 
                            ref pointSource, 
                            0, 
                            ref blend, 
                            (Int32) 0x00000002 // ULW_ALPHA
                            );
            }
            catch { /* Ignore errors */ }
            finally 
            {
                // Clean things up...
                ReleaseDC( IntPtr.Zero, screenDc );
                if( hBitmap != IntPtr.Zero )  
                {
                    SelectObject( memDc, oldBitmap );
                    DeleteObject( hBitmap );
                }
                DeleteDC( memDc );
            }
        }

        /// <summary>
        /// Sets some specific parameters we'll use to control rendering of
        /// our window.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get 
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000; // WS_EX_LAYERED 
                return cp;
            }
        }

        #endregion
        
        #region Menu Tracking Events

        /// <summary>
        /// Monitors Windows system messages for the message the indicates a
        /// window was selected.  If the selected window wasn't this window, 
        /// we'll close ourselves.
        /// </summary>
        protected override void WndProc( ref Message m )
        {
            //2012/06/16 フォーカスアウトで閉じないようにする
            //---------------------------------------------
            //// Did we get unselected? 
            //if( m.Msg == 0x86 ) 
            //    // Maybe... 
            //    if( m.WParam.ToInt32() == 0 ) 
            //        // Yup! 
            //        TriggerClose();
            //---------------------------------------------

            // Let the base class do its normal processing. 
            base.WndProc( ref m );
        }

        /// <summary>
        /// Closes this window and reports the selection to the parent menu
        /// component.
        /// </summary>
        private void CloseWindow() 
        {
            Invoke(new dlgVoidToVoid(this.Close));
            m_menu.PopupComplete( m_selectedOption );
        }

        /// <summary>
        /// Tracks mouse movement and determines if the window needs to be
        /// redrawn.
        /// </summary>
        private void TrackMouseMove(
            object sender, 
            System.Windows.Forms.MouseEventArgs e
            )
        {
            if( !m_isReady ) return;

            m_mouseX = e.X;
            m_mouseY = e.Y;

            MenuOption current = m_animation.FinalFrame.HitTest(
                m_centerX,
                m_centerY,
                e.X,
                e.Y
                );

            if( current != m_lastHoverOption ) 
            {
                // Events. 
                if( m_lastHoverOption != null )
                    m_lastHoverOption.RaiseEndHover();
                if( current != null )
                    current.RaiseStartHover();

                m_lastHoverOption = current;
                RepaintMenu();
            }
        }

        /// <summary>
        /// Indicates that no option should be highlighted.
        /// </summary>
        private void TrackMouseLeave(
            object sender, 
            System.EventArgs e
            )
        {
            if( !m_isReady ) return;

            m_mouseX = this.Width + 1;
            m_mouseY = this.Height + 1;

            if( m_lastHoverOption != null ) m_lastHoverOption.RaiseEndHover();
            m_lastHoverOption = null;
            RepaintMenu();
        }

        /// <summary>
        /// Tracks mouse depressions.  Clicking anywhere on the window that
        /// isn't an option automatically closes the window.
        /// </summary>
        private void TrackMouseDown(
            object sender, 
            System.Windows.Forms.MouseEventArgs e
            )
        {
            if (!m_isReady) return;

            m_pressing = true;
            m_downX = e.X;
            m_downY = e.Y;

            m_pressedOption = m_animation.FinalFrame.HitTest(
                m_centerX,
                m_centerY,
                e.X,
                e.Y
                );

            if (m_pressedOption == null)
            {
                // Immediately close the window. 
                //TriggerClose();
            }
            else
            {
                //RepaintMenu();
            }
        }

        /// <summary>
        /// Tracks mouse ups.  If the user mouse-ups over the same item they
        /// mouse down'd over, that option is selected and the menu closes.
        /// </summary>
        private void TrackMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //準備OK？
            if( !m_isReady ) return;

            //プレッシングフォルス
            m_pressing = false;

            if( m_pressedOption == m_lastHoverOption ) 
            {
                //押されたメニューオプションの取得
                m_selectedOption = m_pressedOption;

                //押されたメニューオプションのレイズエンドホーバー
                m_selectedOption.RaiseEndHover();

                //押されたメニューオプションのレイズクリック
                m_selectedOption.RaiseClick();

                // われわれを閉じる！
                m_lastHoverOption = null;

                //閉じるオプションが有効なメニューの場合閉じる
                if (m_selectedOption.m_click_close)
                {
                    TriggerClose();
                }
                //TriggerClose();
            }
            else 
            {
                // They canceled the selection. 
                //RepaintMenu();
            }
        }

        /// <summary>
        /// Triggers a closing of the window.
        /// </summary>
        private void TriggerClose() 
        {
            if( m_isReady && m_animator == null ) 
            {
                // Make sure we indicate we're no longer hovering. 
                if( m_lastHoverOption != null ) m_lastHoverOption.RaiseEndHover();

                // Trigger the close animation. 
                m_isReady = false;
                m_animator = new Animator(
                    this,
                    m_closeAnimation,
                    true
                    );

                new Thread( new ThreadStart( m_animator.Start ) ).Start();
            }
            else
            {
                // Close instantly once we're done. 
                if( m_animator != null ) m_animator.CloseOnDone = true;
            }
        }

        #endregion

        /// <summary>
        /// closeCircularMenuWindow
        /// クローズする
        /// </summary>
        #region closeCircularMenuWindow
        public void closeCircularMenuWindow()
        {
            Invoke(new dlgVoidToVoid(this.TriggerClose));
        }
        #endregion


        #region Properties

        /// <summary>
        /// This property can be used to test the selected menu option.
        /// It will be null if no option was selected (the menu was
        /// canceled).
        /// </summary>
        public MenuOption SelectedOption 
        {
            get { return m_selectedOption; }
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            base.Dispose( disposing );
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // CircularMenuWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(248, 224);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CircularMenuWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CircularMenuWindow";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrackMouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrackMouseUp);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TrackMouseMove);
            this.MouseLeave += new System.EventHandler(this.TrackMouseLeave);

        }
        #endregion

        #region Windows API

        /// <summary>
        /// A structure passed to the AlphaBlend method.
        /// </summary>
        [StructLayout( LayoutKind.Sequential, Pack=1 )]
            private struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        /// <summary>
        /// The data returned from UpdateLayeredWindow.
        /// </summary>
        private enum W32Bool
        {
            False= 0,
            True
        };

        /// <summary>
        /// A WIN32 point.
        /// </summary>
        [StructLayout( LayoutKind.Sequential )]
            private struct W32Point
        {
            public Int32 X;
            public Int32 Y;

            public W32Point( Int32 x, Int32 y ) 
            { X = x; Y = y; }
        }

        /// <summary>
        /// A WIN32 size structure.
        /// </summary>
        [StructLayout( LayoutKind.Sequential )]
            private struct W32Size 
        {
            public Int32 CX;
            public Int32 CY;

            public W32Size( Int32 cx, Int32 cy ) { CX = cx; CY = cy; }
        }

        /// <summary>
        /// Updates a layered window.
        /// </summary>
        [DllImport( "user32.dll", ExactSpelling=true, SetLastError=true )]
        private static extern W32Bool UpdateLayeredWindow(
            IntPtr hwnd, 
            IntPtr hdcDst, 
            ref W32Point pptDst, 
            ref W32Size psize, 
            IntPtr hdcSrc, 
            ref W32Point pprSrc, 
            Int32 crKey, 
            ref BLENDFUNCTION pblend, 
            Int32 dwFlags
            );

        /// <summary>
        /// Gets a device context.
        /// </summary>
        [DllImport( "user32.dll", ExactSpelling=true, SetLastError=true )]
        private static extern IntPtr GetDC( IntPtr hWnd );

        /// <summary>
        /// Releases a device context.
        /// </summary>
        [DllImport( "user32.dll", ExactSpelling=true )]
        private static extern int ReleaseDC( IntPtr hWnd, IntPtr hDC );

        /// <summary>
        /// Creates a device context that is compatable with the provided
        /// device context.
        /// </summary>
        [DllImport( "gdi32.dll", ExactSpelling=true, SetLastError=true )]
        private static extern IntPtr CreateCompatibleDC( IntPtr hDC );

        /// <summary>
        /// Deletes a device context.
        /// </summary>
        [DllImport( "gdi32.dll", ExactSpelling=true, SetLastError=true )]
        private static extern W32Bool DeleteDC( IntPtr hdc );

        /// <summary>
        /// Gets a GDI object.
        /// </summary>
        [DllImport( "gdi32.dll", ExactSpelling=true )]
        private static extern IntPtr SelectObject( IntPtr hDC, IntPtr hObject );

        /// <summary>
        /// Deletes a GDI object.
        /// </summary>
        [DllImport( "gdi32.dll", ExactSpelling=true, SetLastError=true )]
        private static extern W32Bool DeleteObject( IntPtr hObject );

        #endregion

        #region Animator

        /// <summary>
        /// Handles animation on a separate thread.
        /// </summary>
        private class Animator 
        {
            private CircularMenuWindow m_window;
            private FrameCollection m_animation;
            private bool m_closeOnDone;

            /// <summary>
            /// Creates a new animator.
            /// </summary>
            public Animator( 
                CircularMenuWindow window,
                FrameCollection animation,
                bool closeOnDone
                ) 
            {
                m_window = window;
                m_animation = animation;
                m_closeOnDone = closeOnDone;
            }

            /// <summary>
            /// Determines if the window will be closed when the animation
            /// completes.
            /// </summary>
            public bool CloseOnDone 
            {
                get { return m_closeOnDone; }
                set { m_closeOnDone = value; }
            }

            /// <summary>
            /// Animates!
            /// </summary>
            public void Start() 
            {
                m_window.m_isReady = false;
                int curFrame = 0;

                // A short initial pause.
                Thread.Sleep( 30 );

                // Animate normal-like.
                while( curFrame < m_animation.Count ) 
                {
                    // Render this current frame.
                    m_window.m_currentFrame = m_animation[ curFrame ];
                    m_window.Invoke( 
                        new System.Threading.ThreadStart( m_window.RepaintMenu ),
                        null 
                        );

                    // Advance to the next frame.
                    curFrame++;

                    // Wait for about 1/30th of a second.
                    Thread.Sleep( 30 );
                    if( m_window.Disposing ) curFrame = m_animation.Count + 1;
                }

                // All done.
                m_window.m_animator = null;

                if( m_closeOnDone ) 
                {
                    m_window.CloseWindow();
                    Thread.Sleep( 30 );
                }
                else  
                {
                    m_window.m_isReady = true;
                    m_window.RepaintMenu();
                }
            }
        }

        #endregion
    }
}