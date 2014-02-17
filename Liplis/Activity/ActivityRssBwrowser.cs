//=======================================================================
//  ClassName : ActivityRssBwrowser
//  概要      : アクティビティRSSブラウザ
//
//  Liplis2.0
//  Copyright(c) 2010-2012 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Liplis.Cmp.Tree;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Ser;
using Liplis.Xml;
using Liplis.Common;
using Liplis.Web;

namespace Liplis.Activity
{
	public partial class ActivityRssBwrowser : BaseSystem
	{
        ///=====================================
        /// オブジェクト
        private ObjRssList rssList;
        private Liplis.MainSystem.Liplis lips;
        private ObjSetting os;

        ///=====================================
        /// フラグ
        private bool flgEnd = false;

        //=====================================
        /// リスト
        private List<string> histryList;

		///====================================================================
		///
		///                              onCreate
		///                         
		///====================================================================

		/// <summary>
		/// コンストラクター
		/// </summary>
        /// <param name="rssList"></param>
        #region ActivityRssBwrowser
        public ActivityRssBwrowser(Liplis.MainSystem.Liplis lips, ObjSetting os)
		{
            this.lips = lips;
            this.os = os;

            InitializeComponent();
			initSettingWindow();
            intList();
		}
		#endregion

		/// <summary>
		/// initSettingWindow
		/// initSettingWindowの初期化
		/// </summary>
		#region initSettingWindow
		private void initSettingWindow()
		{
			//一時背景の設定と透過の設定(透明で初期化)
			//setWindowProperty(FctCreateFromResource.getTranse());

			//サイズを設定する
			//setSize(640, 480);

            this.StartPosition = FormStartPosition.CenterScreen;

            //スクリプトサポーティッド ツゥルー
            this.rssWb.ScriptErrorsSuppressed = true;

            this.Opacity = 1;
		}
		#endregion

        /// <summary>
        /// intList
        /// リストの初期化
        /// </summary>
        #region intList
        private void intList()
        {
            histryList = new List<string>();
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
		private void ActivityRss_FormClosing(object sender, FormClosingEventArgs e)
		{
			//エンドフラグが有効でなければ、ハイドさせる
			if (!flgEnd)
			{
				e.Cancel = true;
				this.Hide();
			}
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

            //▼rssListを取得する
            rssList = LiplisApiCus.getRssList(os.uid);

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
		///                              onLoad
		///                         
		///====================================================================

		/// <summary>
		/// ActivityRss_Load
		/// ロードイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region ActivityRss_Load
		private void ActivityRss_Load(object sender, System.EventArgs e)
		{
            //ツリーの再構築
            reBuildTree();
		}
		#endregion

		///====================================================================
		///
		///                           onRecive
		///                         
		///====================================================================

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

        /// <summary>
        /// RSSの登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiRegist_Click
        private void tsmiRegist_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_RSS, "");
        }
        #endregion

        /// <summary>
        /// RSSのリロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiReload_Click
        private void tsmiReload_Click(object sender, EventArgs e)
        {
            reBuildTree();
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

            this.Refresh();
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

	}
}
