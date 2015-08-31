using Liplis.Voice;
using System.Collections.Generic;

namespace Liplis.Msg
{
    public class msgVoiceRoid
    {
        public List<TVoiceRoidInfo> lstVoideRoidInfo = new List<TVoiceRoidInfo>();
        public List<TSwitchKeyword> lstSwitchKeyword = new List<TSwitchKeyword>();
        public List<TSimpleReplace> lstSimpleReplace = new List<TSimpleReplace>();
        public List<TRegReplace> lstRegReplace = new List<TRegReplace>();

        public string sWindowTitle = "VOICEROID＋ 民安ともえ";
        public string sExePath = "VOICEROID.exe";


        public int nWaitOfChar = 750;
        public int nHangBehavior;
        public int nTryCount = 5;
        public int nTryInterval = 100;
        public int nPlayInterval;

        public ECallType eCallType = ECallType.Auto;
        
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
        }

        /// <summary>
        /// デフォルトコンストラクター
        /// </summary>
        public msgVoiceRoid()
        {

        }


    }
}
