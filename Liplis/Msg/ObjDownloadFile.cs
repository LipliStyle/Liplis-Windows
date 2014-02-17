//=======================================================================
//  ClassName : ObjDownloadFile
//  概要      : ダウンロードファイルオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2012 LipliStyle. All Rights Reserved. 
//=======================================================================

using System;
namespace Liplis.Msg
{
    [Serializable]
    public class ObjDownloadFile
    {
        public string url           { get; set; }
        public string title         { get; set; }
        public string dlPath        { get; set; }
        public double fileSize      { get; set; }
        public int kbn              { get; set; }
        public int doing            { get; set; }
        public int X                { get; set; }
        public int Y                { get; set; }
        public bool flgEnd { get; set; }

        public ObjDownloadFile(string url, string title, string dlPath, double fileSize, int kbn, int X, int Y)
        {
            this.url      = url;
            this.title    = title;
            this.dlPath   = dlPath;
            this.fileSize = fileSize;
            this.kbn      = kbn;
            this.doing    = 2;
            this.X        = X;
            this.Y        = Y;
            flgEnd        = false;
        }
    }
}
