//=======================================================================
//  Liplis 3.1.0
//  ClassName : ResLpsTopicSearchWord
//  概要      : リプリス設定情報 検索設定
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
    public class ResLpsTopicSearchWord
    {
        #region プロパティ
        public int topicId { get; set; }
        public string word { get; set; }
        public int flgEnable { get; set; }
        #endregion

        #region コンストラクター
        public ResLpsTopicSearchWord()
        {
        }
        public ResLpsTopicSearchWord(int topicId, string word, int flgEnable)
        {
            this.topicId = topicId;
            this.word = word;
            this.flgEnable = flgEnable;
        }
        #endregion
    }
}
