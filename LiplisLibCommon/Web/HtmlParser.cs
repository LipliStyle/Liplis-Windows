//=======================================================================
//  ClassName : HtmlParser
//  概要      : HTTPパーサ(NTidyのラッパ)
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================

using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Liplis.Common;
using System;
using System.IO;

namespace Liplis.Web
{
    public class HtmlParser
    {
        ///============================
        ///クラス
        protected LpsLogController lc;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public HtmlParser()
        {
            lc = new LpsLogController();
        }

        // ノードを再帰的にチェックする
        //private  void traceTag(TidyNode node, ref string sResult)
        //{
        //    if (node.IsText)
        //    {
        //        sResult += node.Value;
        //    }

        //    foreach (TidyNode n in node.ChildNodes)
        //    {
        //        System.Windows.Forms.Application.DoEvents();
        //        if (!n.IsScript)
        //        {
        //            traceTag(n, ref sResult);
        //        }
        //    }
        //}

        /// <summary>
        /// httpソースに含まれる本文だけ取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHtmlSorceTextData
        public  string getHtmlSorceTextData(string url)
        {
            WebClient wc = new WebClient();
            byte[] bs;
            try
            {
                string result = "";

                //まずソースをバイトデータでダウンロードする
                bs = wc.DownloadData(url);

                //バイトデータから文字コードを判断、取得
                //wc.Encoding = GetCode(bs);
                //設定した文字コードでHTMLソースをテキストでダウンロードする
                //result = htmlTagRegularRemove(wc.DownloadString(url));

                //2011/03/06 ダウンロード部を下記ソースに変更
                Encoding e = HtmlParser.GetCode(bs);
                result = e.GetString(bs);

                result = htmlTagRegularRemove(result);
                result = htmlCommentRegularRemove(result);
                result = htmlGomiRegularRemove(result);
                result = htmlMultiNewLineRemove(result);
                //result = result.Replace("\r\n", "");
                result = result.Replace("\t", "");
                result = result.Replace("&gt;", "");
                result = result.Replace("&nbsp;", "");
                result = result.Replace("&amp;", "");

                return result;
            }
            catch
            {
                return "";
            }
            finally
            {
                bs = null;
                wc.Dispose();
                wc = null;
            }
        }
        #endregion

        /// <summary>
        /// httpソースに含まれる本文だけ取得する
        /// 一般系から逸脱する可能性があるため、スペシャルメソッドとする
        /// 2011/03/21 LiplisLRAnalysisで使用していたが廃止
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHtmlSorceTextData2
        //public string getHtmlSorceTextData2(string url)
        //{
        //    WebClient wc = new WebClient();
        //    byte[] bs;
        //    try
        //    {
        //        string result = "";

        //        //まずソースをバイトデータでダウンロードする
        //        bs = wc.DownloadData(url);
        //        Encoding e = HtmlParser.GetCode(bs);
        //        result = e.GetString(bs);

        //        //正規表現で無駄なものを除去
        //        result = htmlTagRegularRemove(result);
        //        result = htmlCommentRegularRemove(result);
        //        result = htmlGomiRegularRemove(result);
        //        result = htmlMultiNewLineRemove(result);
        //        result = htmYMDRemove(result);
        //        result = result.Replace("\t", "");
        //        result = result.Replace("&gt;", "");
        //        result = result.Replace("&nbsp;", "");
        //        result = result.Replace("&amp;", "");

        //        return result;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //    finally
        //    {
        //        bs = null;
        //        wc.Dispose();
        //        wc = null;
        //    }
        //}
        #endregion

        /// <summary>
        /// httpソースに含まれる本文だけ取得する
        /// 一般系から逸脱する可能性があるため、スペシャルメソッドとする
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHtmlPlainTextFromSource
        public string getHtmlPlainTextFromSource(string source)
        {
            try
            {
                string result = source;

                //正規表現で無駄なものを除去
                result = brRegularReplace(result);
                result = htmlTagRegularRemove(result);
                result = htmlCommentRegularRemove(result);
                result = htmlGomiRegularRemove(result);
                result = htmlMultiNewLineRemove(result);
                result = htmYMDRemove(result);
                result = result.Replace("＃", Environment.NewLine);
                result = result.Replace("\t", "");
                result = result.Replace("&gt;", "");
                result = result.Replace("&nbsp;", "");
                result = result.Replace("&amp;", "");
                result = result.Replace("<", "");
                result = result.Replace(">", "");
                
                return result;
            }
            catch
            {
                return source;
            }
            finally
            {

            }
        }
        #endregion

        /// <summary>
        /// httpソースに含まれる本文だけ取得する
        /// 一般系から逸脱する可能性があるため、スペシャルメソッドとする
        /// ウェブブラウザにソースをかませて本文を取得。
        /// そのときに、プログラムが読みやすいように出力させるため、加工を施す
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHtmlPlainTextFromSource
        public string getHtmlPlainTextFromSourceWB(string source)
        {
            try
            {
                string result = source;

                //正規表現で無駄なものを除去
                result = pRegularReplace(result);

                return result;
            }
            catch
            {
                return source;
            }
            finally
            {

            }
        }
        #endregion

        /// <summary>
        /// タグで改行させる
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHtmlPlainTextFromSource
        public string getHtmlPlainTextFromSourceWBDirect(string source)
        {
            try
            {
                string result = source;

                //正規表現で無駄なものを除去
                result = pRegularReplace(result);

                return result;
            }
            catch
            {
                return source;
            }
            finally
            {

            }
        }
        #endregion

        /// <summary>
        /// HTMLソースを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHtmlSource
        [STAThread]
        public string getHtmlSource(string url)
        {
            return getHtmlSource(url, 30000);
        }
        public string getHtmlSource(string url, int TimeOut)
        {
            try
            {
                //結果
                string resStr = "";

                byte[] result;
                byte[] buffer = new byte[4096];

                //ウェブリクエスト
                WebRequest wr = WebRequest.Create(url);

                wr.Timeout = TimeOut;

                //レスポンス取得
                using (WebResponse response = wr.GetResponse())
                {
                    Console.WriteLine(response.ContentType);

                    //レスポンスをストリームで取得
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        //ストリームをコピー
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            int count = 0;
                            do
                            {
                                count = responseStream.Read(buffer, 0, buffer.Length);
                                memoryStream.Write(buffer, 0, count);

                            } while (count != 0);

                            //バイト配列に変換
                            result = memoryStream.ToArray();

                        }
                    }
                }

                //文字コードを取得
                Encoding e = HtmlParser.GetCode(result);

                //取得した文字コードから、バイト列をエンコード
                resStr = Encoding.GetEncoding(e.CodePage).GetString(result);

                //結果を返す
                return resStr;
            }
            catch (Exception err)
            {
                lc.writingLog("EntLeafRelation : getHtmlSource :" + err);
                return "";
            }
        }
        #endregion

        // HTMLを開いて、テキストを取得する
        #region GetHtmlText
        //public  string GetHtmlText(string htmlText)
        //{
        //    TidyStatus status;
        //    TidyDocument doc = new TidyDocument();
        //    string sResult = "";
        //    try
        //    {
        //        doc.SetCharEncoding("shiftjis");
        //        status = doc.LoadString(htmlText);
        //        status = doc.CleanAndRepair(); // HTMLを解析
        //        traceTag(doc.Body, ref sResult);
        //        return sResult;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //    finally
        //    {
        //        doc = null;
        //    }

        //}

        // HTML"ファイル"を開いて、テキストを取得する
        //public  string GetHtmlTextFromFile(string fileName)
        //{
        //    TidyStatus status;
        //    TidyDocument doc = new TidyDocument();
        //     string sResult = "";
        //    try
        //    {
        //        doc.SetCharEncoding("shiftjis");
        //        if (!ComPathController.checkFileExist(fileName))
        //        {
        //            return "";
        //        }

        //        status = doc.LoadFile(fileName);

        //        status = doc.CleanAndRepair(); // HTMLを解析
        //        traceTag(doc.Body, ref sResult);
        //        return sResult;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //    finally
        //    {
        //        doc = null;
        //    }
        //}
        #endregion

        /// <summary>
        /// 文字コードを判別する
        /// </summary>
        /// <param name="byts">文字コードを調べるデータ</param>
        /// <returns>適当と思われるEncodingオブジェクト。
        /// 判断できなかった時はnull。</returns>
        #region GetCode
        public static Encoding GetCode(byte[] byts)
        {
            try
            {
                const byte bESC = 0x1B;
                const byte bAT = 0x40;
                const byte bDollar = 0x24;
                const byte bAnd = 0x26;
                const byte bOP = 0x28;    //(
                const byte bB = 0x42;
                const byte bD = 0x44;
                const byte bJ = 0x4A;
                const byte bI = 0x49;

                int len = byts.Length;
                int binary = 0;
                int ucs2 = 0;
                int sjis = 0;
                int euc = 0;
                int utf8 = 0;
                byte b1, b2;

                for (int i = 0; i < len; i++)
                {
                    if (byts[i] <= 0x06 || byts[i] == 0x7F || byts[i] == 0xFF)
                    {
                        //'binary'
                        binary++;
                        if (len - 1 > i && byts[i] == 0x00
                            && i > 0 && byts[i - 1] <= 0x7F)
                        {
                            //smells like raw unicode
                            ucs2++;
                        }
                    }
                }

                if (binary > 0)
                {
                    if (ucs2 > 0)
                        //JIS
                        //ucs2(Unicode)
                        return System.Text.Encoding.Unicode;
                    else
                        //binary
                        return null;
                }

                for (int i = 0; i < len - 1; i++)
                {
                    b1 = byts[i];
                    b2 = byts[i + 1];

                    if (b1 == bESC)
                    {
                        if (b2 >= 0x80)
                            //not Japanese
                            //ASCII
                            return System.Text.Encoding.ASCII;
                        else if (len - 2 > i &&
                            b2 == bDollar && byts[i + 2] == bAT)
                            //JIS_0208 1978
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        else if (len - 2 > i &&
                            b2 == bDollar && byts[i + 2] == bB)
                            //JIS_0208 1983
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        else if (len - 5 > i &&
                            b2 == bAnd && byts[i + 2] == bAT && byts[i + 3] == bESC &&
                            byts[i + 4] == bDollar && byts[i + 5] == bB)
                            //JIS_0208 1990
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        else if (len - 3 > i &&
                            b2 == bDollar && byts[i + 2] == bOP && byts[i + 3] == bD)
                            //JIS_0212
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        else if (len - 2 > i &&
                            b2 == bOP && (byts[i + 2] == bB || byts[i + 2] == bJ))
                            //JIS_ASC
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                        else if (len - 2 > i &&
                            b2 == bOP && byts[i + 2] == bI)
                            //JIS_KANA
                            //JIS
                            return System.Text.Encoding.GetEncoding(50220);
                    }
                }

                for (int i = 0; i < len - 1; i++)
                {
                    b1 = byts[i];
                    b2 = byts[i + 1];
                    if (((b1 >= 0x81 && b1 <= 0x9F) || (b1 >= 0xE0 && b1 <= 0xFC)) &&
                        ((b2 >= 0x40 && b2 <= 0x7E) || (b2 >= 0x80 && b2 <= 0xFC)))
                    {
                        sjis += 2;
                        i++;
                    }
                }
                for (int i = 0; i < len - 1; i++)
                {
                    b1 = byts[i];
                    b2 = byts[i + 1];
                    if (((b1 >= 0xA1 && b1 <= 0xFE) && (b2 >= 0xA1 && b2 <= 0xFE)) ||
                        (b1 == 0x8E && (b2 >= 0xA1 && b2 <= 0xDF)))
                    {
                        euc += 2;
                        i++;
                    }
                    else if (len - 2 > i &&
                        b1 == 0x8F && (b2 >= 0xA1 && b2 <= 0xFE) &&
                        (byts[i + 2] >= 0xA1 && byts[i + 2] <= 0xFE))
                    {
                        euc += 3;
                        i += 2;
                    }
                }
                for (int i = 0; i < len - 1; i++)
                {
                    b1 = byts[i];
                    b2 = byts[i + 1];
                    if ((b1 >= 0xC0 && b1 <= 0xDF) && (b2 >= 0x80 && b2 <= 0xBF))
                    {
                        utf8 += 2;
                        i++;
                    }
                    else if (len - 2 > i &&
                        (b1 >= 0xE0 && b1 <= 0xEF) && (b2 >= 0x80 && b2 <= 0xBF) &&
                        (byts[i + 2] >= 0x80 && byts[i + 2] <= 0xBF))
                    {
                        utf8 += 3;
                        i += 2;
                    }
                }

                if (euc > sjis && euc > utf8)
                    //EUC
                    return System.Text.Encoding.GetEncoding(51932);
                else if (sjis > euc && sjis > utf8)
                    //SJIS
                    return System.Text.Encoding.GetEncoding(932);
                else if (utf8 > euc && utf8 > sjis)
                    //UTF8
                    return System.Text.Encoding.UTF8;

                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion




        /// <summary>
        /// URL正規表現チェック
        /// </summary>
        /// <returns></returns>
        public static bool urlRegularCheck(string url)
        {
            Regex regex = new Regex("s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+");
            try
            {
                return regex.IsMatch(url);
            }
            catch
            {
                return false;
            }
            finally
            {
                regex = null;
            }
            
        }

        /// <summary>
        /// HTMLタグ正規表現チェック
        /// </summary>
        /// <returns></returns>
        public static bool htmlTagRegularCheck(string discription)
        {
            Regex regex = new Regex("<.*?>");
             try
            {
                return regex.IsMatch(discription);
            }
            catch
            {
                return false;
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// brタグ改行変換
        /// </summary>
        /// <returns></returns>
        public static string brRegularReplace(string discription)
        {
            Regex regex = new Regex("<br .*?>");
            string result;
            try
            {
                result = regex.Replace(discription, "＃");
                return result;
            }
            catch
            {
                return "";
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// HTMLタグ正規表現除去
        /// </summary>
        /// <returns></returns>
        public static string htmlTagRegularRemove(string discription)
        {
            Regex regex = new Regex("<.*?>");
            string result;
            try
            {
                result = regex.Replace(discription, "");
                return result;
            }
            catch
            {
                return "";
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// 2chの投稿者を削除する
        /// </summary>
        /// <returns></returns>
        public static string html2chWriterRegularRemove(string discription)
        {
            try
            {
                return new Regex(@"([0-9]*:)|([0-9]*\s).*((ID:+[a-zA-Z0-9\s!-/:-@≠\[-`{-~]*)|([0-9]*:[0-9]*:[0-9]*\.[0-9]*\s[0-9]*)|([0-9]*:[0-9]*:[[0-9]*))").Replace(discription, "");
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// dcdescription
        /// </summary>
        /// <returns></returns>
        public static string htmlDcDescriptionRegularRemove(string discription)
        {
            try
            {
                return new Regex("dc[a-zA-Z]*(=\")|(=)").Replace(discription, "");
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// htmlEncodeRegularRemove
        /// htmlエンコード文字を除去する
        /// </summary>
        /// <returns></returns>
        public static string htmlEncodeRegularRemove(string discription)
        {
            try
            {
                return  new Regex("&.*;").Replace(discription, "");
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// HTMLタグ正規表現除去
        /// </summary>
        /// <returns></returns>
        public static string htmlCommentRegularRemove(string discription)
        {
            Regex regex = new Regex("<!--.*-->");
            try
            {
                return regex.Replace(discription, "");
            }
            catch
            {
                return "";
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// 英語記号、半角スペースが連続１０以上HITの場合除去する
        /// </summary>
        /// <returns></returns>
        public static string htmlGomiRegularRemove(string discription)
        {
            return htmlGomiRegularRemove(discription, 10);
        }
        public static string htmlGomiRegularRemove(string discription, int renzoku)
        {
            //Regex regex = new Regex("[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#a-zA-z0-9\\s\\\"\\[\\]\\{\\}]{10,}");
            Regex regex = new Regex("[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#a-zA-z0-9\\s\\\"\\[\\]\\{\\}]{" + renzoku.ToString() + ",}");
            try
            {
                return regex.Replace(discription, "");
            }
            catch
            {
                return "";
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// 同じ内容の行を削除する
        /// </summary>
        /// <returns></returns>
        public static string htmlMultiNewLineRemove(string discription)
        {
            //return Regex.Replace(discription,@"^(.*)(\r?\n1)+$","$1",RegexOptions.Multiline);
            return Regex.Replace(discription, @"^(.*)(\n)+$", "$1", RegexOptions.Multiline);
        }

        /// <summary>
        /// HTMLタグ正規表現除去
        /// </summary>
        /// <returns></returns>
        public static string htmYMDRemove(string discription)
        {
            Regex regex = new Regex("^[0-9]{4}年[0-9]{2}月[0-9]{2}日$");
            try
            {
                return regex.Replace(discription, "");
            }
            catch
            {
                return "";
            }
            finally
            {
                regex = null;
            }
        }

        /// <summary>
        /// pタグ改行変換
        /// </summary>
        /// <returns></returns>
        public static string pRegularReplace(string discription)
        {
            Regex regex = new Regex("</p>");
            Regex regex2 = new Regex("<p .*?>");
            string result;
            result = regex.Replace(discription, "<br>");
            result = regex.Replace(result, "");
            return result;
        }

        /// <summary>
        /// HTMLタグ正規表現を使ってリストに整形
        /// </summary>
        /// <returns></returns>
        public List<string> htmlTagRegularList(string discription)
        {
            List<string> result = new List<string>();

            //discription内で正規表現と一致する対象をすべて検索
            Regex regex = new Regex("<.*?>");
            MatchCollection mc = regex.Matches(discription);

            try
            {
                foreach (System.Text.RegularExpressions.Match m in mc)
                {
                    //正規表現に一致したグループと位置を表示
                    foreach (var g in m.Groups)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        result.Add(g.ToString());
                    }
                }
                return result;
            }
            catch
            {
                return new List<string>();
            }
            finally
            {
                mc = null;
                regex = null;
            }
        }

        /// <summary>
        /// HTMLタグ正規表現チェック
        /// </summary>
        /// <returns></returns>
        public  bool kigoRegularCheck(string discription)
        {
            Regex regex = new Regex("@[｡-ﾟ]");
            return regex.IsMatch(discription);
        }

        /// <summary>
        /// Imgタグを走査し、存在したらURLを返す。
        /// </summary>
        /// <returns></returns>
        public List<string> searchImgTag(List<string> tagList)
        {
            List<string> result = new List<string>();
            string buf = "";

            try
            {
                foreach (string tag in tagList)
                {
                    System.Windows.Forms.Application.DoEvents();
                    if (checkImgTag(tag))
                    {
                        //src属性の検索と取得
                        Regex rSrc = new Regex("src=\"htt.*\"", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        MatchCollection mcSrc = rSrc.Matches(tag);
                        foreach (Match m in mcSrc)
                        {
                            //正規表現に一致したグループと位置を表示
                            buf = m.Groups[0].ToString();
                        }

                        rSrc = null;
                        mcSrc = null;

                        //httpアドレスを取得する
                        Regex rHttp = new Regex("s?https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        MatchCollection mcHttp = rHttp.Matches(tag);
                        foreach (Match m in mcHttp)
                        {
                            //正規表現に一致したグループと位置を表示
                            result.Add(m.Groups[0].ToString());
                        }

                        rHttp = null;
                        mcHttp = null;
                    }
                }
                return result;
            }
            catch
            {
                return new List<string>();
            }

        }

        /// <summary>
        /// imgタグが存在するかチェックする
        /// </summary>
        /// <returns></returns>
        public  bool checkImgTag(string tag)
        {
            Regex regex = new Regex(@"<img.*>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            try
            {
                return regex.IsMatch(tag);
            }
            catch
            {
                return false;
            }
            finally
            {
                regex = null;
            }  

            
        }

    }
}
