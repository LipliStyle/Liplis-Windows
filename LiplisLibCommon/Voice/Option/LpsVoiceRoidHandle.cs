using System;

namespace Liplis.Voice.Option
{
    public struct LpsVoiceRoidHandle
    {
        public IntPtr hWindowHandle;
        public IntPtr hPlayHandle;
        public IntPtr hStopHandle;
        public IntPtr hEditHandle;
        public LpsVoiceRoidInfo Info;
    }
}
