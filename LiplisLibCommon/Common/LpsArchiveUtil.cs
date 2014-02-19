//=======================================================================
//  ClassName : LpsArchiveUtil
//  概要      : アーカイブユーティル
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin.Sachin
//=======================================================================
using Ionic.Zip;
using Ionic.Zlib;
using System.Text;
using System.Collections.Generic;

namespace Liplis.Common
{
    public class LpsArchiveUtil
    {
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
                using (ZipFile zip = new ZipFile(Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
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
                LpsLogController lc = new LpsLogController();
                lc.writingLog("PathController : doArchive\n一部ファイルの削除に失敗しました。");
            }
        }
        public static void doArchive(List<string> fileList, string savePath)
        {
            try
            {
                //(1)ZIPクラスをインスタンス化
                using (ZipFile zip = new ZipFile(Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
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
                LpsLogController lc = new LpsLogController();
                lc.writingLog("PathController : doArchive\n一部ファイルの削除に失敗しました。");
            }
        }
        #endregion

        /// <summary>
        /// doDefrost
        /// 解凍する
        /// </summary>
        /// <param name="filePath"></param>
        #region doDefrost
        public static bool doDefrost(string filePath, string savePath)
        {
            try
            {
                //(1)ZIPファイルを読み込み
                using (ZipFile zip = ZipFile.Read(filePath))
                {
                    //(2)解凍時に既にファイルがあったら上書きする設定
                    zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                    //(3)全て解凍する
                    zip.ExtractAll(savePath);
                }

                return true;
            }
            catch
            {
                LpsLogController lc = new LpsLogController();
                lc.writingLog("PathController : doArchive\n一部ファイルの削除に失敗しました。");

                return false;
            }
        }
        #endregion
    }
}
