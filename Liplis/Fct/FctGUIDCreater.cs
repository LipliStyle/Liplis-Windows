//=======================================================================
//  ClassName : FctGUIDCreater
//  概要      : GUIDファクトリー
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Fct
{
    public static class FctGUIDCreater
    {
        /// <summary>
        /// createLiplisGuid
        /// 独自Guidを作成する
        /// </summary>
        #region createLiplisGuid
        public static string createLiplisGuid()
        {
            return Guid.NewGuid().ToString() + String.Format("{0:000}", DateTime.Now.Millisecond);
        }
        #endregion

        /// <summary>
        /// createGuid
        /// Guidを作成する
        /// </summary>
        #region createGuid
        public static string createGuid()
        {
            return Guid.NewGuid().ToString();
        }
        #endregion
    }
}
