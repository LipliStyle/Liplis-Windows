//=======================================================================
//  Clalis3.1
//  ClassName : ResLiplisId
//  概要      : レスポンスユーザーID
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin.Sachin
//=======================================================================
using System;
using System.Runtime.InteropServices;
namespace Liplis.Msg
{
    public class ResLiplisId
    {
        ///=============================
        ///プロパティ
        public string userId { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLiplisId
        public ResLiplisId()
        {
            this.userId = "";
        }
        public ResLiplisId(string userId)
        {
            this.userId = userId;
        }
        #endregion
    }
}
