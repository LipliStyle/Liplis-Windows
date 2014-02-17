//=======================================================================
//  ClassName : LiplisApi
//  概要      : LiplisApiとのインターフェースクラス
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Liplis.Common;
using Liplis.Msg;
using System.Xml;

namespace Liplis.Web
{
    public class LiplisApi
    {

        ///// <summary>
        ///// ショートニュースの取得
        ///// </summary>
        ///// <returns></returns>
        //#region getShortNews
        //public static MsgShortNews getShortNews()
        //{
        //    MsgShortNews msg = new MsgShortNews();
        //    try
        //    {
        //        NameValueCollection ps = new NameValueCollection();
        //        ps.Add("tone", "");        //トーンURLの指定
        //        string xmlText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_SHORT_NEWS, ps);

        //        //結果の取得
        //        ObjLpsApiMsg apiResult = new ObjLpsApiMsg(xmlText.Replace("\r\n", "\n"));

        //        //結果をメッセージに格納
        //        msg.url = apiResult.url;
        //        msg.nameList = apiResult.nameList;
        //        msg.emotionList = apiResult.emotionList;
        //        msg.pointList = apiResult.pointList;

        //        return msg;
        //    }
        //    catch
        //    {
        //        return msg;
        //    }
        //}
        //#endregion

        ///// <summary>
        ///// getSummaryNews
        ///// サマリーにゅーすの取得
        ///// </summary>
        ///// <returns></returns>
        //#region getSummaryNews
        //public static MsgShortNews getSummaryNews(ObjTone tone, string uid)
        //{
        //    MsgShortNews msg = new MsgShortNews();
        //    try
        //    {
        //        NameValueCollection ps = new NameValueCollection();
        //        ps.Add("uid", uid);        //UIDLの指定
        //        string xmlText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_SHORT_NEWS, ps);

        //        //結果の取得
        //        ObjLpsNewsSum apiResult = new ObjLpsNewsSum(xmlText.Replace("\r\n", "\n"), tone);

        //        ///jpgのダウンロード
        //        if (!apiResult.jpgUrl.Equals(""))
        //        {
        //            msg.jpgUrl = LiplisWedFileDownLoader.downLoadthumb(apiResult.jpgUrl);
        //        }
        //        else
        //        {
        //            msg.jpgUrl = "";
        //        }

        //        if (apiResult.nameList.Count > 0)
        //        {
        //            //結果をメッセージに格納
        //            msg.url = apiResult.url;
        //            msg.title = apiResult.title;
        //            msg.nameList = apiResult.nameList;
        //            msg.emotionList = apiResult.emotionList;
        //            msg.pointList = apiResult.pointList;
        //            msg.converted = apiResult.converted;
        //            msg.calcNewsEmotion();
        //        }
        //        else
        //        {
        //            msg = new MsgShortNews();
        //        }


        //        return msg;
        //    }
        //    catch
        //    {
        //        return msg;
        //    }
        //}
        //#endregion

        ///// <summary>
        ///// getSummaryNews
        ///// URLからサマリーにゅーすの取得
        ///// </summary>
        ///// <returns></returns>
        //#region getSummaryNews
        //public static MsgShortNews getSummaryNews(ObjTone tone, string url, string uid)
        //{
        //    MsgShortNews msg = new MsgShortNews();

        //    try
        //    {
        //        NameValueCollection ps = new NameValueCollection();
        //        ps.Add("uid", uid);        //UIDLの指定
        //        ps.Add("url", url);        //UIDLの指定
        //        string xmlText = HttpPost.sendPost(LiplisDefine.LIPLIS_API_SHORT_NEWS_URL, ps);

        //        //結果の取得
        //        ObjLpsNewsSum apiResult = new ObjLpsNewsSum(xmlText.Replace("\r\n", Environment.NewLine), tone);

        //        ///jpgのダウンロード
        //        if (!apiResult.jpgUrl.Equals(""))
        //        {
        //            msg.jpgUrl = LiplisWedFileDownLoader.downLoadthumb(apiResult.jpgUrl);
        //        }
        //        else
        //        {
        //            msg.jpgUrl = "";
        //        }

        //        //結果をメッセージに格納
        //        msg.url = apiResult.url;
        //        msg.title = apiResult.title;
        //        msg.nameList = apiResult.nameList;
        //        msg.emotionList = apiResult.emotionList;
        //        msg.pointList = apiResult.pointList;
        //        msg.converted = apiResult.converted;
        //        msg.calcNewsEmotion();

        //        return msg;
        //    }
        //    catch (Exception err)
        //    {
        //        LiplisLog.d("getSummaryNews:" + err.ToString());
        //        return msg;
        //    }
        //}
        //#endregion

        ///// <summary>
        ///// RSS登録
        ///// </summary>
        ///// <param name="url">URL</param>
        ///// <param name="title">タイトル</param>
        ///// <param name="cat">カテゴリ</param>
        ///// <param name="uid">UID</param>
        //#region registRss
        //public static void registRss(string url, string title, string cat, string uid)
        //{
        //    try
        //    {
        //        //引数の指定
        //        NameValueCollection ps = new NameValueCollection();
        //        ps.Add("url", url);     //ディスクリプションの指定
        //        ps.Add("cat", cat);        //トーンURLの指定
        //        ps.Add("title", title);        //トーンURLの指定
        //        ps.Add("uid", uid);        //トーンURLの指定

        //        //ポスト
        //        HttpPost.throwPost(LiplisDefine.LIPLIS_API_RSS, ps);

        //        //返す。
        //        return;
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //}
        //#endregion


        /// <summary>
        /// 要約文を返す
        /// </summary>
        //public static msgChatInfo getSummary(msgChatInfo msg, string toneUrl)
        //{
        //    try
        //    {
        //        msgChatInfo result = msg;

        //        //引数の指定
        //        NameValueCollection ps = new NameValueCollection();
        //        ps.Add("url", msg.url);     //ディスクリプションの指定
        //        ps.Add("tone", toneUrl);        //トーンURLの指定

        //        //ポスト
        //        string xmlText = HttpPost.sendPost(ComDefine.LIPLIS_SUMMARY, ps);

        //        //テキストリーダーに変換
        //        //XmlReader tx = new XmlReader(xmlText);

        //        //結果の取得
        //        objLpsApiMsg apiResult = new objLpsApiMsg(xmlText.Replace("\r\n", "\n"));

        //        //結果をメッセージに格納
        //        result.discriptionList = apiResult.nameList;
        //        result.emotionList = apiResult.emotionList;
        //        result.pointList = apiResult.pointList;

        //        //ふぇーるセーフ
        //        if (result.discriptionList.Count < 1)
        //        {
        //            result = lpPlainText(msg, toneUrl);
        //        }

        //        //返す。
        //        return result;
        //    }
        //    catch
        //    {
        //        return msg;
        //    }
        //}


    }
}
