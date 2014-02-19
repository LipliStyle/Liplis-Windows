//=======================================================================
//  Clalis3.1
//  ClassName : ResLpsRegisterTwitterInfoRespons
//  概要      : ツイッターユーザー登録レスポンス
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin.Sachin
//=======================================================================
using System;
using System.Runtime.InteropServices;

namespace Liplis.Msg
{
    public class ResLpsRegisterTwitterInfoRespons
    {
        ///=============================
        ///プロパティ
        public string responseCode { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsRegisterTwitterInfoRespons
        public ResLpsRegisterTwitterInfoRespons()
        {
            this.responseCode = "";
        }
        public ResLpsRegisterTwitterInfoRespons(string responseCode)
        {
            this.responseCode = responseCode;
        }
        #endregion
    }
}
