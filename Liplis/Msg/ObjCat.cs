//=======================================================================
//  ClassName : ObjCat
//  概要      : CATオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;

namespace Liplis.Msg
{
    [Serializable]
    public class ObjCat
    {
        ///============================
        /// RSS情報
        public List<string> catList { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ObjCat
        public ObjCat()
        {
            this.catList = new List<string>();
        }
        public ObjCat(string title, string url, string cat)
        {
            this.catList = new List<string>();
        }
        #endregion
    }
}
