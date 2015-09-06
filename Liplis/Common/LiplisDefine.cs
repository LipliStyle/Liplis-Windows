//=======================================================================
//  ClassName : LiplisDefine
//  概要      : 各種定義
//
//  Liplis4.0
//  Copyright(c) 2010-2014 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Common
{
    public class LiplisDefine
    {
        ///=============================
        /// 設定ファイルパス定義
        #region 設定ファイルパス定義
        public const string SETTING_SKIN_FILE_NAME = "skin.xml";
        public const string SETTING_VERSION_FILE_NAME = "version.xml";
        public const string SETTING_BODY_XML_FILE = "body.xml";
        public const string SETTING_CHAT_XML_FILE = "chat.xml";
        public const string SETTING_TONE_XML_FILE = "tone.xml";
        public const string LIPLIS_NEW_EXE_FILE = "Liplis.lps";
        #endregion

        ///=============================
        /// 設定ファイル定義
        #region 設定ファイル定義
        public const string PREFS_UID                  = "uid";
        public const string PREFS_LOAD_SKIN            = "loadSkin";
        public const string PREFS_LOCATION_X           = "locationX";
        public const string PREFS_LOCATION_Y           = "locationY";
        public const string PREFS_MODE                 = "mode";
        public const string PREFS_SPEED                = "speed";
        public const string PREFS_TALK_CHASE           = "talkChase";
        public const string PREFS_ALWAYSTOP            = "alwaysTop";
        public const string PREFS_WINDOW               = "window";
        public const string PREFS_HELTH                = "helth";
        public const string PREFS_OUTRANGE_RECOVERY    = "outRangeRecovery";
        public const string PREFS_MOUSE_CTLS           = "mouseCtrl";
        public const string PREFS_DROP_ON              = "dropOn";
        public const string PREFS_NICO_ID              = "nicoId";
        public const string PREFS_NICO_PASS            = "nicoPass";
        public const string PREFS_DOWN_PATH            = "downPath";
        public const string PREFS_NOTICE               = "downNotice";
        public const string PREFS_DISC_WINDFOW_ON      = "discWindowOn";
        public const string PREFS_REGION_CD            = "regionCd";
        public const string PREFS_MENU_TYPE            = "menuType";
        public const string PREFS_MENU_TIMING          = "menuTiming";
        public const string PREFS_VERSION              = "version";
        public const string PREFS_TOPIC_NEWS           = "lpsTopicNews";
        public const string PREFS_TOPIC_2CH            = "lpsTopic2ch";
        public const string PREFS_TOPIC_NICO           = "lpsTopicNico";
        public const string PREFS_TOPIC_RSS            = "lpsTopicRss";
        public const string PREFS_TOPIC_TWITTER        = "lpsTopicTwitter";
        public const string PREFS_TOPIC_TWITTER_PB     = "lpsTopicTwitterPb";
        public const string PREFS_TOPIC_TWITTER_MY     = "lpsTopicTwitterMy";
        public const string PREFS_TOPIC_HOUR           = "lpsTopicHour";
        public const string PREFS_TOPIC_ALREADY        = "lpsAlready";
        public const string PREFS_TOPIC_TWITTER_MODE   = "lpsTwitterMode";
        public const string PREFS_TOPIC_RUNOUT         = "lpsRunout";

        public const string PREFS_VOICE_ON               = "lpsVoiceOn";
        public const string PREFS_VOICE_SELECT           = "lpsVoiceSelect";
        public const string PREFS_VOICE_VRPATH_SOFTALK   = "lpsVoiceVRPathSofTalk";
        public const string PREFS_VOICE_VRPATH_YUKARI    = "lpsVoiceVRPathYukari";
        public const string PREFS_VOICE_VRPATH_TOMOE     = "lpsVoiceVRPathTomoe";
        public const string PREFS_VOICE_VRPATH_ZUNKO     = "lpsVoiceVRPathZunko";
        public const string PREFS_VOICE_VRPATH_YUKARI_EX = "lpsVoiceVRPathYukariEx";    // 2015/09/02 Lipli4.5.3
        public const string PREFS_VOICE_VRPATH_TOMOE_EX  = "lpsVoiceVRPathTomoeEx";     // 2015/09/02 Lipli4.5.3
        public const string PREFS_VOICE_VRPATH_ZUNKO_EX  = "lpsVoiceVRPathZunkoEx";     // 2015/09/02 Lipli4.5.3
        public const string PREFS_VOICE_VRPATH_AKANE     = "lpsVoiceVRPathAKANE";       // 2015/09/02 Lipli4.5.5
        public const string PREFS_VOICE_VRPATH_AOI       = "lpsVoiceVRPathAOI";         // 2015/09/02 Lipli4.5.5

        public const string PREFS_TWITTER_ACTIVATE       = "lpsTwitterActivate";

        #endregion

        ///=============================
        /// ウインドウファイル定義
        #region ウインドウファイル定義
        public const string LPS_WINDOW_1       = "window.png";
        public const string LPS_WINDOW_2       = "window_blue.png";
        public const string LPS_WINDOW_3       = "window_green.png";
        public const string LPS_WINDOW_4       = "window_pink.png";
        public const string LPS_WINDOW_5       = "window_purple.png";
        public const string LPS_WINDOW_6       = "window_red.png";
        public const string LPS_WINDOW_7       = "window_yellow.png";
        public const string LPS_WINDOW_DEFAULT = "window_default.png";
        #endregion

        ///=============================
        /// 設定背景ファイル定義
        #region 設定背景ファイル定義

        #endregion

        ///=============================
        /// Liplis定義
        #region Liplis定義
        public const int LPS_ICON_DIF = 90;
        public const string MESSAGE_TITLE = "Liplis";
        #endregion

        ///=============================
        /// デフォルトニコニコ
        #region デフォルトニコニコダウンロード用アカウント
        public const string NICO_DEFAULT_ID   = "LipliStyle@gmail.com";
        public const string NICO_DEFAULT_PASS = "liplistyle";
        #endregion

        ///=============================
        /// Liplisツールズ
        #region ツールパス
        public const string LIPLIS_TOOL_BROOM = @"LiplisBroom.exe";
        #endregion

        ///=============================
        /// URL定義
        #region URL定義
        public const string LIPLIS_API_SUMMARY_NEWS               = @"http://liplis.mine.nu/Clalis/v41/Liplis/ClalisForLiplis.aspx";                        //2014/04/07 ver4.0.0 Clalis4.0採用
        public const string LIPLIS_API_SUMMARY_NEWS_LIST          = @"http://liplis.mine.nu/Clalis/v41/Liplis/ClalisForLiplisFx.aspx";                      //2014/04/07 ver4.0.0 Clalis4.0採用                  //2014/04/07 ver4.0.0 Clalis4.0採用
        public const string LIPLIS_API_SHORT_NEWS                 = @"http://liplis.mine.nu/Clalis/v41/Liplis/ClalisForLiplisWeb.aspx";                     //2014/04/07 ver4.0.0 Clalis4.0採用         
        public const string LIPLIS_API_SHORT_NEWS_LIST            = @"http://liplis.mine.nu/Clalis/v41/Liplis/ClalisForLiplisWebFx.aspx";
        public const string LIPLIS_API_REGISTER_RSS               = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisRegisterAddRssUrl.aspx";
        public const string LIPLIS_API_TOPIC_SETTING              = @"http://liplis.mine.nu/Clalis/v32/Liplis/ClalisForLiplisSettingTopicSetting.aspx";
        public const string LIPLIS_API_DELETE_RSS                 = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisRegisterDelRssUrl.aspx";
        public const string LIPLIS_API_GET_RSSLIST                = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisSettingRssUrlListEachCat.aspx";
        public const string LIPLIS_API_REGISTER_TWITTER           = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisRegisterAddTwitterUserEx.aspx";
        public const string LIPLIS_API_DELETE_TWITTER             = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisRegisterDelTwitterUserEx.aspx";
        public const string LIPLIS_API_GET_TWITTERLIST            = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisSettingTwitterUserList.aspx";
        public const string LIPLIS_API_GET_SEARCH_WORD            = @"http://liplis.mine.nu/Clalis/v32/Liplis/ClalisForLiplisSettingGetSearchWord.aspx";
        public const string LIPLIS_API_REGISTER_SEARCH_WORD       = @"http://liplis.mine.nu/Clalis/v32/Liplis/ClalisForLiplisSettingAddSearchWord.aspx";
        public const string LIPLIS_API_DELETE_SEARCH_WORD         = @"http://liplis.mine.nu/Clalis/v32/Liplis/ClalisForLiplisSettingDelSearchWord.aspx";
        public const string LIPLIS_API_REGISTER_TWITTER_USER_INFO = @"http://liplis.mine.nu/Clalis/v40/Liplis/ClalisForLiplisRegisterTwitterInfo.aspx";

        public const string LIPLIS_API_GET_ONETIME_PASS           = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisGetOnetimePass.aspx";
        public const string LIPLIS_API_GET_USERID                 = @"http://liplis.mine.nu/Clalis/v31/Liplis/ClalisForLiplisGetUserId.aspx";

        public const string LIPLIS_NEW_EXE                        = @"http://liplis.mine.nu/LiplisWinUpd/1_X/Liplis.lps";
        public const string LIPLIS_NEW_XML                        = @"http://liplis.mine.nu/LiplisWinUpd/1_X/version.xml";

        public const string LIPLIS_HELP                           = @"http://liplis.mine.nu/lipliswiki/webroot/?LiplisWindows%20Manual";
        public const string LIPLIS_LIPLISTYLE                     = @"http://liplis.mine.nu/";

        public const string LIPLIS_FREE_TALK                      = @"http://liplis.mine.nu/Clalis/v32/Liplis/ClalisForLiplisFreeTalk.aspx";

        public const string LIPLIS_TWEET                          = @"http://liplis.mine.nu/Clalis/v40/Liplis/ClalisForLiplisTweet.aspx";
        public const string LIPLIS_CHAT                           = @"http://liplis.mine.nu/Clalis/v40/Liplis/ClalisForLiplisTalk.aspx";

        #endregion

        ///=============================
        /// ツイッター認証コード
        ///=============================
        #region ツイッター認証コード
        public const string TWITTER_OAUTH_CONSUMERKEY = "W1tQBXDr3pQu1atfIwp6A";
        public const string TWITTER_OAUTH_CONSUMERSECRET = "eTFat5surbln3MH7f0uIlwmpOcQdjlkyg7vUk90eG8";

        public static readonly string REQUEST_TOKEN_URL = "https://api.twitter.com/oauth/request_token";
        public static readonly string AUTHORIZE_URL = "https://api.twitter.com/oauth/authorize";
        public static readonly string ACCESS_TOKEN_URL = "https://api.twitter.com/oauth/access_token";
        #endregion

        ///=============================
        /// ClalisAPI定義
        #region ClalisAPI定義
        public const string CLALIS_V20_SNEWS= "/p:string";
        public const string CLALIS_V20_SUMNEWS_TITLE = "/p:msgSumNws/p:title";
        public const string CLALIS_V20_SUMNEWS_URL = "/p:msgSumNws/p:url";
        public const string CLALIS_V20_SUMNEWS_JPG_URL = "/p:msgSumNws/p:jpgUrl";
        public const string CLALIS_V20_SUMNEWS_RESULT = "/p:msgSumNws/p:result";
        #endregion

        ///=============================
        /// リソース
        #region リソース
        public const string TRANSE = "transe";
        public const string TRANSE30 = "transe30";
        public const string RES_WINDOW = "window";
        public const string RES_SETTING = "setting";
        #endregion

        ///=============================
        /// skinファイル定義
        #region skin設定ファイル定義
        public const string SKIN_CHAR_NAME   = "charName";
        public const string SKIN_WIDTH       = "width";
        public const string SKIN_HEIGHT      = "height";
        public const string SKIN_FONT        = "textFont";
        public const string SKIN_TEXT_COLOR  = "textColor";
        public const string SKIN_LINK_COLOR  = "linkColor";
        public const string SKIN_TITLE_COLOR = "titleColor";
        public const string SKIN_THEMA_COLOR = "themaColor";
        public const string SKIN_THEMA_COLOR2 = "themaColor2";
        public const string SKIN_CHAR_INTRO  = "charIntroduction";
        public const string SKIN_VERSION     = "version";
        public const string SKIN_TONE_URL    = "tone";
        #endregion

        ///=============================
        /// skinAndファイル定義
        #region skin設定ファイル定義
        public const string SKIN_PACKAGE = "package";
        public const string SKIN_APP_NAME = "appName";
        public const string SKIN_APK_NAME = "apkName";
        #endregion

        ///=============================
        /// skin設定デフォルト定義
        #region skin設定デフォルト定義
        public const string SKIN_DEF_CHAR_NAME      = "Lili";
        public const int SKIN_DEF_WIDTH             = 300;
        public const int SKIN_DEF_HEIGHT            = 334;
        public const string SKIN_DEF_FONT           = "ＭＳ ゴシック";
        public const string SKIN_DEF_TEXT_COLOR     = "0,0,0";
        public const string SKIN_DEF_LINK_COLOR     = "0,0,0";
        public const string SKIN_DEF_TITLE_COLOR    = "0,0,0";
        public const string SKIN_DEF_THEMA_COLOR    = "255,228,96";   //ver2.2.2
        public const string SKIN_DEF_THEMA_COLOR2   = "255.140,00";   //ver2.2.2
        public const string SKIN_DEF_CHAR_INTRO     = "紹介文の読み込みに失敗しました。";
        public const string SKIN_DEF_VERSION        = "1.0.0";
        public const string SKIN_DEF_TONE_URL       = "http://liplis.mine.nu/xml/Tone/LiplisLili.xml";
        #endregion

        ///=============================
        /// ボディ画像定義
        #region ボディ画像xpath定義
        public const string BODY_HEIGHT     = "/define/height";
        public const string BODY_WIDHT      = "/define/width";
        public const string BODY_LOCATION_X = "/define/locationX";
        public const string BODY_LOCATION_Y = "/define/locationY";

        public const string BODY_NORMAL_11    = "/define/normal/normal11";
        public const string BODY_NORMAL_12    = "/define/normal/normal12";
        public const string BODY_NORMAL_21    = "/define/normal/normal21";
        public const string BODY_NORMAL_22    = "/define/normal/normal22";
        public const string BODY_NORMAL_31    = "/define/normal/normal31";
        public const string BODY_NORMAL_32    = "/define/normal/normal32";
        public const string BODY_NORMAL_TOUCH = "/define/normal/touch";

        public const string BODY_JOY_P_11    = "/define/joy_p/joy_p11";
        public const string BODY_JOY_P_12    = "/define/joy_p/joy_p12";
        public const string BODY_JOY_P_21    = "/define/joy_p/joy_p21";
        public const string BODY_JOY_P_22    = "/define/joy_p/joy_p22";
        public const string BODY_JOY_P_31    = "/define/joy_p/joy_p31";
        public const string BODY_JOY_P_32    = "/define/joy_p/joy_p32";
        public const string BODY_JOY_P_TOUCH = "/define/joy_p/touch";

        public const string BODY_JOY_M_11    = "/define/joy_m/joy_m11";
        public const string BODY_JOY_M_12    = "/define/joy_m/joy_m12";
        public const string BODY_JOY_M_21    = "/define/joy_m/joy_m21";
        public const string BODY_JOY_M_22    = "/define/joy_m/joy_m22";
        public const string BODY_JOY_M_31    = "/define/joy_m/joy_m31";
        public const string BODY_JOY_M_32    = "/define/joy_m/joy_m32";
        public const string BODY_JOY_M_TOUCH = "/define/joy_m/touch";

        public const string BODY_ADMIRATION_P_11    = "/define/admiration_p/admiration_p11";
        public const string BODY_ADMIRATION_P_12    = "/define/admiration_p/admiration_p12";
        public const string BODY_ADMIRATION_P_21    = "/define/admiration_p/admiration_p21";
        public const string BODY_ADMIRATION_P_22    = "/define/admiration_p/admiration_p22";
        public const string BODY_ADMIRATION_P_31    = "/define/admiration_p/admiration_p31";
        public const string BODY_ADMIRATION_P_32    = "/define/admiration_p/admiration_p32";
        public const string BODY_ADMIRATION_P_TOUCH = "/define/admiration_p/touch";

        public const string BODY_ADMIRATION_M_11    = "/define/admiration_m/admiration_m11";
        public const string BODY_ADMIRATION_M_12    = "/define/admiration_m/admiration_m12";
        public const string BODY_ADMIRATION_M_21    = "/define/admiration_m/admiration_m21";
        public const string BODY_ADMIRATION_M_22    = "/define/admiration_m/admiration_m22";
        public const string BODY_ADMIRATION_M_31    = "/define/admiration_m/admiration_m31";
        public const string BODY_ADMIRATION_M_32    = "/define/admiration_m/admiration_m32";
        public const string BODY_ADMIRATION_M_TOUCH = "/define/admiration_m/touch";

        public const string BODY_PEACE_P_11    = "/define/peace_p/peace_p11";
        public const string BODY_PEACE_P_12    = "/define/peace_p/peace_p12";
        public const string BODY_PEACE_P_21    = "/define/peace_p/peace_p21";
        public const string BODY_PEACE_P_22    = "/define/peace_p/peace_p22";
        public const string BODY_PEACE_P_31    = "/define/peace_p/peace_p31";
        public const string BODY_PEACE_P_32    = "/define/peace_p/peace_p32";
        public const string BODY_PEACE_P_TOUCH = "/define/peace_p/touch";

        public const string BODY_PEACE_M_11    = "/define/peace_m/peace_m11";
        public const string BODY_PEACE_M_12    = "/define/peace_m/peace_m12";
        public const string BODY_PEACE_M_21    = "/define/peace_m/peace_m21";
        public const string BODY_PEACE_M_22    = "/define/peace_m/peace_m22";
        public const string BODY_PEACE_M_31    = "/define/peace_m/peace_m31";
        public const string BODY_PEACE_M_32    = "/define/peace_m/peace_m32";
        public const string BODY_PEACE_M_TOUCH = "/define/peace_m/touch";

        public const string BODY_ECSTASY_P_11    = "/define/ecstasy_p/ecstasy_p11";
        public const string BODY_ECSTASY_P_12    = "/define/ecstasy_p/ecstasy_p12";
        public const string BODY_ECSTASY_P_21    = "/define/ecstasy_p/ecstasy_p21";
        public const string BODY_ECSTASY_P_22    = "/define/ecstasy_p/ecstasy_p22";
        public const string BODY_ECSTASY_P_31    = "/define/ecstasy_p/ecstasy_p31";
        public const string BODY_ECSTASY_P_32    = "/define/ecstasy_p/ecstasy_p32";
        public const string BODY_ECSTASY_P_TOUCH = "/define/ecstasy_p/touch";

        public const string BODY_ECSTASY_M_11    = "/define/ecstasy_m/ecstasy_m11";
        public const string BODY_ECSTASY_M_12    = "/define/ecstasy_m/ecstasy_m12";
        public const string BODY_ECSTASY_M_21    = "/define/ecstasy_m/ecstasy_m21";
        public const string BODY_ECSTASY_M_22    = "/define/ecstasy_m/ecstasy_m22";
        public const string BODY_ECSTASY_M_31    = "/define/ecstasy_m/ecstasy_m31";
        public const string BODY_ECSTASY_M_32    = "/define/ecstasy_m/ecstasy_m32";
        public const string BODY_ECSTASY_M_TOUCH = "/define/ecstasy_m/touch";

        public const string BODY_AMAZEMENT_P_11    = "/define/amazement_p/amazement_p11";
        public const string BODY_AMAZEMENT_P_12    = "/define/amazement_p/amazement_p12";
        public const string BODY_AMAZEMENT_P_21    = "/define/amazement_p/amazement_p21";
        public const string BODY_AMAZEMENT_P_22    = "/define/amazement_p/amazement_p22";
        public const string BODY_AMAZEMENT_P_31    = "/define/amazement_p/amazement_p31";
        public const string BODY_AMAZEMENT_P_32    = "/define/amazement_p/amazement_p32";
        public const string BODY_AMAZEMENT_P_TOUCH = "/define/amazement_p/touch";

        public const string BODY_AMAZEMENT_M_11    = "/define/amazement_m/amazement_m11";
        public const string BODY_AMAZEMENT_M_12    = "/define/amazement_m/amazement_m12";
        public const string BODY_AMAZEMENT_M_21    = "/define/amazement_m/amazement_m21";
        public const string BODY_AMAZEMENT_M_22    = "/define/amazement_m/amazement_m22";
        public const string BODY_AMAZEMENT_M_31    = "/define/amazement_m/amazement_m31";
        public const string BODY_AMAZEMENT_M_32    = "/define/amazement_m/amazement_m32";
        public const string BODY_AMAZEMENT_M_TOUCH = "/define/amazement_m/touch";

        public const string BODY_RAGE_P_11    = "/define/rage_p/rage_p11";
        public const string BODY_RAGE_P_12    = "/define/rage_p/rage_p12";
        public const string BODY_RAGE_P_21    = "/define/rage_p/rage_p21";
        public const string BODY_RAGE_P_22    = "/define/rage_p/rage_p22";
        public const string BODY_RAGE_P_31    = "/define/rage_p/rage_p31";
        public const string BODY_RAGE_P_32    = "/define/rage_p/rage_p32";
        public const string BODY_RAGE_P_TOUCH = "/define/rage_p/touch";

        public const string BODY_RAGE_M_11    = "/define/rage_m/rage_m11";
        public const string BODY_RAGE_M_12    = "/define/rage_m/rage_m12";
        public const string BODY_RAGE_M_21    = "/define/rage_m/rage_m21";
        public const string BODY_RAGE_M_22    = "/define/rage_m/rage_m22";
        public const string BODY_RAGE_M_31    = "/define/rage_m/rage_m31";
        public const string BODY_RAGE_M_32    = "/define/rage_m/rage_m32";
        public const string BODY_RAGE_M_TOUCH = "/define/rage_m/touch";

        public const string BODY_INTEREST_P_11    = "/define/interest_p/interest_p11";
        public const string BODY_INTEREST_P_12    = "/define/interest_p/interest_p12";
        public const string BODY_INTEREST_P_21    = "/define/interest_p/interest_p21";
        public const string BODY_INTEREST_P_22    = "/define/interest_p/interest_p22";
        public const string BODY_INTEREST_P_31    = "/define/interest_p/interest_p31";
        public const string BODY_INTEREST_P_32    = "/define/interest_p/interest_p32";
        public const string BODY_INTEREST_P_TOUCH = "/define/interest_p/touch";

        public const string BODY_INTEREST_M_11    = "/define/interest_m/interest_m11";
        public const string BODY_INTEREST_M_12    = "/define/interest_m/interest_m12";
        public const string BODY_INTEREST_M_21    = "/define/interest_m/interest_m21";
        public const string BODY_INTEREST_M_22    = "/define/interest_m/interest_m22";
        public const string BODY_INTEREST_M_31    = "/define/interest_m/interest_m31";
        public const string BODY_INTEREST_M_32    = "/define/interest_m/interest_m32";
        public const string BODY_INTEREST_M_TOUCH = "/define/interest_m/touch";

        public const string BODY_RESPECT_P_11    = "/define/respect_p/respect_p11";
        public const string BODY_RESPECT_P_12    = "/define/respect_p/respect_p12";
        public const string BODY_RESPECT_P_21    = "/define/respect_p/respect_p21";
        public const string BODY_RESPECT_P_22    = "/define/respect_p/respect_p22";
        public const string BODY_RESPECT_P_31    = "/define/respect_p/respect_p31";
        public const string BODY_RESPECT_P_32    = "/define/respect_p/respect_p32";
        public const string BODY_RESPECT_P_TOUCH = "/define/respect_p/touch";

        public const string BODY_RESPECT_M_11    = "/define/respect_m/respect_m11";
        public const string BODY_RESPECT_M_12    = "/define/respect_m/respect_m12";
        public const string BODY_RESPECT_M_21    = "/define/respect_m/respect_m21";
        public const string BODY_RESPECT_M_22    = "/define/respect_m/respect_m22";
        public const string BODY_RESPECT_M_31    = "/define/respect_m/respect_m31";
        public const string BODY_RESPECT_M_32    = "/define/respect_m/respect_m32";
        public const string BODY_RESPECT_M_TOUCH = "/define/respect_m/touch";

        public const string BODY_CLAMLY_P_11    = "/define/calmly_p/calmly_p11";
        public const string BODY_CLAMLY_P_12    = "/define/calmly_p/calmly_p12";
        public const string BODY_CLAMLY_P_21    = "/define/calmly_p/calmly_p21";
        public const string BODY_CLAMLY_P_22    = "/define/calmly_p/calmly_p22";
        public const string BODY_CLAMLY_P_31    = "/define/calmly_p/calmly_p31";
        public const string BODY_CLAMLY_P_32    = "/define/calmly_p/calmly_p32";
        public const string BODY_CLAMLY_P_TOUCH = "/define/calmly_p/touch";

        public const string BODY_CLAMLY_M_11    = "/define/calmly_m/calmly_m11";
        public const string BODY_CLAMLY_M_12    = "/define/calmly_m/calmly_m12";
        public const string BODY_CLAMLY_M_21    = "/define/calmly_m/calmly_m21";
        public const string BODY_CLAMLY_M_22    = "/define/calmly_m/calmly_m22";
        public const string BODY_CLAMLY_M_31    = "/define/calmly_m/calmly_m31";
        public const string BODY_CLAMLY_M_32    = "/define/calmly_m/calmly_m32";
        public const string BODY_CLAMLY_M_TOUCH = "/define/calmly_m/touch";

        public const string BODY_PROUD_P_11    = "/define/proud_p/proud_p11";
        public const string BODY_PROUD_P_12    = "/define/proud_p/proud_p12";
        public const string BODY_PROUD_P_21    = "/define/proud_p/proud_p21";
        public const string BODY_PROUD_P_22    = "/define/proud_p/proud_p22";
        public const string BODY_PROUD_P_31    = "/define/proud_p/proud_p31";
        public const string BODY_PROUD_P_32    = "/define/proud_p/proud_p32";
        public const string BODY_PROUD_P_TOUCH = "/define/proud_p/touch";

        public const string BODY_PROUD_M_11    = "/define/proud_m/proud_m11";
        public const string BODY_PROUD_M_12    = "/define/proud_m/proud_m12";
        public const string BODY_PROUD_M_21    = "/define/proud_m/proud_m21";
        public const string BODY_PROUD_M_22    = "/define/proud_m/proud_m22";
        public const string BODY_PROUD_M_31    = "/define/proud_m/proud_m31";
        public const string BODY_PROUD_M_32    = "/define/proud_m/proud_m32";
        public const string BODY_PROUD_M_TOUCH = "/define/proud_m/touch";

        public const string BODY_SITDOWN_11 = "/define/sleep/sleep_11";
        public const string BODY_SITDOWN_12 = "/define/sleep/sleep_12";
        public const string BODY_SITDOWN_21 = "/define/sleep/sleep_21";
        public const string BODY_SITDOWN_22 = "/define/sleep/sleep_22";
        public const string BODY_SITDOWN_31 = "/define/sleep/sleep_31";
        public const string BODY_SITDOWN_32 = "/define/sleep/sleep_32";

        public const string BODY_BATTERY_HI_11    = "/define/batteryHi/batteryHi_11";
        public const string BODY_BATTERY_HI_12    = "/define/batteryHi/batteryHi_12";
        public const string BODY_BATTERY_HI_21    = "/define/batteryHi/batteryHi_21";
        public const string BODY_BATTERY_HI_22    = "/define/batteryHi/batteryHi_22";
        public const string BODY_BATTERY_HI_31    = "/define/batteryHi/batteryHi_31";
        public const string BODY_BATTERY_HI_32    = "/define/batteryHi/batteryHi_32";
        public const string BODY_BATTERY_HI_TOUCH = "/define/batteryHi/touch";

        public const string BODY_BATTERY_MID_11    = "/define/batteryMid/batteryMid_11";
        public const string BODY_BATTERY_MID_12    = "/define/batteryMid/batteryMid_12";
        public const string BODY_BATTERY_MID_21    = "/define/batteryMid/batteryMid_21";
        public const string BODY_BATTERY_MID_22    = "/define/batteryMid/batteryMid_22";
        public const string BODY_BATTERY_MID_31    = "/define/batteryMid/batteryMid_31";
        public const string BODY_BATTERY_MID_32    = "/define/batteryMid/batteryMid_32";
        public const string BODY_BATTERY_MID_TOUCH = "/define/batteryMid/touch";

        public const string BODY_BATTERY_LOW_11    = "/define/batteryLow/batteryLow_11";
        public const string BODY_BATTERY_LOW_12    = "/define/batteryLow/batteryLow_12";
        public const string BODY_BATTERY_LOW_21    = "/define/batteryLow/batteryLow_21";
        public const string BODY_BATTERY_LOW_22    = "/define/batteryLow/batteryLow_22";
        public const string BODY_BATTERY_LOW_31    = "/define/batteryLow/batteryLow_31";
        public const string BODY_BATTERY_LOW_32    = "/define/batteryLow/batteryLow_32";
        public const string BODY_BATTERY_LOW_TOUCH = "/define/batteryLow/touch";

        #endregion

        ///=============================
        /// Emotion画像定義
        #region Emotion定義
        public const string EMOTION_NORMAL       = "normal";
        public const string EMOTION_JOY_P        = "joy_p";
        public const string EMOTION_JOY_M        = "joy_m";
        public const string EMOTION_ADMIRATION_P = "admiration_p";
        public const string EMOTION_ADMIRATION_M = "admiration_m";
        public const string EMOTION_PEACE_P      = "peace_p";
        public const string EMOTION_PEACE_M      = "peace_m";
        public const string EMOTION_ECSTASY_P    = "ecstasy_p";
        public const string EMOTION_ECSTASY_M    = "ecstasy_m";
        public const string EMOTION_AMAZEMENT_P  = "amazement_p";
        public const string EMOTION_AMAZEMENT_M  = "amazement_m";
        public const string EMOTION_RAGE_P       = "rage_p";
        public const string EMOTION_RAGE_M       = "rage_m";
        public const string EMOTION_INTEREST_P   = "interest_p";
        public const string EMOTION_INTEREST_M   = "interest_m";
        public const string EMOTION_RESPECT_P    = "respect_p";
        public const string EMOTION_RESPECT_M    = "respect_m";
        public const string EMOTION_CLAMLY_P     = "calmly_p";
        public const string EMOTION_CLAMLY_M     = "calmly_m";
        public const string EMOTION_PROUD_P      = "proud_p";
        public const string EMOTION_PROUD_M      = "proud_m";
        public const string EMOTION_BATTERY_HI   = "battery_hi";
        public const string EMOTION_BATTERY_MID  = "battery_mid";
        public const string EMOTION_BATTERY_LOW  = "battery_low";        
        #endregion

        ///=============================
        /// chatXpath定義
        #region chatXpath定義
        public const string CHAT_NAME = "/chat/chatDiscription/name";
        public const string CHAT_TYPE = "/chat/chatDiscription/type";
        public const string CHAT_DISCRIPTION = "/chat/chatDiscription/discription";
        public const string CHAT_EMOTION = "/chat/chatDiscription/emotion";
        public const string CHAT_PREREWUISITE = "/chat/chatDiscription/prerequisite";
        #endregion

        ///=============================
        /// versionXpath定義
        #region versionXpath定義
        public const string VERSION_SKIN = "/version/skin";
        public const string VERSION_MIN = "/version/min";
        public const string VERSION_URL = "/version/url";
        public const string VERSION_APKURL = "/version/apkUrl";
        public const string VERSION_SKIN_URL = "/version/skinUrl";
        #endregion

        ///=============================
        /// touchXpath定義
        #region touchXpath定義
        public const string TOUCH_NAME = "/touch/touchDiscription/name";
        public const string TOUCH_TYPE = "/touch/touchDiscription/type";
        public const string TOUCH_SENS = "/touch/touchDiscription/sens";
        public const string TOUCH_TOP = "/touch/touchDiscription/top";
        public const string TOUCH_LEFT = "/touch/touchDiscription/left";
        public const string TOUCH_BOTTOM = "/touch/touchDiscription/bottom";
        public const string TOUCH_RIGHT = "/touch/touchDiscription/right";
        public const string TOUCH_CHAT = "/touch/touchDiscription/chat";
        #endregion

        ///=============================
        /// chatリソース定義
        #region chatリソース定義
        public const string CHAT_RESOURCE = "chat";
        #endregion

        ///=============================
        /// toneXpath定義
        #region toneXpath定義
        public const string TONE_NAME = "/tone/toneDiscription/name";
        public const string TONE_TYPE = "/tone/toneDiscription/type";
        public const string TONE_BEFOR = "/tone/toneDiscription/befor";
        public const string TONE_AFTER = "/tone/toneDiscription/after";
        #endregion

        ///=============================
        /// toneリソース定義
        #region toneリソース定義
        public const string TONE_RESOURCE = "tone";
        #endregion

        ///=============================
        /// ウインドウ画像定義
        #region ウインドウ画像定義
        public const string WIN_BD          = "bd.png";
        public const string WIN_BTN         ="btn.png";
        public const string WIN_BTNPUSH     ="btnPush.png";
        public const string WIN_BTNSELECT   ="btnSelect.png";
        public const string WIN_BTNSELECTED ="btnSelected.png";
        public const string WIN_CHAR        ="char.png";
        public const string WIN_LB          ="lb.png";
        public const string WIN_LM          ="lm.png";
        public const string WIN_LT          ="lt.png";
        public const string WIN_MB          ="mb.png";
        public const string WIN_MT          ="mt.png";
        public const string WIN_RB          ="rb.png";
        public const string WIN_RM          ="rm.png";
        public const string WIN_RT          ="rt.png";
        public const string WIN_SPLUSH      ="splush.png";
        public const string WIN_TALKWIN = "window.png";
        public const string WIN_SETTINGWIN  ="settingWin.png";
        #endregion

        ///=============================
        /// アイコン画像定義
        #region アイコン画像定義
        public const string ICO_NEXT    = "ico_next.png";
        public const string ICO_SLEEP   = "ico_sleep.png";  //ver3.0.4以前と互換性を持たせるために残す
        public const string ICO_ZZZ     = "ico_zzz.png";    //ver3.0.5 ICO_SLEEPからリネーム
        public const string ICO_WAIKUP  = "ico_waikup.png";
        public const string ICO_SETTING = "ico_setting.png";
        public const string ICO_LOG     = "ico_log.png";
        public const string ICO_POW     = "ico_pow.png";
        public const string ICO_RSS     = "ico_rss.png";
        public const string ICO_CHAR    = "ico_char.png";
        public const string ICO_TRAY    = "ico_tray.png";
        public const string ICO_BRW     = "ico_brw.png";
        public const string ICO_WID     = "ico_wid.png";
        public const string ICO_DOWN    = "ico_down.png";
        public const string BG_SETTING  = "setting.png";

        public const string ICO_NON = "battery_non.png";
        public const string ICO_BATTERY_0 = "battery_0.png";
        public const string ICO_BATTERY_12 = "battery_12.png";
        public const string ICO_BATTERY_25 = "battery_25.png";
        public const string ICO_BATTERY_37 = "battery_37.png";
        public const string ICO_BATTERY_50 = "battery_50.png";
        public const string ICO_BATTERY_62 = "battery_62.png";
        public const string ICO_BATTERY_75 = "battery_75.png";
        public const string ICO_BATTERY_87 = "battery_87.png";
        public const string ICO_BATTERY_100 = "battery_100.png";


        #endregion

        ///=============================
        /// ウインドウリソース定義
        #region ウインドウリソース定義
        public const string WIN_DEF_BD          = "bd";
        public const string WIN_DEF_BTN         = "btn";
        public const string WIN_DEF_BTNPUSH     = "btnPush";
        public const string WIN_DEF_BTNSELECT   = "btnSelect";
        public const string WIN_DEF_BTNSELECTED = "btnSelected";
        public const string WIN_DEF_CHAR        = "char";
        public const string WIN_DEF_LB          = "lb";
        public const string WIN_DEF_LM          = "lm";
        public const string WIN_DEF_LT          = "lt";
        public const string WIN_DEF_MB          = "mb";
        public const string WIN_DEF_MT          = "mt";
        public const string WIN_DEF_RB          = "rb";
        public const string WIN_DEF_RM          = "rm";
        public const string WIN_DEF_RT          = "rt";
        public const string WIN_DEF_SPLUSH      = "splush";
        public const string WIN_DEF_SETTINGWIN  = "settingWin";
        #endregion

        ///=============================
        /// アイコンリソース定義
        #region アイコンリソース定義
        public const string ICO_DEF_NEXT    = "ico_next";
        public const string ICO_DEF_SLEEP   = "ico_sleep";
        public const string ICO_DEF_ZZZ     = "ico_zzz";
        public const string ICO_DEF_WAIKUP  = "ico_waikup";
        public const string ICO_DEF_SETTING = "ico_setting";
        public const string ICO_DEF_LOG     = "ico_log";
        public const string ICO_DEF_POW     = "ico_pow";
        public const string ICO_DEF_RSS     = "ico_rss";
        public const string ICO_DEF_CHAR    = "ico_char";
        public const string ICO_DEF_TRAY    = "ico_tray";
        public const string ICO_DEF_BRW     = "ico_brw";
        public const string ICO_DEF_WID     = "ico_wid";
        public const string ICO_DEF_DOWN    = "ico_down";
        public const string BG_DEF_SETTING  = "setting";

        public const string ICO_DEF_NON         = "battery_non";
        public const string ICO_DEF_BATTERY_0   = "battery_0";
        public const string ICO_DEF_BATTERY_12  = "battery_12";
        public const string ICO_DEF_BATTERY_25  = "battery_25";
        public const string ICO_DEF_BATTERY_37  = "battery_37";
        public const string ICO_DEF_BATTERY_50  = "battery_50";
        public const string ICO_DEF_BATTERY_62  = "battery_62";
        public const string ICO_DEF_BATTERY_75  = "battery_75";
        public const string ICO_DEF_BATTERY_87  = "battery_87";
        public const string ICO_DEF_BATTERY_100 = "battery_100";

        public const string ICO_WID_TES = "widTest";
        public const string ICO_WID_SYS = "widSys";
        public const string ICO_WID_BRW = "widBrw";
        public const string ICO_WID_WET = "widWet";
        public const string ICO_WID_RSS = "widRss";
        public const string ICO_WID_CPU = "widCpu";
        public const string ICO_WID_MEM = "widMem";
        public const string ICO_WID_HDD = "widHdd";
        public const string ICO_WID_NET = "widNet";
        public const string ICO_EXT_FLV = "dlDoga";
        public const string ICO_EXT_MP3 = "dlMp3";

        public const string ICO_BTN_LNK = "btnLink";
        #endregion

        ///=============================
        /// picRdoFrq定義
        #region picRdoFrq定義
        public const string FRQ_CHANGEABLE = "picRdoFrqChangeable";
        public const string FRQ_KEEPS      = "picRdoFrqKeeps";
        public const string FRQ_MACHEN     = "picRdoFrqMachen";
        public const string FRQ_NOISY      = "picRdoFrqNoisy";
        public const string FRQ_NORMAL     = "picRdoFrqNormal";
        public const string FRQ_QUIET      = "picRdoFrqQuiet";
        public const string FRQ_REFINED    = "picRdoFrqRefined";
        public const string FRQ_RETIVENT   = "picRdoFrqReticent";
        public const string FRQ_TALKACTIV  = "picRdoFrqTalkative";
        public const string FRQ_VERRYNOISY = "picRdoFrqVerryNoisy";
        #endregion

        ///=============================
        /// ブロック名定義
        #region ブロック名定義
        public const string BLOCK_LT = "lt";
        public const string BLOCK_MT = "mt";
        public const string BLOCK_RT = "rt";
        public const string BLOCK_LB = "lb";
        public const string BLOCK_MB = "mb";
        public const string BLOCK_RB = "rb";
        public const string BLOCK_BD = "bd";
        public const string BLOCK_LM = "lm";
        public const string BLOCK_RM = "rm";
        #endregion

        ///=============================
        /// ブロックサイズ
        #region ブロックサイズ
        public const int BLOCK_SIZE = 10;
        public const int MIN_BLOCK_NUM = 4;
        #endregion

        ///=============================
        /// ウインドウモード定義
        #region ウインドウモード定義
        public const int WIN_MODE_TEXT_ONLY = 0;
        public const int WIN_MODE_WITH_URL  = 1;
        #endregion

        ///=============================
        /// Rcm設定定義
        #region Rcm設定定義
        public const string RCM_TITLE = "/rcm/menu/title";
        public const string RCM_TYPE = "/rcm/menu/type";
        public const string RCM_DATA = "/rcm/menu/data";

        #endregion

        ///=============================
        /// チャット制御コード
        #region 制御コード
        public const string CHAT_DEF_GREET   = "greet";
        public const string CHAT_DEF_GOODBYE = "goodBye";
        public const string CHAT_DEF_CHANGE  = "change";
        public const string CHAT_DEF_BATTERY_INFOT = "batteryInfo";

        public const string setting_Working             = "setting_Working";
        public const string setting_NotFound            = "setting_NotFound";
        public const string setting_AlreadyRegist       = "setting_AlreadyRegist";
        public const string setting_AlreadyDelete       = "setting_AlreadyDelete";
        public const string setting_RssNotValid         = "setting_RssNotValid";
        public const string setting_RssValid            = "setting_RssValid";
        public const string setting_Success             = "setting_Success";
        public const string setting_Rss_Reg_Success     = "setting_Rss_Reg_Success";
        public const string setting_Rss_Upd_Success     = "setting_Rss_Upd_Success";
        public const string setting_Faild               = "setting_Faild";
        public const string setting_Delete              = "setting_Delete";
        public const string setting_DeleteConfirm       = "setting_DeleteConfirm";
        public const string setting_DeleteSuccess       = "setting_DeleteSuccess";
        public const string setting_DeleteFaild         = "setting_DeleteFaild";
        public const string setting_Cancel              = "setting_Cancel";
        public const string setting_UrlEmpty            = "setting_UrlEmpty";
        public const string setting_NotFoundRssDelete   = "setting_NotFoundRssDelete";
        public const string setting_NotFoundNotVaildRss = "setting_NotFoundNotVaildRss";
        public const string setting_FoundNotVaildRss    = "setting_FoundNotVaildRss";
        public const string setting_NFRD_Success        = "setting_NFRD_Success";
        public const string setting_NFRD_SAF            = "setting_NFRD_SAF";
        public const string setting_RSS_ADD             = "setting_RssAdd";
        public const string setting_CacheChange         = "setting_CacheChange";
        public const string setting_CacheChanged        = "setting_CacheChanged";
        public const string setting_updateNow           = "setting_updateNow";


        public const string errNotFoundBrowzer     = "errNotFoundBrowzer";
        public const string errNothingIsFound      = "errNothingIsFound";
        public const string errNotFoundTopix       = "errNotFoundTopix";
        public const string errNoTopixNoConnection = "errNoTopixNoConnection";

        public const string newDataSearchStart = "newDataSearchStart";
        public const string newDataSearchEnd   = "newDataSearchEnd";
        public const string updateNow          = "updateNow";
        public const string err_BrowzerErr     = "err_BrowzerErr";
        public const string err_Commnand       = "err_Commnand";

        public const string rdoFrqReticent      = "rdoFrqReticent";
        public const string rdoFrqRefined       = "rdoFrqRefined";
        public const string rdoFrqKeeps         = "rdoFrqKeeps";
        public const string rdoFrqQuiet         = "rdoFrqQuiet";
        public const string rdoFrqNormal        = "rdoFrqNormal";
        public const string rdoFrqTalkative     = "rdoFrqTalkative";
        public const string rdoFrqNoisy         = "rdoFrqNoisy";
        public const string rdoFrqVerryNoisy    = "rdoFrqVerryNoisy";
        public const string rdoFrqMachen        = "rdoFrqMachen";
        public const string rdoFrqChangeable    = "rdoFrqChangeable";
        public const string notConect           = "notConect";
        public const string setting_DbCrean     = "setting_DbCrean";
        public const string setting_DbCrean_End = "setting_DbCrean_End";

        public const string endChat = "endChat";
        #endregion

        ///=============================
        /// POST定義
        #region POST定義
        public const int WEB_POST_TIMEOUT = 5000;
        public const string WEB_POST_METHOD = "POST";
        public const string WEB_POST_CONTENT_TYPE = "application/x-www-form-urlencoded";
        #endregion

        ///=============================
        /// GET定義
        #region GET定義
        public const int WEB_GET_TIMEOUT = 5000;
        public const string WEB_GET_METHOD = "GET";
        #endregion

        ///=============================
        /// インターバル定義
        #region インターバル定義
        public const int AUTO_EN_INTERVAL       = 30000;
        public const int faidStartInterval     = 5;
        public const int faidEndInterval       = 5;
        public const int ACTION_TIMER_INTERVAL = 100;
        public const int MINITS_TIMER_INTERVAL = 60000;
        public const int MOTHION_INIT          = 30;
        public const int DB_CHECK_COUNT        = 60;
        public const int STOP_CHOKO_STOP       = 120;
        public const int STOP_DOKA_TEI         = 180;
        public const int FIRST_STIFFENING      = 60;
        #endregion

        ///=============================
        /// LiplisCommand定義
        #region LiplisCommand定義
        public const int LM_NEXT             = 1;
        public const int LM_SLEEP            = 2;
        public const int LM_LOG              = 4;
        public const int LM_MINIMIZE         = 5;
        public const int LM_NORMALIZE        = 6;     
        public const int LM_END              = 7;
        public const int LM_RSS              = 8;
        public const int LM_CHAR             = 9;
        public const int LM_CHANGE           = 10;
        public const int LM_LOAD_SETTING     = 11;
        public const int LM_SETTING          = 12;
        public const int LM_WIDGET           = 13;
        public const int LM_DOWN             = 14;
        public const int LM_DOGA_DOWN        = 15;
        public const int LM_MP3_DOWN         = 16;
        public const int LM_RSS_BRW          = 17;
        public const int LM_TWT_ACT          = 18;
        public const int LM_TWITTER          = 19;
        public const int LM_RELOAD_VOICELOID = 20;
        public const int LM_FILTER           = 21;
        public const int LM_BATTERY          = 22;
        public const int LM_OUTRANGE_RECOVERY= 23;
        public const int LM_TWEET            = 24;
        public const int LM_SHOW_TELL_WIN    = 25;
        public const int LM_CHAT_SEND        = 26;
        public const int LM_TOPIC_RELOAD     = 27;
        public const int LM_CHANGE_SPEED     = 28;
        public const int LM_CHANGE_MODE      = 29;

        public const int LM_WIN_FONTS　                 =　1001;
        public const int LM_WIN_IEXPLORE　              =　1002;
        public const int LM_WIN_WUPDMGR　               =　1003;
        public const int LM_WIN_WSCUI_CPL　             =　1004;
        public const int LM_WIN_EXCEL　                 =　1005;
        public const int LM_WIN_CONTROL_ADMINTOOLS　    =　1006;
        public const int LM_WIN_CMD　                   =　1007;
        public const int LM_WIN_CONTROL　               =　1008;
        public const int LM_WIN_SERVICES_MSC　          =　1009;
        public const int LM_WIN_MSCONFIG　              =　1010;
        public const int LM_WIN_DFRG_MSC　              =　1011;
        public const int LM_WIN_DEVMGMT_MSC　           =　1012;
        public const int LM_WIN_CALC　                  =　1013;
        public const int LM_WIN_POWERPNT　              =　1014;
        public const int LM_WIN_TIMEDATE_CPL　          =　1015;
        public const int LM_WIN_APPWIZ_CPL　            =　1016;
        public const int LM_WIN_NOTEPAD　               =　1017;
        public const int LM_WIN_REGEDIT　               =　1018;
        public const int LM_WIN_WINWORD　               =　1019;
        public const int LM_WIN_WRITE　                 =　1020;
        public const int LM_WIN_MOVIEMK　               =　1021;
        public const int LM_WIN_MSIMN　                 =　1022;
        public const int LM_WIN_NUSRMGR_CPL　           =　1023;
        public const int LM_WIN_WMPLAYER　              =　1024;
        public const int LM_WIN_MSMSGS　                =　1025;
        public const int LM_WIN_EXPLORER　              =　1026;
        public const int LM_WIN_CONTROL_DESKTOP　       =　1027;
        public const int LM_WIN_DESK_CPL　              =　1028;
        public const int LM_WIN_COMPMGMT_MSC　          =　1029;
        public const int LM_WIN_DCOMCNFG　              =　1030;
        public const int LM_WIN_MMSYS_CPL　             =　1031;
        public const int LM_WIN_SYSDM_CPL　             =　1032;
        public const int LM_WIN_WUAUCPL_CPL　           =　1033;
        public const int LM_WIN_TASKMGR　               =　1034;
        public const int LM_WIN_DISKMGMT_MSC　          =　1035;
        public const int LM_WIN_POWERCFG_CPL　          =　1036;
        public const int LM_WIN_PERFMON_MSC　           =　1037;
        public const int LM_WIN_PERFMON　               =　1038;
        public const int LM_WIN_FIREWALL_CPL　          =　1039;
        public const int LM_WIN_CONTROL_FOLDERS　       =　1040;
        public const int LM_WIN_CONTROL_PRINTERS　      =　1041;
        public const int LM_WIN_HDWWIZ_CPL　            =　1042;
        public const int LM_WIN_FSQUIRT　               =　1043;
        public const int LM_WIN_DDESHARE　              =　1044;
        public const int LM_WIN_DXDIAG　                =　1045;
        public const int LM_WIN_DISKPART　              =　1046;
        public const int LM_WIN_VERIFIER　              =　1047;
        public const int LM_WIN_IEXPRESS　              =　1048;
        public const int LM_WIN_LOGOFF　                =　1049;
        public const int LM_WIN_MRT　                   =　1050;
        public const int LM_WIN_CONF　                  =　1051;
        public const int LM_WIN_ODBCCP32_CPL　          =　1052;
        public const int LM_WIN_PASSWORD_CPL　          =　1053;
        public const int LM_WIN_PRINTERS　              =　1054;
        public const int LM_WIN_NTMSMGR_MSC　           =　1055;
        public const int LM_WIN_STICPL_CPL　            =　1056;
        public const int LM_WIN_CLICONFG　              =　1057;
        public const int LM_WIN_TCPTEST　               =　1058;
        public const int LM_WIN_TELNET　                =　1059;
        public const int LM_WIN_WMIMGMT_MSC　           =　1060;
        public const int LM_WIN_SYSKEY　                =　1061;
        public const int LM_WIN_TOURSTART　             =　1062;
        public const int LM_WIN_DRWTSN32　              =　1063;
        public const int LM_WIN_WINVER　                =　1064;
        public const int LM_WIN_WAB　                   =　1065;
        public const int LM_WIN_WABMIG　                =　1066;
        public const int LM_WIN_EVENTVWR_MSC　          =　1067;
        public const int LM_WIN_ICWCONN1　              =　1068;
        public const int LM_WIN_INETCPL_CPL　           =　1069;
        public const int LM_WIN_CIADV_MSC　             =　1070;
        public const int LM_WIN_PACKAGER　              =　1071;
        public const int LM_WIN_MAGNIFY　               =　1072;
        public const int LM_WIN_CONTROL_KEYBOARD　      =　1073;
        public const int LM_WIN_FSMGMT_MSC　            =　1074;
        public const int LM_WIN_CLIPBRD　               =　1075;
        public const int LM_WIN_GPEDIT_MSC　            =　1076;
        public const int LM_WIN_JOY_CPL　               =　1077;
        public const int LM_WIN_SYSEDIT　               =　1078;
        public const int LM_WIN_MSINFO32　              =　1079;
        public const int LM_WIN_CERTMGR_MSC　           =　1080;
        public const int LM_WIN_OSK　                   =　1081;
        public const int LM_WIN_SPIDER　                =　1082;
        public const int LM_WIN_DIALER　                =　1083;
        public const int LM_WIN_CONTROL_SCHEDTASKS　    =　1084;
        public const int LM_WIN_INTL_CPL　              =　1085;
        public const int LM_WIN_CHKDSK　                =　1086;
        public const int LM_WIN_WINCHAT　               =　1087;
        public const int LM_WIN_CLEANMGR　              =　1088;
        public const int LM_WIN_RASPHONE　              =　1089;
        public const int LM_WIN_TELEPHON_CPL　          =　1090;
        public const int LM_WIN_MOBSYNC　               =　1091;
        public const int LM_WIN_CONTROL_NETCONNECTIONS　=　1092;
        public const int LM_WIN_NCPA_CPL　              =　1093;
        public const int LM_WIN_MSHEARTS　              =　1094;
        public const int LM_WIN_HYPERTRM　              =　1095;
        public const int LM_WIN_PINBALL　               =　1096;
        public const int LM_WIN_MIGWIZ　                =　1097;
        public const int LM_WIN_SIGVERIF　              =　1098;
        public const int LM_WIN_CONTROL_FONTS　         =　1099;
        public const int LM_WIN_FREECELL　              =　1100;
        public const int LM_WIN_MSPAINT　               =　1101;
        public const int LM_WIN_PBRUSH　                =　1102;
        public const int LM_WIN_HELPCTR　               =　1103;
        public const int LM_WIN_RSOP_MSC　              =　1104;
        public const int LM_WIN_WINMINE　               =　1105;
        public const int LM_WIN_CONTROL_MOUSE　         =　1106;
        public const int LM_WIN_MAIN_CPL　              =　1107;
        public const int LM_WIN_CHARMAP　               =　1108;
        public const int LM_WIN_ACCESS_CPL　            =　1109;
        public const int LM_WIN_ACCWIZ　                =　1110;
        public const int LM_WIN_UTILMAN　               =　1111;
        public const int LM_WIN_NTMSOPRQ_MSC　          =　1112;
        public const int LM_WIN_MSTSC　                 =　1113;
        public const int LM_WIN_REGEDIT32　             =　1114;
        public const int LM_WIN_SECPOL_MSC　            =　1115;
        public const int LM_WIN_LUSRMGR_MSC　           =　1116;
        public const int LM_WIN_NETSETUP_CPL　          =　1117;


        public const string LMP_NONOP = "";
        #endregion

        ///=============================
        /// チャット定義(仮) xmlに起こす
        #region チャット定義(仮)
        public const string SAY_ZZZ = "zzz";
        #endregion
        
        ///=============================
        /// 品詞定義
        #region 品詞
        public const int POS_DEFINE_KOYUMEISHI = 41;
        #endregion

        ///=============================
        /// ウィジェット定義
        #region ウィジェット定義
        public const string WIDGET_TEST    = "TEST";
        public const string WIDGET_SYS     = "システム ウィジェット";
        public const string WIDGET_RSS     = "RSS ウィジェット";
        public const string WIDGET_BRW     = "BROWSER ウィジェット";
        public const string WIDGET_WTH     = "お天気 ウィジェット";
        public const string WIDGET_CPU     = "CPU ウィジェット";
        public const string WIDGET_MEM     = "MEM ウィジェット";
        public const string WIDGET_HDD     = "HDD ウィジェット";
        public const string WIDGET_LAN     = "ネットワーク ウィジェット";
        #endregion

        ///=============================
        /// ウィジェットタイプ定義
        #region ウィジェット定義
        public const int WIDGET_TYPE_TEST    = 99;
        public const int WIDGET_TYPE_SYS     = 10;
        public const int WIDGET_TYPE_RSS     = 21;
        public const int WIDGET_TYPE_BRW     = 22;
        public const int WIDGET_TYPE_WTH     = 23;
        public const int WIDGET_TYPE_CPU     = 24;
        public const int WIDGET_TYPE_MEM     = 25;
        public const int WIDGET_TYPE_HDD     = 26;
        public const int WIDGET_TYPE_LAN     = 27;

        ///=============================
        /// ウィジェットタイプ定義
        public const int EXTENTION_FLV = 28;
        public const int EXTENTION_MP3 = 29;
        #endregion

        ///=============================
        /// ボイスロイド定義
        #region ボイスロイド定義
        public const string LPS_VOICE_ROID_SOFTALK = "SofTalk";
        public const string LPS_VOICE_ROID_YUKARI = "VOICEROID＋ 結月ゆかり";
        public const string LPS_VOICE_ROID_TOMOE = "VOICEROID＋ 民安ともえ";
        public const string LPS_VOICE_ROID_ZUNKO = "VOICEROID＋ 東北ずん子";
        public const string LPS_VOICE_ROID_YUKARI_EX = "VOICEROID＋ 結月ゆかり EX";   // 2015/09/02 Lipli4.5.3
        public const string LPS_VOICE_ROID_TOMOE_EX = "VOICEROID＋ 民安ともえ EX";    // 2015/09/02 Lipli4.5.3
        public const string LPS_VOICE_ROID_ZUNKO_EX = "VOICEROID＋ 東北ずん子 EX";    // 2015/09/02 Lipli4.5.3

        public const string LPS_VOICE_ROID_AKANE = "VOICEROID＋ 琴葉茜";           // 2015/09/02 Lipli4.5.5
        public const string LPS_VOICE_ROID_AOI = "VOICEROID＋ 琴葉葵";             // 2015/09/02 Lipli4.5.5
        #endregion

        ///=============================
        /// ニュース取得時間範囲定義
        #region ニュース取得時間範囲定義
        public const string LPS_TOPIC_HOUR_RANGE_1HOUR = "1時間";
        public const string LPS_TOPIC_HOUR_RANGE_3HOUR = "3時間";
        public const string LPS_TOPIC_HOUR_RANGE_6HOUR = "6時間";
        public const string LPS_TOPIC_HOUR_RANGE_12HOUR = "12時間";
        public const string LPS_TOPIC_HOUR_RANGE_1DAY = "1日";
        public const string LPS_TOPIC_HOUR_RANGE_3DAY = "3日";
        public const string LPS_TOPIC_HOUR_RANGE_7DAY = "7日";
        public const string LPS_TOPIC_HOUR_RANGE_UNLIMITED = "無制限";
        #endregion

        ///=============================
        /// アクティブ度
        #region アクティブ度
        public const int ACCTIVE_OTENBA = 0;
        public const int ACCTIVE_NORMAL = 1;
        public const int ACCTIVE_LITTLE_YUKKURI = 2;
        public const int ACCTIVE_YUKKURI = 3;
        

        public const int ACCTIVE_ECO = 99;

        public const int TALK_INTERVAL = 33;
        #endregion

    }
}
