//=======================================================================
//  ClassName : ObjRssCatList
//  概要      : RSSカテゴリーオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Msg
{
    [Serializable]
    public class ObjRssCatList
    {
        public int              maxIdx  { get; set; }
        public string           cat     { get; set; }
        public List<ObjRss>     rssList { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ObjRssCatList
        public ObjRssCatList(string cat)
        {
            this.maxIdx = 0;
            this.cat = cat;
            this.rssList = new List<ObjRss>();
        }
        #endregion
    }
}
