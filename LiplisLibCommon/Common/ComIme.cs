//=======================================================================
//  ClassName : ComIme
//  概要      : ふりがな変換を行う
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace Liplis.Common
{
    public class ComIme
    {
        private bool FInitialized = false;

        private const int S_OK = 0;
        private const int CLSCTX_LOCAL_SERVER = 4;
        private const int CLSCTX_INPROC_SERVER = 1;
        private const int CLSCTX_INPROC_HANDLER = 2;
        private const int CLSCTX_SERVER = CLSCTX_INPROC_SERVER | CLSCTX_LOCAL_SERVER;
        private const int FELANG_REQ_REV = 0x00030000;
        private const int FELANG_CMODE_PINYIN = 0x00000100;
        private const int FELANG_CMODE_NOINVISIBLECHAR = 0x40000000;

        [DllImport("ole32.dll")]
        private static extern int CLSIDFromString([MarshalAs(UnmanagedType.LPWStr)] string lpsz, out Guid pclsid);

        [DllImport("ole32.dll")]
        private static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] Guid rclsid,
            IntPtr pUnkOuter, uint dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr rpv);

        [DllImport("ole32.dll")]
        private static extern int CoInitialize(IntPtr pvReserved);

        [DllImport("ole32.dll")]
        private static extern int CoUninitialize();

        //-------------------------------------------------------------------------------------
        // コンストラクタ
        public ComIme()
        {
            int res = CoInitialize(IntPtr.Zero);

            if (res == S_OK)
                FInitialized = true;
        }

        public void Dispose()
        {
            if (FInitialized)
            {
                CoUninitialize();
                FInitialized = false;
            }
        }

        // デストラクタ
        ~ComIme()
        {
            if (FInitialized)
                CoUninitialize();
        }

        /// <summary>
        /// ふりがなを取得する
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetYomi(string str)
        {
            string yomi = String.Empty;
            IFELanguage language = null;
            Guid pclsid;
            int res;
            try
            {
                //入力文字列が空なら抜ける
                if (str == "")
                {
                    return "";
                }

                // 文字列の CLSID から CLSID へのポインタを取得する
                res = CLSIDFromString("MSIME.Japan", out pclsid);

                if (res != S_OK)
                {
                    this.Dispose();
                    return yomi;
                }

                Guid riid = new Guid("019F7152-E6DB-11D0-83C3-00C04FDDB82E");
                IntPtr ppv;
                res = CoCreateInstance(pclsid, IntPtr.Zero, CLSCTX_SERVER, riid, out ppv);

                if (res != S_OK)
                {
                    this.Dispose();
                    return Strings.StrConv(str, VbStrConv.Hiragana, 0);
                }

                language = Marshal.GetTypedObjectForIUnknown(ppv, typeof(IFELanguage)) as IFELanguage;
                res = language.Open();

                if (res != S_OK)
                {
                    this.Dispose();
                    return Strings.StrConv(str, VbStrConv.Hiragana, 0);
                }

                IntPtr result;

                res = language.GetJMorphResult(FELANG_REQ_REV, FELANG_CMODE_PINYIN | FELANG_CMODE_NOINVISIBLECHAR,
                        str.Length, str, IntPtr.Zero, out result);

                if (res != S_OK)
                {
                    this.Dispose();
                    return Strings.StrConv(str, VbStrConv.Hiragana, 0);
                }

                yomi = Marshal.PtrToStringUni(Marshal.ReadIntPtr(result, 4), Marshal.ReadInt16(result, 8));
            }
            catch
            {
                return "";
            }
            finally
            {
                if (language != null)
                {
                    language.Close();
                }
                
            }
            
            return yomi;
        }
    }
    //**************************************************************************************
    // IFELanguage Interface（メソッドの実装はランタイムの中にあるので、実装は不要）
    //**************************************************************************************
    [ComImport]
    [Guid("019F7152-E6DB-11D0-83C3-00C04FDDB82E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFELanguage
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int Open();
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int Close();
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int GetJMorphResult(uint dwRequest, uint dwCMode, int cwchInput,
            [MarshalAs(UnmanagedType.LPWStr)] string pwchInput, IntPtr pfCInfo, out IntPtr ppResult);
    } // end of IFELanguage Interface

}
