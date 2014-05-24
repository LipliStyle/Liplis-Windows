//=======================================================================
//  ClassName : ObjSkinSetting
//  概要      : スキン設定
//
//  NoralisEditor
//  2013/07/14 NoralisEditor2.2.2 テーマカラー設定追加
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Reflection;
using Liplis.Common;
using Liplis.Xml;

namespace Liplis.Msg
{
    [Serializable]
    public class ObjSkinSetting : XmlSettingObject
    {
        ///=============================
        ///プロパティ
        public string charName          { get; set; }   //キャラ名
        public string textFont          { get; set; }   //テキストフォント
        public string textColor         { get; set; }   //テキストカラー
        public string linkColor         { get; set; }   //リンクカラー
        public string titleColor        { get; set; }   //タイトルカラー  
        public string themaColor        { get; set; }   //テーマカラー    //ver2.2.2
        public string themaColorSub     { get; set; }   //テーマカラー    //ver2.2.2
        public string charIntroduction  { get; set; }   //キャラクター紹介
        public string version           { get; set; }   //バージョン
        public string toneUrl           { get; set; }   //トーンURL

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public ObjSkinSetting(string skinSettingPath)
        {
            try
            {
                //ログの宣言
                lc = new LpsLogControllerCus();

                //設定の取得
                setting = new SharedPreferences(skinSettingPath);

                //読み込み
                getPreferenceData();

            }
            catch (System.Exception err)
            {
                //設定ファイルの読み込みエラーの旨、異常位置を知らせるウインドウを出すべき
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, LpsPathControllerCus.getSettingFilePath() + "が存在しないため作成します" + Environment.NewLine + err);
            }
        }
        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public ObjSkinSetting(string charName, int width, int height, string textFont, string textColor, string linkColor, string titleColor, string themaColor, string themaColorSub)
        {
            try
            {
                //ログの宣言
                lc = new LpsLogControllerCus();

                //設定
                this.charName = charName;
                this.textFont = textFont;
                this.textColor = textColor;
                this.linkColor = linkColor;
                this.titleColor = titleColor;
                this.themaColor = themaColor;
                this.themaColorSub = themaColorSub;

            }
            catch (System.Exception err)
            {
                //設定ファイルの読み込みエラーの旨、異常位置を知らせるウインドウを出すべき
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, LpsPathControllerCus.getSettingFilePath() + "が存在しないため作成します" + Environment.NewLine + err);
            }
        }
        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public ObjSkinSetting()
        {
        }
        #endregion

        /// <summary>
        /// readResult
        /// readXmlで読み込んだ結果を自変数に読み込む
        /// </summary>
        #region getPreferenceData
        public override void getPreferenceData()
        {
            try
            {
                charName         = setting.getString(LiplisDefine.SKIN_CHAR_NAME, LiplisDefine.SKIN_DEF_CHAR_NAME);
                textFont         = setting.getString(LiplisDefine.SKIN_FONT, LiplisDefine.SKIN_DEF_FONT);
                textColor        = setting.getString(LiplisDefine.SKIN_TEXT_COLOR, LiplisDefine.SKIN_DEF_TEXT_COLOR);
                linkColor        = setting.getString(LiplisDefine.SKIN_LINK_COLOR, LiplisDefine.SKIN_DEF_LINK_COLOR);
                titleColor       = setting.getString(LiplisDefine.SKIN_TITLE_COLOR, LiplisDefine.SKIN_DEF_TITLE_COLOR);
                themaColor       = setting.getString(LiplisDefine.SKIN_THEMA_COLOR, LiplisDefine.SKIN_DEF_THEMA_COLOR);     //ver2.2.2
                themaColorSub      = setting.getString(LiplisDefine.SKIN_THEMA_COLOR2, LiplisDefine.SKIN_DEF_THEMA_COLOR2);   //ver2.2.2
                charIntroduction = setting.getString(LiplisDefine.SKIN_CHAR_INTRO, LiplisDefine.SKIN_DEF_CHAR_INTRO);
                version          = setting.getString(LiplisDefine.SKIN_VERSION, LiplisDefine.SKIN_DEF_VERSION);
                toneUrl          = setting.getString(LiplisDefine.SKIN_TONE_URL, LiplisDefine.SKIN_DEF_TONE_URL);
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }

        }
        #endregion

        /// <summary>
        /// setPreferenceData
        /// セーブ
        /// </summary>
        #region setPreferenceData
        public override void setPreferenceData()
        {

        }
        #endregion
    }
}
