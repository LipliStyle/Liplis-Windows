//=======================================================================
//  ClassName : LiplisWedFileDownLoadercs
//  概要      : ウェブファイルダウンローダー
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================

namespace Liplis.Common
{
    public class LiplisUpdaterDefine
    {
        ///=============================
        /// URL
        public const string URL_VERSION = "http://liplis.mine.nu/LiplisWinUpd/4x/version.xml";
        public const string URL_ROOT = "http://liplis.mine.nu/LiplisWinUpd/4x/";

        ///=============================
        /// DEBUG
        public const string URL_VERSION_TEST = "http://liplis.mine.nu/LiplisWinUpd/4xTest/version.xml";
        public const string URL_ROOT_TEST = "http://liplis.mine.nu/LiplisWinUpd/4xTest/";

        /// <summary>
        /// モードに応じてURLバージョンを取得する
        /// </summary>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static string getUrlVersion(bool debug)
        {
            if (!debug)
            {
                return URL_VERSION;
            }
            else
            {
                return URL_VERSION_TEST;
            }
        }


        /// <summary>
        /// モードに応じてURLルートを取得する
        /// </summary>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static string getUrlRoot(bool debug)
        {
            if (!debug)
            {
                return URL_ROOT;
            }
            else
            {
                return URL_ROOT_TEST;
            }
        }
    }
}
