//=======================================================================
//  ClassName : msgVoiceRoid
//  概要      : ボイスロイドメッセージ
//
//  Liplisシステム      
//  Copyright(c) 2010-2013 sachin.Sachin
//=======================================================================
namespace Liplis.Msg
{
    public class msgVoiceRoid
    {
        public string title { get; set; }
        public string path { get; set; }

        public msgVoiceRoid()
        {
            this.title = "";
            this.path = "";
        }

        public msgVoiceRoid(string title, string path)
        {
            this.title = title;
            this.path = path;
        }
    }
}
