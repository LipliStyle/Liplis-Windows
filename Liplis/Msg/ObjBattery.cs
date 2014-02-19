//=======================================================================
//  ClassName : Liplis
// ■概要     : バッテリー情報管理機能
//
//
//■ Liplis3.0
//  2013/10/29 Liplis3.2.0 バッテリー管理機能追加
//
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================

using Liplis.Fct;
using Liplis.Common;
using System.Drawing;
using System.Windows.Forms;
using System;
namespace Liplis.Msg
{
    public class ObjBattery : FctWindowFileLoader
    {
        ///=============================
        /// ステータス
        public string batteryText { get; set; }
        public bool batteryExists { get; set; }
        public bool batteryStatusChange { get; set; }
        public Bitmap nowBatteryImage { get; set; }
        public Bitmap prvBatteryImage { get; set; }

        ///=============================
        /// 定数
        private Bitmap battery_100;
        private Bitmap battery_87;
        private Bitmap battery_75;
        private Bitmap battery_62;
        private Bitmap battery_50;
        private Bitmap battery_37;
        private Bitmap battery_25;
        private Bitmap battery_12;
        private Bitmap battery_0;
        private Bitmap battery_non;
        
                /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public ObjBattery(string loadSkin, PowerStatus ps)
        {
            //ウインドウファイルパスの取得
            string windowPath = LpsPathControllerCus.getBodyDefinePath(loadSkin);

            //バッテリーステータスを
            batteryExists = setBatteryExists(ps);

            Console.WriteLine(ps.BatteryChargeStatus);

            //body.xmlの存在チェック
            if (LpsPathControllerCus.checkFileExist(windowPath))
            {
                //2013/08/31 ver3.0.5 ウインドウリソースの互換性チェック
                compatibilityCheck(loadSkin);

                //読み込み結果の取得
                loadWindowBitmap(loadSkin);

                //デフォルト設定
                getBatteryImage((int)(ps.BatteryLifePercent * 100));
                prvBatteryImage = nowBatteryImage;
                batteryStatusChange = false;
            }
            else
            {
                //リソース読み込み
                createDefault();
            }
        }
        #endregion

        /// <summary>
        /// setBatteryExists
        /// バッテリー存在チェック
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        #region Dispose
        private bool setBatteryExists(PowerStatus ps)
        {
            switch (ps.BatteryChargeStatus)
            {
                case BatteryChargeStatus.High:
                    return true;
                case BatteryChargeStatus.Low:
                    return true;
                case BatteryChargeStatus.Critical:
                    return true;
                case BatteryChargeStatus.Charging:
                    return true;
                case BatteryChargeStatus.NoSystemBattery:
                    return false;
                case BatteryChargeStatus.Unknown:
                    return true;
                default:
                    return true;
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
            battery_100 = loadIcon(loadSkin, LiplisDefine.ICO_BATTERY_100);
            battery_87 = loadIcon(loadSkin, LiplisDefine.ICO_BATTERY_87);
            battery_75 = loadIcon(loadSkin, LiplisDefine.ICO_BATTERY_75);
            battery_62 = loadIcon(loadSkin, LiplisDefine.ICO_BATTERY_62);
            battery_50 = loadIcon(loadSkin, LiplisDefine.ICO_BATTERY_50);
            battery_37 = loadIcon(loadSkin, LiplisDefine.ICO_BATTERY_37);
            battery_25 = loadIcon(loadSkin, LiplisDefine.ICO_BATTERY_25);
            battery_12 = loadIcon(loadSkin, LiplisDefine.ICO_BATTERY_12);
            battery_0 = loadIcon(loadSkin, LiplisDefine.ICO_BATTERY_0);
            battery_non = loadIcon(loadSkin, LiplisDefine.ICO_NON);


        }
        #endregion

        /// <summary>
        /// createDefault
        /// デフォルト作成
        /// </summary>
        #region createDefault
        private void createDefault()
        {
            battery_100 = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_BATTERY_100));
            battery_87  = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_BATTERY_87));
            battery_75  = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_BATTERY_75));
            battery_62  = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_BATTERY_62));
            battery_50  = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_BATTERY_50));
            battery_37  = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_BATTERY_37));
            battery_25  = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_BATTERY_25));
            battery_12  = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_BATTERY_12));
            battery_0   = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_BATTERY_0));
            battery_non = reductionBitmap(FctCreateFromResource.getResourceBitmap(LiplisDefine.ICO_DEF_NON));
        }
        #endregion

        /// <summary>
        /// getBatteryImage
        /// バッテリーイメージを返す
        /// </summary>
        public void getBatteryImage(int batteryNowLevel)
        {
            //バッテリー存在
            if (batteryExists)
            {
                if (batteryNowLevel <= 10)
                {
                    nowBatteryImage = battery_0;
                    Console.WriteLine("バッテリー0");
                }
                else if (batteryNowLevel <= 12)
                {
                    nowBatteryImage = battery_12;
                    Console.WriteLine("バッテリー12");
                }
                else if (batteryNowLevel <= 25)
                {
                    nowBatteryImage = battery_25;
                    Console.WriteLine("バッテリー25");
                }
                else if (batteryNowLevel <= 37)
                {
                    nowBatteryImage = battery_37;
                    Console.WriteLine("バッテリー37");
                }
                else if (batteryNowLevel <= 50)
                {
                    nowBatteryImage = battery_50;
                    Console.WriteLine("バッテリー50");
                }
                else if (batteryNowLevel <= 62)
                {
                    nowBatteryImage = battery_62;
                    Console.WriteLine("バッテリー62");
                }
                else if (batteryNowLevel <= 75)
                {
                    nowBatteryImage = battery_75;
                    Console.WriteLine("バッテリー75");
                }
                else if (batteryNowLevel <= 87)
                {
                    nowBatteryImage = battery_87;
                    Console.WriteLine("バッテリー87");
                }
                else if (batteryNowLevel > 87)
                {
                    nowBatteryImage = battery_100;
                    Console.WriteLine("バッテリー100");
                }

                //バッテリー残量の表示
                batteryText = batteryNowLevel + "%";


            }
            else
            {
                nowBatteryImage = battery_100;

                //バッテリー接続なし
                batteryText = "-";

                Console.WriteLine("バッテリー接続なし");
            }

            Console.WriteLine(batteryText);

            //バッテリーイメージが変化していたら、ステータスをチェンジにする
            batteryStatusChange = !nowBatteryImage.Equals(prvBatteryImage);

            //前回値の記録
            prvBatteryImage = nowBatteryImage;
        }
    }
}
