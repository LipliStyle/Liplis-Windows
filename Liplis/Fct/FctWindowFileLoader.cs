//=======================================================================
//  ClassName : ObjWindowFile
//  概要      : ウインドウオブジェクト
//
//  2013/08/31 Liplis3.0.5 ico_sleep.png→ico_zzz.pngに変更
//                         バージョン違い windowリソース 互換性チェック機能追加
//  Liplis3.0
//  Copyright(c) 2010-2013 LipliStyle. All Rights Reserved. 
//=======================================================================
using System.Drawing;
using Liplis.Common;
using Liplis.Fct;
using System;

namespace Liplis.Fct
{
    public class FctWindowFileLoader
    {

        /// <summary>
        /// compatibilityCheck
        /// 互換性チェック
        /// 2013/08/31 ver3.0.5
        /// </summary>
        #region compatibilityCheck
        protected void compatibilityCheck(string loadSkin)
        {
            //ver3.0.4以下のスキンの場合、スリープアイコンファイルが異なるため、検出したらコンバート
            if (LpsPathControllerCus.checkFileExist(LpsPathControllerCus.getWindowPath(loadSkin) + LiplisDefine.ICO_SLEEP))
            {
                LpsPathControllerCus.reNameFile(LpsPathControllerCus.getWindowPath(loadSkin), LiplisDefine.ICO_SLEEP, LiplisDefine.ICO_ZZZ);
            }
        }
        #endregion

        /// <summary>
        /// loadIcon
        /// アイコンをロードする
        /// </summary>
        /// <returns>ビットマップ</returns>
        #region loadIcon
        protected Bitmap loadIcon(string loadSkin, string fileName)
        {
            try
            {
                Bitmap canvas = new Bitmap(32, 32);

                using (Bitmap image = new Bitmap(LpsPathControllerCus.getWindowPath(loadSkin) + fileName))
                {
                    using (Graphics g = Graphics.FromImage(canvas))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(image, 0, 0, 32, 32);

                    }
                }

                return canvas;
            }
            catch
            {
                return new Bitmap(1, 1);
            }
        }
        #endregion

        /// <summary>
        /// reductionBitmap
        /// アイコンを縮小する
        /// </summary>
        /// <returns>ビットマップ</returns>
        #region reductionBitmap
        protected Bitmap reductionBitmap(Bitmap source)
        {
            try
            {
                Bitmap canvas = new Bitmap(32, 32);

                using (Bitmap image = source)
                {
                    using (Graphics g = Graphics.FromImage(canvas))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(image, 0, 0, 32, 32);
                    }
                }

                return canvas;
            }
            catch
            {
                return new Bitmap(1, 1);
            }
        }
        #endregion



        /// <summary>
        /// loadBitmap
        /// ビットマップをロードする
        /// </summary>
        /// <returns>ビットマップ</returns>
        #region loadBitmap
        protected Bitmap loadBitmap(string loadSkin, string fileName)
        {
            try
            {
                return new Bitmap(LpsPathControllerCus.getWindowPath(loadSkin) + fileName);
            }
            catch
            {
                return new Bitmap(1, 1);
            }
        }
        #endregion
    }
}
