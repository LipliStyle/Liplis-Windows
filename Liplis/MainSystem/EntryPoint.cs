//=======================================================================
//  ClassName : Main
//  概要      : エントリーポイント
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace Liplis.MainSystem
{
    static class EntryPoint
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //起動チェック
                //LiplisUpdate.waitUpdate();

                //バージョンチェック
                //if (LiplisUpdate.update())
                //{
                //    return;
                //}

            }
            catch
            {

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Liplis());
        }
    }

}
