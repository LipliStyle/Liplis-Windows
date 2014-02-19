//=======================================================================
//  ClassName : ActivityRssRegistAddCat
//  概要      : アクティビティRSSカテゴリ追加
//
//  Liplis2.3
//  2013/06/20 Liplis2.3.0 話題登録画面一新
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Windows.Forms;
using Liplis.Common;

namespace Liplis.Activity
{
    public partial class ActivityRssRegistAddCat : Form
    {
        ///=====================================
        //親インスタンス
        private ActivityTopicRegist arr;

        ///=====================================
        //起動モード
        private int mode;

        ///=====================================
        //ターゲット
        string target = "";
        int targetIdx = 0;

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public ActivityRssRegistAddCat(ActivityTopicRegist arr, int mode, string target, int centerX, int centerY)
        {
            InitializeComponent();
            this.arr = arr;
            this.mode = mode;
            this.StartPosition = FormStartPosition.Manual;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(centerX - this.Width / 2, centerY - this.Height / 2);
            initWindow(target);
        }
        #endregion
        
        /// <summary>
        /// 初期化
        /// </summary>
        #region initWindow
        private void initWindow(string target)
        {
            switch (this.mode)
            {
                case 0:
                    this.btnCatAdd.Text = "追加";
                    break;
                case 1:
                    this.Text = "カテゴリ追加";
                    this.btnCatAdd.Text = "修正";
                    this.txtCat.Text = target;
                    this.target = target;
                    break;
                case 2:
                    this.Text = "RSS検索(URLまたはタイトル)";
                    this.btnCatAdd.Text = "検索";
                    string[] buf = target.Split(',');
                    this.txtCat.Text = buf[0];
                    this.targetIdx = int.Parse(buf[1]);
                    break;
                case 3:
                    this.Text = "Twitterユーザー検索";
                    this.btnCatAdd.Text = "検索";
                    string[] buf2 = target.Split(',');
                    this.txtCat.Text = buf2[0];
                    this.targetIdx = int.Parse(buf2[1]);
                    break;
                case 4:
                    this.Text = "フィルターワード検索";
                    this.btnCatAdd.Text = "検索";
                    string[] buf3 = target.Split(',');
                    this.txtCat.Text = buf3[0];
                    this.targetIdx = int.Parse(buf3[1]);
                    break;
                default:
                    btnCatAdd.Text = "-";
                    break;
            }
        }
        #endregion

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityRssRegistAddCat_Load
        private void ActivityRssRegistAddCat_Load(object sender, EventArgs e)
        {

        }
        #endregion
        
        /// <summary>
        /// イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region イベントハンドラ
        private void btnCatAdd_Click(object sender, EventArgs e)
        {
            switch (this.mode)
            {
                case 0:
                    onCatRegist();
                    break;
                case 1:
                    onCatFix();
                    break;
                case 2:
                    onRssSearch();
                    break;
                case 3:
                    onTwitterSearch();
                    break;
                case 4:
                    onFilterSearch();
                    break;
                default:
                    break;
            }

            
        }
        private void txtCat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                btnCatAdd_Click(null, null);
            }
        }
        #endregion

        /// <summary>
        /// onCatRegist
        /// カテゴリ登録
        /// </summary>
        #region onCatRegist
        private void onCatRegist()
        {
            //テキストチェック
            if (txtCat.Text.Equals(""))
            {
                LpsLiplisUtil.LiplisMessage("カテゴリーを入力して下さい。");
                return;
            }

            //テキストチェック
            if (txtCat.Text.Equals(ActivityTopicRegist.CAT_DEFAULT))
            {
                LpsLiplisUtil.LiplisMessage("その名前は使用できません");
                return;
            }

            //親に処理を投げる
            arr.onCatRegist(txtCat.Text);

            this.Close();
        }
        #endregion

        /// <summary>
        /// onCatFix
        /// カテゴリ修正
        /// </summary>
        #region onCatFix
        private void onCatFix()
        {
            //テキストチェック
            if (txtCat.Text.Equals(ActivityTopicRegist.CAT_DEFAULT))
            {
                LpsLiplisUtil.LiplisMessage("その名前は使用できません");
                return;
            }

            //親に処理を投げる
            arr.onCatFix(this.target,txtCat.Text);

            this.Close();
        }
        #endregion

        /// <summary>
        /// onSearch
        /// RSS検索
        /// </summary>
        #region onSearch
        private void onRssSearch()
        {
            //親に処理を投げる
            targetIdx = arr.onRssSearch(txtCat.Text, targetIdx);
        }
        #endregion

        /// <summary>
        /// onTwitterSearch
        /// ツイッターユーザー検索
        /// </summary>
        #region onTwitterSearch
        private void onTwitterSearch()
        {
            //親に処理を投げる
            targetIdx = arr.onTwitterSearch(txtCat.Text, targetIdx);
        }
        #endregion
        
        /// <summary>
        /// onFilterSearch
        /// ツイッターユーザー検索
        /// </summary>
        #region onFilterSearch
        private void onFilterSearch()
        {
            //親に処理を投げる
            targetIdx = arr.onFilterSearch(txtCat.Text, targetIdx);
        }
        #endregion

        /// <summary>
        /// キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnCancel_Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// テキストチェン時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region txtCat_TextChanged
        private void txtCat_TextChanged(object sender, EventArgs e)
        {
            targetIdx = 0;
        }
        #endregion

        /// <summary>
        /// キーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityRssRegistAddCat_KeyDown
        private void ActivityRssRegistAddCat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion
        
        
    }
}
