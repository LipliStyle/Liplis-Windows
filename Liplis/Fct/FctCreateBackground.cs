//=======================================================================
//  ClassName : FctCreateBackground
//  概要      : ウインドウのバックグラウンドイメージを作成する。
//
//  Liplisシステム      
//  Copyright(c) 2010-2011 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liplis.Common;
using Liplis.Msg;

namespace Liplis.Fct
{
    class FctCreateBackground : IDisposable
    {
        ///==========================
        /// オブジェクト
        #region オブジェクト
        private ObjWindowFile ow;
        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region FctCreateBackground
        public FctCreateBackground(ObjWindowFile ow)
        {
            this.ow = ow;
        }
        #endregion

        /// <summary>
        /// 指定の一つのブロックを取得する
        /// </summary>
        #region getMonoBlock
        public Bitmap getMonoBlock(string block)
        {
            switch (block)
            {
                //case LiplisDefine.BLOCK_LT:
                //    return ow.lt;
                //case LiplisDefine.BLOCK_MT:
                //    return ow.mt;
                //case LiplisDefine.BLOCK_RT:
                //    return ow.rt;
                //case LiplisDefine.BLOCK_LB:
                //    return ow.lb;
                //case LiplisDefine.BLOCK_MB:
                //    return ow.mb;
                //case LiplisDefine.BLOCK_RB:
                //    return ow.rb;
                //case LiplisDefine.BLOCK_BD:
                //    return ow.bd;
                //case LiplisDefine.BLOCK_LM:
                //    return ow.lm;
                //case LiplisDefine.BLOCK_RM:
                //    return ow.rm;
                default:
                    return null;
            }
        }
        #endregion

        /// <summary>
        /// 背景画像を作成する
        /// </summary>
        /// <returns></returns>
        #region createBgi
        public Bitmap createBgi(int wid, int hi)
        {
            return createBgi(culcBolckNum(wid), culcBolckNum(hi), new Bitmap(culcBolckNum(wid) * LiplisDefine.BLOCK_SIZE-1, culcBolckNum(hi) * LiplisDefine.BLOCK_SIZE-1));
        }
        public Bitmap createBgi(int wid, int hi, string bgiPath)
        {
            return createBgi(culcBolckNum(wid), culcBolckNum(hi), new Bitmap(bgiPath));
        }
        public Bitmap createBgi(int widBlock, int hiBlock, Bitmap bmp)
        {
            try
            {
                Graphics gra = Graphics.FromImage(bmp);

                ////-----   1ライン目の描画   -----
                ////gra.DrawImageUnscaled(lt, 0, 0);
                //gra.DrawImage(ow.lt, 0, 0);

                //for (int i = 1; i < widBlock - 1; i++)
                //{
                //    //gra.DrawImageUnscaled(mt, i * ComDefine.BLOCK_SIZE, 0);
                //    gra.DrawImage(ow.mt, i * LiplisDefine.BLOCK_SIZE, 0);
                //}

                ////gra.DrawImageUnscaled(rt, (widBlock-1) * ComDefine.BLOCK_SIZE, 0);
                //gra.DrawImage(ow.rt, (widBlock - 1) * LiplisDefine.BLOCK_SIZE, 0);
                ////-------------------------------

                ////-----   2ライン目～の描画   -----
                //for (int i = 1; i < hiBlock - 1; i++)
                //{
                //    gra.DrawImageUnscaled(ow.lm, 0, i * LiplisDefine.BLOCK_SIZE);
                //    for (int j = 1; j < widBlock - 1; j++)
                //    {
                //        gra.DrawImageUnscaled(ow.bd, j * LiplisDefine.BLOCK_SIZE, i * LiplisDefine.BLOCK_SIZE);
                //    }
                //    gra.DrawImageUnscaled(ow.rm, (widBlock - 1) * LiplisDefine.BLOCK_SIZE, i * LiplisDefine.BLOCK_SIZE);
                //}
                ////-------------------------------

                ////-----   最終ラインの描画   -----
                //gra.DrawImageUnscaled(ow.lb, 0, (hiBlock - 1) * LiplisDefine.BLOCK_SIZE);

                //for (int i = 1; i < widBlock - 1; i++)
                //{
                //    gra.DrawImageUnscaled(ow.mb, i * LiplisDefine.BLOCK_SIZE, (hiBlock - 1) * LiplisDefine.BLOCK_SIZE);
                //}

                //gra.DrawImageUnscaled(ow.rb, (widBlock - 1) * LiplisDefine.BLOCK_SIZE, (hiBlock - 1) * LiplisDefine.BLOCK_SIZE);
                ////-------------------------------

                return bmp;
            }
            catch
            {
                return new Bitmap(widBlock, hiBlock);
            }
        }
        #endregion

        /// <summary>
        /// 透明のグラフィックを生成する
        /// </summary>
        #region createTransParentBgi
        public static Bitmap createTransParentBgi(int wid, int hei)
        {
            try
            {
                Bitmap bmp = new Bitmap(wid, hei);
                Graphics gra = Graphics.FromImage(bmp);

                gra.DrawRectangle(new Pen(Color.Transparent), 0, 0, wid, hei);

                return bmp;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// wid、hiからブロック数を算出する
        /// </summary>
        /// <returns></returns>
        /// <param name="val">幅</param>
        /// <returns>ブロックサイズ</returns>
        #region culcBolckNum
        private static int culcBolckNum(int val)
        {
            try
            {
                int result = 0;

                //0チェック
                if (val == 0)
                {
                    return 0;
                }

                //ブロックサイズ算出
                result = (val / LiplisDefine.BLOCK_SIZE);

                //割り切れない場合は繰り上げる
                if (val % LiplisDefine.BLOCK_SIZE != 0)
                {
                    result = result + 1;
                }
                return result;
            }
            catch
            {
                return LiplisDefine.MIN_BLOCK_NUM;
            }
        }
        #endregion

        /// <summary>
        /// ディスポーズ
        /// </summary>
        #region Dispose
        public void Dispose()
        {
            ow.Dispose();
        }
        #endregion
    }
}
