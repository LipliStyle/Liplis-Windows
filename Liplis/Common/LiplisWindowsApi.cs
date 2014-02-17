using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Liplis.Common
{
    public class LiplisWindowsApi
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
            uint Msg,     // メッセージ
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
            uint msg,     // メッセージ
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

        /// <summary>
        /// RegisterWindowMessage
        /// メッセージ登録
        /// </summary>
        /// <param name="pString"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint RegisterWindowMessage(string lpString);

    }
}
