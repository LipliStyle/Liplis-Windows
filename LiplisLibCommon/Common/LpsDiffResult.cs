//=======================================================================
//  ClassName : DiffResult
//  概要      : 文章差分抽出
//
// 自由に使用させていただく。感謝！
// http://d.hatena.ne.jp/siokoshou/20070315
//
//NET 高速 diff classを公開します。２つの文字列のdiffを取ります。行単位のdiff(UNIXのdiffコマンドのような)と、文字単位のdiffを取れます。
//
//"An O(NP) Sequence Comparison Algorithm"(PDF), Sun Wu, Udi Manber, Gene Myers, (1989) のアルゴリズムを使用したC#によるdiffクラスです。非常に賢い処理を作り上げた著者らに感謝いたします。
//
//自由に使用してください。ただし、内容のいかなる保障もしません。バグを見つけたらコメントで教えてください。
//
//テストはしていますが、仕事に使うのであれば再テストをしてから使用してください。
//
//API
//
//    行単位diff
//        public static DiffResult[] Diff( string textA, string textB )
//        public static DiffResult[] Diff( string textA, string textB, DiffOption option )
//
//    文字単位diff
//        public static DiffResult[] DiffChar( string textA, string textB )
//        public static DiffResult[] DiffChar( string textA, string textB, DiffOption option )
//
//DiffOption は TrimSpace, IgnoreSpace, IgnoreCase があります。詳しくはソースコードを参照してください。
//
//結果は DiffResult 型の配列です。1つ目の文字列をオリジナル、2つ目の文字列を変更後の文字列とすると、両者の共通部分と異なる部分のレポートが結果として返ってきます。こちらも詳しくはソースコードを参照してください。
//
//(2009/8/24 追記 行単位 diff のバグを修正。id:SiroKuro さん、ご指摘ありがとうございます。
//
//  SatelliteServer
//  Copyright(c) 2009-2014 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Liplis.Common
{
    [Serializable]
    public struct DiffOption { public bool TrimSpace, IgnoreSpace, IgnoreCase; }

    [Serializable]
    public class DiffResult
    {
        public bool Modified; // 変更あり?
        public int OriginalStart;
        public int OriginalLength;
        public int ModifiedStart;
        public int ModifiedLength;

        public DiffResult(bool modified, int orgStart, int orgLength, int modStart, int modLength)
        {
            this.Modified = modified;
            this.OriginalStart = orgStart;
            this.OriginalLength = orgLength;
            this.ModifiedStart = modStart;
            this.ModifiedLength = modLength;
        }

        public override string ToString()
        {
            return ((this.Modified) ? "Modified" : "Common")
              + ", OrgStart:" + this.OriginalStart + ", OrgLen:" + this.OriginalLength
              + ", ModStart:" + this.ModifiedStart + ", ModLen:" + this.ModifiedLength;
        }
    }

    public class FastDiff
    {
        private int[] dataA, dataB;
        private string[] linesA, linesB;
        private bool isSwap;
        private Snake[] fp;

        private delegate bool IsSame(int posA, int posB);
        private IsSame isSame;


        private FastDiff() { }

        /// <summary>複数行の文字列を行単位で比較します</summary>
        /// <param name="textA">元テキスト</param>
        /// <param name="textB">変更テキスト</param>
        /// <returns>比較結果</returns>
        public static DiffResult[] Diff(string textA, string textB)
        {
            DiffOption option = new DiffOption();
            return Diff(textA, textB, option);
        }

        /// <summary>複数行の文字列を行単位で比較します</summary>
        /// <param name="textA">元テキスト</param>
        /// <param name="textB">変更テキスト</param>
        /// <param name="option">オプション指定</param>
        /// <returns>比較結果</returns>
        public static DiffResult[] Diff(string textA, string textB, DiffOption option)
        {
            if (string.IsNullOrEmpty(textA) || string.IsNullOrEmpty(textB))
                return StringNullOrEmpty(textA, textB);

            FastDiff diff = new FastDiff();
            return diff.DiffCore(textA, textB, option);
        }

        private DiffResult[] DiffCore(string textA, string textB, DiffOption option)
        {
            this.linesA = SplitLine(textA, option);
            this.linesB = SplitLine(textB, option);

            if (this.linesB.Length < this.linesA.Length)
            {
                this.isSwap = true;

                string[] tmps = this.linesA;
                this.linesA = this.linesB;
                this.linesB = tmps;
            }
            this.dataA = MakeHash(this.linesA);
            this.dataB = MakeHash(this.linesB);

            this.isSame = delegate(int posA, int posB)
            {
                return (this.dataA[posA] == this.dataB[posB]) && (this.linesA[posA] == this.linesB[posB]);
            };

            return DetectDiff();
        }

        /// <summary>単一行の各文字を比較します</summary>
        /// <param name="textA">元テキスト</param>
        /// <param name="textB">変更テキスト</param>
        /// <returns>比較結果</returns>
        public static DiffResult[] DiffChar(string textA, string textB)
        {
            DiffOption option = new DiffOption();
            return DiffChar(textA, textB, option);
        }

        /// <summary>単一行の各文字を比較します</summary>
        /// <param name="textA">元テキスト</param>
        /// <param name="textB">変更テキスト</param>
        /// <param name="option">オプション指定</param>
        /// <returns>比較結果</returns>
        public static DiffResult[] DiffChar(string textA, string textB, DiffOption option)
        {
            if (string.IsNullOrEmpty(textA) || string.IsNullOrEmpty(textB))
                return StringNullOrEmpty(textA, textB);

            FastDiff diff = new FastDiff();
            if (textA.Length <= textB.Length)
            {
                diff.SplitChar(textA, textB, option);
            }
            else
            {
                diff.isSwap = true;
                diff.SplitChar(textB, textA, option);
            }

            diff.isSame = delegate(int posA, int posB)
            {
                return diff.dataA[posA] == diff.dataB[posB];
            };

            return diff.DetectDiff();
        }


        private static DiffResult[] StringNullOrEmpty(string textA, string textB)
        {
            int lengthA = (string.IsNullOrEmpty(textA)) ? 0 : textA.Length;
            int lengthB = (string.IsNullOrEmpty(textB)) ? 0 : textB.Length;
            return PresentDiff(new CommonSubsequence(lengthA, lengthB, 0, null), true);
        }

        private void SplitChar(string textA, string textB, DiffOption option)
        {
            this.dataA = SplitChar(textA, option);
            this.dataB = SplitChar(textB, option);
        }

        private static int[] SplitChar(string text, DiffOption option)
        {
            if (option.IgnoreCase)
                text = text.ToUpperInvariant();

            // TODO: FIXME! Optimize this
            if (option.IgnoreSpace)
                text = Regex.Replace(text, @"\s+", " ");

            if (option.TrimSpace)
                text = text.Trim();

            int[] result = new int[text.Length];
            for (int i = 0; i < text.Length; i++)
                result[i] = text[i];
            return result;
        }

        private static string[] SplitLine(string text, DiffOption option)
        {
            if (option.IgnoreCase)
                text = text.ToUpperInvariant();

            string[] lines = text.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            //string[] lines = text.Split();

            // TODO: FIXME! Optimize this
            if (option.IgnoreSpace)
                for (int i = 0; i < lines.Length; ++i)
                    lines[i] = Regex.Replace(lines[i], @"\s+", " ");

            if (option.TrimSpace)
                for (int i = 0; i < lines.Length; ++i)
                    lines[i] = lines[i].Trim();

            return lines;
        }

        private static int[] MakeHash(string[] texts)
        {
            int[] hashs = new int[texts.Length];

            for (int i = 0; i < texts.Length; ++i)
                hashs[i] = texts[i].GetHashCode();

            return hashs;
        }

        private DiffResult[] DetectDiff()
        {
            Debug.Assert(this.dataA.Length <= this.dataB.Length);

            this.fp = new Snake[this.dataA.Length + this.dataB.Length + 3];
            int d = this.dataB.Length - this.dataA.Length;
            int p = 0;
            do
            {
                //Debug.Unindent();
                //Debug.WriteLine( "p:" + p );
                //Debug.Indent();

                for (int k = -p; k < d; k++)
                    SearchSnake(k);

                for (int k = d + p; k >= d; k--)
                    SearchSnake(k);

                p++;
            }
            while (this.fp[this.dataB.Length + 1].posB != (this.dataB.Length + 1));

            // 末尾検出用のCommonSubsequence
            CommonSubsequence endCS = new CommonSubsequence(this.dataA.Length, this.dataB.Length, 0, this.fp[this.dataB.Length + 1].CS);
            CommonSubsequence result = CommonSubsequence.Reverse(endCS);

            if (this.isSwap)
                return PresentDiffSwap(result, true);
            else
                return PresentDiff(result, true);
        }

        private void SearchSnake(int k)
        {
            int kk = this.dataA.Length + 1 + k;
            CommonSubsequence previousCS = null;
            int posA = 0, posB = 0;

            int lk = kk - 1;
            int rk = kk + 1;

            // 論文のfp[n]は-1始まりだが、0始まりのほうが初期化の都合がよいため、
            // +1のゲタを履かせる。fpから読む際は-1し、書く際は+1する。
            int lb = this.fp[lk].posB;
            int rb = this.fp[rk].posB - 1;

            //Debug.Write( "fp[" + string.Format( "{0,2}", k ) + "]=Snake( " + string.Format( "{0,2}", k )
            //    + ", max( fp[" + string.Format( "{0,2}", ( k - 1 ) ) + "]+1= " + string.Format( "{0,2}", lb )
            //    + ", fp[" + string.Format( "{0,2}", ( k + 1 ) ) + "]= " + string.Format( "{0,2}", rb ) + " ))," );

            if (lb > rb)
            {
                posB = lb;
                previousCS = this.fp[lk].CS;
            }
            else
            {
                posB = rb;
                previousCS = this.fp[rk].CS;
            }
            posA = posB - k;

            int startA = posA;
            int startB = posB;

            //Debug.Write( "(x: " + string.Format( "{0,2}", startA ) + ", y: " + string.Format( "{0,2}", startB ) + " )" );

            while ((posA < this.dataA.Length)
              && (posB < this.dataB.Length)
              && this.isSame(posA, posB))
            {
                posA++;
                posB++;
            }

            if (startA != posA)
            {
                this.fp[kk].CS = new CommonSubsequence(startA, startB, posA - startA, previousCS);
            }
            else
            {
                this.fp[kk].CS = previousCS;
            }
            this.fp[kk].posB = posB + 1; // fpへ+1して書く。論文のfpに+1のゲタを履かせる。

            //Debug.WriteLine( "= " + string.Format( "{0,2}", posB ) );
        }

        /// <summary>結果出力</summary>
        private static DiffResult[] PresentDiff(CommonSubsequence cs, bool wantCommon)
        {
            List<DiffResult> list = new List<DiffResult>();
            int originalStart = 0, modifiedStart = 0;

            while (true)
            {
                if (originalStart < cs.StartA
                  || modifiedStart < cs.StartB)
                {
                    DiffResult d = new DiffResult(
                      true,
                      originalStart, cs.StartA - originalStart,
                      modifiedStart, cs.StartB - modifiedStart);
                    list.Add(d);
                }

                // 末尾検出
                if (cs.Length == 0) break;

                originalStart = cs.StartA;
                modifiedStart = cs.StartB;

                if (wantCommon)
                {
                    DiffResult d = new DiffResult(
                      false,
                      originalStart, cs.Length,
                      modifiedStart, cs.Length);
                    list.Add(d);
                }
                originalStart += cs.Length;
                modifiedStart += cs.Length;

                cs = cs.Next;
            }
            return list.ToArray();
        }

        /// <summary>結果出力</summary>
        private static DiffResult[] PresentDiffSwap(CommonSubsequence cs, bool wantCommon)
        {
            List<DiffResult> list = new List<DiffResult>();
            int originalStart = 0, modifiedStart = 0;

            while (true)
            {
                if (originalStart < cs.StartB
                  || modifiedStart < cs.StartA)
                {
                    DiffResult d = new DiffResult(
                      true,
                      originalStart, cs.StartB - originalStart,
                      modifiedStart, cs.StartA - modifiedStart);
                    list.Add(d);
                }

                // 末尾検出
                if (cs.Length == 0) break;

                originalStart = cs.StartB;
                modifiedStart = cs.StartA;

                if (wantCommon)
                {
                    DiffResult d = new DiffResult(
                      false,
                      originalStart, cs.Length,
                      modifiedStart, cs.Length);
                    list.Add(d);
                }
                originalStart += cs.Length;
                modifiedStart += cs.Length;

                cs = cs.Next;
            }
            return list.ToArray();
        }

        private struct Snake
        {
            public int posB;
            public CommonSubsequence CS;

            public override string ToString()
            {
                return "posB:" + this.posB + ", CS:" + ((this.CS == null) ? "null" : "exist");
            }
        }

        private class CommonSubsequence
        {
            private int startA_, startB_;
            private int length_;
            public CommonSubsequence Next;

            public int StartA { get { return this.startA_; } }
            public int StartB { get { return this.startB_; } }
            public int Length { get { return this.length_; } }

            public CommonSubsequence() { }

            public CommonSubsequence(int startA, int startB, int length, CommonSubsequence next)
            {
                this.startA_ = startA;
                this.startB_ = startB;
                this.length_ = length;
                this.Next = next;
            }

            /// <summary>リンクリスト反転</summary>
            public static CommonSubsequence Reverse(CommonSubsequence old)
            {
                CommonSubsequence newTop = null;
                while (old != null)
                {
                    CommonSubsequence next = old.Next;
                    old.Next = newTop;
                    newTop = old;
                    old = next;
                }
                return newTop;
            }

            public override string ToString()
            {
                return "Length:" + this.Length + ", A:" + this.StartA.ToString()
                  + ", B:" + this.StartB.ToString() + ", Next:" + ((this.Next == null) ? "null" : "exist");
            }
        }

    }
}
