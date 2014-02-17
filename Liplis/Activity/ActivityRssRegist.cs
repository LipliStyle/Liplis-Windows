//=======================================================================
//  ClassName : ActivityRss
//  概要      : アクティビティRSS
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Liplis.Cmp.Tree;
using Liplis.Common;
using Liplis.Fct;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Web;
using Liplis.Xml;
using Liplis.Ser;

namespace Liplis.Activity
{
	public partial class ActivityRssRegist : BaseSystem
	{
		///=====================================
		/// オブジェクト
		public ObjRssList       rssList             { get; set; }
		private ObjRssCatList   nowSelectCatLsiet   { get; set; }
		private ObjRssCatList baskget = new ObjRssCatList("バスケット");
		private string          uid = "";

		///=====================================
		/// フラグ
		private bool flgEnd = false;

		///============================
		/// デリゲート
		#region デリゲート
		private static LpsDelegate.dlgBmpToVoid reqSetBackGround;
		#endregion


		///====================================================================
		///
		///                              onCreate
		///                         
		///====================================================================

		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <param name="rssList"></param>
		#region ActivityRss
		public ActivityRssRegist(ObjRssList rssList,string uid)
		{
			//本体より、RSSリストをもらう
			this.rssList = rssList;
			this.uid = uid;
			this.rssList.createBasket();
			this.nowSelectCatLsiet = null;
			this.StartPosition = FormStartPosition.CenterScreen;
			initSettingWindow();
			initDelegate();
			InitializeComponent();
			addHandler();
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

			this.Opacity = 1;

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
		/// addHandler
		/// ハンドラを追加する
		/// </summary>
		#region addHandler
		private void addHandler()
		{
			tvRss.ItemDrag += new ItemDragEventHandler(tvRss_ItemDrag);
			tvRss.DragOver += new DragEventHandler(tvRss_DragOver);
			tvRss.DragDrop += new DragEventHandler(tvRss_DragDrop);
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
			SerialRssObject.saveRssObject(rssList);
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
            adjustDgv();
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
			foreach (TreeNode p in tvRss.Nodes){if (p.IsExpanded){openList.Add(p.Text);}}

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

				//▼バスケットカテゴリーを取得
				if (catList.cat == "バスケット")
				{
					baskget = catList;
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

		/// <summarあｄじゅy>
		/// 最小化
		/// </summary>
		#region onMinimize
		public void onMinimize()
		{
			this.WindowState = FormWindowState.Minimized;
		}
		#endregion

        /// <summary>
        /// サイズ変更
        /// </summary>
        #region onSizeChange
        private void ActivityRssRegist_SizeChanged(object sender, EventArgs e)
        {
            adjustDgv();
        }
        #endregion

        /// <summary>
		/// 閉じるボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region btnClose_Click
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}
		#endregion

		/// <summary>
		/// btnRegist_Click
		/// 登録ボタンクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region btnRegist_Click
		private void btnRegist_Click(object sender, System.EventArgs e)
		{
			onRegist();
		}
		#endregion

		/// <summary>
		/// btnRegist_Click
		/// リストビュークリック
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
		/// dgv_UserDeletedRow
		/// 行削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region dgv_UserDeletedRow
		private void dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			if (chkDelete.Checked)
			{
				if (!LpsLiplisUtil.LiplisMessage("削除しますか？"))
				{
					e.Cancel = true; // 削除をキャンセル
					return;
				}
			}

            onDelete2();
		}
		#endregion

        /// <summary>
        /// dgv_CellMouseDown
        /// セルマウスダウン
        /// 
        /// 右クリック時に対象セルを選択状態とする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region dgv_CellMouseDown
        private void dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgv.ClearSelection();
                dgv.Rows[e.RowIndex].Selected = true;
            }
        }
        #endregion
        
		/// <summary>
		/// tvRss_ItemDrag
		/// ノードドラッグ時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region tvRss_ItemDrag
		private void tvRss_ItemDrag(object sender, ItemDragEventArgs e)
		{
			TreeView tv = (TreeView)sender;
			tv.SelectedNode = (TreeNode)e.Item;
			tv.Focus();

			//ノードのドラッグを開始する
			DragDropEffects dde = tv.DoDragDrop(e.Item, DragDropEffects.All);

			//移動した時は、ドラッグしたノードを削除する
			if ((dde & DragDropEffects.Move) == DragDropEffects.Move)
			{
				tv.Nodes.Remove((LiplisTreeNodeCld)e.Item);
			}
		}
		#endregion

		/// <summary>
		/// tvRss_DragOver
		/// ノードドラッグ中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region tvRss_DragOver
		private void tvRss_DragOver(object sender, DragEventArgs e)
		{
			//ドラッグされているデータがTreeNodeか調べる
			if (e.Data.GetDataPresent(typeof(LiplisTreeNodeCld)))
			{
				if ((e.KeyState & 8) == 8 &&
					(e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
				{
					//Ctrlキーが押されていればCopy
					//"8"はCtrlキーを表す
					//e.Effect = DragDropEffects.Copy;
					//コピー無効
					e.Effect = DragDropEffects.None;
				}

				else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
				{
					//何も押されていなければMove
					e.Effect = DragDropEffects.Move;
				}
				else
				{
					e.Effect = DragDropEffects.None;
				}
			}
			else
			{
				//TreeNodeでなければ受け入れない
				e.Effect = DragDropEffects.None;
			}

			//マウス下のNodeを選択する
			if (e.Effect != DragDropEffects.None)
			{
				TreeView tv = (TreeView)sender;

				//マウスのあるNodeを取得する
				TreeNode target = (TreeNode)tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));

				//ドラッグされているNodeを取得する
				LiplisTreeNodeCld source = (LiplisTreeNodeCld)e.Data.GetData(typeof(LiplisTreeNodeCld));

				//マウス下のNodeがドロップ先として適切か調べる
				if (target != null && target != source && !IsChildNode(source, target))
				{
					//Nodeを選択する
					if (target.IsSelected == false)
					{
						tv.SelectedNode = target;
					}
				}
				else
				{
					e.Effect = DragDropEffects.None;
				}
			}
		}
		#endregion

		/// <summary>
		/// tvRss_DragDrop
		/// ドロップ時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region tvRss_DragDrop
		private void tvRss_DragDrop(object sender, DragEventArgs e)
		{
			//ドロップされたデータがTreeNodeか調べる
			if (e.Data.GetDataPresent(typeof(LiplisTreeNodeCld)))
			{
				TreeView tv = (TreeView)sender;
				//ドロップされたデータ(TreeNode)を取得
				LiplisTreeNodeCld source = (LiplisTreeNodeCld)e.Data.GetData(typeof(LiplisTreeNodeCld));
				//ドロップ先のTreeNodeを取得する
				TreeNode target = tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
				//マウス下のNodeがドロップ先として適切か調べる
				if (target != null && target != source && !IsChildNode(source, target))
				{
					LiplisTreeNodePar par = null;

					//ペアレントか子か
					if(IsParentNode(target))
					{
						//ドロップ先のTreeNodeを取得する
						par = (LiplisTreeNodePar)tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
					}
					else if (IsCldNode(target))
					{
						//ドロップ先のTreeNodeを取得する
						LiplisTreeNodeCld cld = (LiplisTreeNodeCld)tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));
						par = (LiplisTreeNodePar)cld.Parent;
					}

					rssList.delRss(source.rss.url, source.rss.cat);
					rssList.addRss(source.rss.url, par.cat, source.rss.title);

					//登録データのセーブ
					saveRssList();

					//ツリーのリビルド
					reBuildTree();

					//データグリッドの更新
					onLoadDgv();

					//ドロップ先のNodeを展開
					target.Expand();

					////ドロップされたNodeのコピーを作成
					//LiplisTreeNodeCld cln = (LiplisTreeNodeCld)source.Clone();
					////Nodeを追加
					//target.Nodes.Add(cln);
					////ドロップ先のNodeを展開
					//target.Expand();
					////追加されたNodeを選択
					//tv.SelectedNode = cln;

				}

				else
				{
					e.Effect = DragDropEffects.None;
				}

			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
				
		}
		#endregion

		/// <summary>
		/// ActivityRss_DragEnter
		/// ドラッグエンター
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region ActivityRss_DragEnter
		private void ActivityRss_DragEnter(object sender, DragEventArgs e)
		{
			Fct.FctDragData.getDrag(e);
		}
		#endregion

		/// <summary>
		/// ActivityRss_DragDrop
		/// ドラングエンドロップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region ActivityRss_DragDrop
		private void ActivityRss_DragDrop(object sender, DragEventArgs e)
		{
			dropDataCheckRegist(Fct.FctDragData.getDropTextList(e));
			
		}
		#endregion

        /// <summary>
        /// カテゴリ操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region カテゴリ操作
        private void tvRss_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tvRss.SelectedNode = tvRss.GetNodeAt(e.X, e.Y);
            }
        }
        private void btnCatAdd_Click(object sender, System.EventArgs e)
        {
            using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 0,this.Left + this.Width / 2, this.Top + this.Height / 2))
            {
                arra.ShowDialog();
            }           
        }
        //private void tsmiAddCat_Click(object sender, EventArgs e)
        //{
        //    using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 0, this.Left + this.Width / 2, this.Top + this.Height / 2))
        //    {
        //        arra.ShowDialog();
        //    }    
        //}
        private void tsmiCatAdd_Click(object sender, EventArgs e)
        {
            using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 0, this.Left + this.Width / 2, this.Top + this.Height / 2))
            {
                arra.ShowDialog();
            }    
        }
        private void tsmiCatFix_Click(object sender, EventArgs e)
        {
            using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 0, this.Left + this.Width / 2, this.Top + this.Height / 2))
            {
                arra.ShowDialog();
            }    
        }
        private void tsmiCatDel_Click(object sender, EventArgs e)
        {
            onCatDelete();
        }
        #endregion

        /// <summary>
        /// RSS削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region RSS削除
        private void tsmiRssDel_Click(object sender, EventArgs e)
        {
            if (chkDelete.Checked)
            {
                if (!LpsLiplisUtil.LiplisMessage("削除しますか？"))
                {
                    return;
                }
            }

            //削除処理
            onDelete();
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
			LiplisTreeNodePar par = null;

			//クラス名の取得
			string className = e.Node.GetType().Name;
			
			//ペアレントの取得
			if (className == "LiplisTreeNodePar")
			{
				par = (LiplisTreeNodePar)e.Node;
				
			}
			else if (className == "LiplisTreeNodeCld")
			{
				par = (LiplisTreeNodePar)e.Node.Parent;
			}

			//ヌルチェック
			if (par == null) { return; }

			//dgvのクリア
			dgv.Rows.Clear();

			//dgvの作成
			foreach (ObjRss rss in par.catList.rssList)
			{
				dgv.Rows.Add(new object[] { rss.title, rss.url });
			}

			//カテゴリテキストに出力
			txtSelCat.Text = par.cat;

			//カテゴリーリストを取得
			nowSelectCatLsiet = par.catList;
		}
		#endregion

		/// <summary>
		/// onLoadDgv
		/// リストビュークリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region onLoadDgv
		private void onLoadDgv()
		{
            try
            {
                //dgvのクリア
                dgv.Rows.Clear();

                //dgvの作成
                foreach (ObjRss rss in nowSelectCatLsiet.rssList)
                {
                    dgv.Rows.Add(new object[] { rss.title, rss.url });
                }
            }
            catch
            {

            }

		}
		#endregion

		/// <summary>
		/// onRegist
		/// 登録処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region onRegist
		private void onRegist()
		{
			string title = "";

			//空チェック
			if (txtUrl.Text.Equals(""))
			{
				LpsLiplisUtil.LiplisMessage("URLが空です。");
				return;
			}

			//カテゴリ選択チェック
			if (txtSelCat.Text.Equals(""))
			{
				LpsLiplisUtil.LiplisMessage("カテゴリが選択されていません");
				return;
			}

			//接続チェックとタイトルの取得
			title = LpsLiplisUtil.checkRssConnect(txtUrl.Text);

			//URL妥当性チェック
			if (title == null)
			{
				LpsLiplisUtil.LiplisMessage("有効なRSSのURLではありません");
				return;
			}

			//登録処理
			if (!rssList.searchRss(txtUrl.Text))
			{
				rssList.addRss(txtUrl.Text, txtSelCat.Text, title);
			}
			else
			{
				LpsLiplisUtil.LiplisMessage("既に登録されています。");
				return;
			}

			//登録データのセーブ
			saveRssList();

			//データグリッドの更新
			onLoadDgv();

			//オンライン登録
			LiplisApiCus.registRss(txtUrl.Text, title, txtSelCat.Text, uid);

			//テキストのクリア
			txtUrl.Text = "";

		}
		#endregion

		/// <summary>
		/// onDelete
		/// 削除処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region onDelete
		private void onDelete()
		{
			//DataGridViewCell cell = dgv.SelectedRows[1];

			//削除処理
			if (rssList.searchRss(dgv[1,dgv.CurrentCell.RowIndex].Value.ToString()))
			{
				rssList.delRss(dgv[1, dgv.CurrentCell.RowIndex].Value.ToString(), txtSelCat.Text);
			}
			else
			{
				LpsLiplisUtil.LiplisMessage("既に削除されています。");
				return;
			}

			//登録データのセーブ
			saveRssList();

            //データグリッドの更新
            onLoadDgv();
		}
        private void onDelete2()
        {
            //削除処理
            if (rssList.searchRss(dgv[1, dgv.CurrentCell.RowIndex].Value.ToString()))
            {
                rssList.delRss(dgv[1, dgv.CurrentCell.RowIndex].Value.ToString(), txtSelCat.Text);
            }
            else
            {
                LpsLiplisUtil.LiplisMessage("既に削除されています。");
                return;
            }

            //登録データのセーブ
            saveRssList();

            //データグリッドの更新
            dgv.SelectedRows[0].Visible = false;
        }

		#endregion

		/// <summary>
		/// onCatRegist
		/// カテゴリー登録
		/// </summary>
		#region onCatRegist
		public void onCatRegist(string catName)
		{
			//登録チェック
            if (!rssList.addCat(catName))
			{
				LpsLiplisUtil.LiplisMessage("既に登録されています。");
				return;
			}

			//登録データのセーブ
			saveRssList();

            //データグリッドの更新
            onLoadDgv();
		}
		#endregion  
	  
		/// <summary>
		/// onCatDelete
		/// カテゴリー削除
		/// </summary>
		#region onCatDelete
        public void onCatDelete()
		{
			//削除確認
			if (!LpsLiplisUtil.LiplisMessage("本当に削除しますか？"))
			{
				return;
			}

			if (tvRss.SelectedNode == null)
			{
				LpsLiplisUtil.LiplisMessage("削除するカテゴリーを選択して下さい。");
				return;
			}

			if (tvRss.SelectedNode.Text.Equals("バスケット"))
			{
				return;
			}

			//選択チェック
			if (tvRss.SelectedNode.Text.Equals(""))
			{
				LpsLiplisUtil.LiplisMessage("カテゴリーが選択されていません");
				return;
			}

			//カテゴリーの削除
			rssList.delCat(tvRss.SelectedNode.Text);

			//登録データのセーブ
			saveRssList();

            //データグリッドの更新
            onLoadDgv();
		}
		#endregion        

		/// <summary>
		/// onCatFix
		/// カテゴリー修正
		/// </summary>
		#region onCatFix
        public void onCatFix(string catName)
		{
			//選択チェック
			if (this.nowSelectCatLsiet == null)
			{
				LpsLiplisUtil.LiplisMessage("カテゴリーが選択されていません");
				return;
			}

			//カテゴリーデータの修正
            nowSelectCatLsiet.cat = catName;

			//登録データのセーブ
			saveRssList();

            //データグリッドの更新
            onLoadDgv();
		}
		#endregion        

		/// <summary>
		/// テキストのリストを有効なRSSかチェックし、有効ならバスケットに登録する
		/// </summary>
		/// <param name="textList"></param>
		#region dropDataCheckRegist
		private void dropDataCheckRegist(List<string> textList, string cat)
		{
			string title = "";
			List<string> failList = new List<string>();
			List<string> alRadyList = new List<string>();

			//テキストリストとまわして、有効なRSSかチェックする
			foreach (string url in textList)
			{
				title = LpsLiplisUtil.checkRssConnect(url);
				if (title != null)
				{
					//既に登録済み
					if (rssList.searchRss(url))
					{
						alRadyList.Add(url);
						continue;
					}

					//RSSをバスケットに登録する
					rssList.addRss(url, cat, title);

					//オンライン登録
					//2011/11/05 titleとcatの登録順が逆だったため修正
					LiplisApiCus.registRss(url, title, cat, uid);
				}
				else
				{
					if (!url.Equals(""))
					{
						failList.Add(url);
					}
				}
				Application.DoEvents();
			}

			//フェイルリストが存在したら、メッセージで知らせる
			if (failList.Count > 0)
			{
				StringBuilder msg = new StringBuilder();

				msg.Append("以下のURLの登録ができませんでした。" + Environment.NewLine);

				foreach (string fail in failList)
				{
					msg.Append(fail + Environment.NewLine);
				}
				LpsLiplisUtil.LiplisMessage(msg.ToString());
			}

			//登録済みリストが存在したら、メッセージで知らせる
			if (alRadyList.Count > 0)
			{
				StringBuilder msg = new StringBuilder();

				msg.Append("以下のURLは登録済みでした。" + Environment.NewLine);

				foreach (string url in alRadyList)
				{
					msg.Append(url + Environment.NewLine);
				}
				LpsLiplisUtil.LiplisMessage(msg.ToString());
			}

			//バスケットを選択しておく
			nowSelectCatLsiet = baskget;

			//登録データのセーブ
			saveRssList();

			//データグリッドの更新
			onLoadDgv();

			return;
		}
		private void dropDataCheckRegist(List<string> textList)
		{
			string cat = "";

			//現在選択カテゴリーチェック
			if (nowSelectCatLsiet != null){cat = nowSelectCatLsiet.cat;}
			else{cat = "バスケット";}

			//親メソッドに投げる
			dropDataCheckRegist(textList, cat);
		}
		#endregion

		/// <summary>
		/// テキストのリストを有効なRSSかチェックし、有効ならバスケットに登録する
		/// </summary>
		/// <param name="textList"></param>
		#region dropDataCheckLiplis
		public void dropDataCheckLiplis(List<string> textList)
		{
			dropDataCheckRegist(textList, "バスケット");
		}
		#endregion

		/// <summary>
		/// ActivityRss_MouseDown
		/// MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region ActivityRss_MouseDown
		private void ActivityRss_MouseDown(object sender, MouseEventArgs e)
		{
			mouseDown(e);
		}
		#endregion

		/// <summary>
		/// ActivityRss_MouseMove
		/// MouseMoce
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region ActivityRss_MouseMove
		private void ActivityRss_MouseMove(object sender, MouseEventArgs e)
		{
			mouseMove(e);
		}
		#endregion

		/// <summary>
		/// IsParentNode
		/// ペアレントノードか調べる
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		#region IsParentNode
		private static bool IsParentNode(TreeNode target)
		{
			if (target.GetType().Name == "LiplisTreeNodePar")
			{
				return true;
			}
			return false;
		}
		#endregion

		/// <summary>
		/// IsParentNode
		/// ペアレントノードか調べる
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		#region IsCldNode
		private static bool IsCldNode(TreeNode target)
		{
			if (target.GetType().Name == "LiplisTreeNodeCld")
			{
				return true;
			}
			return false;
		}
		#endregion

		/// <summary>
		/// あるTreeNodeが別のTreeNodeの子ノードか調べる
		/// 別の子ノードならOK
		/// </summary>
		/// <param name="parent">親ノードか調べるTreeNode</param>
		/// <param name="child">子ノードか調べるTreeNode</param>
		/// <returns>子ノードの時はTrue</returns>
		#region IsChildNode
		private static bool IsChildNode(TreeNode parent, TreeNode child)
		{
			if (child.Parent == parent)
				return true;
			else if (child.Parent != null)
				return IsChildNode(parent, child.Parent);
			else
				return false;
		}
		#endregion

        /// <summary>
        /// DGVの自動サイズ調整
        /// </summary>
        #region adjustDgv
        private void adjustDgv()
        {
            try
            {
                int wid = dgv.Width - dgv.Columns[0].Width - 45;
                dgv.Columns[1].Width = wid;
            }
            catch
            {
            }
        }
        #endregion
        


		///====================================================================
		///
		///                           ウインドウ制御
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
		#endregion

		///====================================================================
		///
		///                            WEB
		///                         
		///====================================================================
		///
		/// <summary>
		/// 対象のXMLが有効かどうか判断する
		/// </summary>
		#region checkRssConnect
		public static string checkRssConnect(string url)
		{
			string title = null;
			try
			{
				RssReader rr = new RssReader(url);
				title = rr.title;
				rr.Dispose();
			}
			catch
			{
				title = null;
			}
			return title;
		}
		#endregion









	}
}
