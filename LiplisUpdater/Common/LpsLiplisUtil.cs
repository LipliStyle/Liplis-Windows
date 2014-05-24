//=======================================================================
//  ClassName : LiplisUtil
//  概要      : ユーティリティクラス
//
//  Liplisシステム      
//  Copyright(c) 2010-2013 sachin.Sachin
//=======================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Liplis.Xml;

namespace Liplis.Common
{
    public static class LpsLiplisUtil
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
        #region nullCheck
        public static string nullCheck(string str)
        {
            try
            {
                string result = "";

                //ヌルでなければ結果をそのまま返す
                if (str != null)
                {
                    result = str;
                }

                return result;
            }
            catch
            {
                return "";
            }

        }
        public static string nullCheck(object str)
        {
            try
            {
                if(str == null)
                {
                    return "";
                }

                string result = "";

                //ヌルでなければ結果をそのまま返す
                if (str != null)
                {
                    result = str.ToString();
                }

                return result;
            }
            catch
            {
                return "";
            }

        }
        public static string nullCheck(int val)
        {
            try
            {

                string result = "";

                if (!val.Equals(null))
                {
                    result = val.ToString();
                }

                return result;
            }
            catch
            {
                return "0";
            }

        }

        public static int nullCheckInt(int val)
        {
            try
            {
                int result = 0;

                if (val != 0)
                {
                    result = val;
                }

                return result;
            }
            catch
            {
                return 0;
            }

        }
        #endregion

        /// <summary>
        /// checkSqlParam
        /// SQLパラメータチェック
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        #region checkSqlParam
        public static string checkSqlParam(string param)
        {
            return param.Replace("\'", "\'\'");
        }
        #endregion


        /// <summary>
        /// int数値をフラグに変換する
        /// ※[1]をtrueとする場合に限る!!
        /// </summary>
        /// <param name="flg"></param>
        /// <returns></returns>
        #region bitToBool
        public static bool bitToBool(int flg)
        {
            try
            {
                return flg == 1;
            }
            catch
            {
                return false;
            }       
        }
        #endregion

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
        #region boolToBitList
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
        #endregion

        /// <summary>
        /// 正反対のフラグを返す
        /// </summary>
        /// <param name="flg"></param>
        /// <returns></returns>
        #region returnReverseFlg
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
        #endregion

        /// <summary>
        /// calcSecToMin
        /// 秒から分への変換
        /// </summary>
        /// <returns></returns>
        #region calcSecToMin
        public static double calcSecToMin(double min)
        {
            return min / 60;
        }
        #endregion

        /// <summary>
        /// calcMinToSec
        /// 分から秒への変換
        /// </summary>
        /// <returns></returns>
        #region calcMinToSec
        public static double calcMinToSec(double min)
        {
            return min * 60;
        }
        #endregion

        /// <summary>
        /// calcMinToMilsec
        /// 分からミリ秒への変換
        /// </summary>
        /// <returns></returns>
        #region calcMinToMilsec
        public static double calcMinToMilsec(double min)
        {
            return min * 60000;
        }
        #endregion

        /// <summary>
        /// 時刻ストリングから秒数を計算する
        /// </summary>
        /// <returns></returns>
        #region culcSecond
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
        #endregion

        /// <summary>
        /// 正しい年月かチェックする
        /// </summary>
        /// <returns></returns>
        #region checkDateTime
        public static bool checkDateTime(string timeString)
        {
            DateTime dt;
            return DateTime.TryParse(timeString, out dt);
        }
        #endregion

        /// <summary>
        /// ストリングを日付に変換する
        /// </summary>
        /// <returns></returns>
        #region strToDateTime
        public static DateTime strToDateTime(string timeString)
        {
            try
            {
                return DateTime.Parse(timeString);
            }
            catch
            {
                return DateTime.Now;
            }
            
        }
        #endregion

        /// <summary>
        /// ストリングの日付をデイとに変換して比較する
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>-1 : 失敗,0 : 左大,1 : 等しい,2 : 右大</returns>
        #region comPairToDate
        public static int comPairToDate(string left, string right)
        {
            return comPairToDate(DateTime.Parse(left), DateTime.Parse(right));
        }
        public static int comPairToDate(DateTime left, DateTime right)
        {
            try
            {
                //左大
                if (left> right)
                {
                    return 0;
                }
                //右代
                else if (left < right)
                {
                    return 2;
                }
                //イコール
                else
                {
                    return 1;
                }
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        /// <summary>
        /// getYobi
        /// 日付から曜日を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        #region getYobi
        public static string getYobi(DateTime date)
        {
            return ("日月火水木金土").Substring(int.Parse(date.DayOfWeek.ToString("d")), 1);
        }
        public static string getYobi(string date)
        {
            try
            {
                return getYobi(DateTime.Parse(date));
            }
            catch
            {
                return getYobi(DateTime.Now);
            }
        }
        #endregion


        /// <summary>
        /// atetimeFromCompleteTimeDateTime
        /// </summary>
        /// <param name="completeTime"></param>
        /// <returns></returns>
        #region atetimeFromCompleteTimeDateTime
        public static DateTime atetimeFromCompleteTimeDateTime(string completeTime)
        {
            return DateTime.Parse(atetimeFromCompleteTimeString(completeTime));
        }
        public static string atetimeFromCompleteTimeString(string completeTime)
        {
            try
            {
                string date = "";
                //int hour = 0;
                //int min = 0;
                //int sec = 9;
                string[] buf = completeTime.Split('T');
                string[] buf2 = buf[1].Split('+');
                string[] buf3 = buf2[0].Split(':');
                string[] buf4 = buf2[1].Split(':');

                //日時の算出
                date = buf[0].Replace("-", "/");

                //時刻の算出
                //hour = int.Parse(buf3[0]) + int.Parse(buf4[0]);
                //min = int.Parse(buf3[1]) + int.Parse(buf4[1]);
                //sec = int.Parse(buf3[2]);

                //結果を返す
                //return date + " " + hour + ":" + min + ":" + sec;

                return date + " " + buf2[0];
            }
            catch
            {
                return DateTime.Now.ToString();
            }


        }


        #endregion

        /// <summary>
        /// a/bの結果を切り上げで返す
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        #region divaideRoundUp
        public static int divaideRoundUp(int a, int b)
        {
            decimal A = new decimal(a);
            decimal B = new decimal(b);
            decimal result = Math.Ceiling(A / B);
            return (int)result;
        }
        #endregion

        /// <summary>
        /// 文中に含まれる文字数をカウントする
        /// </summary>
        /// <returns></returns>
        #region countTargetStr
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
        #endregion

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
        /// ストリングをintに変換する
        /// </summary>
        #region stringToInt
        public static int stringToInt(string str)
        {
            try {
                return int.Parse(str);
            }
            catch{
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// ストリングをlongに変換する
        /// </summary>
        #region stringToLong
        public static long stringToLong(string str)
        {
            try
            {
                return long.Parse(str);
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// リストの中身をシャッフルする
        /// </summary>
        /// <returns></returns>
        #region listShaffle
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
        #endregion

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
        /// <param name="sourceFileName">コピー元ファイルパス</param>
        /// <param name="destFileName">コピー先ファイルパス</param>
        #region CopyFile
        public static bool CopyFile(string sourceFileName, string destFileName)
        {
            try
            {
                File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// フォルダにあるファイルを全てコピー
        /// </summary>
        /// <param name="sourceFileName">コピー元ファイルパス</param>
        /// <param name="destFileName">コピー先ファイルパス</param>
        #region CopyFileDirAll
        public static bool CopyFileDirAll(string sourcePath, string destPath)
        {
            try
            {
                foreach (string stCopyFrom in System.IO.Directory.GetFiles(sourcePath))
                {
                    string stCopyTo = System.IO.Path.Combine(destPath, System.IO.Path.GetFileName(stCopyFrom));
                    System.IO.File.Copy(stCopyFrom, stCopyTo, true);
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
        /// RenameFile
        /// ファイルコピー
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        #region RenameFile
        public static bool RenameFile(string sourceFileName, string loadFile)
        {
            try
            {
                System.IO.File.Move(sourceFileName, loadFile);
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
        /// ディレクトリでリート
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        #region DeleteDir
        public static bool DeleteDir(string dirName)
        {
            try
            {
                System.IO.Directory.Delete(dirName, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// getFileList
        /// ファイルリストを取得する
        /// ファイルは名前順に取得される。
        /// ソースはここ:http://msdn.microsoft.com/library/en-us/fileio/fs/findnextfile.asp
        /// </summary>
        /// <param name="sourveFileName"></param>
        /// <returns></returns>
        #region getFileList
        public static List<string> getFileList(string dir, string ptn)
        {
            List<string> fileList = new List<string>();

            string[] files = Directory.GetFiles(dir, ptn);
            foreach (string s in files)
            {
                fileList.Add(s);
            }

            string[] dirs = Directory.GetDirectories(dir);
            foreach (string s in dirs)
            {
                fileList.AddRange(getFileList(s, ptn));
            }

            return fileList;
        }
        public static List<string> getFileListNonCicle(string dir, string ptn)
        {
            List<string> fileList = new List<string>();

            string[] files = Directory.GetFiles(dir, ptn);
            foreach (string s in files)
            {
                fileList.Add(s);
            }

            return fileList;
        }
        #endregion


        /// <summary>
        /// ファイル存在チェック
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        #region ExistsFile
        public static bool ExistsFile(string sourceFileName)
        {
            return File.Exists(sourceFileName); 
        }
        #endregion

        /// <summary>
        /// ディレクトリ存在チェック
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        #region ExistsDir
        public static bool ExistsDir(string sourceDirName)
        {
            return Directory.Exists(sourceDirName);
        }
        #endregion

        /// <summary>
        /// getTextFileText
        /// テキストファイルの内容をリストで返す
        /// </summary>
        /// <param name="sourveFileName"></param>
        /// <returns></returns>
        #region getTextFileText
        public static List<string> getTextFileText(string path)
        {
            string line = "";
            List<string> resList = new List<string>();
            //テキストファイル読み込み
            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
            {
                //処理
                while ((line = sr.ReadLine()) != null)
                {
                    resList.Add(line);
                }
            }

            return resList;
        }
        public static List<string> getTextFileText(string path,Encoding enc)
        {
            string line = "";
            List<string> resList = new List<string>();
            //テキストファイル読み込み
            using (StreamReader sr = new StreamReader(path, enc))
            {
                //処理
                while ((line = sr.ReadLine()) != null)
                {
                    resList.Add(line);
                }
            }

            return resList;
        }
        #endregion

        /// <summary>
        /// writeTextFile
        /// リストの内容をテキストファイルに書き出す
        /// </summary>
        /// <param name="sourveFileName"></param>
        /// <returns></returns>
        #region writeTextFile
        public static bool writeTextFile(List<string> list, string path)
        {
            try
            {
                Encoding sjis = Encoding.GetEncoding("Shift-JIS");
                using (StreamWriter w = new StreamWriter(path, false, sjis))
                {
                    foreach (string line in list)
                    {
                        w.WriteLine(line);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public static bool writeTextFile(List<string> list, string path, Encoding enc)
        {
            try
            {
                using (StreamWriter w = new StreamWriter(path, false, enc))
                {
                    foreach (string line in list)
                    {
                        w.WriteLine(line);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static bool writeTextFile(string msg, string path, Encoding enc)
        {
            try
            {
                using (StreamWriter w = new StreamWriter(path, false, enc))
                {
                    w.Write(msg);
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
        /// 引数文字列をUnicode表現
        /// </summary>
        /// <remarks>
        /// 引数文字列をunicode表現(\u+XXXX)に変換します。
        /// </remarks>
        /// <param name="str">文字列</param>
        /// <returns>unicode表現文字列</returns>
        #region toUnicodeExpression
        public static string toUnicodeExpression(string str)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(str);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < byteArray.Length; i += 2)
            {
                sb.Append(@"\u");
                sb.Append(string.Format("{0:X2}{1:X2}", byteArray[i + 1], byteArray[i]));
            }

            return sb.ToString();
        }
        #endregion


        /// <summary
        /// Unicode表現文字列をUnicodeに変換します。
        /// </summary>
        /// <remarks>
        /// unicode表現(\u+XXXX)をUnicode文字列に変換します。
        /// ToUnicodeExpressionの対になるメソッドです
        /// </remarks>
        /// <param name="str">Unicode表現文字列(\uXXXX)</param>
        /// <returns>Unicode文字列</returns>
        #region reverseToUniCode
        public static string reverseToUniCode(string str)
        {

            //正規表現でユニコード表現文字列を検索・抽出します。

            IList codeList = new ArrayList();

            Regex regUnicode = new Regex(@"(\\u){1}[0-9a-fA-F]{4}");

            for (Match matchUniCode = regUnicode.Match(str);

                        matchUniCode.Success;

                                matchUniCode = matchUniCode.NextMatch())
            {

                codeList.Add(matchUniCode.Groups[0].Value.Replace(@"\u", ""));

            }

            StringBuilder sb = new StringBuilder();

            //リトルエンディアン方式を前提にしているので

            //0,1文字目と2,3文字目の組を16進文字列とみなし、数値に変換します。

            //0,1文字目の組と、2,3文字目の組の順序を入れ替えてbyte配列を作成し、

            //エンコーディングを行います。

            //上記正規表現にマッチしている以上、char配列は4個あることが保証されています。

            //(配列数のチェックはしません)

            foreach (string unicode in codeList)
            {

                char[] codeArray = unicode.ToCharArray();

                int intVal1 = Convert.ToByte(codeArray[0].ToString(), 16) *

                                     16 + Convert.ToByte(codeArray[1].ToString(), 16);

                int intVal2 = Convert.ToByte(codeArray[2].ToString(), 16) *

                                     16 + Convert.ToByte(codeArray[3].ToString(), 16);

                sb.Append(Encoding.Unicode.GetString(

                                new byte[] { (byte)intVal2, (byte)intVal1 }));

            }

            return sb.ToString();

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
                return uri.StartsWith(targetDomain);
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
                driveLetter = LpsPathController.getAppPath().Substring(0, 1);
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

        /// -----------------------------------------------------------------------------------------
        /// <summary>
        ///     文字列の左端から指定したバイト数分の文字列を返します。</summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。<param>
        /// <param name="iByteSize">
        ///     取り出すバイト数。</param>
        /// <returns>
        ///     左端から指定されたバイト数分の文字列。</returns>
        /// -----------------------------------------------------------------------------------------
        #region　LeftB メソッド


        public static string LeftB(string stTarget, int iByteSize)
        {
            return MidB(stTarget, 1, iByteSize);
        }

        #endregion

        /// -----------------------------------------------------------------------------------------
        /// <summary>
        ///     文字列の指定されたバイト位置以降のすべての文字列を返します。</summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。</param>
        /// <param name="iStart">
        ///     取り出しを開始する位置。</param>
        /// <returns>
        ///     指定されたバイト位置以降のすべての文字列。</returns>
        /// -----------------------------------------------------------------------------------------
        #region　MidB メソッド (+1)


        public static string MidB(string stTarget, int iStart)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] btBytes = hEncoding.GetBytes(stTarget);

            return hEncoding.GetString(btBytes, iStart - 1, btBytes.Length - iStart + 1);
        }

        /// -----------------------------------------------------------------------------------------
        /// <summary>
        ///     文字列の指定されたバイト位置から、指定されたバイト数分の文字列を返します。</summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。</param>
        /// <param name="iStart">
        ///     取り出しを開始する位置。</param>
        /// <param name="iByteSize">
        ///     取り出すバイト数。</param>
        /// <returns>
        ///     指定されたバイト位置から指定されたバイト数分の文字列。</returns>
        /// -----------------------------------------------------------------------------------------
        public static string MidB(string stTarget, int iStart, int iByteSize)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] btBytes = hEncoding.GetBytes(stTarget);

            if (btBytes.Length <= iByteSize)
            {
                iByteSize = btBytes.Length;
            }

            return hEncoding.GetString(btBytes, iStart - 1, iByteSize);
        }

        #endregion


        /// -----------------------------------------------------------------------------------------
        /// <summary>
        ///     文字列の右端から指定されたバイト数分の文字列を返します。</summary>
        /// <param name="stTarget">
        ///     取り出す元になる文字列。</param>
        /// <param name="iByteSize">
        ///     取り出すバイト数。</param>
        /// <returns>
        ///     右端から指定されたバイト数分の文字列。</returns>
        /// -----------------------------------------------------------------------------------------
        #region　RightB メソッド


        public static string RightB(string stTarget, int iByteSize)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] btBytes = hEncoding.GetBytes(stTarget);

            return hEncoding.GetString(btBytes, btBytes.Length - iByteSize, iByteSize);
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
        #region getName
        private static readonly string passwordChars = "0123456789abcdefghijklmnopqrstuvwxyz";
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
        #endregion

        /// <summary>
        /// ランダムで複雑な文字列を生成する
        /// </summary>
        #region getPassword
        private static readonly string passwordChars2 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string getPassword(int length)
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
                int pos = r.Next(passwordChars2.Length);
                //選択された位置の文字を取得
                char c = passwordChars2[pos];
                //パスワードに追加
                sb.Append(c);
            }

            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// ランダム数を取得
        /// </summary>
        #region getRandamInt
        public static int getRandamInt(int max)
        {
            BaseCompatilizedRandom r = new BaseCompatilizedRandom(new RndMersenneTwister());
            return r.Next(max);
        }
        #endregion

        /// <summary>
        /// createLiplisGuid
        /// 独自Guidを作成する
        /// </summary>
        #region createLiplisGuid
        public static string createLiplisGuid()
        {
            return Guid.NewGuid().ToString() + String.Format("{0:000}", DateTime.Now.Millisecond);
        }
        #endregion

        /// <summary>
        /// getEisuOnly
        /// 英数字のみを抽出する
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        #region getEisuOnly
        public static string getEisuOnly(string str)
        {
            //結果
            string res = str;

            //Regexオブジェクトを作成
            Regex r = new Regex(@"[A-Z0-9]",RegexOptions.IgnoreCase);

            //TextBox1.Text内で正規表現と一致する対象を1つ検索
            Match m = r.Match(str);

            //次のように一致する対象をすべて検索することもできる
            //System.Text.RegularExpressions.MatchCollection mc = r.Matches(TextBox1.Text);

            while (m.Success)
            {
                str += m.Value.ToString();
                m = m.NextMatch();
            }

            return res;
        }

        #endregion

        /// <summary>
        /// getEisuOnly
        /// 英数字のみを抽出する
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        #region getEisuOnly
        public static string getEisuJpOnly(string str)
        {
            //結果
            string res = "";
            string resb = "";

            //Regexオブジェクトを作成
            Regex r = new Regex(@"[^\x01-\x7E]", RegexOptions.IgnoreCase);

            //TextBox1.Text内で正規表現と一致する対象を1つ検索
            Match m = r.Match(str);

            //次のように一致する対象をすべて検索することもできる
            //System.Text.RegularExpressions.MatchCollection mc = r.Matches(TextBox1.Text);

            while (m.Success)
            {
                resb += m.Value.ToString();
                m = m.NextMatch();
            }

            res += resb;
            resb = "";

            r = new Regex(@"[a-zA-Z0-9]", RegexOptions.IgnoreCase);

            //TextBox1.Text内で正規表現と一致する対象を1つ検索
            m = r.Match(str);

            //次のように一致する対象をすべて検索することもできる
            //System.Text.RegularExpressions.MatchCollection mc = r.Matches(TextBox1.Text);

            while (m.Success)
            {
                resb += m.Value.ToString();
                m = m.NextMatch();
            }

            res += resb;

            return res;
        }

        #endregion

        /// <summary>
        /// クリップボードにテキストを貼り付ける
        /// </summary>
        #region setDataToClipBord
        public static void setDataToClipBord(string str)
        {
            try
            {
                Clipboard.SetText(str);
            }
            catch
            {

            }
        }
        #endregion

        /// <summary>
        /// クリップボードのテキストを取得する
        /// </summary>
        #region setDataToClipBord
        public static string getDataFromClipBord()
        {
            try
            {
                return Clipboard.GetText();
            }
            catch
            {
                return "";
            }
        }
        #endregion

        /// <summary>
        /// md5の計算
        /// </summary>
        #region MD5Sum
        public static string MD5Sum(FileInfo file)
        {
            FileStream fs = new FileStream(file.FullName, FileMode.Open);
            string md5sum = BitConverter.ToString(MD5.Create().ComputeHash(fs)).ToLower().Replace("-", "");
            fs.Close();
            return md5sum;
        }
        public static string MD5Sum(string target)
        {
            //文字列をbyte型配列に変換する
            byte[] data = Encoding.UTF8.GetBytes(target);

            //MD5CryptoServiceProviderオブジェクトを作成
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //ハッシュ値を計算する
            byte[] bs = md5.ComputeHash(data);

            return BitConverter.ToString(bs).ToLower().Replace("-","");
        }
        #endregion

        /// <summary>
        /// ストリームからデータを読み込み、バイト配列に格納
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        #region ReadBinaryData
        public static byte[] readBinaryData(Stream st)
        {
            byte[] buf = new byte[32768]; // 一時バッファ

            using (MemoryStream ms = new MemoryStream())
            {

                while (true)
                {
                    // ストリームから一時バッファに読み込む
                    int read = st.Read(buf, 0, buf.Length);

                    if (read > 0)
                    {
                        // 一時バッファの内容をメモリ・ストリームに書き込む
                        ms.Write(buf, 0, read);
                    }
                    else
                    {
                        break;
                    }
                }
                // メモリ・ストリームの内容をバイト配列に格納
                return ms.ToArray();
            }
        }
        #endregion

        /// <summary>
        /// closeProcess
        /// プロセスを終了させる
        /// 使用不可
        /// </summary>
        #region closeProcess
        public static bool closeProcess(string targetPrgName)
        {
            bool close = false;
            Process[] ps = Process.GetProcessesByName(targetPrgName);

            foreach (System.Diagnostics.Process p in ps)
            {
                //クローズメッセージを送信する
                p.CloseMainWindow();
                close = true;

            }

            return close;
        }
        #endregion

        /// <summary>
        /// GetHwndFromPid
        /// プロセスID(pid)をウィンドウハンドル(hwnd)に変換する
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        #region GetHwndFromPid
        public static int GetHwndFromPid(int pid)
        {
            int hwnd;
            hwnd = LpsWindowsApi.FindWindow(null, null).ToInt32();
            while (hwnd != 0)
            {
                if (LpsWindowsApi.GetParent(hwnd) == 0 &&
                    LpsWindowsApi.IsWindowVisible(hwnd) != 0)
                {
                    if (pid == GetPidFromHwnd(hwnd))
                    {
                        return hwnd;
                    }
                }
                hwnd = LpsWindowsApi.GetWindow(hwnd, LpsWindowsApi.GW_HWNDNEXT);
            }
            return hwnd;
        }
        #endregion

        /// <summary>
        /// ウィンドウハンドル(hwnd)をプロセスID(pid)に変換する
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        #region GetPidFromHwnd
        public static int GetPidFromHwnd(int hwnd)
        {
            int pid;
            LpsWindowsApi.GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }
        #endregion

        /// <summary>
        /// バージョンを取得する
        /// </summary>
        /// <returns></returns>
        #region getVirsion
        public static string getVirsion()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion.ToString();
        }
        #endregion

        /// <summary>
        /// ピング送信
        /// </summary>
        /// <returns></returns>
        #region sendPing
        public static PingReply sendPing()
        {
            return sendPing("localhost");
        }
        public static PingReply sendPing(string target)
        {
            //Pingオブジェクトの作成
            using (Ping p = new Ping())
            {
                PingReply reply = p.Send(target);

                //結果を取得
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("Reply from {0}:bytes={1} time={2}ms TTL={3}", reply.Address, reply.Buffer.Length, reply.RoundtripTime, reply.Options.Ttl);
                }
                else
                {
                    Console.WriteLine("Ping送信に失敗。({0})", reply.Status);
                }

                return reply;
            }
        }
        public static int sendPingResponse(string target)
        {
            //Pingオブジェクトの作成
            using (Ping p = new Ping())
            {
                PingReply reply = p.Send(target);

                //結果を取得
                if (reply.Status == IPStatus.Success)
                {
                    return reply.Buffer.Length;
                }
                else
                {
                    return -1;
                }
            }
        }
        #endregion

        /// <summary>
        /// レスポンス時間を取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region checkResponseTime
        public static double checkResponseTime(string url)
        {
            string status = "";
            try
            {
                //ストップウォッチの設定
                Stopwatch sw = new Stopwatch();
                sw.Start();

                //WebRequestの作成
                HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(url);
                webreq.Timeout = 10000;

                HttpWebResponse webres = null;
                try
                {
                    //サーバーからの応答を受信するためのWebResponseを取得
                    webres = (HttpWebResponse)webreq.GetResponse();

                    //応答したURIを表示する
                    //Console.WriteLine(webres.ResponseUri);
                    //応答ステータスコードを表示する
                    status = webres.StatusCode + " " + webres.StatusDescription;
                }
                catch (System.Net.WebException ex)
                {
                    //HTTPプロトコルエラーかどうか調べる
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        //HttpWebResponseを取得
                        HttpWebResponse errres = (HttpWebResponse)ex.Response;
                        //応答したURIを表示する
                        Console.WriteLine(errres.ResponseUri);
                        //応答ステータスコードを表示する
                        status = errres.StatusCode + " " + errres.StatusDescription;

                        return -1;
                    }
                    else
                    {
                        status = ex.Message;

                        return -2;
                    }

                }
                finally
                {
                    //閉じる
                    if (webres != null)
                        webres.Close();
                }

                //ストップウィッチの停止
                sw.Stop();

                return (double)sw.ElapsedTicks / (double)Stopwatch.Frequency;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        /// <summary>
        /// ウインドウズのバッチファイルを起動する
        /// </summary>
        /// <param name="path"></param>
        #region doWindowsBat
        public static void doWindowsBat(string path)
        {
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = path;                 // 実行するファイル
            psInfo.CreateNoWindow = true;           // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false;         // シェル機能を使用しない

            Process.Start(psInfo);
        }
        #endregion

        /// <summary>
        /// ランダム数を取得
        /// </summary>
        /// <param name="max">最大値</param>
        /// <returns>ランダム値</returns>
        #region getRandamInt
        public static int getRandamInt(int start, int max)
        {
            Random r = new Random();
            return r.Next(max) + start;
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

        /// <summary>
        /// deepClone
        /// オブジェクトをディープコピーする
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        #region deepClone
        public static T deepClone<T>(T source)
        {
            object target = null;

            using (MemoryStream stream = new MemoryStream())
            {

                // コピー元オブジェクトをシリアライズします。

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, source);

                stream.Position = 0;

                // シリアライズデータをコピー先オブジェクトにデシリアライズします。
                target = formatter.Deserialize(stream);
            }


            return (T)target;
        }
        #endregion

        ///====================================================================
        ///
        ///                           グラフィックス
        ///                         
        ///====================================================================

        /// <summary>
        /// getInstallFontList
        /// インストールされているフォントのリストを返す
        /// </summary>
        /// <param name="stTarget"></param>
        /// <returns></returns>
        #region getInstallFontList
        public static List<string> getInstallFontList()
        {
            try
            {
                List<string> res = new List<string>();

                foreach (FontFamily ff in new InstalledFontCollection().Families)
                {
                    //ここではスタイルにRegularが使用できるフォントのみを表示
                    if (ff.IsStyleAvailable(FontStyle.Regular))
                    {
                        res.Add(ff.Name);
                    }
                }

                return res;
            }
            catch
            {
                return new List<string>();
            }
        }
        #endregion

        /// <summary>
        /// checkColor
        /// 入力された色情報が正しければその色を返す。
        /// </summary>
        /// <param name="stTarget"></param>
        /// <returns></returns>
        #region checkColor
        public static Color checkColor(string colorStr, Color defaultColor)
        {
            try
            {
                //デフォルトカラーの設定
                Color result = defaultColor;

                //サン値分解
                string[] rgb = colorStr.Split(',');

                //3値じゃなければエラー
                if (rgb.Length == 3) 
                {
                    try
                    {
                        //成功したら取得して返す
                        result = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));

                        return result;
                    }
                    catch
                    {
                        result = defaultColor;
                    }
                    
                }

                //色名チェック
                result = Color.FromName(colorStr);

                if (result.A == 0 && result.R == 0 && result.G == 0 && result.B == 0)
                {
                    result = defaultColor;
                }
                else
                {
                    return result;
                }

                //HTMLカラーチェック

                try
                {
                    result = ColorTranslator.FromHtml(colorStr);

                    return result;
                }
                catch
                {
                    result = defaultColor;
                }

                return result;
            }
            catch
            {
                return defaultColor;
            }
        }
        #endregion

        /// <summary>
        /// checkColor
        /// 入力された色情報が正しければその色を返す。
        /// </summary>
        /// <param name="stTarget"></param>
        /// <returns></returns>
        #region checkColor
        public static string getColorName(string colorStr, Color defaultColor)
        {
            try
            {
                //デフォルトカラーの設定
                Color result = defaultColor;
                string deafultColorStr = defaultColor.R + "," + defaultColor.G + "," + defaultColor.B;

                //サン値分解
                string[] rgb = colorStr.Split(',');

                //3値じゃなければエラー
                if (rgb.Length == 3)
                {
                    try
                    {
                        //成功したら取得して返す
                        result = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));

                        return colorStr;
                    }
                    catch
                    {
                        result = defaultColor;
                    }

                }

                //色名チェック
                result = Color.FromName(colorStr);

                if (result.A == 0 && result.R == 0 && result.G == 0 && result.B == 0)
                {
                    result = defaultColor;
                }
                else
                {
                    return result.R + "," + result.G + "," + result.B;
                }

                //HTMLカラーチェック

                try
                {
                    result = ColorTranslator.FromHtml("0x" + colorStr);

                    return result.R + "," + result.G + "," + result.B;
                }
                catch
                {
                    result = defaultColor;
                }

                return deafultColorStr;
            }
            catch
            {
                return Color.Black.R + "," + Color.Black.G + "," + Color.Black.B;
            }
        }
        #endregion

        /// <summary>
        /// convertColorCode
        /// 入力されたRGB値の色情報をカラーコードに変換する
        /// </summary>
        /// <param name="stTarget"></param>
        /// <returns></returns>
        #region convertColorCode
        public static string convertColorCode(string colorStr, string defaultColor)
        {
            try
            {
                //サン値分解
                string[] rgb = colorStr.Split(',');

                //3値じゃなければエラー
                if (rgb.Length == 3)
                {
                    try
                    {
                        return "#" + int.Parse(rgb[0]).ToString("X").PadLeft(2, '0') + int.Parse(rgb[1]).ToString("X").PadLeft(2, '0') + int.Parse(rgb[2]).ToString("X").PadLeft(2, '0');
                    }
                    catch
                    {
                        return defaultColor;
                    }

                }

                return defaultColor;
            }
            catch
            {
                return defaultColor;
            }
        }
        public static string convertColorCode(Color color, string defaultColor)
        {
            try
            {
                return "#" + color.R.ToString("X").PadLeft(2, '0') + color.G.ToString("X").PadLeft(2, '0') + color.B.ToString("X").PadLeft(2, '0');
            }
            catch
            {
                return defaultColor;
            }
        }
        #endregion

        /// <summary>
        /// 指定したファイルをロックせずに、System.Drawing.Imageを作成する。
        /// </summary>
        /// <param name="filename">作成元のファイルのパス</param>
        /// <returns>作成したSystem.Drawing.Image。</returns>
        #region createImage
        public static Image createImage(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return Image.FromStream(fs);

                //解像度を統一する場合は以下の処理を有効にし、型をBITMAPとする
                //Bitmap b = new Bitmap(Image.FromStream(fs));
                //解像度を96dpiに統一する
                //b.SetResolution(96.0F, 96.0F);
                //return b;
            }
        }
        #endregion

        /// <summary>
        /// 画像かどうかチェックする
        /// </summary>
        /// <param name="filename">イメージパス</param>
        /// <returns>画像かの可否</returns>
        #region isImage
        public static bool isImage(string imagePath)
        {
            try
            {
                Bitmap bitmap = new Bitmap(imagePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// reductionBitmap
        /// 画像を縮小する
        /// </summary>
        /// <returns>ビットマップ</returns>
        #region reductionBitmap
        public static Bitmap reductionBitmap(Bitmap source, int wid, int hi)
        {
            try
            {
                Bitmap canvas = new Bitmap(wid, hi);

                using (Bitmap image = source)
                {
                    using (Graphics g = Graphics.FromImage(canvas))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(image, 0, 0, wid, hi);
                    }
                }

                return canvas;
            }
            catch
            {
                return new Bitmap(wid, hi);
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                           SQL関連
        ///                         
        ///====================================================================

        // 基本的なエスケープ
        #region SafeSqlLiteral
        public static string SafeSqlLiteral(string inputSql)
        {
            return inputSql.Replace("'", "’");
        }
        #endregion
        
        // LIKE句を使用するする文字列については、
        // さらにいくつかの文字列エスケープが必要
        #region SafeSqlLikeClauseLiteral
        public static string SafeSqlLikeClauseLiteral(string inpurSql)
        {
            return inpurSql.Replace("'", "’")
            .Replace("[", "[[]")
            .Replace("%", "[%]")
            .Replace("_", "[_]");
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
            return MessageBox.Show(msg, LpsDefineMost.MESSAGE_TITLE, MessageBoxButtons.OKCancel) == DialogResult.OK;
        }
        #endregion

        /// <summary>
        /// LiplisMessageOkOnly
        /// OKダイアログを出す
        /// タイトルは固定、メッセージのみ指定可能
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        #region LiplisMessageOkOnly
        public static void LiplisMessageOkOnly(string msg)
        {
            MessageBox.Show(msg, LpsDefineMost.MESSAGE_TITLE, MessageBoxButtons.OK);
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

    }
}
