//=======================================================================
//  ClassName : FctRssObjectCreater
//  概要      : RSSオブジェクトクリエイター
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Liplis.Common;
using Liplis.Msg;

namespace Liplis.Fct
{
    public static class FctRssObjectCreater
    {
        /// <summary>
        /// RSSオブジェクトの内容をファイルから読み込み復元する
        /// </summary>
        /// <param name="path">読み込むファイル名</param>
        /// <returns>復元されたオブジェクト</returns>
        #region loadRssObject
        public static ObjRssList loadRssObject()
        {
            //RSS設定ファイルの存在チェック
            if (!LpsPathControllerCus.checkFileExist(LpsPathControllerCus.getRssSettingPath()))
            {
                //存在しなければ作成しておく
                saveRssObject(new ObjRssList());
            }

            //オブジェクトを取得し、返す
            using (FileStream fs = new FileStream(LpsPathControllerCus.getRssSettingPath(), FileMode.Open, FileAccess.Read))
            {
                return (ObjRssList)new BinaryFormatter().Deserialize(fs);
            } 
        }
        #endregion

        /// <summary>
        /// RSSオブジェクトの内容をファイルに保存する
        /// </summary>
        /// <param name="obj">保存するオブジェクト</param>
        /// <param name="path">保存先のファイル名</param>
        #region saveRssObject
        public static void saveRssObject(ObjRssList obj)
        {
            using (FileStream fs = new FileStream(LpsPathControllerCus.getRssSettingPath(), FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
                fs.Flush();
            }
        }
        #endregion
    }
}
