//=======================================================================
//  ClassName : SerialWidgetList
//  概要      : ウィジェットリスト保存データ
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
    public class SerialWidgetList
    {
        /// <summary>
        /// オブジェクトの内容をファイルから読み込み復元する
        /// </summary>
        /// <param name="path">読み込むファイル名</param>
        /// <returns>復元されたオブジェクト</returns>
        #region loadRssObject
        public static WidgetSettingList loadObject()
        {
            try
            {
                //RSS設定ファイルの存在チェック
                if (!LpsPathControllerCus.checkFileExist(LpsPathControllerCus.getWidgetListPath()))
                {
                    //存在しなければ作成しておく
                    saveObject(new WidgetSettingList());
                }

                //オブジェクトを取得し、返す
                using (FileStream fs = new FileStream(LpsPathControllerCus.getWidgetListPath(), FileMode.Open, FileAccess.Read))
                {
                    return (WidgetSettingList)new BinaryFormatter().Deserialize(fs);
                }
            }
            catch
            {
                LpsLiplisUtil.DeleteFile((LpsPathControllerCus.getWidgetListPath()));
                return loadObject();
            }

        }
        #endregion

        /// <summary>
        /// オブジェクトの内容をファイルに保存する
        /// </summary>
        /// <param name="obj">保存するオブジェクト</param>
        /// <param name="path">保存先のファイル名</param>
        #region saveRssObject
        public static void saveObject(WidgetSettingList obj)
        {
            using (FileStream fs = new FileStream(LpsPathControllerCus.getWidgetListPath(), FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
                fs.Flush();
            }
        }
        #endregion

    }
}
