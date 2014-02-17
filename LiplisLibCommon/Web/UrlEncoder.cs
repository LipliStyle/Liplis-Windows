//=======================================================================
//  ClassName : UrlEncoder
//  概要      : URLエンコードを行う
//
//  Tips      :一度バイト列に変換してからURLエンコードを行う
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System.Text;
using System.Web;

namespace Liplis.Web
{
    public static class UrlEncoder
    {
        ///=============================
        ///ディファイン
        private const string SJIS       = "shift_jis";
        private const string EUC_JP     = "euc-jp";
        private const string JIS        = "iso-2022-jp";
        private const string UTF_8      = "utf-8";
        private const string UTF_16     = "utf-16";
        private const string UTF_7      = "utf-8";
        private const string ANSI_Latin = "windows-1252";
        private const string LatinI     = "iso-8859-1";
        private const string Latin9     = "iso-8859-15";

        /// <summary>
        /// 指定文字コードでエンコードする
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <param name="encStr">指定文字コード</param>
        /// <returns></returns>
        public static string getEncode(string str,string encStr)
        {
            Encoding encoding = System.Text.Encoding.GetEncoding(encStr);
            return HttpUtility.UrlEncode(str, encoding);
        }

        /// <summary>
        /// 各種文字コードでURLエンコードする
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getUTF8(string str)    { return getEncode(str, UTF_8);}
        public static string getSJIS(string str)    { return getEncode(str, SJIS); }
        public static string getEUC_JP(string str)  { return getEncode(str, EUC_JP); }
        public static string getJIS(string str)     { return getEncode(str, JIS); }
        public static string getUTF_16(string str)  { return getEncode(str, UTF_16); }
        public static string getUTF_7(string str)   { return getEncode(str, UTF_7); }
        public static string getANSI_Latin(string str) { return getEncode(str, ANSI_Latin); }
        public static string getLatinI(string str)  { return getEncode(str, LatinI); }
        public static string getLatin9(string str)  { return getEncode(str, Latin9); }
    }
}
