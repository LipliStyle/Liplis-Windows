using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Resources;
using Liplis.Properties;
using Liplis.Common;

namespace Liplis.Fct
{
    public static class FctCreateFromResource
    {
        /// <summary>
        /// getResourceBitmap
        /// リソースからビットマップを取得する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        #region getResourceBitmap
        public static Bitmap getResourceBitmap(string resourceName)
        {
            ResourceManager rm = new ResourceManager("Liplis.Properties.Resources", typeof(Resources).Assembly);
            return (Bitmap)rm.GetObject(resourceName);
        }
        #endregion

        /// <summary>
        /// getResourceXml
        /// リソースからXmlを取得する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        #region getResourceXml
        public static string getResourceXml(string resourceName)
        {
            ResourceManager rm = new ResourceManager("Liplis.Properties.Resources", typeof(Resources).Assembly);
            return (string)rm.GetObject(resourceName);
        }
        #endregion

        /// <summary>
        /// getResourceXml
        /// リソースからXmlを取得する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        #region getTranse
        public static Bitmap getTranse()
        {
            ResourceManager rm = new ResourceManager("Liplis.Properties.Resources", typeof(Resources).Assembly);
            return (Bitmap)rm.GetObject(LiplisDefine.TRANSE);
        }
        #endregion

        /// <summary>
        /// 拡張子アイコンを取得する
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        #region getIconExtention
        public static Bitmap getIconExtention(int s)
        {
            try
            {
                switch (s)
                {
                    case LiplisDefine.WIDGET_TYPE_TEST:
                        return getResourceBitmap(LiplisDefine.ICO_WID_TES);
                    case LiplisDefine.WIDGET_TYPE_SYS:
                        return getResourceBitmap(LiplisDefine.ICO_WID_SYS);
                    case LiplisDefine.WIDGET_TYPE_RSS:
                        return getResourceBitmap(LiplisDefine.ICO_WID_RSS);
                    case LiplisDefine.WIDGET_TYPE_BRW:
                        return getResourceBitmap(LiplisDefine.ICO_WID_BRW);
                    case LiplisDefine.WIDGET_TYPE_WTH:
                        return getResourceBitmap(LiplisDefine.ICO_WID_WET);
                    case LiplisDefine.WIDGET_TYPE_CPU:
                        return getResourceBitmap(LiplisDefine.ICO_WID_CPU);
                    case LiplisDefine.WIDGET_TYPE_MEM:
                        return getResourceBitmap(LiplisDefine.ICO_WID_MEM);
                    case LiplisDefine.WIDGET_TYPE_HDD:
                        return getResourceBitmap(LiplisDefine.ICO_WID_HDD);
                    case LiplisDefine.WIDGET_TYPE_LAN:
                        return getResourceBitmap(LiplisDefine.ICO_WID_NET);
                    case LiplisDefine.EXTENTION_FLV:
                        return getResourceBitmap(LiplisDefine.ICO_EXT_FLV);
                    case LiplisDefine.EXTENTION_MP3:
                        return getResourceBitmap(LiplisDefine.ICO_EXT_MP3);
                    default:
                        return getTranse();
                }
            }
            catch
            {
                return getTranse();
            }
        }
        #endregion

    }
}
