//=======================================================================
//  ClassName : ObjBody
//  概要      : ボディオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Liplis.Msg
{
    public class ObjBodyGen : ObjBody
    {
        ///=============================
        ///プロパティ
        public string body11 { get; set; }
        public string body12 { get; set; }
        public string body21 { get; set; }
        public string body22 { get; set; }
        public string body31 { get; set; }
        public string body32 { get; set; }
        public string bodyDir { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="body11">目開、口デフォ</param>
        /// <param name="body12">目開、口反転</param>
        /// <param name="body21">目半、口デフォ</param>
        /// <param name="body22">目半、口半転</param>
        /// <param name="body31">目閉、口デフォ</param>
        /// <param name="body32">目閉、口反</param>
        #region ObjBody
        public ObjBodyGen(string body11, string body12, string body21, string body22, string body31, string  body32, string bodyDir)
        {
            this.body11 = body11;
            this.body12 = body12;
            this.body21 = body21;
            this.body22 = body22;
            this.body31 = body31;
            this.body32 = body32;
            this.bodyDir = bodyDir;
        }
        #endregion

        /// <summary>
        /// Bodyのゲッター
        /// </summary>
        /// <returns></returns>
        #region getBody
        public override Bitmap getBody11()
        {
            return new Bitmap(bodyDir + body11);
        }
        public override Bitmap getBody12()
        {
            return new Bitmap(bodyDir + body12);
        }
        public override Bitmap getBody21()
        {
            return new Bitmap(bodyDir + body21);
        }
        public override Bitmap getBody22()
        {
            return new Bitmap(bodyDir + body22);
        }
        public override Bitmap getBody31()
        {
            return new Bitmap(bodyDir + body31);
        }
        public override Bitmap getBody32()
        {
            return new Bitmap(bodyDir + body32);
        }
        #endregion

        /// <summary>
        /// reductionBitmap
        /// 画像を縮小する
        /// </summary>
        /// <returns>ビットマップ</returns>
        #region reductionBitmap
        private Bitmap reductionBitmap(Bitmap source, int hi, int wid)
        {
            try
            {
                Bitmap canvas = new Bitmap(hi, wid);

                using (Bitmap image = source)
                {
                    using (Graphics g = Graphics.FromImage(canvas))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(image, 0, 0, hi, wid);
                    }
                }

                return canvas;
            }
            catch
            {
                return source;
            }
        }
        #endregion


        /// <summary>
        /// 自分のインスタンスのコピーを返す
        /// </summary>
        /// <returns></returns>
        #region copy
        public ObjBodyGen copy()
        {
            return new ObjBodyGen(this.body11, this.body12, this.body21, this.body22, this.body31, this.body32, this.bodyDir);
        }
        #endregion
    }
}
