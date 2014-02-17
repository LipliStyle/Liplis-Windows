//=======================================================================
//  ClassName : LiplisUtil
//  概要      : リプリスユーティル
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Liplis.Xml;

namespace Liplis.Common
{
    public class LiplisUtil
    {
        /// <summary>
        /// IsNumeric
        /// 数値チェック
        /// </summary>
        /// <param name="stTarget">ターゲットストリング</param>
        /// <returns>結果の真偽</returns>
        #region IsNumeric
        public static bool IsNumeric(string stTarget)
        {
            double dNullable;

            return double.TryParse(
                stTarget,
                System.Globalization.NumberStyles.Any,
                null,
                out dNullable
            );
        }
        #endregion

        /// <summary>
        /// IsNumericReturnResut
        /// 数値チェック&数値変換
        /// </summary>
        /// <param name="stTarget">ターゲットストリング</param>
        /// <param name="defaultVal">デフォルト値</param>
        /// <returns>真:ターゲットをIntに変換して返す。</returns>
        /// <returns>偽:デフォルト値を返す。</returns>
        #region IsNumericReturnResut
        public static int IsNumericReturnResut(string stTarget, int defaultVal)
        {
            if (stTarget == null)       //nullならデフォルトを返す
            {
                return defaultVal;
            }
            if (IsNumeric(stTarget))     //数値なら数値に変換して返す
            {
                return int.Parse(stTarget);
            }
            return defaultVal;          //数値でなければデフォルト値を返す
        }
        #endregion

        /// <summary>
        /// nullChekc
        /// ヌルチェッカー
        /// </summary>
        /// <param name="str">対象文字列</param>
        /// <returns>nullなら""を返す</returns>
        #region nullCheck
        public static string nullCheck(string str)
        {
            string result = "";

            //ヌルでなければ結果をそのまま返す
            if (str != null)
            {
                result = str;
            }

            return result;
        }
        /// <summary>
        /// nullChekc
        /// ヌルチェッカー
        /// </summary>
        /// <param name="val">対象数値</param>
        /// <returns>nullなら""を返す</returns>
        public static string nullCheck(int val)
        {
            string result = "";

            if (!val.Equals(null))
            {
                result = val.ToString();
            }

            return result;
        }
        /// <summary>
        /// nullChekc
        /// ヌルチェッカー
        /// </summary>
        /// <param name="val">対象数値</param>
        /// <returns>nullなら0を返す</returns>
        public static int nullCheckInt(int val)
        {
            int result = 0;

            if (val != 0)
            {
                result = val;
            }

            return result;
        }
        #endregion

        /// <summary>
        /// int数値をフラグに変換する
        /// ※[1]をtrueとする場合に限る!!
        /// </summary>
        /// <param name="flg"></param>
        /// <returns></returns>
        public static bool bitToBool(int flg)
        {
            return flg == 1;
        }

        /// <summary>
        /// ブール型をビットに変換する
        /// </summary>
        #region boolToBit
        public static int boolToBit(bool flg)
        {
            if (flg)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// ブル型のリストをビットのリストに変換する
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static List<int> boolToBitList(List<bool> boolList)
        {
            List<int> result = new List<int>();
            foreach (bool flg in boolList)
            {
                Application.DoEvents();
                result.Add(boolToBit(flg));
            }
            return result;
        }

        /// <summary>
        /// 正反対のフラグを返す
        /// </summary>
        /// <param name="flg"></param>
        /// <returns></returns>
        public static int returnReverseFlg(bool flg)
        {
            if (flg)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// calcSecToMin
        /// 秒から分への変換
        /// </summary>
        /// <returns></returns>
        public static double calcSecToMin(double min)
        {
            return min / 60;
        }


        /// <summary>
        /// calcMinToSec
        /// 分から秒への変換
        /// </summary>
        /// <returns></returns>
        public static double calcMinToSec(double min)
        {
            return min * 60;
        }

        /// <summary>
        /// calcMinToMilsec
        /// 分からミリ秒への変換
        /// </summary>
        /// <returns></returns>
        public static double calcMinToMilsec(double min)
        {
            return min * 60000;
        }

        /// <summary>
        /// 時刻ストリングから秒数を計算する
        /// </summary>
        /// <returns></returns>
        public static int culcSecond(string time)
        {
            int result = 0;

            try
            {
                string[] hms = time.Split(':');

                //数値チェック
                foreach (string str in hms)
                {
                    Application.DoEvents();
                    if (!IsNumeric(str))
                    {
                        return 0;
                    }
                }

                for (int idx = 0; idx < hms.Length; idx++)
                {
                    Application.DoEvents();
                    if (idx > 2) { return result; }
                    if (idx == 0)
                    {
                        result = result + int.Parse(hms[0]) * 1000;
                    }
                    else if (idx == 1)
                    {
                        result = result + int.Parse(hms[1]) * 60000;
                    }
                    else if (idx == 2)
                    {
                        result = result + int.Parse(hms[2]) * 3600000;
                    }
                }


                return result;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 正しい年月かチェックする
        /// </summary>
        /// <returns></returns>
        public static bool checkDateTime(string timeString)
        {
            DateTime dt;
            return DateTime.TryParse(timeString, out dt);
        }

        /// <summary>
        /// strToDateTime
        /// 時刻のタイムストリングをデイトタイム型に変換する
        /// </summary>
        /// <returns></returns>
        public static DateTime strToDateTime(string timeString)
        {
            return DateTime.Parse(timeString);
        }

        /// <summary>
        /// a/bの結果を切り上げで返す
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int divaideRoundUp(int a, int b)
        {
            decimal A = new decimal(a);
            decimal B = new decimal(b);
            decimal result = Math.Ceiling(A / B);
            return (int)result;
        }

        /// <summary>
        /// 文中に含まれる文字数をカウントする
        /// </summary>
        /// <returns></returns>
        public static int countTargetStr(string targetStr, string countStr)
        {
            int idx = 0;
            int cnt = 0;

            try
            {

                if (targetStr.Length < 1)
                {
                    return 0;
                }

                while (idx >= 0)
                {
                    idx = targetStr.IndexOf(countStr, idx + 1);

                    if (idx >= 0)
                    {
                        cnt++;
                    }

                    //バカよけ
                    if (cnt > 10000)
                    {
                        return 0;
                    }
                }
                return cnt;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// ストリングのバイト数を取得する
        /// </summary>
        #region getStringByte
        public static int getStringByte(string str)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            return sjisEnc.GetByteCount(str);
        }
        #endregion

        /// <summary>
        /// リストの中身をシャッフルする
        /// </summary>
        /// <returns></returns>
        public static void listShaffle(ref List<int> lst)
        {
            Random rdm1 = new Random(unchecked((int)DateTime.Now.Ticks));
            int value = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                value = rdm1.Next(lst.Count);
                object song = lst[value];
                lst.Add(lst[value]);
                lst.RemoveAt(value);
            }
        }
        public static void listShaffle(ref List<string> lst)
        {
            Random rdm1 = new Random(unchecked((int)DateTime.Now.Ticks));
            int value = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                value = rdm1.Next(lst.Count);
                object song = lst[value];
                lst.Add(lst[value]);
                lst.RemoveAt(value);
            }
        }

        /// <summary>
        /// ディレクトリをコピーする
        /// </summary>
        /// <param name="sourceDirName">コピーするディレクトリ</param>
        /// <param name="destDirName">コピー先のディレクトリ</param>
        #region CopyDirectory
        public static void CopyDirectory(string sourceDirName, string destDirName)
        {
            //コピー先のディレクトリがないときは作る
            if (!System.IO.Directory.Exists(destDirName))
            {
                System.IO.Directory.CreateDirectory(destDirName);
                //属性もコピー
                System.IO.File.SetAttributes(destDirName,
                    System.IO.File.GetAttributes(sourceDirName));
            }

            //コピー先のディレクトリ名の末尾に"\"をつける
            if (destDirName[destDirName.Length - 1] !=
                    System.IO.Path.DirectorySeparatorChar)
                destDirName = destDirName + System.IO.Path.DirectorySeparatorChar;

            //コピー元のディレクトリにあるファイルをコピー
            string[] files = System.IO.Directory.GetFiles(sourceDirName);
            foreach (string file in files)
            {
                Application.DoEvents();
                System.IO.File.Copy(file,
                    destDirName + System.IO.Path.GetFileName(file), true);
            }

            //コピー元のディレクトリにあるディレクトリについて、
            //再帰的に呼び出す
            string[] dirs = System.IO.Directory.GetDirectories(sourceDirName);
            {
                Application.DoEvents();
                foreach (string dir in dirs)
                    CopyDirectory(dir, destDirName + System.IO.Path.GetFileName(dir));
            }
        }
        #endregion

        /// <summary>
        /// ファイルコピー
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        #region CopyFile
        public static bool CopyFile(string sourceFileName, string destFileName)
        {
            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// ファイルでリート
        /// </summary>
        /// <param name="sourveFileName"></param>
        /// <returns></returns>
        #region DeleteFile
        public static bool DeleteFile(string sourveFileName)
        {
            try
            {
                System.IO.File.Delete(sourveFileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// ニコニコドメインのURLかチェックする
        /// </summary>
        /// <returns></returns>
        #region domainCheck
        public static bool domainCheck(string uri, string targetDomain)
        {
            //ニコニコドメインチェック
            if (!uri.Equals("") && uri != null)            //uri未設定でなければチェック
            {
                if (uri.Substring(0, targetDomain.Length).Equals(targetDomain))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        #endregion

        /// <summary>
        /// USBメモリーから起動されているか
        /// </summary>
        /// <returns>0 : Unknown, 1 : Normal, 2 : USB, 3 : Network</returns>
        #region usbCheck
        public static int usbCheck()
        {
            string driveLetter = "";
            try
            {
                driveLetter = LpsPathControllerCus.getAppPath().Substring(0, 1);
                DriveInfo drive = new DriveInfo(nullCheck(driveLetter));
                if (drive.DriveType == DriveType.Removable)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                if (driveLetter.Equals("\\"))
                {
                    return 3;
                }
            }
            return 0;
        }
        #endregion

        /// <summary>
        /// 最後の一文字がスラッシュかバックスラッシュなら削る
        /// </summary>
        #region removeLast
        public static string removeLast(string target)
        {
            string result = target;
            if (target.Substring(target.Length - 1, 1).Equals("\\"))
            {
                result = target.Remove(target.Length - 1, 1);
            }
            else if (target.Substring(target.Length - 1, 1).Equals("/"))
            {
                result = target.Remove(target.Length - 1, 1);
            }

            return result;
        }
        #endregion

        /// <summary>
        /// ポイントを取得
        /// </summary>
        #region getPoint
        public static Point getPoint(Rectangle rect)
        {
            BaseCompatilizedRandom r = new BaseCompatilizedRandom(new RndMersenneTwister());
            return new Point(r.Next(rect.X, rect.X + rect.Width), r.Next(rect.Y, rect.Y + rect.Height));
        }
        public static Point getPoint(Rectangle rect, int seed)
        {
            BaseCompatilizedRandom r = new BaseCompatilizedRandom(new RndMersenneTwister(seed));
            return new Point(r.Next(rect.X, rect.X + rect.Width), r.Next(rect.Y, rect.Y + rect.Height));
        }
        #endregion

        /// <summary>
        /// ランダムな文字列を生成する
        /// </summary>
        /// <param name="length">長さ</param>
        /// <returns>生成文字列</returns>
        #region getName
        public static string getName(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            Random r;
            try
            {
                System.Threading.Thread.Sleep(10);
                r = new Random(Environment.TickCount + length);
            }
            catch
            {
                r = new Random();
            }


            for (int i = 0; i < length; i++)
            {
                //文字の位置をランダムに選択
                int pos = r.Next(passwordChars.Length);
                //選択された位置の文字を取得
                char c = passwordChars[pos];
                //パスワードに追加
                sb.Append(c);
            }

            return sb.ToString();
        }
        private static readonly string passwordChars = "0123456789abcdefghijklmnopqrstuvwxyz";
        #endregion

        /// <summary>
        /// ランダム数を取得
        /// </summary>
        /// <param name="max">最大値</param>
        /// <returns>ランダム値</returns>
        #region getRandamIntStrictness
        public static int getRandamIntStrictness(int max)
        {
            BaseCompatilizedRandom r = new BaseCompatilizedRandom(new RndMersenneTwister());
            return r.Next(max);
        }
        #endregion

        /// <summary>
        /// ランダム数を取得
        /// </summary>
        /// <param name="max">最大値</param>
        /// <returns>ランダム値</returns>
        #region getRandamInt
        public static int getRandamInt(int start ,int max)
        {
            Random r = new Random();
            return r.Next(max) + start;
        }
        #endregion

        /// <summary>
        /// ニコニコID正規表現チェック
        /// </summary>
        #region getNicoVideoId
        public static string getNicoVideoId(string url)
        {
            Regex regex = new Regex("sm[0-9]{1,8}");
            try
            {
                MatchCollection mc = regex.Matches(url);
                foreach (Match m in mc)
                {
                    Application.DoEvents();
                    return m.Value;
                }
                return "";
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
        #endregion

        /// <summary>
        /// クリップボードにテキストを貼り付ける
        /// </summary>
        #region setDataToClipBord
        public static void setDataToClipBord(string str)
        {
            Clipboard.SetText(str);
        }
        #endregion

        /// <summary>
        /// クリップボードのテキストを取得する
        /// </summary>
        #region getDataFromClipBord
        public static string getDataFromClipBord()
        {
            return Clipboard.GetText();
        }
        #endregion


        /// <summary>
        /// disposeInamge
        /// 対象のビットマップがNullでなければ破棄する
        /// </summary>
        /// <param name="bmp">破棄対象Bitmap</param>
        #region disposeInamge
        public static void disposeInamge(Bitmap bmp)
        {
            if (bmp != null)
            {
                bmp.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// LenB
        /// 文字列のバイト数を取得する
        /// </summary>
        /// <param name="stTarget"></param>
        /// <returns></returns>
        #region　LenB メソッド
        public static int LenB(string stTarget)
        {
            return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(stTarget);
        }
        #endregion

        ///====================================================================
        ///
        ///                            WEB
        ///                         
        ///====================================================================
        ///
        /// <summary>
        /// 対象のXMLが有効かどうか判断する
        /// </summary>
        #region checkRssConnect
        public static string checkRssConnect(string url)
        {
            string title = null;
            try
            {
                RssReader rr = new RssReader(url);
                title = rr.title;
                rr.Dispose();
            }
            catch
            {
                title = null;
            }
            return title;
        }
        #endregion

        ///====================================================================
        ///
        ///                           メッセージ
        ///                         
        ///====================================================================

        /// <summary>
        /// LiplisMessage
        /// OK/CANCELダイアログを出し、結果を返す
        /// タイトルは固定、メッセージのみ指定可能
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        #region LiplisMessage
        public static bool LiplisMessage(string msg)
        {
            return MessageBox.Show(msg, LiplisDefine.MESSAGE_TITLE,MessageBoxButtons.OKCancel) == DialogResult.OK;
        }
        #endregion
    }
}
