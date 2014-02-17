//=======================================================================
//  Liplis 3.1.0
//  ClassName : ResLpsTopicSearchWordList
//  概要      : リプリス設定情報 検索設定リスト
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Liplis.Msg
{
    [SerializableAttribute]
    [ComVisibleAttribute(true)]
    public class ResLpsTopicSearchWordList
    {
        #region プロパティ
        public List<ResLpsTopicSearchWord> wordList { get; set; }
        #endregion

        #region コンストラクター
        public ResLpsTopicSearchWordList()
        {
            wordList = new List<ResLpsTopicSearchWord>();
        }
        public ResLpsTopicSearchWordList(List<ResLpsTopicSearchWord> wordList)
        {
            this.wordList = wordList;
        }
        #endregion
    }
}
