using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Liplis.Common;
using Liplis.Activity;
using System.Windows.Forms;
using System.Threading;


namespace Liplis.Web
{
    public class LiplisTwitterOAuth
    {
        public static void lpsWitterOAuth(Liplis.MainSystem.Liplis lips)
        {
            LiplisTwitterOAuthBase oauth = new LiplisTwitterOAuthBase();

            // トークン格納用
            Dictionary<string, string> tokens = new Dictionary<string, string>();


            //---------------------------
            // 0.リクエストトークン取得の前処理
            //---------------------------

            // ランダム文字列の生成
            string nonce = oauth.GenerateNonce();
            // タイムスタンプ（unix時間）
            string timestamp = oauth.GenerateTimeStamp();
            string normalizedUrl, normalizedReqParams;

            Uri reqUrl = new Uri(LiplisDefine.REQUEST_TOKEN_URL);

            // Consumer_Secretを暗号鍵とした署名の生成
            string signature = oauth.GenerateSignature(reqUrl
                                                    , LiplisDefine.TWITTER_OAUTH_CONSUMERKEY
                                                    , LiplisDefine.TWITTER_OAUTH_CONSUMERSECRET
                                                    , null
                                                    , null
                                                    , "GET"
                                                    , timestamp
                                                    , nonce
                                                    , LiplisTwitterOAuthBase.SignatureTypes.HMACSHA1
                                                    , out normalizedUrl
                                                    , out normalizedReqParams);

            /// リクエストトークン取得用URL
            string reqTokenUrl = normalizedUrl + "?"
                               + normalizedReqParams
                               + "&oauth_signature=" + signature;

            try
            {
                //---------------------------
                // 1.リクエストトークン取得
                //---------------------------

                WebClient client = null;
                Stream st = null;
                StreamReader sr = null;

                try
                {
                    client = new WebClient();
                    Thread.Sleep(1000);
                    st = client.OpenRead(reqTokenUrl);
                    Thread.Sleep(1000);
                    sr = new StreamReader(st, Encoding.GetEncoding("Shift_JIS"));
                }
                catch 
                {
                    client = new WebClient();
                    Thread.Sleep(1000);
                    st = client.OpenRead(reqTokenUrl);
                    Thread.Sleep(1000);
                    sr = new StreamReader(st, Encoding.GetEncoding("Shift_JIS"));
                }
                

                tokens = convertToTokenForOauth(sr.ReadToEnd());

                // 取得したリクエストトークン
                Console.WriteLine(
                      "(request)oauth_token        = {0}\r\n"
                    + "(requrst)oauth_token_secret = {1}\r\n"
                    , tokens["oauth_token"]
                    , tokens["oauth_token_secret"]
                     );


                //---------------------------
                // 2.オーサライズ
                //---------------------------

                string authorizeUrl = LiplisDefine.AUTHORIZE_URL + "?"
                                        + "oauth_token=" + tokens["oauth_token"]
                                        + "&oauth_token_secret=" + tokens["oauth_token_secret"];

                // ブラウザ起動しPINコードを表示
                System.Diagnostics.Process.Start(authorizeUrl);


                //---------------------------
                // 3.PINコード認証
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
                // 4.アクセストークン取得
                //---------------------------

                // リクエストトークンを加えsignatureを再生成
                signature = oauth.GenerateSignature(reqUrl
                                                    , LiplisDefine.TWITTER_OAUTH_CONSUMERKEY
                                                    , LiplisDefine.TWITTER_OAUTH_CONSUMERSECRET
                                                    , tokens["oauth_token"]
                                                    , tokens["oauth_token_secret"]
                                                    , "GET"
                                                    , timestamp
                                                    , nonce
                                                    , LiplisTwitterOAuthBase.SignatureTypes.HMACSHA1
                                                    , out normalizedUrl
                                                    , out normalizedReqParams);


                // アクセストークン取得用URL
                string accessTokenUrl = LiplisDefine.ACCESS_TOKEN_URL + "?"
                                            + normalizedReqParams
                                            + "&oauth_signature=" + signature
                                            + "&oauth_verifier=" + pin;

                st = client.OpenRead(accessTokenUrl);
                sr = new StreamReader(st, Encoding.GetEncoding("Shift_JIS"));

                tokens = convertToTokenForOauth(sr.ReadToEnd());

                lips.registerTwitterInfo(tokens["oauth_token"],tokens["oauth_token_secret"]);
                MessageBox.Show("アクセストークンの取得に成功しました。", "TwitterPinコード送信");

            }
            catch (Exception)
            {
                MessageBox.Show("アクセストークンの取得に失敗しました。", "TwitterPinコード送信");
            }
        }

        /// <summary>
        /// 取得した文字列を分解し、ハッシュテーブルに格納する
        /// </summary>
        /// <param name="data">文字列</param>
        /// <param name="oauthKey">ハッシュテーブル</param>
        static private Dictionary<string, string> convertToTokenForOauth(string data)
        {
            Dictionary<string, string> oauthKey = new Dictionary<string, string>();

            foreach (string s in data.Split('&'))
            {

                oauthKey.Add(s.Substring(0, s.IndexOf("="))
                    , s.Substring(s.IndexOf("=") + 1));
            }

            return oauthKey;
        }
    }
}
