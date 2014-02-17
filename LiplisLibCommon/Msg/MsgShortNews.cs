//=======================================================================
//  ClassName : MsgShortNews
//  概要      : ショートニュースメッセージ
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
    [Serializable()]
    public class MsgShortNews
    {
        ///=============================
        ///プロパティ
        public string url { get; set; }
        public string title { get; set; }
        public string createDate { get; set; }
        public string source { get; set; }
        public string converted { get; set; }
        public string jpgUrl { get; set; }
        public int newsEmotion { get; set; }
        public int newsPoint { get; set; }
        public List<string> nameList { get; set; }
        public List<int> emotionList { get; set; }
        public List<int> pointList { get; set; }
        public bool flgSuccess { get; set; }
        public bool flgAlreadyRead { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public MsgShortNews()
        {
            url = "";
            title = "";
            createDate = "";
            source = "";
            converted = "";
            jpgUrl = "";
            newsEmotion = 0;
            newsPoint = 0;
            nameList = new List<string>();
            emotionList = new List<int>();
            pointList = new List<int>();
            flgSuccess = false;
            flgAlreadyRead = false;
        }
        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="name"></param>
        /// <param name="emotion"></param>
        /// <param name="point"></param>
        #region コンストラクター
        public MsgShortNews(string name, int emotion, int point)
        {
            url = "";
            createDate = "";
            source = "";
            converted = name;
            jpgUrl = "";
            newsEmotion = 0;
            newsPoint = 0;
            nameList = new List<string>();
            emotionList = new List<int>();
            pointList = new List<int>();
            flgSuccess = false;

            nameList.Add(name);
            emotionList.Add(emotion);
            pointList.Add(point);
        }
        #endregion

        /// <summary>
        /// calcNewsEmotion
        ///	ニュースエモーションを計算する
        /// </summary>
        #region calcNewsEmotion
        public void calcNewsEmotion()
        {
            int idx = 0;
            int maxEmo = 0;
            int maxPoint = 0;

            int joy = 0;
            int admiration = 0;
            int peace = 0;
            int ecstasy = 0;
            int amazement = 0;
            int rage = 0;
            int interest = 0;
            int respect = 0;
            int calmly = 0;
            int proud = 0;

            try
            {
                //名前リストが入っていたら
                if (nameList != null)
                {
                    //エモーションリストを回して集計
                    foreach (int emo in emotionList)
                    {
                        switch (emo)
                        {
                            case 1: joy += pointList[idx]; if (joy > maxEmo) { maxEmo = 1; maxPoint = Math.Abs(joy); } break;
                            case 2: admiration += pointList[idx]; if (admiration > maxEmo) { maxEmo = 2; maxPoint = Math.Abs(admiration); } break;
                            case 3: peace += pointList[idx]; if (peace > maxEmo) { maxEmo = 3; maxPoint = Math.Abs(peace); } break;
                            case 4: ecstasy += pointList[idx]; if (ecstasy > maxEmo) { maxEmo = 4; maxPoint = Math.Abs(ecstasy); } break;
                            case 5: amazement += pointList[idx]; if (amazement > maxEmo) { maxEmo = 5; maxPoint = Math.Abs(amazement); } break;
                            case 6: rage += pointList[idx]; if (rage > maxEmo) { maxEmo = 6; maxPoint = Math.Abs(rage); } break;
                            case 7: interest += pointList[idx]; if (interest > maxEmo) { maxEmo = 7; maxPoint = Math.Abs(interest); } break;
                            case 8: respect += pointList[idx]; if (respect > maxEmo) { maxEmo = 8; maxPoint = Math.Abs(respect); } break;
                            case 9: calmly += pointList[idx]; if (calmly > maxEmo) { maxEmo = 9; maxPoint = Math.Abs(calmly); } break;
                            case 10: proud += pointList[idx]; if (proud > maxEmo) { maxEmo = 10; maxPoint = Math.Abs(proud); } break;
                            default: break;
                        }

                        idx++;
                    }
                }

                //最大値取得
                newsEmotion = maxEmo;
                newsPoint = maxPoint;
            }
            catch
            {
                return;
            }
        }
        #endregion


        /// <summary>
        /// getNameFirst
        ///	１個目のネームを返す
        /// </summary>
        /// <returns>1個目のname</returns>
        #region getMessage
        public string getMessage()
        {
            if (nameList.Count > 0)
            {
                return nameList[0];
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
