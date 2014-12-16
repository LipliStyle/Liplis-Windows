//=======================================================================
//  ClassName : LiplisApiGetSummaryNews
//  概要      : サマリーニュース取得API
//
//  Liplis4.5
//  Copyright(c) 2010-2014 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Msg;
using Newtonsoft.Json;

namespace Liplis.Web.Clalis
{
    public class LiplisApiGetSummaryNews : LiplisApiBase
    {
        ///
        ///
        ResLpsSummaryNews2Json result;


        /// <summary>
        /// APIにポストする
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="toneUrl"></param>
        /// <param name="version"></param>
        /// <param name="sentence"></param>
        public MsgShortNews apiPost(string uid, string toneUrl, string newsFlg)
        {
            MsgShortNews msg = new MsgShortNews();
            try
            {
                LpsLogControllerCus.d("getSummaryNews");
                NameValueCollection ps = new NameValueCollection();
                ps.Add("tone", toneUrl);                //TONE_URLの指定
                ps.Add("newsFlg", newsFlg);             //NEWS_FLGの指定

                //パラメーター設定
                object[] obj = new object[5];

                obj[0] = LiplisDefine.LIPLIS_API_SUMMARY_NEWS;
                obj[1] = ps;

                //スレッドでポストする
                Thread thread = new Thread(new ParameterizedThreadStart(postThread));
                thread.Start(obj);

                //スレッドの終了を待つ
                while (thread.IsAlive)
                {
                    Thread.Sleep(100);
                }

                //タグの付与
                FctTagFactory.setTag(msg);

                //結果を返す
                return convertRlSumNjToMsg(result);
            }
            catch
            {
                return msg;
            }
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
            result = JsonConvert.DeserializeObject<ResLpsSummaryNews2Json>(source);
        }
    }
}
