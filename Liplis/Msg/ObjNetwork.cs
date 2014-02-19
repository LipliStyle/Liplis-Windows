//=======================================================================
//  ClassName : ObjNetwork
//  概要      : ネットワークオブジェクト
//
//  LiplisSystem   
//  Copyright(c) 2010-2011 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Liplis.Msg
{
    public class ObjNetwork
    {
        ///=============================
        ///インターネット接続確認API
        [DllImport("wininet.dll")]
        extern static bool InternetGetConnectedState(out int lpdwFlags, int dwReserved);

        /// <summary>
        /// checkInterNetConnection
        /// インターネット接続されているか確認する
        /// </summary>
        /// <returns></returns>
        #region checkInterNetConnection
        public static bool checkInterNetConnection()
        {
            try
            {
                int flags;
                return InternetGetConnectedState(out flags, 0);
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
