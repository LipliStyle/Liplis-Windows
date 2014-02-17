using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Liplis.Pic
{
    public static class ComImageController
    {
        /// <summary>
        /// ファイルサイズを返す。
        /// </summary>
        /// <returns></returns>
        #region createThumbnail
        public static Image createThumbnail(Image orig)
        {
            return orig.GetThumbnailImage(
              150, 107, delegate { return false; }, IntPtr.Zero);
        }
        public static Image createThumbnail(Image orig, int hi, int wid)
        {
            return orig.GetThumbnailImage(
              wid, hi, delegate { return false; }, IntPtr.Zero);
        }
        #endregion
    }
}
