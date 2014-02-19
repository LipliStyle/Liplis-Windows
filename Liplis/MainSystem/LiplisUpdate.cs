//=======================================================================
//  ClassName : LiplisUpdate
//  概要      : リプリスをアップデートする
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liplis.Msg;
using Liplis.Web;
using Liplis.Common;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;

namespace Liplis.MainSystem
{
    public class LiplisUpdate
    {
        ///=============================
        ///新ファイル名
        private string newFilePath { get; set; }

        /// <summary>
        /// update
        /// リプリスをアップデートする
        /// </summary>
        #region update
        public static bool update()
        {
            if (versionCheck())
            {
                //最新版のダウンロード
                //updateLiplis();

                //Process.Start("Liplis.exe", "/up " + Process.GetCurrentProcess().Id);
                //Application.Exit();
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// アップデートを待つ
        /// </summary>
        #region waitUpdate
        public static void waitUpdate()
        {
            if (Environment.CommandLine.IndexOf("/up", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                try
                {
                    string[] args = Environment.GetCommandLineArgs();
                    int pid = Convert.ToInt32(args[2]);
                    Process.GetProcessById(pid).WaitForExit();    // 終了待ち
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion

        /// <summary>
        /// updateLiplis
        /// リプリスをアップデートする
        /// </summary>
        #region updateLiplis
        public static void updateLiplis()
        {
            //ダウンロードに成功したらexeを入れ替える
            if (downLoadNewLiplis())
            {
                File.Delete("temp\\Liplis.old");
                File.Move("Liplis.exe", "temp\\Liplis.old");
                File.Move("temp\\Liplis.lps", "Liplis.exe");

            }
        }
        #endregion

        /// <summary>
        /// versionCheck
        /// バージョンファイルをダウンロードし、バージョンを比較する
        /// true:バージョンアップあり
        /// false:なし
        /// </summary>
        #region updateLiplis
        private static bool versionCheck()
        {
            //ヴァージョンファイル読み込み
            ObjVersion ovNew = new ObjVersion(LiplisDefine.LIPLIS_NEW_XML);

            return !Assembly.GetExecutingAssembly().GetName().Version.ToString().Equals(ovNew.version);

        }
        #endregion

        /// <summary>
        /// downLoadNewLiplis
        /// 最新のLiplisをダウンロードする
        /// </summary>
        #region downLoadNewLiplis
        private static bool downLoadNewLiplis()
        {
            try
            {
                LiplisWedFileDownLoader.downLoad(LiplisDefine.LIPLIS_NEW_EXE, LpsPathControllerCus.getTempPath() + LiplisDefine.LIPLIS_NEW_EXE_FILE);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        #endregion





    }
}
