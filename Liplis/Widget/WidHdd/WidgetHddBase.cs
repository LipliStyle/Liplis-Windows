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
using System.IO;

namespace Liplis.Widget.WidHdd
{
    public partial class WidgetHddBase : WidgetBaseBase
    {
        ///=============================
        ///クラス
        private DriveInfo           di;
        private string              drive;
        private ObjWidgetSetting    o;
        private bool flgFaild;

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
        #region WidgetHddBase
        public WidgetHddBase(ObjWidgetSetting o, WidgetHddSetting s)
        {
            this.o = o;
            this.drive = s.drive;
            InitializeComponent();
            initWindow();
            init();
            getHddInfo();
        }
        #endregion

        /// <summary>
        /// initWindow
        /// ウインドウの初期化
        /// </summary>
        #region initWindow
        private void initWindow()
        {
            this.lblWidHddTitle.ForeColor = o.widgetTitleColor;
            this.lblWidHddVal.ForeColor  = o.widgetForeColor;
            this.prg.Maximum              = 100;
            this.prg.Minimum              = 0;
            this.prg.Value                = 0;

            this.g.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.picWidCpuIco.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblWidHddVal.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.prg.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblWidHddTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
        }
        #endregion

        /// <summary>
        /// init
        /// 初期化
        /// </summary>
        #region init
        private void init()
        {
            this.lblWidHddTitle.Text = drive;
            timSystemInfo.Start();
            flgFaild = false;
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
            if (!flgFaild)
            {
                getHddInfo();
            }
        }
        #endregion


        /// <summary>
        /// CPU情報の取得
        /// </summary>
        #region getHddInfo
        private void getHddInfo()
        {
            try
            {
                di = new DriveInfo(drive);
                double freeSpase = (double)(di.TotalSize - di.AvailableFreeSpace) / (double)di.TotalSize * 100.0;
                lblWidHddVal.Text = ((double)(di.TotalSize - di.AvailableFreeSpace)/1000000000.0).ToString("######.0") + "GB/" + (di.TotalSize / 1000000000.0).ToString("######.0") + "GB";
                prg.Value = (int)freeSpase;

                this.Refresh();
            }
            catch
            {
                timSystemInfo.Stop();
                flgFaild = true;
            }
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

        /// <summary>
        /// picWidCpuIco_Click
        /// ピクチャークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region picWidCpuIco_Click
        private void picWidCpuIco_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
