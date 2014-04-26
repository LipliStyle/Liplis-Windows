//=======================================================================
//  ClassName : CenterMessageBox
// ■概要     : 親ウインドウの中央に表示するメッセージボックス
//
// ■ Liplis4.0
//
// 参考
// http://support.microsoft.com/kb/180936/en-us 
//=======================================================================
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Liplis.Control
{
    public class CenterMessageBox
    {
        /// <summary>
        /// 親ウィンドウ
        /// </summary>
        private IWin32Window m_ownerWindow = null;

        /// <summary>
        /// フックハンドル
        /// </summary>
        private IntPtr m_hHook = (IntPtr)0;

        /// <summary>
        /// メッセージボックスを表示する
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="button"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static DialogResult Show(
            IWin32Window owner,
            string messageBoxText,
            string caption,
            MessageBoxButtons button,
            MessageBoxIcon icon)
        {
            CenterMessageBox mbox = new CenterMessageBox(owner);
            return mbox.Show(messageBoxText, caption, button, icon);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="window">親ウィンドウ</param>
        private CenterMessageBox(IWin32Window window)
        {
            m_ownerWindow = window;
        }

        /// <summary>
        /// メッセージボックスを表示する
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <param name="button"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        private DialogResult Show(
            string messageBoxText,
            string caption,
            MessageBoxButtons button,
            MessageBoxIcon icon)
        {
            // フックを設定する。
            IntPtr hInstance = WinAPI.GetWindowLong(m_ownerWindow.Handle, WinAPI.GWL_HINSTANCE);
            IntPtr threadId = WinAPI.GetCurrentThreadId();
            m_hHook = WinAPI.SetWindowsHookEx(WinAPI.WH_CBT, new WinAPI.HOOKPROC(HookProc), hInstance, threadId);

            return MessageBox.Show(m_ownerWindow, messageBoxText, caption, button, icon);
        }

        /// <summary>
        /// フックプロシージャ
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {

            if (nCode == WinAPI.HCBT_ACTIVATE)
            {
                WinAPI.RECT rcForm = new WinAPI.RECT(0, 0, 0, 0);
                WinAPI.RECT rcMsgBox = new WinAPI.RECT(0, 0, 0, 0);

                WinAPI.GetWindowRect(m_ownerWindow.Handle, out rcForm);
                WinAPI.GetWindowRect(wParam, out rcMsgBox);

                // センター位置を計算する。
                int x = (rcForm.Left + (rcForm.Right - rcForm.Left) / 2) - ((rcMsgBox.Right - rcMsgBox.Left) / 2);
                int y = (rcForm.Top + (rcForm.Bottom - rcForm.Top) / 2) - ((rcMsgBox.Bottom - rcMsgBox.Top) / 2);

                WinAPI.SetWindowPos(wParam, 0, x, y, 0, 0, WinAPI.SWP_NOSIZE | WinAPI.SWP_NOZORDER | WinAPI.SWP_NOACTIVATE);

                IntPtr result = WinAPI.CallNextHookEx(m_hHook, nCode, wParam, lParam);

                // フックを解除する。
                WinAPI.UnhookWindowsHookEx(m_hHook);
                m_hHook = (IntPtr)0;

                return result;

            }
            else
            {
                return WinAPI.CallNextHookEx(m_hHook, nCode, wParam, lParam);
            }
        }
    }

    /// <summary>
    /// Win API
    /// </summary>
    public class WinAPI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThreadId();
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hInstance, IntPtr threadId);
        [DllImport("user32.dll")]
        public static extern bool UnhookWindowsHookEx(IntPtr hHook);
        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);

        public const int GWL_HINSTANCE = (-6);
        public const int WH_CBT = 5;
        public const int HCBT_ACTIVATE = 5;

        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOACTIVATE = 0x0010;

        public struct RECT
        {
            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}
