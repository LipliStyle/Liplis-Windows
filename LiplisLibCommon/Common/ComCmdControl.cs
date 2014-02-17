//=======================================================================
//  ClassName : ComCmdControl
//  概要      : コマンドコントロール
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Liplis.Common
{
    public static class ComCmdControl
    {
        public static void doCommandN(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo();

            //ComSpecのパスを取得する
            psi.FileName = Environment.GetEnvironmentVariable("ComSpec");

            //出力を読み取れるようにする
            psi.RedirectStandardInput = false;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            //ウィンドウを表示しないようにする
            psi.CreateNoWindow = true;
            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            psi.Arguments = @"/c dir c:\ /w";
            //起動
            Process p = Process.Start(psi);
            //出力を読み取る
            string results = p.StandardOutput.ReadToEnd();
            //WaitForExitはReadToEndの後である必要がある
            //(親プロセス、子プロセスでブロック防止のため)
            p.WaitForExit();

            //出力された結果を表示
            Console.WriteLine(results);

        }

        public static string doCommand(string command)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();

                //ComSpecのパスを取得する
                psi.FileName = Environment.GetEnvironmentVariable("ComSpec");

                //出力を読み取れるようにする
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                //ウィンドウを表示しないようにする
                psi.CreateNoWindow = true;
                //コマンドラインを指定（"/c"は実行後閉じるために必要）
                psi.Arguments = command;
                //起動
                Process p = Process.Start(psi);
                //出力を読み取る
                string results = p.StandardOutput.ReadToEnd();
                //WaitForExitはReadToEndの後である必要がある
                //(親プロセス、子プロセスでブロック防止のため)
                p.WaitForExit();

                //出力された結果を表示
                return results;
            }
            catch
            {
                return "";
            }
        }

        public static string doCommand(List<string> commandList)
        {
            try
            {
                string results = "";
                ProcessStartInfo psi = new ProcessStartInfo();

                //ComSpecのパスを取得する
                psi.FileName = Environment.GetEnvironmentVariable("ComSpec");

                //出力を読み取れるようにする
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                //ウィンドウを表示しないようにする
                //psi.CreateNoWindow = true;

                foreach (string command in commandList)
                {
                    //コマンドラインを指定（"/c"は実行後閉じるために必要）
                    psi.Arguments = command;
                    //起動
                    Process p = Process.Start(psi);
                    //出力を読み取る
                    results += p.StandardOutput.ReadToEnd();
                    //WaitForExitはReadToEndの後である必要がある
                    //(親プロセス、子プロセスでブロック防止のため)
                    p.WaitForExit();
                }


                //出力された結果を表示
                return results;
            }
            catch
            {
                return "";
            }
        }
    }
}
