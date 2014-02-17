//=======================================================================
//  ClassName : ActivityChar
//  概要      : アクティビティキャラクター
//
//  Liplis2.3
//  2013/06/20 Liplis2.3.0 UI変更
//  Copyright(c) 2010-2013 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using Liplis.Cmp.Form;
using Liplis.Common;
using Liplis.MainSystem;
using Liplis.Msg;

namespace Liplis.Activity
{
    public partial class ActivityChar : BaseSystem
    {
        ///=====================================
        /// フラグ
        private bool flgEnd             = false;
        private string selectedCharName = "";

        ///=====================================
        /// liplis
        private MainSystem.Liplis lips;
        private ObjWindowFile owf;

        ///=====================================
        /// liplis
        ObjSkinSettingList ossList;


        ///====================================================================
        ///
        ///                             onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ActivityChar : base()
        public ActivityChar(MainSystem.Liplis lips, ObjSkinSettingList ossList, string selectedCharName, ObjWindowFile owf)
            : base()
        {
            InitializeComponent();
            this.lips             = lips;
            this.ossList          = ossList;
            this.owf              = owf;
            this.selectedCharName = selectedCharName;
            this.StartPosition    = FormStartPosition.CenterScreen;
            initSettingWindow();
            initCharList();
        }
        #endregion

        /// <summary>
        /// initSettingWindow
        /// Windowの初期化
        /// </summary>
        #region initSettingWindow
        private void initSettingWindow()
        {
            //サイズを設定する
            setSize(410, 460);

            //オパシティ1
            this.Opacity = 1;
        }
        #endregion

        /// <summary>
        /// initCharList
        /// キャラクターリストの初期化
        /// </summary>
        #region initCharList
        private void initCharList()
        {
            //OSSリストをまわしてパネルを作成する
            foreach (ObjSkinSetting oss in ossList.ossList)
            {
                 addPanel(oss,oss.charName.Equals(selectedCharName));
            }
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
            flgEnd = true;
            this.Close();
        }
        #endregion

        /// <summary>
        /// onDelete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityChar_FormClosing
        private void ActivityChar_FormClosing(object sender, FormClosingEventArgs e)
        {
            //エンドフラグが有効でなければ、ハイドさせる
            if (!flgEnd)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                             onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// ActivityChar_Load
        /// onLoad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityChar_Load
        private void ActivityChar_Load(object sender, EventArgs e)
        {
            setBackgournd();
        }
        #endregion

        /// <summary>
        /// setBackgournd
        /// 背景をセットする
        /// </summary>
        #region setBackgournd
        private void setBackgournd()
        {
            this.BackgroundImage = owf.bt_setting;
        }
        #endregion

        /// <summary>
        /// 通常化
        /// </summary>
        #region onNormalize
        public void onNormalize()
        {
            this.WindowState = FormWindowState.Normal;
        }
        #endregion

        /// <summary>
        /// 最小化
        /// </summary>
        #region onMinimize
        public void onMinimize()
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        /// <summary>
        /// ActivityChar_MouseDown
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityChar_MouseDown
        private void ActivityChar_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown(e);
        }
        #endregion

        /// <summary>
        /// ActivityChar_MouseMove
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityChar_MouseMove
        private void ActivityChar_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMove(e);
        }
        #endregion

        /// <summary>
        /// btnClose_Click
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion

        ///====================================================================
        ///
        ///                          パネル操作
        ///                         
        ///====================================================================

        /// <summary>
        /// addLog
        /// パネルの追加
        /// </summary>
        #region addPanel
        private void addPanel(ObjSkinSetting oss, bool select)
        {
            //新規要素の追加
            CharPanel d = new CharPanel(lips, oss, select);
            flp.Controls.Add(d);
            this.Refresh();
        }
        #endregion



    }
}
