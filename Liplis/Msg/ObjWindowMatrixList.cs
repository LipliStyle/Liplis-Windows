//=======================================================================
//  ClassName : ObjWIndowMatrixList
//  概要      : ショートニュースメッセージ
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using Liplis.Widget;

namespace Liplis.Msg
{
    [Serializable]
    public class WidgetSettingList
    {
        public List<WidgetBaseSetting> widgetList { get; set; }

        /// <summary>
        /// WidgetSettingList
        /// マトリックスリスト
        /// </summary>
        #region WidgetSettingList
        public WidgetSettingList()
        {
            widgetList = new List<WidgetBaseSetting>();
        }
        #endregion

        /// <summary>
        /// addSetting
        /// マトリックスに追加する
        /// </summary>
        #region addSetting
        public void addSetting(WidgetBaseSetting m)
        {
            widgetList.Add(m);
        }
        #endregion

        /// <summary>
        /// deleteSetting
        /// マトリックスから削除する
        /// </summary>
        #region deleteSetting
        public void deleteSetting(WidgetBaseSetting m)
        {
            for(int idx = 0; idx < widgetList.Count; idx++)
            {
                try
                {
                    if (widgetList[idx].windowId == m.windowId)
                    {
                        widgetList.RemoveAt(idx);
                    }
                }
                catch
                {
                
                }
            }
        }
        #endregion

    }
}
