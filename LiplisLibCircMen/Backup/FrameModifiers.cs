using System;
using System.Drawing;
using PixelEffects;

namespace CircularMenu
{
    /// <summary>
    /// Defines a frame modifier that slowly fades in menu options during the
    /// couse of the animation.  Frame options approach a final opacity of 
    /// 255 (fully opaque).
    /// </summary>
    [Serializable]
    public class FadeInFrameModifier : IFrameModifier
    {
        /// <summary>
        /// Modifies the provided option image, given the current position in
        /// the animation.
        /// </summary>
        public FrameOptionData ModifyFrame(
            FrameOptionData originalOption,
            int frameIndex, 
            int frameCount
            )
        {
            if( originalOption != null && originalOption.HasBitmap ) 
            {
                // Determine maximum opacity as a function of the current 
                // frame. 
                byte alpha = 
                    (byte)(255.0 * 
                    (float)Math.Sqrt( (float)frameIndex / (float)frameCount) 
                    );

                // Make the image only so opaque! 
                Bitmap option = new Bitmap( originalOption.OptionBitmap );
                Effects.SetImageOpacity( option, alpha );

                // Return the modified image. 
                return new FrameOptionData(
                    originalOption.CenterPixelPosition,
                    option
                    );
            }
            else
                return originalOption;
        }

        /// <summary>
        /// Creates and returns an independent copy of this modifier.
        /// </summary>
        public object Clone() { return new FadeInFrameModifier(); }
    }

    /// <summary>
    /// A frame modifier that performs no action.
    /// </summary>
    [Serializable]
    public class NoOpFrameModifier : IFrameModifier
    {
        /// <summary>
        /// Simply returns option.
        /// </summary>
        public FrameOptionData ModifyFrame(
            FrameOptionData option, 
            int frameIndex, 
            int frameCount
            )
        {
            return option;
        }

        /// <summary>
        /// Creates and returns an independent copy of this modifier.
        /// </summary>
        public object Clone() { return new NoOpFrameModifier(); }
    }
  
    /// <summary>
    /// A frame modifier that "zooms in" on an image, resizing it from minimum
    /// width and height to its current width and height.
    /// </summary>
    [Serializable]
    public class ZoomInFrameModifier : IFrameModifier
    {
        /// <summary>
        /// Returns a new option that is the result of the image resized by
        /// a percentage that is dependent on the current position of the
        /// animation.
        /// </summary>
        public FrameOptionData ModifyFrame(
            FrameOptionData option, 
            int frameIndex, 
            int frameCount
            )
        {
            if( option != null && option.HasBitmap ) 
            {
                float scale = (float)Math.Sqrt( 
                    (float)frameIndex / (float)frameCount 
                    );

                int newWidth = (int)Math.Max( 
                    1, option.OptionBitmap.Width * scale 
                    );
                int newHeight = (int)Math.Max( 
                    1, 
                    option.OptionBitmap.Height * scale 
                    );

                Bitmap newImage = new Bitmap( 
                    option.OptionBitmap, 
                    new Size( newWidth, newHeight ) 
                    );

                return new FrameOptionData(
                    option.CenterPixelPosition,
                    newImage
                    );
            }
            else
                return option;
        }

        /// <summary>
        /// Creates and returns an independent copy of this modifier.
        /// </summary>
        public object Clone() { return new ZoomInFrameModifier(); }
    }

    /// <summary>
    /// A combination of the <see cref="FadeInFrameModifier"/> and
    /// <see cref="ZoomInFrameModifier"/> classes.
    /// </summary>
    [Serializable]
    public class FadeInZoomFrameModifier : IFrameModifier
    {
        /// <summary>
        /// Modifies the frame by fading it in and transitioning it from a 
        /// one-pixel image to its normal size.
        /// </summary>
        public FrameOptionData ModifyFrame(
            FrameOptionData option, 
            int frameIndex, 
            int frameCount
            )
        {
            if( option != null && option.HasBitmap ) 
            {
                // Scale the image down first. 
                float scale = (float)Math.Sqrt( 
                    (float)frameIndex / (float)frameCount 
                    );

                int newWidth = (int)Math.Max( 
                    1, option.OptionBitmap.Width * scale 
                    );
                int newHeight = (int)Math.Max( 
                    1, 
                    option.OptionBitmap.Height * scale 
                    );

                Bitmap newImage = new Bitmap( 
                    option.OptionBitmap, 
                    new Size( newWidth, newHeight ) 
                    );

                // Now modify its transparency. 
                Effects.SetImageOpacity(
                    newImage,
                    (byte)(255 * scale)
                    );

                // All set. 
                return new FrameOptionData(
                    option.CenterPixelPosition,
                    newImage
                    );
            }
            else
                return option;
        }

        /// <summary>
        /// Creates and returns an independent copy of this modifier.
        /// </summary>
        public object Clone() { return new FadeInZoomFrameModifier(); }
    }

    /// <summary>
    /// A modified animation in which menu options "burn into" the screen.
    /// All white menu option masks are first faded in for the first 1/3 of
    /// the animation, and then the white masks are faded to the options 
    /// themselves in the last two thirds.
    /// </summary>
    /// <remarks>
    /// This is a slower modifier, so it should be used only if it is 
    /// expected that the animation cache will stay consistent through many
    /// popups.
    /// </remarks>
    [Serializable]
    public class BurnInFrameModifier : IFrameModifier
    {
        /// <summary>
        /// Applies the burn-in modification to the provided frame option.
        /// </summary>
        public FrameOptionData ModifyFrame(
            FrameOptionData option, 
            int frameIndex, 
            int frameCount
            )
        {
            if( option != null && option.HasBitmap ) 
            {
                // Get the transition frame. 
                int transition = (int)Math.Max( 1, frameCount / 3 );

                if( frameIndex < transition ) 
                {
                    // Burn in. 
                    int frames = transition;
                    float percent = (float)Math.Pow(
                        (frameIndex + 1.0) / transition,
                        2
                        );

                    return BurnIn( option, percent );
                }
                else 
                {
                    // Calm down. 
                    int frames = frameCount - transition;
                    float percent = (float)Math.Pow(
                        1 - (frameIndex - transition) / (float)frameCount,
                        2
                        );

                    return CalmDown( option, percent );
                }
            }
            else
                return option;
        }

        /// <summary>
        /// Calculates and returns a while mask for the provided source
        /// bitmap.
        /// </summary>
        private Bitmap GetMask( Bitmap source ) 
        {
            Bitmap mask = new Bitmap( source.Width, source.Height );

            for( int x = 0; x < source.Width; x++ )
                for( int y = 0; y < source.Height; y++ ) 
                    if( source.GetPixel( x, y ).A == 255 )
                        mask.SetPixel( x, y, Color.White );
                    else
                        mask.SetPixel( x, y, Color.Transparent );

            return mask;
        }

        /// <summary>
        /// Does the "burn-in" portion of the frame modifier.
        /// </summary>
        private FrameOptionData BurnIn( 
            FrameOptionData option,
            float percentAlpha
            ) 
        {
            Bitmap mask = GetMask( option.OptionBitmap );
            Effects.SetImageOpacity( 
                mask, 
                (byte)(255 * percentAlpha ) 
                );

            return new FrameOptionData(
                option.CenterPixelPosition,
                mask
                );
        }

        /// <summary>
        /// Fades the white down to the menu option.
        /// </summary>
        private FrameOptionData CalmDown(
            FrameOptionData option,
            float percentAlpha
            )
        {
            Bitmap mask = GetMask( option.OptionBitmap );
            Effects.SetImageOpacity(
                mask,
                (byte)(255 * percentAlpha )
                );

            Bitmap newImage = new Bitmap( option.OptionBitmap );
            Graphics g = Graphics.FromImage( newImage );

            g.DrawImage( mask, 0, 0 );
            g.Dispose();

            return new FrameOptionData(
                option.CenterPixelPosition,
                newImage
                );
        }

        /// <summary>
        /// Creates and returns an independent copy of this modifer.
        /// </summary>
        public object Clone() { return new BurnInFrameModifier(); }
    }
}
