//=======================================================================
//  ClassName : frmBase
//  概要      : こちらに透過しないオブジェクトを置く
//              親側からよば
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using Liplis.Common;
using Liplis.Control;
using Liplis.Msg;
using Liplis.Xml;

namespace Liplis.Widget.WidBrw
{
    public partial class WidgetBrwBase : WidgetBaseBase
    {
        string url;
        int interval;
        ObjWidgetSetting o;
        WidgetBrwSetting s;

        /// <summary>
        /// WidgetRss12Base
        /// コンストラクター
        /// </summary>
        /// <param name="o"></param>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        #region WidgetBrowserBase
        public WidgetBrwBase(ObjWidgetSetting o, WidgetBrwSetting s)
        {
            this.url      = s.url;
            this.interval = s.interval;
            this.o        = o;
            this.s        = s;
            InitializeComponent();
            initWindow();
        }
        #endregion

        /// <summary>
        /// initWindow
        /// ウインドウ設定の初期化
        /// </summary>
        #region initWindow
        internal void initWindow()
        {
            this.Size = new System.Drawing.Size(s.size.Width * 160, s.size.Height * 160);
            this.MouseEnter += new EventHandler(mouseEnter);
            this.lblTitle.ForeColor = o.widgetTitleColor;
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Parent = this;
        }
        #endregion

        ///====================================================================
        ///
        ///                          イベントハンドラ
        ///                         
        ///====================================================================

        /// <summary>
        /// マウスムーブ
        /// </summary>
        #region マウスムーブ
        private void WidgetRss12Base_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) { this.mouseDown(e);}
        private void WidgetRss12Base_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) { this.mouseMove(e);}
        #endregion

        /// <summary>
        /// マウスエンター
        /// </summary>
        #region mouseEnter
        private void mouseEnter(object sender, EventArgs e)
        {
            //this.pnlRss.Focus();
        }
        #endregion

        /// <summary>
        /// ラベルタイトルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region lblTitle_Click
        private void lblTitle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// LinkLblClick
        /// リンクラベルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region LinkLblClick
        private void LinkLblClick(object sender, System.EventArgs e)
        {
            CusCtlLinkLabel l = (CusCtlLinkLabel)sender;

            new LpsDelegate.dlgS1ToVoid(doProcess).BeginInvoke((string)l.Tag,null, null);
        }
        private void LinkPicClick(object sender, System.EventArgs e)
        {
            CusCtlPictureBox l = (CusCtlPictureBox)sender;

            new LpsDelegate.dlgS1ToVoid(doProcess).BeginInvoke((string)l.Tag, null, null);
        }
        #endregion

        /// <summary>
        /// doProcess
        /// ブラウザを呼び出す
        /// </summary>
        #region doProcess
        protected void doProcess()
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception fileNotFoundErr)
            {
                Console.Write(fileNotFoundErr);
            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
        }
        protected void doProcess(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception fileNotFoundErr)
            {
                Console.Write(fileNotFoundErr);
            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
        }
        #endregion







    }
}
