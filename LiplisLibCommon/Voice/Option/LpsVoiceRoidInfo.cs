namespace Liplis.Voice.Option
{
    public class TVoiceRoidInfo
    {
        public string sWindowTitle;
        public string sExePath;
        public int nFreq;

        public TVoiceRoidInfo(string sWindowTitle, string sExePath, int nFreq)
        {
            this.sWindowTitle = sWindowTitle;
            this.sExePath = sExePath;
            this.nFreq = nFreq;
        }
    }
}
