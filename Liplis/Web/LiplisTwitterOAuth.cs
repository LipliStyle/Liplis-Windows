//=======================================================================
//  ClassName : LiplisTwitterOAuth
//  概要      : ツイッター認証を行う
//
//  Liplis4.0
//  Copyright(c) 2010-2015 LipliStyle.Sachin
//
//  2015/08/18 Liplis4.5.2 Twitter登録機能バグ修正
//
//
//=======================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CoreTweet;
using Liplis.Activity;
using Liplis.Common;


namespace Liplis.Web
{
    public class LiplisTwitterOAuth
    {
        public static void lpsTwitterOAuth(Liplis.MainSystem.Liplis lips)
        {
            

            try
            {
                //---------------------------
                // 1.オーサライズセッション取得
                //---------------------------
                var session = OAuth.Authorize(LiplisDefine.TWITTER_OAUTH_CONSUMERKEY, LiplisDefine.TWITTER_OAUTH_CONSUMERSECRET);

                // ブラウザ起動しPINコードを表示
                System.Diagnostics.Process.Start(session.AuthorizeUri.ToString());

                //---------------------------
                // 2.PINコード認証
                //---------------------------
                string pin;

                //ピンコード入力画面を表示する
                using (ActivityTwitterActivation ftip = new ActivityTwitterActivation())
                {
                    //画面表示
                    ftip.ShowDialog();

                    pin = ftip.pin;
                }

                //---------------------------
                // 3.アクセストークン取得
                //---------------------------
                var tokens = session.GetTokens(pin);

                //登録
                lips.registerTwitterInfo(tokens.AccessToken, tokens.AccessTokenSecret, tokens.UserId.ToString(), tokens.ScreenName);

                MessageBox.Show("アクセストークンの取得に成功しました。", "TwitterPinコード送信");
            }
            catch (Exception)
            {
                MessageBox.Show("アクセストークンの取得に失敗しました。", "TwitterPinコード送信");
            }
        }
    }
}
