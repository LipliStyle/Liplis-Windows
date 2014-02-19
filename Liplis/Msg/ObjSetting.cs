//=======================================================================
//  ClassName : ObjSetting
//  概要      : 設定オブジェクト
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
using Liplis.Web;

namespace Liplis.Msg
{
    /// <summary>
    /// オブジェクトセッティング
    /// </summary>
    [Serializable]
    public class ObjSetting : XmlSettingObject
    {
        ///=============================
        ///プロパティ
        public string uid               { get; set; }       //ロードスキン名
        public string loadSkin          { get; set; }       //ロードスキン名
        public int locationX            { get; set; }       //ロケーションX
        public int locationY            { get; set; }       //ロケーションY
        public int mode                 { get; set; }       //チャット頻度
        public int speed                { get; set; }       //表示速度
        public int mouseCtrl            { get; set; }       //マウス移動コントロール入力
        public int talkChase            { get; set; }       //会話ウインドウの追随
        public int alwaysTop            { get; set; }       //常にトップ
        public int window               { get; set; }       //ウインドウ

        public int dropOn               { get; set; }       //dropOn
        public int discWindowOn         { get; set; }       //詳細ウインドウオン

        public string nicoId            { get; set; }       //ニコニコID
        public string nicoPass          { get; set; }       //ニコニコパス

        public int downNotice           { get; set; }       //ダウンロードパス
        public string downPath          { get; set; }       //ダウンロードパス

        public int regionCd             { get; set; }       //地域コード

        public int menuType             { get; set; }       //メニュータイプ 0: サークル 1:ボックス
        public int menuTiming           { get; set; }       //メニュータイミング 0:マウスオン 1:右クリック

        public int lpsHelth             { get; set; }       //健康状態の反映
        public int lpsOutRangeRecovery  { get; set; }       //ver 3.2.2 2014/01/14 

        ///=============================
        /// ニュース
        public int lpsTopicNews         { get; set; }		//ニュース
        public int lpsTopic2ch          { get; set; }		//2ch
        public int lpsTopicNico         { get; set; }		//ニコニコ
        public int lpsTopicRss          { get; set; }		//RSS
        public int lpsTopicTwitter      { get; set; }		//ツイッター
        public int lpsTopicTwitterMy    { get; set; }		//ツイッターマイタイムライン
        public int lpsTopicTwitterPu    { get; set; }		//ツイッターパブリックタイムライン
        public int lpsTopicHour         { get; set; }       //ニュースの時間範囲
        public int lpsAlready           { get; set; }       //ver3.1.0 既読を読まないフラグ
        public int lpsTwitterMode       { get; set; }       //ver3.1.0 ツイッターモード
        public int lpsRunout            { get; set; }       //ver3.1.0 話題が尽きた時の設定
        

        ///=============================
        /// 対応設定値
        public int lpsInterval          { get; set; }       //インターバル
        public int lpsReftesh           { get; set; }       //リフレッシュレート

        ///=============================
        /// 音声設定
        public int lpsVoiceOn           { get; set; }		//ボイス設定
        public int lpsVoiceSelect       { get; set; }		//0:なし、1:ソフトーク、2:結月ゆかり、3:民安ともえ、4:東北ずん子
        public string lpsVoiceVRPathSofTalk { get; set; }		//ソフトークパス
        public string lpsVoiceVRPathYukari  { get; set; }		//ゆかりパス
        public string lpsVoiceVRPathTomoe   { get; set; }		//ともえパス
        public string lpsVoiceVRPathZunko   { get; set; }		//ずんこパス

        ///=============================
        /// twitter認証設定済み
        public int twitterActivate { get; set; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public ObjSetting()
        {
            try
            {
                //設定の取得
                setting = new SharedPreferences(LpsPathControllerCus.getSettingFilePath());

                //読み込み
                getPreferenceData();

            }
            catch (System.Exception err)
            {
                //設定ファイルの読み込みエラーの旨、異常位置を知らせるウインドウを出すべき
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, LpsPathControllerCus.getSettingFilePath() + "が存在しないため作成します" + Environment.NewLine + err);
            }
        }
        #endregion

        /// <summary>
        /// readResult
        /// readXmlで読み込んだ結果を自変数に読み込む
        /// </summary>
        #region getPreferenceData
        public override void getPreferenceData()
        {
            try
            {
                //メイン設定の読込
                setUid(setting.getString(LiplisDefine.PREFS_UID, FctGUIDCreater.createLiplisGuid()));
                setLoadSkin(setting.getString(LiplisDefine.PREFS_LOAD_SKIN, LiplisDefine.SKIN_DEF_CHAR_NAME));
                setLocationX(setting.getInt(LiplisDefine.PREFS_LOCATION_X, 0));
                setLocationY(setting.getInt(LiplisDefine.PREFS_LOCATION_Y, 0));
                setMode(setting.getInt(LiplisDefine.PREFS_MODE, 2));
                setSpeed(setting.getInt(LiplisDefine.PREFS_SPEED, 1));
                setTalkChase(setting.getInt(LiplisDefine.PREFS_TALK_CHASE, 1));
                setAlwaysTop(setting.getInt(LiplisDefine.PREFS_ALWAYSTOP, 0));
                setMouseCtrl(setting.getInt(LiplisDefine.PREFS_MOUSE_CTLS, 0));
                setDropOn(setting.getInt(LiplisDefine.PREFS_DROP_ON, 1));
                setNicoId(setting.getString(LiplisDefine.PREFS_NICO_ID,""));
                setNicoPass(setting.getString(LiplisDefine.PREFS_NICO_PASS,""));
                setDownPath(setting.getString(LiplisDefine.PREFS_DOWN_PATH,""));
                setDownNotice(setting.getInt(LiplisDefine.PREFS_NOTICE, 0));
                setDiscWindowOn(setting.getInt(LiplisDefine.PREFS_DISC_WINDFOW_ON, 1));
                setWindow(setting.getInt(LiplisDefine.PREFS_WINDOW, 0));
                setHelth(setting.getInt(LiplisDefine.PREFS_HELTH, 1));
                setOutRangeRecovery(setting.getInt(LiplisDefine.PREFS_OUTRANGE_RECOVERY, 1));

                setRegionCd(setting.getInt(LiplisDefine.PREFS_REGION_CD, 0));
                setMenuType(setting.getInt(LiplisDefine.PREFS_MENU_TYPE, 0));
                setMenuTiming(setting.getInt(LiplisDefine.PREFS_MENU_TIMING, 0));

                setLpsTopicNews(setting.getInt(LiplisDefine.PREFS_TOPIC_NEWS, 1));
                setLpsTopic2ch(setting.getInt(LiplisDefine.PREFS_TOPIC_2CH, 1));
                setLpsTopicNico(setting.getInt(LiplisDefine.PREFS_TOPIC_NICO, 1));
                setLpsTopicRss(setting.getInt(LiplisDefine.PREFS_TOPIC_RSS, 0));
                setLpsTopicTwitter(setting.getInt(LiplisDefine.PREFS_TOPIC_TWITTER, 0));
                setLpsTopicTwitterPb(setting.getInt(LiplisDefine.PREFS_TOPIC_TWITTER_PB, 0));
                setLpsTopicTwitterMy(setting.getInt(LiplisDefine.PREFS_TOPIC_TWITTER_MY, 0));
                setLpsNewsHour(setting.getInt(LiplisDefine.PREFS_TOPIC_HOUR, 6));
                setLpsAlready(setting.getInt(LiplisDefine.PREFS_TOPIC_ALREADY, 0));             //0:既読スキップしない 1:既読スキップする
                setLpsTwitterMode(setting.getInt(LiplisDefine.PREFS_TOPIC_TWITTER_MODE, 0));    //0:ランダム 1:リアルタイム
                setLpsRunout(setting.getInt(LiplisDefine.PREFS_TOPIC_RUNOUT, 0));               //0:ランダム 1:何もしない
                

                setLpsVoiceOn(setting.getInt(LiplisDefine.PREFS_VOICE_ON, 0));
                setLpsVoiceSelect(setting.getInt(LiplisDefine.PREFS_VOICE_SELECT, 0));
                setLpsVoiceVRPathSofTalk(setting.getString(LiplisDefine.PREFS_VOICE_VRPATH_SOFTALK, ""));
                setLpsVoiceVRPathYukari(setting.getString(LiplisDefine.PREFS_VOICE_VRPATH_YUKARI, ""));
                setLpsVoiceVRPathTomoe(setting.getString(LiplisDefine.PREFS_VOICE_VRPATH_TOMOE, ""));
                setLpsVoiceVRPathZunko(setting.getString(LiplisDefine.PREFS_VOICE_VRPATH_ZUNKO, ""));

                setTwitterActivate(setting.getInt(LiplisDefine.PREFS_TWITTER_ACTIVATE, 0));

                //UIDの再取得
                setPreferenceDataSettingOnly();
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "readResult:設定の読込失敗" + Environment.NewLine + err);
            }

        }
        #endregion

        /// <summary>
        /// loadDefault
        /// デフォルト設定をロードする
        /// </summary>
        #region loadDefault
        public void loadDefault()
        {
            //設定データの削除
            LpsPathControllerCus.delteFile(LpsPathControllerCus.getSettingFilePath());

            //設定の取得
            setting = new SharedPreferences(LpsPathControllerCus.getSettingFilePath());

            //読み込み
            getPreferenceData();
        }
        #endregion

        /// <summary>
        /// セーブ
        /// </summary>
        #region setPreferenceData
        public override void setPreferenceData()
        {
            setPreferenceDataSettingOnly();

            LiplisApiCus.saveTopicSetting(uid, lpsTopicHour.ToString(), lpsAlready.ToString(), lpsTopicNews.ToString(), lpsTopic2ch.ToString(), lpsTopicNico.ToString(), lpsTopicRss.ToString(),
                lpsTopicTwitterPu.ToString(), lpsTopicTwitterMy.ToString(), lpsTopicTwitter.ToString(), lpsTwitterMode.ToString(), lpsRunout.ToString());
        }
        public void setPreferenceDataSettingOnly()
        {
            setting.setString(LiplisDefine.PREFS_UID, this.uid);
            setting.setString(LiplisDefine.PREFS_LOAD_SKIN, this.loadSkin);
            setting.setInt(LiplisDefine.PREFS_LOCATION_X, this.locationX);
            setting.setInt(LiplisDefine.PREFS_LOCATION_Y, this.locationY);
            setting.setInt(LiplisDefine.PREFS_MODE, this.mode);
            setting.setInt(LiplisDefine.PREFS_SPEED, this.speed);
            setting.setInt(LiplisDefine.PREFS_TALK_CHASE, this.talkChase);
            setting.setInt(LiplisDefine.PREFS_ALWAYSTOP, this.alwaysTop);//
            setting.setInt(LiplisDefine.PREFS_MOUSE_CTLS, this.mouseCtrl);
            setting.setInt(LiplisDefine.PREFS_DROP_ON, this.dropOn);
            setting.setInt(LiplisDefine.PREFS_DISC_WINDFOW_ON, this.discWindowOn);
            setting.setInt(LiplisDefine.PREFS_HELTH, this.lpsHelth);
            setting.setInt(LiplisDefine.PREFS_OUTRANGE_RECOVERY, this.lpsOutRangeRecovery);
            
            setting.setString(LiplisDefine.PREFS_NICO_ID, this.nicoId);
            setting.setString(LiplisDefine.PREFS_NICO_PASS, this.nicoPass);
            setting.setString(LiplisDefine.PREFS_DOWN_PATH, this.downPath);
            setting.setInt(LiplisDefine.PREFS_NOTICE, this.downNotice);
            setting.setInt(LiplisDefine.PREFS_WINDOW, this.window);
            setting.setInt(LiplisDefine.PREFS_REGION_CD, this.regionCd);
            setting.setInt(LiplisDefine.PREFS_MENU_TYPE, this.menuType);
            setting.setInt(LiplisDefine.PREFS_MENU_TIMING, this.menuTiming);

            setting.setInt(LiplisDefine.PREFS_TOPIC_NEWS, this.lpsTopicNews);
            setting.setInt(LiplisDefine.PREFS_TOPIC_2CH, this.lpsTopic2ch);
            setting.setInt(LiplisDefine.PREFS_TOPIC_NICO, this.lpsTopicNico);
            setting.setInt(LiplisDefine.PREFS_TOPIC_RSS, this.lpsTopicRss);
            setting.setInt(LiplisDefine.PREFS_TOPIC_TWITTER, this.lpsTopicTwitter);
            setting.setInt(LiplisDefine.PREFS_TOPIC_TWITTER_PB, this.lpsTopicTwitterPu);
            setting.setInt(LiplisDefine.PREFS_TOPIC_TWITTER_MY, this.lpsTopicTwitterMy);
            setting.setInt(LiplisDefine.PREFS_TOPIC_HOUR, this.lpsTopicHour);
            setting.setInt(LiplisDefine.PREFS_TOPIC_ALREADY, this.lpsAlready);
            setting.setInt(LiplisDefine.PREFS_TOPIC_TWITTER_MODE, this.lpsTwitterMode);
            setting.setInt(LiplisDefine.PREFS_TOPIC_RUNOUT, this.lpsRunout);

            setting.setInt(LiplisDefine.PREFS_VOICE_ON, this.lpsVoiceOn);
            setting.setInt(LiplisDefine.PREFS_VOICE_SELECT, this.lpsVoiceSelect);
            setting.setString(LiplisDefine.PREFS_VOICE_VRPATH_SOFTALK, this.lpsVoiceVRPathSofTalk);
            setting.setString(LiplisDefine.PREFS_VOICE_VRPATH_YUKARI, this.lpsVoiceVRPathYukari);
            setting.setString(LiplisDefine.PREFS_VOICE_VRPATH_TOMOE, this.lpsVoiceVRPathTomoe);
            setting.setString(LiplisDefine.PREFS_VOICE_VRPATH_ZUNKO, this.lpsVoiceVRPathZunko);

            setting.setInt(LiplisDefine.PREFS_TWITTER_ACTIVATE, this.twitterActivate);

            setting.saveSettings();
        }
        #endregion

        /// <summary>
        /// 座標のチェック
        /// </summary>
        #region checkPos
        private void checkPos()
        {
            int h, w;
            h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            if (locationX < 0 || locationX > w) { locationX = 100; }
            if (locationY < 0 || locationY > h) { locationY = 100; }
        }
        #endregion

        /// <summary>
        /// ニュースフラグの取得
        /// </summary>
        #region getNewsFlg
        public string getNewsFlg()
        {
            return lpsTopicNews + "," + lpsTopic2ch + "," + lpsTopicNico + "," + lpsTopicRss + "," + lpsTopicTwitterPu + "," + lpsTopicTwitterMy + "," + lpsTopicTwitter;
        }
        #endregion

        /// <summary>
        /// ウインドウパスを取得する
        /// </summary>
        #region getWindowPath
        public string getWindowPath()
        {
            string path = LpsPathController.getSkinPath() + "\\" + loadSkin + "\\window\\" + getWindowName();

            if (LpsLiplisUtil.ExistsFile(path))
            {
            }
            else
            {
                try
                {
                    path = LpsPathController.getSkinPath() + "\\" + loadSkin + "\\window\\" + getWindowName(99);
                    using (Bitmap b = FctCreateFromResource.getResourceBitmap(LiplisDefine.RES_WINDOW))
                    {
                        b.Save(path);
                    }
                }
                catch
                {
                    
                }
            }

            return path;
        }
        public string getWindowPath(int window)
        {
            string path = LpsPathController.getSkinPath() + "\\" + loadSkin + "\\window\\" + getWindowName(window);

            if (LpsLiplisUtil.ExistsFile(path))
            {
            }
            else
            {
                path = LpsPathController.getSkinPath() + "\\" + loadSkin + "\\window\\" + getWindowName(99);
                using (Bitmap b = FctCreateFromResource.getResourceBitmap(LiplisDefine.RES_WINDOW))
                {
                    b.Save(path);
                }
            }

            return path;
        }
        #endregion


        /// <summary>
        /// ウインドウ名を取得する
        /// </summary>
        #region getWindowName
        public string getWindowName()
        {
            switch(this.window)
            {
                case 0:
                    return LiplisDefine.LPS_WINDOW_1;
                case 1:
                    return LiplisDefine.LPS_WINDOW_2;
                case 2:
                    return LiplisDefine.LPS_WINDOW_3;
                case 3:
                    return LiplisDefine.LPS_WINDOW_4;
                case 4:
                    return LiplisDefine.LPS_WINDOW_5;
                case 5:
                    return LiplisDefine.LPS_WINDOW_6;
                case 6:
                    return LiplisDefine.LPS_WINDOW_7;
                default:
                    return LiplisDefine.LPS_WINDOW_DEFAULT;
            }
        }
        public string getWindowName(int window)
        {
            switch (window)
            {
                case 0:
                    return LiplisDefine.LPS_WINDOW_1;
                case 1:
                    return LiplisDefine.LPS_WINDOW_2;
                case 2:
                    return LiplisDefine.LPS_WINDOW_3;
                case 3:
                    return LiplisDefine.LPS_WINDOW_4;
                case 4:
                    return LiplisDefine.LPS_WINDOW_5;
                case 5:
                    return LiplisDefine.LPS_WINDOW_6;
                case 6:
                    return LiplisDefine.LPS_WINDOW_7;
                default:
                    return LiplisDefine.LPS_WINDOW_DEFAULT;
            }
        }
        #endregion

        //===========================================================
        //　　　　　　　　　　　セッター
        //===========================================================

        #region セッター
        
        /// <summary>
        /// UIDセッター
        /// setUid
        /// </summary>
        /// <param name="uid">UID</param>
        private void setUid(string uid)
        {
            if (uid.Equals(""))
            {
                this.uid = FctGUIDCreater.createLiplisGuid();
            }
            else
            {
                this.uid = uid;
            }
        }

        /// <summary>
        /// ロードスキンセッター
        /// setLoadSkin
        /// </summary>
        /// <param name="uid">loadSkin</param>
        private void setLoadSkin(string loadSkin)
        {
            if (loadSkin.Equals(""))
            {
                loadSkin = "Lili";
            }
            this.loadSkin = loadSkin;
        }

        /// <summary>
        /// ロケーションXセッター
        /// </summary>
        /// <param name="x"></param>
        private void setLocationX(int x)
        {
            this.locationX = x;
        }

        /// <summary>
        /// ロケーションYセッター
        /// </summary>
        /// <param name="y"></param>
        private void setLocationY(int y)
        {
            this.locationY = y;
        }

        /// <summary>
        /// モードセッター
        /// </summary>
        /// <param name="mode"></param>
        public void setMode(int mode)
        {
            this.mode = mode;
            switch(mode)
            {
                case 0:
                    lpsInterval = 0;
                    mode = 9;
                    break;
                case 1:
                    lpsInterval = 50;
                    break;
                case 2:
                    lpsInterval = 100;
                    break;
                case 3:
                    lpsInterval = 200;
                    break;
                case 4:
                    lpsInterval = 300;
                    break;
                case 5:
                    lpsInterval = 600;
                    break;
                case 6:
                    lpsInterval = 1200;
                    break;
                case 7:
                    lpsInterval = 1800;
                    break;
                case 8:
                    lpsInterval = 3000;
                    break;
                case 9:
                    lpsInterval = 0;
                    break;
                default:
                    lpsInterval = 100;
                    break;
            }
        }

        /// <summary>
        /// 話速度セッター
        /// </summary>
        /// <param name="speed"></param>
        public void setSpeed(int speed)
        {
            this.speed = speed;
            switch (speed)
            {
                case 0:
                    lpsReftesh = 0;
                    break;
                case 1:
                    lpsReftesh = 1;
                    break;
                case 2:
                    lpsReftesh = 2;
                    break;
                default:
                    lpsReftesh = 3;
                    break;
            }
        }

        /// <summary>
        /// トークチェイスセッター
        /// </summary>
        /// <param name="talkChase"></param>
        private void setTalkChase(int talkChase)
        {
            this.talkChase = talkChase;
        }

        /// <summary>
        /// いつもトップ
        /// </summary>
        /// <param name="talkChase"></param>
        private void setAlwaysTop(int alwaysTop)
        {
            this.alwaysTop = alwaysTop;
        }
        

        /// <summary>
        /// マウスコントロールセッター
        /// </summary>
        /// <param name="mouseCtlr"></param>
        private void setMouseCtrl(int mouseCtlr)
        {
            this.mouseCtrl = mouseCtrl;
        }

        /// <summary>
        /// ドロップオンセッター
        /// </summary>
        /// <param name="dropOn"></param>
        private void setDropOn(int dropOn)
        {
            this.dropOn = dropOn;
        }

        /// <summary>
        /// ニコIDセッター
        /// </summary>
        /// <param name="id"></param>
        private void setNicoId(string id)
        {
            this.nicoId = id;
        }


        /// <summary>
        /// ニコIDゲッター
        /// </summary>
        /// <param name="id"></param>
        public string getNicoId()
        {
            if (this.nicoId.Equals(""))
            {
                return LiplisDefine.NICO_DEFAULT_ID;
            }
            else
            {
                return this.nicoId;
            }
        }

        /// <summary>
        /// ニコパスセッター
        /// </summary>
        /// <param name="pass"></param>
        private void setNicoPass(string pass)
        {
            this.nicoPass = pass;
        }

        /// <summary>
        /// ニコパスゲッター
        /// </summary>
        /// <param name="pass"></param>
        public string getNicoPass()
        {
            if (this.nicoPass.Equals(""))
            {
                return LiplisDefine.NICO_DEFAULT_PASS;
            }
            else
            {
                return this.nicoPass;
            }
        }

        /// <summary>
        /// ダウンパスセッター
        /// </summary>
        /// <param name="path"></param>
        private void setDownPath(string downPath)
        {
            this.downPath = downPath;
        }

        /// <summary>
        /// ゲットパスセッター
        /// </summary>
        /// <param name="path"></param>
        public string getDownPath()
        {
            if (downPath == "")
            {
                return downPath;
            }
            else
            {
                return LpsPathController.getDownPath();
            }
        }

        /// <summary>
        /// ダウンパスセッター
        /// </summary>
        /// <param name="path"></param>
        private void setDiscWindowOn(int discWindowOn)
        {
            this.discWindowOn = discWindowOn;
        }
       

        /// <summary>
        /// ダウンパスセッター
        /// </summary>
        /// <param name="path"></param>
        private void setDownNotice(int downNotice)
        {
            this.downNotice = downNotice;
        }

        /// <summary>
        /// ウインドウセッター
        /// </summary>
        /// <param name="window"></param>
        private void setWindow(int window)
        {
            this.window = window;
        }

        /// <summary>
        /// 健康状態反映
        /// </summary>
        /// <param name="window"></param>
        private void setHelth(int lpsHelth)
        {
            this.lpsHelth = lpsHelth;
        }

        /// <summary>
        /// 範囲外復帰
        /// </summary>
        /// <param name="window"></param>
        private void setOutRangeRecovery(int lpsOutRangeRecovery)
        {
            this.lpsOutRangeRecovery = lpsOutRangeRecovery;
        }
        

        /// <summary>
        /// リージョンコードの設定
        /// </summary>
        /// <param name="path"></param>
        private void setRegionCd(int regionCd)
        {
            this.regionCd = regionCd;
        }

        /// <summary>
        /// メニュータイプの設定
        /// </summary>
        /// <param name="path"></param>
        private void setMenuType(int　menuType)
        {
            this.menuType = menuType;
        }

        /// <summary>
        /// メニュータイミングの設定
        /// </summary>
        /// <param name="path"></param>
        private void setMenuTiming(int menuTiming)
        {
            this.menuTiming = menuTiming;
        }

        /// <summary>
        /// ニュースフラグの設定
        /// </summary>
        /// <param name="path"></param>
        private void setLpsTopicNews(int lpsTopicNews)
        {
            this.lpsTopicNews = lpsTopicNews;
        }

        /// <summary>
        /// 2chフラグの設定
        /// </summary>
        /// <param name="path"></param>
        private void setLpsTopic2ch(int lpsTopic2ch)
        {
            this.lpsTopic2ch = lpsTopic2ch;
        }

        /// <summary>
        /// ニコニコフラグの設定
        /// </summary>
        /// <param name="path"></param>
        private void setLpsTopicNico(int lpsTopicNico)
        {
            this.lpsTopicNico = lpsTopicNico;
        }

        /// <summary>
        /// RSSフラグの設定
        /// </summary>
        /// <param name="path"></param>
        private void setLpsTopicRss(int lpsTopicRss)
        {
            this.lpsTopicRss = lpsTopicRss;
        }

        /// <summary>
        /// ツイッターフラグの設定
        /// </summary>
        /// <param name="path"></param>
        private void setLpsTopicTwitter(int lpsTopicTwitter)
        {
            this.lpsTopicTwitter = lpsTopicTwitter;
        }

        /// <summary>
        /// ツイッターマイフラグの設定
        /// </summary>
        /// <param name="path"></param>
        private void setLpsTopicTwitterMy(int lpsTopicTwitterMy)
        {
            this.lpsTopicTwitterMy = lpsTopicTwitterMy;
        }

        /// <summary>
        /// ツイッターパブリックフラグの設定
        /// </summary>
        /// <param name="path"></param>
        private void setLpsTopicTwitterPb(int lpsTopicTwitterPb)
        {
            this.lpsTopicTwitterPu = lpsTopicTwitterPb;
        }

        /// <summary>
        /// ボイスオンフラグの設定
        /// </summary>
        /// <param name="path"></param>
        private void setLpsVoiceOn(int lpsVoiceOn)
        {
            this.lpsVoiceOn = lpsVoiceOn;
        }

        /// <summary>
        /// 使用エンジン番号設定
        /// </summary>
        /// <param name="path"></param>
        private void setLpsVoiceSelect(int lpsVoiceSelect)
        {
            this.lpsVoiceSelect = lpsVoiceSelect;
        }

        /// <summary>
        /// ソフトークパス
        /// </summary>
        /// <param name="path"></param>
        private void setLpsVoiceVRPathSofTalk(string lpsVoiceVRPathSofTalk)
        {
            this.lpsVoiceVRPathSofTalk = lpsVoiceVRPathSofTalk;
        }

        /// <summary>
        /// 結月ゆかりパス
        /// </summary>
        /// <param name="path"></param>
        private void setLpsVoiceVRPathYukari(string lpsVoiceVRPathYukari)
        {
            this.lpsVoiceVRPathYukari = lpsVoiceVRPathYukari;
        }

        /// <summary>
        /// 民安ともえパス
        /// </summary>
        /// <param name="path"></param>
        private void setLpsVoiceVRPathTomoe(string lpsVoiceVRPathTomoe)
        {
            this.lpsVoiceVRPathTomoe = lpsVoiceVRPathTomoe;
        }

        /// <summary>
        /// 東北ずん子パス
        /// </summary>
        /// <param name="path"></param>
        private void setLpsVoiceVRPathZunko(string lpsVoiceVRPathZunko)
        {
            this.lpsVoiceVRPathZunko = lpsVoiceVRPathZunko;
        }

        /// <summary>
        /// 選択されているボイスロイドを取得する
        /// </summary>
        /// <returns></returns>
        public msgVoiceRoid getSelectedVoiceRoid()
        {
            switch (lpsVoiceSelect)
            {
                case 0:
                    return new msgVoiceRoid("", "");
                case 1://ソフトーク
                    return new msgVoiceRoid(LiplisDefine.LPS_VOICE_ROID_SOFTALK, lpsVoiceVRPathSofTalk);
                case 2://結月ゆかり
                    return new msgVoiceRoid(LiplisDefine.LPS_VOICE_ROID_YUKARI, lpsVoiceVRPathYukari);
                case 3://民安ともえ
                    return new msgVoiceRoid(LiplisDefine.LPS_VOICE_ROID_TOMOE, lpsVoiceVRPathTomoe);
                case 4://東北ずん子
                    return new msgVoiceRoid(LiplisDefine.LPS_VOICE_ROID_ZUNKO, lpsVoiceVRPathZunko);
                default:
                    return new msgVoiceRoid("", "");
            }
        }

        /// <summary>
        /// ツイッター認証フラグ
        /// </summary>
        /// <param name="path"></param>
        public void setTwitterActivate(int twitterActivate)
        {
            this.twitterActivate = twitterActivate;
        }

        /// <summary>
        /// ニュース時間範囲を設定する
        /// </summary>
        /// <param name="hourWord"></param>
        public void setLpsNewsHourStr(string hourWord)
        {
            switch(hourWord)
            {
                case LiplisDefine.LPS_TOPIC_HOUR_RANGE_1HOUR:
                    lpsTopicHour = 0;
                    break;
                case LiplisDefine.LPS_TOPIC_HOUR_RANGE_3HOUR:
                    lpsTopicHour = 1;
                    break;
                case LiplisDefine.LPS_TOPIC_HOUR_RANGE_6HOUR:
                    lpsTopicHour = 2;
                    break;
                case LiplisDefine.LPS_TOPIC_HOUR_RANGE_12HOUR:
                    lpsTopicHour = 3;
                    break;
                case LiplisDefine.LPS_TOPIC_HOUR_RANGE_1DAY:
                    lpsTopicHour = 4;
                    break;
                case LiplisDefine.LPS_TOPIC_HOUR_RANGE_3DAY:
                    lpsTopicHour = 5;
                    break;
                case LiplisDefine.LPS_TOPIC_HOUR_RANGE_7DAY:
                    lpsTopicHour = 6;
                    break;
                case LiplisDefine.LPS_TOPIC_HOUR_RANGE_UNLIMITED:
                    lpsTopicHour = 7;
                    break;
                default:
                    lpsTopicHour = 1;
                    break;
            }
        }
        public void setLpsNewsHour(int lpsNewsHour)
        {
            this.lpsTopicHour = lpsNewsHour;
        }

        public void setLpsAlready(int lpsAlready)
        {
            this.lpsAlready = lpsAlready;
        }

        public void setLpsTwitterMode(int lpsTwitterMode)
        {
            this.lpsTwitterMode = lpsTwitterMode;
        }

        public void setLpsRunout(int lpsRunout)
        {
            this.lpsRunout = lpsRunout;
        }
        #endregion






    }


 
}
