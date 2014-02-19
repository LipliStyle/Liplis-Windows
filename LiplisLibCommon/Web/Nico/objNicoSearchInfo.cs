//=======================================================================
//  ClassName : objNicoSearchInfo
//  概要      : ニコインフォオブジェクト
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin.Sachin
//=======================================================================

namespace Liplis.Web.Nico
{
    public class objNicoSearchInfo
    {
        public string page { get; set; }
        public string query { get; set; }
        public string words { get; set; }
        public int total { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
    }
}
