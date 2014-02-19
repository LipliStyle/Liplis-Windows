//=======================================================================
//  ClassName : xmlWetherYahoo
//  概要      : ヤフー天気
//
//  SatelliteServer
//  Copyright(c) 2009-2012 sachin.Sachin
//=======================================================================
using System.Collections.Generic;
using System.Xml;
namespace Liplis.Msg
{
    public class msgWetherDescription
    {
        ///==========================
        /// リスト
        public string date          { get; set; }
        public string region        { get; set; }
        public int region_id        { get; set; }
        public string dow           { get; set; }
        public string wether        { get; set; }
        public int maxTemperature   { get; set; }
        public int minTemperature   { get; set; }
        public int chanceOfRain     { get; set; }
        public string warming       { get; set; }
        public string link          { get; set; }

        /// <summary>
        /// msgWetherDescription
        /// コンストラクター
        /// </summary>
        /// <returns></returns>
        #region msgWetherDescription
        public msgWetherDescription(string date, string region, int region_id, string dow, string wether, int maxTemperature, int minTemperature, int chanceOfRain, string warming, string link)
        {
            this.date = date;
            this.region = region;
            this.region_id = region_id;
            this.dow = dow;
            this.wether = wether;
            this.maxTemperature = maxTemperature;
            this.minTemperature = minTemperature;
            this.chanceOfRain = chanceOfRain;
            this.warming = warming;
            this.link = link;
        }
        public msgWetherDescription()
        {
        }
        #endregion
    }
}
