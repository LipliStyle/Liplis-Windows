//=======================================================================
//  ClassName : ComDefneMost
//  概要      : 共通定数定義
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin. All Rights Reserved. 
//=======================================================================
namespace Liplis.Common
{
    public static class ComDefineMost
    {
        ///=============================
        /// エンコード
        public const string ENCODING_SJIS         = "Shift-JIS";     //日本語
        public const string ENCODING_GB2312       = "936";           //中国語 簡易
        public const string ENCODING_BIG5         = "950";           //中国語 繁
        public const string ENCODING_IBM860       = "860";           //ポルトガル
        public const int ENCODING_ISO_2022_KR     = 50225;         //朝鮮
        public const string ENCODING_WINDOWS_1256 = "1256";          //アラビア
        public const string ENCODING_IBM863       = "863";           //フランス語
        public const int ENCODING_UNICODE         = 1200;



        //=====================================
        // HTMLコンテントタイプ
        public const string CONTENT_TYPE_TEXT_JAVASCRIPT = "text/javascript";
        public const string CONTENT_TYPE_TEXT_XML        = "text/xml";
        

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

    }
}
