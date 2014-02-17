//=======================================================================
//  ClassName : FctRssObjectCreater
//  概要      :CATオブジェクトクリエイター
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Msg;

namespace Liplis.Ser
{
    public static class SerialCatObject
    {
        /// <summary>
        /// RSSオブジェクトの内容をファイルから読み込み復元する
        /// </summary>
        /// <param name="path">読み込むファイル名</param>
        /// <returns>復元されたオブジェクト</returns>
        #region loadRssObject
        public static ObjCat loadCatObject()
        {
            //RSS設定ファイルの存在チェック
            if (!LpsPathControllerCus.checkFileExist(LpsPathControllerCus.getCatSettingPath()))
            {
                //存在しなければ作成しておく
                saveRssObject(new ObjCat());
            }

            //オブジェクトを取得し、返す
            try
            {
                using (FileStream fs = new FileStream(LpsPathControllerCus.getCatSettingPath(), FileMode.Open, FileAccess.Read))
                {
                    return (ObjCat)new BinaryFormatter().Deserialize(fs);
                } 
            }
            catch
            {
                //存在しなければ作成しておく
                saveRssObject(new ObjCat());

                using (FileStream fs = new FileStream(LpsPathControllerCus.getCatSettingPath(), FileMode.Open, FileAccess.Read))
                {
                    return (ObjCat)new BinaryFormatter().Deserialize(fs);
                } 
            }
            
        }
        #endregion

        /// <summary>
        /// RSSオブジェクトの内容をファイルに保存する
        /// </summary>
        /// <param name="obj">保存するオブジェクト</param>
        /// <param name="path">保存先のファイル名</param>
        #region saveRssObject
        public static void saveRssObject(ObjCat obj)
        {
            using (FileStream fs = new FileStream(LpsPathControllerCus.getCatSettingPath(), FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
                fs.Flush();
            }
        }
        #endregion
    }
}
