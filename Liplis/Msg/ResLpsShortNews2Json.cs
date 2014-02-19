//=======================================================================
//  Clalis3.1
//  ClassName : ResLpsShortNews2Json
//  概要      : レスポンスショートニュースJsonオブジェクトリスト
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin.Sachin
//=======================================================================
using System;
using System.Runtime.InteropServices;

namespace Liplis.Msg
{
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class ResLpsShortNews2Json
    {
        ///=============================
        ///プロパティ
        public string url { get; set; }
        public string result { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region resShortNews
        public ResLpsShortNews2Json()
        {
            this.url = "";
            this.result = "";
        }
        public ResLpsShortNews2Json(string url, string result)
        {
            this.url = url;
            this.result = result;
        }
        #endregion
    }
}
