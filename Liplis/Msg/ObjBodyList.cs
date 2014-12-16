//=======================================================================
//  ClassName : ObjBodyList
//  概要      : ボディオブジェクトリスト
//
//  Liplis3.0.2
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Liplis.Common;
using Liplis.Xml;

namespace Liplis.Msg
{
	[Serializable]
	public class ObjBodyList : XmlReadList
	{
		///==========================
		/// スキンパス
		protected string loadSkin;

		///==========================
		/// ボディプロパティ
		public int height { get; set; }
		public int width { get; set; }
		public int locX { get; set; }
		public int locY { get; set; }

		///==========================
		/// リスト
		public LstShufflableList<ObjBody> normalList { get; set; }
		public LstShufflableList<ObjBody> joyPList { get; set; }
		public LstShufflableList<ObjBody> joyMList { get; set; }
		public LstShufflableList<ObjBody> admirationPList { get; set; }
		public LstShufflableList<ObjBody> admirationMList { get; set; }
		public LstShufflableList<ObjBody> peacePList { get; set; }
		public LstShufflableList<ObjBody> peaceMList { get; set; }
		public LstShufflableList<ObjBody> ecstasyPList { get; set; }
		public LstShufflableList<ObjBody> ecstasyMList { get; set; }
		public LstShufflableList<ObjBody> amazementPList { get; set; }
		public LstShufflableList<ObjBody> amazementMList { get; set; }
		public LstShufflableList<ObjBody> ragePList { get; set; }
		public LstShufflableList<ObjBody> rageMList { get; set; }
		public LstShufflableList<ObjBody> interestPList { get; set; }
		public LstShufflableList<ObjBody> interestMList { get; set; }
		public LstShufflableList<ObjBody> respectPList { get; set; }
		public LstShufflableList<ObjBody> respectMList { get; set; }
		public LstShufflableList<ObjBody> calmlyPList { get; set; }
		public LstShufflableList<ObjBody> calmlyMList { get; set; }
		public LstShufflableList<ObjBody> proudPList { get; set; }
		public LstShufflableList<ObjBody> proudMList { get; set; }

		public LstShufflableList<ObjBody> sitdownList { get; set; }

		///=============================
		/// 破損ボディ
		public LstShufflableList<ObjBody> batteryHiList { get; set; }			//小破 2013/10/27 ver3.2.0
		public LstShufflableList<ObjBody> batteryMidList { get; set; }			//中破 2013/10/27 ver3.2.0
		public LstShufflableList<ObjBody> batteryLowList { get; set; }			//大破 2013/10/27 ver3.2.0

		///==========================
		/// インデックス
		protected int idx;

		/// <summary>
		/// RssListControllerコンストラクタ
		/// 設定ファイルを読み込む
		/// </summary>
		#region ObjBodyList
		public ObjBodyList(string loadSkin)
		{
			try
			{
				//ロードスキン
				this.loadSkin = loadSkin;

				//xmlドキュメント
				xmlDoc = new XmlDocument();

				//リストの初期化
				initList();

				//キャッシュファイルの取得
				xmlFilePath = LpsPathControllerCus.getBodyDefinePath(loadSkin);

				//body.xmlの存在チェック
				if (LpsPathControllerCus.checkFileExist(xmlFilePath))
				{
					//xmlの読み込み
					readXml();

					//サイズとロケーションを取得する
					getSizeAndLocation();

					//読み込み結果の取得
					createList();
				}
				else
				{
					//リソース読み込み
					createDefault();
				}

				
			}
			catch (System.Exception err)
			{
				LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
				createDefault();
			}
		}
		#endregion

		/// <summary>
		/// コンストラクター
		/// </summary>
		#region ObjBodyList
		public ObjBodyList()
		{
			try
			{
				this.height = 0;
				this.width = 0;
				this.locX = 0;
				this.locY = 0;
			}
			catch (System.Exception err)
			{
				LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
				createDefault();
			}
		}
		#endregion

		///====================================================================
		///
		///                          初期化処理
		///                         
		///====================================================================

		/// <summary>
		/// getSizeAndLocation
		/// ロケーションとサイズを取得する
		/// ☆Miniオーバーライド
		/// </summary>
		#region getSizeAndLocation
		protected virtual void getSizeAndLocation()
		{
			this.height = rXLMSInt(xmlDoc.SelectNodes(LiplisDefine.BODY_HEIGHT));
			this.width = rXLMSInt(xmlDoc.SelectNodes(LiplisDefine.BODY_WIDHT));
			this.locX = rXLMSInt(xmlDoc.SelectNodes(LiplisDefine.BODY_LOCATION_X));
			this.locY = rXLMSInt(xmlDoc.SelectNodes(LiplisDefine.BODY_LOCATION_Y));
		}
		#endregion

		/// <summary>
		/// リストの初期化
		/// </summary>
		#region initList
		protected void initList()
		{
			normalList = new LstShufflableList<ObjBody>();
			joyPList = new LstShufflableList<ObjBody>();
			joyMList = new LstShufflableList<ObjBody>();
			admirationPList = new LstShufflableList<ObjBody>();
			admirationMList = new LstShufflableList<ObjBody>();
			peacePList = new LstShufflableList<ObjBody>();
			peaceMList = new LstShufflableList<ObjBody>();
			ecstasyPList = new LstShufflableList<ObjBody>();
			ecstasyMList = new LstShufflableList<ObjBody>();
			amazementPList = new LstShufflableList<ObjBody>();
			amazementMList = new LstShufflableList<ObjBody>();
			ragePList = new LstShufflableList<ObjBody>();
			rageMList = new LstShufflableList<ObjBody>();
			interestPList = new LstShufflableList<ObjBody>();
			interestMList = new LstShufflableList<ObjBody>();
			respectPList = new LstShufflableList<ObjBody>();
			respectMList = new LstShufflableList<ObjBody>();
			calmlyPList = new LstShufflableList<ObjBody>();
			calmlyMList = new LstShufflableList<ObjBody>();
			proudPList = new LstShufflableList<ObjBody>();
			proudMList = new LstShufflableList<ObjBody>();
			sitdownList = new LstShufflableList<ObjBody>();
		}
		#endregion

		/// <summary>
		/// リストの作成
		/// </summary>
		#region createList
		protected virtual void createList()
		{
			normalList      = readResult(LiplisDefine.BODY_NORMAL_11, LiplisDefine.BODY_NORMAL_12, LiplisDefine.BODY_NORMAL_21, LiplisDefine.BODY_NORMAL_22, LiplisDefine.BODY_NORMAL_31, LiplisDefine.BODY_NORMAL_32, LiplisDefine.BODY_NORMAL_TOUCH);
			joyPList        = readResult(LiplisDefine.BODY_JOY_P_11, LiplisDefine.BODY_JOY_P_12, LiplisDefine.BODY_JOY_P_21, LiplisDefine.BODY_JOY_P_22, LiplisDefine.BODY_JOY_P_31, LiplisDefine.BODY_JOY_P_32, LiplisDefine.BODY_JOY_P_TOUCH);
			joyMList        = readResult(LiplisDefine.BODY_JOY_M_11, LiplisDefine.BODY_JOY_M_12, LiplisDefine.BODY_JOY_M_21, LiplisDefine.BODY_JOY_M_22, LiplisDefine.BODY_JOY_M_31, LiplisDefine.BODY_JOY_M_32, LiplisDefine.BODY_JOY_M_TOUCH);
			admirationPList = readResult(LiplisDefine.BODY_ADMIRATION_P_11, LiplisDefine.BODY_ADMIRATION_P_12, LiplisDefine.BODY_ADMIRATION_P_21, LiplisDefine.BODY_ADMIRATION_P_22, LiplisDefine.BODY_ADMIRATION_P_31, LiplisDefine.BODY_ADMIRATION_P_32, LiplisDefine.BODY_ADMIRATION_P_TOUCH);
			admirationMList = readResult(LiplisDefine.BODY_ADMIRATION_M_11, LiplisDefine.BODY_ADMIRATION_M_12, LiplisDefine.BODY_ADMIRATION_M_21, LiplisDefine.BODY_ADMIRATION_M_22, LiplisDefine.BODY_ADMIRATION_M_31, LiplisDefine.BODY_ADMIRATION_M_32, LiplisDefine.BODY_ADMIRATION_M_TOUCH);
			peacePList      = readResult(LiplisDefine.BODY_PEACE_P_11, LiplisDefine.BODY_PEACE_P_12, LiplisDefine.BODY_PEACE_P_21, LiplisDefine.BODY_PEACE_P_22, LiplisDefine.BODY_PEACE_P_31, LiplisDefine.BODY_PEACE_P_32, LiplisDefine.BODY_PEACE_P_TOUCH);
			peaceMList      = readResult(LiplisDefine.BODY_PEACE_M_11, LiplisDefine.BODY_PEACE_M_12, LiplisDefine.BODY_PEACE_M_21, LiplisDefine.BODY_PEACE_M_22, LiplisDefine.BODY_PEACE_M_31, LiplisDefine.BODY_PEACE_M_32, LiplisDefine.BODY_PEACE_M_TOUCH);
			ecstasyPList    = readResult(LiplisDefine.BODY_ECSTASY_P_11, LiplisDefine.BODY_ECSTASY_P_12, LiplisDefine.BODY_ECSTASY_P_21, LiplisDefine.BODY_ECSTASY_P_22, LiplisDefine.BODY_ECSTASY_P_31, LiplisDefine.BODY_ECSTASY_P_32, LiplisDefine.BODY_ECSTASY_P_TOUCH);
			ecstasyMList    = readResult(LiplisDefine.BODY_ECSTASY_M_11, LiplisDefine.BODY_ECSTASY_M_12, LiplisDefine.BODY_ECSTASY_M_21, LiplisDefine.BODY_ECSTASY_M_22, LiplisDefine.BODY_ECSTASY_M_31, LiplisDefine.BODY_ECSTASY_M_32, LiplisDefine.BODY_ECSTASY_M_TOUCH);
			amazementPList  = readResult(LiplisDefine.BODY_AMAZEMENT_P_11, LiplisDefine.BODY_AMAZEMENT_P_12, LiplisDefine.BODY_AMAZEMENT_P_21, LiplisDefine.BODY_AMAZEMENT_P_22, LiplisDefine.BODY_AMAZEMENT_P_31, LiplisDefine.BODY_AMAZEMENT_P_32, LiplisDefine.BODY_AMAZEMENT_P_TOUCH);
			amazementMList  = readResult(LiplisDefine.BODY_AMAZEMENT_M_11, LiplisDefine.BODY_AMAZEMENT_M_12, LiplisDefine.BODY_AMAZEMENT_M_21, LiplisDefine.BODY_AMAZEMENT_M_22, LiplisDefine.BODY_AMAZEMENT_M_31, LiplisDefine.BODY_AMAZEMENT_M_32, LiplisDefine.BODY_AMAZEMENT_M_TOUCH);
			ragePList       = readResult(LiplisDefine.BODY_RAGE_P_11, LiplisDefine.BODY_RAGE_P_12, LiplisDefine.BODY_RAGE_P_21, LiplisDefine.BODY_RAGE_P_22, LiplisDefine.BODY_RAGE_P_31, LiplisDefine.BODY_RAGE_P_32, LiplisDefine.BODY_RAGE_P_TOUCH);
			rageMList       = readResult(LiplisDefine.BODY_RAGE_M_11, LiplisDefine.BODY_RAGE_M_12, LiplisDefine.BODY_RAGE_M_21, LiplisDefine.BODY_RAGE_M_22, LiplisDefine.BODY_RAGE_M_31, LiplisDefine.BODY_RAGE_M_32, LiplisDefine.BODY_RAGE_M_TOUCH);
			interestPList   = readResult(LiplisDefine.BODY_INTEREST_P_11, LiplisDefine.BODY_INTEREST_P_12, LiplisDefine.BODY_INTEREST_P_21, LiplisDefine.BODY_INTEREST_P_22, LiplisDefine.BODY_INTEREST_P_31, LiplisDefine.BODY_INTEREST_P_32, LiplisDefine.BODY_INTEREST_P_TOUCH);
			interestMList   = readResult(LiplisDefine.BODY_INTEREST_M_11, LiplisDefine.BODY_INTEREST_M_12, LiplisDefine.BODY_INTEREST_M_21, LiplisDefine.BODY_INTEREST_M_22, LiplisDefine.BODY_INTEREST_M_31, LiplisDefine.BODY_INTEREST_M_32, LiplisDefine.BODY_INTEREST_M_TOUCH);
			respectPList    = readResult(LiplisDefine.BODY_RESPECT_P_11, LiplisDefine.BODY_RESPECT_P_12, LiplisDefine.BODY_RESPECT_P_21, LiplisDefine.BODY_RESPECT_P_22, LiplisDefine.BODY_RESPECT_P_31, LiplisDefine.BODY_RESPECT_P_32, LiplisDefine.BODY_RESPECT_P_TOUCH);
			respectMList    = readResult(LiplisDefine.BODY_RESPECT_M_11, LiplisDefine.BODY_RESPECT_M_12, LiplisDefine.BODY_RESPECT_M_21, LiplisDefine.BODY_RESPECT_M_22, LiplisDefine.BODY_RESPECT_M_31, LiplisDefine.BODY_RESPECT_M_32, LiplisDefine.BODY_RESPECT_M_TOUCH);
			calmlyPList     = readResult(LiplisDefine.BODY_CLAMLY_P_11, LiplisDefine.BODY_CLAMLY_P_12, LiplisDefine.BODY_CLAMLY_P_21, LiplisDefine.BODY_CLAMLY_P_22, LiplisDefine.BODY_CLAMLY_P_31, LiplisDefine.BODY_CLAMLY_P_32, LiplisDefine.BODY_CLAMLY_P_TOUCH);
			calmlyMList     = readResult(LiplisDefine.BODY_CLAMLY_M_11, LiplisDefine.BODY_CLAMLY_M_12, LiplisDefine.BODY_CLAMLY_M_21, LiplisDefine.BODY_CLAMLY_M_22, LiplisDefine.BODY_CLAMLY_M_31, LiplisDefine.BODY_CLAMLY_M_32, LiplisDefine.BODY_CLAMLY_M_TOUCH);
			proudPList      = readResult(LiplisDefine.BODY_PROUD_P_11, LiplisDefine.BODY_PROUD_P_12, LiplisDefine.BODY_PROUD_P_21, LiplisDefine.BODY_PROUD_P_22, LiplisDefine.BODY_PROUD_P_31, LiplisDefine.BODY_PROUD_P_32, LiplisDefine.BODY_PROUD_P_TOUCH);
			proudMList      = readResult(LiplisDefine.BODY_PROUD_M_11, LiplisDefine.BODY_PROUD_M_12, LiplisDefine.BODY_PROUD_M_21, LiplisDefine.BODY_PROUD_M_22, LiplisDefine.BODY_PROUD_M_31, LiplisDefine.BODY_PROUD_M_32, LiplisDefine.BODY_PROUD_M_TOUCH);
			sitdownList     = readResult(LiplisDefine.BODY_SITDOWN_11, LiplisDefine.BODY_SITDOWN_12, LiplisDefine.BODY_SITDOWN_21, LiplisDefine.BODY_SITDOWN_22, LiplisDefine.BODY_SITDOWN_31, LiplisDefine.BODY_SITDOWN_32,"");
			batteryHiList   = readResult(LiplisDefine.BODY_BATTERY_HI_11, LiplisDefine.BODY_BATTERY_HI_12, LiplisDefine.BODY_BATTERY_HI_21, LiplisDefine.BODY_BATTERY_HI_22, LiplisDefine.BODY_BATTERY_HI_31, LiplisDefine.BODY_BATTERY_HI_32, LiplisDefine.BODY_BATTERY_HI_TOUCH);			            //小破 2013/10/27 ver3.2.0
			batteryMidList  = readResult(LiplisDefine.BODY_BATTERY_MID_11, LiplisDefine.BODY_BATTERY_MID_12, LiplisDefine.BODY_BATTERY_MID_21, LiplisDefine.BODY_BATTERY_MID_22, LiplisDefine.BODY_BATTERY_MID_31, LiplisDefine.BODY_BATTERY_MID_32, LiplisDefine.BODY_BATTERY_MID_TOUCH);			//中破 2013/10/27 ver3.2.0
			batteryLowList = readResult(LiplisDefine.BODY_BATTERY_LOW_11, LiplisDefine.BODY_BATTERY_LOW_12, LiplisDefine.BODY_BATTERY_LOW_21, LiplisDefine.BODY_BATTERY_LOW_22, LiplisDefine.BODY_BATTERY_LOW_31, LiplisDefine.BODY_BATTERY_LOW_32, LiplisDefine.BODY_BATTERY_LOW_TOUCH);			//大破 2013/10/27 ver3.2.0
		}
		#endregion

		/// <summary>
		/// デフォルトリストの作成
		/// </summary>
		#region createDefault
		protected void createDefault()
		{
			this.height = 300;
			this.width = 300;
			this.locX = 0;
			this.locY = 0;

			normalList      = readResultDef(LiplisDefine.EMOTION_NORMAL, 1);
			joyPList        = readResultDef(LiplisDefine.EMOTION_JOY_P, 1);
			joyMList        = readResultDef(LiplisDefine.EMOTION_JOY_M, 1);
			admirationPList = readResultDef(LiplisDefine.EMOTION_ADMIRATION_P, 1);
			admirationMList = readResultDef(LiplisDefine.EMOTION_ADMIRATION_M, 1);
			peacePList      = readResultDef(LiplisDefine.EMOTION_PEACE_P, 1);
			peaceMList      = readResultDef(LiplisDefine.EMOTION_PEACE_M, 1);
			ecstasyPList    = readResultDef(LiplisDefine.EMOTION_ECSTASY_P, 1);
			ecstasyMList    = readResultDef(LiplisDefine.EMOTION_ECSTASY_M, 1);
			amazementPList  = readResultDef(LiplisDefine.EMOTION_AMAZEMENT_P, 1);
			amazementMList  = readResultDef(LiplisDefine.EMOTION_AMAZEMENT_M, 1);
			ragePList       = readResultDef(LiplisDefine.EMOTION_RAGE_P, 1);
			rageMList       = readResultDef(LiplisDefine.EMOTION_RAGE_M, 1);
			interestPList   = readResultDef(LiplisDefine.EMOTION_INTEREST_P, 1);
			interestMList   = readResultDef(LiplisDefine.EMOTION_INTEREST_M, 1);
			respectPList    = readResultDef(LiplisDefine.EMOTION_RESPECT_P, 1);
			respectMList    = readResultDef(LiplisDefine.EMOTION_RESPECT_M, 1);
			calmlyPList     = readResultDef(LiplisDefine.EMOTION_CLAMLY_P, 1);
			calmlyMList     = readResultDef(LiplisDefine.EMOTION_CLAMLY_M, 1);
			proudPList      = readResultDef(LiplisDefine.EMOTION_PROUD_P, 1);
			proudMList      = readResultDef(LiplisDefine.EMOTION_PROUD_M, 1);
			batteryHiList   = readResultDef(LiplisDefine.EMOTION_BATTERY_HI, 1);
			batteryMidList  = readResultDef(LiplisDefine.EMOTION_BATTERY_MID, 1);
			batteryLowList  = readResultDef(LiplisDefine.EMOTION_BATTERY_LOW, 1);

			//normalList = readResultDef(LiplisDefine.EMOTION_NORMAL, 5);
			//joyPList = readResultDef(LiplisDefine.EMOTION_JOY_P, 4);
			//joyMList = readResultDef(LiplisDefine.EMOTION_JOY_M, 4);
			//admirationPList = readResultDef(LiplisDefine.EMOTION_ADMIRATION_P, 3);
			//admirationMList = readResultDef(LiplisDefine.EMOTION_ADMIRATION_M, 5);
			//peacePList = readResultDef(LiplisDefine.EMOTION_PEACE_P, 2);
			//peaceMList = readResultDef(LiplisDefine.EMOTION_PEACE_M, 3);
			//ecstasyPList = readResultDef(LiplisDefine.EMOTION_ECSTASY_P, 3);
			//ecstasyMList = readResultDef(LiplisDefine.EMOTION_ECSTASY_M, 3);
			//amazementPList = readResultDef(LiplisDefine.EMOTION_AMAZEMENT_P, 3);
			//amazementMList = readResultDef(LiplisDefine.EMOTION_AMAZEMENT_M, 3);
			//ragePList = readResultDef(LiplisDefine.EMOTION_RAGE_P, 3);
			//rageMList = readResultDef(LiplisDefine.EMOTION_RAGE_M, 2);
			//interestPList = readResultDef(LiplisDefine.EMOTION_INTEREST_P, 5);
			//interestMList = readResultDef(LiplisDefine.EMOTION_INTEREST_M, 2);
			//respectPList = readResultDef(LiplisDefine.EMOTION_RESPECT_P, 3);
			//respectMList = readResultDef(LiplisDefine.EMOTION_RESPECT_M, 2);
			//calmlyPList = readResultDef(LiplisDefine.EMOTION_CLAMLY_P, 2);
			//calmlyMList = readResultDef(LiplisDefine.EMOTION_CLAMLY_M, 4);
			//proudPList = readResultDef(LiplisDefine.EMOTION_PROUD_P, 1);
			//proudMList = readResultDef(LiplisDefine.EMOTION_PROUD_M, 3);
		}
		#endregion

		/// <summary>
		/// 設定読込
		/// </summary>
		#region readResult
		protected virtual LstShufflableList<ObjBody> readResult(string b11, string b12, string b21, string b22, string b31, string b32, string touch)
		{
			LstShufflableList<ObjBody> result = new LstShufflableList<ObjBody>();
			int idx = 0;
			List<string> b11l = new List<string>();
			List<string> b12l = new List<string>();
			List<string> b21l = new List<string>();
			List<string> b22l = new List<string>();
			List<string> b31l = new List<string>();
			List<string> b32l = new List<string>();
			List<string> tl = new List<string>();

			readXmlList(xmlDoc.SelectNodes(b11), b11l);
			readXmlList(xmlDoc.SelectNodes(b12), b12l);
			readXmlList(xmlDoc.SelectNodes(b21), b21l);
			readXmlList(xmlDoc.SelectNodes(b22), b22l);
			readXmlList(xmlDoc.SelectNodes(b31), b31l);
			readXmlList(xmlDoc.SelectNodes(b32), b32l);

			//タッチ設定のロード
			if (touch.Length > 0)
			{
				readXmlList(xmlDoc.SelectNodes(touch), tl);
			}

			//タッチ設定が無い場合は、空のタッチリスト作成
			if (tl.Count < 1)
			{
				foreach (string r11 in b11l)
				{
					tl.Add("");
				}
			}
			

			//リストを回してオブジェクト生成
			foreach (string r11 in b11l)
			{
				try
				{
					result.Add(new ObjBodyGen(b11l[idx], b12l[idx], b21l[idx], b22l[idx], b31l[idx], b32l[idx], tl[idx],LpsPathControllerCus.getBodyPath(loadSkin)));
				}
				catch
				{
					continue;
				}
				idx++;
			}

			return result;
		}
		#endregion

		/// <summary>
		/// デフォルト設定読込
		/// </summary>
		#region readResultDef
		protected LstShufflableList<ObjBody> readResultDef(string emotion, int num)
		{
			try
			{
				LstShufflableList<ObjBody> lst = new LstShufflableList<ObjBody>();

				for (int idx = 1; idx <= num; idx++)
				{
					lst.Add(new ObjBodyDef(emotion, idx.ToString()));
				}

				return lst;
			}
			catch (Exception err)
			{
				LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
				return new LstShufflableList<ObjBody>();
			}

		}
		#endregion

		/// <summary>
		/// setPreferenceData
		/// セーブ
		/// </summary>
		#region saveSettings
		public virtual void saveSettings()
		{

		}
		#endregion

		///====================================================================
		///
		///                     ボディオブジェクトの取得
		///                         
		///====================================================================

		/// <summary>
		/// bodyの取得
		/// </summary>
		/// <param name="emotion"></param>
		/// <returns></returns>
		#region getLiplisBody
		public ObjBody getLiplisBody(int emotion, int point)
		{
			//2014/10/04 emotionが0でなく、ポイントが0の場合、エモーションの値をポイントにセットする。
			//文章の場合は、エモーションのみの設定となるため。
			if (emotion != 0 && point == 0)
			{
				point = emotion;
			}

			//絶対値をとっておく。
			emotion = Math.Abs(emotion);

			if (emotion == 0)
			{
				return selectBody(normalList);
			}
			else if (emotion == 1)
			{
				if (point >= 0)
				{
					return selectBody(joyPList);
				}
				else
				{
					return selectBody(joyMList);
				}
			}
			else if (emotion == 2)
			{
				if (point >= 0)
				{
					return selectBody(admirationPList);
				}
				else
				{
					return selectBody(admirationMList);
				}
			}
			else if (emotion == 3)
			{
				if (point >= 0)
				{
					return selectBody(peacePList);
				}
				else
				{
					return selectBody(peaceMList);
				}
			}
			else if (emotion == 4)
			{
				if (point >= 0)
				{
					return selectBody(ecstasyPList);
				}
				else
				{
					return selectBody(ecstasyMList);
				}
			}
			else if (emotion == 5)
			{
				if (point >= 0)
				{
					return selectBody(amazementPList);
				}
				else
				{
					return selectBody(amazementMList);
				}
			}
			else if (emotion == 6)
			{
				if (point >= 0)
				{
					return selectBody(ragePList);
				}
				else
				{
					return selectBody(rageMList);
				}
			}
			else if (emotion == 7)
			{
				if (point >= 0)
				{
					return selectBody(interestPList);
				}
				else
				{
					return selectBody(interestMList);
				}
			}
			else if (emotion == 8)
			{
				if (point >= 0)
				{
					return selectBody(respectPList);
				}
				else
				{
					return selectBody(respectMList);
				}
			}
			else if (emotion == 9)
			{
				if (point >= 0)
				{
					return selectBody(calmlyPList);
				}
				else
				{
					return selectBody(calmlyMList);
				}
			}
			else if (emotion == 10)
			{
				if (point >= 0)
				{
					return selectBody(proudPList);
				}
				else
				{
					return selectBody(proudMList);
				}
			}
			else if (emotion == 100)
			{
				return selectBody(sitdownList);
			}
			else
			{
				return selectBody(normalList);
			}
		}
		#endregion

		/// <summary>
		/// ボティをランダムに取得する
		/// </summary>
		#region selectBody
		protected ObjBody selectBody(LstShufflableList<ObjBody> lst)
		{
			if (lst.Count > 0)
			{
				lst.Shuffle();
				return lst[LpsLiplisUtil.getRandamInt(0, lst.Count)];
			}
			return normalList[0];
		}
		#endregion
		
		/// <summary>
		/// 健康状態状態からIDを取得する
		/// </summary>
		#region getLiplisBodyHelth
		public ObjBody getLiplisBodyHelth(int helth, int emotion, int point)
		{
			try
			{
				//小破以上
				if (helth > 50)
				{
					if (batteryHiList.Count == 0)
					{
						return getLiplisBody(emotion, point);
					}
					else
					{
						return selectBody(batteryHiList);
					}
				}
				//中破
				else if (helth > 25)
				{
					if (batteryMidList.Count == 0)
					{
						return getLiplisBody(emotion, point);
					}
					else
					{
						return selectBody(batteryMidList);
					}
				}
				//大破
				else
				{
					if (batteryLowList.Count == 0)
					{
						return getLiplisBody(emotion, point);
					}
					else
					{
						return selectBody(batteryLowList);
					}
				}
			}
			catch
			{
				return normalList[0];
			}
		}
		#endregion        
	}
}
