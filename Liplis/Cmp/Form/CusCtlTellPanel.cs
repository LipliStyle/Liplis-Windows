//=======================================================================
//  ClassName : CusCtlDataPanel
//  概要      : カスタムデータパネル(サムネイルあり)
//
//  Liplis2.3
//  2013/06/20 Liplis2.3.0 UI変更
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.ComponentModel;
using System.Drawing;
using Liplis.Control;
using Liplis.Msg;

namespace Liplis.Cmp.Form
{
    public class CusCtlTellPanel : CusCtlDataPanel
    {
        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region DataPanel
        public CusCtlTellPanel(Liplis.MainSystem.Liplis lips,ObjSetting os, string discription, IContainer components)
        {
            this.lips = lips;
            this.os = os;
            initDataPanel(discription);
        }
        public CusCtlTellPanel()
        {
        }
        #endregion


        /// <summary>
        /// データパネルの初期化
        /// </summary>
        #region initDataPanel
        protected void initDataPanel(string discription)
        {
            //要素の取得
            this.discription = discription;;

            //初期化
            this.lnkLbl = new CusCtlLinkLabel();
            this.lblEmotion = new CusCtlLabel();
            this.lblPoint   = new CusCtlLabel();

            // 
            // panel
            // 
            this.Controls.Add(this.lnkLbl);
            this.Controls.Add(this.lblPoint);
            this.Controls.Add(this.lblEmotion);

            this.Location = new System.Drawing.Point(3, 3);
            this.Name = "panel";
            this.Size = new System.Drawing.Size(445, 30);
            this.TabIndex = 0;

            // 
            // lnkLbl
            // 
            this.lnkLbl.AutoSize = false;
            this.lnkLbl.Location = new System.Drawing.Point(85, 0);
            this.lnkLbl.Name = "lnkLbl";
            this.lnkLbl.Size = new System.Drawing.Size(360, 30);
            this.lnkLbl.TabIndex = 1;
            this.lnkLbl.TabStop = true;
            this.lnkLbl.Text = discription;
            this.lnkLbl.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkLbl.BackColor = Color.AliceBlue;
        }
        #endregion

        ///====================================================================
        ///
        ///                           onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// 破棄処理
        /// </summary>
        #region dispose
        public virtual void dispose()
        {
            //lblText.Dispose();
            lnkLbl.Dispose();
            this.Dispose();
        }
        #endregion

        ///====================================================================
        ///
        ///                           処理メソッド
        ///                         
        ///====================================================================

    }
}
