//=======================================================================
//  ClassName : ComDefneMost
//  概要      : 共通定数定義
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin. All Rights Reserved. 
//=======================================================================
namespace Liplis.Common
{
    public static class LpsDefineMost
    {
        ///=============================
        /// エンコード
        public const string ENCODING_SJIS         = "Shift-JIS";     //日本語
        public const string ENCODING_UTF8         = "UTF-8";     //日本語
        public const string ENCODING_GB2312       = "936";           //中国語 簡易
        public const string ENCODING_BIG5         = "950";           //中国語 繁
        public const string ENCODING_IBM860       = "860";           //ポルトガル
        public const int ENCODING_ISO_2022_KR     = 50225;         //朝鮮
        public const string ENCODING_WINDOWS_1256 = "1256";          //アラビア
        public const string ENCODING_IBM863       = "863";           //フランス語
        public const int ENCODING_UNICODE         = 1200;

        ///=============================
        /// EXE
        public const string EXE_FFMPEG = @"ffmpeg.exe";

        //=====================================
        // HTMLコンテントタイプ
        public const string CONTENT_TYPE_TEXT_JAVASCRIPT = "text/javascript";
        public const string CONTENT_TYPE_TEXT_XML        = "text/xml";

        ///=============================
        /// 拡張子定義
        #region 拡張子定義
        public const string LPS_EXTENSION_FLV = "flv";
        public const string LPS_EXTENSION_JPG = "jpg";
        public const string LPS_EXTENSION_PNG = "png";
        public const string LPS_EXTENSION_MHT = "mht";
        public const string LPS_EXTENSION_MP3 = "mp3";
        #endregion


        //=====================================
        // Mecab アウトプットフォーマット定義
        public const string MECAB_OUTPUTFORMT_LATTICE = "lattice";
        public const string MECAB_OUTPUTFORMT_WAKATI  = "wakati";
        public const string MECAB_OUTPUTFORMT_DUMP    = "dump";

        //=====================================
        // Mecab リテラル
        public const char LITE_KIGO_TAB       = '\t';
        public const char LITE_KIGO_LINE_FEED = '\n';
        public const char LITE_KIGO_CONMA     = ',';

        ///=============================
        /// Liplis定義
        #region Liplis定義
        public const int LPS_ICON_DIF = 90;
        public const string MESSAGE_TITLE = "Liplis";
        #endregion

        ///=============================
        /// タグ定義
        #region タグ定義
        public const string LPS_TAG_KOYUMEISHI  = "KM";
        public const string LPS_TAG_LINK        = "URL";
        public const string LPS_TAG_NICO_VIDEO  = "NICO";
        public const string LPS_TAG_NICO_MYLIST = "MYLIST";
        #endregion

        ///=============================
        /// URL定義
        #region URL定義
        public const string URL_NICO_VIDEO           = "http://www.nicovideo.jp/watch/";
        public const string URL_NICO_DOMAIN          = "http://www.nicovideo.jp/";
        public const string URL_NICO_LOGIN_PAGE_URL  = "https://secure.nicovideo.jp/secure/login?site=niconico";
        public const string URL_NICO_INFO            = "http://ext.nicovideo.jp/api/getthumbinfo/";
        #endregion

        ///=============================
        /// ニコニコ検索ソート定義
        #region ニコニコ検索ソート定義
        public const string NICO_SEARCH_SORT_NEW_D     = "?sort=n&order=d";
        public const string NICO_SEARCH_SORT_NEW_A     = "?sort=n&order=a";
        public const string NICO_SEARCH_SORT_SAISEI_D  = "?sort=v&order=d";
        public const string NICO_SEARCH_SORT_SAISEI_A  = "?sort=v&order=a";
        public const string NICO_SEARCH_SORT_COMMENT_D = "?sort=r&order=d";
        public const string NICO_SEARCH_SORT_COMMENT_A = "?sort=r&order=a";
        public const string NICO_SEARCH_SORT_MYLIST_D  = "?sort=m&order=d";
        public const string NICO_SEARCH_SORT_MYLIST_A  = "?sort=m&order=a";
        public const string NICO_SEARCH_SORT_TOKO_D    = "?sort=f&order=d";
        public const string NICO_SEARCH_SORT_TOKO_A    = "?sort=f&order=a";
        public const string NICO_SEARCH_SORT_LONG_D    = "?sort=l&order=d";
        public const string NICO_SEARCH_SORT_LONG_A    = "?sort=l&order=a";
        #endregion

        ///=============================
        /// ニコニコ検索オプション定義
        #region ニコニコ検索オプション定義
        public const string NICO_SEARCH_OPT_TAG = "tag/";
        public const string NICO_SEARCH_OPT_WORD = "search/";
        public const string NICO_SEARCH_OPT_BOTH = "both/";
        #endregion

        ///=============================
        /// ノラリスパスコード
        public const string NORALIS_CODE = "akc8z494759mpmt4ndf2767dfjmu32zjuz5gch6zdkxu8cxubt6id88cnn3h39rp9y4dt3k3y635ypsda5wtbbbfe36jidh4ydhb";
        public const string NORALIS_ARCHIVE_NAME = "NoralisArchive.zip";

        ///=============================
        /// ツイッター LiplisTalk コード
        public const string TWITTER_LIPLIS_TALK_TOKEN = "W1tQBXDr3pQu1atfIwp6A";
        public const string TWITTER_LIPLIS_TALK_SECRET = "eTFat5surbln3MH7f0uIlwmpOcQdjlkyg7vUk90eG8";


    }
}
