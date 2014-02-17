//=======================================================================
//  ClassName : WidgetMap
//  概要      : ウィジェットマップ
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using Liplis.Activity;
using Liplis.Msg;
using Liplis.Widget.WidBrw;
using Liplis.Widget.WidRss;
using Liplis.Widget.WidSys;
using Liplis.Widget.WidCpu;
using Liplis.Widget.WidMem;
using Liplis.Widget.WidHdd;
using Liplis.Widget.WidLan;

namespace Liplis.Widget
{
    public class WidgetMap
    {
        ///=====================================
        /// クラス名ディファイン
        private const string WIDGET_BASE    = "Liplis.Widget.WidCpu.WidgetBaseSetting";
        private const string WIDGET_SYS     = "Liplis.Widget.WidSys.WidgetCpuSetting";
        private const string WIDGET_BRW     = "Liplis.Widget.WidBrw.WidgetBrwSetting";
        private const string WIDGET_RSS     = "Liplis.Widget.WidRss.WidgetRssSetting";
        private const string WIDGET_CPU     = "Liplis.Widget.WidCpu.WidgetCpuSetting";
        private const string WIDGET_MEM     = "Liplis.Widget.WidMem.WidgetMemSetting";
        private const string WIDGET_HDD     = "Liplis.Widget.WidHdd.WidgetHddSetting";
        private const string WIDGET_LAN     = "Liplis.Widget.WidLan.WidgetLanSetting";

        ///====================================================================
        ///
        ///                           マッピング
        ///                         
        ///====================================================================

        #region WidgetBaseParent
        public static WidgetBaseParent getWidget(ObjWidgetSetting ws, ActivityWidget arr, WidgetBaseSetting s)
        {
            switch(s.GetType().FullName)
            {
                case WIDGET_BASE:
                    return getWidgetTest(ws, arr, s);
                case WIDGET_SYS:
                    return new WidgetSysParent(ws, arr, (WidgetSysSetting)s);
                case WIDGET_BRW:
                    return new WidgetBrwParent(ws, arr, s);
                case WIDGET_RSS:
                    return new WidgetRssParent(ws, arr, s);
                case WIDGET_CPU:
                    return new WidgetCpuParent(ws, arr, s);
                case WIDGET_MEM:
                    return new WidgetMemParent(ws, arr, s);
                case WIDGET_HDD:
                    return new WidgetHddParent(ws, arr, s);
                case WIDGET_LAN:
                    return new WidgetLanParent(ws, arr, s);
                default:
                    return getWidgetTest(ws, arr, s);
            }
        }
        #endregion

        /// <summary>
        /// getWidgetTest
        /// テストウィジェットを取得する
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="arr"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        #region getWidgetTest
        private static WidgetBaseParent getWidgetTest(ObjWidgetSetting ws, ActivityWidget arr, WidgetBaseSetting s)
        {
            if (s.size.Height == 1 && s.size.Width == 1)
            {
                return new frmParent(ws, arr, s);
            }
            else if (s.size.Height == 2 && s.size.Width == 1)
            {
                return new frm21Parent(ws, arr, s);
            }
            else if (s.size.Height == 1 && s.size.Width == 2)
            {
                return new frm12Parent(ws, arr, s);
            }
            else if (s.size.Height == 2 && s.size.Width == 2)
            {
                return new frm22Parent(ws, arr, s);
            }
            else
            {
                return new frmParent(ws, arr, s);
            }
        }
        #endregion

    }
}
