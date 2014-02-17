//=======================================================================
//  ClassName : ComIme
//  概要      : ふりがな変換を行う
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace Liplis.Common
{
    public class LpsIme : IDisposable
    {
        ///=============================
        ///プロパティ
        private bool FInitialized = false;

        ///=============================
        ///定数
        #region 定数
        private const int S_OK = 0;
        private const int CLSCTX_LOCAL_SERVER = 4;
        private const int CLSCTX_INPROC_SERVER = 1;
        private const int CLSCTX_INPROC_HANDLER = 2;
        private const int CLSCTX_SERVER = CLSCTX_INPROC_SERVER | CLSCTX_LOCAL_SERVER;
        private const int FELANG_REQ_REV = 0x00030000;
        private const int FELANG_CMODE_PINYIN = 0x00000100;
        private const int FELANG_CMODE_NOINVISIBLECHAR = 0x40000000;
        #endregion

        ///=============================
        ///DLL Import
        #region DLL Import
        [DllImport("ole32.dll")]
        private static extern int CLSIDFromString([MarshalAs(UnmanagedType.LPWStr)] string lpsz, out Guid pclsid);

        [DllImport("ole32.dll")]
        private static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid,
            IntPtr pUnkOuter, uint dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr rpv);

        [DllImport("ole32.dll")]
        private static extern int CoInitialize(IntPtr pvReserved);

        [DllImport("ole32.dll")]
        private static extern int CoUninitialize();
        #endregion

        ///====================================================================
        ///
        ///                  　      　初期化処理
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクタ
        public LpsIme()
        {
            int res = CoInitialize(IntPtr.Zero);

            if (res == S_OK)
                FInitialized = true;
        }
        #endregion

        /// <summary>
        /// ディスポーズ
        /// </summary>
        #region Dispose
        public void Dispose()
        {
            if (FInitialized)
            {
                CoUninitialize();
                FInitialized = false;
            }
        }

        #endregion
        
        /// <summary>
        /// デストラクタ
        /// </summary>
        #region ~LpsIme
        ~LpsIme()
        {
            if (FInitialized)
                CoUninitialize();
        }
        #endregion

        ///====================================================================
        ///
        ///                  　      　処理メソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// ふりがなを取得する
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        #region GetYomi
        public string GetYomi(string str)
        {
            string yomi = String.Empty;
            IFELanguage language = null;
            Guid pclsid;
            int res;
            try
            {
                //入力文字列が空なら抜ける
                if (str == "")
                {
                    return "";
                }

                // 文字列の CLSID から CLSID へのポインタを取得する
                res = CLSIDFromString("MSIME.Japan", out pclsid);

                if (res != S_OK)
                {
                    this.Dispose();
                    return yomi;
                }

                Guid riid = new Guid("019F7152-E6DB-11D0-83C3-00C04FDDB82E");
                IntPtr ppv;
                res = CoCreateInstance(pclsid, IntPtr.Zero, CLSCTX_SERVER, riid, out ppv);

                if (res != S_OK)
                {
                    this.Dispose();
                    return Strings.StrConv(str, VbStrConv.Hiragana, 0);
                }

                language = Marshal.GetTypedObjectForIUnknown(ppv, typeof(IFELanguage)) as IFELanguage;
                res = language.Open();

                if (res != S_OK)
                {
                    this.Dispose();
                    return Strings.StrConv(str, VbStrConv.Hiragana, 0);
                }

                IntPtr result;

                res = language.GetJMorphResult(FELANG_REQ_REV, FELANG_CMODE_PINYIN | FELANG_CMODE_NOINVISIBLECHAR,
                        str.Length, str, IntPtr.Zero, out result);

                if (res != S_OK)
                {
                    this.Dispose();
                    return Strings.StrConv(str, VbStrConv.Hiragana, 0);
                }

                yomi = Marshal.PtrToStringUni(Marshal.ReadIntPtr(result, 4), Marshal.ReadInt16(result, 8));
            }
            catch
            {
                return "";
            }
            finally
            {
                if (language != null)
                {
                    language.Close();
                }

            }

            return yomi;
        }
        #endregion

        /// <summary>
        /// ローマ字を取得する
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        #region GetLatin
        public string GetLatin(string str)
        {
            string yomi = GetYomi(str);
            string latin = "";

            //1文字ずつローマ字に変換する
            foreach (char c in str)
            {
                latin = latin + convertLatin(c);
            }

            return latin;
        }
        #endregion

        /// <summary>
        /// ひらがなをローマ字に変換する
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region convertLatin
        public static string convertLatin(char c)
        {
            switch(c)
            {
                case 'あ': return "a";
                case 'い': return "i";
                case 'う': return "u";
                case 'え': return "e";
                case 'お': return "o";
                case 'か': return "ka";
                case 'き': return "ki";
                case 'く': return "ku";
                case 'け': return "ke";
                case 'こ': return "ko";
                case 'さ': return "sa";
                case 'し': return "si";
                case 'す': return "su";
                case 'せ': return "se";
                case 'そ': return "so";
                case 'た': return "ta";
                case 'ち': return "ti";
                case 'つ': return "tu";
                case 'て': return "te";
                case 'と': return "to";
                case 'な': return "na";
                case 'に': return "ni";
                case 'ぬ': return "nu";
                case 'ね': return "ne";
                case 'の': return "no";
                case 'は': return "ha";
                case 'ひ': return "hi";
                case 'ふ': return "hu";
                case 'へ': return "he";
                case 'ほ': return "ho";
                case 'ま': return "ma";
                case 'み': return "mi";
                case 'む': return "mu";
                case 'め': return "me";
                case 'も': return "mo";
                case 'や': return "ya";
                case 'ゆ': return "yu";
                case 'よ': return "yo";
                case 'ら': return "ra";
                case 'り': return "ri";
                case 'る': return "ru";
                case 'れ': return "re";
                case 'ろ': return "ro";
                case 'わ': return "wa";
                case 'を': return "wo";
                case 'ん': return "n";
                case 'ア': return "a";
                case 'イ': return "i";
                case 'ウ': return "u";
                case 'エ': return "e";
                case 'オ': return "o";
                case 'カ': return "ka";
                case 'キ': return "ki";
                case 'ク': return "ku";
                case 'ケ': return "ke";
                case 'コ': return "ko";
                case 'サ': return "sa";
                case 'シ': return "si";
                case 'ス': return "su";
                case 'セ': return "se";
                case 'ソ': return "so";
                case 'タ': return "ta";
                case 'チ': return "ti";
                case 'ツ': return "tu";
                case 'テ': return "te";
                case 'ト': return "to";
                case 'ナ': return "na";
                case 'ニ': return "ni";
                case 'ヌ': return "nu";
                case 'ネ': return "ne";
                case 'ノ': return "no";
                case 'ハ': return "ha";
                case 'ヒ': return "hi";
                case 'フ': return "hu";
                case 'ヘ': return "he";
                case 'ホ': return "ho";
                case 'マ': return "ma";
                case 'ミ': return "mi";
                case 'ム': return "mu";
                case 'メ': return "me";
                case 'モ': return "mo";
                case 'ヤ': return "ya";
                case 'ユ': return "yu";
                case 'ヨ': return "yo";
                case 'ラ': return "ra";
                case 'リ': return "ri";
                case 'ル': return "ru";
                case 'レ': return "re";
                case 'ロ': return "ro";
                case 'ワ': return "wa";
                case 'ヲ': return "wo";
                case 'ン': return "n";
                case '.' :return ".";
                case 'が': return "ga";
                case 'ぎ': return "gi";
                case 'ぐ': return "gu";
                case 'げ': return "ge";
                case 'ご': return "go";
                case 'ざ': return "za";
                case 'じ': return "zi";
                case 'ず': return "zu";
                case 'ぜ': return "ze";
                case 'ぞ': return "zo";
                case 'だ': return "da";
                case 'ぢ': return "di";
                case 'づ': return "du";
                case 'で': return "de";
                case 'ど': return "do";
                case 'ば': return "ba";
                case 'び': return "bi";
                case 'ぶ': return "bu";
                case 'べ': return "be";
                case 'ぼ': return "bo";
                case 'ぱ': return "pa";
                case 'ぴ': return "pi";
                case 'ぷ': return "pu";
                case 'ぺ': return "pe";
                case 'ぽ': return "po";
                case 'っ': return "";
                case 'ゃ': return "";
                case 'ゅ': return "";
                case 'ょ': return "";
                case 'ガ': return "ga";
                case 'ギ': return "gi";
                case 'グ': return "gu";
                case 'ゲ': return "ge";
                case 'ゴ': return "go";
                case 'ザ': return "za";
                case 'ジ': return "zi";
                case 'ズ': return "zu";
                case 'ゼ': return "ze";
                case 'ゾ': return "zo";
                case 'ダ': return "da";
                case 'ヂ': return "di";
                case 'ヅ': return "du";
                case 'デ': return "de";
                case 'ド': return "do";
                case 'バ': return "ba";
                case 'ビ': return "bi";
                case 'ブ': return "bu";
                case 'ベ': return "be";
                case 'ボ': return "bo";
                case 'パ': return "pa";
                case 'ピ': return "pi";
                case 'プ': return "pu";
                case 'ペ': return "pe";
                case 'ポ': return "po";
                case 'ッ': return "";
                case 'ャ': return "";
                case 'ュ': return "";
                case 'ョ': return "";
                case 'a': return "a";
                case 'b': return "b";
                case 'c': return "c";
                case 'd': return "d";
                case 'e': return "e";
                case 'f': return "f";
                case 'g': return "g";
                case 'h': return "h";
                case 'i': return "i";
                case 'j': return "j";
                case 'k': return "k";
                case 'l': return "l";
                case 'm': return "m";
                case 'n': return "n";
                case 'o': return "o";
                case 'p': return "p";
                case 'q': return "q";
                case 'r': return "r";
                case 's': return "s";
                case 't': return "t";
                case 'u': return "u";
                case 'v': return "v";
                case 'w': return "w";
                case 'x': return "x";
                case 'y': return "y";
                case 'z': return "z";
                case 'A': return "a";
                case 'B': return "b";
                case 'C': return "c";
                case 'D': return "d";
                case 'E': return "e";
                case 'F': return "f";
                case 'G': return "g";
                case 'H': return "h";
                case 'I': return "i";
                case 'J': return "j";
                case 'K': return "k";
                case 'L': return "l";
                case 'M': return "m";
                case 'N': return "n";
                case 'O': return "o";
                case 'P': return "p";
                case 'Q': return "q";
                case 'R': return "r";
                case 'S': return "s";
                case 'T': return "t";
                case 'U': return "u";
                case 'V': return "v";
                case 'W': return "w";
                case 'X': return "x";
                case 'Y': return "y";
                case 'Z': return "z";
                default: return "";
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                  　      　判定メソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// ひらがなかどうかチェックする
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsHiragana
        public static bool IsHiragana(char c)
        {
            //「ぁ」～「より」までと、「ー」「ダブルハイフン」をひらがなとする
            return ('\u3041' <= c && c <= '\u309F')
                || c == '\u30FC' || c == '\u30A0';
        }
        #endregion

        /// <summary>
        /// カタカナかどうかチェックする
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsFullwidthKatakana
        public static bool IsFullwidthKatakana(char c)
        {
            //「ダブルハイフン」から「コト」までと、カタカナフリガナ拡張と、
            //濁点と半濁点を全角カタカナとする
            //中点と長音記号も含む
            return ('\u30A0' <= c && c <= '\u30FF')
                || ('\u31F0' <= c && c <= '\u31FF')
                || ('\u3099' <= c && c <= '\u309C');
        }
        #endregion

        /// <summary>
        /// 半角カタカナかどうかチェックする
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsHalfwidthKatakana
        public static bool IsHalfwidthKatakana(char c)
        {
            //「･」から「ﾟ」までを半角カタカナとする
            return '\uFF65' <= c && c <= '\uFF9F';
        }

        #endregion

        /// <summary>
        /// 漢字チェック
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsKanji
        public static bool IsKanji(char c)
        {
            //CJK統合漢字、CJK互換漢字、CJK統合漢字拡張Aの範囲にあるか調べる
            return ('\u4E00' <= c && c <= '\u9FCF')
                || ('\uF900' <= c && c <= '\uFAFF')
                || ('\u3400' <= c && c <= '\u4DBF');
        }
        #endregion

        /// <summary>
        /// アルファベットの小文字かどうかチェックする(半角 + 全角)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsLowerLatin
        public static bool IsLowerLatin(char c)
        {
            //半角英字と全角英字の小文字の時はTrue
            return ('a' <= c && c <= 'z') || ('ａ' <= c && c <= 'ｚ');
        }
        #endregion

        /// <summary>
        /// アルファベットの小文字かどうかチェックする(半角)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsLowerHerlfLatin
        public static bool IsLowerHerlfLatin(char c)
        {
            //半角英字と全角英字の小文字の時はTrue
            return ('a' <= c && c <= 'z');
        }
        #endregion

        /// <summary>
        /// アルファベットの大文字かどうかチェックする(半角 + 全角)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsUpperLatin
        public static bool IsUpperLatin(char c)
        {
            //半角英字と全角英字の大文字の時はTrue
            return ('A' <= c && c <= 'Z') || ('Ａ' <= c && c <= 'Ｚ');
        }
        #endregion

        /// <summary>
        /// アルファベットの大文字かどうかチェックする(半角)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsUpperHerlfLatin
        public static bool IsUpperHerlfLatin(char c)
        {
            //半角英字と全角英字の大文字の時はTrue
            return ('A' <= c && c <= 'Z');
        }
        #endregion

        /// <summary>
        /// アルファベットの半角かどうかチェックする
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsUpperLatin
        public static bool IsHerlfLatin(char c)
        {
            //半角英字と全角英字の大文字の時はTrue
            return ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z');
        }
        #endregion

        /// <summary>
        /// 半角数字かどうかチェックする
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsAsciiDigit
        public static bool IsAsciiDigit(char c)
        {
            return '0' <= c && c <= '9';
        }
        #endregion

        /// <summary>
        /// 半角数字または半角アルファベットかどうかチェックする
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        #region IsHankakuKomojiSuji
        public static bool IsHankakuKomojiSuji(char c)
        {
            //半角英字と全角英字の大文字の時はTrue
            return ('a' <= c && c <= 'z') || ('0' <= c && c <= '9');
        }
        #endregion

    }
    //**************************************************************************************
    // IFELanguage Interface（メソッドの実装はランタイムの中にあるので、実装は不要）
    //**************************************************************************************
    [ComImport]
    [Guid("019F7152-E6DB-11D0-83C3-00C04FDDB82E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFELanguage
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int Open();
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int Close();
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int GetJMorphResult(uint dwRequest, uint dwCMode, int cwchInput,
            [MarshalAs(UnmanagedType.LPWStr)] string pwchInput, IntPtr pfCInfo, out IntPtr ppResult);
    } // end of IFELanguage Interface

}
