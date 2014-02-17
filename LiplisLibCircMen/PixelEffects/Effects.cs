using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PixelEffects
{
    /// <summary>
    /// This non-instantiable class provides static method for applying 
    /// graphical effects to images, such as a drop shadow or glow.
    /// </summary>
    public class Effects
    {
        /// <summary>
        /// Restrict constructor access.
        /// </summary>
        private Effects() { }

        #region Primary Methods

        /// <summary>
        /// Makes the provided color fully transparent wherever it appears in 
        /// the given bitmap.  Ignores the alpha channel for comparisson.
        /// </summary>
        /// <param name="image">
        /// The image to mask.  Cannot be null.
        /// </param>
        /// <param name="color">
        /// The color to make transparent.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided bitmap is a null reference.
        /// </exception>
        public static void MakeColorTransparent(
            Bitmap image,
            Color color
            )
        {
            if (image == null)
                throw new ArgumentNullException(
                    "image",
                    "You must provide an image to work with"
                    );

            else
            {
                Color transparent = Color.FromArgb(0, 0, 0, 0);

                for (int x = 0; x < image.Width; x++)
                    for (int y = 0; y < image.Width; y++)
                    {
                        Color pixel = image.GetPixel(x, y);
                        if (
                            pixel.R == color.R &&
                            pixel.G == color.G &&
                            pixel.B == color.B
                            )
                            image.SetPixel(x, y, transparent);
                    }
            }
        }

        /// <summary>
        /// Adjusts the opacity for every pixel in the provided image so that
        /// it is no greater than the provided maximum opacity level.
        /// </summary>
        /// <param name="image">
        /// The image whose opacity levels are to be adjusted.  Cannot be null.
        /// </param>
        /// <param name="maximumOpacity">
        /// The maximum alpha value for any pixel in the image.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provide image is a null reference.
        /// </exception>
        public static void SetImageOpacity(
            Bitmap image,
            byte maximumOpacity
            )
        {
            if (image == null)
                throw new ArgumentNullException(
                    "image",
                    "The image to make transparent cannot be null"
                    );

            else
                for (int x = 0; x < image.Width; x++)
                    for (int y = 0; y < image.Height; y++)
                    {
                        if (maximumOpacity == 0)
                            image.SetPixel
                                (x, y, Color.FromArgb(0, 0, 0, 0));

                        else
                        {
                            Color pixel = image.GetPixel(x, y);
                            if (pixel.A != 0)
                            {
                                image.SetPixel(
                                    x,
                                    y,
                                    Color.FromArgb(
                                        (int)(((float)maximumOpacity) * (pixel.A / 255.0)),
                                        pixel
                                        )
                                    );
                            }
                        }
                    }
        }

        /// <summary>
        /// Attempts to extract the provided resource from the provided 
        /// assembly as an image.  Throws an exception if the image cannot be
        /// extracted.
        /// </summary>
        /// <param name="assembly">
        /// The assembly to search for the resource within.
        /// </param>
        /// <param name="resourceName">
        /// The name of the resource to extract.
        /// </param>
        /// <returns>
        /// The extracted image.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if either of the provided arguments are null.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown if the resource cannot be found or is of an unsupported
        /// format.
        /// </exception>
        public static Bitmap GetAssemblyImageResource(
            Assembly assembly,
            string resourceName
            )
        {
            if (assembly == null)
                throw new ArgumentNullException(
                    "assembly",
                    "The parameter 'assembly' cannot be null"
                    );

            else if (resourceName == null)
                throw new ArgumentNullException(
                    "resourceName",
                    "The parameter 'resourceName' cannot be null"
                    );

            else
            {
                try
                {
                    return new Bitmap(
                        assembly.GetManifestResourceStream(resourceName)
                        );
                }
                catch
                {
                    throw new Exception(
                        "Could not extract the resource " +
                        resourceName + " from the assembly " +
                        assembly.FullName
                        );
                }
            }
        }

        /// <summary>
        /// Captures a section of the screen to a new bitmap resource and
        /// returns the bitmap.
        /// </summary>
        /// <param name="region">
        /// The specific region of the screen to capture.
        /// </param>
        public static Bitmap GetScreenCapture(Rectangle region)
        {
            // Get a DC for the screen.
            IntPtr dc1 = Api.CreateDC("DISPLAY", null, null, (IntPtr)null);

            // Get a graphics device for the screen.
            Graphics g1 = Graphics.FromHdc(dc1);

            // Get a bitmap to be the destination of the capture.
            Bitmap controlBackground = new Bitmap(region.Width, region.Height, g1);

            // Get a graphics device to render to the target.
            Graphics g2 = Graphics.FromImage(controlBackground);

            // Get a device context for the target.
            dc1 = g1.GetHdc();
            IntPtr dc2 = g2.GetHdc();

            // Copy!
            Api.BitBlt(
                dc2,
                0,
                0,
                region.Width,
                region.Height,
                dc1,
                region.X,
                region.Y,
                Api.RASTER_OP_SRCPAINT
                );

            // Clean up.
            g1.ReleaseHdc(dc1);
            g2.ReleaseHdc(dc2);

            // Return the result.
            return controlBackground;
        }

        /// <summary>
        /// Increases the opacity of the provided color, taking care not to
        /// increase beyond the provided maximum.
        /// </summary>
        /// <param name="currentValue">
        /// The current pixel value.
        /// </param>
        /// <param name="step">
        /// The amount to increase the current pixel's value by.  Must be at
        /// least zero.
        /// </param>
        /// <param name="maximumValue">
        /// The maximum value for the pixel's alpha channel.  Must be between
        /// 0 and 255.
        /// </param>
        /// <returns>
        /// A new color structure representing a more opaque version of the
        /// provided color.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the value for the provided maximum or step is out of 
        /// range.
        /// </exception>
        public static Color AugmentPixel(
            Color currentValue,
            int step,
            int maximumValue
            )
        {
            if (maximumValue < 0 || maximumValue > 255)
                throw new ArgumentOutOfRangeException(
                    "maximumValue",
                    maximumValue,
                    "The maximum must be between 0 and 255."
                    );

            else if (step < 0)
                throw new ArgumentOutOfRangeException(
                    "step",
                    step,
                    "The step must be at least zero."
                    );

            else if (currentValue.A + step < maximumValue)
                return Color.FromArgb(
                    currentValue.A + 5,
                    currentValue
                    );

            else
                return Color.FromArgb(
                    maximumValue,
                    currentValue
                    );
        }

        /// <summary>
        /// Calculates and produces a bitmap representing a drop-shadow for the
        /// provided bitmap.
        /// </summary>
        /// <param name="original">
        /// The bitmap whose drop-shadow is to be generated.
        /// </param>
        /// <param name="dropShadowColor">
        /// The color to use for the drop shadow.  Cannot be null.
        /// </param>
        /// <param name="blurRadius">
        /// This value defines the amount of "blur" placed in the drop shadow.
        /// Each pixel will cast a shadow having this radius.  Must be at least
        /// 0.  Note that larger values will significantly increase rendering
        /// time.
        /// </param>
        /// <param name="maximumShadowOpacity">
        /// Determines the maximum value for any pixel in the drop shadow's
        /// alpha channel.  Must be between 0 and 255.
        /// </param>
        /// <param name="shadowOpacityStep">
        /// Determines the amount by which alpha channel transparency in the
        /// drop shadow is augmented by.  Larger values will produce a sharper
        /// shadow, smaller values will produce a very subtle shadow.  Must
        /// be at least 1.
        /// </param>
        /// <returns>
        /// A new bitmap that is slightly larger than the provided bitmap
        /// (based on the blur radius) that contains the drop-shadow for the
        /// provided bitmap.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided image is a null reference.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if any of the provided arguments are outside the allowed
        /// range.
        /// </exception>
        public static Bitmap GetImageDropShadow(
            Bitmap original,
            Color dropShadowColor,
            int blurRadius,
            int maximumShadowOpacity,
            int shadowOpacityStep
            )
        {
            if (original == null)
                throw new ArgumentNullException(
                    "original",
                    "The original bitmap cannot be null"
                    );

            else if (blurRadius < 0)
                throw new ArgumentOutOfRangeException(
                    "blurRadius",
                    blurRadius,
                    "The blur radius must be at least 0"
                    );

            else if (maximumShadowOpacity < 0 || maximumShadowOpacity > 255)
                throw new ArgumentOutOfRangeException(
                    "maximumShadowOpacity",
                    maximumShadowOpacity,
                    "The maximum shadow opacity must be between 0 and 255"
                    );

            else if (shadowOpacityStep < 1)
                throw new ArgumentOutOfRangeException(
                    "shadowOpacityStep",
                    shadowOpacityStep,
                    "The shadow opacity step must be at least 1"
                    );

            else
            {
                int blurSize = blurRadius * 2;

                // Create the new bitmap to be slightly larger than the original.
                Bitmap ds = new Bitmap(
                    original.Width + blurSize,
                    original.Height + blurSize
                    );

                int[,] alphas = new int[ds.Width, ds.Height];

                // Now do the spread!
                for (int x = 0; x < original.Width; x++)
                    for (int y = 0; y < original.Height; y++)
                    {
                        Color pixel = original.GetPixel(x, y);

                        // Only blur non-transparent pixels.
                        if (pixel.A == 255)
                            for (int destX = x; destX <= x + blurSize; destX++)
                                for (int destY = y; destY <= y + blurSize; destY++)
                                    alphas[destX, destY] += shadowOpacityStep;
                    }

                // Copy the bitmap data to the image. 
                int r = dropShadowColor.R;
                int g = dropShadowColor.G;
                int b = dropShadowColor.B;

                for (int x = 0; x < ds.Width; x++)
                    for (int y = 0; y < ds.Height; y++)
                        ds.SetPixel(
                            x,
                            y,
                            Color.FromArgb(
                                Math.Min(alphas[x, y], maximumShadowOpacity),
                                r,
                                g,
                                b
                                )
                            );

                // Return the result.
                return ds;
            }
        }

        #endregion

        #region Shortcuts

        /// <summary>
        /// Calculates and returns a drop-shadow image that can be used with
        /// the provided bitmap.
        /// </summary>
        /// <param name="image">
        /// The image whose drop-shadow is to be created.  Cannot be null.
        /// </param>
        /// <returns>
        /// A new image that is four pixels wider and four pixels taller than
        /// the provided image.  This image will be filled entirely in black,
        /// but will have varying levels for the alpha channel.  This image
        /// should be drawn before the original image, and the original image
        /// should be drawn on top of the drop shadow, with a slight offset.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided image is a null reference.
        /// </exception>
        public static Bitmap GetImageDropShadow(Bitmap image)
        {
            return GetImageDropShadow(image, Color.Black, 4, 128, 5);
        }

        /// <summary>
        /// Returns a screen capture for the entire display area, across all
        /// monitors.
        /// </summary>
        public static Bitmap GetScreenCapture()
        {
            // Find the capture boundaries.
            int left = Screen.AllScreens[0].Bounds.Left;
            int top = Screen.AllScreens[0].Bounds.Top;
            int right = Screen.AllScreens[0].Bounds.Right;
            int bottom = Screen.AllScreens[0].Bounds.Bottom;

            for (int i = 1; i < Screen.AllScreens.Length; i++)
            {
                if (Screen.AllScreens[1].Bounds.Left < left)
                    left = Screen.AllScreens[1].Bounds.Left;

                if (Screen.AllScreens[i].Bounds.Right > right)
                    right = Screen.AllScreens[i].Bounds.Right;

                if (Screen.AllScreens[i].Bounds.Top < top)
                    top = Screen.AllScreens[i].Bounds.Top;

                if (Screen.AllScreens[i].Bounds.Bottom > bottom)
                    bottom = Screen.AllScreens[i].Bounds.Bottom;
            }

            // Return the capture.
            return GetScreenCapture(
                Rectangle.FromLTRB(left, top, right, bottom)
                );
        }

        #endregion
    }

    /// <summary>
    /// Provides access to Windows API functions.
    /// </summary>
    internal class Api
    {
        /// <summary>
        /// Restrict constructor access.
        /// </summary>
        private Api() { }

        /// <summary>
        /// A structure used by the AlphaBlend function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        /// <summary>
        /// Blits image data from one surface to another.
        /// </summary>
        /// <param name="hdcDest">
        /// The destination DC.
        /// </param>
        /// <param name="nXDest">
        /// The x-coordinate of the upper-left corner of the blitted data on
        /// the destination surface.
        /// </param>
        /// <param name="nYDest">
        /// The y-coordinate of the upper-left corner of the blitted data on
        /// the destination surface.
        /// </param>
        /// <param name="nWidth">
        /// The width of the rectangle to blit.
        /// </param>
        /// <param name="nHeight">
        /// The height of the rectangle to blit.
        /// </param>
        /// <param name="hdcSrc">
        /// The source DC.
        /// </param>
        /// <param name="nXSrc">
        /// The x-coordinate of the source rectangle's upper-left corner.
        /// </param>
        /// <param name="nYSrc">
        /// The y-coordinate of the source rectangle's upper-left corner.
        /// </param>
        /// <param name="dwRop">
        /// The raster operation code.  Use <see cref="RASTER_OP_DEFAULT"/>.
        /// </param>
        [DllImportAttribute("gdi32.dll")]
        public static extern bool BitBlt(
            IntPtr hdcDest,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            System.Int32 dwRop
            );

        public const Int32 RASTER_OP_SRCCOPY = 0xCC0020;

        public const Int32 RASTER_OP_DEFAULT = 13369376;

        public const Int32 RASTER_OP_SRCAND = 0x8800C6;

        public const Int32 RASTER_OP_SRCPAINT = 0xEE0086;

        /// <summary>
        /// Creates a device context for a given resource.
        /// </summary>
        /// <param name="lpszDriver">
        /// Driver name.  "DISPLAY" for the display.
        /// </param>
        /// <param name="lpszDevice">
        /// The device name.  Null for the display.
        /// </param>
        /// <param name="lpszOutput">
        /// Not used, should be null.
        /// </param>
        /// <param name="lpInitData">
        /// Optional printer data.  Set to (IntPtr)null for display.
        /// </param>
        [DllImportAttribute("gdi32.dll")]
        public static extern IntPtr CreateDC(
            string lpszDriver,
            string lpszDevice,
            string lpszOutput,
            IntPtr lpInitData
            );
    }
}
