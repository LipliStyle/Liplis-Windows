//=======================================================================
//  ClassName : BaseCompatilizedRandom
//  概要      : ランダムコンポーネント
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Common
{
    public class BaseCompatilizedRandom : System.Random
    {
        private BaseRandom original;

        /// <summary>
        /// randをソースとしてアダプタクラスを初期化します。
        /// </summary>
        public BaseCompatilizedRandom(BaseRandom rand)
        {
            original = rand;
        }

        /// <summary>
        /// [0,Int32.MaxValue)の擬似乱数を取得します。
        /// </summary>
        public override Int32 Next()
        {
            uint r = 0;
            do
                r = original.NextUInt32() & 0x7FFFFFFF;
            while (r == Int32.MaxValue);
            return (Int32)r;
        }

        /// <summary>
        /// [0,maxValue)の擬似乱数を取得します。
        /// 但しmaxValue=0の場合は0を返します。
        /// </summary>
        public override Int32 Next(Int32 maxValue)
        {
            return Next(0, maxValue);
        }

        /// <summary>
        /// [minValue,maxValue)の擬似乱数を取得します。
        /// ただし、minValue=maxValueのときはminValueを返します。
        /// </summary>
        public override Int32 Next(Int32 minValue, Int32 maxValue)
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException();
            if (minValue == maxValue) return minValue;
            UInt32 range = (UInt32)((Int64)maxValue - minValue);
            UInt32 residue = (UInt32.MaxValue - range + 1) % range;// (MaxValue+1) % range でだとオーバーフローするのでrangeを引いておく。
            UInt32 r;
            do
            {
                r = original.NextUInt32();
            } while (r < residue);
            return (Int32)((Int64)((r - residue) % range) + minValue);
        }

        /// <summary>
        /// [0,1)の擬似乱数を取得します。
        /// </summary>
        protected override double Sample()
        {
            return this.Next() * 4.6566128752457969E-10;
        }

        /// <summary>
        /// バイト配列に擬似乱数を格納します。
        /// </summary>
        public override void NextBytes(byte[] buffer)
        {
            original.NextBytes(buffer);
        }

    }

}