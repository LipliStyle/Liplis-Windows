//=======================================================================
//  ClassName : ActivityChat
//  概要      : チャット画面。
//              会話機能追加
//
//  Liplis4.5
//  Copyright(c) 2010-2015 LipliStyle.Sachin
//=======================================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using Liplis.Cmp.Form;
using Liplis.Common;
using Liplis.MainSystem;
using Liplis.Msg;


namespace Liplis.Activity
{
    public partial class ActivityChat : BaseSystem
    {        
        ///=====================================
        /// リプリス
        private Liplis.MainSystem.Liplis lips;
        private ObjSetting os;
        private ObjWindowFile owf;

        ///=====================================
        /// ほうき
        private ObjBroom obr;

        ///=====================================
        /// フラグ
        private bool flgEnd = false;

        ///=====================================
        /// 前回値
        private string prvUrl = " ";
        private string prvTitle = " ";

        ///====================================================================
        ///
        ///                             onCreate
        ///                         
        ///====================================================================

        //コンストラクター
        #region ActivityChat
        //コンストラクター
        public ActivityChat(Liplis.MainSystem.Liplis lips, ObjSetting os, ObjBroom obr, ObjWindowFile owf)
            : base()
        {
            this.lips = lips;
            this.os   = os;
            this.obr  = obr;
            this.owf  = owf;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            initSettingWindow();
        }
        #endregion

        /// <summary>
        /// initSettingWindow
        /// initSettingWindowの初期化
        /// </summary>
        #region initSettingWindow
        private void initSettingWindow()
        {
            this.components = new System.ComponentModel.Container();

            //サイズを設定する
            setSize(490, 500);

            //オパシティ1
            this.Opacity = 1;
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
        private void ActivityLog_FormClosing(object sender, FormClosingEventArgs e)
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void btnSend_Click(object sender, EventArgs e)
        {
            addTell(txtTell.Text);
        }
        #endregion

        /// <summary>
        /// エンターキー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region txtTell_KeyDown
        private void txtTell_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addTell(txtTell.Text);
                e.Handled = true;
            }
        }

        private void txtTell_KeyUp(object sender, KeyEventArgs e)
        {
            
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
            addPanel(msg.url, msg.title, msg.sorce, msg.jpgUrl, msg.newsEmotion, msg.newsPoint, charBody);
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
            if (!url.Equals("") && url.Equals(prvUrl) || !title.Equals("") && title.Equals(prvTitle)) { return; }

            //データパネル
            CusCtlDataPanel d;

            //前回値上書き
            prvUrl = url;
            prvTitle = title;

            //500件目の破棄
            if (flp.Controls.Count >= 500)
            {
                //500件目のパネルを取得
                using (CusCtlDataPanel dc = (CusCtlDataPanel)flp.Controls[499])
                {
                    try
                    {
                        //ごみすて
                        obr.deleteTargetFile(dc.jpgPath);

                        //破棄
                        dc.dispose();

                        //flpから追放
                        flp.Controls.RemoveAt(499);
                    }
                    catch
                    {

                    }
                }
            }

            //新規要素の追加
            d = new CusCtlTellPanelChar(lips, os, url, title, discription, newsEmotion, newsPoint, charBody, new System.EventHandler(this.mouseEnter), this.components);

            //アッドする
            flp.Controls.Add(d);
            flp.Controls.SetChildIndex(d, 0);
            flp.VerticalScroll.Value = flp.VerticalScroll.Maximum;

            this.Refresh();

            this.txtTell.Text = "";
        }
        #endregion


        /// <summary>
        /// addTell
        /// ログの追加
        /// </summary>
        #region addTell
        private void addTell(string description)
        {
            //空でエンターが押された時の対策。
            string tellString = txtTell.Text.Replace(Environment.NewLine, "");

            //入力チェック
            if (tellString.Equals(""))
            {
                return;
            }

            //データパネル
            CusCtlDataPanel d;

            //500件目の破棄
            if (flp.Controls.Count >= 500)
            {
                //500件目のパネルを取得
                using (CusCtlDataPanel dc = (CusCtlDataPanel)flp.Controls[499])
                {
                    try
                    {
                        //ごみすて
                        obr.deleteTargetFile(dc.jpgPath);

                        //破棄
                        dc.dispose();

                        //flpから追放
                        flp.Controls.RemoveAt(499);
                    }
                    catch
                    {

                    }
                }
            }

            //新規要素の追加
            d = new CusCtlTellPanel(lips, os, description, this.components);

            //アッドする
            flp.Controls.Add(d);
            flp.Controls.SetChildIndex(d, 0);
            flp.VerticalScroll.Value = flp.VerticalScroll.Maximum;
            
            this.Refresh();

            lips.onRecive(LiplisDefine.LM_CHAT_SEND, description);
        }
        #endregion






    }
}
