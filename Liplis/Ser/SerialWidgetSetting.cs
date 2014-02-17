//=======================================================================
//  ClassName : FctRssObjectCreater
//  概要      : RSSオブジェクトクリエイター
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Liplis.Common;
using Liplis.Msg;

namespace Liplis.Ser
{
    public static class SerialWidgetSetting
    {
        /// <summary>
        /// オブジェクトの内容をファイルから読み込み復元する
        /// </summary>
        /// <param name="path">読み込むファイル名</param>
        /// <returns>復元されたオブジェクト</returns>
        #region loadRssObject
        public static ObjWidgetSetting loadObject()
        {
            try
            {
                //RSS設定ファイルの存在チェック
                if (!LpsPathControllerCus.checkFileExist(LpsPathControllerCus.getWidgetSettingPath()))
                {
                    //存在しなければ作成しておく
                    saveObject(new ObjWidgetSetting());
                }

                //オブジェクトを取得し、返す
                using (FileStream fs = new FileStream(LpsPathControllerCus.getWidgetSettingPath(), FileMode.Open, FileAccess.Read))
                {
                    return (ObjWidgetSetting)new BinaryFormatter().Deserialize(fs);
                } 
            }
            catch
            {
                LpsLiplisUtil.DeleteFile((LpsPathControllerCus.getWidgetSettingPath()));
                return loadObject();
            }

        }
        #endregion

        /// <summary>
        /// オブジェクトの内容をファイルに保存する
        /// </summary>
        /// <param name="obj">保存するオブジェクト</param>
        /// <param name="path">保存先のファイル名</param>
        #region saveObject
        public static void saveObject(ObjWidgetSetting obj)
        {
            using (FileStream fs = new FileStream(LpsPathControllerCus.getWidgetSettingPath(), FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
                fs.Flush();
            }
        }
        #endregion
    }
}
