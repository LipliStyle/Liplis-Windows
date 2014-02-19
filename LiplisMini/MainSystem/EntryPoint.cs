//=======================================================================
//  ClassName : Main
//  概要      : エントリーポイント
//
//  Liplis3.0.1
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Windows.Forms;

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

            }
            catch
            {

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LiplisMini());
        }
    }

}
