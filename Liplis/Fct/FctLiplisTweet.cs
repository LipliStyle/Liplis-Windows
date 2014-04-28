//=======================================================================
//  ClassName : ObjBody
//  概要      : ボディオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using Liplis.Msg;
using Liplis.Web;


namespace Liplis.Fct
{
    public static class FctLiplisTweet
    {
        /// <summary>
        /// ツイートする
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        public static void tweet(string uid,string msg)
        {
            LiplisApiCus.tweet(uid, msg);
        }

        /// <summary>
        /// ツイッター向けメッセージを作成する
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string createLiplisTweet(string title, string msg)
        {
            string tweetMessage = "";

            if (msg.Length > 130)
            {
                tweetMessage = title;
            }
            else
            {
                tweetMessage = msg;
            }


            return tweetMessage + " #Liplis";
        }
        public static string createLiplisTweet(string msg)
        {
            string tweetMessage = "";

            if (msg.Length > 130)
            {
                tweetMessage = msg.Substring(0,130);
            }
            else
            {
                tweetMessage = msg;
            }


            return tweetMessage + " #Liplis";
        }


    }
}
