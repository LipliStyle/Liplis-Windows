//=======================================================================
//  ClassName : LogController
//  概要      : ログファイルを作ったり出力したり
//
//  Tips      :System.IO.File.AppendAllTextメソッドは、ファイルを開き、テキストを追加し、閉じてくれる。
//             エラー発生時でも、ファイルが閉じられることが保証されている。
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Liplis.Common
{
    public class ComLogController
    {
        //
        string logFilePath;
        string logStr;
        Encoding enc;


        /// <summary>
        /// コンストラクター
        /// </summary>
        public ComLogController()
        {
            //ログファイルパスの取得
            logFilePath = getLogPath();

            //ログエンコーディングの設定
            enc = Encoding.GetEncoding(932);
        }

        /// <summary>
        /// writingLog
        /// 引数で指定された内容をログに書き込む
        /// </summary>
        /// <param name="body">書き込み内容</param>
        public void writingLog(string body)
        {
            logStr = "[INFO ] " + DateTime.Now + body + "\n";

            try { System.IO.File.AppendAllText(logFilePath, logStr, enc); }
            catch (System.ComponentModel.Win32Exception)
            {
                Application.Exit();
            }
            catch { }

        }


        /// <summary>
        /// 引数で指定された内容をログに書き込む
        /// </summary>
        /// <param name="errBody"></param>
        public void callErrMsg(System.Exception e)
        {
            //ログ文の作成
            logStr = "[ERROR] " + DateTime.Now + " " + e.ToString() + "\r\n";

            //メッセージボックス
            MessageBox.Show(e.ToString(), "Liplis");

            //ログ書込
            try { System.IO.File.AppendAllText(logFilePath, logStr, enc); }
            catch { }
        }
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

        /// <summary>
        /// 引数で指定された内容をログに書き込む
        /// </summary>
        /// <param name="errBody"></param>
        public void openDialog(string errBody)
        {
            //メッセージボックス
            MessageBox.Show(errBody, "Liplis");

        }

        //ログファイルパスを返す
        public string getLogPath()
        {
            try
            {
                checkDir(getAppPath() + "\\log");
                return getAppPath() + "\\log\\liplis.log";
            }
            catch (System.Exception err)
            {
                writingLog("PathController : getLogPath\n" + err);
                return "";
            }
        }

        /// <summary>
        /// ディレクトリの存在チェックを行い、無かったら生成する
        /// </summary>
        /// <param name="path"></param>
        public void checkDir(string path)
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
        public string getAppPath()
        {
            return System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

    }
}
