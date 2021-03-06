﻿//=======================================================================
//  ClassName : ObjTopic
//  概要      : 話題オブジェクト
//
//  Liplis3.0
//  2013/06/23 Liplis3.0.2 トピックスミニ追加
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Web;

namespace Liplis.Msg
{
    public class ObjTopic
    {
        ///=====================================
        /// 必須情報
        protected ObjSetting os;
        protected ObjSkinSetting oss;

        ///=====================================
        /// 話題キュー
        protected Queue<MsgShortNews> newsQ;

        ///=============================
        /// フラグ
        protected bool flgCollect = false;

        ///====================================================================
        ///
        ///                              初期化処理
        ///                         
        ///====================================================================

        /// <summary>
        /// ObjTopic
        /// コンストラクター
        /// </summary>
        #region ObjTopic
        public ObjTopic(ObjSetting os, ObjSkinSetting oss)
        {
            this.os = os;
            this.oss = oss;
            initObjTopic();
        }
        #endregion

        /// <summary>
        /// initObjTopic
        /// 初期化処理
        /// </summary>
        #region initObjTopic
        protected void initObjTopic()
        {
            newsQ = new Queue<MsgShortNews>();
        }
        #endregion

        ///====================================================================
        ///
        ///                              話題収集
        ///                         
        ///====================================================================

        /// <summary>
        //  MethodType : child
        /// MethodName : getShortNews
        /// ニュースの取得
        /// ☆Miniオーバーライド
        /// </summary>
        #region getShortNews
        protected virtual MsgShortNews getShortNews()
        {
            MsgShortNews result;
            try
            {
                result = LiplisApiCus.getSummaryNews(os.uid, oss.toneUrl, os.getNewsFlg());

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

        ///====================================================================
        ///
        ///                              話題取得
        ///                         
        ///====================================================================


        /// <summary>
        /// getTopic
        /// 話題を取得する
        /// 
        /// 2014/01/07 ver3.2.1 話題が尽きた時の挙動の修正
        /// </summary>
        /// <returns></returns>
        #region getTopic
        public MsgShortNews getTopic()
        {
            //ニュースキューチェック
            if (newsQ.Count > 0)
            {
                if (newsQ.Count <= 25)
                {
                    //ニュースキューの収集
                    doCollectThread();
                }
                return newsQ.Dequeue();
            }
            else
            {
                //収集要請
                doCollectThread();

                //2014/01/07 ver3.2.1 話題が尽きた時の挙動の修正
                if (os.lpsRunout == 0)
                {
                    return getShortNews();
                }
                else
                {
                    return null;
                }
                
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                              話題収集
        ///                         
        ///====================================================================

        #region 話題収集
        /// <summary>
        /// doCollectThread
        /// コレクトメソッドをスレッドを実行する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public void doCollectThread()
        {
            if (!flgCollect)
            {
                //画像作成するスレッドを生成
                Thread imgThread = new Thread(new ThreadStart(collect));

                //WebBrowserはシングルスレッドアパートメントモードでのみ実行可能なのでスレッドのモードを設定して実行する
                imgThread.SetApartmentState(ApartmentState.STA);

                //スレッドスタート
                imgThread.Start();
            }

            return;
        }

        /// <summary>
        /// collect
        /// 話題を非同期で収集する
        /// (非同期実行)
        /// ☆Miniオーバーライド
        /// 
        /// 2014/04/20 Liplis4.0 キューに直接投入するように変更 
        /// </summary>
        public virtual void collect()
        {
            if (!flgCollect)
            {
                try
                {
                    //開始時ON
                    flgCollect = true;

                    LiplisApiCus.getSummaryNewsList(newsQ, os.uid, oss.toneUrl, os.getNewsFlg(), "100", os.lpsTopicHour.ToString(), os.lpsAlready.ToString(), os.lpsTwitterMode.ToString(), os.lpsRunout.ToString());
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


        /// <summary>
        /// doCollectThread
        /// コレクトメソッドをスレッドを実行する
        /// 
        ///2015/09/04 Liplis4.5.4 話題をリロードするように変更 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public void doReloadThread()
        {
            if (!flgCollect)
            {
                //画像作成するスレッドを生成
                Thread imgThread = new Thread(new ThreadStart(reload));

                //WebBrowserはシングルスレッドアパートメントモードでのみ実行可能なのでスレッドのモードを設定して実行する
                imgThread.SetApartmentState(ApartmentState.STA);

                //スレッドスタート
                imgThread.Start();
            }

            return;
        }

        /// <summary>
        /// collect
        /// 話題を非同期で収集する
        /// (非同期実行)
        /// ☆Miniオーバーライド
        /// 
        ///2015/09/04 Liplis4.5.4 話題をリロードするように変更 
        /// </summary>
        public virtual void reload()
        {
            if (!flgCollect)
            {
                try
                {
                    //開始時ON
                    flgCollect = true;

                    LiplisApiCus.reloadSummaryNewsList(newsQ, os.uid, oss.toneUrl, os.getNewsFlg(), "100", os.lpsTopicHour.ToString(), os.lpsAlready.ToString(), os.lpsTwitterMode.ToString(), os.lpsRunout.ToString());
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
