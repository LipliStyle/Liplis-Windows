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
    public class LiplisLog
    {
        //
        string logFilePath;
        string logStr;
        Encoding enc;


        /// <summary>
        /// コンストラクター
        /// </summary>
        #region LiplisLog
        public LiplisLog()
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

            try { System.IO.File.AppendAllText(getLogPath(), logStr, Encoding.GetEncoding(932)); }
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
        #region writingLog
        public static void writingTestLog(string className, string methodName, string body)
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

        /// <summary>
        /// 引数で指定された内容をログに書き込む
        /// </summary>
        /// <param name="errBody"></param>
        #region callErrMsg
        public void callErrMsg(System.Exception e)
        {
            //ログ文の作成
            logStr = "[ERROR] " + DateTime.Now + " " + e.ToString() + "\r\n";

            //メッセージボックス
            MessageBox.Show(e.ToString(),"Liplis");

            //ログ書込
            try { System.IO.File.AppendAllText(logFilePath, logStr, enc); }
            catch { }
        }
        #endregion

        /// <summary>
        /// エラーメッセージを表示する
        /// </summary>
        /// <param name="msg">エラーメッセージ</param>
        #region callErrMsg
        public void callErrMsg(string msg)
        {
            //ログ文の作成
            logStr = "[ERROR] " + DateTime.Now + " " + msg + "\r\n";

            //メッセージボックス
            MessageBox.Show(msg, "Liplis");

            //ログ書込
            try { System.IO.File.AppendAllText(logFilePath, logStr, enc); }
            catch { }
        }
        #endregion

        /// <summary>
        /// 引数で指定された内容をメッセージボックスに表示する
        /// </summary>
        /// <param name="errBody">エラーメッセージ</param>
        #region openDialog
        public void openDialog(string errBody)
        {
            //メッセージボックス
            MessageBox.Show(errBody,"Liplis");

        }
        #endregion

        /// <summary>
        /// ログファイルパスを返す
        /// </summary>
        /// <returns>ログファイルパス</returns>
        #region getLogPath
        public static string getLogPath()
        {
            try
            {
                checkDir(getAppPath() + "\\log");
                return getAppPath() + "\\log\\liplis.log";
            }
            catch (System.Exception err)
            {
                d(MethodBase.GetCurrentMethod().Name + "PathController : getLogPath\n" + err);
                return "";
            }
        }
        #endregion

        /// <summary>
        /// テストログファイルパスを返す
        /// </summary>
        /// <returns>ログファイルパス</returns>
        #region getTestLogPath
        public static string getTestLogPath()
        {
            try
            {
                checkDir(getAppPath() + "\\log");
                return getAppPath() + "\\log\\liplist.log";
            }
            catch (System.Exception err)
            {
                d(MethodBase.GetCurrentMethod().Name + "PathController : getLogPath\n" + err);
                return "";
            }
        }
        #endregion

        /// <summary>
        /// ディレクトリの存在チェックを行い、無かったら生成する
        /// </summary>
        /// <param name="path">チェック対象パス</param>
        #region checkDir
        public static void checkDir(string path)
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
        #endregion

        /// <summary>
        /// アプリケーションの起動パスを返す
        /// </summary>
        /// <returns>起動パス</returns>
        #region getAppPath 
        public static string getAppPath()
        {
            return System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
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
        
    }
}