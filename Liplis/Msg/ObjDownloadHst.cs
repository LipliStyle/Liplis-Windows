//=======================================================================
//  ClassName : LiplisContentDownloder
//  概要      : コンテンツダウンローダー
//
//  Liplis2.0
//  Copyright(c) 2010-2012 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using Liplis.Msg;

namespace Liplis.MainSystem
{
    [Serializable]
    public class ObjDownloadHst
    {
        ///============================
        /// ダウンロードキュー
        public List<ObjDownloadFile> downList { get; set; }

        /// <summary>
        /// LiplisContentDownloder
        /// コンストラクター
        /// </summary>
        #region LiplisContentDownloder
        public ObjDownloadHst()
        {
            initList();
        }
        #endregion

        /// <summary>
        /// リストの初期化
        /// </summary>
        #region initList
        public void initList()
        {
            downList = new List<ObjDownloadFile>();
        }
        #endregion
    }
}
