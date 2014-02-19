//=======================================================================
//  Clalis3.1
//  ClassName : ResLpsLoginRegisterInfoTw
//  概要      : リプリス設定情報Twitter
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Liplis.Msg
{
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class ResLpsLoginRegisterInfoTw
    {
        ///=============================
        ///プロパティ
        public List<RegisterTwUserInfo> twuserlist { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsLoginRegisterInfoTw
        public ResLpsLoginRegisterInfoTw()
        {
            this.twuserlist = new List<RegisterTwUserInfo>();
        }
        public ResLpsLoginRegisterInfoTw(List<RegisterTwUserInfo> twuserlist)
        {
            this.twuserlist = twuserlist;
        }
        #endregion
    }
}
