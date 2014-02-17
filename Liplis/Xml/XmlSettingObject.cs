//=======================================================================
//  ClassName : XmlSettingObject
//  概要      : 設定ファイルオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liplis.Common;

namespace Liplis.Xml
{
    [Serializable]
    public abstract class XmlSettingObject
    {
        ///=============================
        ///クラス
        protected SharedPreferences setting;
        protected LpsLogControllerCus lc;

        //===========================================================
        //　　　　　　　　　　　実装メソッド
        //===========================================================

        /// <summary>
        /// プリファレンスの取得
        /// </summary>
        public abstract void getPreferenceData();

        /// <summary>
        /// プリファレンスの設定
        /// </summary>
        public abstract void setPreferenceData();

        //===========================================================
        //　　　　　　　　　　　チェッカー
        //===========================================================


        /// <summary>
        /// ビット値のチェック
        /// </summary>
        #region bitCheck
        protected int bitCheck(int bit)
        {
            if (bit == 1 || bit == 0)
            {
                return bit;
            }
            else
            {
                return 0;
            }
        }
        #endregion

    }
}
