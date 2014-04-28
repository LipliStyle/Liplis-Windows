using System;
//=======================================================================
//  ClassName : LiplisApi
//  概要      : LiplisApiとのインターフェースクラス
//
//  Liplis4.0
//  Copyright(c) 2010-2014 LipliStyle.Sachin
//
//
//  2014/04/07 Liplis4.0 Clalis4.0対応
//=======================================================================
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Msg;
using Newtonsoft.Json;

namespace Liplis.Web
{
    public class LiplisApiCus : LiplisApi
    {
        ///====================================================================
        ///
        ///                         サマリーニュース
        ///                        
        ///====================================================================

        /// <summary>
        /// getSummaryNews
        /// サマリーにゅーすの取得
        /// </summary>
        /// <returns></returns>
        #region getSummaryNews
        public static MsgShortNews getSummaryNews(string uid, string toneUrl, string newsFlg)
        {
            MsgShortNews msg = new MsgShortNews();
            try
            {
                LpsLogControllerCus.d("getSummaryNews");
                NameValueCollection ps = new NameValueCollection();
                ps.Add("tone", toneUrl);                //TONE_URLの指定
                ps.Add("newsFlg", newsFlg);             //NEWS_FLGの指定

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_SUMMARY_NEWS, ps);

                //APIの結果受け取り用クラス
                ResLpsSummaryNews2Json result = JsonConvert.DeserializeObject<ResLpsSummaryNews2Json>(jsonText);

                //2013/06/23 ver3.0.2タグ付与のタイミングを変更
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
        #endregion

        /// <summary>
        /// getSummaryNewsList
        /// サマリーニュースリストの取得
        /// Liplis3.1.0 URL変更、引数追加
        /// Liplis4.0.0 キューに直接投入するように変更
        /// </summary>
        /// <returns></returns>
        #region getSummaryNewsList
        public static void getSummaryNewsList(Queue<MsgShortNews> newsQ, string uid, string toneUrl, string newsFlg, string num, string hour, string already, string twitterMode, string runout)
        {
            try
            {
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);              //UIDLの指定
                ps.Add("tone", toneUrl);            //TONE_URLの指定
                ps.Add("newsFlg", newsFlg);         //ニュースフラグの指定
                ps.Add("num", num);                 //個数
                ps.Add("hour", hour);               //時間範囲の指定
                ps.Add("already", already);         //オールレディ
                ps.Add("twitterMode", twitterMode); //ツイッターモード
                ps.Add("runout", runout);           //ランアウト

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_SUMMARY_NEWS_LIST, ps);

                //APIの結果受け取り用クラス
                ResLpsSummaryNews2JsonList result = JsonConvert.DeserializeObject<ResLpsSummaryNews2JsonList>(jsonText);

                //取得したリストをメッセージリストに変換する
                foreach (ResLpsSummaryNews2Json rlsn2 in result.lstNews)
                {
                    MsgShortNews msg = convertRlSumNjToMsg(rlsn2);
                    FctTagFactory.setTag(msg);
                    newsQ.Enqueue(msg);
                }
            }
            catch{}
            return;
        }
        #endregion

        ///====================================================================
        ///
        ///                         ショートニュース
        ///                        
        ///====================================================================

        /// <summary>
        /// getSummaryNews
        /// ショートにゅーすの取得
        /// </summary>
        /// <returns></returns>
        #region getShortNews
        public static MsgShortNews getShortNews(string uid, string toneUrl, string newsFlg)
        {
            MsgShortNews msg = new MsgShortNews();
            try
            {
                LpsLogControllerCus.d("getSummaryNews");
                NameValueCollection ps = new NameValueCollection();
                ps.Add("tone", toneUrl);                //TONE_URLの指定
                ps.Add("newsFlg", newsFlg);             //NEWS_FLGの指定

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_SHORT_NEWS, ps);

                //APIの結果受け取り用クラス
                ResLpsShortNews2Json result = JsonConvert.DeserializeObject<ResLpsShortNews2Json>(jsonText);

                //結果を返す
                return convertRlShtNjToMsg(result);
            }
            catch
            {
                return msg;
            }
        }
        #endregion

        /// <summary>
        /// getShortNewsList
        /// ショートニュースリストの取得
        /// Liplis4.0.0 キューに直接投入するように変更
        /// </summary>
        /// <returns></returns>
        #region getShortNewsList
        public static void getShortNewsList(Queue<MsgShortNews> newsQ, string uid, string toneUrl, string newsFlg, string num, string hour, string already, string twitterMode, string runout)
        {
            try
            {
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);              //UIDLの指定
                ps.Add("tone", toneUrl);            //TONE_URLの指定
                ps.Add("newsFlg", newsFlg);         //ニュースフラグの指定
                ps.Add("num", num);                 //個数
                ps.Add("hour", hour);               //時間範囲の指定
                ps.Add("already", already);         //オールレディ
                ps.Add("twitterMode", twitterMode); //ツイッターモード
                ps.Add("runout", runout);           //ランアウト

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_SHORT_NEWS_LIST, ps);

                //APIの結果受け取り用クラス
                ResLpsShortNews2JsonList result = JsonConvert.DeserializeObject<ResLpsShortNews2JsonList>(jsonText);

                //取得したリストをメッセージリストに変換する
                foreach (ResLpsShortNews2Json rlsn2 in result.lstNews)
                {
                    newsQ.Enqueue(convertRlShtNjToMsg(rlsn2));
                }

                return;
            }
            catch
            {
                return;
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                          フリーワード
        ///                        
        ///====================================================================

        /// <summary>
        /// getFreeWord
        /// フリーワードの取得
        /// </summary>
        /// <returns></returns>
        #region getFreeWord
        public static MsgShortNews getFreeWord(string sentence,string tone)
        {
            MsgShortNews msg = new MsgShortNews();
            try
            {
                LpsLogControllerCus.d("getFreeWord");
                NameValueCollection ps = new NameValueCollection();

                ps.Add("tone", tone);                //TONE_URLの指定
                ps.Add("sentence", sentence);             //NEWS_FLGの指定

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_FREE_TALK, ps);

                //APIの結果受け取り用クラス
                ResLpsShortNews2Json result = JsonConvert.DeserializeObject<ResLpsShortNews2Json>(jsonText);

                //結果を返す
                return convertRlShtNjToMsg(result);
            }
            catch
            {
                return msg;
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                            RSS関連
        ///                        
        ///====================================================================

        /// <summary>
        /// RSSリストを取得する
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="title">タイトル</param>
        /// <param name="cat">カテゴリ</param>
        /// <param name="uid">UID</param>
        #region getRssList
        public static ResLpsLoginRegisterInfoRssEachCat getRLLRIREC(string uid)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_GET_RSSLIST, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginRegisterInfoRssEachCat>(jsonText);
            }
            catch
            {
                return new ResLpsLoginRegisterInfoRssEachCat();
            }
        }
        #endregion

        /// <summary>
        /// getRssList
        /// RSSリストを取得する
        /// </summary>
        #region getRssList
        public static ObjRssList getRssList(string uid)
        {
            ObjRssList resList = new ObjRssList();

            ResLpsLoginRegisterInfoRssEachCat getList = LiplisApiCus.getRLLRIREC(uid);

            //rllriからRSSリストを作成する
            foreach (RegisterRsUserInfoCat rric in getList.rsslist)
            {
                //カテゴリーの器を作成する。
                ObjRssCatList orcl = new ObjRssCatList(rric.cat);

                //回してObjRssCatListに変換する。
                foreach (RegisterRsUserInfo rrui in rric.rsslist)
                {
                    orcl.rssList.Add(new ObjRss(rrui.title, rrui.url, rrui.cat));
                }

                resList.rssCatList.Add(orcl);
            }


            return resList;
        }
        #endregion

      　/// <summary>
        /// RSS登録
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="title">タイトル</param>
        /// <param name="cat">カテゴリ</param>
        /// <param name="uid">UID</param>
        #region registRss
        public static ResLpsLoginStatus registRss(string url, string cat, string uid)
        {
            try
            {
                if (cat == "デフォルトカテゴリ")
                {
                    cat = "";
                }

                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);
                ps.Add("addRssUrl", url);     //ディスクリプションの指定
                ps.Add("addRssCat", cat);        //トーンURLの指定

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_REGISTER_RSS, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText);
            }
            catch
            {
                return new ResLpsLoginStatus("-1", uid);
            }
        }
        #endregion

        /// <summary>
        /// RSS削除
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="title">タイトル</param>
        /// <param name="cat">カテゴリ</param>
        /// <param name="uid">UID</param>
        #region registRss
        public static ResLpsLoginStatus deleteRss(string uid, string url)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);
                ps.Add("addRssUrl", url);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_DELETE_RSS, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText);
            }
            catch
            {
                return new ResLpsLoginStatus();
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                         ツイッター関連
        ///                        
        ///====================================================================

        /// <summary>
        /// ツイッターリストを取得する
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="title">タイトル</param>
        /// <param name="cat">カテゴリ</param>
        /// <param name="uid">UID</param>
        #region getTwitterList
        public static ResLpsLoginRegisterInfoTw getTwitterList(string uid)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_GET_TWITTERLIST, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginRegisterInfoTw>(jsonText);
            }
            catch
            {
                return new ResLpsLoginRegisterInfoTw();
            }
        }
        #endregion

        /// <summary>
        ///TWITTER登録
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="title">タイトル</param>
        /// <param name="cat">カテゴリ</param>
        /// <param name="uid">UID</param>
        #region registTwitter
        public static ResLpsLoginStatus registTwitter(string userName, string uid)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);
                ps.Add("addUser", userName);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_REGISTER_TWITTER, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText);
            }
            catch
            {
                return new ResLpsLoginStatus("-1", uid);
            }
        }
        #endregion

        /// <summary>
        /// TWITTER削除
        /// </summary>
        /// <param name="userName">URL</param>
        /// <param name="title">タイトル</param>
        /// <param name="cat">カテゴリ</param>
        /// <param name="uid">UID</param>
        #region deleteTwitter
        public static ResLpsLoginStatus deleteTwitter(string uid, string userName)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);
                ps.Add("delUser", userName);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_DELETE_TWITTER, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText);
            }
            catch
            {
                return new ResLpsLoginStatus();
            }
        }
        #endregion

        /// <summary>
        /// TWITTER情報(トークンとキー)登録
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="title">タイトル</param>
        /// <param name="cat">カテゴリ</param>
        /// <param name="uid">UID</param>
        #region twitterRegister
        public static ResLpsRegisterTwitterInfoRespons twitterRegister(string uid, string token, string secret, string userId, string screanNam)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);
                ps.Add("token", token);
                ps.Add("secret", secret);
                ps.Add("twitteruid", userId);
                ps.Add("twittersname", screanNam);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_REGISTER_TWITTER_USER_INFO, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsRegisterTwitterInfoRespons>(jsonText);
            }
            catch
            {
                return new ResLpsRegisterTwitterInfoRespons("-1");
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                         ツイッター関連
        ///                        
        ///====================================================================
        
        /// <summary>
        /// フィルターリストを取得する
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        #region getFilterList
        public static ResLpsTopicSearchWordList getFilterList(string uid)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_GET_SEARCH_WORD, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsTopicSearchWordList>(jsonText);
            }
            catch
            {
                return new ResLpsTopicSearchWordList();
            }
        }
        #endregion

        /// <summary>
        /// フィルター登録
        /// </summary>
        /// <param name="uid">UID</param>
        /// <param name="word">対象語</param>
        /// <param name="enableFlg">有効フラグ</param>
        /// <returns></returns>
        #region registFilter
        public static ResLpsLoginStatus registFilter(string uid, string topicId, string word, string flgEnable)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);
                ps.Add("topicId", topicId);
                ps.Add("word", word);
                ps.Add("flgEnable", flgEnable);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_REGISTER_SEARCH_WORD, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText);
            }
            catch
            {
                return new ResLpsLoginStatus("-1", uid);
            }
        }
        #endregion

        /// <summary>
        /// フィルター削除
        /// </summary>
        /// <param name="uid">UID</param>
        /// <param name="word">対象語</param>
        /// <param name="enableFlg">有効フラグ</param>
        /// <returns></returns>
        #region deleteFilter
        public static ResLpsLoginStatus deleteFilter(string uid, string topicId, string word)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);
                ps.Add("topicId", topicId);
                ps.Add("word", word);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_DELETE_SEARCH_WORD, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText);
            }
            catch
            {
                return new ResLpsLoginStatus();
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="topicId"></param>
        /// <returns></returns>
        #region registFilter
        public static ResLpsLoginStatus tweet(string uid, string sentence)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);
                ps.Add("sentence", sentence);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_TWEET, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText);
            }
            catch
            {
                return new ResLpsLoginStatus("-1", uid);
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                      ワンタイムパスワード関連
        ///                        
        ///====================================================================

        /// <summary>
        /// getOneTimePass
        /// ワンタイムパスワードを取得する
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        #region getOneTimePass
        public static ResUserOnetimePass getOneTimePass(string uid)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userid", uid);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_GET_ONETIME_PASS, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResUserOnetimePass>(jsonText);
            }
            catch
            {
                return new ResUserOnetimePass("-1");
            }
        }
        #endregion

        /// <summary>
        /// getLiplisId
        /// リプリスIDを取得する
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        #region getLiplisId
        public static ResLiplisId getLiplisId(string oneTimePass)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("onetimePass", oneTimePass);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_GET_USERID, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLiplisId>(jsonText);
            }
            catch
            {
                return new ResLiplisId("");
            }
        }
        #endregion

        /// <summary>
        /// 設定送信
        /// </summary>
        #region saveTopicSetting
        public static ResLpsLoginStatus saveTopicSetting(string userId, string range, string already, string f_ne, string f_2c, string f_ni, string f_rs, string f_tp, string f_tm, string f_tr, string f_tmode, string f_runout)
        {
            try
            {
                //引数の指定
                NameValueCollection ps = new NameValueCollection();
                ps.Add("userId", userId);
                ps.Add("range", range);
                ps.Add("already", already);
                ps.Add("f_ne", f_ne);
                ps.Add("f_2c", f_2c);
                ps.Add("f_ni", f_ni);
                ps.Add("f_rs", f_rs);
                ps.Add("f_tp", f_tp);
                ps.Add("f_tm", f_tm);
                ps.Add("f_tr", f_tr);
                ps.Add("f_tmode", f_tmode);
                ps.Add("f_runout", f_runout);

                //Jsonで結果取得
                string jsonText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_TOPIC_SETTING, ps);

                //APIの結果受け取り用クラス
                return JsonConvert.DeserializeObject<ResLpsLoginStatus>(jsonText);
            }
            catch
            {
                return new ResLpsLoginStatus("-1", userId);
            }

        }
        #endregion

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
        private static MsgShortNews convertRlSumNjToMsg(ResLpsSummaryNews2Json rlsn2)
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

        /// <summary>
        /// convertRlShtNjToMsg
        /// ResLpsShortNews2Jsonからショートニュースメッセージに変換する
        /// </summary>
        /// <param name="ols"></param>
        /// <returns></returns>
        #region convertRlShtNjToMsg
        private static MsgShortNews convertRlShtNjToMsg(ResLpsShortNews2Json rlsn2)
        {
            //結果メッセージを作成
            MsgShortNews msg = new MsgShortNews();

            //リザルトSB
            StringBuilder sbResult = new StringBuilder();

            //ネームリスト、等作成
            string[] bufList = rlsn2.result.Split(';');

            foreach (string buf in bufList)
            {
                string[] bufList2 = buf.Split(',');

                if (buf.Length < 3) { break; }

                msg.nameList.Add(bufList2[0]);
                msg.emotionList.Add(int.Parse(bufList2[1]));
                msg.pointList.Add(int.Parse(bufList2[2]));
                sbResult.Append(bufList2[0]);
            }

            string result = sbResult.ToString().Replace("EOS", "");

            //結果をメッセージに格納
            msg.url = rlsn2.url;
            msg.title = result;
            msg.result = result;
            msg.sorce = result;
            msg.calcNewsEmotion();
            msg.jpgUrl = "";

            return msg;
        }
        #endregion
    }
}
