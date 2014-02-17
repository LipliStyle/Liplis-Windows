//=======================================================================
//  ClassName : LpsBaseMovieConverter
//  概要      : MP3変換
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Diagnostics;

namespace Liplis.Common
{
    public class LpsBaseMovieConverter
    {
        protected delegate void dlgVoidToStr(string command);
        dlgVoidToStr dlgDoProcess;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LpsBaseMovieConverter()
        {
            dlgDoProcess = new dlgVoidToStr(doProcess);
        }

        /// <summary>
        /// FLVからMP3に変換する
        /// </summary>
        /// <returns></returns>
        public void convertFlvToMP3(string flvFilePath, string mp3savePath)
        {
            convertFlvToMP3(flvFilePath, mp3savePath, "128k");
        }
        public void convertFlvToMP3High(string flvFilePath, string mp3savePath)
        {
            convertFlvToMP3(flvFilePath, mp3savePath, "312k");
        }
        public void convertFlvToMP3(string flvFilePath, string mp3savePath, string rate)
        {
            string command = "";
            command =  "\"-y\" \"-i\" \"" + flvFilePath + "\" \"-ab\" \"" + rate + "\" \"-ar\" \"48000\" \"-ac\" \"2\" \"" + mp3savePath + "\""; 

            dlgDoProcess.BeginInvoke(command, null, null);
        }

        /// <summary>
        /// FLVからMP4に変換する
        /// </summary>
        /// <returns></returns>
        public void convertFlvToMP4(string flvFilePath, string mp3savePath)
        {
            string command = "";
            dlgDoProcess.BeginInvoke(command, null, null);
        }


        /// <summary>
        /// プロセス起動のスレッド
        /// </summary>
        #region doProcess
        private void doProcess(string command)
        {
            try
            {
                ProcessStartInfo pi = new ProcessStartInfo();
                pi.FileName = LpsPathController.getBinPath() + LpsDefineMost.EXE_FFMPEG;
                pi.WindowStyle = ProcessWindowStyle.Hidden;
                pi.Arguments = command;
                Process.Start(pi);
            }
            catch (System.ComponentModel.Win32Exception fileNotFoundErr)
            {
                Console.Write(fileNotFoundErr);
            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
        }
        #endregion
    }
}
