//=======================================================================
//  ClassName : msgEmotion
//  概要      : エモーション
//
//  LiplisSystemシステム      
//  Copyright(c) 2010-2014 sachin. All Rights Reserved. 
//=======================================================================

using System;
namespace Liplis.Msg
{
    public class MsgEmotion
    {
        ///=====================================
        /// ウインドウのリスト
        #region プロパティ
        public string name { get; set; }            //ワード
        public int joy { get; set; }                //たのしい
        public int admiration { get; set; }         //好意
        public int peace { get; set; }              //安心
        public int ecstasy { get; set; }            //快感」
        public int amazement { get; set; }          //驚き
        public int rage { get; set; }               //怒り
        public int interest { get; set; }           //興味
        public int respect { get; set; }            //尊敬
        public int calmly { get; set; }             //冷静
        public int proud { get; set; }              //ほこり

        #endregion

        /// <summary>
        /// デフォルトコンストラクター
        /// </summary>
        public MsgEmotion()
        {
            this.name = "";
            init();
        }
        public MsgEmotion(string name, int joy, int admiration, int peace, int ecstasy, int amazement, int rage, int interest, int respect, int calmly, int proud)
        {
            this.name = name;
            this.joy = joy;
            this.admiration = admiration;
            this.peace = peace;
            this.ecstasy = ecstasy;
            this.amazement = amazement;
            this.rage = rage;
            this.interest = interest;
            this.respect = respect;
            this.calmly = calmly;
            this.proud = proud;
        }

        /// <summary>
        /// エモーション値の初期化
        /// </summary>
        public void init()
        {
            this.joy = 0;
            this.admiration = 0;
            this.peace = 0;
            this.ecstasy = 0;
            this.amazement = 0;
            this.rage = 0;
            this.interest = 0;
            this.respect = 0;
            this.calmly = 0;
            this.proud = 0;
        }

        /// <summary>
        /// エモーションをセットする
        /// </summary>
        /// <param name="emotion"></param>
        /// <param name="point"></param>
        public void set(int emotion, int point)
        {
            switch (emotion)
            {
                case 0:
                    break;
                case 1:
                    joy += point;
                    break;
                case 2:
                    admiration += point;
                    break;
                case 3:
                    peace += point;
                    break;
                case 4:
                    ecstasy += point;
                    break;
                case 5:
                    amazement += point;
                    break;
                case 6:
                    rage += point;
                    break;
                case 7:
                    interest += point;
                    break;
                case 8:
                    respect += point;
                    break;
                case 9:
                    calmly += point;
                    break;
                case 10:
                    proud += point;
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 合計を返す
        /// </summary>
        /// <returns></returns>
        public int summary()
        {
            return Math.Abs(joy) + Math.Abs(admiration) + Math.Abs(peace) + Math.Abs(ecstasy) + Math.Abs(amazement) + Math.Abs(rage) + Math.Abs(interest) + Math.Abs(respect) + Math.Abs(calmly) + Math.Abs(proud);
        }

        /// <summary>
        /// 全て０ならトウルー
        /// </summary>
        /// <returns></returns>
        public bool checkAllZero()
        {
            return joy == 0 && admiration == 0 && peace == 0 && ecstasy == 0 && amazement == 0 && rage == 0 && interest == 0 && respect == 0 && calmly == 0 && proud == 0;
        }

        /// <summary>
        /// MAXを倍加する
        /// </summary>
        public void maxDoubelation()
        {
            //MAX値を倍加する
            double max = 0;
            int emotion = 0;
            if (Math.Abs(max) <= Math.Abs(joy)) { emotion = 1; max = joy; }
            if (Math.Abs(max) <= Math.Abs(admiration)) { emotion = 2; max = admiration; }
            if (Math.Abs(max) <= Math.Abs(peace)) { emotion = 3; max = peace; }
            if (Math.Abs(max) <= Math.Abs(ecstasy)) { emotion = 4; max = ecstasy; }
            if (Math.Abs(max) <= Math.Abs(amazement)) { emotion = 5; max = amazement; }
            if (Math.Abs(max) <= Math.Abs(rage)) { emotion = 6; max = rage; }
            if (Math.Abs(max) <= Math.Abs(interest)) { emotion = 7; max = interest; }
            if (Math.Abs(max) <= Math.Abs(respect)) { emotion = 8; max = respect; }
            if (Math.Abs(max) <= Math.Abs(calmly)) { emotion = 9; max = calmly; }
            if (Math.Abs(max) <= Math.Abs(proud)) { emotion = 10; max = proud; }

            switch (emotion)
            {
                case 0:
                    break;
                case 1:
                    joy *= 2;
                    break;
                case 2:
                    admiration *= 2;
                    break;
                case 3:
                    peace *= 2;
                    break;
                case 4:
                    ecstasy *= 2;
                    break;
                case 5:
                    amazement *= 2;
                    break;
                case 6:
                    rage *= 2;
                    break;
                case 7:
                    interest *= 2;
                    break;
                case 8:
                    respect *= 2;
                    break;
                case 9:
                    calmly *= 2;
                    break;
                case 10:
                    proud *= 2;
                    break;
                default:
                    break;
            }
        }
    }
}
