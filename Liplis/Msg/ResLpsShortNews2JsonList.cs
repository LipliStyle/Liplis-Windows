//=======================================================================
//  Clalis3.1
//  ClassName : ResLpsShortNews2JsonList
//  概要      : レスポンスショートニュースJsonオブジェクトリスト
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin.Sachin
//=======================================================================
using System;
using System.Runtime.InteropServices;
using Liplis.Common;

namespace Liplis.Msg
{
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class ResLpsShortNews2JsonList
    {
        ///=============================
        ///プロパティ
        public LstShufflableList<ResLpsShortNews2Json> lstNews { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsShortNews2JsonList
        public ResLpsShortNews2JsonList()
        {
            this.lstNews = new LstShufflableList<ResLpsShortNews2Json>();
        }
        public ResLpsShortNews2JsonList(string url, LstShufflableList<ResLpsShortNews2Json> lstNews)
        {
            this.lstNews = lstNews;
        }
        #endregion
    }
}
