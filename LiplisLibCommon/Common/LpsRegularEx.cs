//=======================================================================
//  ClassName : LpsRegularEx
//  概要      : 正規表現クラス
//
//  Liplisシステム      
//  Copyright(c) 2009-2012 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Liplis.Common
{
    public static class LpsRegularEx
    {
        ///====================================================================
        ///
        ///                          マスターメソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// regularExList
        /// 正規表現で結果を抽出する
        /// </summary>
        /// <returns></returns>
        #region regularExList
        public static List<string> regularExList(string regDefine, string target)
        {
            List<string> res = new List<string>();

            foreach (Match mm in new Regex(regDefine, RegexOptions.IgnoreCase).Matches(target))
            {
                res.Add(mm.Value);
            }

            return res;
        }
        #endregion

        /// <summary>
        /// regularMatch
        /// 正規表現でチェックする
        /// </summary>
        /// <param name="regDefine"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        #region regularMatch
        public static bool regularMatch(string regDefine, string target)
        {
            Regex regex = new Regex(regDefine);
            if (!regex.IsMatch(target))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                          抽出、除去
        ///                         
        ///====================================================================

        /// <summary>
        /// getUrlList
        /// 文章からURLを抽出する
        /// </summary>
        /// <returns></returns>
        #region getUrlList
        public static List<string> getUrlList(string target)
        {
            string regDefine = "";
            return regularExList(regDefine,target);
        }
        #endregion

        /// <summary>
        /// getNicoId
        /// ニコIDを抽出する
        /// </summary>
        /// <returns></returns>
        #region getNicoId
        public static string getNicoId(string target)
        {
            List<string> l = regularExList("sm[0-9]{1,10}", target);
            if(l.Count > 0)
            {
                return l[0];
            }
            else
            {
                return "";
            }
        }
        #endregion

        /// <summary>
        /// getNicoVideoUrl
        /// ニコIDを抽出する
        /// </summary>
        /// <returns></returns>
        #region getNicoId
        public static List<string> getNicoVideoUrl(string target)
        {
            return regularExList("watch/sm[0-9]{1,10}", target);
        }
        #endregion

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

        /// <summary>
        /// removeRubi
        /// 文章からルビを削除する
        /// </summary>
        /// <returns></returns>
        #region removeRubi
        public static string removeRubi(string target)
        {
            return  new Regex(@"\《.+?\》").Replace(target, "");
        }
        #endregion

        /// <summary>
        /// getInnerKakko
        /// カッコ内の文字を取得する
        /// </summary>
        /// <returns></returns>
        #region getInnerKakko
        public static string getInnerKakko(string target)
        {
            string res = "";

            List<string> resList = regularExList(@"\(.+?\)", target);

            if (resList.Count > 0)
            {
                res = resList[0];
            }
            else
            {
                res = target;
            }

            return res;
        }
        #endregion

        ///====================================================================
        ///
        ///                          Liplis用
        ///                         
        ///====================================================================

        /// <summary>
        /// replaceHtmlLink
        /// HTMLにタグを付与する
        /// Liplisのおしゃべり用
        /// </summary>
        /// <returns></returns>
        #region fctReplaceHtmlLink
        public static string fctReplaceHtmlLink(string target)
        {
            //return new Regex(@"s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+").Replace(target, "《" + LpsDefineMost.LPS_TAG_LINK + "》\"$&\">$&《/" + LpsDefineMost.LPS_TAG_LINK + "》");
            return new Regex(@"s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+").Replace(target, "<a href=\"$&\" target=\"_blank\">$&</a>");
        }
        #endregion

        /// <summary>
        /// replaceKoyuMeishi
        /// 対象の固有名詞にタグを付与する
        /// Liplisのおしゃべり用
        /// </summary>
        /// <returns></returns>
        #region fctReplaceKoyuMeishi
        public static string fctReplaceKoyuMeishi(string target, string koyumeishi)
        {
            //return new Regex(koyumeishi).Replace(target, "《" + LpsDefineMost.LPS_TAG_KOYUMEISHI + "》$&《/" + LpsDefineMost.LPS_TAG_KOYUMEISHI + "》");
            return new Regex(koyumeishi).Replace(target, "<a href=\"$&\" target=\"_blank\">$&</a>");
        }
        #endregion

        /// <summary>
        /// fctReplaceNicoVideoId
        /// ニコニコヴィデオID正規表現チェック
        /// </summary>
        #region fctReplaceNicoVideoId
        public static string fctReplaceNicoVideoId(string target)
        {
            if (target.IndexOf("sm") > 0)
            {
                Console.Write("");
            }

            return new Regex("sm[0-9]{1,10}").Replace(target, "<a href=\"" + LpsDefineMost.URL_NICO_VIDEO + "$&\" target=\"_blank\">$&</a>");
        }
        #endregion

        /// <summary>
        /// fctReplaceNicoMyListId
        /// ニコニコマイリストID正規表現チェック
        /// 
        /// </summary>
        #region fctReplaceNicoMyListId
        public static string fctReplaceNicoMyListId(string target)
        {
            return new Regex("mylist[0-9]{1,10}").Replace(target, "<a href=\"" + LpsDefineMost.URL_NICO_DOMAIN + "$&\" target=\"_blank\">$&</a>");
        }
        #endregion


        /// <summary>
        /// fctReplaceNewLine
        /// 新規行
        /// 
        /// </summary>
        #region fctReplaceNewLine
        public static string fctReplaceNewLine(string target)
        {
            string result = target;

            result = new Regex("。").Replace(result, "。<br><br>");
            result = new Regex("」").Replace(result, "」<br><br>");
            result = new Regex("「").Replace(result, "<br><br>「");
            result = new Regex("■").Replace(result, "<br><br>■");

            return new Regex("@").Replace(result, "<br><br>");
        }
        #endregion

        /// <summary>
        /// fctRemoveHtmlLink
        /// </summary>
        /// <param name="src">タグ除去前文字列</param>
        /// <returns>タグ除去後文字列</returns>
        public static string fctRemoveHtmlLink(string src)
        {
            Regex re = new Regex("《.*?》", RegexOptions.Singleline);
            return re.Replace(src, "");
        }

        /// <summary>
        /// fctBikkuriTogo
        /// びっくりとハテナを統合する
        /// Liplisのおしゃべり用
        /// </summary>
        /// <returns></returns>
        #region fctBikkuriTogo
        public static string fctBikkuriTogo(string target)
        {
            string res = "";

            res = new Regex(@"(!)\1+").Replace(target, "!");
            res = new Regex(@"(！)\1+").Replace(target, "!");
            //res = new Regex(@"(?)\1+").Replace(target, "?");
            res = new Regex(@"(？)\1+").Replace(target, "?");


            return res;
        }
        #endregion



        ///====================================================================
        ///
        ///                          入力チェック
        ///                         
        ///====================================================================

        /// <summary>
        /// 正規表現数値チェック
        /// </summary>
        #region checkNumeric
        public static bool checkNumeric(string target)
        {
            return regularMatch("^[0-9]+$", target);
        }
        #endregion

        /// <summary>
        /// 正規表現全角カタカナチェック
        /// </summary>
        #region checkZenKatakana
        public static bool checkZenKatakana(string target)
        {
            return regularMatch("^[ァ-ヴ!ー]+$", target);
        }
        #endregion

        /// <summary>
        /// 正規表現半角カタカナチェック
        /// </summary>
        #region checkHanKatakana
        public static bool checkHanKatakana(string target)
        {
            return regularMatch("^[ｱ-ﾞ]+$", target);
        }
        #endregion

        /// <summary>
        /// 正規表現全角ひらがなチェック
        /// </summary>
        #region checkZenHiragana
        public static bool checkZenHiragana(string target)
        {
            return regularMatch("^[ぁ-ん!ー]+$", target);
        }
        #endregion

        /// <summary>
        /// 正規表現郵便番号チェック
        /// </summary>
        #region checkPhoneNo
        public static bool checkPhoneNo(string target)
        {
            return regularMatch("^[0-9]{3}[-][0-9]{4}$", target);
        }
        #endregion

        /// <summary>
        /// 正規表現電話番号チェック
        /// </summary>
        #region checkPostal
        public static bool checkPostal(string target)
        {
            return regularMatch("^[0-9]{2,5}-[0-9]{1,4}-[0-9]{4}$", target);
        }
        #endregion

        /// <summary>
        /// 正規表現URLチェック
        /// </summary>
        #region checkUrl
        public static bool checkUrl(string target)
        {
            return regularMatch(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", target);
        }
        #endregion

        /// <summary>
        /// fctReplaceNicoVideoId
        /// ニコニコヴィデオID正規表現チェック
        /// </summary>
        #region chekcNicoVideoId
        public static bool chekcNicoVideoId(string target)
        {
            return regularMatch("sm[0-9]{1,10}", target);
        }
        #endregion
    }
}
