using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Common
{
    public abstract class BaseRandom
    {
        /// <summary>
        /// 派生クラスで符号なし32bitの擬似乱数を生成する必要があります。
        /// </summary>
        public abstract UInt32 NextUInt32();

        /// <summary>
        /// 符号あり32bitの擬似乱数を取得します。
        /// </summary>
        public virtual Int32 NextInt32()
        {
            return (Int32)NextUInt32();
        }

        /// <summary>
        /// 符号なし64bitの擬似乱数を取得します。
        /// </summary>
        public virtual UInt64 NextUInt64()
        {
            return ((UInt64)NextUInt32() << 32) | NextUInt32();
        }

        /// <summary>
        /// 符号あり64bitの擬似乱数を取得します。
        /// </summary>
        public virtual Int64 NextInt64()
        {
            return ((Int64)NextUInt32() << 32) | NextUInt32();
        }

        /// <summary>
        /// 擬似乱数列を生成し、バイト配列に順に格納します。
        /// </summary>
        public virtual void NextBytes(byte[] buffer)
        {
            int i = 0;
            UInt32 r;
            while (i + 4 <= buffer.Length)
            {
                r = NextUInt32();
                buffer[i++] = (byte)r;
                buffer[i++] = (byte)(r >> 8);
                buffer[i++] = (byte)(r >> 16);
                buffer[i++] = (byte)(r >> 24);
            }
            if (i >= buffer.Length) return;
            r = NextUInt32();
            buffer[i++] = (byte)r;
            if (i >= buffer.Length) return;
            buffer[i++] = (byte)(r >> 8);
            if (i >= buffer.Length) return;
            buffer[i++] = (byte)(r >> 16);
        }

        /// <summary>
        /// [0,1)の擬似乱数を取得します。
        /// [0,1)を2^53個に均等にわけ、そのうち一つを返します。
        /// NextUInt32を2回呼び出します。
        /// </summary>
        public virtual double NextDouble()
        {
            UInt32 r1, r2;
            r1 = NextUInt32();
            r2 = NextUInt32();
            return (r1 * (double)(2 << 11) + r2) / (double)(2 << 53);
        }
    }
}
