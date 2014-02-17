//=======================================================================
//  ClassName : xmlWetherYahoo
//  概要      : ヤフー天気
//
//  SatelliteServer
//  Copyright(c) 2009-2012 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Xml;
using Liplis.Msg;

namespace Liplis.Xml
{
    public class xmlWetherBase : XmlReadList
    {
        ///==========================
        /// リスト
        public string title                         { get; set; }
        public string link                          { get; set; }
        public string lastBuildDate                 { get; set; }
        public List<string> titleList               { get; set; }
        public List<string> linkList                { get; set; }
        public List<string> descriptionList         { get; set; }
        public List<string> pubDateList             { get; set; }
        public List<msgWetherDescription> msgList   { get; set; }
    }
}
