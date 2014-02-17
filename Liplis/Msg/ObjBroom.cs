//=======================================================================
//  ClassName : ObjBroom
//  概要      : ほうきオブジェクト
//
//  LiplisSystem   
//  Copyright(c) 2010-2011 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Diagnostics;
using Liplis.Common;

namespace Liplis.Msg
{
    public class ObjBroom
    {
        ///=====================================
        /// パス
        private string broomPath = "";

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ObjBroom
        public ObjBroom()
        {
            //ブルームパスの定義
            broomPath = LpsPathControllerCus.getBinPath() + LiplisDefine.LIPLIS_TOOL_BROOM;
        }
        #endregion

        ///====================================================================
        ///
        ///                          実行メソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// すべてのファイルを削除する
        /// </summary>
        /// <param name="command"></param>
        #region deleteAllTempFile
        public void deleteAllTempFile()
        {
            callBroom("/a", LpsPathControllerCus.getTempPath2());
            callBroom("/a", LpsPathControllerCus.getAppPath() + "\\log");
        }
        #endregion

        /// <summary>
        /// すべてのファイルを削除する
        /// </summary>
        /// <param name="command"></param>
        #region deleteTargetFile
        public void deleteTargetFile(string filePath)
        {
            callBroom("/o", filePath);
        }
        #endregion


        ///====================================================================
        ///
        ///                          ほうき処理
        ///                         
        ///====================================================================

        /// <summary>
        /// ほうきをコールする
        /// </summary>
        #region callBroom
        private void callBroom(string pCom, string pOp)
        {
            Process pCaChuSha;
            ProcessStartInfo pCaChuShaInfo;
            pCaChuShaInfo = new ProcessStartInfo();
            pCaChuShaInfo.FileName = broomPath;
            pCaChuShaInfo.Arguments = pCom + " \"" + pOp + "\"";
            pCaChuShaInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;     //非表示

            pCaChuSha = new Process();
            pCaChuSha.Exited += new EventHandler(ExitedLeafCaChuShaFileWriter);     //イベントの登録
            pCaChuSha.EnableRaisingEvents = true;                                   //プロセスが終了したときに Exited イベントを発生させる
            pCaChuSha.StartInfo = pCaChuShaInfo;
            pCaChuSha.Start();

        }

        private void ExitedLeafCaChuShaFileWriter(object sender, EventArgs e)
        {
            Process process = (Process)sender;
            int idx = process.ExitCode;

            //プロセスを破棄しておく
            process.Dispose();
        }

        #endregion

    }
}
