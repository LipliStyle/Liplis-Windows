using Liplis.Msg;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Liplis.Voice
{
    public sealed class OptionManager
    {
        public const int c_nMinWaitTime = 5000;
        public const string c_sOptionFile = "vrx.xml";
        public msgVoiceRoid prop = new msgVoiceRoid();
        private static OptionManager m_instance = new OptionManager();
        public static OptionManager Instance
        {
            get
            {
                return OptionManager.m_instance;
            }
        }
        public OptionManager()
        {
            string path = Application.StartupPath + "\\vrx.xml";
            if (File.Exists(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(msgVoiceRoid));
                FileStream fileStream = new FileStream(path, FileMode.Open);
                this.prop = (msgVoiceRoid)xmlSerializer.Deserialize(fileStream);
                fileStream.Close();
                if (this.prop.lstVoideRoidInfo.Count == 0)
                {
                    TVoiceRoidInfo item;
                    item.sWindowTitle = this.prop.sWindowTitle;
                    item.sExePath = this.prop.sExePath;
                    item.nFreq = 100;
                    this.prop.lstVoideRoidInfo.Add(item);
                }
            }
        }
        public void saveOption()
        {
            string path = Application.StartupPath + "\\vrx.xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(msgVoiceRoid));
            MemoryStream memoryStream = new MemoryStream();
            Encoding.UTF8.GetString(memoryStream.ToArray());
            FileStream fileStream = new FileStream(path, FileMode.Create);
            xmlSerializer.Serialize(fileStream, this.prop);
            fileStream.Close();
        }
    }
}
