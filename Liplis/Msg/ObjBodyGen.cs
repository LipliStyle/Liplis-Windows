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
        public List<string> lstTouch { get; set; }

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
        public ObjBodyGen(string body11, string body12, string body21, string body22, string body31, string  body32, string touch, string bodyDir)
        {
            this.body11 = body11;
            this.body12 = body12;
            this.body21 = body21;
            this.body22 = body22;
            this.body31 = body31;
            this.body32 = body32;
            this.bodyDir = bodyDir;
            this.lstTouch = new List<string>(touch.Split(','));
        }
        public ObjBodyGen(string body11, string body12, string body21, string body22, string body31, string body32, string bodyDir, List<string> lstTouch)
        {
            this.body11 = body11;
            this.body12 = body12;
            this.body21 = body21;
            this.body22 = body22;
            this.body31 = body31;
            this.body32 = body32;
            this.bodyDir = bodyDir;
            this.lstTouch = lstTouch;
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
        /// インデックスからパスを取得する
        /// </summary>
        /// <returns></returns>
        #region getPathFromIdx
        public string getPathFromIdx(int idx)
        {
            switch(idx)
            {
                case 1:
                    return this.body11;
                case 2:
                    return this.body12;
                case 3:
                    return this.body21;
                case 4:
                    return this.body22;
                case 5:
                    return this.body31;
                case 6:
                    return this.body32;
                default:
                    return "";
            }
        }
        #endregion

        /// <summary>
        /// インデックスからパスをセットする
        /// </summary>
        /// <returns></returns>
        #region setPathFromIdx
        public void setPathFromIdx(int idx, string newPath)
        {
            switch (idx)
            {
                case 1:
                    this.body11 = newPath;
                    break;
                case 2:
                    this.body12 = newPath;
                    break;
                case 3:
                    this.body21 = newPath;
                    break;
                case 4:
                    this.body22 = newPath;
                    break;
                case 5:
                    this.body31 = newPath;
                    break;
                case 6:
                    this.body32 = newPath;
                    break;
                default:
                    break;
            }
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
            return new ObjBodyGen(this.body11, this.body12, this.body21, this.body22, this.body31, this.body32, this.bodyDir, this.lstTouch);
        }
        #endregion

        /// <summary>
        /// タッチリストを取得する
        /// </summary>
        /// <returns></returns>
        #region getLstTouch
        public override List<string> getLstTouch()
        {
            return this.lstTouch;
        }
        #endregion

        /// <summary>
        /// タッチリストを取得する
        /// </summary>
        /// <returns></returns>
        #region getTouch
        public string getTouch()
        {
            string result = "";

            //コンマ区切りで設定
            foreach (string touch in lstTouch)
            {
                result = result + touch + ",";
            }

            //最後のコンマ除去
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result;
        }
        #endregion

    }
}
