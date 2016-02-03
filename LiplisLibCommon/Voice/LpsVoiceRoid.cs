//=======================================================================
//  ClassName : LpsVoiceRoid
//  概要      : ボイスロイドAPI
//  このクラスにうえさんの「民安★TALK」のソースをMITライセンスにもとづき、
//  使用させて頂いております。
//
//  民安★TALK
//  http://uep.s321.xrea.com/vrx/
//
//  2015/08/31 Liplis4.5.3 VoiceLoid最新バージョン対応
//
//  Liplisシステム       
//  Copyright(c) 2010-2015 LipliStyle Sachin
//=======================================================================
using Liplis.Common;
using Liplis.Msg;
using Liplis.Voice.Option;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Liplis.Voice
{
    public class LpsVoiceRoid : IDisposable
    {
        //------------------------------------------------
        //ウインドウハンドル
        #region ウインドウハンドル
        protected IntPtr UNDO = new IntPtr(46);
        protected IntPtr PASTE = new IntPtr(56);
        protected IntPtr hWindow = IntPtr.Zero;
        protected IntPtr hPlayButton = IntPtr.Zero;
        protected IntPtr hStopButton = IntPtr.Zero;
        protected IntPtr hEdit = IntPtr.Zero;

        protected IntPtr msgHWindow = IntPtr.Zero;
        protected IntPtr msgHPlayButton = IntPtr.Zero;
        protected IntPtr msgHStopButton = IntPtr.Zero;
        protected IntPtr msgHEdit = IntPtr.Zero;
        #endregion

        //------------------------------------------------
        //プロパティ
        //------------------------------------------------
        #region プロパティ
        //LiplisVoiceイベントオブジェクト
        protected static IntPtr LpsVoiceEvent = LpsWindowsApi.CreateEvent(IntPtr.Zero, true, false, "LiplisVoice");

        //スレッド制御
        protected bool bKillThread = false;
        protected Thread thread;

        //メッセージリスト
        protected List<string> lstMessage = new List<string>();

        //実行オプション
        protected msgVoiceRoid setting;

        //トライ定義
        protected int TRY_CNT = 5;
        protected int TRY_INTERVAL = 5;

        //フラグ
        protected bool flgNotFound = false;

        //ボイスロイドハンドルリスト
        protected LpsVoiceRoidHandle lpsVrHandle = new LpsVoiceRoidHandle();

        //ロックオブジェクト
        public object objlock = new object();

        //ランダムインスタンス
        protected Random lpsRand;
        #endregion


        ///====================================================================
        ///
        ///                           初期化処理
        ///                         
        ///====================================================================
        #region 初期化処理
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="option"></param>
        public LpsVoiceRoid(msgVoiceRoid option)
        {
            this.lpsRand = new Random(Environment.TickCount);
            this.initSetting(option);
            this.startThread();
        }
        /// <summary>
        /// デフォルトコンストラクター
        /// 150
        /// </summary>
        public LpsVoiceRoid()
        {

        }

        /// <summary>
        /// initThread
        /// スレッドを初期化する
        /// 150
        /// </summary>
        protected void initThread()
        {
            this.thread = new Thread(new ThreadStart(this.ThreadFunction));
            this.thread.SetApartmentState(ApartmentState.STA);
        }

        /// <summary>
        /// デストラクター
        /// </summary>
        ~LpsVoiceRoid()
        {
            if (this.thread != null)
            {
                this.stopThread();
            }
        }

        /// <summary>
        /// ディスポーズ
        /// 150
        /// </summary>
        public virtual void Dispose()
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
        /// 150_o
        /// </summary>
        /// <param name="msg"></param>
        public virtual void addMessage(string msg)
        {
            //スレッドチェック
            if (!this.thread.IsAlive)
            {
                this.startThread();
            }

            //おしゃべり開始前に、一度停止しておく。
            callStopButtonDown();

            //ロックオブジェクト
            object obj;

            //同期制御
            lock(obj = this.objlock)
            {
                this.lstMessage.Add(msg);
                LpsWindowsApi.SetEvent(LpsVoiceRoid.LpsVoiceEvent);
            }
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
        /// 150_o
        /// </summary>
        protected virtual void clearMessage()
        {
            object obj;
            lock (obj = this.objlock)
            {
                this.lstMessage.Clear();
            }
            LpsWindowsApi.SetEvent(LpsVoiceRoid.LpsVoiceEvent);
        }

        /// <summary>
        /// ロング値を作成する
        /// 150
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        protected long MakeLong(short a, short b)
        {
            return (long)(((int)a & 65535) | ((int)b & 268369920));
        }
        protected long MakeWParam(short l, short h)
        {
            return this.MakeLong(l, h);
        }


        /// <summary>
        /// 設定の初期化
        /// </summary>
        /// <param name="option"></param>
        protected void initSetting(msgVoiceRoid option)
        {
            object obj;
            lock (obj = this.objlock)
            {
                //オプション取得
                this.setting = option;

                //ボイスロイドハンドル生成
                lpsVrHandle = default(LpsVoiceRoidHandle);

                //ウインドウ、ボタンハンドル初期化
                lpsVrHandle.hWindowHandle = IntPtr.Zero;
                lpsVrHandle.hStopHandle = IntPtr.Zero;
                lpsVrHandle.hPlayHandle = IntPtr.Zero;

                //ボイスロイドリストの登録
                lpsVrHandle.Info = this.setting.vrInfo;
            }
        }
      
        #endregion

        ///====================================================================
        ///
        ///                           スレッド操作
        ///                         
        ///====================================================================
        #region スレッド操作
        /// <summary>
        /// スレッド停止
        /// 150
        /// </summary>
        protected void stopThread()
        {
            this.bKillThread = true;
            this.clearMessage();
            Thread.Sleep(100);
            if (LpsVoiceRoid.LpsVoiceEvent == IntPtr.Zero)
            {
                LpsWindowsApi.ResetEvent(LpsVoiceRoid.LpsVoiceEvent);
                LpsWindowsApi.CloseHandle(LpsVoiceRoid.LpsVoiceEvent);
                LpsVoiceRoid.LpsVoiceEvent = IntPtr.Zero;
            }
            if (this.thread.IsAlive)
            {
                this.thread.Abort();
            }
        }

        /// <summary>
        /// スレッド開始
        /// 150_o+
        /// </summary>
        protected virtual void startThread()
        {
            this.thread = new Thread(new ThreadStart(this.ThreadFunction));
            this.thread.SetApartmentState(ApartmentState.STA);
            this.thread.Start();
        }
  
        /// <summary>
        /// 本処理(別スレッドで実行)
        /// 150_o
        /// </summary>
        protected virtual void ThreadFunction()
        {
            //データ
            DataObject data = new DataObject();

            //メッセージ件数
            int messageNum = 0;

            //スレッド本処理
            while (true)
            {
                //メッセージ数取得(スレッド同期)
                object obj;
                lock (obj = this.objlock){ messageNum = this.lstMessage.Count;}

                //スレッドが終了されていた、または停止リクエストがあった場合ループを抜ける
                if (this.bKillThread || this.thread.ThreadState == System.Threading.ThreadState.StopRequested){break;}

                //メッセージ長さが0の場合、LpsVoiceRoid.hEventを待ち、メッセージ数を再取得
                while (messageNum == 0)
                {
                    //ウェイト
                    LpsWindowsApi.WaitForSingleObject(LpsVoiceRoid.LpsVoiceEvent, -1);

                    //スレッドが終了されていた、または停止リクエストがあった場合終了
                    if (this.bKillThread || this.thread.ThreadState == System.Threading.ThreadState.StopRequested){return;}

                    //メッセージ再取得
                    object obj2;
                    lock (obj2 = this.objlock){messageNum = this.lstMessage.Count;}
                }

                //LiplisVoiceイベントを非シグナル状態にする
                LpsWindowsApi.ResetEvent(LpsVoiceRoid.LpsVoiceEvent);

                //おしゃべりテキスト
                string chatText = "";

                //おしゃべりテキストの取得
                object obj3;
                lock (obj3 = this.objlock){chatText = this.lstMessage.First<string>();}

                //取得チャットテキストのの長さが0以下の場合、該当メッセージを削除し、本ループを抜ける
                if (chatText.Length <= 0)
                {
                    removeMessage();
                }

                //ウインドウタイトルチェック
                //ソフトーくの場合
                if (this.setting.windowTitle == "SofTalk")
                {
                    if (!File.Exists(this.setting.voiceRoidPath))
                    {
                        MessageBox.Show("SofTalkが見つかりませんでした", "error");
                    }
                    else
                    {
                        using (Process process = new Process())
                        {
                            process.StartInfo.FileName = this.setting.voiceRoidPath;
                            object obj10;
                            lock (obj10 = this.objlock)
                            {
                                process.StartInfo.Arguments = "/W:" + chatText;
                            }
                            process.Start();
                            process.WaitForInputIdle(this.setting.nTryInterval * this.setting.nTryCount);

                            //先頭メッセージ削除
                            removeMessage();

                            if (this.bKillThread || this.thread.ThreadState == System.Threading.ThreadState.StopRequested)
                            {
                                return;
                            }
                        }

                    }
                }
                //その他(ボイスロイドの場合)
                else
                {
                    //ボイスロイドウインドウを探す。なければ起動する。
                    if (!this.findVoiceroidWindow(this.setting, true))
                    {
                        //発見できたの！
                        Console.WriteLine("!findVoiceroidWindow");

                        //メッセージは受け取れているので、クリアしておく
                        this.clearMessage();
                    }
                    else
                    {

                        bool flag = false;

                        //ボイスロイド名をリストから探す
                        foreach(string name in LpsVoiceRoidDefine.voiceLoidNameList)
                        {
                            //ウインドウタイトルと名前を比較
                            if (this.setting.windowTitle == name)
                            {
                                //見つかった
                                flag = true;
                                break;
                            }
                        }

                        if (flag)
                        {
                            //☆
                            int tryCount = 0;

                            //VoiceRoidにおしゃべりメッセージを送信
                            while (LpsWindowsApi.SendMessage(this.msgHEdit, 12, 0, chatText) == 0 && ++tryCount <= this.setting.nTryCount)
                            {
                            }

                            //待ち
                            Thread.Sleep(this.setting.nTryInterval);

                            //ボタンのIDを取得
                            int dlgCtrlID = LpsWindowsApi.GetDlgCtrlID(this.msgHPlayButton);

                            //再生ボタンの有効チェック
                            bool flgEnablePlayButton = this.isEnabledPlayButton(this.msgHPlayButton);

                            //再生ボタンが有効になるまで待つ
                            while (!flgEnablePlayButton)
                            {
                                flgEnablePlayButton = this.isEnabledPlayButton(this.msgHPlayButton);
                                Thread.Sleep(this.setting.nTryInterval);
                            }

                            //プレイボタンを押す
                            LpsWindowsApi.SendMessage(this.msgHWindow, 273, new IntPtr(this.MakeWParam((short)dlgCtrlID, 0)), this.msgHPlayButton);

                            //ボタン有効フラグを寝かす
                            flgEnablePlayButton = false;

                            //再度、再生ボタンが有効になるの待つ
                            while (!flgEnablePlayButton)
                            {
                                Thread.Sleep(this.setting.nTryInterval);
                                flgEnablePlayButton = this.isEnabledPlayButton(this.msgHPlayButton);
                            }
                        }
                        else
                        {
                            if (this.setting.windowTitle.IndexOf("OpenJTalk") >= 0)
                            {
                                int num4 = 0;
                                while (LpsWindowsApi.SendMessage(this.msgHEdit, 12, 0, chatText) == 0 && ++num4 <= this.setting.nTryCount)
                                {
                                }
                                Thread.Sleep(this.setting.nTryInterval);
                                int dlgCtrlID2 = LpsWindowsApi.GetDlgCtrlID(this.msgHPlayButton);
                                LpsWindowsApi.SendMessage(this.msgHWindow, 273, new IntPtr(this.MakeWParam((short)dlgCtrlID2, 0)), this.msgHPlayButton);
                            }
                            else
                            {
                                this.callVoiceroidUndoAndPaste(this.setting.nTryCount);
                                int nWaitTime = Math.Max(5000, chatText.Length * setting.nWaitOfChar);
                                this.callPlayButtonDown(nWaitTime);
                            }
                        }

                        //先頭メッセージ削除
                        removeMessage();

                        //スレッドが終了されていた、または停止リクエストがあった場合終了
                        if (this.bKillThread || this.thread.ThreadState == System.Threading.ThreadState.StopRequested)
                        {
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 先頭メッセージを削除する
        /// </summary>
        protected void removeMessage()
        {
            object obj;
            lock(obj = this.objlock)
            {
                this.lstMessage.RemoveAt(0);
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                       クリップボード操作
        ///                         
        ///====================================================================
        #region クリップボード操作

        /// <summary>
        /// クリップボードがデータを取得する
        /// 150
        /// </summary>
        /// <returns></returns>
        protected DataObject getClipboardData()
        {
            IDataObject dataObject = null;
            try
            {
                dataObject = Clipboard.GetDataObject();
            }
            catch (ExternalException)
            {
                DataObject result = null;
                return result;
            }
            if (dataObject == null)
            {
                return null;
            }
            DataObject dataObject2 = new DataObject();
            string[] formats = dataObject.GetFormats(true);
            string[] array = formats;
            for (int i = 0; i < array.Length; i++)
            {
                string format = array[i];
                dataObject2.SetData(format, dataObject.GetData(format));
            }
            return dataObject2;
        }

        /// <summary>
        /// テキストをクリップボードにセットする
        /// 150
        /// </summary>
        /// <param name="text"></param>
        protected void setClipboardText(string text)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetText(text);
            }
            catch (ExternalException)
            {
            }
        }


        /// <summary>
        /// クリップボードにデータをセットする
        /// 150
        /// </summary>
        /// <param name="data"></param>
        protected void setClipboardData(DataObject data)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetDataObject(data, true);
            }
            catch (ExternalException)
            {
            }
        }
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
        protected virtual bool findVoiceroidWindow(msgVoiceRoid option, bool bStartup)
        {
            //起動チェック
            bool flag = LpsWindowsApi.IsWindow(this.hWindow);

            //プレイボタンチェック
            if (!flag && !LpsWindowsApi.IsWindow(this.hPlayButton))
            {
                flag = false;
            }

            //ウインドウハンドルのチェック
            if (!flag)
            {
                //ボイスロイドウインドウハンドル取得
                flag = this.getVoiceroidWindowHandle();

                //取得できなかった場合は初期値設定
                if (!flag)
                {
                    this.hWindow = IntPtr.Zero;
                    this.hPlayButton = IntPtr.Zero;
                    this.hStopButton = IntPtr.Zero;
                }
                //取得できた場合は、登録する。
                else
                {
                    LpsVoiceRoidHandle value = default(LpsVoiceRoidHandle);
                    value.hPlayHandle = this.msgHPlayButton;
                    value.hStopHandle = this.msgHStopButton;
                    value.hEditHandle = this.msgHEdit;
                    value.hWindowHandle = this.msgHWindow;
                    value.Info = this.setting.vrInfo;
                    lpsVrHandle = value;
                }
            }

            //未起動の場合は起動
            if (!flag && bStartup)
            {
                //対象のソフトのEXEを起動する
                flag = StartupTargetExe(ref option);
            }

            //見つかった！
            flgNotFound = false;

            //起動結果を返す
            return flag;
        }

        /// <summary>
        /// ボイスロイドのスタートボタンをコールする
        /// 150
        /// </summary>
        public void callStartButtonDown()
        {
            int num2 = 0;

            //センドメッセージのトライ
            while (LpsWindowsApi.SendMessage(this.hPlayButton, 0, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero && ++num2 <= TRY_CNT)
            {
            }
        }

        /// <summary>
        /// ボイスロイドのストップボタンをコールする
        /// </summary>
        public virtual void callStopButtonDown()
        {
            bool flag = this.getSearchProcessName(this.setting.windowTitle) != "";
            if (flag)
            {
                int num = 0;
                while (LpsWindowsApi.SendMessage(this.msgHStopButton, 513, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero && ++num <= this.setting.nTryCount)
                {
                }
                Thread.Sleep(this.setting.nTryInterval);
                while (LpsWindowsApi.SendMessage(this.msgHStopButton, 514, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero)
                {
                    if (++num > this.setting.nTryCount)
                    {
                        return;
                    }
                }
                return;
            }
            if (!(this.setting.windowTitle == "SofTalk"))
            {
                bool flag2 = this.findVoiceroidWindow(this.setting, true);
                if (flag2)
                {
                    int num2 = 0;
                    while (LpsWindowsApi.SendMessage(this.msgHStopButton, 0, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero)
                    {
                        if (++num2 > this.setting.nTryCount)
                        {
                            return;
                        }
                    }
                }
                return;
            }
            if (!File.Exists(this.setting.voiceRoidPath))
            {
                return;
            }
            Process process = new Process();
            process.StartInfo.FileName = this.setting.voiceRoidPath;
            process.StartInfo.Arguments = "/stop";
            process.Start();
            process.WaitForInputIdle(this.setting.nTryCount * this.setting.nTryInterval);
        }

        /// <summary>
        /// ボイスロイドのウインドウハンドルを取得する
        /// 150_o
        /// </summary>
        /// <returns></returns>
        protected virtual bool getVoiceroidWindowHandle()
        {
            if (this.setting.windowTitle.IndexOf("OpenJTalk") == 0)
            {
                this.msgHWindow = LpsWindowsApi.FindWindow("#32770", this.setting.windowTitle);
                this.msgHStopButton = IntPtr.Zero;
                this.msgHEdit = IntPtr.Zero;
                this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHWindow, 5u);
                bool result;
                while (true)
                {
                    this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 2u);
                    StringBuilder stringBuilder = new StringBuilder(255);
                    LpsWindowsApi.SendMessage(this.msgHPlayButton, 13, stringBuilder.Capacity, stringBuilder);
                    using (StringReader stringReader = new StringReader(stringBuilder.ToString()))
                    {
                        if (stringReader.ReadToEnd() == "音声合成")
                        {
                            this.msgHEdit = LpsWindowsApi.GetWindow(this.msgHPlayButton, 3u);
                            result = true;
                            break;
                        }
                    }
                    if (!(this.msgHPlayButton != IntPtr.Zero))
                    {
                        goto IL_E8;
                    }
                }
                return result;
            }
            IL_E8:
            string searchProcessName = this.getSearchProcessName(this.setting.windowTitle);
            bool flag = searchProcessName != "";
            if (flag)
            {
                this.searchProcces(this.setting.voiceRoidPath, searchProcessName);
            }
            else
            {
                this.msgHWindow = LpsWindowsApi.FindWindow("TkTopLevel", this.setting.windowTitle);
            }
            if (this.msgHWindow == IntPtr.Zero)
            {
                return false;
            }
            return this.GetVoiceroidPlayButtonHandle(this.msgHWindow, searchProcessName);
        }




        /// <summary>
        /// isEnabledPlayButton
        /// 再生ボタン有効チェック
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        protected bool isEnabledPlayButton(IntPtr handle)
        {
            string text = "";
            StringBuilder stringBuilder = new StringBuilder(255);
            LpsWindowsApi.SendMessage(this.msgHPlayButton, 13, stringBuilder.Capacity, stringBuilder);
            using (StringReader stringReader = new StringReader(stringBuilder.ToString()))
            {
                text = stringReader.ReadToEnd();
            }
            return text.IndexOf("再生") >= 0 && LpsWindowsApi.IsWindowEnabled(handle);
        }


        /// <summary>
        /// 再生ボタン呼び出し
        /// </summary>
        /// <param name="nWaitTime"></param>
        protected void callPlayButtonDown(int nWaitTime)
        {
            if (setting.nHangBehavior == 2)
            {
                int num = 0;
                while (LpsWindowsApi.SendMessage(this.msgHPlayButton, 0, IntPtr.Zero, IntPtr.Zero) != IntPtr.Zero)
                {
                    num++;
                    if (num > setting.nTryCount)
                    {
                        return;
                    }
                }
                return;
            }
            IntPtr intPtr;
            if (LpsWindowsApi.SendMessageTimeout(this.msgHPlayButton, 0, 0, 0, 2, nWaitTime, out intPtr) == 0)
            {
                int lastWin32Error = Marshal.GetLastWin32Error();
                if (lastWin32Error == 1460)
                {
                    switch (setting.nHangBehavior)
                    {
                        case 0:
                            {
                                int processId = 0;
                                if (LpsWindowsApi.GetWindowThreadProcessId(this.msgHWindow, out processId) > 0)
                                {
                                    Process processById = Process.GetProcessById(processId);
                                    if (LpsWindowsApi.TerminateProcess(processById.Handle, 0u))
                                    {
                                        this.StartupTargetExe(ref this.setting);
                                        return;
                                    }
                                }
                                break;
                            }
                        case 1:
                            this.callStopButtonDown();
                            return;
                        case 2:
                            break;
                        default:
                            return;
                    }
                }
                else
                {
                    int processId2 = 0;
                    if (LpsWindowsApi.GetWindowThreadProcessId(this.msgHWindow, out processId2) > 0)
                    {
                        Process processById2 = Process.GetProcessById(processId2);
                        if (LpsWindowsApi.TerminateProcess(processById2.Handle, 0u))
                        {
                            this.StartupTargetExe(ref this.setting);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// callVoiceroidUndoAndPaste
        /// ボイスロイド起動 + アンドゥー＋ペースト
        /// </summary>
        /// <param name="nTryCount"></param>
        protected void callVoiceroidUndoAndPaste(int nTryCount)
        {
            //息継ぎ
            Thread.Sleep(TRY_INTERVAL);
            int num = 0;

            //アンドゥー
            while (num < nTryCount && !(LpsWindowsApi.PostMessage(this.hWindow, 273, this.UNDO, IntPtr.Zero) == IntPtr.Zero))
            {
                num++;
            }

            //息継ぎ
            Thread.Sleep(TRY_INTERVAL);
            int num2 = 0;

            //ペースト
            while (num2 < nTryCount && !(LpsWindowsApi.SendMessage(this.hWindow, 273, this.PASTE, IntPtr.Zero) == IntPtr.Zero))
            {
                num2++;
            }

            //息継ぎ
            Thread.Sleep(TRY_INTERVAL);
        }

        /// <summary>
        /// 対象のEXEを実行する
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        protected bool StartupTargetExe(ref msgVoiceRoid option)
        {
            bool flag = false;

            //ファイル存在チェック
            if (!File.Exists(option.voiceRoidPath))
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
            if (new Process{StartInfo ={FileName = option.voiceRoidPath,WorkingDirectory = Path.GetDirectoryName(option.voiceRoidPath)}}.Start())
            {
                int num = 40;
                do
                {
                    Thread.Sleep(500);
                    num--;
                }
                while (num >= 0 && !this.findVoiceroidWindow(option, false));

                //起動チェック(マイナス値になったら起動失敗と判断)
                flag = (num >= 0);
            }
            if (!flag)
            {
                this.clearMessage();
                MessageBox.Show("VOICEROIDの起動が確認できませんでした", "Liplis", MessageBoxButtons.OK);
                return false;
            }
            return flag;
        }

        /// <summary>
        /// 再生ボタンのハンドルを取得する
        /// </summary>
        /// <param name="hWindow"></param>
        /// <param name="sVRName"></param>
        /// <returns></returns>
        public bool GetVoiceroidPlayButtonHandle(IntPtr hWindow, string sVRName)
        {
            this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHWindow, 5u);
            if (this.msgHPlayButton == IntPtr.Zero)
            {
                return false;
            }
            this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 5u);
            if (this.msgHPlayButton == IntPtr.Zero)
            {
                return false;
            }
            this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 2u);
            if (this.msgHPlayButton == IntPtr.Zero)
            {
                return false;
            }
            this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 5u);
            if (this.msgHPlayButton == IntPtr.Zero)
            {
                return false;
            }
            this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 5u);
            if (this.msgHPlayButton == IntPtr.Zero)
            {
                return false;
            }
            this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 5u);
            if (this.msgHPlayButton == IntPtr.Zero)
            {
                return false;
            }
            this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 5u);
            if (this.msgHPlayButton == IntPtr.Zero)
            {
                return false;
            }
            this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 5u);
            if (this.msgHPlayButton == IntPtr.Zero)
            {
                return false;
            }
            if (sVRName == "東北ずん子")
            {
                this.msgHEdit = this.msgHPlayButton;
            }
            else
            {
                this.msgHEdit = LpsWindowsApi.GetWindow(this.msgHPlayButton, 5u);
            }
            this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 2u);
            if (this.msgHPlayButton == IntPtr.Zero)
            {
                return false;
            }
            this.msgHPlayButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 5u);
            if (this.msgHPlayButton == IntPtr.Zero)
            {
                return false;
            }
            this.msgHStopButton = LpsWindowsApi.GetWindow(this.msgHPlayButton, 2u);
            return !(this.msgHStopButton == IntPtr.Zero);
        }        
        #endregion

        ///====================================================================
        ///
        ///                          プロセス関連
        ///                         
        ///====================================================================
        #region プロセス関連
        /// <summary>
        /// プロセス名を検索する
        /// </summary>
        /// <param name="sWindowTitle"></param>
        /// <returns></returns>
        protected string getSearchProcessName(string sWindowTitle)
        {
            foreach (string name in LpsVoiceRoidDefine.voiceLoidNameList)
            {
                if (sWindowTitle == name)
                {
                    return name;
                }
            }

                return "";
        }

        /// <summary>
        /// プロセスを検索する
        /// </summary>
        /// <param name="sExePath"></param>
        /// <param name="sVRName"></param>
        /// <returns></returns>
        protected bool searchProcces(string sExePath, string sVRName)
        {
            Process[] processes = Process.GetProcesses();
            this.msgHWindow = IntPtr.Zero;
            Process[] array = processes;
            for (int i = 0; i < array.Length; i++)
            {
                Process process = array[i];
                try
                {
                    if (process.MainModule.FileName.ToLower() == sExePath.ToLower())
                    {
                        string mainWindowTitle = process.MainWindowTitle;
                        if (mainWindowTitle.IndexOf(sVRName) != -1)
                        {
                            this.msgHWindow = process.MainWindowHandle;
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            return true;
        }

        #endregion
    }
}
