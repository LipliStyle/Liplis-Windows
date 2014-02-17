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
using Liplis.Msg;
using Liplis.Control;
using Liplis.Common;

namespace Liplis.Widget.WidCpu
{
    public partial class WidgetCpuBase : WidgetBaseBase
    {
        ///=============================
        ///クラス
        private CpuInfo cpu;
        private List<CusCtlLabel> gageList;
        private ObjWidgetSetting o;

        ///=============================
        /// 定数
        private const int HONSU = 60;

        ///====================================================================
        ///
        ///                       onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// WidgetCpu02Base
        /// 
        /// </summary>
        #region WidgetCpuBase
        public WidgetCpuBase(ObjWidgetSetting o)
        {
            this.o = o;
            InitializeComponent();
            initWindow();
            init();
        }
        #endregion

        /// <summary>
        /// initWindow
        /// ウインドウの初期化
        /// </summary>
        #region initWindow
        private void initWindow()
        {
            this.lblWidCpuTitle.ForeColor = o.widgetTitleColor;
            this.lblWidCpuRate.ForeColor  = o.widgetForeColor;
            this.prg.Maximum              = 100;
            this.prg.Minimum              = 0;
            this.prg.Value                = 0;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);

            this.g.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.picWidCpuIco.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblWidCpuTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.prg.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblWidCpuRate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);

        }
        #endregion

        /// <summary>
        /// init
        /// 初期化
        /// </summary>
        #region init
        private void init()
        {
            //CPU情報取得クラス
            cpu = new CpuInfo();

            gageList = new List<CusCtlLabel>();

            for (int idx = 0; idx < HONSU; idx++)
            {
                CusCtlLabel lbl = createLabel(idx);
                gageList.Add(lbl);
                
            }

            timSystemInfo.Start();
        }
        #endregion

        #region createLabel
        private CusCtlLabel createLabel(int idx)
        {
            //Label lbl    = new Label();
            CusCtlLabel lbl = new CusCtlLabel();
            lbl.Location = new System.Drawing.Point(36 + idx * 2,43);
            lbl.Size     = new System.Drawing.Size(1,0);
            lbl.Name = "lbl" + idx;
            lbl.BackColor = Color.Lime;
            lbl.AutoSize = false;
            this.g.Controls.Add(lbl);
            return lbl;
        }
        #endregion


        ///====================================================================
        ///
        ///                           onRecive
        ///                         
        ///====================================================================


        /// <summary>
        /// timSystemInfo_Tick
        /// タイマー
        /// </summary>
        #region timSystemInfo_Tick
        private void timSystemInfo_Tick(object sender, EventArgs e)
        {
            getCpuInfo();
        }
        #endregion


        /// <summary>
        /// CPU情報の取得
        /// </summary>
        #region getCpuInfo
        private void getCpuInfo()
        {
            //パーセンテージの判定
            double cpurate = cpu.getCpuRate();
            double cpuHi = cpurate / 5;

            for (int idx = 1; idx <= HONSU - 1; idx++)
            {
                gageList[idx - 1].Height = gageList[idx].Height;
                gageList[idx - 1].Top = gageList[idx].Top;
            }

            gageList[HONSU -1].Height = (int)cpuHi;
            gageList[HONSU - 1].Top = 43 - gageList[HONSU - 1].Height;

            lblWidCpuRate.Text = cpurate.ToString("#0.00") + "%";
            try{this.prg.Value = (int)cpurate;}catch{}            

            this.Refresh();
        }
        #endregion

        /// <summary>
        /// マウスムーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region マウスムーブ
        private void lblWidCpuRate_MouseDown(object sender, MouseEventArgs e) { mouseDown(e); }
        private void lblWidCpuRate_MouseMove(object sender, MouseEventArgs e) { mouseMoveWidget(e); }
        #endregion



    }
}
