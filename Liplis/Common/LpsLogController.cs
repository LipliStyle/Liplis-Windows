//=======================================================================
//  ClassName : LiplisLog
//  概要      : ログクラス
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Liplis.Common
{
    public class LpsLogControllerCus : LpsLogController
    {
        //
        string logFilePath;
        //string logStr;
        Encoding enc;
        
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        #region LiplisLog
        public LpsLogControllerCus()
        {
            //ログファイルパスの取得
            logFilePath = getLogPath();

            //ログエンコーディングの設定
            enc = Encoding.GetEncoding(932);
        }
        #endregion

        /// <summary>
        /// writingLog
        /// 引数で指定された内容をログに書き込む
        /// </summary>
        /// <param name="body">書き込み内容</param>
        #region writingLog
        public static void writingLog(string className, string methodName, string body)
        {
            string logStr = "[INFO ] " + DateTime.Now + " " + className + " " + methodName + ":" + body + Environment.NewLine;

            try { System.IO.File.AppendAllText(getAppPath_(), logStr, Encoding.GetEncoding(932)); }
            catch (System.ComponentModel.Win32Exception)
            {
                d("ログ書き込みエラー");
            }
            catch { }
            
        }
        #endregion

        /// <summary>
        /// writingLog
        /// 引数で指定された内容をログに書き込む
        /// </summary>
        /// <param name="body">書き込み内容</param>
        #region writingTestLog
        public static void writingTestLog(string body)
        {
            string logStr = body + Environment.NewLine;

            try { System.IO.File.AppendAllText(getTestLogPath(), logStr, Encoding.GetEncoding(932)); }
            catch (System.ComponentModel.Win32Exception)
            {
                d("ログ書き込みエラー");
            }
            catch { }

        }
        #endregion

        /// テストログファイルパスを返す
        /// </summary>
        /// <returns>ログファイルパス</returns>
        #region getTestLogPath
        public static string getTestLogPath()
        {
            try
            {
                checkDir_(getAppPath_() + "\\log");
                return getAppPath_() + "\\log\\liplist.log";
            }
            catch (System.Exception err)
            {
                d(MethodBase.GetCurrentMethod().Name + "PathController : getLogPath\n" + err);
                return "";
            }
        }
        #endregion

        /// <summary>
        /// d
        /// メッセージをダンプする
        /// </summary>
        /// <param name="msg">ダンプメッセージ</param>
        #region d
        public static void d(string msg)
        {
            Console.WriteLine(msg);
        }
        #endregion

        /// <summary>
        /// ディレクトリの存在チェックを行い、無かったら生成する
        /// </summary>
        /// <param name="path"></param>
        public static void checkDir_(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch
            {
                //MessageBox.Show("ファイルの書き込みが許可されているか確認して下さい。\nLiplisをインストールし直すと解決する可能性もあります。","Liplis");
                Application.Exit();
            }
        }


        /// <summary>
        /// アプリケーションの起動パスを返す
        /// </summary>
        /// <returns></returns>
        public static string getAppPath_()
        {
            return System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
    }
}