//=======================================================================
//  ClassName : frmBase
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

namespace Liplis.Widget
{
    public partial class WidgetSysBase : WidgetBaseBase
    {
        ///=============================
        ///クラス
        TranserateInfoClass tic;
        MemoryInfoClass mem;
        CpuInfo cpu;

        ///=============================
        ///マックススピード補正
        int maxSpeedFix = 10;

        public WidgetSysBase(int interFaseNum)
        {
            InitializeComponent();
            init(interFaseNum);
        }

        private void init(int interFaseNum)
        {
            //CPU情報取得クラス
            cpu = new CpuInfo();

            //メモリ情報取得クラス
            mem = new MemoryInfoClass();

            //取得モニタリングネットワーク情報
            tic = new TranserateInfoClass(interFaseNum);

            //マックススピードの取得
            NetworkInfoClass nic = new NetworkInfoClass();
            prgReciv.Maximum = (int)(nic.getTargetNicMaxSpeed(interFaseNum) / maxSpeedFix);
            prgReciv.Minimum = 0;
            prgSend.Maximum = (int)(nic.getTargetNicMaxSpeed(interFaseNum) / maxSpeedFix);
            prgSend.Minimum = 0;

            //マックスメモリーの取得
            prgMem.Maximum = 100;
            prgMem.Minimum = 0;

            //マックスCPUの設定
            prgCpuRate.Maximum = 100;
            prgCpuRate.Minimum = 0;

            timSystemInfo.Start();
        }


        /// <summary>
        /// システム情報取得タイマー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timSystemInfo_Tick(object sender, EventArgs e)
        {
            getNetworkInfo();
            getMemInfo();
            getCpuInfo();
            this.Refresh();
        }

        /// <summary>
        /// 転送量の取得
        /// </summary>
        private void getNetworkInfo()
        {
            //ネットワーク情報の取得
            tic.getNetWorkIntaerfaseInfo();
            this.lblReceiveByte.Text = tic.getReceiveByte().ToString();
            this.lblSentByte.Text = tic.getSentByte().ToString();

            //プログレスバーの設定
            if (prgReciv.Maximum >= tic.getReceiveByte() && prgReciv.Minimum <= tic.getReceiveByte())
            {
                prgReciv.Value = (int)tic.getReceiveByte();
            }

            if (prgSend.Maximum >= tic.getSentByte() && prgSend.Minimum <= tic.getSentByte())
            {
                prgSend.Value = (int)tic.getSentByte();
            }

        }


        /// <summary>
        /// メモリ情報の取得
        /// </summary>
        private void getMemInfo()
        {
            mem.getMemoryInfo();
            //パーセンテージの判定
            if ((int)mem.getPhysicalRate() > 80)
            {
                prgMem.ForeColor = Color.FromArgb(255, 0, 0);
            }
            else if ((int)mem.getPhysicalRate() > 50)
            {
                prgMem.ForeColor = Color.FromArgb(255, 255, 0);
            }
            else
            {
                prgMem.ForeColor = Color.FromArgb(0, 255, 0);
            }
            lblMem.Text = mem.getPhysicalUseabel() + " / " + mem.getPhysicalAll();
            prgMem.Value = (int)mem.getPhysicalRate();
        }


        /// <summary>
        /// CPU情報の取得
        /// </summary>
        private void getCpuInfo()
        {
            //パーセンテージの判定
            double cpurate = cpu.getCpuRate();
            if (cpurate > 90)
            {
                prgCpuRate.ForeColor = Color.FromArgb(255, 0, 0);
            }
            else if ((cpurate > 50))
            {
                prgCpuRate.ForeColor = Color.FromArgb(255, 255, 0);
            }
            else
            {
                prgCpuRate.ForeColor = Color.FromArgb(0, 255, 0);
            }
            lblCpuRate.Text = cpurate.ToString() + "%";
            prgCpuRate.Value = (int)cpurate;
            this.Refresh();
        }


        /// <summary>
        /// 親クラスからメモリー容量を取得する
        /// </summary>
        /// <returns></returns>
        public uint getUseabelMemory()
        {
            mem.getMemoryInfo();
            return (uint)mem.getPhysicalUseabelNonFix();
        }
    }
}
