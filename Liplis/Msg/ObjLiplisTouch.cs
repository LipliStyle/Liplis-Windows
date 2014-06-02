using System;
//=======================================================================
//  ClassName : ObjLiplisTouch
//  概要      : タッチオブジェクト
//
//sens 10段階
//type 0 なで
//1 クリック
//2 ダブルクリック
// 各座標は必須設定
// chat 設定は任意だが、指定しないと何も喋らない。chatに定義した定義名を設定する。
// name
//  Liplis4.0.2 2014/5/1 タッチ定義追加
//  Copyright(c) 2010-2014 LipliStyle.Sachin
//=======================================================================
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Xml;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Xml;

namespace Liplis.Msg
{
    public class ObjLiplisTouch : XmlReadList
    {
        ///==========================
		/// 内容
        public List<ObjTouch> touchDefList { get; set; }

        ///==========================
        /// タッチおしゃべり中
        public bool touchChatting = false;  

		///==========================
		/// フラグ
		public bool checkFlg { get; set; }

		///===========================================
		/// スキンファイル読み込み完了フラグ
		public bool loadDefault;

		/// <summary>
		/// RssListControllerコンストラクタ
		/// 設定ファイルを読み込む
		/// </summary>
		#region ObjLiplisTouch
		public ObjLiplisTouch(string loadSkin)
		{
			try
			{
				xmlDoc = new XmlDocument();
				initList();

				//キャッシュファイルの取得
                xmlFilePath = LpsPathControllerCus.getTouchDefinePath(loadSkin);

				//body.xmlの存在チェック
				if (LpsPathControllerCus.checkFileExist(xmlFilePath))
				{
					//xmlの読み込み
					readXml();
					readResult();
				}
				else
				{
					//リソースからデフォルトチャットファイルを読み込む
					readXmlFromXmlstring(FctCreateFromResource.getResourceXml(LiplisDefine.CHAT_RESOURCE));
					readResult();
				}

			}
			catch (System.Exception err)
			{
				LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
			}
		}
        public ObjLiplisTouch()
		{
			initList();
		}
		#endregion

		/// <summary>
		/// リストの初期化
		/// </summary>
		#region initList
		protected void initList()
		{
            touchDefList = new List<ObjTouch>();

		}
		#endregion

		/// <summary>
		/// readResult
		/// 設定読込
		/// </summary>
		#region readResult
		public void readResult()
		{
            int idx = 0;
            List<string> nameList = new List<string>();
            List<int> typeList = new List<int>();
            List<int> sensList = new List<int>();
            List<int> topList = new List<int>();
            List<int> leftList = new List<int>();
            List<int> bottomList = new List<int>();
            List<int> rightList = new List<int>();
            List<string> chatList = new List<string>();
            touchDefList = new List<ObjTouch>();

			readXmlList(xmlDoc.SelectNodes(LiplisDefine.TOUCH_NAME), nameList);
            readXmlListInt(xmlDoc.SelectNodes(LiplisDefine.TOUCH_TYPE), typeList);
            readXmlListInt(xmlDoc.SelectNodes(LiplisDefine.TOUCH_SENS), sensList);
            readXmlListInt(xmlDoc.SelectNodes(LiplisDefine.TOUCH_TOP), topList);
            readXmlListInt(xmlDoc.SelectNodes(LiplisDefine.TOUCH_LEFT), leftList);
            readXmlListInt(xmlDoc.SelectNodes(LiplisDefine.TOUCH_BOTTOM), bottomList);
            readXmlListInt(xmlDoc.SelectNodes(LiplisDefine.TOUCH_RIGHT), rightList);
            readXmlList(xmlDoc.SelectNodes(LiplisDefine.TOUCH_CHAT), chatList);

            foreach (string name in nameList)
            {
                touchDefList.Add(new ObjTouch(name, typeList[idx], sensList[idx], topList[idx], leftList[idx], bottomList[idx], rightList[idx], chatList[idx]));
                idx++;
            }
		}
		#endregion

        /// <summary>
        /// タッチチェック
        /// </summary>
        /// <returns></returns>
        #region checkTouch
        public objTouchResult checkTouch(int x, int y, List<string> checkList)
        {
            objTouchResult result = new objTouchResult(0, null);

            foreach (ObjTouch msg in touchDefList)
            {
                if (!checkList.Contains(msg.name))
                {
                    continue;
                }

                if (msg.sens == 0)
                {
                    continue;
                }

                int res = msg.checkTouch(x, y);

                if (res == 2)
                {
                    result.result = 2;
                    result.obj = msg;

                    return result;
                }
                else if (res == 1)
                {
                    result.result = 1;
                }
            }

            return result;
        }
        #endregion

        /// <summary>
        /// クリックチェック
        /// </summary>
        /// <returns></returns>
        #region checkTouch
        public objTouchResult checkClick(int x, int y, List<string> checkList, int mode)
        {
            objTouchResult result = new objTouchResult(0, null);

            foreach (ObjTouch msg in touchDefList)
            {
                if (!checkList.Contains(msg.name))
                {
                    continue;
                }

                if (mode == msg.type)
                {
                    result.result = mode;
                    if (msg.checkClick(x, y))
                    {
                        result.obj = msg;
                        return result;
                    }
                }
               
            }

            return result;
        }
        #endregion


    }

    #region objTouchResult
    public class objTouchResult
    {
        public int result { get; set; }
        public ObjTouch obj { get; set; }

        public objTouchResult(int result, ObjTouch obj)
        {
            this.result = result;
            this.obj = obj;
        }
    }
    #endregion


    #region ObjTouch
    public class ObjTouch
    {
        public string name { get; set; }
        public int type { get; set; }
        public int sens { get; set; }
        public int top { get; set; }
        public int left { get; set; }
        public int bottom { get; set; }
        public int right { get; set; }
        public string chatSelected { get; set; }
        public Rectangle rect { get; set; }
        public LstShufflableList<string> chatList { get; set; }

        ///==========================
        /// 感度
        private int sennsitivity = 0; 

        ///==========================
        /// タッチカウント
        public int sennsitivityCnt { get; set; }


        public ObjTouch(string name, int type, int sens, int top, int left, int bottom, int right, LstShufflableList<string> chatList)
        {
            this.name = name;
            this.top = top;
            this. type = type;
            this.sens = sens;
            this.left = left;
            this.bottom = bottom;
            this.right = right;
            this.rect = new Rectangle(top, left, right - left, bottom - top);
            this.chatList = chatList;
            this.sennsitivityCnt = 0;

            this.setSennsitivity();
        }

        public ObjTouch(string name, int type, int sens, int top, int left, int bottom, int right, string chat)
        {
            this.name = name;
            this.type = type;
            this.sens = sens;
            this.top = top;
            this.left = left;
            this.bottom = bottom;
            this.right = right;
            this.rect = new Rectangle(left,top, right - left, bottom - top);
            this.chatList = new LstShufflableList<string>(chat.Split(','));
            this.sennsitivityCnt = 0;

            this.setSennsitivity();
        }

        /// <summary>
        /// センシティビティをセットする
        /// </summary>
        private void setSennsitivity()
        {
            if (sens == 0)
            {
                sennsitivity = 10;
            }
            else if (sens == 1)
            {
                sennsitivity = 25;
            }
            else if (sens == 2)
            {
                sennsitivity = 50;
            }
            else if (sens == 3)
            {
                sennsitivity = 100;
            }
            else if (sens == 4)
            {
                sennsitivity = 250;
            }
            else if (sens == 5)
            {
                sennsitivity = 500;
            }
            else if (sens == 6)
            {
                sennsitivity = 1000;
            }
            else if (sens == 7)
            {
                sennsitivity = 2500;
            }
            else if (sens == 8)
            {
                sennsitivity = 5000;
            }
            else if (sens == 9)
            {
                sennsitivity = 10000;
            }
            else
            {
                this.sennsitivity = 0;
            }
        }

        /// <summary>
        /// タッチチェック
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int checkTouch(int x, int y)
        {
            int result = 0;

            //句形範囲チェック
            if (rect.Contains(x, y))
            {
                //なでタイプならカウントアップ
                if (this.type == 0)
                {
                    sennsitivityCnt++;
                }

                //カーソルON
                result = 1;
            }

            //撫で閾値チェック
            if (sennsitivityCnt > sennsitivity)
            {
                Console.WriteLine("HIT" + this.name);

                //0にリセット
                sennsitivityCnt = 0;

                //おしゃべりする文章を選択する
                chatSelected = getChat();

                //2を返す
                result =  2;
            }

            //結果を返す
            return result;
        }

        /// <summary>
        /// クリックチェック
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool checkClick(int x, int y)
        {
            //句形範囲チェック
            if (rect.Contains(x, y))
            {
                //おしゃべりする文章を選択する
                chatSelected = getChat();

                //2を返す
                return true;
            }

            //結果を返す
            return false;
        }

        /// <summary>
        /// 矩形範囲に含まれているかチェックする
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Contains(int x, int y)
        {
            return rect.Contains(x, y);
        }

        /// <summary>
        /// チャットリストからランダムで1個返す
        /// </summary>
        /// <returns></returns>
        private string getChat()
        {
            if (chatList.Count > 0)
            {
                chatList.Shuffle();

                return chatList[0];
            }
            else
            {
                return "";
            }
        }

    }
    #endregion
   
}
