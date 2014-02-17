//=======================================================================
//  ClassName : ObjEmotion
//  概要      : エモーションオブジェクト
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Msg
{
    public class ObjEmotion
    {
        ///=============================
        ///プロパティ
	    private int joy        = 0;
        private int admiration = 0;
        private int peace      = 0;
        private int ecstasy    = 0;
        private int amazement  = 0;
        private int rage       = 0;
        private int interest   = 0;
        private int respect    = 0;
        private int calmly     = 0;
        private int proud      = 0;

        public int maxEmo   { get; set; }
        public int maxPoint { get; set; }

        /// <summary>
        /// ObjEmotion
        /// コンストラクター
        /// </summary>
        #region ObjEmotion
        public ObjEmotion()
        {

        }
        public ObjEmotion(List<int> emotionList, List<int> pointList)
        {
            culcEmotion(emotionList, pointList);
            getMaxEmotion();
        }
        #endregion

        /// <summary>
        /// culcEmotion
        /// エモーションの計算
        /// </summary>
        #region culcEmotion
        private void culcEmotion(List<int> emotionList, List<int> pointList)
        {
            int idx = 0;

            foreach (int emo in emotionList)
            {
                switch(emo)
                {
                    case 1: joy        += pointList[idx];   break;
                    case 2: admiration += pointList[idx];   break;
                    case 3: peace      += pointList[idx];   break;
                    case 4: ecstasy    += pointList[idx];   break;
                    case 5: amazement  += pointList[idx];   break;
                    case 6: rage       += pointList[idx];   break;
                    case 7: interest   += pointList[idx];   break;
                    case 8: respect    += pointList[idx];   break;
                    case 9: calmly     += pointList[idx];   break;
                    case 10:proud      += pointList[idx];   break;
                    default:                                break;
                }

                idx++;
            }
        }
        #endregion

                /// <summary>
        /// culcEmotion
        /// エモーションの計算
        /// </summary>
        #region culcEmotion
        private void getMaxEmotion()
        {
            int max = 0;
            int point = 0;

            //ジョイを取得
            max = 0;

            if (max < Math.Abs(joy))            { max = Math.Abs(joy);          maxEmo = 1; point = joy; }
            if (max < Math.Abs(admiration))     { max = Math.Abs(admiration);   maxEmo = 2; point = admiration; }
            if (max < Math.Abs(peace))          { max = Math.Abs(peace);        maxEmo = 3; point = peace; }
            if (max < Math.Abs(ecstasy))        { max = Math.Abs(ecstasy);      maxEmo = 4; point = ecstasy; }
            if (max < Math.Abs(amazement))      { max = Math.Abs(amazement);    maxEmo = 5; point = amazement; }
            if (max < Math.Abs(rage))           { max = Math.Abs(rage);         maxEmo = 6; point = rage; }
            if (max < Math.Abs(interest))       { max = Math.Abs(interest);     maxEmo = 7; point = interest; }
            if (max < Math.Abs(respect))        { max = Math.Abs(respect);      maxEmo = 8; point = respect; }
            if (max < Math.Abs(calmly))         { max = Math.Abs(calmly);       maxEmo = 9; point = calmly; }
            if (max < Math.Abs(proud))          { max = Math.Abs(proud);        maxEmo = 10; point = joy; }

            maxPoint = point;

        }
        #endregion
    }
}
