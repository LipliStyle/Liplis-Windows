//=======================================================================
//  Clalis3.1
//  ClassName : ResLpsLoginStatus
//  概要      : リプリス設定ろぐいんすてーたす
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Runtime.InteropServices;

namespace Liplis.Msg
{
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class ResLpsLoginStatus
    {
        ///=============================
        ///プロパティ
        public string responseCode { get; set; }
        public string userCode { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsLoginStatus
        public ResLpsLoginStatus()
        {
            this.responseCode = "";
            this.userCode = "";
        }
        public ResLpsLoginStatus(string responseCode, string userCode)
        {
            this.responseCode = responseCode;
            this.userCode = userCode;
        }
        #endregion
    }
}
