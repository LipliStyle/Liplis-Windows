using System;
using System.ComponentModel;
using System.Drawing;

namespace CircularMenu
{
    #region Tool Tip Renderer Interface 

    /// <summary>
    /// This interface describes an object that is capable of rendering menu
    /// tool tips.
    /// </summary>
    public interface IToolTipRenderer 
    {
        /// <summary>
        /// Instructs the renderer to draw the provided tool tip to the 
        /// provided graphics context.
        /// </summary>
        /// <param name="g">
        /// The graphics context to use for rendering.
        /// </param>
        /// <param name="toolTip">
        /// The tool tip to render.  Should not be null.
        /// </param>
        /// <param name="menuCenter">
        /// Defines the point where the menu's center pixel is located.
        /// </param>
        /// <param name="renderArea">
        /// Defines the rendering area that the tool tip should be placed
        /// within.
        /// </param>
        void Render( 
            Graphics g, 
            String toolTip,
            Point menuCenter, 
            Rectangle renderArea
            );

        /// <summary>
        /// Instructs the renderer to indicate that there is no tool tip
        /// currently available (either because the user is not hovering over
        /// an option or because the option the user is hovering over does
        /// not define a tool tip.
        /// </summary>
        /// <param name="g">
        /// The graphics context to use for rendering.
        /// </param>
        /// <param name="menuCenter">
        /// Defines the point where the menu's center pixel is located.
        /// </param>
        /// <param name="renderArea">
        /// Defines the rendering area that the tool tip should be placed
        /// within.
        /// </param>
        void RenderEmpty(
            Graphics g,
            Point menuCenter,
            Rectangle renderArea
            );
    }

    #endregion

    #region Extra Space Tool Tip Renderer Interface

    /// <summary>
    /// Defines a tool-tip renderer that requires more menu space than is
    /// allocated when accounting solely for icon sizes and positions.
    /// The rectangle returned by the <see cref="GetMaximumRenderArea"/>
    /// method will be passed to the <see cref="CircularMenu.IToolTipRenderer.Render"/>
    /// and <see cref="CircularMenu.IToolTipRenderer.RenderEmpty"/> methods 
    /// when rendering.
    /// </summary>
    public interface IExtraSpaceToolTipRenderer : IToolTipRenderer 
    {
        /// <summary>
        /// Determines the space required for this renderer in order to display
        /// tool tips.
        /// </summary>
        /// <param name="g">
        /// A graphics context that can be used to measure font heights and 
        /// widths.
        /// </param>
        /// <param name="menuCenter">
        /// A point within defaultMenuBoundaries that defines the central 
        /// position of a circular menu.
        /// </param>
        /// <param name="defaultMenuSize">
        /// The boundaries allocated for the menu, accounting for icon sizes 
        /// and widths.
        /// </param>
        /// <returns>
        /// A rectangle indicating the area (relative to 0, 0)
        /// that the renderer requests for drawing tool tips.  If this area
        /// extends the provided menu boundaries, the menu size will be
        /// expanded to accomodate the new area.  This area will be passed to
        /// the <see cref="CircularMenu.IToolTipRenderer.Render"/> and 
        /// <see cref="CircularMenu.IToolTipRenderer.RenderEmpty"/> methods.
        /// </returns>
        Rectangle GetMaximumRenderArea(
            Graphics g,
            Point menuCenter,
            Size defaultMenuSize
            );
    }

    #endregion

    #region Standard Tool Tip Data

    /// <summary>
    /// Provides settings for common tool tip data.
    /// </summary>
    /// <remarks>
    /// <p>StandardToolTipData provides a number of options for controlling 
    /// the display of tool-tip information.  The tool-tip background is 
    /// controlled via the <see cref="BackgroundColor"/>, 
    /// <see cref="BackgroundOpacity"/>, and 
    /// <see cref="RenderBackgroundOnEmpty"/> properties; text is defined by 
    /// the <see cref="ForegroundColor"/>, <see cref="ForegroundOpacity"/>, and 
    /// <see cref="Font"/> properties; the tool-tip border by the 
    /// <see cref="BorderColor"/>, <see cref="BorderOpacity"/>, and 
    /// <see cref="BorderThickness"/> properties.
    /// </p>
    /// <p>Each of the opacity properties controls the transparency of the 
    /// element and is analogous to the 
    /// <see cref="CircularMenu.MenuOptionImage.MaximumOpacity"/> property of 
    /// the <see cref="MenuOptionImage"/> class.
    /// </p>
    /// </remarks>
    [
    Serializable(),
    TypeConverter( typeof( ExpandableObjectConverter ) )
    ]
    public class StandardToolTipData 
    {
        private int m_borderThickness = 1;
        private Color m_backgroundColor = SystemColors.Info;
        private Color m_foregroundColor = SystemColors.InfoText;
        private Color m_edgeColor = SystemColors.InfoText;
        private Font m_font = new Font( "Verdana", 10, FontStyle.Bold );
        private byte m_backgroundOpacity = 175;
        private byte m_foregroundOpacity = 255;
        private byte m_edgeOpacity = 255;
        private bool m_renderEmptyBack = false;

        /// <summary>
        /// Provides the thickness of the border.
        /// </summary>
        [
        DefaultValue( 1 ),
        Description( "The thickness of the tool-tip's border" )
        ]
        public int BorderThickness 
        {
            get { return m_borderThickness; }
            set 
            {
                if( value <= 0 )
                    throw new ArgumentOutOfRangeException(
                        "BorderThickness",
                        value,
                        "The border thickness must be at least 1"
                        );
                
                else
                    m_borderThickness = value; 
            }
        }

        /// <summary>
        /// Provides the color used to render text.
        /// </summary>
        [
        DefaultValue( "InfoText" ),
        Description( "The color used to render tool-tip text." )
        ]
        public Color ForegroundColor 
        {
            get { return m_foregroundColor; }
            set { m_foregroundColor = value; }
        }

        /// <summary>
        /// The color used to render tool-tip backgrounds.
        /// </summary>
        [
        DefaultValue( "Info" ),
        Description( "The color used to render tool-tip backgrounds." )
        ]
        public Color BackgroundColor 
        {
            get { return m_backgroundColor; }
            set { m_backgroundColor = value; }
        }

        /// <summary>
        /// The color of the background's border.
        /// </summary>
        [
        DefaultValue( "InfoText" ),
        Description( "The color of the tool-tip background's border." )
        ]
        public Color BorderColor 
        {
            get { return m_edgeColor; }
            set { m_edgeColor = value; }
        }

        /// <summary>
        /// Provides the opacity of the foreground text.
        /// </summary>
        [
        DefaultValue( 255 ),
        Description( "The opacity of the tool-tip text.  0 is fully transparent; 255 is fully opaque." )
        ]
        public byte ForegroundOpacity 
        {
            get { return m_foregroundOpacity; }
            set { m_foregroundOpacity = value; }
        }

        /// <summary>
        /// Provides the opacity of the background.
        /// </summary>
        [
        DefaultValue( 175 ),
        Description( "The opacity of the tool-tip background.  0 is fully transparent; 255 is fully opaque." )
        ]
        public byte BackgroundOpacity 
        {
            get { return m_backgroundOpacity; }
            set { m_backgroundOpacity = value; }
        }

        /// <summary>
        /// Provides the opacity of the tool-tip border.
        /// </summary>
        [
        DefaultValue( 255 ),
        Description( "The opacity of the tool-tip border.  0 is fully transparent; 255 is fully opaque." )
        ]
        public byte BorderOpacity 
        {
            get { return m_edgeOpacity; }
            set { m_edgeOpacity = value; }
        }

        /// <summary>
        /// Provides the foreground text color with the alpha value applied.
        /// </summary>
        [
        Browsable( false )
        ]
        public Color RenderingForegroundColor 
        {
            get 
            { 
                return Color.FromArgb( m_foregroundOpacity, m_foregroundColor );
            }
        }

        /// <summary>
        /// Provides the background color with the alpha value applied.
        /// </summary>
        [
        Browsable( false )
        ]
        public Color RenderingBackgroundColor
        {
            get 
            { 
                return Color.FromArgb( m_backgroundOpacity, m_backgroundColor );
            }
        }

        /// <summary>
        /// Provides the border color with the alpha value applied.
        /// </summary>
        [
        Browsable( false )
        ]
        public Color RenderingBorderColor
        {
            get 
            { 
                return Color.FromArgb( m_edgeOpacity, m_edgeColor );
            }
        }

        /// <summary>
        /// The font used to render the text.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        DefaultValue( "Verdana, 10pt, style=Bold" ),
        Description( "The font used to render the tool-tip text." )
        ]
        public Font Font 
        {
            get { return m_font; }
            set 
            { 
                if( value == null )
                    throw new ArgumentNullException(
                        "Font",
                        "You must provide a value for the tool-tip font"
                        );

                else
                    m_font = value; 
            }
        }

        /// <summary>
        /// Determines if the tool-tip background will be rendered when there
        /// is no tool tip.
        /// </summary>
        [
        DefaultValue( false ),
        Description( "Determines if the tool-tip background will be rendered when there is no tool-tip." )
        ]
        public bool RenderBackgroundOnEmpty 
        {
            get { return m_renderEmptyBack; }
            set { m_renderEmptyBack = value; }
        }
    }

    #endregion

    #region Standard Tool Tip Renderer

    /// <summary>
    /// This class provides basic functionality for describing and rendering
    /// tool tips.
    /// </summary>
    [
    Serializable(),
    TypeConverter( typeof( ExpandableObjectConverter ) )
    ]
    public class StandardToolTipRenderer 
        : StandardToolTipData, IToolTipRenderer
    {
        private int m_toolTipRadius = 30;

        /// <summary>
        /// The radius of the tool tip's background.
        /// </summary>
        [
        DefaultValue( 30 ),
        Description( "The radius of the tool tip's background circle" )
        ]
        public int BackgroundRadius 
        {
            get { return m_toolTipRadius; }
            set 
            {
                if( value <= 0 )
                    throw new ArgumentOutOfRangeException(
                        "BackgroundRadius",
                        value,
                        "The background radius must be at least 1"
                        );

                else
                    m_toolTipRadius = value;
            }
        }

        #region IToolTipRenderer Members

        /// <summary>
        /// Renders the tool tip bacground centered on the provided center 
        /// point, and draws the tool tip text on top of that.
        /// </summary>
        public void Render(
            Graphics g, 
            String toolTip, 
            Point menuCenter, 
            Rectangle renderArea
            )
        {
            RectangleF textArea = new RectangleF(
                menuCenter.X - m_toolTipRadius,
                menuCenter.Y - m_toolTipRadius,
                m_toolTipRadius * 2, 
                m_toolTipRadius * 2
                );

            // Render the background.
            g.FillEllipse(
                new SolidBrush( base.RenderingBackgroundColor ),
                textArea
                );

            // Render the background's edge.
            g.DrawEllipse(
                new Pen( base.RenderingBorderColor, base.BorderThickness ),
                textArea
                );

            // Render the text.
            if( toolTip != null && toolTip != "" ) 
            {
                StringFormat f = 
                    new StringFormat( StringFormatFlags.FitBlackBox );

                f.LineAlignment = StringAlignment.Center;
                f.Alignment = StringAlignment.Center;

                f.Trimming = StringTrimming.EllipsisCharacter;

                g.DrawString(
                    toolTip,
                    base.Font,
                    new SolidBrush( base.RenderingForegroundColor ),
                    textArea,
                    f
                    );
            }
        }

        /// <summary>
        /// If <see cref="CircularMenu.StandardToolTipData.RenderBackgroundOnEmpty"/> 
        /// is true, behaves like <see cref="Render"/> when toolTip is the 
        /// empty string ("").  Otherwise, performs no action.
        /// </summary>
        public void RenderEmpty(
            Graphics g, 
            Point menuCenter, 
            Rectangle renderArea
            )
        {
            if( base.RenderBackgroundOnEmpty )
                Render( g, "", menuCenter, renderArea );
        }

        #endregion
    }

    #endregion

    #region Below-Menu Tool Tip Renderer

    /// <summary>
    /// Provides a tool-tip renderer that is draws tool tips below the menu, 
    /// instead of centrally (the default position).
    /// </summary>
    public class BelowMenuToolTipRenderer 
        : StandardToolTipData, IExtraSpaceToolTipRenderer
    {
        #region IExtraSpaceToolTipRenderer Members

        /// <summary>
        /// Determines the space required for the extra tool tips.
        /// </summary>
        public Rectangle GetMaximumRenderArea(
            Graphics g, 
            Point menuCenter, 
            Size defaultMenuSize
            )
        {
            // Figure out the text height.
            int textHeight = (int)g.MeasureString(
                "Sample Text j X D ! |",
                this.Font
                ).Height + 4;

            // Return our desired rectangle.
            return new Rectangle(
                0, defaultMenuSize.Height + 4,
                defaultMenuSize.Width, textHeight
                );
        }

        #endregion

        #region IToolTipRenderer Members

        /// <summary>
        /// Renders the tool tip for the given menu option.
        /// </summary>
        public void Render(
            Graphics g, 
            String toolTip, 
            Point menuCenter, 
            Rectangle renderArea
            )
        {
            RectangleF textArea = new RectangleF(
                renderArea.Left,
                renderArea.Top,
                renderArea.Width,   
                renderArea.Height   
                );

            // Render the background.
            g.FillRectangle(
                new SolidBrush( base.RenderingBackgroundColor ),
                textArea
                );

            // Render the background's edge.
            g.DrawRectangle(
                new Pen( base.RenderingBorderColor, base.BorderThickness ),
                textArea.Left,
                textArea.Top,
                textArea.Width - 1,
                textArea.Height - 1
                );

            // Render the text.
            if( toolTip != null && toolTip != "" ) 
            {
                StringFormat f = 
                    new StringFormat( StringFormatFlags.FitBlackBox );

                f.LineAlignment = StringAlignment.Center;
                f.Alignment = StringAlignment.Center;

                f.Trimming = StringTrimming.EllipsisCharacter;

                g.DrawString(
                    toolTip,
                    base.Font,
                    new SolidBrush( base.RenderingForegroundColor ),
                    textArea,
                    f
                    );
            }
        }

        /// <summary>
        /// If <see cref="CircularMenu.StandardToolTipData.RenderBackgroundOnEmpty"/> 
        /// is true, behaves like <see cref="Render"/> when toolTip is the 
        /// empty string (""). Otherwise, performs no action.
        /// </summary>
        public void RenderEmpty(
            Graphics g, 
            Point menuCenter, 
            Rectangle renderArea
            )
        {
            if( base.RenderBackgroundOnEmpty )
                Render( g, "", menuCenter, renderArea );
        }

        #endregion
    }


    #endregion
}
