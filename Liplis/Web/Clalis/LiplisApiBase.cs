
//=======================================================================
//  ClassName : LiplisApiBase
//  概要      : LiplisAPIのベースクラス
//
//  Liplis4.5
//  Copyright(c) 2010-2014 LipliStyle.Sachin
//=======================================================================
using System.IO;
using System;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.Net.Cache;
using Liplis.Msg;
using Liplis.Fct;

namespace Liplis.Web.Clalis
{
    public abstract class LiplisApiBase
    {        
        ///=====================================
        /// 設定
        const int WEB_POST_TIMEOUT = 30000;
        const string WEB_POST_METHOD = "POST";
        const string WEB_POST_CONTENT_TYPE = "application/x-www-form-urlencoded";


        ///=====================================
        /// リプリスオブジェクト
        protected Liplis.MainSystem.Liplis lips;

        ///=====================================
        /// オブジェクト
        protected MemoryStream requestData;      ///受信データ
        protected byte[] bufferData;              //受信データバッファ

        /// <summary>
        /// ポスト処理を実装する
        /// このメソッドは必ず実装するが処理ごとに引数が異なる。
        /// </summary>
        /// <param name="url"></param>
        public void post(string url, NameValueCollection ps)
        {
            //送信データを作成
            byte[] data = getParamDataByte(ps);

            //HttpWebRequestを作成
            HttpWebRequest req = (System.Net.HttpWebRequest)WebRequest.Create(url);

            //設定
            req.Timeout = WEB_POST_TIMEOUT;
            req.Method = WEB_POST_METHOD;
            req.ContentType = WEB_POST_CONTENT_TYPE;
            req.ContentLength = data.Length;
            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);//キャッシュポリシー設定

            //パラメータ設定
            sendPostRequest(req, data);

            //非同期要求を開始
            //状態オブジェクトとしてHttpWebRequestを渡す
            AsyncCallback callback = new AsyncCallback(ResponseCallback);
            IAsyncResult r = (IAsyncResult)req.BeginGetResponse(callback, req);
        }

        /// <summary>
        /// ResponseCallback
        /// 送信完了時コールバックメソッドを実装する
        /// </summary>
        /// <param name="ar"></param>
        protected virtual void ResponseCallback(IAsyncResult ar)
        {
            try
            {
                //状態オブジェクトとして渡されたHttpWebRequestを取得
                HttpWebRequest webreq = (HttpWebRequest)ar.AsyncState;

                //非同期要求を終了
                HttpWebResponse webres = (HttpWebResponse)webreq.EndGetResponse(ar);

                //データを読み込むためのストリームを取得
                Stream st = webres.GetResponseStream();

                //データを読み込むための準備をする
                requestData = new MemoryStream();
                bufferData = new byte[1024];

                //非同期でデータの読み込みを開始
                //状態オブジェクトとしてStreamを渡す
                IAsyncResult r = (IAsyncResult)st.BeginRead(bufferData, 0, bufferData.Length, new AsyncCallback(ReadCallback), st);
            }
            catch
            {
                //アクションを発生させる
                action("");
            }
        }
        
        /// <summary>
        /// ReadCallback
        /// 読込終了時のコールバックメソッドを実装する。
        /// このメソッドで、Liplisにアクションを起こす
        /// </summary>
        /// <param name="ar"></param>
        protected virtual void ReadCallback(IAsyncResult ar)
        {
            //状態オブジェクトとして渡されたStreamを取得
            using (Stream st = (Stream)ar.AsyncState)
            {
                //データを読み込む
                int readSize = st.EndRead(ar);

                //データが読み込めたか調べる
                if (readSize > 0)
                {
                    //データが読み込めた時
                    //読み込んだデータをMemoryStreamに保存する
                    requestData.Write(bufferData, 0, readSize);

                    //再び非同期でデータを読み込む
                    IAsyncResult r = (IAsyncResult)st.BeginRead(bufferData, 0, bufferData.Length, new AsyncCallback(ReadCallback), st);
                }
                else
                {
                    //データの読み込みが終了した時
                    //ダウンロードしたデータをバイト型配列に変換
                    byte[] sourceData = requestData.ToArray();

                    //データを文字列に変換
                    string sourceHtml = System.Text.Encoding.UTF8.GetString(sourceData);

                    //アクションを発生させる
                    action(sourceHtml);

                    //閉じる
                    requestData.Close();
                }
            }
        }


        /// <summary>
        /// コールバックから呼び出され、リプリスにアクションを返す。
        /// 各APIにより処理が異なるため、各クラスで実装する
        /// </summary>
        /// <param name="source"></param>
        protected abstract void action(string source);

        /// <summary>
        /// getParamDataByte
        /// パラメータをバイトで取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected byte[] getParamDataByte(NameValueCollection postData)
        {
            string param = "";

            //バラメータの取得
            foreach (string k in postData)
            {
                param += String.Format("{0}={1}&", k, postData[k]);
            }

            //パラメータをバイト変換
            return Encoding.UTF8.GetBytes(param);
        }

        /// <summary>
        /// getWebRequest
        /// ウェブリクエストを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected void sendPostRequest(HttpWebRequest req, byte[] pramData)
        {
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(pramData, 0, pramData.Length);
            }
        }

        ///====================================================================
        ///
        ///                            汎用処理
        ///                        
        ///====================================================================

        /// <summary>
        /// convertRlSumNjToMsg
        /// ResLpsSummaryNews2Jsonからショートニュースメッセージに変換する
        /// </summary>
        /// <param name="ols"></param>
        /// <returns></returns>
        #region convertRlSumNjToMsg
        protected virtual MsgShortNews convertRlSumNjToMsg(ResLpsSummaryNews2Json rlsn2)
        {
            //ディスクリプションチェック
            if (rlsn2.descriptionList.Count < 1)
            {
                return FctLiplisMsg.createMsgMassageDlFaild();
            }

            //結果メッセージを作成
            MsgShortNews msg = new MsgShortNews();

            //リザルトSB
            StringBuilder sbResult = new StringBuilder();

            //ネームリスト、等作成
            foreach (string desc in rlsn2.descriptionList)
            {
                try
                {
                    string[] bufList = desc.Split(';');

                    foreach (string buf in bufList)
                    {
                        string[] bufList2 = buf.Split(',');

                        if (bufList2.Length == 3)
                        {
                            msg.nameList.Add(bufList2[0]);
                            msg.emotionList.Add(int.Parse(bufList2[1]));
                            msg.pointList.Add(int.Parse(bufList2[2]));
                            sbResult.Append(bufList2[0]);
                        }
                        else
                        {

                        }
                    }
                }
                catch
                {

                }
            }

            string result = sbResult.ToString().Replace("EOS", "");

            //結果をメッセージに格納
            msg.url = rlsn2.url;
            msg.title = rlsn2.title;
            msg.result = result;
            msg.sorce = result;
            msg.calcNewsEmotion();

            ///jpgのダウンロード
            if (rlsn2.jpgUrl != null && !rlsn2.jpgUrl.Equals(""))
            {
                msg.jpgUrl = LiplisWedFileDownLoader.downLoadthumb(rlsn2.jpgUrl);
            }
            else
            {
                msg.jpgUrl = "";
            }

            return msg;
        }
        #endregion


    }
}
