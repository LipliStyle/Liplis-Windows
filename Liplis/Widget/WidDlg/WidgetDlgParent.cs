//=======================================================================
//  ClassName : frmParent
//  概要      : こちらに透過するオブジェクトを置く
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using Liplis.Activity;
using Liplis.Common;
using Liplis.MainSystem;
using Liplis.Msg;
using System.IO;

namespace Liplis.Widget.WidDlg
{
    public partial class WidgetDlgParent : WidgetBaseParent
    {

        ///=============================
        /// ファイル情報
        private Liplis.MainSystem.Liplis lips;
        private Stream stream;
        private string fileName;
        private long fileSize;
        private DataGridViewRow dgv;

        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region WidgetDlgParent
        public WidgetDlgParent(Liplis.MainSystem.Liplis lips, ObjWidgetSetting o, WidgetBaseSetting s, Stream stream, string fileName, long fileSize, DataGridViewRow dgv)
        {
            this.lips     = lips;
            this.stream   = stream;
            this.fileName = fileName;
            this.fileSize = fileSize;
            this.dgv      = dgv;

            this.o = o;
            this.a = a;
            this.s = s;
            this.initBaseForm();        //初期化
            this.loadBaseForm();        //子クラスでのロード

            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// ベースフォームをインスタンスかするメソッドを定義する
        /// </summary>
        #region loadBaseForm
        protected override void loadBaseForm()
        {
            f2 = new WidgetDlgBase(lips, o, stream,fileName,fileSize, this.dgv);
        }
        #endregion


        ///====================================================================
        ///
        ///                             onRecive
        ///                         
        ///====================================================================


        #region イベント
        private void WidgetCpuParent_MouseDown(object sender, MouseEventArgs e) { mouseDown(e); }
        private void WidgetCpuParent_MouseMove(object sender, MouseEventArgs e) { mouseMoveWidget(e); }
        #endregion

        #region frmParent_Load
        private void frmParent_Load(object sender, EventArgs e)
        {
            parentFormInit();
            initBackGroundLabel();
        }
        #endregion 

        /// <summary>
        /// イニットスレッド
        /// </summary>
        #region doInitThread
        public void doInitThread()
        {
            WidgetDlgBase f = (WidgetDlgBase)f2;
            f.doInitThread();
        }
        #endregion


    }
}
