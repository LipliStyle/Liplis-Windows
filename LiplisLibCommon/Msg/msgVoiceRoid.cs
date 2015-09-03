using Liplis.Voice;
using Liplis.Voice.Option;
using System.Collections.Generic;

namespace Liplis.Msg
{
    public class msgVoiceRoid
    {
        public List<TVoiceRoidInfo> lstVoideRoidInfo = new List<TVoiceRoidInfo>();
        public List<TSwitchKeyword> lstSwitchKeyword = new List<TSwitchKeyword>();
        public List<LpsSimpleReplace> lstSimpleReplace = new List<LpsSimpleReplace>();
        public List<LpsRegReplace> lstRegReplace = new List<LpsRegReplace>();

        public string sWindowTitle = "VOICEROID＋ 民安ともえ";
        public string sExePath = "VOICEROID.exe";


        public int nWaitOfChar = 750;
        public int nHangBehavior;
        public int nTryCount = 5;
        public int nTryInterval = 100;
        public int nPlayInterval;

        public LpsCallType eCallType = LpsCallType.Auto;
        
        public bool bPlayStop;
        public bool bStudy;
        public bool bSelWord;
        public bool bSizeChange = true;
        public bool bCmdInterval;
        public bool bCmdIntervalRead;
        public bool bCmdFile;
        public bool bCmdFileRead;

        public string strCmdIntervalText = "/I:";
        public string strCmdFileText = "/F:";

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="windowTitle"></param>
        /// <param name="path"></param>
        public msgVoiceRoid(string windowTitle, string path)
        {
            this.sWindowTitle = windowTitle;
            this.sExePath = path;

            //ボイスロイドインフォ追加
            lstVoideRoidInfo.Add(new TVoiceRoidInfo(windowTitle, path,100));
        }

        /// <summary>
        /// デフォルトコンストラクター
        /// </summary>
        public msgVoiceRoid()
        {

        }


    }
}
