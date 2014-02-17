//=======================================================================
//  ClassName : ObjSkinSetting
//  概要      : スキン設定
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Liplis.Common;
using System.Reflection;

namespace Liplis.Msg
{
    public class ObjSkinSettingList
    {
        ///=============================
        ///プロパティ
        public List<ObjSkinSetting> ossList { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ObjSkinSettingList
        public ObjSkinSettingList()
        {

        }
        #endregion

        /// <summary>
        /// loadAllSkinFile
        /// すべてのスキン設定ファイルを読み込む
        /// </summary>
        #region loadAllSkinFile
        public void loadAllSkinFile()
        {
            try
            {
                //ossListの初期化
                ossList = new List<ObjSkinSetting>();

                //スキンフォルダ内のフォルダ一覧を取得する
                string[] dirs = Directory.GetDirectories(LpsPathControllerCus.getSkinPath());

                //スキンファイルの存在確認とオブジェクトリストの生成
                foreach (string dir in dirs)
                {
                    string skinSettingpath = dir + "\\skin.xml";
                    //ファイルの存在チェック
                    if (LpsPathControllerCus.checkFileExist(skinSettingpath))
                    {
                        //ファイルが存在したら読み込み
                        ossList.Add(new ObjSkinSetting(skinSettingpath));
                    }
                }
            }
            catch (Exception err)
            {
                LpsLogControllerCus lc = new LpsLogControllerCus();
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }

        #endregion

        /// <summary>
        /// loadTargetSkin
        /// 対象のスキンファイルを取得する
        /// </summary>
        #region loadTargetSkin
        public ObjSkinSetting loadTargetSkin(string charName)
        {
            try
            {
                //ossの初期化
                ObjSkinSetting result = null;

                //すべてのスキンファイルの読み込み
                loadAllSkinFile();

                foreach (ObjSkinSetting oss in ossList)
                {
                    //検索
                    if (charName.Equals(oss.charName))
                    {
                        result = oss;
                        break;
                    }
                }

                //検索して、ossがnullのままであれば、見つからなかったので、リソース値をセット
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return loadResorceSkin();
                }
            }
            catch (Exception err)
            {
                LpsLogControllerCus lc = new LpsLogControllerCus();
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return loadResorceSkin();
            }
        }
        #endregion

        /// <summary>
        /// loadResorceSkin
        /// リソースのスキン設定を返す
        /// </summary>
        /// <returns>リソーススキンオブジェクト</returns>
        #region loadResorceSkin
        private ObjSkinSetting loadResorceSkin()
        {
            return new ObjSkinSetting("Lili", 300, 334, "ＭＳ ゴシック", "0,0,0", "0,0,0", "0,0,0", "255,228,96", "255,140,0");
        }
        #endregion



    }
}
