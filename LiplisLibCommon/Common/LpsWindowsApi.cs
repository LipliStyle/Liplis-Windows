//=======================================================================
//  ClassName : WindowsApi
//  概要      : ウインドウズAPIの定義
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin.Sachin
//=======================================================================
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Liplis.Common
{
    public static class LpsWindowsApi
    {
        ///=====================================
        /// センドメッセージ
        /// 

        /// <summary>
        /// SendMessage
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(
            IntPtr hWnd,    // 送信先ウィンドウのハンドル
            UInt32 Msg,     // メッセージ
            IntPtr wParam,  // メッセージの最初のパラメータ
            IntPtr lParam   // メッセージの 2 番目のパラメータ
            );

        /// <summary>
        /// PostMessage
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr PostMessage(
            IntPtr hWnd,    // 送信先ウィンドウのハンドル
            UInt32 msg,     // メッセージ
            IntPtr wParam,  // メッセージの最初のパラメータ
            IntPtr lParam   // メッセージの 2 番目のパラメータ
            );

        //[DllImport("user32.dll", SetLastError = true)]
        //public static extern IntPtr FindWindowEx(
        //    IntPtr hwndParent, 
        //    IntPtr hwndChildAfter, 
        //    string lpszClass, 
        //    string lpszWindow
        //    );

        /// <summary>
        /// FindWindowEx
        /// </summary>
        /// <param name="parentHandle"></param>
        /// <param name="childAfter"></param>
        /// <param name="className"></param>
        /// <param name="windowTitle"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(
            IntPtr parentHandle,
            IntPtr childAfter,
            string className,
            IntPtr windowTitle
            );

        /// <summary>
        /// GetWindowThreadProcessId
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        /// <summary>
        /// GetWindowThreadProcessId
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        /// // When you don't want the ProcessId, use this overload and pass IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        ///// <summary>
        ///// OpenProcess
        ///// </summary>
        ///// <param name="dwDesiredAccess"></param>
        ///// <param name="bInheritHandle"></param>
        ///// <param name="dwProcessId"></param>
        ///// <returns></returns>
        //[DllImport("kernel32.dll")]
        //static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        ///// <summary>
        ///// VirtualAllocEx
        ///// </summary>
        ///// <param name="dwDesiredAccess"></param>
        ///// <param name="bInheritHandle"></param>
        ///// <param name="dwProcessId"></param>
        ///// <returns></returns>
        //[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        //static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
        //   uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);


        /// <summary>
        /// OLEコムラップ
        /// </summary>
        [DllImport("ole32")]
        public static extern int OleDraw(IntPtr pUnk, DVASPECT dwAspect, IntPtr hdcDraw, ref Rectangle lprcBounds);

        /// <summary>
        /// gdiコムラップ
        /// </summary>
        [DllImport("gdi32")]
        public static extern int GetDeviceCaps(IntPtr hdc, DEVICECAPS caps);

        /// <summary>
        /// GetScrollPos
        /// Horizonal = 0x0000
        /// Vertical  = 0x0001
        /// </summary>
        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        public static extern int GetScrollPos(
            IntPtr handle,
            int nBar
        );

        /// <summary>
        /// GetScrollPos
        /// Horizonal = 0x0000
        /// Vertical  = 0x0001
        /// bRedraw = 再描画フラグ
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int SetScrollPos(
            IntPtr hWnd,
            int nBar,
            int nPos,
            bool bRedraw
            );

        ///====================================================================
        ///
        ///                          プロセス取得
        ///                         
        ///====================================================================

        /// <summary>
        /// EnumWindows
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate bool EnumWindowCallBack(int hwnd, int lParam);
        [DllImport("user32.Dll")]
        public static extern int EnumWindows(EnumWindowCallBack x, int y); 

        /// <summary>
        /// IsWindowVisible
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int IsWindowVisible(int hWnd);

        /// <summary>
        /// GetWindowText
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpString"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowText(int hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// GetWindowThreadProcessId
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        public const int GW_HWNDNEXT = 2;
        [DllImport("user32")]
        public extern static int GetParent(int hwnd);
        [DllImport("user32")]
        public extern static int GetWindow(int hwnd, int wCmd);

        ///=============================
        ///インターネット接続確認API
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);

        /// <summary>
        /// SetParent
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="hWndNewParent"></param>
        /// <returns></returns>
        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        public extern static IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /// <summary>
        /// 常に非アクティブを実現するAPI
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="hWndNewParent"></param>
        /// <returns></returns>
        #region 常に非アクティブ
        public const UInt32 WS_EX_NOACTIVATE = 0x8000000;  // アクティブ化されないスタイル
        public enum GWL : int
        {
            WINDPROC = -4,
            HINSTANCE = -6,
            HWNDPARENT = -8,
            STYLE = -16,
            EXSTYLE = -20,
            USERDATA = -21,
            ID = -12
        }
        public enum SWP : int
        {
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOSENDCHANGING = 0x400
        }
        [DllImport("user32.dll")]
        public static extern UInt32 GetWindowLong(IntPtr hWnd,
        GWL index);
        [DllImport("user32.dll")]
        public static extern UInt32 SetWindowLong(IntPtr hWnd,
        GWL index, UInt32 unValue);
        [DllImport("user32.dll")]
        public static extern UInt32 SetWindowPos(IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int x, int y, int width, int height, SWP flags);
        #endregion


        ///====================================================================
        ///
        ///                        ボイスロイド制御関連
        ///                         
        ///====================================================================
        #region ボイスロイド制御関連

        //指定されたコントロールの識別子を取得します。
        //https://msdn.microsoft.com/ja-jp/library/windows/desktop/ms645478%28v=vs.85%29.aspx
        [DllImport("user32.dll")]
        public static extern int GetDlgCtrlID(IntPtr hwndCtl);

        //名前付きまたは名前なしのイベントオブジェクトを作成または開きます。
        //https://msdn.microsoft.com/ja-jp/library/Cc429057.aspx
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);

        //呼び出し側スレッドが持つ最新のエラーコードを設定します。
        //https://msdn.microsoft.com/ja-jp/library/Cc428989.aspx
        [DllImport("kernel32.dll")]
        public static extern void SetLastError(int errorCode);

        //指定されたオブジェクトをシグナル状態に設定します。
        //https://msdn.microsoft.com/ja-jp/library/Cc429328.aspx
        [DllImport("kernel32.dll")]
        public static extern bool SetEvent(IntPtr handle);

        //指定されたイベントオブジェクトを非シグナル状態に設定します。
        //https://msdn.microsoft.com/ja-jp/library/windows/desktop/ms685081%28v=vs.85%29.aspx
        [DllImport("kernel32.dll")]
        public static extern bool ResetEvent(IntPtr handle);

        //次のいずれかが成立すると、制御を返します。
        //•指定されたオブジェクトがシグナル状態になった。
        //•タイムアウト時間が経過した。
        //アラート可能な待機状態に入るには、WaitForMultipleObjectsEx 関数を使います。複数のオブジェクトを待機するには、WaitForMultipleObjects 関数を使います。
        //https://msdn.microsoft.com/ja-jp/library/Cc429427.aspx
        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int WaitForSingleObject(IntPtr handle, int milliseconds);

        //指定されたウィンドウハンドルを持つウィンドウが存在しているかどうかを調べます。
        //https://msdn.microsoft.com/ja-jp/library/Cc364813.aspx
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);

        //指定された文字列と一致するクラス名とウィンドウ名を持つトップレベルウィンドウ（ 親を持たないウィンドウ）のハンドルを返します。
        //この関数は、子ウィンドウは探しません。検索では、大文字小文字は区別されません。
        //子ウィンドウを探すには、FindWindowEx 関数を使ってください。FindWindowEx 関数による検索は、指定した子ウィンドウから開始されます。
        //https://msdn.microsoft.com/ja-jp/library/Cc364634.aspx
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        //指定されたウィンドウと指定された関係（ またはオーナー）にあるウィンドウのハンドルを返します。
        //https://msdn.microsoft.com/ja-jp/library/Cc364757.aspx
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        //ウインドウのクラス名を取得する
        //https://msdn.microsoft.com/ja-jp/library/windows/desktop/ms633582%28v=vs.85%29.aspx
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        //1 つまたは複数のウィンドウへ、指定されたメッセージを送信します。この関数は、指定されたウィンドウのウィンドウプロシージャを呼び出し、
        //そのウィンドウプロシージャがメッセージを処理し終わった後で、制御を返します。
        //メッセージを送信して即座に制御を返すには、SendMessageCallback または SendNotifyMessage 関数を使ってください。
        //メッセージを 1 つのスレッドのメッセージキューにポストして即座に制御を返すには、PostMessage または PostThreadMessage 関数を使ってください。
        //https://msdn.microsoft.com/ja-jp/library/Cc411022.aspx
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int Param, string s);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int Param, StringBuilder text);

        //1 つまたは複数のウィンドウへ、指定されたメッセージを送信します。
        //この関数は、指定されたウィンドウのウィンドウプロシージャを呼び出し、
        //そのウィンドウプロシージャがメッセージを処理し終わった後で、制御を返します。
        //メッセージを送信して即座に制御を返すには、SendMessageCallback または SendNotifyMessage 関数を使ってください。
        //メッセージを 1 つのスレッドのメッセージキューにポストして即座に制御を返すには、PostMessage または PostThreadMessage 関数を使ってください。
        //https://msdn.microsoft.com/ja-jp/library/Cc411022.aspx
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessageTimeout(IntPtr hWnd, int uMsg, int wParam, int lParam, int fuFlags, int uTimeout, out IntPtr lpdwResult);


        //開いているオブジェクトハンドルを閉じます。
        //https://msdn.microsoft.com/ja-jp/library/Cc429605.aspx
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);


        //画面上のすべてのトップレベルウィンドウを列挙します。
        //この関数を呼び出すと、各ウィンドウのハンドルが順々にアプリケーション定義のコールバック関数に渡されます。
        //EnumWindows 関数は、すべてのトップレベルリンドウを列挙し終えるか、またはアプリケーション定義のコールバック関数から 0（FALSE）が返されるまで処理を続けます。
        //https://msdn.microsoft.com/ja-jp/library/Cc410851.aspx
        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowsDelegate lpEnumFunc, int lParam);

        //指定されたウィンドウの表示状態を調べます。
        //https://msdn.microsoft.com/ja-jp/library/Cc364819.aspx
        [DllImport("user32.dll")]
        public static extern int IsWindowVisible(IntPtr hWnd);

        //指定されたウインドウのタイトルバーのテキストを取得する
        //https://msdn.microsoft.com/ja-jp/library/windows/desktop/ms633520%28v=vs.85%29.aspx
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        //指定されたウインドウのプロセスIDを取得する
        //https://msdn.microsoft.com/ja-jp/library/windows/desktop/ms633522%28v=vs.85%29.aspx
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        //ウィンドウが有効であるか、無効であるかを判断します。
        //https://msdn.microsoft.com/ja-jp/library/windows/desktop/ms646303%28v=vs.85%29.aspx
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        //指定されたプロセスと、そのプロセスに所属するすべてのスレッドを終了させます。
        //https://msdn.microsoft.com/ja-jp/library/Cc429376.aspx
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        //ウィンドウのサイズと表示位置を変更するた
        //https://msdn.microsoft.com/ja-jp/library/windows/desktop/ms633534%28v=vs.85%29.aspx
        [DllImport("user32.dll")]
        public static extern int MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, int bRepaint);

        //ダイアログボックス内の指定されたコントロールへメッセージを送信します。
        //https://msdn.microsoft.com/ja-jp/library/cc411002.aspx
        [DllImport("user32.dll")]
        private static extern IntPtr SendDlgItemMessage(IntPtr hDlg, int nIDDlgItem, uint Msg, UIntPtr wParam, IntPtr lParam);

        //指定されたウインドウタイトルを持つプロセスの一覧を取得する
        public delegate int EnumWindowsDelegate(IntPtr hWnd, int lParam);
        #endregion


    }
}
