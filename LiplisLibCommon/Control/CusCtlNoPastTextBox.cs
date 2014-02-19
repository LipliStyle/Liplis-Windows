//=======================================================================
//  ClassName : CusCtlNoPastTextBox
//  概要      : コピー・ペーストをさせないテキストボックス
//
//  Liplis2.0
//  Copyright(c) 2010-2012 LipliStyle.Sachin
//=======================================================================
using System.Windows.Forms;


namespace Liplis.Control
{
    public class CusCtlNoPastTextBox : TextBox
    {
        const int WM_PASTE = 0x302;

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTE)
            {
                IDataObject iData = Clipboard.GetDataObject();
                //文字列がクリップボードにあるか
                if (iData != null && iData.GetDataPresent(DataFormats.Text))
                {
                    string clipStr = (string)iData.GetData(DataFormats.Text);
                    //クリップボードの文字列が数字か調べる
                    if (!System.Text.RegularExpressions.Regex.IsMatch(
                        clipStr,
                        @"^[0-9]+$"))
                        return;
                }
            }

            base.WndProc(ref m);
        }
    }
}