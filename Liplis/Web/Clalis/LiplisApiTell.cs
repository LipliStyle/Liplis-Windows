//=======================================================================
//  ClassName : LiplisApiTell
//  概要      : テルAPI
//
//  Liplis4.5
//  Copyright(c) 2010-2014 LipliStyle.Sachin
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
    public class LiplisApiTell : LiplisApiBase
    {

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LiplisApiTell(Liplis.MainSystem.Liplis lips)
        {
            this.lips = lips;
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
            ps.Add("userid", uid);                  //TONE_URLの指定
            ps.Add("tone", toneUrl);                  //TONE_URLの指定
            ps.Add("version", version);               //TONE_URLの指定
            ps.Add("sentence", sentence);             //NEWS_FLGの指定

            object[] obj = new object[5];

            obj[0] = LiplisDefine.LIPLIS_TELL;
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
            ResLpsSummaryNews2Json result = JsonConvert.DeserializeObject<ResLpsSummaryNews2Json>(source);

            lips.tellGetResponse(convertRlSumNjToMsg(JsonConvert.DeserializeObject<ResLpsSummaryNews2Json>(source)));

        }


        /// <summary>
        /// convertRlSumNjToMsg
        /// ResLpsSummaryNews2Jsonからショートニュースメッセージに変換する
        /// </summary>
        /// <param name="ols"></param>
        /// <returns></returns>
        #region convertRlSumNjToMsg
        protected override MsgShortNews convertRlSumNjToMsg(ResLpsSummaryNews2Json rlsn2)
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


            return msg;
        }
        #endregion
    }
}
