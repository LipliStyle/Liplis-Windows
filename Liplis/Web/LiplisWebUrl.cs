//=======================================================================
//  ClassName : LiplisWebUrl
//  概要      : URLを作成する
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Specialized;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Msg;


namespace Liplis.Web
{
    public class LiplisWebUrl
    {
        /// <summary>
        /// crtUrlNicoVideo
        /// ニコニコ動画のURLを作成する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region crtUrlNicoVideo
        public static string crtUrlNicoVideo(string id)
        {
            return LpsDefineMost.URL_NICO_VIDEO + id;
        }
        #endregion

        /// <summary>
        /// crtUrlNicoMylist
        /// ニコニコマイリストのURLを作成する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region crtUrlNicoMylist
        public static string crtUrlNicoMylist(string myListId)
        {
            return LpsDefineMost.URL_NICO_DOMAIN + myListId;
        }
        #endregion
    }
}
