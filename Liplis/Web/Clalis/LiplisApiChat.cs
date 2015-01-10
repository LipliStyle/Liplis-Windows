//=======================================================================
//  ClassName : LiplisApiChat
//  概要      : チャットAPI
//
//  Liplis4.5
//  Copyright(c) 2010-2015 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using Liplis.Common;
using Liplis.Msg;
using Newtonsoft.Json;

namespace Liplis.Web.Clalis
{
    public class LiplisApiChat : LiplisApiBase
    {
        ///=====================================
        /// 会話継続コンテキスト
        private string mode { get; set; }
        private string context { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LiplisApiChat(Liplis.MainSystem.Liplis lips)
        {
            this.lips = lips;
            this.mode = "";
            this.context = "";
        }

        /// <summary>
        /// APIにポストする
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="toneUrl"></param>
        /// <param name="version"></param>
        /// <param name="sentence"></param>
        public void apiPost(string uid, string toneUrl, string version, string sentence)
        {
            LpsLogControllerCus.d("getTellResponse");
            NameValueCollection ps = new NameValueCollection();
            ps.Add("userid", uid);                    //TONE_URLの指定
            ps.Add("tone", toneUrl);                  //TONE_URLの指定
            ps.Add("version", version);               //TONE_URLの指定
            ps.Add("sentence", sentence);             //NEWS_FLGの指定
            ps.Add("mode", mode);                 //MODEの指定
            ps.Add("context", context);              //CONTEXTの指定

            object[] obj = new object[5];

            obj[0] = LiplisDefine.LIPLIS_CHAT;
            obj[1] = ps;

            Thread thread = new Thread(new ParameterizedThreadStart(postThread));
            thread.Start(obj);
        }

        /// <summary>
        /// 実行メソッド
        /// </summary>
        /// <param name="args"></param>
        private void postThread(object args)
        {
            // 引数の展開
            object[] argsTmp = (object[])args;

            //ポストする
            post((string)argsTmp[0], (NameValueCollection)argsTmp[1]);
        }

        /// <summary>
        /// コールバックから呼び出されるアクション
        /// </summary>
        /// <param name="source"></param>
        protected override void action(string source)
        {
            Console.WriteLine(source);
            ResLpsChatResponse result = JsonConvert.DeserializeObject<ResLpsChatResponse>(source);

            lips.chatGetResponse(convertRlSumNjToMsg(result));
        }


        /// <summary>
        /// convertRlSumNjToMsg
        /// ResLpsSummaryNews2Jsonからショートニュースメッセージに変換する
        /// </summary>
        /// <param name="ols"></param>
        /// <returns></returns>
        #region convertRlSumNjToMsg
        protected MsgShortNews convertRlSumNjToMsg(ResLpsChatResponse rlsn2)
        {
            //ディスクリプションチェック
            if (rlsn2 == null || rlsn2.descriptionList.Count < 1)
            {
                return lips.getOlc().getChatWord("noreply");
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

            //データの作成
            msg.result = sbResult.ToString();
            msg.sorce = sbResult.ToString();
            msg.title = "";

            string result = sbResult.ToString().Replace("EOS", "");

            //結果をメッセージに格納
            msg.url = LpsLiplisUtil.nullCheck(rlsn2.url);
            msg.title = LpsLiplisUtil.nullCheck(rlsn2.title);
            msg.result = result;
            msg.sorce = result;
            msg.calcNewsEmotion();

            //
            if(rlsn2.opList.Count == 2)
            {
                this.context = rlsn2.opList[0];
                this.mode = rlsn2.opList[1];
            }

            return msg;
        }
        #endregion
    }
}
