//=======================================================================
//  ClassName : LpsVoiceRoid
//  概要      : ボイスロイドAPI
//  このクラスにうえさんの「民安★TALK」のソースをMITライセンスにもとづき、
//  使用させて頂いております。
//
//  民安★TALK
//  http://uep.s321.xrea.com/vrx/
//
//  2013/06/23 Liplis3.0.2 待機スレッドでスリープし、負荷低減
//                         読み上げメソッド内のストップ処理のタイミングを変更
//  Liplisシステム      
//  Copyright(c) 2010-2014 LipliStyle Sachin
//=======================================================================
using Liplis.Common;
using Liplis.Msg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Liplis.Voice
{
    public class LpsVoiceRoid150 : LpsVoiceRoid
    {
        ///====================================================================
        ///
        ///                           初期化処理
        ///                         
        ///====================================================================
        #region 初期化処理
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="option"></param>
        public LpsVoiceRoid150(msgVoiceRoid option):base()
        {
            this.setting = option;
            this.initThread();

            //空以外の場合にスレッド起動
            if (option.sWindowTitle == "")
            {
                flgNotFound = true;
            }
            else
            {

            }
        }
   
        /// <summary>
        /// デストラクター
        /// </summary>
        ~LpsVoiceRoid150()
        {
            if (this.thread != null)
            {
                this.stopThread();
            }
        }

        /// <summary>
        /// ディスポーズ
        /// </summary>
        public override void Dispose()
        {
            if (this.thread != null)
            {
                this.stopThread();
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                        パブリックメソッド
        ///                         
        ///====================================================================
        #region パブリックメソッド

        /// <summary>
        /// メッセージを追加する
        /// </summary>
        /// <param name="msg"></param>
        public override void addMessage(string msg)
        {
            if (flgNotFound)
            {
                this.stopThread();
                return;
            }

            //2013/06/24 ver3.0.2
            //うまくしゃべらないため、ストップボタンコールのタイミングをずらす
            callStopButtonDown();
            Thread.Sleep(100);

            if (!this.thread.IsAlive)
            {
                this.startThread();
            }
            this.lstMessage.Add(msg);
            if (this.findVoiceroidWindow(this.setting, true))
            {
                searchErrorWindow();
                Thread.Sleep(TRY_INTERVAL);
            }
            LpsWindowsApi.SetEvent(LpsVoiceRoid150.hEvent);
        }
        #endregion

        ///====================================================================
        ///
        ///                            一般処理
        ///                         
        ///====================================================================
        #region 一般処理
        /// <summary>
        /// メッセージをクリアする
        /// </summary>
        protected override void clearMessage()
        {
            this.lstMessage.Clear();
            LpsWindowsApi.SetEvent(LpsVoiceRoid150.hEvent);
        }
        #endregion

        ///====================================================================
        ///
        ///                           スレッド操作
        ///                         
        ///====================================================================
        #region スレッド操作
        /// <summary>
        /// スレッド開始
        /// </summary>
        protected override void startThread()
        {
            this.thread.Start();
        }
 
        /// <summary>
        /// 本処理(別スレッドで実行)
        /// </summary>
        protected override void ThreadFunction()
        {
            int num = 0;
            while (true)
            {
                //リストのカウントを取得
                num = this.lstMessage.Count;

                //スレッドの生死チェック
                if (this.bKillThread || this.thread.ThreadState == System.Threading.ThreadState.StopRequested) { break; }

                //カウントが0になるまでループ
                while (num == 0)
                {
                    //2013/06/24 ver3.0.2
                    //負荷低減のため、スリープ
                    Thread.Sleep(100);
                    LpsWindowsApi.WaitForSingleObject(LpsVoiceRoid150.hEvent, -1);
                    if (this.bKillThread || this.thread.ThreadState == System.Threading.ThreadState.StopRequested)
                    {
                        return;
                    }
                    num = this.lstMessage.Count;
                }

                //イベントを発生させる
                LpsWindowsApi.ResetEvent(LpsVoiceRoid150.hEvent);

                string text = "";

                //先頭のメッセージを取得する
                text = this.lstMessage.First<string>();

                if (text == null)
                {
                    return;
                }

                //メッセージを3つ削る
                if (text.Length <= 0) { this.lstMessage.RemoveAt(0); continue; }
                if (text.Length <= 0) { this.lstMessage.RemoveAt(0); continue; }
                if (text.Length <= 0) { this.lstMessage.RemoveAt(0); continue; }

                //ソフトークの場合
                if (this.setting.sWindowTitle == "SofTalk")
                {
                    if (!File.Exists(this.setting.sExePath))
                    {
                        MessageBox.Show("SofTalkが見つかりませんでした", "error");
                    }
                    else
                    {
                        Process process2 = new Process();
                        process2.StartInfo.FileName = this.setting.sExePath;
                        process2.StartInfo.Arguments = "/W:" + text;
                        process2.Start();
                        process2.WaitForInputIdle(TRY_INTERVAL * TRY_CNT);
                        this.lstMessage.RemoveAt(0);
                        if (this.bKillThread || this.thread.ThreadState == System.Threading.ThreadState.StopRequested)
                        {
                            return;
                        }
                    }
                }
                //ボイスロイドの場合
                else
                {

                    if (!this.findVoiceroidWindow(this.setting, true))
                    {
                        Console.WriteLine("!findVoiceroidWindow");
                        this.clearMessage();
                    }
                    else
                    {
                        //東北ずん子
                        if (this.setting.sWindowTitle == "VOICEROID＋ 東北ずん子")
                        {
                            LpsWindowsApi.SendMessage(this.hEdit, 12, 0, text);
                            Thread.Sleep(TRY_INTERVAL);
                            int dlgCtrlID = LpsWindowsApi.GetDlgCtrlID(this.hPlayButton);
                            LpsWindowsApi.PostMessage(this.hWindow, 273, new IntPtr(this.MakeWParam((short)dlgCtrlID, 0)), this.hPlayButton);
                            StringBuilder stringBuilder = new StringBuilder(255);
                            bool flag12 = true;
                            while (flag12)
                            {
                                LpsWindowsApi.SendMessage(this.hPlayButton, 13, stringBuilder.Capacity, stringBuilder);
                                using (StringReader stringReader = new StringReader(stringBuilder.ToString()))
                                {
                                    if (stringReader.ReadToEnd().IndexOf("再生") >= 0)
                                    {
                                        flag12 = false;
                                    }
                                }
                                Thread.Sleep(TRY_INTERVAL);
                            }
                        }
                        //ずん子以外
                        else
                        {
                            //クリップボードデータオブジェクト
                            DataObject data = new DataObject();

                            //クリップボードからデータを退避
                            data = getClipboardData();

                            //クリップボードにテキストをセット
                            setClipboardText(text);
                            this.callVoiceroidUndoAndPaste(TRY_CNT);

                            //クリップボードにデータを復元
                            if (data != null)
                            {
                                setClipboardData(data);
                            }

                            //センドメッセージ
                            LpsWindowsApi.PostMessage(this.hPlayButton, 0, IntPtr.Zero, IntPtr.Zero);
                        }

                        //メッセージを削除する
                        this.lstMessage.RemoveAt(0);

                        if (this.bKillThread || this.thread.ThreadState == System.Threading.ThreadState.StopRequested)
                        {
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                         　プロセス操作
        ///                         
        ///====================================================================
        #region プロセス操作
        /// <summary>
        /// エラーウインドウの検索
        /// </summary>
        protected void searchErrorWindow()
        {
            LpsWindowsApi.EnumWindows(new LpsWindowsApi.EnumWindowsDelegate(delegate (IntPtr hWnd, int lParam)
            {
                //ウインドウタイトル取得用SB
                StringBuilder sb = new StringBuilder(0x1024);

                //ウインドウチェック
                //対象ハンドルが有効で、ウインドウテキストが取得可能
                if (LpsWindowsApi.IsWindowVisible(hWnd) != 0 && LpsWindowsApi.GetWindowText(hWnd, sb, sb.Capacity) != 0)
                {
                    //タイトル取得
                    string title = sb.ToString();

                    //ウインドウのpid取得
                    int pid;
                    LpsWindowsApi.GetWindowThreadProcessId(hWnd, out pid);

                    //ウインドウのプロセスインスタンス生成
                    Process p = Process.GetProcessById(pid);

                    //エラーウインドウの検索
                    if (title + p.ProcessName == "エラーVOICEROID")
                    {
                        //以下コードでOKボタンを取得し、押下する
                        IntPtr okButton = IntPtr.Zero;
                        okButton = LpsWindowsApi.GetWindow(hWnd, 5u);
                        int num = 0;

                        //東北ずん子の場合
                        if (this.setting.sWindowTitle == "VOICEROID＋ 東北ずん子")
                        {
                            num = 0;
                            while (LpsWindowsApi.SendMessage(okButton, 513, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero && ++num <= TRY_CNT)
                            {
                            }
                            Thread.Sleep(TRY_INTERVAL);
                            while (LpsWindowsApi.SendMessage(okButton, 514, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero)
                            {
                                if (++num > TRY_CNT)
                                {
                                    return 1;
                                }
                            }
                        }
                        //東北ずん子以外の場合
                        else
                        {
                            num = 0;
                            while (LpsWindowsApi.SendMessage(okButton, 0, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero)
                            {
                                if (++num > TRY_CNT)
                                {
                                    break;
                                }
                            }
                        }
                        return 1;
                    }
                }
                return 1;
            }), 0);
        }
        #endregion

        ///====================================================================
        ///
        ///                       クリップボード操作
        ///                         
        ///====================================================================
        #region クリップボード操作
        //親クラスで定義
        #endregion

        ///====================================================================
        ///
        ///                          ボイスロイド操作
        ///                         
        ///====================================================================
        #region ボイスロイド操作
        /// <summary>
        /// ボイスロイドウインドウを探す
        /// </summary>
        /// <param name="option"></param>
        /// <param name="bStartup"></param>
        /// <returns></returns>
        protected override bool findVoiceroidWindow(msgVoiceRoid option, bool bStartup)
        {
            //起動チェック
            bool flag = LpsWindowsApi.IsWindow(this.hWindow);

            //プレイボタンチェック
            if (!flag && !LpsWindowsApi.IsWindow(this.hPlayButton))
            {
                flag = false;
            }

            //ストップボタンチェック
            if (!LpsWindowsApi.IsWindow(this.hStopButton))
            {
                flag = false;
            }

            //ストップボタンチェック
            if (!flag)
            {
                flag = this.getVoiceroidWindowHandle();
                if (!flag)
                {
                    this.hWindow = IntPtr.Zero;
                    this.hPlayButton = IntPtr.Zero;
                    this.hStopButton = IntPtr.Zero;
                }
            }

            //起動チェック
            if (!flag && bStartup)
            {
                bool flag2 = false;

                //ファイル存在チェック
                if (!File.Exists(option.sExePath))
                {
                    this.clearMessage();
                    if (MessageBox.Show("VOICEROID.exeが見つかりません。\n設定を行なってください。", "Liplis", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //起動をうながす。
                        flgNotFound = true;
                    }
                    return false;
                }

                //起動して待つ
                if (new Process { StartInfo = { FileName = option.sExePath, WorkingDirectory = Path.GetDirectoryName(option.sExePath) } }.Start())
                {
                    int num = 30;
                    do
                    {
                        Thread.Sleep(500);
                        num--;
                    }
                    while (num >= 0 && !this.findVoiceroidWindow(option, false));

                    //起動チェック(マイナス値になったら起動失敗と判断)
                    flag2 = (num >= 0);
                }

                //起動失敗チェック
                if (!flag2)
                {
                    this.clearMessage();
                    MessageBox.Show("VOICEROIDの起動が確認できませんでした", "Liplis", MessageBoxButtons.OK);
                    return false;
                }
                flag = flag2;
            }

            //見つかった！
            flgNotFound = false;

            //起動結果を返す
            return flag;
        }

        /// <summary>
        /// ボイスロイドのウインドウハンドルを取得する
        /// </summary>
        /// <returns></returns>
        protected override bool getVoiceroidWindowHandle()
        {
            Process[] processes = Process.GetProcesses();

            //東北ずん子の場合
            if (this.setting.sWindowTitle == "VOICEROID＋ 東北ずん子")
            {
                this.hWindow = IntPtr.Zero;
                Process[] array = processes;
                for (int i = 0; i < array.Length; i++)
                {
                    Process process = array[i];
                    try
                    {
                        if (Path.GetFileName(process.MainModule.FileName) == Path.GetFileName(this.setting.sExePath))
                        {
                            string mainWindowTitle = process.MainWindowTitle;

                            if (mainWindowTitle.IndexOf("東北ずん子") != -1)
                            {
                                this.hWindow = process.MainWindowHandle;
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            //ソフトーク
            else if (this.setting.sWindowTitle == "SofTalk")
            {
                this.hWindow = IntPtr.Zero;
                foreach (Process p in processes)
                {
                    try
                    {
                        if (p.MainWindowTitle.Equals("SofTalk"))
                        {
                            this.hWindow = p.MainWindowHandle;
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            //東北ずん子以外の場合
            else
            {
                this.hWindow = LpsWindowsApi.FindWindow("TkTopLevel", this.setting.sWindowTitle);
            }


            //ウインドウハンドル取得失敗
            if (this.hWindow == IntPtr.Zero) { return false; }

            //ボタンハンドル取得
            //以降の処理について順番を変えるとバグル
            //----------------------------------------------------------------------
            //東北ずん子の場合
            if (this.setting.sWindowTitle == "VOICEROID＋ 東北ずん子")
            {
                this.hPlayButton = LpsWindowsApi.GetWindow(this.hWindow, 5u);
                this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 5u);
                this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 2u);
                this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 5u);
                this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 5u);
                this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 5u);
                this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 5u);
                this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 5u);
                this.hEdit = this.hPlayButton;
                this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 2u);
                this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 5u);
                this.hStopButton = LpsWindowsApi.GetWindow(this.hPlayButton, 2u);
                return true;
            }

            this.hPlayButton = LpsWindowsApi.GetWindow(this.hWindow, 5u);
            this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 5u);
            this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 2u);
            this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 5u);
            this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 2u);
            this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 2u);

            //アイ&ショウタの場合
            //if (this.mvr.title == "VOICEROID - アイ" || this.mvr.title == "VOICEROID - ショウタ")
            //{
            //    this.hPlayButton = LpsWindowsApi.GetWindow(this.hPlayButton, 2u);
            //}

            this.hStopButton = LpsWindowsApi.GetWindow(this.hPlayButton, 2u);
            this.hPlayButton = LpsWindowsApi.GetWindow(this.hStopButton, 2u);
            //----------------------------------------------------------------------

            StringBuilder stringBuilder2 = new StringBuilder();
            if (LpsWindowsApi.GetClassName(this.hStopButton, stringBuilder2, 256) == 0)
            {
                return false;
            }
            if (stringBuilder2.ToString() != "Button")
            {
                return false;
            }
            stringBuilder2.Length = 0;
            if (LpsWindowsApi.GetClassName(this.hPlayButton, stringBuilder2, 256) == 0)
            {
                return false;
            }
            if (stringBuilder2.ToString() != "Button")
            {
                return false;
            }
            stringBuilder2.Length = 0;
            return true;
        }

        /// <summary>
        /// ボイスロイドのストップボタンをコールする
        /// </summary>
        public override void callStopButtonDown()
        {
            //東北ずんこの場合
            if (this.setting.sWindowTitle == "VOICEROID＋ 東北ずん子")
            {
                int num = 0;
                while (LpsWindowsApi.PostMessage(this.hStopButton, 513, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero && ++num <= TRY_CNT)
                {
                }
                Thread.Sleep(TRY_INTERVAL);
                while (LpsWindowsApi.PostMessage(this.hStopButton, 514, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero)
                {
                    if (++num > TRY_CNT)
                    {
                        return;
                    }
                }
                return;
            }

            //ソフトーク以外のボイスロイドの場合
            if (!(this.setting.sWindowTitle == "SofTalk"))
            {
                bool flag = this.findVoiceroidWindow(this.setting, true);
                if (flag)
                {
                    int num2 = 0;
                    while (LpsWindowsApi.PostMessage(this.hStopButton, 0, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero)
                    {
                        if (++num2 > TRY_CNT)
                        {
                            return;
                        }
                    }
                }
                return;
            }

            //エグゼが存在しない場合
            if (!File.Exists(this.setting.sExePath))
            {
                return;
            }

            //ソフトークの場合
            Process process = new Process();
            process.StartInfo.FileName = this.setting.sExePath;
            process.StartInfo.Arguments = "/stop";
            process.Start();
            process.WaitForInputIdle(TRY_CNT * TRY_INTERVAL);
        }
        #endregion




    }
}
