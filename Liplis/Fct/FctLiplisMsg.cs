//=======================================================================
//  ClassName : ObjBody
//  概要      : ボディオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using Liplis.Msg;

namespace Liplis.Fct
{
    public class FctLiplisMsg
    {
        /// <summary>
        /// createMsgMassageDlFaild
        /// データ取得失敗メッセージの作成
        /// </summary>
        #region createMsgMassageDlFaild
        public static MsgShortNews createMsgMassageDlFaild()
        {
            MsgShortNews msg = new MsgShortNews();

            msg.result = "データの取得に失敗しました。";

            msg.nameList.Add("データ");
            msg.nameList.Add("の");
            msg.nameList.Add("取得");
            msg.nameList.Add("に");
            msg.nameList.Add("失敗");
            msg.nameList.Add("し");
            msg.nameList.Add("まし");
            msg.nameList.Add("た。");

            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);
            msg.emotionList.Add(1);

            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);
            msg.pointList.Add(-1);

            return msg;

        }
        #endregion
    }
}
