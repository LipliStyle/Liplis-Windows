using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Fct;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Ser;
using Liplis.Widget;
using Liplis.Widget.WidBrw;
using Liplis.Widget.WidCpu;
using Liplis.Widget.WidHdd;
using Liplis.Widget.WidLan;
using Liplis.Widget.WidMem;
using Liplis.Widget.WidRss;
using Liplis.Widget.WidSys;
using System.IO;
using Liplis.Sys;

namespace Liplis.Activity
{
    public partial class ActivityWidget : BaseSystem
    {
        ///=====================================
        /// オブジェクト
        private ObjRssList rssList;
        private ObjWidgetSetting ows;

        ///=====================================
        /// ウィジェットオブジェクト
        internal WidgetSettingList owml;
        internal List<WidgetBaseParent> widgetList;
        private Dictionary<int, string> rssDicUrl;


        ///=====================================
        /// クラス
        private NetworkInfoClass nic;

        ///=====================================
        /// フラグ
        private bool flgEnd = false;

        ///=====================================
        /// リスト
        private Dictionary<int, string> nicList;

        ///=====================================
        /// RSSディクショナリー
        private Dictionary<int, string> rssDicTitle;

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ActivityWidget
        public ActivityWidget(ObjRssList rssList)
        {
            InitializeComponent();
            this.rssList = rssList;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.initClass();
            this.intList();
            this.loadObject();
            this.initSettingWindow();

            this.intWidgetColor();
            this.loadWidget();
            this.intcombo();
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
        /// intList
        /// リストの初期化
        /// </summary>
        #region intList
        private void intList()
        {
            widgetList = new List<WidgetBaseParent>();
            rssDicUrl = new Dictionary<int, string>();;
            rssDicTitle = new Dictionary<int, string>();
        }
        #endregion

        /// <summary>
        /// initSettingWindow
        /// initSettingWindowの初期化
        /// </summary>
        #region initSettingWindow
        private void initSettingWindow()
        {
            //オパシティ1
            this.Opacity = 1;
        }
        #endregion


        /// <summary>
        /// loadWidget
        /// ウィジェットのロード
        /// </summary>
        #region loadWidget
        private void loadWidget()
        {
            foreach (WidgetBaseSetting s in owml.widgetList)
            {
                summonWidgetLoad(s);
            }
        }
        #endregion

        /// <summary>
        /// intList
        /// リストの初期化
        /// </summary>
        #region intWidgetColor
        private void intWidgetColor()
        {
            this.btnWidgetColorUp.BackColor = ows.widgetColorUp;
            this.btnWidgetColorUnder.BackColor = ows.widgetColorUnder;
            this.btnForeColor.BackColor = ows.widgetForeColor;
            this.btnLinkColor.BackColor = ows.widgetLinkColor;
            this.cboOpa.Text = ows.opacity.ToString();
            this.cboBrowserUpdInterval.SelectedIndex = ows.getBroserInterval();
            this.chkCtrl.Checked = ows.ctlRock;
        }
        #endregion

        /// <summary>
        /// intcombo
        /// リストの初期化
        /// </summary>
        #region intcombo
        private void intcombo()
        {
            cboRssTate.SelectedIndex = 0;
            cboRssYoko.SelectedIndex = 0;
            cboBrowserTate.SelectedIndex = 2;
            cboBrowserYoko.SelectedIndex = 2;

            cboHdd.Items.AddRange(Directory.GetLogicalDrives());
            if (cboHdd.Items.Count > 0) { cboHdd.SelectedIndex = 0; }
            nicList = nic.getNicList();         //ネットワークカードのリストを取得する
            foreach (KeyValuePair<int, string> kp in nicList) { cboLan.Items.Add(kp.Value); }
            if (cboLan.Items.Count > 0) { cboLan.SelectedIndex = 0; }

            //RSSコンボの初期化
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
                    rssDicTitle.Add(idx, rss.title);
                    rssDicUrl.Add(idx, rss.url);
                    cboRss.Items.Add(rss.title);
                    idx++;
                }
            }
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
        /// onDelete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityRss_FormClosing
        private void ActivityWidget_FormClosing(object sender, FormClosingEventArgs e)
        {
            //エンドフラグが有効でなければ、ハイドさせる
            if (!flgEnd)
            {
                e.Cancel = true;
                Invoke(new LpsDelegate.dlgVoidToVoid(this.Hide));
            }
        }
        #endregion

        ///===============================================
        ///
        ///                 onRecive
        ///                         
        ///===============================================
        ///
        /// <summary>
        /// 通常化
        /// </summary>
        #region onNormalize
        public void onNormalize()
        {
            this.WindowState = FormWindowState.Normal;
        }
        #endregion

        /// <summary>
        /// 最小化
        /// </summary>
        #region onMinimize
        public void onMinimize()
        {
            this.WindowState = FormWindowState.Minimized;
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
        ///                     ウィジェット追加・設定
        ///                         
        ///====================================================================

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

            summonWidget(new WidgetBrwSetting(LiplisDefine.WIDGET_BRW, LiplisDefine.WIDGET_TYPE_BRW, new Size(tate, yoko), txtBrowserUrl.Text, ows.getBroserInterval2(), chkAutoUpdate.Checked));
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
                dgvWidgetManager.Rows.Add(new object[] { FctCreateFromResource.getIconExtention(s.kbn), s.title, s.getSizeStr(), s.rect.X, s.rect.Y });
                return dgvWidgetManager.Rows[dgvWidgetManager.Rows.Count - 1];
            }
            catch
            {
                return null;
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




    }
}
