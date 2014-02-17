//=======================================================================
//  ClassName : LiplisIconMini
//  概要      : アイコンミニ
//
//  Liplis3.0
//  Copyright(c) 2010-2013 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Msg;

namespace Liplis.MainSystem
{
    public partial class LiplisIconMini : LiplisIcon
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="lips"></param>
        #region コンストラクター
        public LiplisIconMini(Liplis lips)
            : base(lips)
        {

        }
        #endregion
        

        /// <summary>
        /// initLiplisIcon
        /// 初期化処理
        /// ☆Miniオーバーライド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region initLiplisIcon
        protected override void initLiplisIcon()
        {
            //画面サイズの調整と
            setWindowProperty(FctCreateBackground.createTransParentBgi(this.wid, this.hi));

            //サーフェスの設定
            this.btnNext.Top = 80;
            this.btnNext.Left = 5;

            this.btnSleep.Top = 125;
            this.btnSleep.Left = 5;

            this.btnLog.Top = 170;
            this.btnLog.Left = 5;

            this.btnMenu.Top = 215;
            this.btnMenu.Left = 5;

            this.btnChar.Top = 80;
            this.btnChar.Left = this.Width -37;

            this.btnBattery.Top = 125;
            this.btnBattery.Left = this.Width - 37;

            this.btnMinimize.Top = 170;
            this.btnMinimize.Left = this.Width - 37;

            this.btnEnd.Top = 215;
            this.btnEnd.Left = this.Width - 37;
        }
        #endregion
    }
}
