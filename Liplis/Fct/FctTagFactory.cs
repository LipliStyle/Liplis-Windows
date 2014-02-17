//=======================================================================
//  ClassName : LiplisDelegate
//  概要      : デリゲート定義
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Liplis.Msg;
using System.Windows.Forms;
using Liplis.Common;

namespace Liplis.Fct
{
    public class FctTagFactory
    {
        /// <summary>
        /// setTag
        /// タグを付与する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        #region setTag
        public static void setTag(MsgShortNews msg)
        {
            //ソースで既に振られてくるため廃止
            //HTMLリンクにタグを付与する
            msg.result = LpsRegularEx.fctReplaceHtmlLink(msg.result);

            //動画IDにタグを付与する
            msg.result = LpsRegularEx.fctReplaceNicoVideoId(msg.result);

            //myリストにタグを付与する
            msg.result = LpsRegularEx.fctReplaceNicoMyListId(msg.result);

            //BR変換
            msg.result = LpsRegularEx.fctReplaceNewLine(msg.result);

            //連続びっくりマークを統合
            LpsRegularEx.fctBikkuriTogo(msg.result);
        }
        #endregion

    }
}
