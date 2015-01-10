//=======================================================================
//  ClassName : WetherUtil
//  概要      : 天気ユーティリティ
//
//  Liplis4.5
//  Copyright(c) 2010-2015 LipliStyle.Sachin
//=======================================================================

namespace Liplis.Web.WetherInfo
{
    public class WetherUtil
    {
        /// <summary>
        /// 天気文字列からコードに変換する
        /// </summary>
        /// <param name="wether"></param>
        /// <returns></returns>
        #region convertWetherId
        public static string convertWetherStrToId(string wetherStr)
        {
            //ゆらぎの補正
            string fixStr = convertWetherStr(wetherStr);

            switch (fixStr)
            {
                case "晴": return "1";
                case "晴時々曇": return "2";
                case "晴時々雨": return "3";
                case "晴時々雪": return "4";
                case "晴後曇": return "5";
                case "晴後雨": return "6";
                case "晴後雪": return "7";
                case "曇": return "8";
                case "曇時々晴": return "9";
                case "曇時々雨": return "10";
                case "曇時々雪": return "11";
                case "曇後晴": return "12";
                case "曇後雨": return "13";
                case "曇後雪": return "14";
                case "雨": return "15";
                case "雨時々晴": return "16";
                case "雨時々曇": return "17";
                case "雨時々雪": return "18";
                case "雨後晴": return "19";
                case "雨後曇": return "20";
                case "雨後雪": return "21";
                case "雪": return "22";
                case "雪時々晴": return "23";
                case "雪時々曇": return "24";
                case "雪時々雨": return "25";
                case "雪後晴": return "26";
                case "雪後曇": return "27";
                case "雪後雨": return "28";
                case "暴風雪": return "29";
                default: return "0";
            }
        }

        /// <summary>
        /// コードから天気文字列に変換する
        /// </summary>
        /// <param name="wetherStr"></param>
        /// <returns></returns>
        public static string convertWetherIdToStr(string wetherStr)
        {
            //ゆらぎの補正
            string fixStr = convertWetherStr(wetherStr);

            switch (fixStr)
            {
                case "1": return "晴";
                case "2": return "晴時々曇";
                case "3": return "晴時々雨";
                case "4": return "晴時々雪";
                case "5": return "晴後曇";
                case "6": return "晴後雨";
                case "7": return "晴後雪";
                case "8": return "曇";
                case "9": return "曇時々晴";
                case "10": return "曇時々雨";
                case "11": return "曇時々雪";
                case "12": return "曇後晴";
                case "13": return "曇後雨";
                case "14": return "曇後雪";
                case "15": return "雨";
                case "16": return "雨時々晴";
                case "17": return "雨時々曇";
                case "18": return "雨時々雪";
                case "19": return "雨後晴";
                case "20": return "雨後曇";
                case "21": return "雨後雪";
                case "22": return "雪";
                case "23": return "雪時々晴";
                case "24": return "雪時々曇";
                case "25": return "雪時々雨";
                case "26": return "雪後晴";
                case "27": return "雪後曇";
                case "28": return "雪後雨";
                case "29": return "暴風雪";
                default: return "晴";
            }
        }

        /// <summary>
        /// ゆらぎ表現を統一する。
        /// 晴れ→晴
        /// 曇り→曇
        /// ときどき→時々
        /// のち→後
        /// </summary>
        /// <param name="wetherStr"></param>
        /// <returns></returns>
        public static string convertWetherStr(string wetherStr)
        {
            return wetherStr.Replace("晴れ", "晴").Replace("曇り", "曇").Replace("ときどき", "時々").Replace("のち", "後");
        }
        #endregion
    }
}
