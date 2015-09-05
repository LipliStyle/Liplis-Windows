using Liplis.Voice;
using Liplis.Voice.Option;
using System.Collections.Generic;

namespace Liplis.Msg
{
    public class msgVoiceRoid
    {
        //必須プロパティ
        public LpsVoiceRoidInfo vrInfo;
        public string windowTitle = "VOICEROID＋ 民安ともえ";
        public string voiceRoidPath = "VOICEROID.exe";

        //設定
        public int nWaitOfChar = 750;
        public int nHangBehavior;
        public int nTryCount = 5;
        public int nTryInterval = 100;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="windowTitle"></param>
        /// <param name="path"></param>
        public msgVoiceRoid(string windowTitle, string voiceRoidPath)
        {
            this.windowTitle = windowTitle;
            this.voiceRoidPath = voiceRoidPath;

            //ボイスロイドインフォ追加
            vrInfo = new LpsVoiceRoidInfo(windowTitle, voiceRoidPath);
        }

        /// <summary>
        /// デフォルトコンストラクター
        /// </summary>
        public msgVoiceRoid()
        {

        }


    }
}
