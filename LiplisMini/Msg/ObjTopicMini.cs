//=======================================================================
//  ClassName : ObjTopicMini
//  概要      : 話題オブジェクト
//
//  Liplis3.0
//  2013/06/23 Liplis3.0.2 トピックスミニ追加
//  Copyright(c) 2010-2013 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Reflection;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Web;

namespace Liplis.Msg
{
    public class ObjTopicMini : ObjTopic
    {
        /// <summary>
        /// ObjTopicMini
        /// コンストラクター
        /// </summary>
        #region ObjTopicMini
        public ObjTopicMini(ObjSetting os, ObjSkinSetting oss) :base(os,oss)
        {
            
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : getShortNews
        /// ニュースの取得
        /// </summary>
        #region getShortNews
        protected override MsgShortNews getShortNews()
        {
            MsgShortNews result;
            try
            {
                result = LiplisApiCus.getShortNews(os.uid, oss.toneUrl, os.getNewsFlg());

                if (result == null)
                {
                    result = FctLiplisMsg.createMsgMassageDlFaild();
                }
                return result;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.d(this.GetType().Name + ":" + MethodBase.GetCurrentMethod().Name + ":" + err.ToString());
                return FctLiplisMsg.createMsgMassageDlFaild();
            }
        }
        #endregion

        /// <summary>
        /// collect
        /// 話題を非同期で収集する
        /// (非同期実行)
        /// </summary>
        #region collect
        public override void collect()
        {
            if (!flgCollect)
            {
                try
                {
                    //開始時ON
                    flgCollect = true;

                    List<MsgShortNews> res = LiplisApiCus.getShortNewsList(os.uid, oss.toneUrl, os.getNewsFlg(), "100", os.lpsTopicHour.ToString(), os.lpsAlready.ToString(), os.lpsAlready.ToString(), os.lpsRunout.ToString());

                    //取得したメッセージをキューに入れる
                    foreach (MsgShortNews msg in res)
                    {
                        newsQ.Enqueue(msg);
                    }
                }
                catch
                {

                }
                finally
                {
                    //完了時OFF
                    flgCollect = false;
                }
            }
        }
        #endregion
    }
}
