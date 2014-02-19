//=======================================================================
//  ClassName : LpsDefineRegion
//  概要      : リージョン定義
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin.Sachin
//=======================================================================
using System.Collections.Generic;
namespace Liplis.Common
{
    public static class LpsDefineRegion
    {
        /// <summary>
        /// リージョンリストを返す
        /// </summary>
        /// <returns></returns>
        #region getRegion
        public static List<string> getRegion()
        {
            List<string> lstRegion = new List<string>();

            lstRegion.Add("北海道");
            lstRegion.Add("青森県");
            lstRegion.Add("岩手県");
            lstRegion.Add("宮城県");
            lstRegion.Add("秋田県");
            lstRegion.Add("山形県");
            lstRegion.Add("福島県");
            lstRegion.Add("茨城県");
            lstRegion.Add("栃木県");
            lstRegion.Add("群馬県");
            lstRegion.Add("埼玉県");
            lstRegion.Add("千葉県");
            lstRegion.Add("東京都");
            lstRegion.Add("神奈川県");
            lstRegion.Add("新潟県");
            lstRegion.Add("富山県");
            lstRegion.Add("石川県");
            lstRegion.Add("福井県");
            lstRegion.Add("山梨県");
            lstRegion.Add("長野県");
            lstRegion.Add("岐阜県");
            lstRegion.Add("静岡県");
            lstRegion.Add("愛知県");
            lstRegion.Add("三重県");
            lstRegion.Add("滋賀県");
            lstRegion.Add("京都府");
            lstRegion.Add("大阪府");
            lstRegion.Add("兵庫県");
            lstRegion.Add("奈良県");
            lstRegion.Add("和歌山県");
            lstRegion.Add("鳥取県");
            lstRegion.Add("島根県");
            lstRegion.Add("岡山県");
            lstRegion.Add("広島県");
            lstRegion.Add("山口県");
            lstRegion.Add("徳島県");
            lstRegion.Add("香川県");
            lstRegion.Add("愛媛県");
            lstRegion.Add("高知県");
            lstRegion.Add("福岡県");
            lstRegion.Add("佐賀県");
            lstRegion.Add("長崎県");
            lstRegion.Add("熊本県");
            lstRegion.Add("大分県");
            lstRegion.Add("宮崎県");
            lstRegion.Add("鹿児島県");
            lstRegion.Add("沖縄県");

            return lstRegion;
        }
        #endregion

        /// <summary>
        /// リージョンリストを返す
        /// </summary>
        /// <returns></returns>
        #region getRegionDictionary
        public static Dictionary<int, string> getRegionDictionary()
        {
            Dictionary<int, string> lstRegion = new Dictionary<int, string>();
            int idx = 0;

            foreach (string reg in getRegion())
            {
                lstRegion.Add(idx, reg);
                idx++;
            }

            return lstRegion;
        }
        #endregion

    }
}
