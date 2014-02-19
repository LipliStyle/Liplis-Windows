//=======================================================================
//  ClassName : ActivityLog
//  概要      : ログ管理画面。
//              ログの管理もおまかせ
//
//  Liplis2.3
//  2013/06/20 Liplis2.3.0 UI変更
//  Copyright(c) 2010-2013 LipliStyle.Sachin
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
    public partial class ActivityLog : BaseSystem
    {
        ///=====================================
        /// リプリス
        private Liplis.MainSystem.Liplis lips;
        private ObjSetting os;
        private ObjWindowFile owf;

        ///=====================================
        /// ほうき
        private ObjBroom obr;

        ///=====================================
        /// フラグ
        private bool flgEnd = false;

        ///=====================================
        /// 前回値
        private string prvUrl = " ";

        ///====================================================================
        ///
        ///                             onCreate
        ///                         
        ///====================================================================

        //コンストラクター
        #region ActivitySetting : base()
        public ActivityLog(Liplis.MainSystem.Liplis lips,ObjSetting os, ObjBroom obr, ObjWindowFile owf)
            : base()
        {
            this.lips = lips;
            this.os   = os;
            this.obr  = obr;
            this.owf  = owf;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            initSettingWindow();
        }
        #endregion

        /// <summary>
        /// initSettingWindow
        /// initSettingWindowの初期化
        /// </summary>
        #region initSettingWindow
        private void initSettingWindow()
        {
            this.components = new System.ComponentModel.Container();

            //サイズを設定する
            setSize(490, 500);

            //オパシティ1
            this.Opacity = 1;
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
        #region ActivityRss_FormClosing
        private void ActivityLog_FormClosing(object sender, FormClosingEventArgs e)
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
        /// ロードする
        /// </summary>
        #region ActivityLog_Load
        private void ActivityLog_Load(object sender, EventArgs e)
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
        /// ActivityLog_MouseDown
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityLog_MouseDown
        private void ActivityLog_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown(e);
        }
        #endregion

        /// <summary>
        /// ActivityLog_MouseMove
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityLog_MouseMove
        private void ActivityLog_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMove(e);
        }
        #endregion

        /// <summary>
        /// btnClose_Click
        /// 閉じるボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion

        /// <summary>
        /// tsmiTitleCopy_Click
        /// タイトルコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiTitleCopy_Click
        private void tsmiTitleCopy_Click(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// tsmiLinkCopy_Click
        /// リンクコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiLinkCopy_Click
        private void tsmiLinkCopy_Click(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// tsmiShowNews_Click
        /// ニュース参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiShowNews_Click
        private void tsmiShowNews_Click(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// tsmiCmsNewsCopy_Click
        /// 本文 ニュースコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiCmsNewsCopy_Click
        private void tsmiCmsNewsCopy_Click(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// tsmiCmsShowNews_Click
        /// 本文 ニュース参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region tsmiCmsShowNews_Click
        private void tsmiCmsShowNews_Click(object sender, EventArgs e)
        {

        }
        #endregion

        ///====================================================================
        ///
        ///                           PanelOnRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// mouseEnter
        /// マウスが上に来たときにはFLPにフォーカスする
        /// (スクロール対策)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region mouseEnter
        private void mouseEnter(object sender, EventArgs e)
        {
            this.flp.Focus();
        }
        #endregion

        ///====================================================================
        ///
        ///                           ログの追加
        ///                         
        ///====================================================================

        /// <summary>
        /// addLog
        /// ログの追加
        /// </summary>
        #region addLog
        public void addLog(MsgShortNews msg, Bitmap charBody)
        {
            addPanel(msg.url, msg.title, msg.sorce, msg.jpgUrl, msg.newsEmotion, msg.newsPoint, charBody);
        }
        #endregion

        /// <summary>
        /// addLog
        /// ログの追加
        /// </summary>
        #region addPanel
        private void addPanel(string url, string title, string discription, string jpgPath, int newsEmotion, int newsPoint, Bitmap charBody)
        {
            //前回値と同じなら登録しない
            if (url.Equals(prvUrl)){return;}

            //データパネル
            CusCtlDataPanel d;

            //前回値上書き
            prvUrl = url;

            //500件目の破棄
            if(flp.Controls.Count >= 500)
            {
                //500件目のパネルを取得
                using (CusCtlDataPanel dc = (CusCtlDataPanel)flp.Controls[499])
                {
                    try
                    {
                        //ごみすて
                        obr.deleteTargetFile(dc.jpgPath);

                        //破棄
                        dc.dispose();

                        //flpから追放
                        flp.Controls.RemoveAt(499);
                    }
                    catch
                    {

                    }
                }                
            }

            //新規要素の追加
            if (!jpgPath.Equals(""))
            {
                d = new CusCtlDataPanel(lips, os, url, title, discription, jpgPath, newsEmotion, newsPoint, charBody, new System.EventHandler(this.mouseEnter), this.components);
            }
            else
            {
                d = new CusCtlDataPanelNonThum(url, title, discription, newsEmotion, newsPoint, charBody, new System.EventHandler(this.mouseEnter), this.components);
                
            }

            //アッドする
            flp.Controls.Add(d);
            flp.Controls.SetChildIndex(d, 0);

            this.Refresh();
        }
        #endregion




    }
}
