//=======================================================================
//  ClassName : LiplisTreeNodePar
//  概要      : ツリーノード(親)
//
//  Liplis2.3   
//  Copyright(c) 2010-2013 sachin.Sachin
//=======================================================================
using System.Windows.Forms;
using Liplis.Msg;

namespace Liplis.Cmp.Tree
{
    public class LiplisTreeNodePar : TreeNode
    {
        ///==========================
        /// プロパティ
        public ObjRssCatList catList { get; set; }
        public string cat { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region LiplisTreeNodePar
        public LiplisTreeNodePar(ObjRssCatList catList, string cat)
            : base(cat)
        {
            this.cat = cat;
            this.catList = catList;
        }
        #endregion
    }
}
