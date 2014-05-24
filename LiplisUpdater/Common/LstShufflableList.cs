//=======================================================================
//  ClassName : LstShufflableList
//  概要      : シャッフル可能リスト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================

using System;
using System.Collections.Generic;

namespace Liplis.Common
{
    public class LstShufflableList<T> : List<T>
    {


        /// <summary>
        /// 空で、既定の初期量を備えた、ShufflableList クラスの新しいインスタンスを初期化します。
        /// </summary>
        public LstShufflableList()
            : base()
        {
        }


        /// <summary>
        /// ShufflableList クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="capacity"> 新しいリストに格納できる要素の数。 </param>
        public LstShufflableList(int capacity)
            : base(capacity)
        {
        }


        /// <summary>
        /// ShufflableList クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="collection"> 新しいリストに要素がコピーされたコレクション。 </param>
        public LstShufflableList(IEnumerable<T> collection)
            : base(collection)
        {
        }


        /// <summary>
        /// リストの要素をシャッフルします。
        /// </summary>
        public void Shuffle()
        {
            LstShufflableList<T>.Shuffle(this);
        }


        /// <summary>
        /// 指定した IList の要素をシャッフルします。
        /// </summary>
        /// <param name="list"> シャッフルするIList。 </param>
        public static void Shuffle(IList<T> list)
        {
            BaseCompatilizedRandom r = new BaseCompatilizedRandom(new RndMersenneTwister());

            for (int i = 0; i < list.Count; i++)
            {
                int value = r.Next(list.Count);
                T temp = list[i];
                list[i] = list[value];
                list[value] = temp;
            }

        }


    }  // class

}  // namespace