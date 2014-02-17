//=======================================================================
//  ClassName : LiplisTaskBar
//  概要      : タスクバーオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Windows.Forms;
using Liplis.Common;

namespace Liplis.MainSystem
{
    public partial class LiplisTaskBar : Form
    {
        ///=====================================
        /// オブジェクト
        private Liplis lips;

        ///====================================================================
        ///
        ///                         onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// LiplisTaskBar
        /// コンストラクター
        /// </summary>
        /// <param name="lips"></param>
        #region LiplisTaskBar
        public LiplisTaskBar(Liplis lips)
        {
            InitializeComponent();
            this.lips = lips;
            initTaskBar();
        }
        #endregion

        /// <summary>
        /// タスクバーの初期化
        /// </summary>
        #region initTaskBar
        private void initTaskBar()
        {
            this.Opacity = 0;
            //this.AddOwnedForm(lips);
            //this.AddOwnedForm(lips.getAt());
        }
        #endregion

        ///====================================================================
        ///
        ///                         onLoad
        ///                         
        ///====================================================================

        /// <summary>
        /// LiplisTaskBar_Load
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region LiplisTaskBar_Load
        private void LiplisTaskBar_Load(object sender, EventArgs e)
        {

        }
        #endregion

        ///====================================================================
        ///
        ///                         onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// メッセージ制御
        /// </summary>
        /// <param name="m">ウインドウズメッセージ</param>
        #region WndProc
        protected override void WndProc(ref Message m)
        {
            //--- サイズ変更の制御 ---
            if (m.Msg == LpsWindowsApiDefine.WM_SIZE)
            {
                //WParamの値を評価
                switch ((int)m.WParam)
                {
                    case LpsWindowsApiDefine.SIZE_RESTORED:
                        lips.onRecive(LiplisDefine.LM_NORMALIZE, "");
                        return;
                    case LpsWindowsApiDefine.SIZE_MINIMIZED:
                        lips.onRecive(LiplisDefine.LM_MINIMIZE, "");
                        return;
                    case LpsWindowsApiDefine.SIZE_MAXIMIZED:     //最大化はリターン
                        return;
                    case LpsWindowsApiDefine.SIZE_MAXSHOW:
                        break;
                    case LpsWindowsApiDefine.SIZE_MAXHIDE:
                        break;
                }
            }

            base.WndProc(ref m);
        }
        #endregion

        #region tsmSleep_Click
        private void tsmSleep_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_SLEEP, "");
        }
        #endregion
        #region tsmMinimize_Click
        private void tsmMinimize_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_MINIMIZE, "task");
        }
        #endregion
        #region tsmLog_Click
        private void tsmLog_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_LOG, "");
        }
        #endregion
        #region tsmSetting_Click
        private void tsmSetting_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_SETTING, "");
        }
        #endregion
        #region tsmEnd_Click
        private void tsmEnd_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_END, "");
        }
        #endregion
        #region tsmOutrangeRecovery_Click
        private void tsmOutrangeRecovery_Click(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_OUTRANGE_RECOVERY, "");
        }
        #endregion
        #region onNormalize
        public void onNormalize()
        {
            this.WindowState = FormWindowState.Normal;
        }
        #endregion
        #region onMinimize
        public void onMinimize()
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion
        /// <summary>
        /// 終了処理のフック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region LiplisTaskBar_FormClosing
        private void LiplisTaskBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lips.getFlgEnd())
            {

            }
            else
            {
                e.Cancel = true;
                lips.onRecive(LiplisDefine.LM_END, "");
            }
        }
        #endregion



    }
}
