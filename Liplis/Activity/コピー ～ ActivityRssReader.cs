//=======================================================================
//  ClassName : ActivityRssReader
//  概要      : RSSリーダー
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Liplis.Cmp.Form;
using Liplis.Cmp.Tree;
using Liplis.Common;
using Liplis.Fct;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Ser;
using Liplis.Sys;
using Liplis.Widget;
using Liplis.Widget.WidBrw;
using Liplis.Widget.WidCpu;
using Liplis.Widget.WidHdd;
using Liplis.Widget.WidLan;
using Liplis.Widget.WidMem;
using Liplis.Widget.WidRss;
using Liplis.Widget.WidSys;
using Liplis.Xml;
using Liplis.Control;
using Liplis.Web.Nico;
using System.Threading;

namespace Liplis.Activity
{
    public partial class ActivityRssReader : BaseSystem
    {
        ///=====================================
        /// リプリスオブジェクト
        private Liplis.MainSystem.Liplis lips;

        ///=====================================
        /// オブジェクト
        private ObjRssList                  rssList;
        private ObjSetting                  os;
        private ObjWidgetSetting            ows;
        private ObjBroom                    obr;

        ///=====================================
        /// クラス
        private NetworkInfoClass            nic;

        ///=====================================
        /// ウィジェットオブジェクト
        internal WidgetSettingList          owml;
        internal List<WidgetBaseParent>     widgetList;

        ///=====================================
        /// リスト
        private List<CusCtlRssPanel>        rssPanelList;
        private List<string>                histryList;
        private Dictionary<int, string>     nicList;

        ///=====================================
        /// RSSディクショナリー
        private Dictionary<int, string >    rssDicTitle;
        private Dictionary<int, string>     rssDicUrl;


        ///=====================================
        /// フラグ
        private bool flgEnd = false;
        private bool flgNicoSearch = false;

        ///============================
        /// デリゲート
        #region デリゲート
        private static LpsDelegate.dlgBmpToVoid reqSetBackGround;
        #endregion

        ///=====================================
        /// 前回値
        private string prvUrl = " ";

        ///=====================================
        /// ニコニコ検索
        private string opt = LpsDefineMost.NICO_SEARCH_OPT_WORD;
        private Thread nicoSearchThread;

        ///====================================================================
        ///
        ///                       onCreate
        ///                         
        ///====================================================================

        //コンストラクター
        #region ActivityRssReader : base()
        public ActivityRssReader(Liplis.MainSystem.Liplis lips, ObjSetting os, ObjRssList rssList)
            : base()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.lips          = lips;
            this.os            = os;
            this.rssList       = rssList;
            this.loadObject();
            this.initClass();
            this.intList();
            this.initSettingWindow();
            this.initRssTab();
            this.initSetting();
            this.initDelegate();
            this.intWidgetColor();
            this.intcombo();
            this.intWebBrowser();
            this.initSummary();
            this.initDownloader();
            this.initCmbRegion();

            this.loadWidget();
            this.intRssCombo();

            this.lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.components = new System.ComponentModel.Container();
        }
        #endregion


        /// <summary>
        /// loadObject
        /// オブジェクトのロード
        /// </summary>
        #region loadObject
        private void loadObject()
        {
            this.ows = SerialWidgetSetting.loadObject();
            this.owml = SerialWidgetList.loadObject();
        }
        #endregion

        /// <summary>
        /// initClass
        /// クラスの初期化
        /// </summary>
        #region initClass
        private void initClass()
        {
            nic = new NetworkInfoClass();
        }
        #endregion


        /// <summary>
        /// loadWidget
        /// ウィジェットのロード
        /// </summary>
        #region loadWidget
        private void loadWidget()
        {
            foreach (WidgetBaseSetting s in LpsLiplisUtil.deepClone<List<WidgetBaseSetting>>(owml.widgetList))
            {
                summonWidgetLoad(s);
            }
        }
        #endregion

        /// <summary>
        /// initRssTab
        /// initRssTabの初期化
        /// </summary>
        #region initRssTab
        private void initRssTab()
        {

        }
        #endregion

        /// <summary>
        /// initSettingWindow
        /// initSettingWindowの初期化
        /// </summary>
        #region initSettingWindow
        private void initSettingWindow()
        {
            rssWb.ScriptErrorsSuppressed = false;
        }
        #endregion

        /// <summary>
        /// initDelegate
        /// delegateの初期化
        /// </summary>
        #region initDelegate
        private void initDelegate()
        {
            //セットバックグラウンド
            reqSetBackGround = new LpsDelegate.dlgBmpToVoid(dlgSetBackGround);
        }
        #endregion

        /// <summary>
        /// initSetting
        /// initSettingの初期化
        /// </summary>
        #region initSetting
        private void initSetting()
        {
            //おしゃべりモード
            switch(os.mode)
            {
                case 0: rdoFrqChangeable.Checked  = true; break;
                case 1: rdoFrqMachen.Checked      = true; break;
                case 2: rdoFrqVerryNoisy.Checked  = true; break;
                case 3: rdoFrqNoisy.Checked       = true; break;
                case 4: rdoFrqTalkative.Checked   = true; break;
                case 5: rdoFrqNormal.Checked      = true; break;
                case 6: rdoFrqQuiet.Checked       = true; break;
                case 7: rdoFrqKeeps.Checked       = true; break;
                case 8: rdoFrqRefined.Checked     = true; break;
                default: rdoFrqChangeable.Checked = true; break;
            }

            //アクティブ度
            switch (os.speed)
            {
                case 0: rboOtenba.Checked  = true; break;
                case 1: rboNormal.Checked  = true; break;
                case 2: rboYukkuri.Checked = true; break;
                case 3: rboEco.Checked     = true; break;
            }

            //ダウンロードパス
            txtSetDownPath.Text = os.downPath;
            chkSetDownNotice.Checked = LpsLiplisUtil.bitToBool(os.downNotice);

            //ニコニコID/パス
            txtSetNicoId.Text = os.nicoId;
            txtSetNicoPass.Text = os.nicoPass;

            chkDiscWindow.Checked = LpsLiplisUtil.bitToBool(os.discWindowOn);
        }
        #endregion

        /// <summary>
        /// intList
        /// リストの初期化
        /// </summary>
        #region intList
        private void intList()
        {
            rssPanelList = new List<CusCtlRssPanel>();
            widgetList   = new List<WidgetBaseParent>();
            histryList   = new List<string>();
            rssDicUrl    = new Dictionary<int, string>();
            rssDicTitle  = new Dictionary<int, string>();
        }
        #endregion

        /// <summary>
        /// intList
        /// リストの初期化
        /// </summary>
        #region intWidgetColor
        private void intWidgetColor()
        {
            this.btnWidgetColorUp.BackColor          = ows.widgetColorUp;
            this.btnWidgetColorUnder.BackColor       = ows.widgetColorUnder;
            this.btnForeColor.BackColor              = ows.widgetForeColor;
            this.btnLinkColor.BackColor              = ows.widgetLinkColor;
            this.cboOpa.Text                         = ows.opacity.ToString();
            this.cboBrowserUpdInterval.SelectedIndex = ows.getBroserInterval();
            this.chkCtrl.Checked                     = ows.ctlRock;
        }
        #endregion
             
        /// <summary>
        /// intcombo
        /// リストの初期化
        /// </summary>
        #region intcombo
        private void intcombo()
        {
            cboTestTate.SelectedIndex = 0;
            cboTestYoko.SelectedIndex = 0;
            cboSysTate.SelectedIndex = 0;
            cboSysYoko.SelectedIndex = 0;
            cboRssTate.SelectedIndex = 0;
            cboRssYoko.SelectedIndex = 0;
            cboBrowserTate.SelectedIndex = 2;
            cboBrowserYoko.SelectedIndex = 2;

            cboHdd.Items.AddRange(Directory.GetLogicalDrives());
            if (cboHdd.Items.Count > 0) { cboHdd.SelectedIndex = 0; }
            nicList = nic.getNicList();         //ネットワークカードのリストを取得する
            foreach (KeyValuePair<int, string> kp in nicList) { cboLan.Items.Add(kp.Value); }
            if (cboLan.Items.Count > 0) { cboLan.SelectedIndex = 0; }
        }
        #endregion

        /// <summary>
        /// intRssCombo
        /// RSSコンボ
        /// </summary>
        #region intcombo
        private void intRssCombo()
        {
            int idx = 0;

            //▼読み込み済みをいったんクリア
            rssDicTitle.Clear();
            rssDicUrl.Clear();
            cboRss.Items.Clear();

            //▼RSSリストを回して、読み込み
            foreach (ObjRssCatList catList in rssList.rssCatList)
            {
                //▼子ノードの作成
                foreach (ObjRss rss in catList.rssList)
                {
                    rssDicTitle.Add(idx,rss.title);
                    rssDicUrl.Add(idx, rss.url);
                    cboRss.Items.Add(rss.title);
                    idx++;
                }
            }
        }
        #endregion

        /// <summary>
        /// intWebBrowser
        /// リストの初期化
        /// </summary>
        #region intWebBrowser
        private void intWebBrowser()
        {
            rssWb.ScriptErrorsSuppressed = true;
        }
        #endregion

        /// <summary>
        /// initSummary
        /// サマリーの初期化
        /// </summary>
        #region initSummary
        private void initSummary()
        {
            this.flp.MouseWheel += new System.Windows.Forms.MouseEventHandler(winChatLog_MouseWheel);
        }
        #endregion


        /// <summary>
        /// initDownloader
        /// ダウンローダーの初期化
        /// </summary>
        #region initDownloader
        private void initDownloader()
        {
            //列が自動的に作成されないようにする
            dgvDownloader.AutoGenerateColumns = false;

            //DataGridViewTextBoxColumn列を作成する
            CusCtlDataGridViewProgressBarColumn prgColumn = new CusCtlDataGridViewProgressBarColumn();
            prgColumn.DataPropertyName = "prg";
            prgColumn.Name       = "prg";
            prgColumn.HeaderText = "ダウン進捗";
            prgColumn.Width      = 100;
            prgColumn.Maximum    = 100;
            prgColumn.Mimimum    = 0;
            //列を追加する
            dgvDownloader.Columns.Add(prgColumn);
        }
        #endregion

        /// <summary>
        /// initCmbRegion
        /// コンボボックスの初期化
        /// </summary>
        #region initCmbRegion
        private void initCmbRegion()
        {
            this.cboRegion.Items.AddRange(LpsDefineRegion.getRegion().ToArray());
        }
        #endregion
        
        ///====================================================================
        ///　onLoad
        ///  リプリスからロードするメソッドを定義する                         
        ///                             
        ///                         
        ///====================================================================

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        /// <param name="os"></param>
        #region loadSetting
        public void loadSetting(ObjSetting os)
        {
            this.Opacity = 1;
            this.os = os;
            setMode();
            setSpeed();
        }
        #endregion

        /// <summary>
        /// reBuildTree
        /// ツリーの再構成
        /// </summary>
        /// <param name="os"></param>
        #region reBuildTree
        public void reBuildTree()
        {
            int nodeIdx = 0;
            List<string> openList = new List<string>();

            //▼このウインドウをロードするためにLiplisから呼ばれている
            this.Opacity = 1;

            //▼ツリービュー更新開始
            tvRss.BeginUpdate();

            //▼開いているツリービュー名を記録する
            foreach (TreeNode p in tvRss.Nodes) { if (p.IsExpanded) { openList.Add(p.Text); } }

            //▼ツリービュー初期化
            tvRss.Nodes.Clear();


            //▼RSSリストを回して、読み込み
            foreach (ObjRssCatList catList in rssList.rssCatList)
            {
                //▼カテゴリ名が空なら、"なし"を登録
                if (catList.cat == "")
                {
                    catList.cat = "なし";
                }

                //▼ノードの作成
                LiplisTreeNodePar tne = new LiplisTreeNodePar(catList, catList.cat);
                tvRss.Nodes.Add(tne);

                //▼子ノードの作成
                foreach (ObjRss rss in catList.rssList)
                {
                    LiplisTreeNodeCld cld = new LiplisTreeNodeCld(rss, rss.title);
                    tvRss.Nodes[nodeIdx].Nodes.Add(cld);
                }

                //▼ノードのインクリメント
                nodeIdx++;
            }

            //▼開いていたツリービューを開きなおす
            foreach (string name in openList)
            {
                foreach (TreeNode p in tvRss.Nodes)
                {
                    if (p.Text.Equals(name))
                    {
                        p.Expand();
                    }
                }
            }

            //▼ツリービュー更新完了
            tvRss.EndUpdate();
        }
        #endregion

        ///====================================================================
        ///
        ///                              onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// onDelete
        /// </summary>
        #region Dispose
        public void dispose()
        {
            SerialWidgetSetting.saveObject(this.ows);
            SerialWidgetList.saveObject(this.owml);
            flgEnd = true;
            this.Close();
        }
        #endregion

        /// <summary>
        /// ActivityRssReader_FormClosing
        /// クロージングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityRssReader_FormClosing
        private void ActivityRssReader_FormClosing(object sender, FormClosingEventArgs e)
        {
            //エンドフラグが有効でなければ、ハイドさせる
            if (!flgEnd)
            {
                e.Cancel = true;
                Invoke(new LpsDelegate.dlgVoidToVoid(this.Hide));
            }
        }

        #endregion

        ///====================================================================
        ///
        ///                           onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// ActivitySetting_MouseDown
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivitySetting_MouseDown
        private void ActivitySetting_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown(e);
        }
        #endregion

        /// <summary>
        /// ActivitySetting_MouseMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivitySetting_MouseMove
        private void ActivitySetting_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMove(e);
        }
        #endregion

        /// <summary>
        /// 通常化
        /// </summary>
        #region onNormalize
        public void onNormalize()
        {
            this.Show();
        }
        #endregion

        /// <summary>
        /// 最小化
        /// </summary>
        #region onMinimize
        public void onMinimize()
        {
            this.Hide();
        }
        #endregion

        /// <summary>
        /// tvRss_AfterSelect
        /// RSSツリーのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tvRss_AfterSelect
        private void tvRss_AfterSelect(object sender, TreeViewEventArgs e)
        {
            onSelect(e);
        }
        #endregion

        /// <summary>
        /// dgv_CellContentClick
        /// RSS記事のクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region dgv_CellContentClick
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            string url = (string)dgv[3, e.RowIndex].Value;

            rssWb.Navigate(url);

            addRssWbHistry(url);

            widowTitleChange(rssWb.DocumentTitle);

        }
        #endregion

        /// <summary>
        /// 1秒置きのプロセス処理
        /// 100msでやるまでもないことをこのプロセスで行う
        /// (処理が冗長だったり、1秒で十分なこと)
        /// </summary>
        #region onProcess
        internal void onProcess()
        {
            //サーフェス
            Invoke(new LpsDelegate.dlgVoidToVoid(setTopicCnt));

            Invoke(new LpsDelegate.dlgVoidToVoid(setTalkCnt));
        }
        #endregion


        ///====================================================================
        ///
        ///                           PanelOnRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// mouseEnter
        /// マウスが上に来たときにはFLPにフォーカスする
        /// (スクロール対策)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region mouseEnter
        private void mouseEnter(object sender, EventArgs e)
        {
            this.flp.Focus();
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
            int scr = flp.VerticalScroll.Value - (e.Delta / 120);

            if (flp.VerticalScroll.Maximum <= scr)
            {
                scr = flp.VerticalScroll.Maximum;
            }

            if (flp.VerticalScroll.Minimum >= scr)
            {
                scr = flp.VerticalScroll.Minimum;
            }


            flp.VerticalScroll.Value = scr;
        }
        #endregion

        ///====================================================================
        ///
        ///                           widgetOnRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// tsmiMdgDelete_Click
        /// ウィジェットの削除クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiMdgDelete_Click
        private void tsmiMdgDelete_Click(object sender, EventArgs e)
        {
            widgetList[dgvWidgetManager.SelectedCells[0].RowIndex].Close();
        }
        #endregion


        /// <summary>
        /// btnWidgetSys_Click
        /// システムウィジェット追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnWidgetSys_Click
        private void btnWidgetSys_Click(object sender, EventArgs e)
        {
            summonWidget(new WidgetSysSetting(LiplisDefine.WIDGET_SYS, LiplisDefine.WIDGET_TYPE_SYS, new Size(1, 1), 0));
        }
        #endregion

        /// <summary>
        /// ブラウザウインドウ追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnBrowser_Click
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            addBrowserWidget();
        }
        #endregion

        /// <summary>
        /// ブラウザウインドウ追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnBrowser_Click
        private void btnRss_Click(object sender, EventArgs e)
        {
            addRssWidget();
        }
        #endregion

        /// <summary>
        /// CPUウインドウ追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnCpu_Click
        private void btnCpu_Click(object sender, EventArgs e)
        {
            summonWidget(new WidgetCpuSetting(LiplisDefine.WIDGET_SYS, LiplisDefine.WIDGET_TYPE_SYS, new Size(1, 1)));
        }
        #endregion

        /// <summary>
        /// メモリウインドウ追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnMem_Click
        private void btnMem_Click(object sender, EventArgs e)
        {
            summonWidget(new WidgetMemSetting(LiplisDefine.WIDGET_MEM, LiplisDefine.WIDGET_TYPE_MEM, new Size(1, 1)));
        }
        #endregion

        /// <summary>
        /// HDDウインドウ追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnHdd_Click
        private void btnHdd_Click(object sender, EventArgs e)
        {
            addHddWidget();
        }
        #endregion

        /// <summary>
        /// ネットワークウインドウ追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnNetwork_Click
        private void btnNetwork_Click(object sender, EventArgs e)
        {
            addLanWidget();
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region button2_Click
        private void button2_Click(object sender, EventArgs e)
        {
            addTestWidget();
        }
        #endregion


        /// <summary>
        /// btnWidgetColor_Click
        /// ウィジェットカラー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnWidgetColor_ClickUp
        private void btnWidgetColor_ClickUp(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.btnWidgetColorUp.BackColor = colorDialog1.Color;
                ows.widgetColorUp = colorDialog1.Color;
                SerialWidgetSetting.saveObject(ows);
            }
        }

        private void btnWidgetColorUnder_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.btnWidgetColorUnder.BackColor = colorDialog1.Color;
                ows.widgetColorUnder = colorDialog1.Color;
                SerialWidgetSetting.saveObject(ows);
            }
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.btnForeColor.BackColor = colorDialog1.Color;
                ows.widgetForeColor = colorDialog1.Color;
                SerialWidgetSetting.saveObject(ows);
            }
        }

        private void btnLinkColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.btnLinkColor.BackColor = colorDialog1.Color;
                ows.widgetLinkColor = colorDialog1.Color;
                SerialWidgetSetting.saveObject(ows);
            }
        }

        private void cboOpa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ows.checkOpacity(cboOpa.Text);
            SerialWidgetSetting.saveObject(ows);
        }

        private void btnTitleColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.btnTitleColor.BackColor = colorDialog1.Color;
                ows.widgetTitleColor = colorDialog1.Color;
                SerialWidgetSetting.saveObject(ows);
            }
        }

        private void chkCtrl_CheckedChanged(object sender, EventArgs e)
        {
            ows.ctlRock = chkCtrl.Checked;
            SerialWidgetSetting.saveObject(ows);
        }


        private void cboBrowserUpdInterval_SelectedIndexChanged(object sender, EventArgs e)
        {

            ows.setBroserInterval(cboBrowserUpdInterval.Text);
            SerialWidgetSetting.saveObject(ows);
        }
        #endregion

        ///====================================================================
        ///
        ///                      動画ダウンローダーOnRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// btnNicoCheck_Click
        /// ニコチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnNicoCheck_Click
        private void btnNicoCheck_Click(object sender, EventArgs e)
        {
            if (LpsLiplisUtil.domainCheck(txtNicoUrl.Text, LpsDefineMost.URL_NICO_DOMAIN) || LpsRegularEx.chekcNicoVideoId(txtNicoUrl.Text))
            {
                createNicoInfo(txtNicoUrl.Text);
            }
            else
            {
                //どちらにも該当しない場合はエラー
                lblNicoMessage.Text = "URLが不正か、動画が見つかりません。";
                return;
            }
        }
        #endregion

        /// <summary>
        /// btnNicoDl_Click
        /// ニコチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnNicoDl_Click
        private void btnNicoDl_Click(object sender, EventArgs e)
        {
            fileDonwload(10);
        }
        #endregion

        /// <summary>
        /// btnNicoMp3_Click
        /// ニコチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnNicoMp3_Click
        private void btnNicoMp3_Click(object sender, EventArgs e)
        {
            fileDonwload(11);
        }
        #endregion

        private void fileDonwload(int code)
        {
            string url = "";
            if (LpsLiplisUtil.domainCheck(txtNicoUrl.Text, LpsDefineMost.URL_NICO_DOMAIN))
            {
                string[] splitStr = txtNicoUrl.Text.Split('/');
                url = LpsDefineMost.URL_NICO_VIDEO + splitStr[splitStr.Length - 1]; ;
            }
            else if (LpsRegularEx.chekcNicoVideoId(txtNicoUrl.Text))
            {
                url = LpsDefineMost.URL_NICO_VIDEO + txtNicoUrl.Text;
            }
            else
            {
                //どちらにも該当しない場合はエラー
                lblNicoMessage.Text = "URLが不正か、動画が見つかりません。";
                return;
            }
            objNicoInfo n = new objNicoInfo(LpsRegularEx.getNicoId(url));
            string downPath = LpsPathController.getSaveFileName(LpsDefineMost.LPS_EXTENSION_FLV, os.downPath, n.title, os.downNotice);
            lips.addDownload(new ObjDownloadFile(url, n.title, downPath, 0.0, code, 0, 0));
        }


        /// <summary>
        /// btnNicoSearchWord_Click
        /// ワード検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnNicoSearchWord_Click
        private void btnNicoSearchWord_Click(object sender, EventArgs e)
        {
            opt = LpsDefineMost.NICO_SEARCH_OPT_WORD;
            nicoSearchWord();
        }
        #endregion

        /// <summary>
        /// btnNicoSearchWord_Click
        /// ワード検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnNicoSearchWord_Click
        private void btnNicoSearchTag_Click(object sender, EventArgs e)
        {
            opt = LpsDefineMost.NICO_SEARCH_OPT_TAG;
            nicoSearchWord();
        }
        #endregion

        /// <summary>
        /// dgvNicoSearch_CellContentClick
        /// DGV選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region dgvNicoSearch_CellContentClick
        private void dgvNicoSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtNicoUrl.Text = (string)dgvNicoSearch[0, e.RowIndex].Value;

                if (LpsLiplisUtil.domainCheck(txtNicoUrl.Text, LpsDefineMost.URL_NICO_DOMAIN) || LpsRegularEx.chekcNicoVideoId(txtNicoUrl.Text))
                {
                    createNicoInfo(txtNicoUrl.Text);
                }
                else
                {
                    //どちらにも該当しない場合はエラー
                    lblNicoMessage.Text = "URLが不正か、動画が見つかりません。";
                    return;
                }
            }
            catch
            {

            }
        }
        #endregion







        ///====================================================================
        ///
        ///                     ウィジェット追加・設定
        ///                         
        ///====================================================================


        /// <summary>
        /// addTestWidget
        /// テストウィジェットを追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region addTestWidget
        private void addTestWidget()
        {
            WidgetBaseSetting s = new WidgetBaseSetting(
                LiplisDefine.WIDGET_TEST,
                LiplisDefine.WIDGET_TYPE_TEST,
                new Size(cboCheck(cboTestYoko.Text, 2), cboCheck(cboTestTate.Text, 2))
                
                );

            summonWidget(s);
        }
        #endregion

        /// <summary>
        /// addBrowserWidget
        /// ブラウザウィジェットを追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region addBrowserWidget
        private void addBrowserWidget()
        {
            int tate = cboCheck(cboBrowserTate.Text, 9);
            int yoko = cboCheck(cboBrowserYoko.Text, 9);

            if (txtBrowserUrl.Text.Equals(""))
            {
                MessageBox.Show("URLを入力してください", LiplisDefine.LIPLIS);
                return;
            }

            summonWidget(new WidgetBrwSetting(LiplisDefine.WIDGET_BRW, LiplisDefine.WIDGET_TYPE_BRW, new Size(tate, yoko), txtBrowserUrl.Text, ows.getBroserInterval2(),chkAutoUpdate.Checked));
        }
        #endregion

        /// <summary>
        /// addRssWidget
        /// RSSウィジェットを追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region addRssWidget
        private void addRssWidget()
        {
            if (cboRss.Text.Equals(""))
            {
                MessageBox.Show("RSSを選択して下さい。", LiplisDefine.LIPLIS);
                return;
            }

            WidgetRssSetting s = new WidgetRssSetting(
                LiplisDefine.WIDGET_RSS + " - " + cboRss.Text,
                LiplisDefine.WIDGET_TYPE_RSS,
                new Size(cboCheck(cboRssYoko.Text, 9), cboCheck(cboRssTate.Text, 9)),
                getRssUrl(),
                ows.getBroserInterval2()
                );
            summonWidget(s);
        }
        #endregion

        /// <summary>
        /// addHddWidget
        /// HDDウィジェットを追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region addHddWidget
        private void addHddWidget()
        {
            if (cboHdd.Text.Equals(""))
            {
                MessageBox.Show("HDDを選択して下さい。", LiplisDefine.LIPLIS);
                return;
            }

            summonWidget(new WidgetHddSetting(LiplisDefine.WIDGET_HDD, LiplisDefine.WIDGET_TYPE_HDD, new Size(1, 1), cboHdd.Text));
        }
        #endregion

        /// <summary>
        /// addHddWidget
        /// HDDウィジェットを追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region addLanWidget
        private void addLanWidget()
        {
            if (cboLan.Text.Equals(""))
            {
                MessageBox.Show("LANを選択して下さい。", LiplisDefine.LIPLIS);
                return;
            }

            summonWidget(new WidgetLanSetting(LiplisDefine.WIDGET_LAN, LiplisDefine.WIDGET_TYPE_LAN, new Size(1, 1), cboLan.SelectedIndex, cboLan.Text));
        }
        #endregion

        /// <summary>
        /// summonWidget
        /// ウィジェットをデスクトップ上に召還する
        /// </summary>
        #region summonWidget
        private void summonWidget(WidgetBaseSetting s)
        {
            summonWidget(WidgetMap.getWidget(ows, this, s), s);     //　ウィジェットを取得する
            owml.addSetting(s);                                     //　セッティングリストに追加
        }
        private void summonWidget(WidgetBaseParent w, WidgetBaseSetting s)
        {
            w.d = addDgv(s);                                            //  ウィジェットにDGVを付与
            widgetList.Add(w);                                          //　管理用ウィジェットリストに追加
            w.Show();                                                   //　召還
        }
        //再呼び出し時
        private void summonWidgetLoad(WidgetBaseSetting s)
        {
            summonWidget(WidgetMap.getWidget(ows, this, s), s);     //　ウィジェットを取得する
        }
        #endregion

        /// <summary>
        /// cboCheck
        /// コンボボックスの入力値の妥当性を確認する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region cboCheck
        private int cboCheck(string val, int max)
        {
            try
            {
                //パース
                int iVal = int.Parse(val);

                //最小値チェック
                if (iVal < 1)
                {
                    iVal = 1;
                }
                //最大値チェック
                else if (iVal > max)
                {
                    iVal = max;
                }
                //チェックした妥当値を返す。
                return iVal;
            }
            catch
            {
                //デフォルトで1は保障
                return 1;
            }
        }
        #endregion        

        /// <summary>
        /// addDgv
        /// DGVに追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region addDgv
        private DataGridViewRow addDgv(WidgetBaseSetting s)
        {
            try
            {
                dgvWidgetManager.Rows.Add(new object[] { getIcon(s), s.title, s.getSizeStr(), s.rect.X, s.rect.Y });
                return dgvWidgetManager.Rows[dgvWidgetManager.Rows.Count - 1];
            }
            catch
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// getIcon
        /// アイコンを一つ取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region getIcon
        private Bitmap getIcon(WidgetBaseSetting s)
        {
            try
            {
                switch(s.kbn)
                {
                    case LiplisDefine.WIDGET_TYPE_TEST:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_TES);
                    case LiplisDefine.WIDGET_TYPE_SYS:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_SYS);
                    case LiplisDefine.WIDGET_TYPE_RSS:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_RSS);
                    case LiplisDefine.WIDGET_TYPE_BRW:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_BRW);
                    case LiplisDefine.WIDGET_TYPE_WTH:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_WET);
                    case LiplisDefine.WIDGET_TYPE_CPU:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_CPU);
                    case LiplisDefine.WIDGET_TYPE_MEM:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_MEM);
                    case LiplisDefine.WIDGET_TYPE_HDD:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_HDD);
                    case LiplisDefine.WIDGET_TYPE_LAN:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_NET);
                    default:
                        return FctCreateFromResource.getTranse();
                }
            }
            catch
            {
                return FctCreateFromResource.getTranse();
            }
        }

        #endregion

        /// <summary>
        /// getRssUrl
        /// RSSURL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region getRssUrl
        private string getRssUrl()
        {
            string url = "";
            try
            {
                rssDicUrl.TryGetValue(cboRss.SelectedIndex, out url);
                return url;
            }
            catch
            {

            }
            return "";
        }
        #endregion

        

        ///====================================================================
        ///
        ///                         ブラウザ操作
        ///                         
        ///====================================================================

        #region ブラウザ操作
 
        /// <summary>
        /// btnRssBrBack_Click
        /// 戻る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRssBrBack_Click(object sender, EventArgs e)
        {
            rssWb.GoBack();
        }

        /// <summary>
        /// btnRssBrNext_Click
        /// 次へ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRssBrNext_Click(object sender, EventArgs e)
        {
            rssWb.GoForward();
        }

        /// <summary>
        /// btnUpdate_Click
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            rssWb.Refresh();
        }

        /// <summary>
        /// btnStop_Click
        /// 中止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            rssWb.Stop();
        }




        #endregion


        /// <summary>
        /// addRssWbHistry
        /// rssブラウザの履歴管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region addRssWbHistry
        private void addRssWbHistry(string url)
        {
            //URLの空チェック
            if (url.Equals("")) { return; }

            //履歴の追加
            histryList.Add(url);

            //最初の履歴を削除
            if (cboUrl.Items.Count >= 10)
            {
                cboUrl.Items.RemoveAt(0);
            }

            //空じゃなければ追加
            if (!cboUrl.Text.Equals(""))
            {
                //1つ前を入力
                cboUrl.Items.Add(cboUrl.Text);
            }

            //URL表示
            cboUrl.Text = url;
        }
        #endregion
       
        

        ///====================================================================
        ///
        ///                           ログの追加
        ///                         
        ///====================================================================

        /// <summary>
        /// addLog
        /// ログの追加
        /// </summary>
        #region addLog
        public void addLog(MsgShortNews msg, Bitmap charBody)
        {
            addPanel(msg.url, msg.title, msg.converted, msg.jpgUrl, msg.newsEmotion, msg.newsPoint, charBody);
        }
        #endregion

        /// <summary>
        /// addLog
        /// ログの追加
        /// </summary>
        #region addPanel
        private void addPanel(string url, string title, string discription, string jpgPath, int newsEmotion, int newsPoint, Bitmap charBody)
        {
            //前回値と同じなら登録しない
            if (url.Equals(prvUrl)) { return; }

            //データパネル
            DataPanel d;

            //前回値上書き
            prvUrl = url;

            //500件目の破棄
            if (flp.Controls.Count >= 500)
            {
                //500件目のパネルを取得
                DataPanel dc = (DataPanel)flp.Controls[499];

                //ごみすて
                try
                {
                     obr.deleteTargetFile(dc.jpgPath);
                }
                catch
                {
                
                }

                //破棄
                dc.dispose();

                //flpから追放
                flp.Controls.RemoveAt(499);
            }

            //新規要素の追加
            if (!jpgPath.Equals(""))
            {
                d = new DataPanel(url, title, discription, jpgPath, newsEmotion, newsPoint, charBody, new System.EventHandler(this.mouseEnter), this.components);
            }
            else
            {
                d = new DataPanelNonThum(url, title, discription, newsEmotion, newsPoint, charBody,new System.EventHandler(this.mouseEnter), this.components);

            }

            //アッドする
            flp.Controls.Add(d);
            flp.Controls.SetChildIndex(d, 0);

            this.Refresh();
        }
        #endregion


        ///====================================================================
        ///
        ///                            モード変更
        ///                         
        ///====================================================================
        #region setting モード変更
        private void rdoFrqMachen_CheckedChanged(object sender, EventArgs e)
        {
            picFrq.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.FRQ_MACHEN);
            os.setMode(1);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rdoFrqVerryNoisy_CheckedChanged(object sender, EventArgs e)
        {
            picFrq.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.FRQ_VERRYNOISY);
            os.setMode(2);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rdoFrqNoisy_CheckedChanged(object sender, EventArgs e)
        {
            picFrq.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.FRQ_NOISY);
            os.setMode(3);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rdoFrqTalkative_CheckedChanged(object sender, EventArgs e)
        {
            picFrq.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.FRQ_TALKACTIV);
            os.setMode(4);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rdoFrqNormal_CheckedChanged(object sender, EventArgs e)
        {
            picFrq.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.FRQ_NORMAL);
            os.setMode(5);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rdoFrqQuiet_CheckedChanged(object sender, EventArgs e)
        {
            picFrq.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.FRQ_QUIET);
            os.setMode(6);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rdoFrqKeeps_CheckedChanged(object sender, EventArgs e)
        {
            picFrq.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.FRQ_KEEPS);
            os.setMode(7);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rdoFrqRefined_CheckedChanged(object sender, EventArgs e)
        {
            picFrq.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.FRQ_REFINED);
            os.setMode(8);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rdoFrqReticent_CheckedChanged(object sender, EventArgs e)
        {
            picFrq.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.FRQ_RETIVENT);
            os.setMode(9);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rdoFrqChangeable_CheckedChanged(object sender, EventArgs e)
        {
            picFrq.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.FRQ_CHANGEABLE);
            os.setMode(0);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void chkSetDownNotice_CheckedChanged(object sender, EventArgs e)
        {
            os.downNotice = LpsLiplisUtil.boolToBit(chkSetDownNotice.Checked);
            os.setPreferenceData();
        }

        private void txtSetDownPath_TextChanged(object sender, EventArgs e)
        {
            os.downPath = txtSetDownPath.Text;
            os.setPreferenceData();
        }

        private void txtSetNicoId_TextChanged(object sender, EventArgs e)
        {
            os.nicoId = txtSetNicoId.Text;
            os.setPreferenceData();
        }

        private void txtSetNicoPass_TextChanged(object sender, EventArgs e)
        {
            os.nicoPass = txtSetNicoPass.Text;
            os.setPreferenceData();
        }


        private void chkDiscWindow_CheckedChanged(object sender, EventArgs e)
        {
            os.discWindowOn = LpsLiplisUtil.boolToBit(chkDiscWindow.Checked);
            os.setPreferenceData();
        }

        #endregion

        ///====================================================================
        ///
        ///                       スピード変更
        ///                         
        ///====================================================================
        #region setting スピード変更
        private void rboOtenba_CheckedChanged(object sender, EventArgs e)
        {
            os.setSpeed(0);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rboNormal_CheckedChanged(object sender, EventArgs e)
        {
            os.setSpeed(1);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rboYukkuri_CheckedChanged(object sender, EventArgs e)
        {
            os.setSpeed(2);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }

        private void rboEco_CheckedChanged(object sender, EventArgs e)
        {
            os.setSpeed(3);
            os.setPreferenceData();
            lips.onRecive(LiplisDefine.LM_LOAD_SETTING, LiplisDefine.LMP_NONOP);
        }
        #endregion


        ///====================================================================
        ///
        ///                       メニューの出し方
        ///                         
        ///====================================================================
        #region メニューの出し方
        private void rboMeneActMouthOn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rboMeneActRigthClick_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rboMeneBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rboMeneCricle_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        ///====================================================================
        ///
        ///                       メニューの出し方
        ///                         
        ///====================================================================
        #region 地域設定
        private void cboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion


        ///====================================================================
        ///
        ///                       ウインドウ制御
        ///                         
        ///====================================================================

        /// <summary>
        /// setBackGround
        /// 背景のセット
        /// </summary>
        #region setBackGround
        public void setBackGround(Bitmap bmp)
        {
            reqSetBackGround(bmp);
        }
        #endregion


        ///====================================================================
        ///
        ///                  コンポーネントの表示変更処理
        ///                         
        ///====================================================================
        
        /// <summary>
        /// setMode
        /// モードをセットする
        /// </summary>
        #region setMode
        private void setMode()
        {
            switch(os.mode)
            {
                case 1:
                    rdoFrqMachen.Checked = true;
                    break;
                case 2:
                    rdoFrqVerryNoisy.Checked = true;
                    break;
                case 3:
                    rdoFrqNoisy.Checked = true;
                    break;
                case 4:
                    rdoFrqTalkative.Checked = true;
                    break;
                case 5:
                    rdoFrqNormal.Checked = true;
                    break;
                case 6:
                    rdoFrqQuiet.Checked = true;
                    break;
                case 7:
                    rdoFrqKeeps.Checked = true;
                    break;
                case 8:
                    rdoFrqRefined.Checked = true;
                    break;
                case 9:
                    rdoFrqReticent.Checked = true;
                    break;
                case 0:
                    rdoFrqMachen.Checked = true;
                    break;
                default:
                    rdoFrqMachen.Checked = true;
                    break;
            }
        }
        #endregion

        /// <summary>
        /// setSpeed
        /// スピードをセットする
        /// </summary>
        #region setSpeed
        private void setSpeed()
        {
            switch (os.speed)
            {
                case 0:
                    rboOtenba.Checked = true;
                    break;
                case 1:
                    rboNormal.Checked = true;
                    break;
                case 2:
                    rboYukkuri.Checked = true;
                    break;
                case 3:
                    rboEco.Checked = true;
                    break;
                default:
                    rboOtenba.Checked = true;
                    break;
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                     ダウンロード関連
        ///                         
        ///====================================================================

        /// <summary>
        /// addDownload
        /// ダウンロードに追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region addDownload
        public DataGridViewRow addDownload(ObjDownloadFile item)
        {
            try
            {
                DataGridViewProgressBarCell ccdgcpb = new DataGridViewProgressBarCell();
                ccdgcpb.Maximum = 100;
                ccdgcpb.Mimimum = 0;
                ccdgcpb.Value = 0;

                dgvDownloader.Rows.Add(new object[] { getIconExtention(item.kbn), item.title, item.fileSize, ccdgcpb });
                return dgvDownloader.Rows[dgvDownloader.Rows.Count - 1];
            }
            catch
            {
                return null;
            }
        }

        private Bitmap getIconExtention(int s)
        {
            try
            {
                switch (s)
                {
                    case LiplisDefine.WIDGET_TYPE_TEST:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_TES);
                    case LiplisDefine.WIDGET_TYPE_SYS:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_SYS);
                    case LiplisDefine.WIDGET_TYPE_RSS:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_RSS);
                    case LiplisDefine.WIDGET_TYPE_BRW:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_BRW);
                    case LiplisDefine.WIDGET_TYPE_WTH:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_WET);
                    case LiplisDefine.WIDGET_TYPE_CPU:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_CPU);
                    case LiplisDefine.WIDGET_TYPE_MEM:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_MEM);
                    case LiplisDefine.WIDGET_TYPE_HDD:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_HDD);
                    case LiplisDefine.WIDGET_TYPE_LAN:
                        return FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_WID_NET);
                    default:
                        return FctCreateFromResource.getTranse();
                }
            }
            catch
            {
                return FctCreateFromResource.getTranse();
            }
        }
        #endregion

        /// <summary>
        /// delDgvDownload
        /// ダウンロードディージーブイから削除する
        /// </summary>
        /// <param name="d"></param>
        #region delDgvDownload
        public void delDgvDownload(DataGridViewRow d)
        {
            try
            {
                dgvDownloader.Rows.Remove(d);
            }
            catch
            {
                return;
            }
        }
        #endregion

        /// <summary>
        /// クリエイトニコインフォ
        /// </summary>
        #region createNicoInfo
        public void createNicoInfo(string url)
        {
            string htmlStr = "";
            string fixFileName;
            string[] splitStr;

            splitStr = url.Split('/');
            url = splitStr[splitStr.Length - 1];

            fixFileName = LpsPathController.getTempPath() + "nicoDl.html";

            //ファイル読込
            using (StreamWriter fixFile = new StreamWriter(@fixFileName, false, System.Text.Encoding.GetEncoding(932)))
            {
                //HTML作成
                htmlStr = htmlStr + "<HTML>\n";
                htmlStr = htmlStr + "<HEAD>\n";
                htmlStr = htmlStr + "</HEAD>\n";
                htmlStr = htmlStr + "	<BODY>\n";
                htmlStr = htmlStr + "   <iframe src=http://www.nicovideo.jp/thumb?v=" + url + " width=300 height=180  scrolling=no border=0 frameborder=0></iframe>\n";
                htmlStr = htmlStr + "   </BODY>\n";
                htmlStr = htmlStr + "</HTML>\n";

                fixFile.Write(htmlStr);
            }

            wbNico.Navigate(fixFileName);
            wbNico.Refresh();
        }
        #endregion


        /// <summary>
        /// nicoSearch
        /// ニコニコ検索
        /// </summary>
        #region
        private void nicoSearch()
        {
            //宣言
            List<string> nicoVideoIdList = new List<string>();
            string nicoVideoId;
            string word = txtNicoSearchWord.Text;

            //デリゲート
            LpsDelegate.dlgS2ToVoid d = new LpsDelegate.dlgS2ToVoid(addNicoDgv);
            LpsDelegate.dlgVoidToVoid c = new LpsDelegate.dlgVoidToVoid(clearNicoDgv);

            //クリア
            Invoke(c);

            //ダウンロード
            NicoSearch nico = new NicoSearch(os.nicoId, os.nicoPass);

            //ログインチェック
            if (!nico._isLogin)
            {
                //ログインし直す
                nico = new NicoSearch(LiplisDefine.NICO_DEFAULT_ID, LiplisDefine.NICO_DEFAULT_PASS);
                //ログインチェック
                if (!nico._isLogin)
                {
                    //検索失敗
                    MessageBox.Show("ニコニコ動画へのログインに失敗しました。", "Liplis");
                    return;
                }
            }

            //ページ
            for (int page = 1; page <= 50; page++)
            {
                List<string> urlList = nico.getUrlList(word, opt, 2, page);

                foreach (string url in urlList)
                {
                    nicoVideoId = LpsRegularEx.getNicoId(url);

                    if (nicoVideoIdList.IndexOf(nicoVideoId) < 0)
                    {
                        nicoVideoIdList.Add(nicoVideoId);
                        objNicoInfo o =  new objNicoInfo(nicoVideoId);
                        Invoke(d, nicoVideoId, o.title);
                        Application.DoEvents();
                        Thread.Sleep(5);
                    }
                }
            }



        }
        private void nicoSearchWord()
        {
            //スレッドチェック
            if (nicoSearchThread != null && nicoSearchThread.ThreadState == ThreadState.Running)
            {
                nicoSearchThread.Abort();
            }

            //画像作成するスレッドを生成
            nicoSearchThread = new Thread(new ThreadStart(nicoSearch));

            //WebBrowserはシングルスレッドアパートメントモードでのみ実行可能なのでスレッドのモードを設定して実行する
            nicoSearchThread.SetApartmentState(ApartmentState.STA);

            //スレッドスタート
            nicoSearchThread.Start();
        }
        private void addNicoDgv(string nicoVideoId, string title)
        {
            try
            {
                dgvNicoSearch.Rows.Add(new object[] { nicoVideoId, title } );
                this.Refresh();
            }
            catch
            {
                return;
            }
        }
        private void clearNicoDgv()
        {
            dgvNicoSearch.Rows.Clear();
        }
        #endregion


        ///====================================================================
        ///
        ///                       デリゲート
        ///                         
        ///====================================================================

        /// <summary>
        /// dlgSetBackGround
        /// 背景を設定する
        /// </summary>
        #region dlgSetBackGround
        private void dlgSetBackGround(Bitmap bmp)
        {
            this.BackgroundImage = bmp;
        }
        #endregion

        ///====================================================================
        ///
        ///                       処理メソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// onSelect
        /// リストビュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region onSelect
        private void onSelect(TreeViewEventArgs e)
        {
            LiplisTreeNodeCld cld = null;

            //クラス名の取得
            string className = e.Node.GetType().Name;

            //ペアレントの取得
            if (className == "LiplisTreeNodePar")
            {
                

            }
            else if (className == "LiplisTreeNodeCld")
            {
                cld = (LiplisTreeNodeCld)e.Node;
            }

            //ヌルチェック
            if (cld == null) { return; }

            //dgvのクリア
            dgv.Rows.Clear();

            int idx = 0;

            using (RssReader rr = new RssReader(cld.rss.url))
            {
                //dgvの作成
                foreach (string title in rr.urlTitleList)
                {
                    dgv.Rows.Add(new object[] { "", title, rr.dateList[idx], rr.urlList[idx] });
                    idx++;
                }
            }

            //using (RssReader rr = new RssReader(cld.rss.url))
            //{
            //    //dgvの作成
            //    foreach (MsgShortNews msg in cld.rss.topicList)
            //    {
            //        dgv.Rows.Add(new object[] { "", msg.title, msg.createDate, msg.url });
            //        idx++;
            //    }
            //}

            this.Refresh();
        }
        #endregion

        ///====================================================================
        ///
        ///                      その他処理メソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// 登録データのセーブ
        /// </summary>
        #region saveRssList
        private void saveRssList()
        {
            //登録データのセーブ
            SerialRssObject.saveRssObject(rssList);

            //登録完了。ツリーの再構築
            reBuildTree();
        }
        private void saveRssListOnly()
        {
            //登録データのセーブ
            SerialRssObject.saveRssObject(rssList);
        }
        #endregion

        /// <summary>
        /// widowTitleChange
        /// タイトルを変更する
        /// </summary>
        /// <param name="msg"></param>
        #region widowTitleChange
        private void widowTitleChange(string msg)
        {
            this.Text = "LiplisBrowser - " + msg;
        }
        #endregion


        ///====================================================================
        ///
        ///                         表示値のセット
        ///                         
        ///====================================================================

        /// <summary>
        /// setTopicCnt
        /// 話題数をセットする
        /// </summary>
        #region setTopicCnt
        private void setTopicCnt()
        {
            lblSumWadaiCnt.Text = lips.getTopicCnt().ToString();
        }
        #endregion

        /// <summary>
        /// setTalkCnt
        /// 発言数をセットする
        /// </summary>
        #region setTalkCnt
        private void setTalkCnt()
        {
            lblSumHatsugenCnt.Text = lips.talkCnt.ToString();
        }
        #endregion


        

    }
}
