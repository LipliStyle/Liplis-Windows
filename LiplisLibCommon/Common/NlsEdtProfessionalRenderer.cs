//=======================================================================
//  ClassName : NlsEdtProfessionalRenderer
//  概要      : ノラリスエディタ用プロフェッショナルレンダラ
//
//  Liplis2.0
//  Copyright(c) 2010-2012 LipliStyle.Sachin
//=======================================================================
using System.Drawing;
using System.Windows.Forms;

namespace Liplis.Common
{
    public class NlsEdtProfessionalRenderer : ProfessionalColorTable
    {
        //ToolStripのグラデーションの色を指定
        public override Color ToolStripGradientBegin
        {
            get
            {
                return Color.WhiteSmoke;
            }
        }

        public override Color ToolStripGradientMiddle
        {
            get
            {
                return Color.FromArgb(217, 227, 188);
            }
        }

        public override Color ToolStripGradientEnd
        {
            get
            {
                return Color.FromArgb(217, 227, 188);
            }
        }

        //ToolStripPanelのグラデーションの色を指定
        public override Color ToolStripPanelGradientBegin
        {
            get
            {
                return Color.Gold;
            }
        }

        public override Color ToolStripPanelGradientEnd
        {
            get
            {
                return Color.Ivory;
            }
        }
    }
}