//=======================================================================
//  ClassName : ActivityTwitterActivation
//  概要      : ツイッターアクティベーション
//
//  Liplis2.3
//  2013/06/20 var2.3.0 作成
/// 2014/02/02 ver3.2.4 ツイッターの認証方式変更
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Windows.Forms;
using Liplis.Web;

namespace Liplis.Activity
{
    public partial class ActivityTwitterActivation : Form
    {
        ///=====================================
        /// オブジェクト
        //private Liplis.MainSystem.Liplis lips;
        //private TwitterService tokenGetObject;
        //private OAuthRequestToken reqToken;
        public string pin { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="tokenGetObject"></param>
        /// <param name="reqToken"></param>
        #region コンストラクター
        //public ActivityTwitterActivation(Liplis.MainSystem.Liplis lips, TwitterService tokenGetObject, OAuthRequestToken reqToken)
        //{
        //    InitializeComponent();
        //    this.lips = lips;
        //    this.tokenGetObject = tokenGetObject;
        //    this.reqToken = reqToken;
        //    this.KeyPreview = true;
        //}
        public ActivityTwitterActivation()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
        #endregion
        
        /// <summary>
        /// ピンコード取得
        /// 2014/02/02 ver3.2.4 ツイッターの認証方式変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnSendPin_Click
        //private void btnSendPin_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //認証処理
        //        OAuthAccessToken access = tokenGetObject.GetAccessToken(reqToken, txtPin.Text);
        //        tokenGetObject.AuthenticateWith(access.Token, access.TokenSecret);
        //        btnSendPin.Enabled = false;

        //        //アクセストークンを登録する
        //        lips.registerTwitterInfo(access.Token, access.TokenSecret);

        //        MessageBox.Show("アクセストークンの取得に成功しました。", "TwitterPinコード送信");
        //    }
        //    catch
        //    {
        //        MessageBox.Show("アクセストークンの取得に失敗しました。", "TwitterPinコード送信");
        //    }
        //    this.Close();
        //}

        private void btnSendPin_Click(object sender, EventArgs e)
        {
            try
            {
                pin = txtPin.Text;
            }
            catch
            {
            }
            this.Close();
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
        /// txtPin_KeyDown
        /// キーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region txtPin_KeyDown
        private void txtPin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                btnSendPin_Click(null, null);
            }
        }
        private void ActivityTwitterActivation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        
        
    }
}
