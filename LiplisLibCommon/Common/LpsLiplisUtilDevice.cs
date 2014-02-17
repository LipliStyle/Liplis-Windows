//=======================================================================
//  ClassName : LpsLiplisUtilDevice
//  概要      : デバイス関連のユーティリティクラス
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liplis.Common
{
    public static class LpsLiplisUtilDevice
    {
        /// <summary>
        /// getDriveList
        /// ドライブのリストを取得する
        /// </summary>
        /// <returns></returns>
        #region getDriveList
        public static List<string> getDriveList()
        {
            return new List<string>(Directory.GetLogicalDrives());
        }
        #endregion

    }
}
