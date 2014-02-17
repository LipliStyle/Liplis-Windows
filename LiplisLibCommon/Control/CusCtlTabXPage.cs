//=======================================================================
//  ClassName : CusCtlTabXPage
//  概要      : カスタムタブページ
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin. All Rights Reserved. 
//=======================================================================
using System.Windows.Forms;

namespace Liplis.Control
{
    public class CusCtlTabXPage : TabPage
    {



        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public CusCtlTabXPage()
        {
            ///透明化
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        #endregion
        



    }
}
