//=======================================================================
//  ClassName : WindowsApi
//  概要      : ウインドウズAPIの定義
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;


namespace Liplis.Common
{
    public static class ComWindowsApi
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
        [DllImport("user32")]
        public extern static int FindWindow(String lpClassName, String lpWindowName);

        ///=============================
        ///インターネット接続確認API
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);



    }
}
