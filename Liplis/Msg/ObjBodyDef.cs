//=======================================================================
//  ClassName : ObjBody
//  概要      : ボディオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System.Drawing;
using Liplis.Fct;

namespace Liplis.Msg
{
    public class ObjBodyDef : ObjBody
    {
        ///=============================
        ///プロパティ
        public string body11 { get; set; }
        public string body12 { get; set; }
        public string body21 { get; set; }
        public string body22 { get; set; }
        public string body31 { get; set; }
        public string body32 { get; set; }

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
        public ObjBodyDef(string emotion, string idx)
        {
            this.body11 = emotion + "_" + idx + "_1_1";
            this.body12 = emotion + "_" + idx + "_1_2";
            this.body21 = emotion + "_" + idx + "_2_1";
            this.body22 = emotion + "_" + idx + "_2_2";
            this.body31 = emotion + "_" + idx + "_3_1";
            this.body32 = emotion + "_" + idx + "_3_2";
        }
        #endregion

        /// <summary>
        /// Bodyのゲッター
        /// </summary>
        /// <returns></returns>
        #region getBody
        public override Bitmap getBody11()
        {
            return FctCreateFromResource.getResourceBitmap(body11);
        }
        public override Bitmap getBody12()
        {
            return FctCreateFromResource.getResourceBitmap(body12);
        }
        public override Bitmap getBody21()
        {
            return FctCreateFromResource.getResourceBitmap(body21);
        }
        public override Bitmap getBody22()
        {
            return FctCreateFromResource.getResourceBitmap(body22);
        }
        public override Bitmap getBody31()
        {
            return FctCreateFromResource.getResourceBitmap(body31);
        }
        public override Bitmap getBody32()
        {
            return FctCreateFromResource.getResourceBitmap(body32);
        }
        #endregion


    }
}
