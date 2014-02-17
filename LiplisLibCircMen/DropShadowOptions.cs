using System;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using PixelEffects;

namespace CircularMenu
{
    #region Type Editor

    /// <summary>
    /// Provides a class that launches a graphical user interface for editing
    /// the values of a <see cref="DropShadowOptions"/> object.
    /// </summary>
    public class DropShadowOptionsEditor : UITypeEditor
    {
        /// <summary>
        /// Edits the provided value in a modal dialog.
        /// </summary>
        public override object EditValue(
            ITypeDescriptorContext context, 
            IServiceProvider provider, 
            object value
            )
        {
            if( value == null || !(value is DropShadowOptions) )
                return value;

            else 
            {
                // Edit a clone. 
                DropShadowOptionsEditorUi ui = new DropShadowOptionsEditorUi( 
                    (DropShadowOptions)((ICloneable)value).Clone()
                    );

                if( ui.ShowDialog() == System.Windows.Forms.DialogResult.OK )
                    return ui.Options;

                else
                    return value;
            }
        }

        /// <summary>
        /// Indicates that this is a modal editor.
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle
            ( ITypeDescriptorContext context )
        {
            return UITypeEditorEditStyle.Modal;
        }
    }

    #endregion

    #region Drop Shadow Options

    /// <summary>
    /// Provides a centralized collection of options used to render image drop
    /// shadows.  Also provides a method, <see cref="GetDropShadow"/>, that 
    /// returns a drop shadow for a given image based on the settings defined
    /// for the class.
    /// </summary>
    /// <remarks>
    /// This class has been designed to take advantage of the .NET design-time
    /// features.  The default type converter is the 
    /// <see cref="ExpandableObjectConverter"/>, and the UI type editor is 
    /// the <see cref="DropShadowOptionsEditor"/>.
    /// </remarks>
    [
    Serializable, 
    TypeConverter( typeof( ExpandableObjectConverter ) ),
    Editor( typeof( DropShadowOptionsEditor ), typeof( UITypeEditor ) )
    ]
    public class DropShadowOptions : ICloneable
    {
        private Color m_color = Color.Black;
        private int m_blurRadius = 4;
        private int m_opacityStep = 4;
        private int m_maximumOpacity = 255;
        private int m_xOffset = -2;
        private int m_yOffset = -2;

        /// <summary>
        /// Creates a new drop shadow settings object and sets the properties 
        /// to their defaults.
        /// </summary>
        public DropShadowOptions() {}

        /// <summary>
        /// Initializes a drop shadow options set and assigns the provided 
        /// shadow color.
        /// </summary>
        public DropShadowOptions( Color shadowColor ) 
        {
            m_color = shadowColor;
        }

        /// <summary>
        /// Initializes an option set and applies the provided offset.
        /// </summary>
        public DropShadowOptions( int xOffset, int yOffset ) 
        {
            m_xOffset = xOffset;
            m_yOffset = yOffset;
        }

        /// <summary>
        /// Initializes an option set and applies the provided offset.
        /// </summary>
        public DropShadowOptions( Point offset ) 
        {
            m_xOffset = offset.X;
            m_yOffset = offset.Y;
        }

        /// <summary>
        /// Initializes an option set and applies the provided blur radius.
        /// </summary>
        /// <param name="blurRadius">
        /// The blur radius for the new option set.  Must be at least 0.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided radius is less than zero.
        /// </exception>
        public DropShadowOptions( int blurRadius ) 
        {
            BlurRadius = blurRadius;
        }

        /// <summary>
        /// Initializes a new option set.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided radius is less than zero.
        /// </exception>
        public DropShadowOptions( int blurRadius, int xOffset, int yOffset ) 
        {
            BlurRadius = blurRadius;
            m_xOffset = xOffset;
            m_yOffset = yOffset;
        }

        /// <summary>
        /// Initializes a new option set.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided radius is less than zero.
        /// </exception>
        public DropShadowOptions( int blurRadius, Point offset ) 
        {
            BlurRadius = blurRadius;
            m_xOffset = offset.X;
            m_yOffset = offset.Y;
        }

        /// <summary>
        /// Creates a new, independent copy of the provided settings object.
        /// </summary>
        /// <param name="copyFrom">
        /// The object to copy settings from.  If null, all settings will be
        /// initialized to their defaults.
        /// </param>
        public DropShadowOptions( DropShadowOptions copyFrom ) 
        {
            if( copyFrom != null ) 
            {
                m_color = copyFrom.m_color;
                m_blurRadius = copyFrom.m_blurRadius;
                m_opacityStep = copyFrom.m_opacityStep;
                m_maximumOpacity = copyFrom.m_maximumOpacity;
                m_xOffset = copyFrom.m_xOffset;
                m_yOffset = copyFrom.m_yOffset;
            }
        }

        /// <summary>
        /// The color to use when rendering the drop shadow.
        /// </summary>
        /// <remarks>
        /// Changing this value can cause any connected parent to re-render
        /// itself.
        /// </remarks>
        [
        Description( "The color of the drop shadow" ),
        DefaultValue( "Black" )
        ]
        public Color ShadowColor 
        {
            get { return m_color; }
            set { m_color = value; }
        }

        /// <summary>
        /// The number of pixels by which each shadow pixel should be blurred.
        /// Must be at least 0.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if an attempt is made to set this property to a value of 
        /// less than zero.
        /// </exception>
        [
        Description( "The number of pixels by which each shadow pixel is blurred." ),
        DefaultValue( 4 )
        ]
        public int BlurRadius 
        {
            get { return m_blurRadius; }
            set 
            {
                if( value < 0 )
                    throw new ArgumentOutOfRangeException(
                        "BlurRadius",
                        value,
                        "The blur radius property must be at least 0"
                        );

                else
                    m_blurRadius = value;
            }
        }

        /// <summary>
        /// The amount by which the alpha channel of each pixel in the drop
        /// shadow is increased by for each blur.  Smaller values for this
        /// property tend to produce more subtle shadows, while larger values
        /// will produce shaper shadows.  Must be at least 1.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if an attempt is made to set this property to a value of
        /// less than one.
        /// </exception>
        [
        Description( "Controls the subtlety of the rendered shadow.  Smaller values produce more subtle shadows, while larger values produce sharper shadows.  Must be at least 1." ),
        DefaultValue( 4 )
        ]
        public int OpacityStep 
        {
            get { return m_opacityStep; }
            set 
            {
                if( value < 1 )
                    throw new ArgumentOutOfRangeException(
                        "OpacityStep",
                        value,
                        "The opacity step property must be at least 1"
                        );

                else
                    m_opacityStep = value;
            }
        }

        /// <summary>
        /// Defines the maximum opacity of any pixel in the rendered shadow.
        /// Must be between 0 and 255.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if an attempt is made to set this property to a value 
        /// outside the range 0 to 255, inclusive.
        /// </exception>
        [
        Description( "Defines the maximum opacity of the rendered shadow.  This value must be between 0 and 255, inclusive, where 255 is completely opaque and 0 is completely transparent." ),
        DefaultValue( 255 )
        ]
        public int MaximumOpacity 
        {
            get { return m_maximumOpacity; }
            set 
            {
                if( value < 0 || value > 255 )
                    throw new ArgumentOutOfRangeException(
                        "MaximumOpacity",
                        value,
                        "The maximum opacity property must be between 0 and 255, inclusive"
                        );

                else
                    m_maximumOpacity = value;
            }
        }

        /// <summary>
        /// Controls the horizontal offset of the shadow from the original
        /// image.
        /// </summary>
        [
        Description( "The number of pixels the shadow should be offset from the image along the x axis." ),
        DefaultValue( -2 )
        ]
        public int ShadowOffsetX 
        {
            get { return m_xOffset; }
            set { m_xOffset = value; }
        }

        /// <summary>
        /// Controls the vertical offset of the shadow from the original
        /// image.
        /// </summary>
        [
        Description( "The number of pixels the shadow should be offset from the image along the y axis." ),
        DefaultValue( -2 )
        ]
        public int ShadowOffsetY 
        {
            get { return m_yOffset; }
            set { m_yOffset = value; }
        }

        /// <summary>
        /// Returns a drop shadow for the provided image, rendered using the
        /// settings currently applied to this object.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided image is a null reference.
        /// </exception>
        public Bitmap GetDropShadow( Bitmap image ) 
        {
            if( image == null )
                throw new ArgumentNullException(
                    "image",
                    "You must provide a valid image"
                    );

            else
                return Effects.GetImageDropShadow(
                    image,
                    m_color,
                    m_blurRadius,
                    m_maximumOpacity,
                    m_opacityStep
                    );
        }

        /// <summary>
        /// Returns the provided image composited with its drop shadow, 
        /// based on the settings defined in this class.
        /// </summary>
        /// <param name="image">
        /// The image you wish to composite.  Cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided image is a null reference.
        /// </exception>
        public Bitmap GetImageWithDropShadow( Bitmap image ) 
        {
            if( image == null )
                throw new ArgumentNullException(
                    "image",
                    "You must provide a valid image"
                    );

            else
            {
                Bitmap dropShadow = GetDropShadow( image );

                // Calculate metrics...
                int width = 0, height = 0;
                int imageX = 0, imageY = 0;
                int dsX = 0, dsY = 0;

                int x = m_xOffset;
                int y = m_yOffset;

                if( x >= 0 )
                {
                    width = x + dropShadow.Width;
                    dsX = x;
                }
                else 
                {
                    width = Math.Max(
                        dropShadow.Width,
                        image.Width + (-x)
                        );
                    imageX = -x;
                }

                if( y >= 0 )
                {
                    height = y + dropShadow.Height;
                    dsY = y;
                }
                else 
                {
                    height = Math.Max(
                        dropShadow.Height,
                        image.Height + (-y)
                        );
                    imageY = -y;
                }

                // Create the surface.
                Bitmap composite = new Bitmap( width, height );
                Graphics g = Graphics.FromImage( composite );

                // Clear.
                g.Clear( Color.FromArgb( 0, m_color ) );

                // Draw the composite.
                g.DrawImage( dropShadow, dsX, dsY );
                g.DrawImage( image, imageX, imageY );

                // Done.
                g.Dispose();    
                return composite;
            }
        }
        
        /// <summary>
        /// Returns a new, independent copy of this settings object.  Changes
        /// to the clone will not cause the parent to reanimate.
        /// </summary>
        public object Clone()
        {
            return new DropShadowOptions( this );
        }
    }

    #endregion
}
