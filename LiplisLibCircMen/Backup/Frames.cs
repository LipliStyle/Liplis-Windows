using System;
using System.Collections;
using System.Drawing;

namespace CircularMenu
{
    #region Frame Option Data

    /// <summary>
    /// Provides data that intermittently describes a menu option during
    /// the opening animation.
    /// </summary>
    public class FrameOptionData 
    {
        private Point m_centerPos;
        private Bitmap m_option;

        /// <summary>
        /// Initializes the option data.
        /// </summary>
        /// <param name="centerPosition">
        /// The position of the central point of the option.
        /// </param>
        /// <param name="option">
        /// The bitmap representing the option.  If null, the option will
        /// not be rendered.
        /// </param>
        public FrameOptionData(
            Point centerPosition,
            Bitmap option
            ) 
        {
            m_centerPos = centerPosition;
            m_option = option;
        }

        /// <summary>
        /// Defines the location of the option's central pixel.
        /// </summary>
        public Point CenterPixelPosition 
        {
            get { return m_centerPos; }
        }

        /// <summary>
        /// Defines the bitmap to paint for the option.  If null, no bitmap is
        /// to be painted.
        /// </summary>
        public Bitmap OptionBitmap 
        {
            get { return m_option; }
        }

        /// <summary>
        /// Determines if this option defines a bitmap to be painted.
        /// </summary>
        public bool HasBitmap 
        {
            get { return m_option != null; }
        }

        /// <summary>
        /// Renders this option to the provided graphics context.
        /// </summary>
        /// <param name="g">
        /// The graphics context to render to.  Cannot be null.
        /// </param>
        /// <param name="xOffset">
        /// An origin offset to apply to the x position.
        /// </param>
        /// <param name="yOffset">
        /// An origin offset to apply to the y position.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided graphics context is a null reference.
        /// </exception>
        public void RenderTo(
            Graphics g, 
            int xOffset, 
            int yOffset
            ) 
        {
            if( g == null )
                throw new ArgumentNullException(
                    "g",
                    "You must provide a graphics context to render to"
                    );

            else if( HasBitmap ) 
                RenderTo( 
                    m_option, 
                    g, 
                    xOffset, 
                    yOffset
                    );
        }

        /// <summary>
        /// Renders the provided image to the provided graphics context at the
        /// location specified by this class.
        /// </summary>
        protected void RenderTo( 
            Bitmap image,
            Graphics g,
            int xOffset,
            int yOffset
            ) 
        {
            if( image != null && g != null )
            {
                // Get our position.
                int left = xOffset + (m_centerPos.X - (image.Width / 2));
                int top = yOffset + (m_centerPos.Y - (image.Height / 2));
                
                // Draw us.
                g.DrawImage( image, left, top );
            }
        }
    }

    #endregion

    #region Final Frame Option Data

    /// <summary>
    /// Defines auxillary information for an option appearing in the final
    /// frame of an animation.
    /// </summary>
    public class FinalFrameOptionData : FrameOptionData
    {
        private Rectangle m_boundingBox;
        private MenuOption m_option;

        /// <summary>
        /// Initializes a new final frame option data set.
        /// </summary>
        /// <param name="centerPixelLocation">
        /// The location of the bitmap's central pixel.
        /// </param>
        /// <param name="option">
        /// The menu option associated with this frame.  Cannot be null.
        /// </param>
        /// <exception cref="NullReferenceException">
        /// Thrown if the provided option is a null reference.
        /// </exception>
        public FinalFrameOptionData(
            Point centerPixelLocation,
            MenuOption option
            ) :
            base( centerPixelLocation, option.CachedPrimaryImage ) 
        {
            if( option == null )
                throw new ArgumentNullException(
                    "option",
                    "The menu option for the final frame cannot be null"
                    );

            else 
            {
                m_option = option;

                m_boundingBox = new Rectangle(
                    new Point(
                        centerPixelLocation.X - OptionBitmap.Width / 2,
                        centerPixelLocation.Y - OptionBitmap.Height / 2
                        ),
                    OptionBitmap.Size
                    );
            }
        }

        /// <summary>
        /// Renders this option to the provided graphics context.
        /// </summary>
        /// <param name="g">
        /// The graphics context to render to.  Cannot be null.
        /// </param>
        /// <param name="xOffset">
        /// An origin offset to apply to the x position.
        /// </param>
        /// <param name="yOffset">
        /// An origin offset to apply to the y position.
        /// </param>
        /// <param name="optionState">
        /// The state of the option.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided graphics context is a null reference.
        /// </exception>
        public void RenderTo(
            Graphics g,
            int xOffset,
            int yOffset,
            FrameMenuOptionState optionState
            ) 
        {
            if( g == null )
                throw new ArgumentNullException(
                    "g",
                    "You must provide a graphics context to render to"
                    );

            else if( optionState == FrameMenuOptionState.Normal ) 
                RenderTo( 
                    OptionBitmap, 
                    g, 
                    xOffset, 
                    yOffset
                    );
            
            else if( optionState == FrameMenuOptionState.Hover )
                RenderTo( 
                    m_option.CachedPrimaryHoverImage, 
                    g, 
                    xOffset, 
                    yOffset
                    );

            else
                RenderTo( 
                    m_option.CachedPrimaryPressedImage, 
                    g, 
                    xOffset, 
                    yOffset
                    );
        }

        /// <summary>
        /// Returns the bounding box of this option (using the normal mode
        /// bitmap).
        /// </summary>
        public Rectangle BoundingBox 
        {
            get { return m_boundingBox; }
        }

        /// <summary>
        /// Gets the menu option rendered.
        /// </summary>
        public MenuOption Option 
        {
            get { return m_option; }
        }
    }

    #endregion

    #region Frame

    /// <summary>
    /// Defines an individual animation frame.
    /// </summary>
    public class Frame 
    {
        /// <summary>
        /// The options contained in this frame.
        /// </summary>
        protected internal ArrayList m_options = new ArrayList();

        /// <summary>
        /// The overall boundaries of this frame.
        /// </summary>
        protected internal Rectangle m_bounds;

        /// <summary>
        /// An internal default constructor 
        /// </summary>
        internal Frame() {}

        /// <summary>
        /// Initializes the data for the given frame.
        /// </summary>
        /// <param name="options">
        /// The options that are to be rendered into this frame.  Cannot
        /// be null.
        /// </param>
        /// <param name="radius">
        /// The radius to apply to the menu layout.
        /// </param>
        /// <param name="frameIndex">
        /// The index of the frame to render.  Must be at least zero and
        /// must be less than frameCount - 1.
        /// </param>
        /// <param name="frameCount">
        /// The number of frames to render.  Must be at least 1.
        /// </param>
        /// <param name="layout">
        /// The object to use for performing layout of the frame.  Cannot be
        /// null.
        /// </param>
        /// <param name="modifier">
        /// The modifications to apply to the rendered options.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null references.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the arguments are out of range.
        /// </exception>
        public Frame( 
            MenuOptionCollection options,
            int radius,
            int frameIndex,
            int frameCount,
            IFrameLayoutManager layout,
            IFrameModifier modifier
            ) 
        {
            if( options == null )
                throw new ArgumentNullException(
                    "options",
                    "The options parameter cannot be null"
                    );

            else if( layout == null )
                throw new ArgumentNullException(
                    "layout",
                    "The layout manager cannot be null"
                    );

            else if( modifier == null )
                throw new ArgumentNullException(
                    "modifier",
                    "You must provide a frame modifier"
                    );

            else if( frameCount < 1 )
                throw new ArgumentOutOfRangeException(
                    "frameCount",
                    frameCount,
                    "The frame count must be at least one."
                    );

            else if( frameIndex < 0 || frameIndex >= frameCount - 1 )
                throw new ArgumentOutOfRangeException(
                    "frameIndex",
                    frameIndex,
                    "The frame index must be at least zero, and must be strictly less than frameCount - 1"
                    );

            else 
            {
                // Track the boundaries of this frame... 
                int left = 0, top = 0, right = 0, bottom = 0;

                // Position and modify the options... 
                for( int i = 0; i < options.Count; i++ ) 
                {
                    // Get the position and default image. 
                    FrameOptionData original = new FrameOptionData(
                        layout.GetOptionPosition(
                        radius,
                        i,
                        options.Count,
                        frameIndex,
                        frameCount
                        ),
                        options[ i ].CachedPrimaryImage
                        );

                    FrameOptionData current = new FrameOptionData(
                        original.CenterPixelPosition,
                        (Bitmap)original.OptionBitmap.Clone()
                        );

                    // Modify the default image. 
                    current = modifier.ModifyFrame(
                        original,
                        frameIndex,
                        frameCount
                        );

                    // Was the data cleared? 
                    if( current != null && current.HasBitmap )
                    {
                        m_options.Add( current );

                        int l = current.CenterPixelPosition.X -
                            current.OptionBitmap.Width / 2;

                        int t = current.CenterPixelPosition.Y -
                            current.OptionBitmap.Height / 2;

                        int r = current.CenterPixelPosition.X +
                            current.OptionBitmap.Width / 2;

                        int b = current.CenterPixelPosition.Y +
                            current.OptionBitmap.Height / 2;

                        if( l < left ) left = l;
                        if( t < top ) top = t;
                        if( r > right ) right = r;
                        if( b > bottom ) bottom = b;
                    }
                }

                // Set our bounds. 
                m_bounds = Rectangle.FromLTRB( left, top, right, bottom );
            }
        }   

        /// <summary>
        /// Gets the bounding box for this frame.
        /// </summary>
        public Rectangle Bounds 
        {
            get { return m_bounds; }
        }

        /// <summary>
        /// Renders this frame to the provided graphics context.
        /// </summary>
        public void Render( 
            Graphics g, 
            int xOffset, 
            int yOffset
            ) 
        {
            foreach( FrameOptionData option in m_options )
                option.RenderTo( 
                    g, 
                    xOffset, 
                    yOffset
                    );
        }
    }

    #endregion

    #region Final Frame

    /// <summary>
    /// Defines the final frame of an animation.  This frame is referenced
    /// when the system is rendering the menu for the user, and thus has some
    /// constraints that are not present in "normal" frames.
    /// </summary>
    public class FinalFrame : Frame 
    {
        /// <summary>
        /// Initializes the data for this frame.
        /// </summary>
        /// <param name="options">
        /// The options that are to be rendered into this frame.  Cannot
        /// be null, nor can it contain less than one option.
        /// </param>
        /// <param name="radius">
        /// The radius to apply to the menu layout.
        /// </param>
        /// <param name="frameIndex">
        /// Ignored.
        /// </param>
        /// <param name="frameCount">
        /// The number of frames comprising the animation, including this
        /// frame.  Must be at least one.
        /// </param>
        /// <param name="layout">
        /// The object to use for performing layout of the frame.  Cannot be
        /// null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the arguments are null references.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided options collection contains no elements.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the arguments are out of range.
        /// </exception>
        public FinalFrame( 
            MenuOptionCollection options,
            int radius,
            int frameIndex,
            int frameCount,
            IFrameLayoutManager layout
            ) 
        {
            if( options == null )
                throw new ArgumentNullException(
                    "options",
                    "The options parameter cannot be null"
                    );

            else if( options.Count == 0 )
                throw new ArgumentException(
                    "The options collection must contain at least one option",
                    "options"
                    );

            else if( layout == null )
                throw new ArgumentNullException(
                    "layout",
                    "The layout manager cannot be null"
                    );

            else if( frameCount < 1 )
                throw new ArgumentOutOfRangeException(
                    "frameCount",
                    frameCount,
                    "The frame count must be at least one."
                    );

            else 
            {
                // Track the boundaries of this frame... 
                int left = 0, top = 0, right = 0, bottom = 0;

                // Position the options... 
                for( int i = 0; i < options.Count; i++ ) 
                {
                    // Get the position and default image. 
                    FinalFrameOptionData option = new FinalFrameOptionData(
                        layout.GetOptionPosition(
                            radius,
                            i,
                            options.Count,
                            frameCount - 1,
                            frameCount
                            ),
                        options[ i ]
                        );

                    m_options.Add( option );

                    // Get the width of the normal option. 
                    int l = option.CenterPixelPosition.X -
                        option.OptionBitmap.Width / 2;

                    int t = option.CenterPixelPosition.Y -
                        option.OptionBitmap.Height / 2;

                    int r = option.CenterPixelPosition.X +
                        option.OptionBitmap.Width / 2;

                    int b = option.CenterPixelPosition.Y +
                        option.OptionBitmap.Height / 2;

                    if( l < left ) left = l;
                    if( t < top ) top = t;
                    if( r > right ) right = r;
                    if( b > bottom ) bottom = b;

                    // Adjust for the height of the hover image. 
                    l = option.CenterPixelPosition.X -
                        option.Option.CachedPrimaryHoverImage.Width / 2;

                    t = option.CenterPixelPosition.Y -
                        option.Option.CachedPrimaryHoverImage.Height / 2;

                    r = option.CenterPixelPosition.X +
                        option.Option.CachedPrimaryHoverImage.Width / 2;

                    b = option.CenterPixelPosition.Y +
                        option.Option.CachedPrimaryHoverImage.Height / 2;

                    if( l < left ) left = l;
                    if( t < top ) top = t;
                    if( r > right ) right = r;
                    if( b > bottom ) bottom = b;

                    // Adjust for the height of the pressed image. 
                    l = option.CenterPixelPosition.X -
                        option.Option.CachedPrimaryPressedImage.Width / 2;

                    t = option.CenterPixelPosition.Y -
                        option.Option.CachedPrimaryPressedImage.Height / 2;

                    r = option.CenterPixelPosition.X +
                        option.Option.CachedPrimaryPressedImage.Width / 2;

                    b = option.CenterPixelPosition.Y +
                        option.Option.CachedPrimaryPressedImage.Height / 2;

                    if( l < left ) left = l;
                    if( t < top ) top = t;
                    if( r > right ) right = r;
                    if( b > bottom ) bottom = b;
                }

                // Set our bounds. 
                m_bounds = Rectangle.FromLTRB( left, top, right, bottom );
            }
        }   

        /// <summary>
        /// Renders this frame, taking the current mouse position into account.
        /// </summary>
        public void Render(
            Graphics g,
            int xOffset,
            int yOffset,
            int mouseX,
            int mouseY,
            bool mousePressed
            ) 
        {
            int offsettedMouseX = mouseX - xOffset;
            int offsettedMouseY = mouseY - yOffset;

            foreach( FinalFrameOptionData option in m_options ) 
            {
                FrameMenuOptionState state;
                if( option.BoundingBox.Contains
                    ( offsettedMouseX, offsettedMouseY ) 
                    ) 
                {
                    if( mousePressed )
                        state = FrameMenuOptionState.Pressed;

                    else
                        state = FrameMenuOptionState.Hover;

                }
                else
                    state = FrameMenuOptionState.Normal;

                option.RenderTo( 
                    g,
                    xOffset,
                    yOffset,
                    state
                    );
            }
        }

        /// <summary>
        /// Determines the menu option whose image contains the provided mouse
        /// coordinate when rendered, if any.  If no such option exists, this 
        /// method returns null.
        /// </summary>
        public MenuOption HitTest( 
            int xOffset, 
            int yOffset, 
            int mouseX, 
            int mouseY 
            ) 
        {
            Point checkPoint = new Point(
                mouseX - xOffset,
                mouseY - yOffset
                );

            foreach( FinalFrameOptionData option in m_options ) 
                if( option.BoundingBox.Contains( checkPoint ) )
                    return option.Option;

            return null;
        }
    }

    #endregion

    #region Frame Collection

    /// <summary>
    /// A collection of frames to be rendered in an animation.
    /// </summary>
    public abstract class FrameCollection : IEnumerable
    {
        /// <summary>
        /// The list of frames in this collection.
        /// </summary>
        protected internal ArrayList m_frames = new ArrayList();

        /// <summary>
        /// The final frame in this colleciton.
        /// </summary>
        protected internal FinalFrame m_finalFrame = null;

        /// <summary>
        /// Protect access to the constructor.
        /// </summary>
        protected internal FrameCollection() {}

        /// <summary>
        /// Returns the number of frames defined in this animation.
        /// </summary>
        public int Count { get { return m_frames.Count; } }

        /// <summary>
        /// Returns a reference to the frame at the provided position.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if the provided index is outside the allowable range.
        /// </exception>
        public Frame this[ int index ] 
        {
            get 
            {
                if( index < 0 || index >= Count )
                    throw new IndexOutOfRangeException(
                        "Frame indexer out of range"
                        );

                else
                    return (Frame)m_frames[ index ];
            }
        }

        /// <summary>
        /// Returns the final frame of this animation.
        /// </summary>
        public FinalFrame FinalFrame { get { return m_finalFrame; } }

        /// <summary>
        /// Returns an object that can be used to enumerate the frames in this
        /// animation.
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            return m_frames.GetEnumerator();
        }
    }

    #endregion

    #region Forward Frame Collection

    /// <summary>
    /// Defines a frame collection that animates its elements from the 
    /// initial state to the final state.
    /// </summary>
    public class ForwardFrameCollection : FrameCollection   
    {
        /// <summary>
        /// Initializes an animation for the provided collection.
        /// </summary>
        /// <param name="options">
        /// The options to animate.  This parameter cannot be null,
        /// nor can the result of its 
        /// <see cref="MenuOptionCollection.GetVisibleOptions"/>
        /// method be empty.
        /// </param>
        /// <param name="radius">
        /// The radius to apply to the menu layout.
        /// </param>
        /// <param name="frameCount">
        /// The number of frames to render.  Must be at least one.
        /// </param>
        /// <param name="layout">
        /// The object used to lay out the menu options during the animation.
        /// </param>
        /// <param name="modifier">
        /// An object that transforms image options during the animation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the provided parameters are null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided frameCount option is less than one.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if there are no visible options in the provided collection.
        /// </exception>
        public ForwardFrameCollection(
            MenuOptionCollection options,
            int radius,
            int frameCount,
            IFrameLayoutManager layout,
            IFrameModifier modifier
            ) 
        {
            if( options == null )
                throw new ArgumentNullException(
                    "options",
                    "You must provide a set of options"
                    );

            else if( layout == null )
                throw new ArgumentNullException(
                    "layout",
                    "You must provide a layout manager"
                    );

            else if( modifier == null )
                throw new ArgumentNullException(
                    "modifier",
                    "You must provide a modifier"
                    );

            else if( frameCount < 1 )
                throw new ArgumentOutOfRangeException(
                    "frameCount",
                    frameCount,
                    "You must provide at least one frame to render"
                    );

            else 
            {
                MenuOptionCollection visibleOptions = 
                    options.GetVisibleOptions();

                if( visibleOptions.Count == 0 )
                    throw new ArgumentException(
                        "There were no visible options to render",
                        "options"
                        );

                else 
                {
                    // Animate. 
                    for( int i = 0; i < frameCount - 1; i++ ) 
                        m_frames.Add( new Frame(
                            visibleOptions,
                            radius,
                            i,
                            frameCount,
                            layout,
                            modifier
                            ) );

                    // Final frame. 
                    m_finalFrame = new FinalFrame(
                        visibleOptions,
                        radius,
                        frameCount - 1,
                        frameCount,
                        layout
                        );

                    m_frames.Add( m_finalFrame );
                }
            }
        }
    }

    #endregion

    #region Reverse Frame Collection

    /// <summary>
    /// Defines a frame collection that animates its elements from the 
    /// final state to the initial state.
    /// </summary>
    public class ReverseFrameCollection : FrameCollection   
    {
        /// <summary>
        /// Initializes an animation for the provided collection.
        /// </summary>
        /// <param name="options">
        /// The options to animate.  This parameter cannot be null,
        /// nor can the result of its 
        /// <see cref="MenuOptionCollection.GetVisibleOptions"/>
        /// method be empty.
        /// </param>
        /// <param name="radius">
        /// The radius to apply to the menu layout.
        /// </param>
        /// <param name="frameCount">
        /// The number of frames to render.  Must be at least one.
        /// </param>
        /// <param name="layout">
        /// The object used to lay out the menu options during the animation.
        /// </param>
        /// <param name="modifier">
        /// An object that transforms images during the animation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any of the provided parameters are null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided frameCount option is less than one.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if there are no visible options in the provided collection.
        /// </exception>
        public ReverseFrameCollection(
            MenuOptionCollection options,
            int radius,
            int frameCount,
            IFrameLayoutManager layout,
            IFrameModifier modifier
            ) 
        {
            if( options == null )
                throw new ArgumentNullException(
                    "options",
                    "You must provide a set of options"
                    );

            else if( layout == null )
                throw new ArgumentNullException(
                    "layout",
                    "You must provide a layout manager"
                    );

            else if( modifier == null )
                throw new ArgumentNullException(
                    "modifier",
                    "You must provide a modifier"
                    );

            else if( frameCount < 1 )
                throw new ArgumentOutOfRangeException(
                    "frameCount",
                    frameCount,
                    "You must provide at least one frame to render"
                    );

            else 
            {
                MenuOptionCollection visibleOptions = 
                    options.GetVisibleOptions();

                if( visibleOptions.Count == 0 )
                    throw new ArgumentException(
                        "There were no visible options to render",
                        "options"
                        );

                else
                {
                    // Final frame. 
                    m_finalFrame = new FinalFrame(
                        visibleOptions,
                        radius,
                        frameCount - 1,
                        frameCount,
                        layout
                        );

                    m_frames.Add( m_finalFrame );

                    // Animate. 
                    for( int i = frameCount - 2; i >= 0; i-- ) 
                        m_frames.Add( new Frame(
                            visibleOptions,
                            radius,
                            i,
                            frameCount,
                            layout,
                            modifier
                            ) );
                }
            }
        }
    }

    #endregion

    #region Frame Menu Option State Enumeration

    /// <summary>
    /// Defines the possible states a frame menu option can take.
    /// </summary>
    public enum FrameMenuOptionState 
    {
        /// <summary>
        /// The mouse is not over the option.
        /// </summary>
        Normal,

        /// <summary>
        /// The mouse is over the option, but no mouse button is pressed.
        /// </summary>
        Hover,

        /// <summary>
        /// The mouse is over the option and a mouse button is pressed.
        /// </summary>
        Pressed
    }

    #endregion
}
