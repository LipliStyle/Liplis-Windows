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
    public class ResLpsSummaryNews2JsonList
    {
        ///=============================
        ///プロパティ
        public LstShufflableList<ResLpsSummaryNews2Json> lstNews { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ResLpsShortNews2JsonList
        public ResLpsSummaryNews2JsonList()
        {
            this.lstNews = new LstShufflableList<ResLpsSummaryNews2Json>();
        }
        public ResLpsSummaryNews2JsonList(string url, LstShufflableList<ResLpsSummaryNews2Json> lstNews)
        {
            this.lstNews = lstNews;
        }
        #endregion
    }
}
