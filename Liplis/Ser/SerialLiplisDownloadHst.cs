//=======================================================================
//  ClassName : SerialLiplisContentDownloder
//  概要      : シリアルコンテントダウンローダー
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System.IO;
using Liplis.Common;
using Liplis.MainSystem;
using System.Runtime.Serialization.Formatters.Binary;
using Liplis.Msg;

namespace Liplis.Ser
{
    public class SerialLiplisDownloadHst
    {
        /// <summary>
        /// オブジェクトの内容をファイルから読み込み復元する
        /// </summary>
        /// <param name="path">読み込むファイル名</param>
        /// <returns>復元されたオブジェクト</returns>
        #region loadObject
        public static ObjDownloadHst loadObject()
        {
            //ファイルの存在チェック
            if (!LpsPathControllerCus.checkFileExist(LpsPathControllerCus.getLcdSettingPath()))
            {
                //存在しなければ作成しておく
                saveObject(new ObjDownloadHst());
            }

            //オブジェクトを取得し、返す
            using (FileStream fs = new FileStream(LpsPathControllerCus.getLcdSettingPath(), FileMode.Open, FileAccess.Read))
            {
                return (ObjDownloadHst)new BinaryFormatter().Deserialize(fs);
            }
        }
        #endregion

        /// <summary>
        /// オブジェクトの内容をファイルに保存する
        /// </summary>
        /// <param name="obj">保存するオブジェクト</param>
        /// <param name="path">保存先のファイル名</param>
        #region saveObject
        public static void saveObject(ObjDownloadHst obj)
        {
            using (FileStream fs = new FileStream(LpsPathControllerCus.getLcdSettingPath(), FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
                fs.Flush();
            }
        }
        #endregion
    }
}
