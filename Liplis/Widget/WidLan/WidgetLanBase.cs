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

namespace Liplis.Widget.WidLan
{
    public partial class WidgetLanBase : WidgetBaseBase
    {
        ///=============================
        ///クラス
        private TranserateInfoClass tic;
        private List<CusCtlLabel> gageRcvList;
        private List<double>      gageRcvValueList;
        private List<CusCtlLabel> gageSndList;
        private List<double>      gageSndValueList;
        private ObjWidgetSetting o;

        ///=============================
        /// 定数
        private const int HONSU = 60;

        ///=============================
        /// スピード
        private double rcvMaxMax = 10;
        private double rcvMax = 10;
        private double rcvMin = 0;
        private double sndMaxMax = 10;
        private double sndMax = 10;
        private double sndAvg = 10;
        private double sndMin = 0;

        ///====================================================================
        ///
        ///                       onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// WidgetLanBase
        /// 
        /// </summary>
        #region WidgetLanBase
        public WidgetLanBase(ObjWidgetSetting o, WidgetLanSetting s)
        {
            this.o = o;
            InitializeComponent();
            initWindow();
            init(s);
        }
        #endregion

        /// <summary>
        /// initWindow
        /// ウインドウの初期化
        /// </summary>
        #region initWindow
        private void initWindow()
        {
            this.lblWidMemTitle.ForeColor    = o.widgetTitleColor;
            this.lblWidLanRateRcv.ForeColor  = o.widgetForeColor;
            this.lblWidLanRateSnd.ForeColor  = o.widgetForeColor;

            this.g.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblWidMemTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblWidLanRateSnd.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblWidLanRateRcv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
        }
        #endregion

        /// <summary>
        /// init
        /// 初期化
        /// </summary>
        #region init
        private void init(WidgetLanSetting s)
        {
            this.lblWidMemTitle.Text = s.interfaceName;

            //取得モニタリングネットワーク情報
            tic              = new TranserateInfoClass(s.interfaceNum);
            gageRcvList      = new List<CusCtlLabel>();
            gageSndList      = new List<CusCtlLabel>();
            gageRcvValueList = new List<double>();
            gageSndValueList = new List<double>();

            //マックススピードの取得
            NetworkInfoClass nic = new NetworkInfoClass();
            rcvMaxMax            = (int)(nic.getTargetNicMaxSpeed(s.interfaceNum) / 1000);
            rcvMax               = rcvMaxMax;
            rcvMin               = 0;
            sndMaxMax            = (int)(nic.getTargetNicMaxSpeed(s.interfaceNum) / 1000);
            sndMax               = sndMaxMax;
            sndMin               = 0;

            for (int idx = 0; idx < HONSU; idx++)
            {
                CusCtlLabel lbl = createLabel(idx);
                CusCtlLabel lbl2 = createLabel2(idx);
                gageRcvList.Add(lbl);
                gageSndList.Add(lbl2);
                gageRcvValueList.Add(0.0);
                gageSndValueList.Add(0.0);
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
        private CusCtlLabel createLabel2(int idx)
        {
            //Label lbl    = new Label();
            CusCtlLabel lbl = new CusCtlLabel();
            lbl.Location = new System.Drawing.Point(37 + idx * 2, 43);
            lbl.Size = new System.Drawing.Size(1, 0);
            lbl.Name = "lbl" + idx;
            lbl.BackColor = Color.Magenta;
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
            getLanInfo();
        }
        #endregion


        /// <summary>
        /// LAN情報の取得
        /// </summary>
        #region getMemInfo
        private void getLanInfo()
        {
            tic.getNetWorkIntaerfaseInfo();
            double rcv = tic.getReceiveByte();
            double snd = tic.getSentByte();


            this.lblWidLanRateRcv.Text = rcv.ToString("0.0") + "kbps";
            this.lblWidLanRateSnd.Text = snd.ToString("0.0") + "kbps";

            //値シフト
            for (int idx = 1; idx <= HONSU - 1; idx++)
            {
                gageRcvList[idx - 1].Height = gageRcvList[idx].Height;
                gageRcvList[idx - 1].Top    = gageRcvList[idx].Top;
                gageSndList[idx - 1].Height = gageSndList[idx].Height;
                gageSndList[idx - 1].Top    = gageSndList[idx].Top;

                if (gageRcvValueList[idx - 1] < rcv){rcvMax = rcv;}
                if (gageSndValueList[idx - 1] < snd){sndMax = snd;}
            }

            if (rcvMax < rcvMaxMax){rcvMax = rcvMaxMax;}
            if (sndMax < sndMaxMax){sndMax = sndMaxMax;}

            //高算出
            double lanRcvHi = rcv / rcvMax * 20;
            double lanSndHi = snd / sndMax * 20;

            //値入力
            gageRcvValueList[HONSU - 1] = rcv;
            gageSndValueList[HONSU - 1] = snd;
            

            gageRcvList[HONSU - 1].Height = (int)lanRcvHi;
            gageRcvList[HONSU - 1].Top = 43 - gageRcvList[HONSU - 1].Height;

            gageSndList[HONSU - 1].Height = (int)lanSndHi;
            gageSndList[HONSU - 1].Top = 43 - gageSndList[HONSU - 1].Height;

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
