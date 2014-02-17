//=======================================================================
//  ClassName : LpsJavaCode
//  概要      : Javaコードを扱うユーティリティー
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Common
{
    public static class LpsJavaCode
    {
        /// <summary>
        /// パッケージ名として正しいかチェックする
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        #region checkPackageName
        public static bool checkPackageName(string packageName)
        {
            try
            {
                //空じゃないか確認
                if (packageName.Length < 1)
                {
                    return false;
                }

                //数値で始まっていたら✕
                if (LpsIme.IsAsciiDigit(packageName[0]))
                {
                    return false;
                }

                //回してチェックする
                foreach (char c in packageName)
                {
                    if (!LpsIme.IsHankakuKomojiSuji(c))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// パッケージ名を作成する
        /// 入力される文字列はローマ字前提とする
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        #region createPackageNameFromJp
        public static string createPackageNameFromJp(string name)
        {
            //数値で始まっていたら、アンダーバーを入れる

            //

            return "";
        }
        #endregion

        /// <summary>
        /// パッケージ名を作成する
        /// 入力される文字列はローマ字前提とする
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        #region createPackageName
        public static string createPackageName(string name)
        {
            try
            {
                //空じゃないか確認
                if (name.Length < 1)
                {
                    return createPackageNameRandom();
                }

                //数値で始まっていたら✕
                if (LpsIme.IsAsciiDigit(name[0]))
                {
                    return "_" + name;
                }

                return name;
            }
            catch
            {
                return createPackageNameRandom();
            }
        }
        #endregion

        /// <summary>
        /// パッケージ名を作成する
        /// 入力される文字列はローマ字前提とする
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        #region createPackageName
        public static string createPackageNameRandom()
        {
            return "com.rafine." + LpsLiplisUtil.getName(10);
        }
        #endregion
    }
}
