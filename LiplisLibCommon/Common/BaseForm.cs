//=======================================================================
//  ClassName : frmMain
//  概要      : メインフォーム
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Liplis.Common
{
    public partial class BaseForm : Form
    {
        ///=============================
        ///定数
        protected string revision { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region BaseForm
        public BaseForm()
        {
            this.revision = LpsLiplisUtil.getVirsion();
        }
        #endregion

        /// <summary>
        /// タイトルを設定する
        /// </summary>
        #region setTitle
        public void setTitle(string formName, string msg)
        {
            this.Text = formName + " - ver." + this.revision + " " + msg;
        }
        #endregion

    }
}
