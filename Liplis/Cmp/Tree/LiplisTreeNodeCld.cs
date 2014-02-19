//=======================================================================
//  ClassName : LiplisTreeNodeCld
//  概要      : ツリーノード(子)
//
//  Liplis2.3   
//  Copyright(c) 2010-2013 sachin.Sachin
//=======================================================================
using System.Windows.Forms;
using Liplis.Msg;

namespace Liplis.Cmp.Tree
{
    public class LiplisTreeNodeCld : TreeNode
    {
        ///==========================
        /// プロパティ
        public ObjRss rss { get; set; }
        public string title { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region LiplisTreeNodeCld
        public LiplisTreeNodeCld() : base()
        {

        }
        public LiplisTreeNodeCld(ObjRss rss, string title)
            : base(title)
        {
            this.title = title;
            this.rss = rss;
        }
        #endregion
    }
}
