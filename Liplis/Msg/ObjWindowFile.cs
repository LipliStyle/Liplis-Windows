//=======================================================================
//  ClassName : ObjWindowFile
//  概要      : ウインドウオブジェクト
//
//  2013/08/31 Liplis3.0.5 ico_sleep.png→ico_zzz.pngに変更
//                         バージョン違い windowリソース 互換性チェック機能追加
//  Liplis3.0
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System.Drawing;
using Liplis.Common;
using Liplis.Fct;
using System;

namespace Liplis.Msg
{
    public class ObjWindowFile : FctWindowFileLoader, IDisposable
    {
        public Bitmap ico_next      { get; set; }
        public Bitmap ico_setting   { get; set; }
        public Bitmap ico_zzz       { get; set; }   //ver3.0.5 リネーム
        public Bitmap ico_waikUp    { get; set; }
        public Bitmap ico_pow       { get; set; }
        public Bitmap ico_log       { get; set; }
        public Bitmap ico_rss       { get; set; }
        public Bitmap ico_tray      { get; set; }
        public Bitmap ico_char      { get; set; }

        public Bitmap bt_setting    { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public ObjWindowFile(string loadSkin)
        {
            //ウインドウファイルパスの取得
            string windowPath = LpsPathControllerCus.getBodyDefinePath(loadSkin);

            //body.xmlの存在チェック
            if (LpsPathControllerCus.checkFileExist(windowPath))
            {
                //2013/08/31 ver3.0.5 ウインドウリソースの互換性チェック
                compatibilityCheck(loadSkin);

                //読み込み結果の取得
                loadWindowBitmap(loadSkin);
            }
            else
            {
                //リソース読み込み
                createDefault();
            }
        }
        #endregion

        /// <summary>
        /// ディスポーズ
        /// </summary>
        #region Dispose
        public void Dispose()
        {

        }
        #endregion

        /// <summary>
        /// loadWindowBitmap
        /// ウインドウビットマップをロードする
        /// </summary>
        #region loadWindowBitmap
        private void loadWindowBitmap(string loadSkin)
        {
            ico_next    = loadIcon(loadSkin, LiplisDefine.ICO_NEXT);
            ico_setting = loadIcon(loadSkin, LiplisDefine.ICO_SETTING);
            ico_zzz     = loadIcon(loadSkin, LiplisDefine.ICO_ZZZ);
            ico_waikUp  = loadIcon(loadSkin, LiplisDefine.ICO_WAIKUP);
            ico_pow     = loadIcon(loadSkin, LiplisDefine.ICO_POW);
            ico_log     = loadIcon(loadSkin, LiplisDefine.ICO_LOG);
            ico_rss     = loadIcon(loadSkin, LiplisDefine.ICO_RSS);
            ico_char    = loadIcon(loadSkin, LiplisDefine.ICO_CHAR);
            ico_tray    = loadIcon(loadSkin, LiplisDefine.ICO_TRAY);
            bt_setting  = loadBitmap(loadSkin, LiplisDefine.BG_SETTING);
        }
        #endregion

        /// <summary>
        /// createDefault
        /// デフォルト作成
        /// </summary>
        #region createDefault
        private void createDefault()
        {
            ico_next       = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_NEXT));
            ico_setting    = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_SETTING));
            ico_zzz      = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_ZZZ));
            ico_waikUp     = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_WAIKUP));
            ico_pow        = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_POW));
            ico_log        = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_LOG));
            ico_rss        = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_RSS));
            ico_char       = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_CHAR));
            ico_tray       = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_TRAY));
            bt_setting     = FctCreateFromResource.getResourceBitmap(LiplisDefine.BG_DEF_SETTING);
        }
        #endregion


    }
}
