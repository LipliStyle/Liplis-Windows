//=======================================================================
//  ClassName : ObjWidgetSetting
//  概要      : ウィジェット設定オブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Reflection;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Xml;
using System.Drawing;
using System.Windows.Forms;


namespace Liplis.Msg
{
    [Serializable]
    public class ObjWidgetSetting
    {
        ///=============================
        ///プロパティ
        public Color widgetColorUp          { get; set; }       //ウィジェットカラー上
        public Color widgetColorUnder       { get; set; }       //ウェイジェトカラー下
        public Color widgetForeColor        { get; set; }       //ウェイジェト文字カラー
        public Color widgetLinkColor        { get; set; }       //ウェイジェト文字カラー
        public Color widgetTitleColor       { get; set; }       //ウェイジェトタイトルカラー
        public double opacity               { get; set; }       //オパシティ
        public bool ctlRock                 { get; set; }       //オパシティ
        public int interval                 { get; set; }       //更新インターバル

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ObjWidgetSetting
        public ObjWidgetSetting()
        {
            widgetColorUp       = Color.Gold;
            widgetColorUnder    = Color.White;
            widgetForeColor     = Color.Black;
            widgetLinkColor     = Color.Blue;
            widgetTitleColor    = Color.Gold;
            opacity             = 0.75;
            ctlRock             = false;
            interval            = 5000;
        }
        #endregion

        /// <summary>
        /// checkOpacity
        /// オパシティをチェックする
        /// </summary>
        /// <returns></returns>
        #region checkOpacity
        public void checkOpacity()
        {
            if (this.opacity > 1.0)
            {
                opacity = 1.0;
                MessageBox.Show("透明度は0.25～1.0の範囲で入力してください", "Liplis");
            }
            if (this.opacity < 0.25)
            {
                opacity = 0.25;
                MessageBox.Show("透明度は0.25～1.0の範囲で入力してください", "Liplis");
            }
        }
        public void checkOpacity(string opa)
        {
            try
            {
                opacity = double.Parse(opa);
            }
            catch
            {
                MessageBox.Show("透明度は0.25～1.0の範囲で入力してください", "Liplis");
            }
        }
        #endregion

        /// <summary>
        /// getBroserInterval
        /// ブラウザの更新間隔を生成する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region getBroserInterval
        internal void setBroserInterval(string txt)
        {
            try
            {
                switch (txt)
                {
                    case "5  秒":
                        this.interval = 5000;
                        break;
                    case "10秒":
                        this.interval = 10000;
                        break;
                    case "30秒":
                        this.interval = 30000;
                        break;
                    case "1  分":
                        this.interval = 60000;
                        break;
                    case "2  分":
                        this.interval = 120000;
                        break;
                    case "3  分":
                        this.interval = 180000;
                        break;
                    case "4  分":
                        this.interval = 240000;
                        break;
                    case "5  分":
                        this.interval = 300000;
                        break;
                    case "10分":
                        this.interval = 600000;
                        break;
                    case "30分":
                        this.interval = 1800000;
                        break;
                    case "1  時間":
                        this.interval = 3600000;
                        break;
                    default:
                        this.interval = 600000;
                        break;
                }
            }
            catch
            {
                //デフォルトは10分
                this.interval = 600000;
            }
        }
        #endregion

        /// <summary>
        /// getBroserInterval
        /// ブラウザの更新間隔を取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region getBroserInterval
        internal int getBroserInterval()
        {
            try
            {
                switch (this.interval)
                {
                    case 5000:
                        return 0;
                    case 10000:
                        return 1;
                    case 30000:
                        return 2;
                    case 60000:
                        return 3;
                    case 120000:
                        return 4;
                    case 180000:
                        return 5;
                    case 240000:
                        return 6;
                    case 300000:
                        return 7;
                    case 600000:
                        return 8;
                    case 1800000:
                        return 9;
                    case 3600000:
                        return 10;
                    default:
                        return 4;
                }
            }
            catch
            {
                //デフォルトは10分
                return 600000;
            }
        }
        internal int getBroserInterval2()
        {
            try
            {
                if(this.interval <= 5000)
                {
                    return 60000;
                }
                return interval;
            }
            catch
            {
                //デフォルトは10分
                return 600000;
            }
        }
        #endregion


                /// <summary>
        /// getBroserInterval
        /// ブラウザの更新間隔を取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region getOpa
        internal int getOpa()
        {
            try
            {
                switch (this.interval)
                {
                    case 5000:
                        return 0;
                    case 10000:
                        return 1;
                    case 30000:
                        return 2;
                    case 60000:
                        return 3;
                    case 120000:
                        return 4;
                    default:
                        return 4;
                }
            }
            catch
            {
                //デフォルトは10分
                return 600000;
            }
        }
        #endregion

        
    }
}
