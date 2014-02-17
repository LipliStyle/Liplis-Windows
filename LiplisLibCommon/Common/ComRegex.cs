//=======================================================================
//  ClassName : ComRegex
//  概要      : 正規表現系機能
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Liplis.Common
{
    public static class ComRegex
    {
        /// <summary>
        /// removeHtmlTag
        /// </summary>
        /// <param name="src">タグ除去前文字列</param>
        /// <returns>タグ除去後文字列</returns>
        public static string removeHtmlTag(string src)
        {
            Regex re = new Regex("<.*?>", RegexOptions.Singleline);
            return re.Replace(src, "");
        }

        /// <summary>
        /// removeBigKakko
        /// </summary>
        /// <param name="src">除去前文字列</param>
        /// <returns>除去後文字列</returns>
        public static string removeBigKakko(string src)
        {
            Regex re = new Regex("【.*?】", RegexOptions.Singleline);
            return re.Replace(src, "");
        }

        /// <summary>
        /// removeKikkoKakko
        /// </summary>
        /// <param name="src">除去前文字列</param>
        /// <returns>除去後文字列</returns>
        public static string removeKikkoKakko(string src)
        {
            Regex re = new Regex("〔.*?〕", RegexOptions.Singleline);
            return re.Replace(src, "");
        }
    }
}
