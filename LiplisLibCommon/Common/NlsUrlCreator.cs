//=======================================================================
//  ClassName : NlsUrlCreator
//  概要      : ノラリス関連のURLを作成する。WEBとクライアントの共有クラス
//
//  Liplis2.0
//  Copyright(c) 2010-2012 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Common
{
    public static class NlsUrlCreator
    {
        /// <summary>
        /// APKのURLを取得する
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        #region getApkUrl
        public static string getApkUrl(string packageName, string appName)
        {
            return "http://liplis.mine.nu/FileSystem/Noralis/Apk/" + packageName + "/" + appName + ".apk";
        }
        #endregion

        /// <summary>
        /// APKのURLを取得する
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        #region getApkQrUrl
        public static string getApkQrUrl(string packageName)
        {
            return "http://liplis.mine.nu/FileSystem/Noralis/Apk/" + packageName + "/qr.png";
        }
        #endregion

        /// <summary>
        /// TONEのURLを取得する
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        #region getToneUrl
        public static string getToneUrl(string packageName)
        {
            return "http://liplis.mine.nu/FileSystem/Xml/Tone/" + packageName + ".xml";
        }
        #endregion

        /// <summary>
        /// WindowsSkinURLを取得する
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        #region getWindowsSkinUrl
        public static string getWindowsSkinUrl(string packageName)
        {
            return "http://liplis.mine.nu/FileSystem/Noralis/Rt/" + packageName;
        }
        #endregion

        /// <summary>
        /// WindowsLiplisWebURLを取得する
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        #region getWindowsJsUrl
        public static string getWindowsLiplisWebUrl(string packageName)
        {
            return "http://liplis.mine.nu/FileSystem/Noralis/Js/" + packageName + "/liplisWeb.html";
        }
        #endregion


    }
}
