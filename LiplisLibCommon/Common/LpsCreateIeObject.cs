//=======================================================================
//  ClassName : LpsCreateObject
//  概要      : クリエイトオブジェクト
//
//  Liplisシステム      
//  Copyright(c) 2009-2013 sachin.Sachin
//=======================================================================
using System;

namespace Liplis.Common
{
    public static class LpsCreateIeObject
    {
        /// <summary>
        /// COMオブジェクトへの参照を作成および取得する
        /// </summary>
        /// <param name="progId">作成するオブジェクトのプログラムID</param>
        /// <param name="serverName">
        /// オブジェクトが作成されるネットワーク サーバーの名前
        /// </param>
        /// <returns>作成されたCOMオブジェクト</returns>
        #region createObject
        static object createObject(string progId, string serverName)
        {
            Type t;
            if (serverName == null || serverName.Length == 0)
            {
                t = Type.GetTypeFromProgID(progId);
            }
            else
            {
                t = Type.GetTypeFromProgID(progId, serverName, true);
            }

            //インスタンス作成
            return Activator.CreateInstance(t);
        }
        #endregion
        

        /// <summary>
        /// COMオブジェクトへの参照を作成および取得する
        /// </summary>
        /// <param name="progId">作成するオブジェクトのプログラムID</param>
        /// <returns>作成されたCOMオブジェクト</returns>
        #region createObject
        public static object createObject(string progId)
        {
            return createObject(progId, null);
        }
        #endregion

        /// <summary>
        /// getIeObject
        /// IEオブジェクトを作成する
        /// </summary>
        /// <param name="progId"></param>
        /// <returns></returns>
        public static object getIeObject()
        {
            return createObject("InternetExplorer.Application");
        }
    }
}
