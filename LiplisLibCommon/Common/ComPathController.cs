//=======================================================================
//  ClassName : PathController
//  概要      : アプリケーション階層以下のパスを返す
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================

using System;
using System.IO;
using System.Collections.Generic;
using Ionic.Zip;
using Ionic.Zlib;
using System.Text;

namespace Liplis.Common
{
    public class ComPathController
    {
        ///=====================================
        /// クラス
        ComLogController lc;

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ComPathController
        public ComPathController(string cacheFilePath)
        {
            lc = new ComLogController();
        }
        #endregion

        /// <summary>
        /// セッティングパスを返す
        /// </summary>
        #region getSettingPath
        public static string getSettingPath()
        {
            ComLogController lc = new ComLogController();
            try
            {
                checkDir(getAppPath() + "\\setting");
                return getAppPath() + "\\setting\\";
            }
            catch (System.Exception err)
            {
                lc.writingLog("PathController : getSettingPath\n" + err);
                return "";
            }
        }
        #endregion

        /// <summary>
        /// skinパスを返す
        /// </summary>
        #region getSkinPath
        public static string getSkinPath()
        {
            ComLogController lc = new ComLogController();
            try
            {
                checkDir(getAppPath() + "\\skin");
                return getAppPath() + "\\skin\\";
            }
            catch (System.Exception err)
            {
                lc.writingLog("PathController : getSkinPath\n" + err);
                return "";
            }
        }
        #endregion

        /// <summary>
        /// skinSettingsパスを返す
        /// </summary>
        #region getSkinSettingsPath
        public static string getSkinSettingsPath()
        {
            ComLogController lc = new ComLogController();
            try
            {
                checkDir(getAppPath() + "\\skinSettings");
                return getAppPath() + "\\skinSettings\\";
            }
            catch (System.Exception err)
            {
                lc.writingLog("PathController : getSkinSettingsPath\n" + err);
                return "";
            }
        }
        #endregion

        /// <summary>
        /// xmlファイルパスを返す
        /// </summary>
        #region getXmlPath
        public static string getXmlPath()
        {
            ComLogController lc = new ComLogController();
            try
            {
                checkDir(getAppPath() + "\\xml");
                return getAppPath() + "\\xml\\";
            }
            catch (System.Exception err)
            {
                lc.writingLog("PathController : getXmlPath\n" + err);
                return "";
            }
        }
        #endregion


        /// <summary>
        /// javascriptファイルパスを返す
        /// </summary>
        #region getJavascriptPath
        public string getJavascriptPath()
        {
            lc = new ComLogController();
            try
            {
                checkDir(getAppPath() + "\\js");
                return getAppPath() + "\\js\\";
            }
            catch (System.Exception err)
            {
                lc.writingLog("PathController : getJavascriptPath\n" + err);
                return "";
            }
        }
        #endregion

        /// <summary>
        /// ログファイルパスを返す
        /// </summary>
        #region getLogPath
        public string getLogPath()
        {
            lc = new ComLogController();
            try
            {
                checkDir(getAppPath() + "\\log");
                return getAppPath() + "\\log\\liplis.log";
            }
            catch (System.Exception err)
            {
                lc.writingLog("PathController : getLogPath\n" + err);
                return "";
            }
        }
        #endregion

        /// <summary>
        /// binパスを返す
        /// </summary>
        #region getBinPath
        public static string getBinPath()
        {
            ComLogController lc = new ComLogController();
            try
            {
                checkDir(getAppPath() + "\\bin");
                return getAppPath() + "\\bin\\";
            }
            catch (System.Exception err)
            {
                lc.writingLog("PathController : getBinPath\n" + err);
                return "";
            }
        }
        #endregion

        /// <summary>
        /// エモーションDBのパスを返す
        /// </summary>
        #region getDownPath
        public static string getDownPath()
        {
            ComLogController lc = new ComLogController();
            try
            {
                checkDir(getAppPath() + "\\down");
                return getAppPath() + "\\down\\";
            }
            catch (System.Exception err)
            {
                lc.writingLog("PathController : getBinPath\n" + err);
                return "";
            }
        }
        #endregion

        /// <summary>
        /// アプリケーションの起動パスを返す
        /// </summary>
        /// <returns></returns>
        #region getAppPath
        public static string getAppPath()
        {
            return System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
        }
        #endregion
        

        /// <summary>
        /// ディレクトリの存在チェックを行い、無かったら生成する
        /// </summary>
        /// <param name="path"></param>
        public static bool checkDir(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ファイル存在チェック
        /// </summary>
        /// <returns></returns>
        public static bool checkFileExist(string path)
        {
            return System.IO.File.Exists(path);
        }

        /// <summary>
        /// ファイルサイズを返す。
        /// </summary>
        /// <returns></returns>
        public static long getFileSize(string filePath)
        {
            if (checkFileExist(filePath))
            {
                FileInfo fi = new System.IO.FileInfo(filePath);
                return fi.Length;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// ファイルを消去する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool delteFile(string filePath)
        {
            if (checkFileExist(filePath))
            {
                try
                {
                    FileInfo fi = new System.IO.FileInfo(filePath);
                    fi.Delete();
                }
                catch
                {
                    ComLogController lc = new ComLogController();
                    lc.writingLog("PathController : delteFile\nファイルの削除に失敗しました。");
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ディクトリを消去する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool delteDir(string filePath)
        {
            if (checkFileExist(filePath))
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(filePath);
                    di.Delete(true);;
                }
                catch
                {
                    ComLogController lc = new ComLogController();
                    lc.writingLog("PathController : delteFile\nファイルの削除に失敗しました。");
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ディレクトリ作成を試してみる
        /// </summary>
        /// <returns></returns>
        public static bool tryCreateDir(string path)
        {
            try
            {
                System.IO.Directory.CreateDirectory(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ファイルリストを取得する
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="searchPattern"></param>
        /// <param name="result"></param>
        public static void getFileList(string folder, string searchPattern, ref List<string> result)
        {
            //folderにあるファイルを取得する
            string[] fs = Directory.GetFiles(folder, searchPattern);
            //ArrayListに追加する
            result.AddRange(fs);

            //folderのサブフォルダを取得する
            string[] ds = Directory.GetDirectories(folder);
            //サブフォルダにあるファイルも調べる
            foreach (string d in ds)
            {
                System.Windows.Forms.Application.DoEvents();
                getFileList(d, searchPattern, ref result);
            }

        }

        /// <summary>
        /// 古いテンプファイルを削除する
        /// 設定時間以上アクセスの無いファイルを削除する
        /// </summary>
        public static void deleteOldTempFile(string tempDirPath, DateTime dt)
        {
            //ファイルリスト
            List<string> tempFileList = new List<string>();
            //テンプファイルリストを取得
            getFileList(tempDirPath, "*", ref tempFileList);

            try
            {
                //作成時刻と照らし合わせ、基準以上であれば削除する
                foreach (string path in tempFileList)
                {
                    System.Windows.Forms.Application.DoEvents();
                    FileInfo fi = new System.IO.FileInfo(path);

                    //ファイルの作成日が基準以前であれば削除する
                    if (fi.CreationTime < dt)
                    {
                        fi.Delete();
                    }
                }
            }
            catch
            {
                ComLogController lc = new ComLogController();
                lc.writingLog("PathController : delteFile\n一部ファイルの削除に失敗しました。");
            }
        }

        /// <summary>
        /// doArchive
        /// アーカイブする
        /// </summary>
        /// <param name="filePath"></param>
        #region doArchive
        public static void doArchive(string filePath, string savePath)
        {
              try
              {
                //(1)ZIPクラスをインスタンス化
                using (ZipFile zip = new ZipFile(Encoding.GetEncoding(ComDefineMost.ENCODING_SJIS)))
                {
                  //(2)圧縮レベルを設定
                  zip.CompressionLevel = CompressionLevel.BestCompression;

                  //(3)ファイルを追加
                  zip.AddFile(filePath);

                  //(5)ZIPファイルを保存
                  zip.Save(savePath);
                }
              }
              catch
              {
                ComLogController lc = new ComLogController();
                lc.writingLog("PathController : doArchive\n一部ファイルの削除に失敗しました。");
              }
        }
        public static void doArchive(List<string> fileList, string savePath)
        {
            try
            {
                //(1)ZIPクラスをインスタンス化
                using (ZipFile zip = new ZipFile(Encoding.GetEncoding(ComDefineMost.ENCODING_SJIS)))
                {
                    //(2)圧縮レベルを設定
                    zip.CompressionLevel = CompressionLevel.BestCompression;

                    foreach (string filePath in fileList)
                    {
                        //(3)ファイルを追加
                        zip.AddFile(filePath);
                    }
                    
                    //(5)ZIPファイルを保存
                    zip.Save(savePath);
                }
            }
            catch
            {
                ComLogController lc = new ComLogController();
                lc.writingLog("PathController : doArchive\n一部ファイルの削除に失敗しました。");
            }
        }
        #endregion




    }
}
