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
using System.Windows.Forms;

namespace Liplis.Widget.WidRss
{
    public partial class WidgetRssBase : WidgetBaseBase
    {
        ///=====================================
        /// プロパティ
        private string              url;
        private int                 interval;
        private RssReader           rr;
        private ObjWidgetSetting    o;
        private WidgetRssSetting    s;

        /// <summary>
        /// WidgetRss12Base
        /// コンストラクター
        /// </summary>
        /// <param name="o"></param>
        /// <param name="url"></param>
        /// <param name="interval"></param>
        #region WidgetRss11Base
        public WidgetRssBase(ObjWidgetSetting o,WidgetRssSetting s)
        {
            this.url      = s.url;
            this.interval = s.interval;
            this.o        = o;
            this.s        = s;
            this.ctrlCheckFlg = LpsLiplisUtil.boolToBit(o.ctlRock);

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
            this.pnlRss.Size = new Size(s.size.Width * 160 - 8, s.size.Height * 160-20);
            this.MouseEnter += new EventHandler(mouseEnter);
            this.pnlRss.MouseWheel += new System.Windows.Forms.MouseEventHandler(winChatLog_MouseWheel);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.pnlRss.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblTitle.ForeColor = o.widgetTitleColor;
            this.lblTitle.BackColor = Color.Transparent;
            this.lblTitle.Parent = this;
        }
        #endregion

        /// <summary>
        /// タイマーを更新する
        /// </summary>
        #region initTimer
        internal void initTimer()
        {
            this.timUpdate.Interval = this.interval;
            this.timUpdate.Start();
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
        private void WidgetRssMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) { this.mouseDown(e);}
        private void WidgetRssMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) { this.mouseMoveWidget(e); }
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
        /// winChatLog_MouseWheel
        /// ホイールイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region winChatLog_MouseWheel
        private void winChatLog_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int scr = pnlRss.VerticalScroll.Value - (e.Delta / 120);

            if (pnlRss.VerticalScroll.Maximum <= scr)
            {
                scr = pnlRss.VerticalScroll.Maximum;
            }

            if (pnlRss.VerticalScroll.Minimum >= scr)
            {
                scr = pnlRss.VerticalScroll.Minimum;
            }


            pnlRss.VerticalScroll.Value = scr;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region timUpdate_Tick
        private void timUpdate_Tick(object sender, EventArgs e)
        {
            update();
        }
        #endregion


        /// <summary>
        /// update
        /// RSSを更新して読み直す
        /// </summary>
        #region update
        internal void update()
        {
            int idx = 0;

            //RSSリード
            rr = new RssReader(url);

            //一度クリアする
            this.pnlRss.Controls.Clear();

            //タイトルの設定
            this.lblTitle.Text      = rr.title;
            this.lblTitle.Tag       = url;

            //まわして更新
            foreach (string title in rr.urlTitleList)
            {
                try
                {
                    CusCtlPanel pn = createPnl();
                    CusCtlPictureBox p = createPic(idx);
                    CusCtlLinkLabel lnk = createLnkLbl(idx, title, p.Width);

                    pn.Controls.Add(p);
                    pn.Controls.Add(lnk);
                    pn.Tag = lnk;
                    this.pnlRss.Controls.Add(pn);
                }
                catch
                {

                }
                idx++;
            }
        }
        #endregion


        #region クリエイトコントロール
        private CusCtlLinkLabel createLnkLbl(int idx, string title, int pWid)
        {
            CusCtlLinkLabel lnk = new CusCtlLinkLabel();
            lnk.Tag = rr.urlList[idx];
            lnk.AutoSize = false;
            lnk.Text = title;
            lnk.Width = this.pnlRss.Width - 40;
            lnk.LinkColor = o.widgetLinkColor;
            lnk.VisitedLinkColor = o.widgetForeColor;
            lnk.Tag = rr.urlList[idx];
            lnk.Click += new EventHandler(this.LinkLblClick);
            lnk.MouseWheel += new System.Windows.Forms.MouseEventHandler(winChatLog_MouseWheel);
            lnk.MouseEnter += new EventHandler(mouseEnter);
            lnk.Location = new Point(pWid + 2, 0);
            return lnk;
        }
        private CusCtlPictureBox createPic(int idx)
        {
            CusCtlPictureBox p = new CusCtlPictureBox();
            p.Image = Fct.FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_BTN_LNK);
            p.Size = new Size(12, 12);
            p.Tag = rr.urlList[idx];
            p.Location = new Point(0, 0);
            p.Click += new EventHandler(this.LinkPicClick);
            return p;
        }
        private CusCtlPanel createPnl()
        {
            CusCtlPanel pn = new CusCtlPanel();
            pn.Width = this.pnlRss.Width - 25;
            pn.Height = 12;
            pn.MouseWheel += new System.Windows.Forms.MouseEventHandler(winChatLog_MouseWheel);
            pn.MouseEnter += new EventHandler(mouseEnter);
            pn.Click += new EventHandler(this.LinkPnlClick);
            pn.Cursor = System.Windows.Forms.Cursors.Hand;
            return pn;
        }
        #endregion

        /// <summary>
        /// LinkLblClick
        /// リンクラベルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region LinkLblClick
        public void LinkLblClick(object sender, System.EventArgs e)
        {
            if (ctrlCheck()) { return; }
            CusCtlLinkLabel l = (CusCtlLinkLabel)sender;

            new LpsDelegate.dlgS1ToVoid(doProcess).BeginInvoke((string)l.Tag,null, null);
        }
        private void LinkPnlClick(object sender, System.EventArgs e)
        {
            if (ctrlCheck()) { return; }
            CusCtlPanel p = (CusCtlPanel)sender;
            CusCtlLinkLabel l = (CusCtlLinkLabel)p.Tag;

            new LpsDelegate.dlgS1ToVoid(doProcess).BeginInvoke((string)l.Tag, null, null);
        }
        private void LinkPicClick(object sender, System.EventArgs e)
        {
            if (ctrlCheck()) { return; }
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
