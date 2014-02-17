//=======================================================================
//  ClassName : WidgetCpu02Base
//  概要      : こちらに透過しないオブジェクトを置く
//              親側からよば
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System.Windows.Forms;
using Liplis.Sys;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace Liplis.Widget
{
    public partial class WidgetHdd02Base : WidgetBaseBase
    {
        ///=============================
        ///クラス

        private CpuInfo cpu;
        private List<Label> gageList;

        /// <summary>
        /// WidgetCpu02Base
        /// 
        /// </summary>
        #region WidgetHdd02Base
        public WidgetHdd02Base()
        {
            InitializeComponent();
            init();
        }
        #endregion


        #region init
        private void init()
        {
            //CPU情報取得クラス
            cpu = new CpuInfo();

            gageList = new List<Label>();

            for (int idx = 0; idx < 61; idx++)
            {
                gageList.Add(createLabel(idx));
            }

            timSystemInfo.Start();
        }
        #endregion

        #region createLabel
        private Label createLabel(int idx)
        {
            Label lbl = new Label();
            lbl.Location = new System.Drawing.Point(5 + idx * 2, 17);
            lbl.Size = new System.Drawing.Size(1, 1);
            lbl.Text = "lbl" + idx;
            return lbl;
        }
        #endregion

        #region timSystemInfo_Tick
        private void timSystemInfo_Tick(object sender, EventArgs e)
        {
            getCpuInfo();
        }
        #endregion


        /// <summary>
        /// CPU情報の取得
        /// </summary>
        private void getCpuInfo()
        {
            //パーセンテージの判定
            double cpurate = cpu.getCpuRate();

            //gageList[0].

            this.Refresh();
        }
    }
}
