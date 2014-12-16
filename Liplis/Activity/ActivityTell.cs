//=======================================================================
//  ClassName : ActivityTell
//  概要      : 会話ウインd脳
//
//  Liplis4.5.0
//  2014/11/30 ver4.5.0 サイズ変更
//
//  Copyright(c) 2010-2014 LipliStyle.Sachin
//=======================================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.MainSystem;
using Liplis.Msg;

namespace Liplis.Activity
{
    public partial class ActivityTell : BaseSystem
    {
        ///====================================================================
        ///
        ///                             初期化処理
        ///                         
        ///====================================================================

        ///=====================================
        /// リプリス
        protected MainSystem.Liplis lips;

        ///=====================================
        /// 設定
        protected ObjSetting os;
        protected ObjSkinSetting oss;

        ///=====================================
        /// フラグ
        protected bool flgEnd = false;

        #region コンストラクター
        public ActivityTell(Liplis.MainSystem.Liplis lips, ObjSetting os, ObjSkinSetting oss)
        {
            //リプリス
            this.lips = lips;

            //設定オブジェクト
            this.os = os;

            //スキン設定オブジェクト 2013/08/31 ver3.0.5
            this.oss = oss;

            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivityTell_Load(object sender, EventArgs e)
        {
            initTalkWindow();
        }

        ///====================================================================
        ///
        ///                             onLoad
        ///                         
        ///====================================================================

        /// <summary>
        /// initTalkWindow
        /// initTalkWindowの初期化
        /// </summary>
        #region initTalkWindow
        protected virtual void initTalkWindow()
        {

            //オーパシティ
            this.Opacity = 0.8;
            this.trc.Value = 80;
        }
        #endregion

        ///====================================================================
        ///
        ///                          onDelete
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
        /// ActivityTalk_FormClosing
        /// フォームクロージングキャンセラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityTalk_FormClosing
        protected void ActivityTell_FormClosing(object sender, FormClosingEventArgs e)
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
        ///                             onRecive
        ///                         
        ///====================================================================
        #region onRecive
        private void ActivityTell_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void ActivityTell_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        /// <summary>
        /// 発言送信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            sendMessage();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trc_Scroll(object sender, EventArgs e)
        {
            if (this.trc.Value < 20)
            {
                this.Opacity = 0.2;
                this.trc.Value = 20;
            }
            else
            {
                this.Opacity = this.trc.Value * 0.01;
            }
            
        }

        private void txtSendData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                sendMessage();
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                              処理
        ///                         
        ///====================================================================

        #region 処理
        /// <summary>
        /// メッセージを送信する
        /// </summary>
        private void sendMessage()
        {
            if (txtSendData.Text.Equals(""))
            {
                return;
            }

            lstLog.Items.Add(txtSendData.Text);
            lips.onRecive(LiplisDefine.LM_TELL_SEND, txtSendData.Text);
            txtSendData.Text = "";
        }

        /// <summary>
        /// 返答メッセージをセットする
        /// </summary>
        /// <param name="msg"></param>
        public void setResponse(string m)
        {
            string msg = oss.charName + ":" + m;
            Invoke(new LpsDelegate.dlgS1ToVoid(addLog),msg);
        }


        /// <summary>
        /// アッドログ
        /// </summary>
        /// <param name="s"></param>
        public void addLog(string s)
        {
            if (lstLog.Items.Count > 1000) { lstLog.Items.RemoveAt(0); }
            lstLog.TopIndex = lstLog.Items.Count - 1;
            lstLog.Items.Add(s);
        }
        #endregion

    }
}
