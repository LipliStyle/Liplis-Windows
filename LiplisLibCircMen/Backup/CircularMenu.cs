using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CircularMenu
{
    #region Circular Menu Control

    /// <summary>
    /// Provides functionality for the display of a "circular" menu, an
    /// iconic menu that appears as a circle around the user's mouse
    /// position.
    /// </summary>
    /// <remarks>
    /// <p>The circular menu class provides six important properties.  
    /// <see cref="MenuOptions"/> provides a 
    /// <see cref="MenuOptionCollection"/> that collects the actual items that 
    /// will be displayed in the menu.  <see cref="OpeningAnimation"/> and 
    /// <see cref="ClosingAnimation"/> provide the animations for the menu, 
    /// while <see cref="Radius"/> defines the overall size of the menu.  
    /// Finally, <see cref="ToolTip"/> exposes a set of options for 
    /// controlling how the menu’s tool tips are displayed.
    /// </p>
    /// <p>In addition to these, there are also two properties that allow you 
    /// to override the default tool-tip behavior with the 
    /// <see cref="BelowMenuToolTipRenderer"/> or with a custom tool-tip 
    /// implementation.  To override the default behavior, set the 
    /// <see cref="ToolTipOverride"/> property to a non-null object that 
    /// implements the <see cref="IToolTipRenderer"/> interface.  When 
    /// <see cref="ToolTipOverride "/> is <c>null</c> (<c>Nothing</c> in 
    /// Visual Basic), the default tool-tip behavior as defined by the 
    /// <see cref="ToolTip"/> property is used instead.  You can obtain a 
    /// reference to the actual tool-tip renderer used by checking the value 
    /// of the <see cref="ActualRenderer"/> property.
    /// </p>
    /// <p>CircularMenuPopup exposes a number of useful methods.  The most 
    /// important of these is undoubtedly the <see cref="Popup"/> method.  
    /// This method, which provides four overrides, is called when you wish 
    /// the pop-up menu to be displayed and the user to select an option.  
    /// You can provide either the exact screen coordinates of the menu’s 
    /// central pixel, or you can provide the coordinates of that location 
    /// relative to the edge of a control (such as your main form).
    /// </p>
    /// <p>You can control the cached menu animations via the 
    /// <see cref="CacheAnimations"/> and 
    /// <see cref="ClearAnimationCaches"/> methods.  The first of these builds 
    /// and stores the menu animations, while the second clears them.  
    /// Use <see cref="CacheAnimations"/> to preload the animations when your 
    /// application starts, instead of the first time the pop-up is shown.  
    /// Use <see cref="ClearAnimationCaches"/> to reset the caches when you 
    /// change an option that would change how the menu is rendered.
    /// </p>
    /// </remarks>
    [
    ToolboxBitmap( typeof( CircularMenuPopup ), "Icon.bmp" ),
    DefaultEvent( "OptionSelected" )
    ]
    public class CircularMenuPopup : Component
    {   
        #region Private Data Members

        /// <summary>
        /// The position of the menu on the screen.
        /// </summary>
        private Point m_menuScreenPos;

        /// <summary>
        /// Our menu options.
        /// </summary>
        private MenuOptionCollection m_options = new MenuOptionCollection();
    
        /// <summary>
        /// Our radius.
        /// </summary>
        private int m_radius = 50;

        /// <summary>
        /// Our tool-tip options.
        /// </summary>
        private StandardToolTipRenderer m_toolTips = 
            new StandardToolTipRenderer();

        /// <summary>
        /// Our opening animation.
        /// </summary>
        private MenuAnimation m_openingAnimation = 
            new ForwardMenuAnimation();

        /// <summary>
        /// Our closing animation.
        /// </summary>
        private MenuAnimation m_closingAnimation = 
            new ReverseMenuAnimation();

        /// <summary>
        /// Tracks a currently visible popup.
        /// </summary>
        private CircularMenuWindow m_currentPopup;

        /// <summary>
        /// Our default object for tool-tip rendering.
        /// </summary>
        private StandardToolTipRenderer m_standardToolTip =
            new StandardToolTipRenderer();

        /// <summary>
        /// An object that can override the default tool tip renderer with
        /// extended options.
        /// </summary>
        private IToolTipRenderer m_rendererOverride = null;

        #endregion

        #region Events

        /// <summary>
        /// Fired when the menu is shown.  Will report the selected option as
        /// <c>null</c>.
        /// </summary>
        [Description( "Fired when the menu is shown." )]
        public event MenuEventHandler MenuShown;

        /// <summary>
        /// Fired when a menu option has been selected, and the menu is closed.
        /// This event will be fired before <see cref="MenuClosed"/>.  It will
        /// not be fired if the menu was canceled.
        /// </summary>
        [Description( "Fired when a menu option has been selected." )]
        public event MenuEventHandler OptionSelected;

        /// <summary>
        /// Fired when the user cancels a menu (closes the menu without 
        /// selecting an option).  It will be fired before
        /// <see cref="MenuClosed"/>.
        /// </summary>
        [Description( "Fired when the menu closes without an option having been selected." )]
        public event MenuEventHandler MenuCanceled;

        /// <summary>
        /// Fired when the user closes the menu.  The 
        /// <see cref="MenuEventArgs.SelectedOption"/> property of the supplied
        /// event arguments will be <c>null</c> if the menu was canceled, and
        /// the selected option otherwise.  This event will be fired after
        /// any events of this class, and after the selected event for the
        /// given menu option.
        /// </summary>
        [Description( "Fired when the menu is closed (with or without a selected option)." )]
        public event MenuEventHandler MenuClosed;

        #endregion

        #region Methods and Properties

        /// <summary>
        /// Invokes the MenuShown event.
        /// </summary>
        private void RaiseMenuShown( Point screenPos ) 
        {
            m_menuScreenPos = screenPos;
            if( MenuShown != null )
                MenuShown.DynamicInvoke( 
                    new object[] {
                                     this,
                                     new MenuEventArgs( m_menuScreenPos, null )
                                 }
                    );
        }

        /// <summary>
        /// Invokes the MenuCanceled event.
        /// </summary>
        private void RaiseMenuCanceled() 
        {
            if( MenuCanceled != null ) 
                MenuCanceled.DynamicInvoke(
                    new object[] {
                                     this,
                                     new MenuEventArgs( m_menuScreenPos, null )
                                 }
                    );
        }

        /// <summary>
        /// Invokes the OptionSelected event.
        /// </summary>
        private void RaiseOptionSelected( MenuOption selected ) 
        {
            if( OptionSelected != null )
                OptionSelected.DynamicInvoke(
                    new object[] {
                                     this,
                                     new MenuEventArgs( 
                                        m_menuScreenPos, 
                                        selected 
                                     )
                                 }
                    );
        }

        /// <summary>
        /// Invokes the MenuClosed event.
        /// </summary>
        private void RaiseMenuClosed( MenuOption selected ) 
        {
            if( MenuClosed != null )
                MenuClosed.DynamicInvoke(
                    new object[] {
                                     this,
                                     new MenuEventArgs(
                                        m_menuScreenPos,
                                        selected
                                     )
                                 }
                    );
        }

        /// <summary>
        /// Clears the cached animation for both menu animations.
        /// </summary>
        public void ClearAnimationCaches() 
        {
            m_openingAnimation.ClearAnimation();
            m_closingAnimation.ClearAnimation();
        }

        /// <summary>
        /// Builds the cached animations for both the opening and closing
        /// animations.  Clears caches before rebuilding them.
        /// </summary>
        public void CacheAnimations() 
        {
            ClearAnimationCaches();
            m_openingAnimation.GetAnimation( m_options, m_radius );
            m_closingAnimation.GetAnimation( m_options, m_radius );
        }

        /// <summary>
        /// The options that can be displayed on this menu.  Cannot be null.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "The options that can be displayed on this menu." ),
        DesignerSerializationVisibility( DesignerSerializationVisibility.Content )
        ]
        public MenuOptionCollection MenuOptions { 
            get { return m_options; } 
            set 
            {
                if( value == null )
                    throw new ArgumentNullException();
                else
                    m_options = value;
            }
        }

        /// <summary>
        /// Provides options related to the default rendering of tool tips 
        /// within the menu.
        /// </summary>
        [
        Description( "Options for default tool-tip rendering.  These options can be modified using the ToolTipOverride property." ),
        DesignerSerializationVisibility( DesignerSerializationVisibility.Content )
        ]
        public StandardToolTipRenderer ToolTip 
        {
            get { return m_standardToolTip; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "ToolTip",
                        "You must provide a set of tool-tip options"
                        );

                else
                    m_standardToolTip = value;
            }
        }

        /// <summary>
        /// A tool-tip renderer to use instead of the default one provided by
        /// the <see cref="ToolTip"/> property.  If null, the 
        /// <see cref="ToolTip"/> renderer will be used.  Otherwise, this 
        /// renderer will be used.
        /// </summary>
        [Browsable( false )]
        public IToolTipRenderer ToolTipOverride 
        {
            get { return m_rendererOverride; }
            set { m_rendererOverride = value; }
        }

        /// <summary>
        /// Defines the actual tool tip renderer to use.
        /// </summary>
        [Browsable( false )]
        public IToolTipRenderer ActualRenderer 
        {
            get 
            {
                return (m_rendererOverride == null)?ToolTip:ToolTipOverride;
            }
        }

        /// <summary>
        /// The radius of the menu's layout.
        /// </summary>
        [
        Description( "The radius of the menu layout." ),
        DefaultValue( 50 )
        ]
        public int Radius 
        {
            get { return m_radius; }
            set { m_radius = value; }
        }

        /// <summary>
        /// The animation to play when opening this menu.  Cannot be null.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "The animation to play when opening this menu." ),
        DesignerSerializationVisibility( DesignerSerializationVisibility.Content )
        ]
        public MenuAnimation OpeningAnimation 
        {
            get { return m_openingAnimation; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException();
                else
                    m_openingAnimation = value;
            }
        }

        /// <summary>
        /// The animation to play when closing this menu.  Cannot be null.
        /// </summary>
        [
        Description( "The animation to play when closing this menu." ),
        DesignerSerializationVisibility( DesignerSerializationVisibility.Content )
        ]
        public MenuAnimation ClosingAnimation
        {
            get { return m_closingAnimation; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException();
                else
                    m_closingAnimation = value;
            }
        }

        /// <summary>
        /// Determines if the popup menu is currently shown.
        /// </summary>
        public bool IsPopupShown { get { return m_currentPopup != null; } }

        /// <summary>
        /// Indicates that a the popup window has been closed, and provides the
        /// selected option.
        /// </summary>
        internal void PopupComplete( MenuOption selectedOption ) 
        {
            m_currentPopup = null;

            if( selectedOption == null )
                RaiseMenuCanceled();
            else
                RaiseOptionSelected( selectedOption );

            RaiseMenuClosed( selectedOption );
        }

        /// <summary>
        /// Shows the menu popup centered at the given screen position.  If the
        /// popup is currently shown (see <see cref="IsPopupShown"/>), this
        /// method does nothing.
        /// </summary>
        /// <param name="screenPos">
        /// The screen position around which the popup menu should be centered.
        /// </param>
        public void Popup( Point screenPos ) 
        {
            if( m_currentPopup == null ) 
            {
                RaiseMenuShown( screenPos );
                
                CircularMenuWindow wnd = 
                    new CircularMenuWindow( this, screenPos );

                m_currentPopup = wnd;
                wnd.Show();
            }
        }

        /// <summary>
        /// Shows the menu popup centered at the given screen position.  If the
        /// popup is currently shown (see <see cref="IsPopupShown"/>), this
        /// method does nothing.
        /// </summary>
        public void Popup( int screenX, int screenY ) 
        {
            Popup( new Point( screenX, screenY ) );
        }

        /// <summary>
        /// Shows the menu popup centered at the given point on the given 
        /// control.  If a popup is currently shown (see 
        /// <see cref="IsPopupShown"/>), this method does nothing.
        /// </summary>
        /// <param name="control">
        /// The form on which the popup will be displayed.  Cannot be null.
        /// </param>
        /// <param name="centerPos">
        /// The position, relative to the corner's position, where the popup
        /// should be displayed.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided control is a null reference.
        /// </exception>
        public void Popup( Control control, Point centerPos ) 
        {
            if( control == null ) 
                throw new ArgumentNullException(
                    "control",
                    "You must provide a control."
                    );

            else 
                Popup( control.PointToScreen(
                    centerPos
                    )
                    );
        }

        /// <summary>
        /// Shows the menu popup centered at the given point on the given 
        /// control.
        /// </summary>
        /// <param name="control">
        /// The form on which the popup will be displayed.  Cannot be null.
        /// </param>
        /// <param name="centerX">
        /// The x-coordinate on control where the popup menu should be 
        /// centered.
        /// </param>
        /// <param name="centerY">
        /// The y-coordinate on control where the popup menu should be
        /// centered.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided control is a null reference.
        /// </exception>
        public void Popup( Control control, int centerX, int centerY ) 
        {
            Popup( control, new Point( centerX, centerY ) );
        }

        #endregion
    }

    #endregion

    #region Menu Event Args

    /// <summary>
    /// This class provides event arguments specific to menu-level events.
    /// </summary>
    public class MenuEventArgs : EventArgs 
    {
        private Point m_menuPos;
        private MenuOption m_selectedOption;

        /// <summary>
        /// Creates a new instance of the menu event arguments class.
        /// </summary>
        /// <param name="menuPos">
        /// The position of the shown menu, in screen coordinates.
        /// </param>
        /// <param name="selectedOption">
        /// The option from the menu that was selected.  Can be <c>null</c> if
        /// not applicable or if no option was selected.
        /// </param>
        public MenuEventArgs(
            Point menuPos,
            MenuOption selectedOption
            ) 
        {
            m_menuPos = menuPos;
            m_selectedOption = selectedOption;
        }

        /// <summary>
        /// The position of the shown menu, in screen coordinates.
        /// </summary>
        public Point MenuPos 
        {
            get { return m_menuPos; }
        }

        /// <summary>
        /// Returns the option selected from the menu, or <c>null</c> if not
        /// applicable or if no option was selected.
        /// </summary>
        public MenuOption SelectedOption 
        {
            get { return m_selectedOption; }
        }
    }

    #endregion

    #region Menu Event Handler

    /// <summary>
    /// Defines an event handler for a menu event.
    /// </summary>
    public delegate void MenuEventHandler( object sender, MenuEventArgs args );

    #endregion
    
}
