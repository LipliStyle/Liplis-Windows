using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace CircularMenu
{
    #region Frame Layout Interface

    /// <summary>
    /// Defines a class that can position menu options within an animation
    /// frame.
    /// </summary>
    public interface IFrameLayoutManager : ICloneable
    {
        /// <summary>
        /// Returns the position of the central pixel of the given option's
        /// bitmap for the given frame.
        /// </summary>
        /// <param name="radius">
        /// The preferred distance between the central location (0, 0) and the
        /// center of option images.
        /// </param>
        /// <param name="optionIndex">
        /// The index of the option whose position is to be determined.
        /// </param>
        /// <param name="optionCount">
        /// The total number of options to be rendered.
        /// </param>
        /// <param name="frameIndex">
        /// The index of the frame within which the option is to be psitioned.
        /// </param>
        /// <param name="frameCount">
        /// The total number of frames to be rendered.
        /// </param>
        /// <returns></returns>
        Point GetOptionPosition( 
            int radius,
            int optionIndex,
            int optionCount,
            int frameIndex,
            int frameCount
            );
    }

    #endregion

    #region Frame Modifier Interface

    /// <summary>
    /// Defines an option that can modify an existing frame image during an
    /// animation.  
    /// </summary>
    /// <remarks>
    /// These modifiers are applied to every frame in an animation with the
    /// exception of the final frame.  For this reason, frame modifiers should
    /// attempt to render frames so that they approach a non-modified.
    /// </remarks>
    public interface IFrameModifier : ICloneable
    {
        /// <summary>
        /// Modifies the image from the provided menu option for the given
        /// animation frame.  Note that this method will not be called for the
        /// final frame. 
        /// </summary>
        /// <param name="option">
        /// The data for the menu option currently being modified.
        /// </param>
        /// <param name="frameIndex">
        /// The index of the frame currently being modified.
        /// </param>
        /// <param name="frameCount">
        /// The total number of frames to be rendered.
        /// </param>
        FrameOptionData ModifyFrame(
            FrameOptionData option,
            int frameIndex,
            int frameCount
            );
    }

    #endregion

    #region Menu Animation
   
    #region Type Editor

    /// <summary>
    /// A class that provides a modal graphical user interface for editing
    /// a <see cref="MenuAnimation"/> instance.
    /// </summary>
    public class MenuAnimationEditor : UITypeEditor
    {
        /// <summary>
        /// Edits the provided value.
        /// </summary>
        public override object EditValue(
            ITypeDescriptorContext context, 
            IServiceProvider provider, 
            object value
            )
        {
            if( value == null || !(value is MenuAnimation) )
                return value;

            else 
            {
                MenuAnimationEditorUI wnd = new MenuAnimationEditorUI(
                    (value as MenuAnimation).Clone() as MenuAnimation
                    );

                if( wnd.ShowDialog() == DialogResult.OK )
                    return wnd.Animation;
                else
                    return value;
            }
        }

        /// <summary>
        /// Indicates that this editor uses a modal dialog for editing.
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle
            ( ITypeDescriptorContext context )
        {
            return UITypeEditorEditStyle.Modal;
        }
    }

    #endregion

    #region Menu Animation

    /// <summary>
    /// Provides options that control a menu animation.
    /// </summary>
    /// <remarks>
    /// <p>Both the <see cref="CircularMenuPopup.OpeningAnimation"/> and 
    /// <see cref="CircularMenuPopup.ClosingAnimation"/> properties of the 
    /// <see cref="CircularMenuPopup"/> class accept instances of the 
    /// MenuAnimation class.  This class provides basic properties that 
    /// control both the final and animated layouts of the menu, as well as 
    /// special effects that are applied during the animation.
    /// </p>
    /// <p>The <see cref="FrameImageEffect"/> property of this class provides 
    /// access to the object that controls the special effects in the animation.
    /// Set this property to an object that implements the 
    /// <see cref="IFrameModifier"/> interface to specify the special effect 
    /// you desire.  The built-in options are:
    /// </p>
    /// <p><see cref="BurnInFrameModifier"/>:  This special effect produces 
    /// white-masks of the option images which it first fades in to full white.
    /// From there, the white images fade towards the normal images.
    /// </p>
    /// <p><see cref="FadeInFrameModifier"/>:  This special effect fades the 
    /// option images in from fully transparent to their normal settings.
    /// </p>
    /// <p><see cref="FadeInZoomFrameModifier"/>:  Combines the 
    /// <see cref="FadeInFrameModifier"/> and 
    /// <see cref="ZoomInFrameModifier"/> effects.
    /// </p>
    /// <p><see cref="NoOpFrameModifier"/>:  This special type performs no 
    /// action on the menu images.  You must use this type to turn off special 
    /// effects, since the <see cref="FrameImageEffect"/> property cannot be 
    /// set to <c>null</c>.
    /// </p>
    /// <p><see cref="ZoomInFrameModifier"/>:  This effect enlarges the images 
    /// from 1x1 to their full size.
    /// </p>
    /// <p>Similarly, the <see cref="LayoutAnimator"/> property holds a 
    /// reference to an object that implements the 
    /// <see cref="IFrameLayoutManager"/> interface.  Objects of this type are 
    /// responsible for the layout of menu options during and after an 
    /// animation.  There are six built-in layout managers:
    /// </p>
    /// <p><see cref="CircularLayoutManager"/>:  This class is the base class 
    /// for the other options, and places menu options in a circular formation 
    /// around the click position.  However, this class does not animate the 
    /// options in any way.
    /// </p>
    /// <p><see cref="PerimeterUnfoldLayoutManager"/>:  Animates options moving
    /// along the perimeter of the circle defined by 
    /// <see cref="CircularLayoutManager"/>.  All options start at the same 
    /// position and move with different speeds towards their final position.  
    /// This provides an ìunfoldingÅEeffect.
    /// </p>
    /// <p><see cref="SpinLayoutManager"/>:  Similarly to the 
    /// <see cref="PerimeterUnfoldLayoutManager"/>, this manager animates 
    /// options moving along the perimeter of the circle.  Unlike the other 
    /// form, however, options start off in separate positions (180 degrees 
    /// from their final location), and all move with the same speed towards 
    /// their final location.
    /// </p>
    /// <p><see cref="StarburstLayoutManager"/>:  While this manager doesnít 
    /// animate the degree positions of the options, it does animate them 
    /// moving along their radii from the center of the circle out towards the 
    /// edge.
    /// </p>
    /// <p><see cref="SpinningStarburstLayoutManager"/>:  Combines the effects 
    /// of the spin layout manager and the starburst layout manager.
    /// </p>
    /// <p><see cref="UnfoldingStarburstLayoutManager"/>:  Combines the 
    /// effects of the perimeter unfold layout manager and the starburst 
    /// layout manager.
    /// </p>
    /// <p>You can control the length (and graininess) of the animation via 
    /// the <see cref="FramesToRender"/> property.  The final frame in the 
    /// animation is adopted as the layout for the menu while the user is 
    /// interacting with it.  Because of this, this property has a minimum 
    /// value of one.
    /// </p>
    /// <p>Frames are rendered once every thirty milliseconds, which produces 
    /// a frame rate of about 30 frames per second.  This means that an 
    /// animation of 15 frames will take half a second to complete, a 30-frame 
    /// animation takes about a second, and a 60-frame animation takes two 
    /// seconds.
    /// </p>
    /// <p>Note that the animations may move slower when using more menu 
    /// options and on slower computers.  The complexity of the layout manager 
    /// and frame modifier can also slow down an animation.  Because of this, 
    /// it is best to work with fewer frames.
    /// </p>
    /// <p>Finally, you can use the <see cref="GetUncachedAnimation"/>, 
    /// <see cref="GetAnimation"/>, and <see cref="ClearAnimation"/> methods 
    /// to control the cached menu animation.  The first of these always 
    /// regenerates the animation and returns the result without replacing or 
    /// setting the actual cached animation.  The other effects work with the 
    /// cached animation.
    /// </p>
    /// </remarks>
    [
    Serializable,
    TypeConverter( typeof( ExpandableObjectConverter ) ),
    Editor( typeof( MenuAnimationEditor ), typeof( UITypeEditor ) )
    ]
    public abstract class MenuAnimation : ICloneable
    {
        /// <summary>
        /// The object responsible for laying out the frames.
        /// </summary>
        private IFrameLayoutManager m_layout = 
            new UnfoldingStarburstLayoutManager();

        /// <summary>
        /// A modifier to apply to frame option images during the animation.
        /// </summary>
        private IFrameModifier m_modifier = new FadeInFrameModifier();

        /// <summary>
        /// The number of frames to render.
        /// </summary>
        private int m_framesToRender = 15;

        /// <summary>
        /// A prepared, cached animation.
        /// </summary>
        [NonSerialized]
        private FrameCollection m_animation = null;

        /// <summary>
        /// The number of frames to render for the animation.  Must be at
        /// least one.
        /// </summary>
        /// <remarks>
        /// <p>Frames are rendered once every thirty milliseconds, which produces 
        /// a frame rate of about 30 frames per second.  This means that an 
        /// animation of 15 frames will take half a second to complete, a 30-frame 
        /// animation takes about a second, and a 60-frame animation takes two 
        /// seconds.
        /// </p>
        /// <p>Note that the animations may move slower when using more menu 
        /// options and on slower computers.  The complexity of the layout manager 
        /// and frame modifier can also slow down an animation.  Because of this, 
        /// it is best to work with fewer frames.
        /// </p>
        /// </remarks>
        [
        Description( "The number of frames to render for the animiation.  Must be at least one." ),
        DefaultValue( 15 )
        ]
        public int FramesToRender 
        {
            get { return m_framesToRender; }
            set 
            {
                if( value < 1 ) 
                    throw new ArgumentOutOfRangeException();

                else
                    m_framesToRender = value;
            }
        }

        /// <summary>
        /// Defines the object that is responsible for laying out menu options
        /// during and at the end of an animation.  Cannot be null.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "Defines the layout manager for this animation sequence." ),
        RefreshProperties( RefreshProperties.All )
        ]
        public IFrameLayoutManager LayoutAnimator
        {
            get { return m_layout; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "LayoutAnimator",
                        "The layout animator property cannot be null"
                        );

                else
                    m_layout = value;
            }
        }

        /// <summary>
        /// Defines an object that is responsible for modifying the option 
        /// images during the course of an animation.  Cannot be null.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if an attempt is made to set this property to null.
        /// </exception>
        [
        Description( "Provides an object that modifies the menu options during the course of the animation." ),
        RefreshProperties( RefreshProperties.All )
        ]
        public IFrameModifier FrameImageEffect 
        {
            get { return m_modifier; }
            set 
            {
                if( value == null )
                    throw new ArgumentNullException(
                        "FrameModifier",
                        "The frame modifier property cannot be null"
                        );
                else
                    m_modifier = value;
            }
        }

        /// <summary>
        /// Ensures that the cached set of frames for this animation is 
        /// cleared.
        /// </summary>
        public void ClearAnimation() 
        {
            m_animation = null;
        }

        /// <summary>
        /// Returns a new set of frames for this animation, and does not cache
        /// the result.
        /// </summary>
        /// <param name="menuOptions">
        /// The menu options to animate.  Cannot be null.
        /// </param>
        /// <param name="radius">
        /// The radius to apply to the menu layout.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided collection is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided collection does not contain any visible 
        /// options to animate.
        /// </exception>
        public abstract FrameCollection GetUncachedAnimation(
            MenuOptionCollection menuOptions,
            int radius
            );

        /// <summary>
        /// Returns a set of frames for this animation.
        /// </summary>
        /// <param name="menuOptions">
        /// The menu options to animate.  Cannot be null.
        /// </param>
        /// <param name="radius">
        /// The radius to apply to the menu layout.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided collection is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided collection does not contain any visible 
        /// options to animate.
        /// </exception>
        public FrameCollection GetAnimation( 
            MenuOptionCollection menuOptions,
            int radius
            ) 
        {
            return GetAnimation( menuOptions, radius, true );
        }

        /// <summary>
        /// Returns a set of frames for this animation.
        /// </summary>
        /// <param name="menuOptions">
        /// The menu options to animate.  Cannot be null.
        /// </param>
        /// <param name="radius">
        /// The radius to apply to the menu layout.
        /// </param>
        /// <param name="cacheResult">
        /// If true, the result of this method will be saved for quick 
        /// reference later.  To clear a cached result, use the 
        /// <see cref="ClearAnimation"/> method.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided collection is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided collection does not contain any visible 
        /// options to animate.
        /// </exception>
        public FrameCollection GetAnimation( 
            MenuOptionCollection menuOptions, 
            int radius,
            bool cacheResult 
            ) 
        {
            if( m_animation != null )
                return m_animation;

            else 
            {
                FrameCollection animation = 
                    GetUncachedAnimation( menuOptions, radius );

                if( cacheResult ) m_animation = animation;
                return animation;
            }
        }

        /// <summary>
        /// Returns a new, independent copy of this animation.  Note that when
        /// an animation is cloned, the clone will not have a cached animation.
        /// </summary>
        public object Clone() 
        {
            MenuAnimation clone = GetClone();
            clone.m_framesToRender = m_framesToRender;
            clone.m_layout = (IFrameLayoutManager)m_layout.Clone();
            clone.m_modifier = (IFrameModifier)m_modifier.Clone();

            return clone;
        }

        /// <summary>
        /// Returns a new instance of the implementing class.
        /// </summary>
        protected abstract MenuAnimation GetClone();
    }

    #endregion

    /// <summary>
    /// A menu animation that animates forward.
    /// </summary>
    [Serializable]
    public class ForwardMenuAnimation : MenuAnimation
    {
        /// <summary>
        /// Returns a new set of frames for this animation, and does not cache
        /// the result.
        /// </summary>
        /// <param name="menuOptions">
        /// The menu options to animate.  Cannot be null.
        /// </param>
        /// <param name="radius">
        /// The radius to apply to the menu layout.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided collection is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided collection does not contain any visible 
        /// options to animate.
        /// </exception>
        public override FrameCollection GetUncachedAnimation(
            MenuOptionCollection menuOptions,
            int radius
            )
        {
            return new ForwardFrameCollection( 
                menuOptions,
                radius,
                FramesToRender,
                LayoutAnimator,
                FrameImageEffect
                );
        }

        /// <summary>
        /// Returns a new forward menu animation.
        /// </summary>
        /// <returns></returns>
        protected override MenuAnimation GetClone()
        {
            return new ForwardMenuAnimation();
        }
    }

    /// <summary>
    /// A menu animation that animates backwards.
    /// </summary>
    [Serializable]
    public class ReverseMenuAnimation : MenuAnimation
    {
        /// <summary>
        /// Returns a new set of frames for this animation, and does not cache
        /// the result.
        /// </summary>
        /// <param name="menuOptions">
        /// The menu options to animate.  Cannot be null.
        /// </param>
        /// <param name="radius">
        /// The radius to apply to the menu layout.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided collection is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the provided collection does not contain any visible 
        /// options to animate.
        /// </exception>
        public override FrameCollection GetUncachedAnimation(
            MenuOptionCollection menuOptions,
            int radius
            )
        {
            return new ReverseFrameCollection( 
                menuOptions,
                radius,
                FramesToRender,
                LayoutAnimator,
                FrameImageEffect
                );
        }

        /// <summary>
        /// Returns a new reverse-frame animation.
        /// </summary>
        protected override MenuAnimation GetClone()
        {
            return new ReverseMenuAnimation();
        }
    }

    #endregion
}
