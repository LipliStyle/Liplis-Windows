using System;
using System.ComponentModel;
using System.Drawing;

namespace CircularMenu
{
    #region Circular Layout Manager

    /// <summary>
    /// Defines a super type that can assist subtypes in creating circular
    /// arrangements.
    /// </summary>
    [Serializable]
    public class CircularLayoutManager : IFrameLayoutManager
    {
        /// <summary>
        /// Returns the position of the provided option for the provided frame.
        /// Options are laid out in a clockwise, circular manner ending 
        /// directly to the right of the clicked position.
        /// </summary>
        public Point GetOptionPosition(
            int radius,
            int optionIndex, 
            int optionCount, 
            int frameIndex, 
            int frameCount
            ) 
        {
            float theta = (float)(optionIndex + 1) * 
                ((2 * (float)Math.PI) / (float)optionCount);

            float fradius = (float)radius;

            if( frameIndex < frameCount - 1 ) 
            {
                theta = ModifyTheta(
                    theta,
                    optionIndex,
                    optionCount,
                    frameIndex,
                    frameCount
                    );

                fradius = ModifyRadius(
                    fradius,
                    optionIndex,
                    optionCount,
                    frameIndex,
                    frameCount
                    );
            }

            return new Point(
                (int)(fradius * Math.Cos( theta )),
                (int)(fradius * Math.Sin( theta ))
                );
        }

        /// <summary>
        /// Simply returns the theta value without modification.
        /// </summary>
        protected virtual float ModifyTheta(
            float theta,
            float optionIndex,
            float optionCount,
            float frameIndex,
            float frameCount
            ) 
        {
            return theta;
        }

        /// <summary>
        /// Simply returns the radius without modification.
        /// </summary>
        protected virtual float ModifyRadius( 
            float radius,
            float optionIndex,
            float optionCount,
            float frameIndex,
            float frameCount
            ) 
        {
            return radius;
        }

        /// <summary>
        /// Creates and returns an independent copy of this layout manager.
        /// </summary>
        public virtual object Clone() 
        {
            return new CircularLayoutManager();
        }
    }

    #endregion

    #region Starburst Layout Manager

    /// <summary>
    /// A layout manager that animates options "bursting" rapidly from the
    /// origin and slowing towards their final points.
    /// </summary>
    [Serializable]
    public class StarburstLayoutManager : CircularLayoutManager
    {
        /// <summary>
        /// Increases the radius from zero towards its normal value.
        /// </summary>
        protected override float ModifyRadius(
            float radius, 
            float optionIndex, 
            float optionCount, 
            float frameIndex, 
            float frameCount
            )
        {
            return radius * (float)Math.Sqrt( frameIndex / frameCount );
        }

        /// <summary>
        /// Creates and returns an independent copy of this layout manager.
        /// </summary>
        public override object Clone()
        {
            return new StarburstLayoutManager();
        }

    }

    #endregion

    #region Spin Layout Manager

    /// <summary>
    /// A layout manager that animates options spinning rapidly along the
    /// circle's edge and slowing towards their final points.  This effect is
    /// particularly cool when used with a fade-in frame modifier.
    /// </summary>
    [Serializable]
    public class SpinLayoutManager : CircularLayoutManager
    {
        /// <summary>
        /// Modifies the theta value by moving it along the perimeter.
        /// </summary>
        protected override float ModifyTheta(
            float theta, 
            float optionIndex, 
            float optionCount, 
            float frameIndex, 
            float frameCount
            )
        {
            return theta - (float)Math.PI + 
                (float)(Math.PI * Math.Sqrt( frameIndex / frameCount ));
        }

        /// <summary>
        /// Creates and returns an independent copy of this layout manager.
        /// </summary>
        public override object Clone()
        {
            return new SpinLayoutManager();
        }
    }

    #endregion

    #region Perimeter Unfold Layout Manager

    /// <summary>
    /// A layout manager that animates options "unfolding" along the circle's
    /// perimeter from 0 radians to their final positions.
    /// </summary>
    [Serializable]
    public class PerimeterUnfoldLayoutManager : CircularLayoutManager
    {
        /// <summary>
        /// Modifies the theta value by moving it slowly from zero to the 
        /// normal value as the animation advances.
        /// </summary>
        protected override float ModifyTheta(
            float theta, 
            float optionIndex, 
            float optionCount, 
            float frameIndex, 
            float frameCount
            )
        {
            return theta * (float)Math.Sqrt( frameIndex / frameCount );
        }

        /// <summary>
        /// Creates and returns an independent copy of this layout manager.
        /// </summary>
        public override object Clone()
        {
            return new PerimeterUnfoldLayoutManager();
        }
    }

    #endregion

    #region Spinning Starburst Layout Manager

    /// <summary>
    /// A combination of the <see cref="StarburstLayoutManager"/> and
    /// <see cref="SpinLayoutManager"/> managers.
    /// </summary>
    [Serializable]
    public class SpinningStarburstLayoutManager : CircularLayoutManager
    {
        /// <summary>
        /// Performs the starburst modification.
        /// </summary>
        protected override float ModifyRadius(
            float radius, 
            float optionIndex, 
            float optionCount, 
            float frameIndex, 
            float frameCount
            )
        {
            return radius * (float)Math.Sqrt( frameIndex / frameCount );
        }

        /// <summary>
        /// Performs the spin modification.
        /// </summary>
        protected override float ModifyTheta(
            float theta, 
            float optionIndex, 
            float optionCount, 
            float frameIndex, 
            float frameCount
            )
        {
            return theta - (float)Math.PI + 
                (float)(Math.PI * Math.Sqrt( frameIndex / frameCount ));
        }

        /// <summary>
        /// Creates and returns an independent copy of this layout manager.
        /// </summary>
        public override object Clone()
        {
            return new SpinningStarburstLayoutManager();
        }
    }

    #endregion

    #region Unfolding Starburst Layout Manager 

    /// <summary>
    /// A combination of the <see cref="StarburstLayoutManager"/> and
    /// <see cref="PerimeterUnfoldLayoutManager"/> types.
    /// </summary>
    [Serializable]
    public class UnfoldingStarburstLayoutManager : CircularLayoutManager 
    {
        /// <summary>
        /// Performs the starbursh modification.
        /// </summary>
        protected override float ModifyRadius(
            float radius, 
            float optionIndex, 
            float optionCount, 
            float frameIndex, 
            float frameCount
            )
        {
            return radius * (float)Math.Sqrt( frameIndex / frameCount );
        }

        /// <summary>
        /// Performs the unfold modifcation.
        /// </summary>
        protected override float ModifyTheta(
            float theta, 
            float optionIndex, 
            float optionCount, 
            float frameIndex, 
            float frameCount
            )
        {
            return theta * (float)Math.Sqrt( frameIndex / frameCount );
        }

        /// <summary>
        /// Creates and returns an independent copy of this layout manager.
        /// </summary>
        public override object Clone()
        {
            return new UnfoldingStarburstLayoutManager();
        }
    }

    #endregion
}
