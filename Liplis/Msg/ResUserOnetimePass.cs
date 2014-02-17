//=======================================================================
//  Clalis3.1
//  ClassName : ResLpsShortNews2Json
//  概要      : レスポンスショートニュースJsonオブジェクトリスト
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
    public class ResUserOnetimePass
    {
        ///=============================
        ///プロパティ
        public string oneTimePass { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResUserOnetimePass
        public ResUserOnetimePass()
        {
            this.oneTimePass = "";
        }
        public ResUserOnetimePass(string oneTimePass)
        {
            this.oneTimePass = oneTimePass;
        }
        #endregion
    }
}
