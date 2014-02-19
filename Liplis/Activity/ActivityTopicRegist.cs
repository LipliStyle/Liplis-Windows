//=======================================================================
//  ClassName : ActivityTopicRegist
//  概要      : アクティビティ話題登録
//
//  Liplis3.0
//  2013/06/23 Liplis3.0.2 トピックスミニ追加
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Liplis.Cmp.Tree;
using Liplis.Common;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Ser;
using Liplis.Web;
using Liplis.Xml;

namespace Liplis.Activity
{
	public partial class ActivityTopicRegist : BaseSystem
	{
		///=====================================
		/// オブジェクト
		private Liplis.MainSystem.Liplis lips;
		private ObjSetting os;
		public ObjRssList rssList;
		private ObjRssCatList nowSelectCatLsiet;
		private ObjCat catList;
		private ResLpsLoginRegisterInfoTw twitterList;
		private ResLpsTopicSearchWordList filterList;
		private ObjWindowFile owf;
		private int nowSelectTopicId;

		///=====================================
		/// フラグ
		private bool flgEnd = false;

		///=====================================
		/// フラグ
		public const string CAT_DEFAULT = "デフォルトカテゴリ";


		///====================================================================
		///
		///                              onCreate
		///                         
		///====================================================================

		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <param name="rssList"></param>
		#region ActivityTopicRegist
		public ActivityTopicRegist(Liplis.MainSystem.Liplis lips, ObjSetting os, ObjWindowFile owf)
		{
			this.lips = lips;
			this.os = os;
			this.owf = owf;
			this.nowSelectTopicId = 0;

			getCatList();                   //カテゴリファイルの読み込み
			rssList = new ObjRssList();     //RSSリストの初期化
			this.nowSelectCatLsiet = null;
			this.KeyPreview = true;         //フォームがキーイベントを受け取る
			initSettingWindow();
			InitializeComponent();
			initSettting();
			addHandler();
			getTwitterList();
			getFilterList();
		}
		#endregion

		/// <summary>
		/// initSettingWindow
		/// initSettingWindowの初期化
		/// </summary>
		#region initSettingWindow
		private void initSettingWindow()
		{
			this.Opacity = 1;
			this.StartPosition = FormStartPosition.CenterScreen;
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

		/// <summary>
		/// getCatList
		/// カテゴリリストを取得する
		/// </summary>
		#region getCatList
		private void getCatList()
		{
			this.catList = SerialCatObject.loadCatObject();

			bool flgDefault = false;

			foreach (string cat in catList.catList)
			{
				if (cat == CAT_DEFAULT)
				{
					flgDefault = true;
				}
			}

			//デフォルトカテゴリが無かったら一番目に追加する
			if (!flgDefault)
			{
				if (catList.catList.Count > 0)
				{
					catList.catList.Insert(0, CAT_DEFAULT);
				}
				else
				{
					catList.catList.Add(CAT_DEFAULT);
				}

				//セーブしておく
				SerialCatObject.saveRssObject(catList);
			}
		}
		#endregion
		
		/// <summary>
		/// getCatList
		/// 設定の初期化
		/// </summary>
		#region initSettting
		private void initSettting()
		{
			this.catList = SerialCatObject.loadCatObject();

			bool flgDefault = false;

			foreach (string cat in catList.catList)
			{
				if (cat == CAT_DEFAULT)
				{
					flgDefault = true;
				}
			}

			//デフォルトカテゴリが無かったら一番目に追加する
			if (!flgDefault)
			{
				if (catList.catList.Count > 0)
				{
					catList.catList.Insert(0, CAT_DEFAULT);
				}
				else
				{
					catList.catList.Add(CAT_DEFAULT);
				}

				//セーブしておく
				SerialCatObject.saveRssObject(catList);
			}

			

			//Twitter認証
			if (os.twitterActivate == 1)
			{
				tsmiTwitterActivate.Text = "Twitter認証登録(認証済)";
			}
			else
			{
				tsmiTwitterActivate.Text = "Twitter認証登録(未認証)";
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
			setBackgournd();
		}
		#endregion

		/// <summary>
		/// setBackgournd
		/// 背景をセットする
		/// </summary>
		#region setBackgournd
		private void setBackgournd()
		{
			this.BackgroundImage = owf.bt_setting;
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
			//ノードインデックス
			int parNodeIdx = 0;

			//照合チェックフラグ
			bool flgCheck = false;

			//オープンリスト
			List<string> openList = new List<string>();

			//RSSリストの再取得
			rssList = LiplisApiCus.getRssList(os.uid);

			//▼このウインドウをロードするためにLiplisから呼ばれている
			this.Opacity = 1;

			//▼ツリービュー更新開始
			tvRss.BeginUpdate();

			//▼開いているツリービュー名を記録する
			foreach (TreeNode p in tvRss.Nodes){if (p.IsExpanded){openList.Add(p.Text);}}

			//▼ツリービュー初期化
			tvRss.Nodes.Clear();

			//▼カテゴリのツリー親ノード作成
			foreach (string cat in this.catList.catList)
			{
				//▼ノードの作成
				LiplisTreeNodePar tne = new LiplisTreeNodePar(new ObjRssCatList(cat), cat);
				tvRss.Nodes.Add(tne);
			}

			//▼RSSリストを回して、読み込み
			foreach (ObjRssCatList orcl in rssList.rssCatList)
			{
				//▼カテゴリ名が空なら、"なし"を登録
				if (orcl.cat == null || orcl.cat == "")
				{
					orcl.cat = CAT_DEFAULT;
				}

				//▼カテゴリ照合
				flgCheck = false;
				foreach (string cat in this.catList.catList)
				{
					if (cat == orcl.cat)
					{
						flgCheck = true; 
					}
				}

				//▼フラグチェック
				if (flgCheck)
				{
					//登録リストにあったので、カテゴリリストを更新する
					LiplisTreeNodePar ltn = getTargetTreeNode(orcl.cat);
					
					//念のためNULLチェック(NULLはありえないが・・・)
					if (ltn != null)
					{
						//ORCLをセットする
						ltn.catList = orcl;
						parNodeIdx = tvRss.Nodes.IndexOf(ltn);
					}
				}
				else
				{
					//▼ノードの作成                 
					LiplisTreeNodePar tne = new LiplisTreeNodePar(orcl, orcl.cat);
					tvRss.Nodes.Add(tne);
					parNodeIdx = tvRss.Nodes.Count-1;
				}

				//▼子ノードの作成
				foreach (ObjRss rss in orcl.rssList)
				{
					LiplisTreeNodeCld cld = new LiplisTreeNodeCld(rss, rss.title);
					tvRss.Nodes[parNodeIdx].Nodes.Add(cld);
				}
				
				//リストの更新
				//getTargetTreeNode(orcl.cat).catList = orcl;  
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

		/// <summary>
		/// getTargetTreeNode
		/// 探したいツリーノードを得る
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		#region getTargetTreeNode
		private LiplisTreeNodePar getTargetTreeNode(string name)
		{
			foreach (LiplisTreeNodePar tn in tvRss.Nodes)
			{
				if (tn.Text == name)
				{
					return tn;
				}
			}

			return null;
		}
		#endregion

		/// <summary>
		/// loadRss
		/// RSSタブを選択して開く
		/// </summary>
		/// <param name="os"></param>
		#region loadRss
		public void loadRss()
		{
			reBuildTree();
			tb.SelectedIndex = 1;
		}
		#endregion

		/// <summary>
		/// loadTwitter
		/// Twitterタブを選択して開く
		/// </summary>
		/// <param name="os"></param>
		#region loadTwitter
		public void loadTwitter()
		{
			getTwitterList();
			tb.SelectedIndex = 2;
		}
		#endregion   
	
		/// <summary>
		/// loadFilter
		/// フィルタータブを選択して開く
		/// </summary>
		/// <param name="os"></param>
		#region loadFilter
		public void loadFilter()
		{
			getTwitterList();
			tb.SelectedIndex = 0;
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
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region dgv_UserDeletedRow
		private void dgvFilter_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			if (dgvFilter.CurrentCellAddress.X == 1 && dgvFilter.IsCurrentCellDirty)
			{
				//コミットする
				dgvFilter.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}
		}
		private void dgvFilter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			//列のインデックスを確認する
			if (e.ColumnIndex == 1)
			{
				if (e.RowIndex > -1)
				{
					onFilterFix((string)dgvFilter[0, e.RowIndex].Value, (bool)dgvFilter[1, e.RowIndex].Value);
				}
			}
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
			//親ノードの場合も許可
			else if (e.Data.GetDataPresent(typeof(LiplisTreeNodePar)))
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
				return;
			}
			else
			{
				//TreeNodeでなければ受け入れない
				e.Effect = DragDropEffects.None;
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

					//ここを変更
					LiplisApiCus.registRss(source.rss.url, par.cat, os.uid);

					//ツリーのリビルド
					reBuildTree();

					//データグリッドの更新
					onLoadDgv();

					//ドロップ先のNodeを展開
					target.Expand();
				}
				else
				{
					e.Effect = DragDropEffects.None;
				}

			}
			else if (e.Data.GetDataPresent(typeof(LiplisTreeNodePar)))
			{
				TreeView tv = (TreeView)sender;

				//ドロップされたデータ(TreeNode)を取得
				LiplisTreeNodePar source = (LiplisTreeNodePar)e.Data.GetData(typeof(LiplisTreeNodePar));

				//ドロップ先のTreeNodeを取得する
				TreeNode target = tv.GetNodeAt(tv.PointToClient(new Point(e.X, e.Y)));

				//マウス下のNodeがドロップ先として適切か調べる
				if (target != null && target != source && !IsChildNode(source, target))
				{
					LiplisTreeNodePar par = null;

					//ペアレントか子か
					if (IsParentNode(target))
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

					//ここで順番入れ替え
					//ドラッグされたTNがデフォルトカテゴリの場合は何もしない
					if (source.Text == CAT_DEFAULT)
					{
						return;
					}
					else
					{
						//ツリーノード数チェック
						if (catList.catList.Count > 1)
						{
							string sourceName = source.Text;

							//移動先のノードインデックス取得
							int idx = catList.catList.IndexOf(par.Text);

							//インデックスチェック(存在確認)
							if (idx < 0){return;}
							
							//移動対象ノード(ドロップノード)削除
							catList.catList.Remove(sourceName);

							//移動先インデックスの再取得
							idx = catList.catList.IndexOf(par.Text);

							//挿入
							catList.catList.Insert(idx + 1, sourceName);

						}
					}



					//ツリーのリビルド
					reBuildTree();

					//データグリッドの更新
					onLoadDgv();

					//ドロップ先のNodeを展開
					target.Expand();
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
			using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 0, "",this.Left + this.Width / 2, this.Top + this.Height / 2))
			{
				arra.ShowDialog();
			}           
		}
		private void tsmiAddCat_Click(object sender, EventArgs e)
		{
			using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this,0, "",this.Left + this.Width / 2, this.Top + this.Height / 2))
			{
				arra.ShowDialog();
			}
		}
		private void tsmiCatAdd_Click(object sender, EventArgs e)
		{
			using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 0,"", this.Left + this.Width / 2, this.Top + this.Height / 2))
			{
				arra.ShowDialog();
			}    
		}
		private void tsmiCatFix_Click(object sender, EventArgs e)
		{
			//選択中のTreeNodeを取得する
			TreeNode target = this.tvRss.SelectedNode;

			//マウス下のNodeがドロップ先として適切か調べる
			if (target != null)
			{
				LiplisTreeNodePar par = null;

				//ペアレントか子か
				if (IsParentNode(target))
				{
					//ドロップ先のTreeNodeを取得する
					par = (LiplisTreeNodePar)target;
				}
				else if (IsCldNode(target))
				{
					par = (LiplisTreeNodePar)target.Parent;
				}

				using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 1, par.Text, this.Left + this.Width / 2, this.Top + this.Height / 2))
				{
					arra.ShowDialog();
				}   
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

		/// <summary>
		/// フォームキーダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region ActivityTopicRegist_KeyDown
		private void ActivityTopicRegist_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.F)
			{
				if (tb.SelectedIndex == 1)
				{
					tsmiCatSearch_Click(null, null);
				}
				else if (tb.SelectedIndex == 2)
				{
					tsmiTwitterSearch_Click(null, null);
				}
			}
		}
		#endregion

		/// <summary>
		/// フィルター対象ジャンル選択変更時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region cboFilterTopic_SelectedIndexChanged
		private void cboFilterTopic_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.nowSelectTopicId = cboFilterTopic.SelectedIndex;
		}
		#endregion
		

		/// <summary>
		/// フィルターアッドクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region btnFillterAdd_Click
		private void btnFillterAdd_Click(object sender, EventArgs e)
		{
			onFilterRegist();
		}
		#endregion

		/// <summary>
		/// フィールドデリート
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region dgvFilter_UserDeletingRow
		private void dgvFilter_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			if (chkDeleteFilter.Checked)
			{
				if (!LpsLiplisUtil.LiplisMessage("削除しますか？"))
				{
					e.Cancel = true; // 削除をキャンセル
					return;
				}
			}
			onDeleteFilter();
		}
		#endregion
		
		///====================================================================
		///
		///                        メニューレシーブ
		///                         
		///====================================================================

		#region メニューレシーブ
		/// <summary>
		/// カテゴリ追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiCatAdd__Click(object sender, EventArgs e)
		{
			using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 0,"", this.Left + this.Width / 2, this.Top + this.Height / 2))
			{
				arra.ShowDialog();
			}
		}

		/// <summary>
		/// カテゴリ再読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiCatReload_Click(object sender, EventArgs e)
		{
			//リビルド
			reBuildTree();

			//データグリッドの更新
			onLoadDgv();
		}

		/// <summary>
		/// RSS検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiCatSearch_Click(object sender, EventArgs e)
		{
			using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 2, ",0", this.Left + this.Width / 2, this.Top + this.Height / 2))
			{
				arra.ShowDialog();
			}
		}

		/// <summary>
		/// ツイッター認証登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiTwitterActivate_Click(object sender, EventArgs e)
		{
			lips.onRecive(LiplisDefine.LM_TWT_ACT, "");
		}

		/// <summary>
		/// ツイッターユーザー検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiTwitterSearch_Click(object sender, EventArgs e)
		{
			using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 3, ",0", this.Left + this.Width / 2, this.Top + this.Height / 2))
			{
				arra.ShowDialog();
			}
		}

		/// <summary>
		/// ツイッター再読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiTwitterReload_Click(object sender, EventArgs e)
		{
			getTwitterList();
		}

		/// <summary>
		/// フィルター検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiFilterSearch_Click(object sender, EventArgs e)
		{
			using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 4, ",0", this.Left + this.Width / 2, this.Top + this.Height / 2))
			{
				arra.ShowDialog();
			}
		}

		/// <summary>
		/// 終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiEnd_Click(object sender, EventArgs e)
		{
			this.Hide();
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
		/// onLoadDgvTwitter
		/// ツイッターDGVの更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region onLoadDgvTwitter
		private void onLoadDgvTwitter()
		{
			try
			{
				//dgvのクリア
				dgvTwitter.Rows.Clear();

				//dgvの作成
				foreach (RegisterTwUserInfo rllrit in twitterList.twuserlist)
				{
					dgvTwitter.Rows.Add(new object[] { rllrit.name, rllrit.description });
				}
			}
			catch
			{

			}

		}
		#endregion

		/// <summary>
		/// onLoadDgvFilter
		/// フィルターDGVの更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region onLoadDgvFilter
		private void onLoadDgvFilter()
		{
			try
			{
				//dgvのクリア
				dgvFilter.Rows.Clear();

				//dgvの作成
				foreach (ResLpsTopicSearchWord rlts in filterList.wordList)
				{
					if(nowSelectTopicId == rlts.topicId)
					{
						dgvFilter.Rows.Add(new object[] { rlts.word, rlts.flgEnable });
					}
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
				ResLpsLoginStatus res = LiplisApiCus.registRss(txtUrl.Text, txtSelCat.Text, os.uid);

				//レスポンスコードが0でなければエラー
				if (res.responseCode != "0")
				{
					LpsLiplisUtil.LiplisMessage("RSSの登録に失敗しました。");
					return;
				}
			}
			else
			{
				LpsLiplisUtil.LiplisMessage("既に登録されています。");
				return;
			}

			//登録データのセーブ
			reBuildTree();

			//データグリッドの更新
			onLoadDgv();

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
			//削除処理
			if (rssList.searchRss(dgv[1,dgv.CurrentCell.RowIndex].Value.ToString()))
			{
				//rssList.delRss(dgv[1, dgv.CurrentCell.RowIndex].Value.ToString(), txtSelCat.Text);

				ResLpsLoginStatus res = LiplisApiCus.deleteRss(os.uid, dgv[1, dgv.CurrentCell.RowIndex].Value.ToString());

				//レスポンスコードが0でなければエラー
				if (res.responseCode != "0")
				{
					LpsLiplisUtil.LiplisMessage("RSSの登録に削除しました。");
					return;
				}
			}
			else
			{
				LpsLiplisUtil.LiplisMessage("既に削除されています。");
				return;
			}

			reBuildTree();

			//データグリッドの更新
			onLoadDgv();
		}
		private void onDelete2()
		{
			//削除処理
			if (rssList.searchRss(dgv[1, dgv.CurrentCell.RowIndex].Value.ToString()))
			{
				ResLpsLoginStatus res = LiplisApiCus.deleteRss(os.uid, dgv[1, dgv.CurrentCell.RowIndex].Value.ToString());

				//レスポンスコードが0でなければエラー
				if (res.responseCode != "0")
				{
					LpsLiplisUtil.LiplisMessage("RSSを削除しました。");
					return;
				}
			}
			else
			{
				LpsLiplisUtil.LiplisMessage("既に削除されています。");
				return;
			}

			reBuildTree();

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
			foreach (string cat in catList.catList)
			{
				if (cat == catName)
				{
					LpsLiplisUtil.LiplisMessage("既に登録されています。");
					return;
				}
			}

			//追加する。
			catList.catList.Add(catName);

			//カテゴリリストをセーブする
			SerialCatObject.saveRssObject(catList);

			reBuildTree();

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

			if (tvRss.SelectedNode.Text.Equals(CAT_DEFAULT))
			{
				return;
			}

			//選択チェック
			if (tvRss.SelectedNode.Text.Equals(""))
			{
				LpsLiplisUtil.LiplisMessage("カテゴリーが選択されていません");
				return;
			}

			//カテゴリーの存在チェック
			for (int idx = 0; idx < rssList.rssCatList.Count; idx++)
			{
				//対象カテゴリを探す
				if (rssList.rssCatList[idx].cat == tvRss.SelectedNode.Text)
				{
					//一致したら、CatList内のRSSのを回してURLを抽出、削除処理する。
					foreach (ObjRss or in rssList.rssCatList[idx].rssList)
					{
						//削除
						LiplisApiCus.deleteRss(os.uid, or.url);
					}
				}
			}

			//カテゴリを削除する
			catList.catList.Remove(tvRss.SelectedNode.Text);

			//カテゴリリストをセーブする
			SerialCatObject.saveRssObject(catList);

			reBuildTree();

			//データグリッドの更新
			onLoadDgv();
		}
		#endregion        

		/// <summary>
		/// onCatFix
		/// カテゴリー修正
		/// </summary>
		#region onCatFix
		public void onCatFix(string oldName, string newName)
		{
			//選択チェック
			if (this.nowSelectCatLsiet == null)
			{
				LpsLiplisUtil.LiplisMessage("カテゴリーが選択されていません");
				return;
			}

			//カテゴリーデータの修正

			//まず、新しいカテゴリを作成する
			//追加する。
			catList.catList.Add(newName);

			//カテゴリの削除
			for (int idx = 0; idx < rssList.rssCatList.Count; idx++)
			{
				if (rssList.rssCatList[idx].cat == oldName)
				{
					foreach (ObjRss or in rssList.rssCatList[idx].rssList)
					{
						LiplisApiCus.deleteRss(os.uid, or.url);
					}
				}
			}

			//新規カテゴリで登録し直し
			for (int idx = 0; idx < rssList.rssCatList.Count; idx++)
			{
				if (rssList.rssCatList[idx].cat == oldName)
				{
					foreach (ObjRss or in rssList.rssCatList[idx].rssList)
					{
						LiplisApiCus.registRss(or.url, newName, os.uid);
					}
				}
			}


			//旧カテゴリを削除する
			catList.catList.Remove(oldName);

			//カテゴリリストをセーブする
			SerialCatObject.saveRssObject(catList);

			reBuildTree();

			//データグリッドの更新
			onLoadDgv();
		}
		#endregion        

		/// <summary>
		/// onRssSearch
		/// 検索処理
		/// </summary>
		/// <param name="word"></param>
		#region onRssSearch
		public int onRssSearch(string word, int startIdx)
		{
			int idx = 0;
			bool flgFind = false;
			int resIdx = 0;

			foreach (TreeNode tn in tvRss.Nodes)
			{
				foreach (LiplisTreeNodeCld tnc in tn.Nodes)
				{
					//タイトルが指定ワードと同じか
					if (tnc.rss.title.Equals(word))
					{

						idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; tvRss.SelectedNode = tnc; } }
					}

					//タイトルが指定ワードで始まるか
					if (tnc.rss.title.StartsWith(word))
					{

						idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; tvRss.SelectedNode = tnc; } }
					}

					//タイトルに指定ワードが含まれているか
					if (tnc.rss.title.IndexOf(word) > 0)
					{

						idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; tvRss.SelectedNode = tnc; } }
					}

					//タイトルが指定ワードで終わるか
					if (tnc.rss.title.EndsWith(word))
					{

						idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; tvRss.SelectedNode = tnc; } }
					}

					//タイトルが指定ワードと同じか
					if (tnc.rss.url.Equals(word))
					{

						idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; tvRss.SelectedNode = tnc; } }
					}

					//URLに指定ワードが含まれているか
					if (tnc.rss.url.IndexOf(word) > 0)
					{

						idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; tvRss.SelectedNode = tnc; } }
					}

					//URLに指定ワードが含まれているか
					if (tnc.rss.url.StartsWith(word))
					{

						idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; tvRss.SelectedNode = tnc; } }
					}

					//URLに指定ワードが含まれているか
					if (tnc.rss.url.EndsWith(word))
					{

						idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; tvRss.SelectedNode = tnc; } }
					}
				}
			}

			//最大まで行ったら0に戻しておく
			if (idx-1 == startIdx)
			{
				resIdx = 0;
			}

			return resIdx;

			//using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 2, word + "," + resIdx.ToString(), this.Left + this.Width / 2, this.Top + this.Height / 2))
			//{
			//    arra.ShowDialog();
		}
		#endregion

		/// <summary>
		/// onSearch
		/// 検索処理
		/// </summary>
		/// <param name="word"></param>
		#region onSearch
		public int onTwitterSearch(string word, int startIdx)
		{
			int idx = 0;
			bool flgFind = false;
			int resIdx = 0;

			foreach (DataGridViewRow dgvr in dgvTwitter.Rows)
			{
				//タイトルが指定ワードと同じか
				if (dgvr.Cells[0].Value.ToString().Equals(word))
				{

					idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; dgvr.Selected = true; } }
				}

				//タイトルが指定ワードで始まるか
				if (dgvr.Cells[0].Value.ToString().StartsWith(word))
				{

					idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; dgvr.Selected = true; } }
				}

				//タイトルに指定ワードが含まれているか
				if (dgvr.Cells[0].Value.ToString().IndexOf(word) > 0)
				{

					idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; dgvr.Selected = true; } }
				}

				//タイトルが指定ワードで終わるか
				if (dgvr.Cells[0].Value.ToString().EndsWith(word))
				{

					idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; dgvr.Selected = true; } }
				}
			}

			//最大まで行ったら0に戻しておく
			if (idx - 1 == startIdx)
			{
				resIdx = 0;
			}

			return resIdx;

			//using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 3, word + "," + resIdx.ToString(), this.Left + this.Width / 2, this.Top + this.Height / 2))
			//{
			//    arra.ShowDialog();
			//}
		}
		#endregion

		/// <summary>
		/// onFilterSearch
		/// フィルター検索処理
		/// </summary>
		/// <param name="word"></param>
		#region onFilterSearch
		public int onFilterSearch(string word, int startIdx)
		{
			int idx = 0;
			bool flgFind = false;
			int resIdx = 0;

			foreach (DataGridViewRow dgvr in dgvFilter.Rows)
			{
				//タイトルが指定ワードと同じか
				if (dgvr.Cells[0].Value.ToString().Equals(word))
				{

					idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; dgvr.Selected = true; } }
				}

				//タイトルが指定ワードで始まるか
				if (dgvr.Cells[0].Value.ToString().StartsWith(word))
				{

					idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; dgvr.Selected = true; } }
				}

				//タイトルに指定ワードが含まれているか
				if (dgvr.Cells[0].Value.ToString().IndexOf(word) > 0)
				{

					idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; dgvr.Selected = true; } }
				}

				//タイトルが指定ワードで終わるか
				if (dgvr.Cells[0].Value.ToString().EndsWith(word))
				{

					idx++; if (idx == startIdx + 1) { if (!flgFind) { flgFind = true; resIdx = idx; dgvr.Selected = true; } }
				}
			}

			//最大まで行ったら0に戻しておく
			if (idx - 1 == startIdx)
			{
				resIdx = 0;
			}

			return resIdx;

			//using (ActivityRssRegistAddCat arra = new ActivityRssRegistAddCat(this, 3, word + "," + resIdx.ToString(), this.Left + this.Width / 2, this.Top + this.Height / 2))
			//{
			//    arra.ShowDialog();
			//}
		}
		#endregion

		/// <summary>
		/// onTwitterRegist
		/// ツイッター登録処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region onTwitterRegist
		private void onTwitterRegist()
		{
			//空チェック
			if (txtTwitterUserName.Text.Equals(""))
			{
				LpsLiplisUtil.LiplisMessage("ツイッターユーザー名が空です。");
				return;
			}

			//登録
			ResLpsLoginStatus res = LiplisApiCus.registTwitter(txtTwitterUserName.Text, os.uid);

			//レスポンスコードが0でなければエラー
			if (res.responseCode != "0")
			{
				LpsLiplisUtil.LiplisMessage("ツイッターユーザーの登録に失敗しました。");
				return;
			}

			//データグリッドの更新
			getTwitterList();

			//テキストのクリア
			txtTwitterUserName.Text = "";

		}
		#endregion

		/// <summary>
		/// onDeleteTwitter
		/// ツイッターユーザーを削除する
		/// </summary>
		#region onDeleteTwitter
		private void onDeleteTwitter()
		{
			//削除処理
			ResLpsLoginStatus res = LiplisApiCus.deleteTwitter(os.uid, dgvTwitter[0, dgvTwitter.CurrentCell.RowIndex].Value.ToString());

			//レスポンスコードが0でなければエラー
			if (res.responseCode != "0")
			{
				LpsLiplisUtil.LiplisMessage("Twitterユーザーの削除に失敗しました。");
				return;
			}
		}
		#endregion

		/// <summary>
		/// フィルターを追加する
		/// </summary>
		#region onFilterRegist
		private void onFilterRegist()
		{
			//空チェック
			if (txtFillterWord.Text.Equals(""))
			{
				return;
			}

			//登録
			ResLpsLoginStatus res = LiplisApiCus.registFilter(os.uid, this.nowSelectTopicId.ToString(), txtFillterWord.Text, "1");

			//レスポンスコードが0でなければエラー
			if (!res.responseCode.Equals("0") && !res.responseCode.Equals("1"))
			{
				LpsLiplisUtil.LiplisMessage("フィルターの登録に失敗しました。");
				return;
			}

			//データグリッドの更新
			getFilterList();

			//テキストのクリア
			txtFillterWord.Text = "";

		}
		#endregion

		/// <summary>
		/// フィルターを修正する
		/// </summary>
		#region onFilterFix
		private void onFilterFix(string word, bool flg)
		{
			//空チェック
			if (word.Equals(""))
			{
				return;
			}

			//登録
			ResLpsLoginStatus res = LiplisApiCus.registFilter(os.uid, this.nowSelectTopicId.ToString(), word, LpsLiplisUtil.boolToBit(flg).ToString());

			//レスポンスコードが0でなければエラー
			if (!res.responseCode.Equals("1") && !res.responseCode.Equals("0"))
			{
				LpsLiplisUtil.LiplisMessage("フィルターの修正に失敗しました。");
				return;
			}

			//データグリッドの更新
			getFilterList();

			//テキストのクリア
			txtFillterWord.Text = "";

		}
		#endregion

		/// <summary>
		/// フィルターを削除する
		/// </summary>
		#region onDeleteFilter
		private void onDeleteFilter()
		{
			//削除処理
			ResLpsLoginStatus res = LiplisApiCus.deleteFilter(os.uid, this.nowSelectTopicId.ToString(), dgvFilter[0, dgvFilter.CurrentCell.RowIndex].Value.ToString());

			//レスポンスコードが0でなければエラー
			if (res.responseCode != "0")
			{
				LpsLiplisUtil.LiplisMessage("フィルターの削除に失敗しました。");
				return;
			}
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

					ResLpsLoginStatus res = LiplisApiCus.registRss(url,"", os.uid);

					//レスポンスコードが0でなければエラー
					if (res.responseCode != "0")
					{
						LpsLiplisUtil.LiplisMessage("RSSの登録に失敗しました。");
						return;
					}
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

			//リビルド
			reBuildTree();

			//データグリッドの更新
			onLoadDgv();

			return;
		}
		private void dropDataCheckRegist(List<string> textList)
		{
			string cat = "";

			//現在選択カテゴリーチェック
			if (nowSelectCatLsiet != null){cat = nowSelectCatLsiet.cat;}
			else { cat = CAT_DEFAULT; ; }

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
			dropDataCheckRegist(textList, CAT_DEFAULT);
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

		///====================================================================
		///
		///                      その他処理メソッド
		///                         
		///====================================================================

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

		/// <summary>
		/// getTwitterList
		/// ツイッターリストを取得する
		/// </summary>
		#region getTwitterList
		public void getTwitterList()
		{
			//リスト取得
			twitterList = LiplisApiCus.getTwitterList(os.uid);

			//DGVのロード
			onLoadDgvTwitter();
		}
		#endregion

		/// <summary>
		/// getFilterList
		/// フィルターリストを取得する
		/// </summary>
		#region getFilterList
		public void getFilterList()
		{
			//リスト取得
			filterList = LiplisApiCus.getFilterList(os.uid);

			//DGVのロード
			onLoadDgvFilter();
		}
		#endregion


		///====================================================================
		///
		///                         onReciveTwitter
		///                         
		///====================================================================

		/// <summary>
		/// ツイッター登録ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region ツイッター登録ボタン
		private void btnRegistTwitter_Click(object sender, EventArgs e)
		{
			onTwitterRegist();
		}
		#endregion

		/// <summary>
		/// DGVツイッター削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region dgvTwitter_UserDeletingRow
		private void dgvTwitter_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			if (chkDeleteTwitter.Checked)
			{
				if (!LpsLiplisUtil.LiplisMessage("削除しますか？"))
				{
					e.Cancel = true; // 削除をキャンセル
					return;
				}
			}
			onDeleteTwitter();
		}
		#endregion

		/// <summary>
		/// DGVツイッター削除(右クリック)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		#region tsmiTwitterUserDel_Click
		private void tsmiTwitterUserDel_Click(object sender, EventArgs e)
		{
			onDeleteTwitter();
		}
		#endregion

















		




		










	}
}
