using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using PixelEffects;

namespace CircularMenu
{
    #region Menu Option Collection

    /// <summary>
    /// Defines a collection of menu options.
    /// </summary>
    [
    Serializable,
    Editor( typeof( CollectionEditor ), typeof( UITypeEditor ) )
    ]
    public class MenuOptionCollection : CollectionBase
    {
        /// <summary>
        /// Initializes a new collection, copying only the visible elements
        /// from the provided collection.
        /// </summary>
        private MenuOptionCollection( MenuOptionCollection getVisibles ) 
        {
            foreach( MenuOption option in getVisibles )
                if( option.Visible ) InnerList.Add( option );
        }

        /// <summary>
        /// Initializes a new collection.
        /// </summary>
        public MenuOptionCollection() {}

        /// <summary>
        /// Creates a new menu option and adds it to the list.  Returns the
        /// created option.
        /// </summary>
        public MenuOption Add() 
        {
            MenuOption newOption = new MenuOption();
            Add( newOption );

            return newOption;
        }

        /// <summary>
        /// Adds a menu option to the collection.
        /// </summary>
        /// <remarks>
        /// Null options are ignored.
        /// </remarks>
        public int Add( MenuOption option ) 
        {
            return InnerList.Add( option );
        }

        /// <summary>
        /// Adds a range of options to the collections.  None of the options
        /// can be null.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if an attempt is made to add any null or non MenuOption 
        /// items to the collection.
        /// </exception>
        public void AddRange( ICollection c ) 
        {
            foreach( object o in c )
                if( o == null || !(o is MenuOption ) )
                    throw new InvalidOperationException();

            InnerList.AddRange( c );
        }

        /// <summary>
        /// Removes the provided option.
        /// </summary>
        public void Remove( MenuOption option ) 
        {
            InnerList.Remove( option );
        }

        /// <summary>
        /// Determines if this collection contains the provided option.
        /// </summary>
        public bool Contains( MenuOption option ) 
        {
            return InnerList.Contains( option );
        }

        /// <summary>
        /// Determines the index of the provided option. 
        /// </summary>
        public int IndexOf( MenuOption option ) 
        {
            return InnerList.IndexOf( option );
        }

        /// <summary>
        /// Returns a subset of this collection that contains only its visible
        /// elements.
        /// </summary>
        public MenuOptionCollection GetVisibleOptions() 
        {
            return new MenuOptionCollection( this );
        }

        /// <summary>
        /// Returns the menu option at the provided index.
        /// </summary>
        public MenuOption this[ int index ] 
        {
            get { return (MenuOption)InnerList[ index ]; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException();

                else
                    InnerList[ index ] = value;
            }
        }
    }

    #endregion

    #region Menu Option

    /// <summary>
    /// Defines an individual option in a circular menu.
    /// </summary>
    /// <remarks>
    /// <p>An option’s <see cref="Image"/> property is rendered whenever the 
    /// user is not currently hovering over the option, and the option is 
    /// enabled.  Conversely, when the user is hovering over the option but 
    /// not actually depressing it, the <see cref="HoverImage"/> property is 
    /// shown.  The <see cref="PressedImage"/> is shown when the user is 
    /// depressing an enabled option, and the <see cref="DisabledImage"/> is 
    /// shown regardless of the mouse state when an option is not enabled.  
    /// </p>
    /// <p>You can view the actual images shown for each of the particular 
    /// states by accessing the <see cref="CachedPrimaryImage"/> property for 
    /// the normal state, the <see cref="CachedPrimaryHoverImage"/> property 
    /// for the hover state, and the <see cref="CachedPrimaryPressedImage"/> 
    /// property for the pressed state.  Each of these properties either 
    /// returns the <see cref="DisabledImage"/> property or their counterpart.
    /// </p>
    /// <p>It is via instances of the MenuOption class that a client 
    /// application can actually track when options are clicked in a shown 
    /// pop-up.  This is accomplished via the <see cref="Click"/>, 
    /// <see cref="StartHover"/>, and <see cref="EndHover"/> events.  These 
    /// events are raised when the user selects a menu option, hovers the 
    /// mouse over the option, or moves the mouse away from the option, 
    /// respectively.  Each event provides an instance to the source menu 
    /// option instance and <see cref="System.EventArgs.Empty"/> as 
    /// arguments.
    /// </p>
    /// </remarks>
    [
    Serializable,
    TypeConverter( typeof( ExpandableObjectConverter ) ),
    ToolboxBitmap( typeof( MenuOption ), "OrbSmall.bmp" ),
    DefaultEvent( "Click" )
    ]
    public class MenuOption : Component
    {
        #region Fields

        /// <summary>
        /// A tool tip to show when the mouse hovers over the item.
        /// </summary>
        private string m_toolTip = "";

        /// <summary>
        /// Is the control visible?
        /// </summary>
        private bool m_visible = true;

        /// <summary>
        /// Is the control enabled?
        /// </summary>
        private bool m_enabled = true;

        /// <summary>
        /// Defines the normal image for this option.
        /// </summary>
        private MenuOptionImage m_image = new MenuOptionImage();

        /// <summary>
        /// The image to render when hovering over the menu option.
        /// </summary>
        private MenuOptionImage m_hoverImage = new MenuOptionImage();

        /// <summary>
        /// The image to render when the menu option is pressed.
        /// </summary>
        private MenuOptionImage m_pressedImage = new MenuOptionImage();

        /// <summary>
        /// The image to render when the menu option is disabled.
        /// </summary>
        private MenuOptionImage m_disabledImage = new MenuOptionImage();

        #endregion

        #region Events

        /// <summary>
        /// This event is raised when the user is first hovers over this
        /// option.
        /// </summary>
        public event EventHandler StartHover;

        /// <summary>
        /// This event is raised when the user stops hovering over an option.
        /// It will be raised before the <see cref="Click"/> event, if the
        /// Click event will be raised.
        /// </summary>
        public event EventHandler EndHover;

        /// <summary>
        /// This event is raised when the user clicks this option from a 
        /// menu.
        /// </summary>
        public event EventHandler Click;

        #endregion

        #region Properties

        /// <summary>
        /// Determines if this menu option is visible.
        /// </summary>
        [
        Description( "Determines if this option is visible." ),
        DefaultValue( true )
        ]
        public bool Visible 
        {
            get { return true; } 
            set { m_visible = value; }
        }

        /// <summary>
        /// A small "tool tip" that is drawn whenever the user hovers over the
        /// menu.  Cannot be null, but if "", no tool tip will be drawn.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "A short string to display when the user hovers over the menu option." ),
        DefaultValue( "" )
        ]
        public string ToolTip 
        {
            get { return m_toolTip; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "ToolTip",
                        "You must provide a value for the ToolTip property"
                        );
                else
                    m_toolTip = value;
            }
        }

        /// <summary>
        /// Determines if this menu option is enabled.
        /// </summary>
        [
        Description( "Determines if this option is enabled." ),
        DefaultValue( true )
        ]
        public bool Enabled { 
            get { return m_enabled; } 
            set { m_enabled = value; }
        }

        /// <summary>
        /// The image rendered when the option is in its normal state.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "The image shown when the option is in its normal state." ),
        DesignerSerializationVisibility( DesignerSerializationVisibility.Content )
        ]
        public MenuOptionImage Image 
        {
            get { return m_image; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "Image",
                        "You must specify an image"
                        );

                else
                    m_image = value;
            }
        }

        /// <summary>
        /// The image rendered when the user hovers the mouse over the image.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "The image rendered when the user hovers the mouse over the image." ),
        DesignerSerializationVisibility( DesignerSerializationVisibility.Content )
        ]
        public MenuOptionImage HoverImage 
        {
            get { return m_hoverImage; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "HoverImage",
                        "You must specify a hover image"
                        );

                else
                    m_hoverImage = value;
            }
        }

        /// <summary>
        /// The image to render when the option is depressed.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "The image to render when the option is depressed." ),
        DesignerSerializationVisibility( DesignerSerializationVisibility.Content )
        ]
        public MenuOptionImage PressedImage
        {
            get { return m_pressedImage; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "PressedImage",
                        "You must specify a PressedImage"
                        );

                else
                    m_pressedImage = value;
            }
        }

        /// <summary>
        /// The image to render when the option is disabled.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "The image to render when the option is disabled." ),
        DesignerSerializationVisibility( DesignerSerializationVisibility.Content )
        ]
        public MenuOptionImage DisabledImage
        {
            get { return m_disabledImage; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "DisabledImage",
                        "You must specify a DisabledImage"
                        );

                else
                    m_disabledImage = value;
            }
        }

        /// <summary>
        /// Returns the bitmap to be drawn when the option is in its normal 
        /// state (e.g, not hovered and not pressed).  Returns the cached 
        /// bitmap of either <see cref="Image"/> or 
        /// <see cref="DisabledImage"/>.
        /// </summary>
        [Browsable( false )]
        public Bitmap CachedPrimaryImage 
        {
            get 
            {
                if( Enabled ) 
                    return Image.CachedImage; 
                else 
                    return DisabledImage.CachedImage;
            }
        }

        /// <summary>
        /// Returns the bitmap to be drawn when the option is in its hover
        /// state.  Returns the cached bitmap of either 
        /// <see cref="HoverImage"/> or <see cref="DisabledImage"/>.
        /// </summary>
        [Browsable( false )]
        public Bitmap CachedPrimaryHoverImage 
        {
            get 
            {
                if( Enabled ) 
                    return HoverImage.CachedImage; 
                else 
                    return DisabledImage.CachedImage;
            }
        }

        /// <summary>
        /// Returns the bitmap to be drawn when the option is in its pressed
        /// state.  Returns the cached bitmap of either
        /// <see cref="PressedImage"/> or <see cref="DisabledImage"/>.
        /// </summary>
        [Browsable( false )]
        public Bitmap CachedPrimaryPressedImage 
        {
            get 
            {
                if( Enabled )
                    return PressedImage.CachedImage;
                else
                    return DisabledImage.CachedImage;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the arguments to pass to events.
        /// </summary>
        private object[] GetEventArgs() 
        {
            return new object[] { this, EventArgs.Empty };
        }

        /// <summary>
        /// Fires the StartHover event.
        /// </summary>
        internal void RaiseStartHover() 
        {
            if( StartHover != null ) 
                StartHover.DynamicInvoke( GetEventArgs() );
        }

        /// <summary>
        /// Fires the EndHover event.
        /// </summary>
        internal void RaiseEndHover()
        {
            if( EndHover != null )
                EndHover.DynamicInvoke( GetEventArgs() );
        }
        
        /// <summary>
        /// Fires the Click event.
        /// </summary>
        internal void RaiseClick() 
        {
            if( Click != null ) Click.DynamicInvoke( GetEventArgs() );
        }

        #endregion
    }

    #endregion

    #region Menu Option Image Editor

    /// <summary>
    /// Provides a class that can display a UI for editing menu option
    /// images.
    /// </summary>
    public class MenuOptionImageEditor : UITypeEditor
    {
        /// <summary>
        /// Displays a modal dialog to edit the provided value.
        /// </summary>
        public override object EditValue(
            ITypeDescriptorContext context, 
            IServiceProvider provider, 
            object value
            )
        {
            if( value != null && value is MenuOptionImage )
            {
                // Edit this. 
                MenuOptionImageEditorUI dialog = new MenuOptionImageEditorUI(
                    (MenuOptionImage)((ICloneable)value).Clone()
                    );

                if( dialog.ShowDialog() == DialogResult.OK )
                    return dialog.OptionImage;
                else
                    return value;
            }
            else
                // Can't edit this.
                return value;
        }

        /// <summary>
        /// Indicates that we edit with a modal dialog.
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext context
            )
        {
            return UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// Indicates that we support painting a preview image of our edited
        /// type to a property listing.
        /// </summary>
        public override bool GetPaintValueSupported(
            ITypeDescriptorContext context
            )
        {
            return true;
        }

        /// <summary>
        /// Paints the image for the provided value.
        /// </summary>
        public override void PaintValue( PaintValueEventArgs e )
        {
            if( e.Value != null && e.Value is MenuOptionImage ) 
                e.Graphics.DrawImage(
                    ((MenuOptionImage)e.Value).CachedImage,
                    e.Bounds
                    );
        }
    }

    #endregion

    #region Menu Option Image

    /// <summary>
    /// Provides options for one of the various images associated with a menu
    /// option, and supports rendering that image and caching the result.
    /// </summary>
    /// <remarks>
    /// <p>The MenuOptionImage class consolidates the options for each image 
    /// into a single instance.  These options include the image itself, its 
    /// transparency key, overall transparency, and drop shadow.
    /// </p>
    /// <p>The base image, specified via the <see cref="Image"/> property, is 
    /// an instance of the .NET framework’s 
    /// <see cref="System.Drawing.Bitmap"/> class.  This can be an image of 
    /// any format supported by the framework, including Windows Bitmap, GIF, 
    /// JPEG, and PNG, or it can be an image that your rendered manually via 
    /// code.
    /// </p>
    /// <p>Use the <see cref="TransparencyKey"/> property to specify a color 
    /// from the <see cref="Image"/> that is not drawn when the pop-up menu is 
    /// rendered.  For example, if you wanted your option to be circular in 
    /// shape, you could place the circle on a bright green background, and 
    /// set <see cref="TransparencyKey"/> to the background color.  If you are 
    /// using an image format that intrinsically supports transparency, such 
    /// as PNG, you should set this property to 
    /// <see cref="System.Drawing.Color.Empty"/>.
    /// </p>
    /// <p>Similarly, the <see cref="MaximumOpacity"/> property controls image 
    /// semi-transparency.  This is different from 
    /// <see cref="TransparencyKey"/> in that it is applied to every pixel in 
    /// the image, regardless of color.  Furthermore, whereas the transparency 
    /// key renders a pixel as either fully transparent (0) or fully opaque 
    /// (255), this property allows you to make a pixel semi-transparent, so 
    /// that its color will be blended with the background color.  This value 
    /// can be set to an integer between 0 and 255, inclusive.  The smaller 
    /// the number, the more of the background shows; the larger the number, 
    /// the more of the image itself shows.  0 renders as fully transparent, 
    /// and 255 renders as fully opaque.
    /// </p>
    /// <p>Finally, the <see cref="DropShadow"/> and 
    /// <see cref="UseDropShadow"/> options control the visibility of the 
    /// image’s drop shadow.  The <see cref="DropShadowOptions"/> class allows 
    /// you to configure the details of the drop shadow algorithm.
    /// </p>
    /// <p>The image is rendered by applying the options in the following 
    /// order:
    /// </p>
    /// <p><see cref="Image"/> --> <see cref="TransparencyKey"/> --> 
    /// <see cref="UseDropShadow"/> and <see cref="DropShadow"/> --> 
    /// <see cref="MaximumOpacity"/>
    /// </p>
    /// <p>Because MenuOptionImage instances represent a number of operations 
    /// that are applied to a basic image, these objects cache a pre-rendered 
    /// version of the option that can be accessed rapidly whenever the option 
    /// needs to be drawn.  Menu option images automatically clear their image 
    /// cache whenever any of their properties change, but you can take direct 
    /// control over this via the <see cref="ClearCache"/> and 
    /// <see cref="CreateCache"/> methods.  You can obtain a reference to the 
    /// cached, fully rendered image, via the <see cref="CachedImage"/> 
    /// property.
    /// </p>
    /// </remarks>
    [
    Serializable,
    TypeConverter( typeof( ExpandableObjectConverter ) ),
    Editor( typeof( MenuOptionImageEditor ), typeof( UITypeEditor ) )
    ]
    public class MenuOptionImage : ICloneable
    {
        #region Fields

        /// <summary>
        /// Stores the cached image.
        /// </summary>
        [NonSerialized]
        private Bitmap m_cache = null;

        /// <summary>
        /// Stores the base image.
        /// </summary>
        private Bitmap m_image = Effects.GetAssemblyImageResource( 
            Assembly.GetExecutingAssembly(), 
            "CircularMenu.Orb.bmp" 
            );

        /// <summary>
        /// Stores the transparency key.  If Color.Empty, no transparency key
        /// is applied.
        /// </summary>
        private Color m_transparencyKey = Color.White;

        /// <summary>
        /// The drop shadow options for this image.
        /// </summary>
        private DropShadowOptions m_dropShadow = new DropShadowOptions();

        /// <summary>
        /// Determines if the drop shadow settings are applied.
        /// </summary>
        private bool m_useDropShadow = true;

        /// <summary>
        /// Specifies the maximum alpha channel value for the cached image.
        /// When the filter for this setting is applied, existing alpha values
        /// will be scaled down, instead of simply capped.
        /// </summary>
        private byte m_transparency = 255;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new image with default settings.
        /// </summary>
        public MenuOptionImage() {}

        /// <summary>
        /// Initializes a new image by cloning data from the old image.  If the
        /// provided image is null, this constructor behaves identically to the
        /// default constructor.
        /// </summary>
        public MenuOptionImage( MenuOptionImage copyFrom ) 
        {
            if( copyFrom != null ) 
            {
                m_dropShadow = (DropShadowOptions)copyFrom.m_dropShadow.Clone();
                m_image = (Bitmap)copyFrom.m_image.Clone();
                m_transparency = copyFrom.m_transparency;
                m_transparencyKey = copyFrom.m_transparencyKey;
                m_useDropShadow = copyFrom.m_useDropShadow;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns a bitmap that is the end product of rendering the
        /// <see cref="Image"/> property with all of the settings defined in
        /// this instance.  This image will be created if required, and then
        /// saved for later use.  Changing any of the other properties in this
        /// instance will clear the cached bitmap.
        /// </summary>
        public Bitmap CachedImage 
        {
            get 
            {
                CreateCache();
                return m_cache;
            }
        }

        /// <summary>
        /// The base image for this option image.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "The base image for this option image." ),
        Editor( typeof( BitmapEditor ), typeof( UITypeEditor ) )
        ]
        public Bitmap Image 
        {
            get { return m_image; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "Image",
                        "You must provide a base image"
                        );

                else
                {
                    m_image = value;
                    ClearCache();
                }
            }
        }

        /// <summary>
        /// If set, any pixel in <see cref="Image"/> of this color will be made
        /// 100% transparent.
        /// </summary>
        [
        DefaultValue( "White" ),
        Description( "A color on the image to be made transparent." ),
        RefreshProperties( RefreshProperties.Repaint )
        ]
        public Color TransparencyKey 
        {
            get { return m_transparencyKey; }
            set 
            {
                m_transparencyKey = value;
                ClearCache();
            }
        }

        /// <summary>
        /// Determines if a drop shadow should be applied to the image.
        /// </summary>
        [
        DefaultValue( true ),
        Description( "Determines if a drop shadow should be applied to the image." ),
        RefreshProperties( RefreshProperties.Repaint )
        ]
        public bool UseDropShadow 
        {
            get { return m_useDropShadow; }
            set 
            {
                m_useDropShadow = value;
                ClearCache();
            }
        }

        /// <summary>
        /// The drop shadow options for this image.  Cannot be null.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "The drop shadow options for this image." ),
        RefreshProperties( RefreshProperties.Repaint ),
        DesignerSerializationVisibility( DesignerSerializationVisibility.Content )
        ]
        public DropShadowOptions DropShadow 
        {
            get { return m_dropShadow; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "DropShadow",
                        "You must provide a drop shadow options set"
                        );

                else 
                {
                    m_dropShadow = value;
                    ClearCache();
                }
            }
        }

        /// <summary>
        /// Determines the maximum value for the alpha-channel of any pixel in
        /// the cached bitmap.
        /// </summary>
        [
        DefaultValue( (byte)255 ),
        Description( "Determines the maximum opacity for the rendered image.  255 is fully opaque, 0 is fully transparent." ),
        RefreshProperties( RefreshProperties.Repaint )
        ]
        public byte MaximumOpacity 
        {
            get { return m_transparency; }
            set 
            {
                m_transparency = value;
                ClearCache();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders the cached bitmap if required.
        /// </summary>
        public void CreateCache() 
        {
            if( m_cache == null ) 
            {
                // Copy the source image.
                Bitmap cache = new Bitmap( m_image );

                // Mask the transparent color.
                if( !Color.Empty.Equals( m_transparencyKey ) ) 
                    cache.MakeTransparent( m_transparencyKey );

                // Apply the drop-shadow.
                if( m_useDropShadow ) 
                {
                    Bitmap withDS = m_dropShadow.GetImageWithDropShadow( cache );
                    cache.Dispose();
                    cache = withDS;
                }

                // Apply the transparency setting.
                if( m_transparency != 255 )
                    Effects.SetImageOpacity( cache, m_transparency );

                // All done.
                m_cache = cache;
            }
        }

        /// <summary>
        /// Returns a new, independent copy of this menu option image.
        /// </summary>
        public object Clone()
        {
            return new MenuOptionImage( this );
        }

        /// <summary>
        /// Disposes and clears the cached bitmap, forcing it to be recreated
        /// the next time it is required.
        /// </summary>
        public void ClearCache() 
        { 
            if( m_cache != null ) 
            {
                m_cache.Dispose();
                m_cache = null;
            }
        }

        #endregion
    }

    #endregion
}
