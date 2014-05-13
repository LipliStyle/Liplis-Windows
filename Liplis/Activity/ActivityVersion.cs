using System.Diagnostics;
//=======================================================================
//  ClassName : ActivityVersion
//  概要      : バージョンアクティヴィティ
//
//  Liplis2.3
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System.Reflection;
using System.Windows.Forms;
using Liplis.Common;

namespace Liplis.Activity
{
    public partial class ActivityVersion :Form
    {
        ///====================================================================
        ///
        ///                       onCreate
        ///                         
        ///====================================================================

        //コンストラクター
        #region ActivityVersion : base()
        public ActivityVersion(int centerX, int centerY)
            : base()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(centerX-this.Width/2, centerY - this.Height/2);
            this.lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        #endregion

        ///====================================================================
        ///
        ///                       onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// btnClose_Click
        /// </summary>
        #region btnClose_Click
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// バージョンチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnVersion_Click
        private void btnVersion_Click(object sender, System.EventArgs e)
        {
            runUpdate();
        }
        #endregion
        



        ///====================================================================
        ///
        ///                              onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// onDelete
        /// </summary>
        #region Dispose
        public void dispose()
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// onDelete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityRss_FormClosing
        private void ActivitySetting_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        #endregion

        /// <summary>
        /// アップデートプログラム起動
        /// </summary>
        #region runUpdate
        private void runUpdate()
        {
            if (LpsPathController.checkFileExist(LpsPathController.getUpdatePrgPath()))
            {
                Process p = Process.Start(LpsPathController.getUpdatePrgPath());
            }

        }
        #endregion

    }
}
