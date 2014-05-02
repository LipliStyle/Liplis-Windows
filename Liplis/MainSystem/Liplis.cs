//=======================================================================
//  ClassName : Liplis
// ■概要      : Liplisのメインクラス
//
//
//■ Liplis3.0
//　2013/05/04 Liplis2.2 音声機能実装
//             動画ダウンロード機能の廃止
//             設定画面変更
//             使用APIの変更(Clalis3.1)
//  2013/05/22 Liplis2.2.0 ログの改善
//  2013/05/28 Liplis2.2.1 デバッグモードの追加
//  2013/06/20 Liplis2.3.0 音声機能完全実装
//                         仕様の統一化
//  2013/06/22 Liplis3.0.0 機能改善レベルからバージョニング見直し
//  2013/06/22 Liplis3.0.1 マニュアル作成、リンク
//  2013/06/23 Liplis3.0.2 LiplisMini対応、ソース微修正
//  2013/06/23 Liplis3.0.3 LiplisMini対応
//  2013/07/06 Liplis3.0.4 ニコニコサムネイル最新API対応
//  2013/08/31 Liplis3.0.5 ico_sleep.png→ico_zzz.pngに変更
//                         →互換性保持のため、ico_sleep.pngを見つけたら変換する。
//                         ObjWindowFileクラスにて、互換性チェックを行うようにした。
//                         Atにてフォントカラーを適用するように実装
//
//  2013/10/27 Liplis3.1.0 ツイッターパブリックTL、マイTL、フィルター機能実装、Clalis3.2適用
//  2013/10/29 Liplis3.2.0 バッテリー管理システム実装、小破、中破、大破追加
//  2014/01/08 Liplis3.2.1 話題が尽きた時に何もしない設定が有効になっていないバグ修正
//                         キャラクター変更時、キャラクター選択画面をHIDEするように修正
//　2014/01/14 Liplis3.2.2 起動時や移動時にWEBセーブメソッドを実行しないように修正
//                         時刻範囲設定のバグ修正
//                         ログタイトル右クリック処理の修正
//                         画面外復帰機能
//　2014/01/14 Liplis3.2.3 再起動時、話題取得範囲が正しく設定されなかったバグを修正
//　2014/02/02 Liplis3.2.4 ツイッター認証が行えないバグを修正
//
//  2014/04/07 Liplis4.0.0 Clalis4.0対応
//  2014/04/16             おしゃべり内容の解析方法修正
//  2014/04/20             データ受信処理修正(リアルタイム受信)    
//
//  2014/04/28 Liplis4.0.1 ウインドウデザイン変更(ボタン、感情値追加)
//                         ツイート機能追加  
//  2014/05/01 Liplis4.0.2 タッチ定義追加
//  2014/05/01 Liplis4.0.3 時報追加
//
//
// ■運用
//  ミニからのオーバーライドを必要とする場合は、メソッドをvirtualにした上で、
//  「☆Miniオーバーライド」をコメントに付け加える。
//
// ■デバッグモード(/debugオプション)
//　　デバッグモード時はlogフォルダにファイルが出力される。
//
// ■オプション
//　　/debug デバッグモードON
//
//
//
// Copyright(c) 2014 LipliStyle さちん MITライセンス
//=======================================================================
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Liplis.Activity;
using Liplis.Common;
using Liplis.Fct;
using Liplis.Msg;
using Liplis.Web;

namespace Liplis.MainSystem
{
    public partial class Liplis : BaseSystem
    {
        ///====================================================================
        ///
        ///                       初期化処理
        ///                         
        ///====================================================================

        ///=====================================
        /// オブジェクト
        protected ObjSetting          os;
        protected ObjSkinSetting      oss;
        protected ObjSkinSettingList  ossList;
        protected ObjBodyList         obl;
        protected ObjWindowFile       owf;
        protected ObjBattery          obtry;
        protected ObjLiplisChat       olc;
        protected ObjLiplisTouch      olt;
        protected ObjTopic            otp;
        protected ObjBroom            obr;

        ///=====================================
        /// Liplis制御オブジェクト
        protected ObjBody                 ob;
        protected LiplisIcon              li;
        protected LiplisPopIcon           lpi;
        protected LiplisTaskBar           ltb;
        public LpsVoiceRoid               lvr;
        protected Bitmap                  paintBuffBmp;

        ///=====================================
        /// Activity
        protected ActivityTalk           at;
        protected ActivityPic            ap;
        protected ActivitySetting        ast;
        protected ActivityLog            al;
        protected ActivityChar           ac;
        internal  ActivityTopicRegist    ar;
        protected ActivityNicoBrowser    anb;
         

        ///=====================================
        /// 制御プロパティ
        protected MsgShortNews liplisNowTopic;
        protected string liplisNowWord  = "";	    //現在読み込みの単語(cntLnwでカウント)
        protected string liplisChatText = "";		//現在読み込みの文字(cntLctでカウント)
        protected int cntLct            = 0;		//リプリスチャットテキストカウント
        protected int cntLnw            = 0;		//リプリスナウワードカウント
        protected int nowPoint          = 0;		//現在感情ポイント
        protected int nowPos            = 0;		//現在品詞ポイント
        protected int cntMouth          = 0;        //1回/1s
        protected int cntBlink          = 0;        //1回/5～10s
        protected int nowBlink          = 0;        //まばたき現在値
        protected int prvBlink          = 0;        //まばたき前回値
        protected int nowDirection      = 0;        //方向 0:左向き　1:右向き
        protected int prvDirection      = 0;        //方向 前回値
        protected int cntSlow           = 0;        //スローカウント

        protected int nowEmotion = 0;		//感情現在値
        protected int prvEmotion = 0;		//感情前回値
        protected MsgEmotion sumEmotion;    //感情蓄積値

        ///=====================================
        /// ツイッター発言控え
        protected string liplisTweetMessege = "";
        protected string liplisTweetMessegeTitle = "";

        ///=====================================
        /// 発言数
        public int talkCnt { get; set; } 

        ///=====================================
        /// タイマー
        #region timer
        protected System.Threading.Timer timUpdate;             //タイマー
        protected System.Threading.Timer timRefresh;             //タイマー
        protected int cntEnd = 30;                              //エンドカウンター
        #endregion

        //=====================================
        /// デリゲート
        #region delegate

        //=============
        /// デリゲート宣言
        protected static LpsDelegate.dlgVoidToVoid reqReflesh;
        protected static LpsDelegate.dlgS1ToVoid reqProcess;       

        #endregion

        ///=====================================
        /// フラグ
        #region flg
        protected int flgAlarm        = 0;
        protected bool flgConnect     = false;
        protected bool flgBodyChencge = false;
        protected bool flgChatting    = false;
        protected bool flgSkip        = false;
        protected bool flgSkipping    = false;
        protected bool flgSitdown     = false;
        protected bool flgThinking    = false;
        protected bool flgEnd         = false;
        protected bool flgRestart     = false;
        protected bool flgMinimize    = false;
        protected bool flgTagCheck    = false;
        protected bool flgDebug       = false;    //2013/05/28 デバッグモード追加
        protected bool flgOutputDemo  = false;    //2013/05/28 デバッグモード追加
        #endregion

        ///=====================================
        /// 設定値
        //NOTE : liplisRefreshRate * liplisRate = 更新間隔 (updateCntに関連)
        #region 設定値
        protected static int liplisInterval = 100;		    //インターバル
        protected static int liplisRefresh = 10;			//リフレッシュレート
        #endregion

        ///=====================================
        /// チャット制御カウント
        #region チャット制御カウント
        protected static int cntUpdate = 10;
        #endregion

        ///=====================================
        /// 開放オブジェクト
        #region 開放オブジェクト
        public ActivityTalk getAt()   { return this.at; }
        public ObjWindowFile getOwf() { return this.owf; }
        public ObjBattery getObtry() { return this.obtry; }
        public bool getFlgEnd()       { return this.flgEnd; }
        #endregion

        ///=====================================
        /// バッテリー関連オブジェクト
        #region バッテリー関連オブジェクト
        protected int prevBatteryLevel      { get; set; }
        protected int liplisBatteryLevel    { get; set; }
        protected PowerStatus ps;
        #endregion

        ///====================================================================
        ///
        ///                          onCreate
        ///                         
        ///====================================================================
       
        /// <summary>
        /// コンストラクター
        /// </summary>
        #region コンストラクター
        public Liplis() : base()
        {
            //要素の初期化
            InitializeComponent();

            //コマンドライン引数の処理 
            //ver2.2.1 2013/05/28
            getCommand();

            //初期化
            initObject();

            //デリゲートの初期化
            initDelegate();

            //アクティビティの初期化
            initActivity();

            //設定の読み込み
            loadSetting();
        }
        #endregion

        /// <summary>
        /// getCommand
        /// コマンドラインの処理
        /// ver2.2.1 2013/05/28
        /// </summary>
        #region getCommand
        protected void getCommand()
        {
            flgDebug      = false;
            flgOutputDemo = false;

            foreach (string cmd in Environment.GetCommandLineArgs())
            {
                switch(cmd)
                {
                    case "/debug":
                        flgDebug = true;
                        break;
                    case "/demo":
                        flgOutputDemo = true;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        
        /// <summary>
        /// Liplisの初期化
        /// 主にクラスの初期化
        /// ☆Miniオーバーライド
        /// 
        /// 2014/04/20 Liplis4.0 総合エモーション追加
        /// </summary>
        #region initObject
        protected virtual void initObject()
        {
            //バッテリーオブジェクト
            ps = SystemInformation.PowerStatus;

            //設定ファイルの読み込み
            os = new ObjSetting();

            //スキンファイルの読み込み
            ossList = new ObjSkinSettingList();

            //対象スキンの取得
            oss = ossList.loadTargetSkin(os.loadSkin);

            //ボディリストの初期化
            obl = new ObjBodyList(os.loadSkin);

            //ボディを初期化しておく
            ob = obl.getLiplisBody(0, 0);

            //チャットファイルの読み込み
            olc = new ObjLiplisChat(os.loadSkin);

            //タッチ定義の追加
            olt = new ObjLiplisTouch(os.loadSkin);

            //ウインドウファイルの初期化
            owf = new ObjWindowFile(os.loadSkin);

            //バッテリーオブジェクトの初期あk
            obtry = new ObjBattery(os.loadSkin, this.ps);

            //トピックオブジェクトの初期化
            otp = new ObjTopic(os, oss);

            //アイコンクラスの初期化
            li = new LiplisIcon(this);

            //アイコンクラスv2初期化
            lpi = new LiplisPopIcon(this, 0);

            //リプリスタスクバー
            ltb = new LiplisTaskBar(this);

            //ほうきオブジェクトの初期化
            obr = new ObjBroom();

            ///2014/04/20 Liplis4.0 総合エモーション追加
            //総合エモーション
            sumEmotion = new MsgEmotion();

            //アイコンクラスを連動登録
            this.AddOwnedForm(li);
        }
        #endregion

        /// <summary>
        /// loadSetting
        /// 設定の読み込み
        /// </summary>
        #region loadSetting
        protected void loadSetting()
        {
            liplisInterval = os.lpsInterval;
            liplisRefresh = os.lpsReftesh;
            liplisBatteryLevel = (int)(ps.BatteryLifePercent * 100);
            prevBatteryLevel = (int)(ps.BatteryLifePercent * 100);
        }
        #endregion

        /// <summary>
        /// initDelegate
        /// デリゲートの初期化
        /// </summary>
        #region initDelegate
        protected bool initDelegate()
        {
            try
            {
                //リフレッシュデリゲート
                reqReflesh = new LpsDelegate.dlgVoidToVoid(dlgReflesh);
                reqProcess = new LpsDelegate.dlgS1ToVoid(dlgProcess);
                return true;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion

        /// <summary>
        /// initActivity
        /// アクティビティの初期化
        /// ☆Miniオーバーライド
        /// </summary>
        #region initActivity
        protected virtual bool initActivity()
        {
            try
            {
                //トークアクティビティの初期化
                at = new ActivityTalk(this,os,oss);

                //ニコブラウザ
                anb = new ActivityNicoBrowser();

                //ピクチャーアクティビティの初期化
                ap = new ActivityPic();

                //Rss
                ar = new ActivityTopicRegist(this,this.os, this.owf);

                //Log
                al = new ActivityLog(this, this.os, obr, this.owf);

                //Char
                ac = new ActivityChar(this, ossList, this.os.loadSkin, this.owf);

                //設定
                ast = new ActivitySetting(this, this.os, this.owf);

                //オーナーフォーム登録
                this.AddOwnedForm(at);
                this.AddOwnedForm(anb);
                this.AddOwnedForm(ap);
                return true;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                          onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// onDelete
        /// 終了
        /// ☆Miniオーバーライド
        /// </summary>
        #region onDelete
        protected virtual void onDelete()
        {
            try
            {
                //まずはタイマーをとめる
                flgAlarm = 0;               //フラグ0
                timRefresh.Dispose();       //タイマーの破棄
                timUpdate.Dispose();        //タイマーの破棄

                //ボイスロイドスレッドの終了
                lvr.Dispose(); lvr = null;

                //セーブをしておく
                if(os != null)os.setPreferenceData();

                //おそうじ
                if (obr != null) obr.deleteAllTempFile();

                //アクティビティの破棄
                if (at != null) Invoke(new LpsDelegate.dlgVoidToVoid(at.dispose));
                if (anb != null) Invoke(new LpsDelegate.dlgVoidToVoid(anb.dispose));
                if (ap != null) Invoke(new LpsDelegate.dlgVoidToVoid(ap.dispose));
                if (ar != null) Invoke(new LpsDelegate.dlgVoidToVoid(ar.dispose));
                if (li != null) Invoke(new LpsDelegate.dlgVoidToVoid(li.Dispose));
                if (at != null) Invoke(new LpsDelegate.dlgVoidToVoid(al.dispose));
                if (ac != null) Invoke(new LpsDelegate.dlgVoidToVoid(ac.dispose));
                if (ast != null) Invoke(new LpsDelegate.dlgVoidToVoid(ast.dispose));

                //オブジェクトの破棄
                os = null;
                oss = null;
                ossList = null;
                obl = null;
                olc = null;
                owf.Dispose(); owf = null;
                otp = null;
                obr = null;

                //最後に自分自身の終了
                if (!flgRestart)
                {
                    flgEnd = true;
                    Invoke(new LpsDelegate.dlgVoidToVoid(this.Close));
                }
                else
                {
                    Invoke(new LpsDelegate.dlgVoidToVoid(restert));
                }
            }
            catch(Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                if (!flgRestart)
                {
                    flgEnd = true;
                }
                else
                {

                }
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                          onLoad
        ///                         
        ///====================================================================

        /// <summary>
        /// initLiplis
        /// Liplisの初期化
        /// </summary>
        #region initLiplis
        protected void initLiplis()
        {
            //一時背景の設定と透過の設定
            setWindowProperty(FctCreateFromResource.getTranse());

            //ウインドウサイズ設定
            setSize(obl.width, obl.height);    

            //座標設定
            setLocation(os.locationX, os.locationY);

            //方向チェック
            setDirection();
        }
        #endregion

        /// <summary>
        /// initFlg
        /// フラグの初期化
        /// </summary>
        #region initFlg
        protected void initFlg()
        {
            //発言数の初期化
            talkCnt = 0;                  
        }
        #endregion

        /// <summary>
        /// createTimer
        /// タイマーの初期化
        /// </summary>
        #region initTimer
        protected bool initTimer()
        {
            try
            {
                //リフレッシュタイマーの初期化
                timUpdate = new System.Threading.Timer(new TimerCallback(onUpdate), null, 0, 100);
                timRefresh = new System.Threading.Timer(new TimerCallback(onProcess), null, 0, 1000);

                return true;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion

        /// <summary>
        /// initChatInfo
        /// チャット情報の初期化
        /// </summary>
        #region initChatInfo
        protected void initChatInfo()
        {
            try
            {
                //ナウワードの初期化
                liplisNowWord = "";

                //ナウ文字インデックスの初期化
                cntLct = 0;

                //ナウワードインデックスの初期化
                cntLnw = 0;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// initActivityTalk
        /// トークアクティビティを初期化する
        /// ☆Miniオーバーライド
        /// </summary>
        #region initActivityTalk
        protected virtual void initActivityTalk()
        {
            try
            {
                Invoke(new LpsDelegate.dlgVoidToVoid(ap.Hide));
                Invoke(new LpsDelegate.dlgVoidToVoid(anb.Hide));  

                if (!liplisNowTopic.url.Equals("")){
                    at.url = liplisNowTopic.url;
                    at.title = liplisNowTopic.title;
                    at.setWindowMode(LiplisDefine.WIN_MODE_WITH_URL);

                    ap.url = liplisNowTopic.url;

                    Invoke(new LpsDelegate.dlgVoidToVoid(at.activityInit));


                    if (os.discWindowOn == 1)
                    {
                        if (LpsLiplisUtil.domainCheck(liplisNowTopic.url, LpsDefineMost.URL_NICO_DOMAIN))
                        {
                            Invoke(new LpsDelegate.dlgS1ToVoid(anb.setImage), liplisNowTopic.url);
                        }
                        else
                        {
                            Invoke(new LpsDelegate.dlgS2ToVoid(ap.setImage), liplisNowTopic.jpgUrl, liplisNowTopic.url);
                        }
                    }
                    else
                    {
                        Invoke(new LpsDelegate.dlgS2ToVoid(ap.setImage), liplisNowTopic.jpgUrl, liplisNowTopic.url);
                    } 

                }//ウインドウモードをウィズURLモードに設定
                else
                {
                    at.setWindowMode(LiplisDefine.WIN_MODE_TEXT_ONLY);
                    anb.Hide();
                }//ウインドウモードをテキストモードに設定

                
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// initLiplisIcon
        /// リプリスボイスロイド
        /// </summary>
        #region initVoiceRoid
        protected void initVoiceRoid()
        {
            if (lvr != null)
            {
                lvr.Dispose();
            }

            lvr = new LpsVoiceRoid(os.getSelectedVoiceRoid());
        }
        #endregion

        /// <summary>
        /// initLiplisIcon
        /// アイコンとタスクバーを初期化する
        /// </summary>
        #region initIcoAndTaskBar
        protected void initIcoAndTaskBar()
        {
            this.li.setSurface(this.Left, this.Top, this.Width, this.Height);
            this.li.Show();

            this.ltb.Show();
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : reSetUpdateCount
        /// チャットの開始
        /// </summary>
        #region reSetUpdateCount
        protected void reSetUpdateCount()
        {
            try
            {
                if (liplisInterval == 0)
                {
                }
                else if (liplisInterval == 9999)
                {
                    cntUpdate = LpsLiplisUtil.getRandamInt(50, 600);
                }
                else
                {
                    cntUpdate = liplisInterval;
                    flgAlarm = 10;
                }
                
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                          onUpdate
        ///                         
        ///====================================================================

        /// <summary>
        /// onUpdate
        /// タイマー処理
        /// </summary>
        #region onUpdate
        protected void onUpdate(object o)
        {
            Application.DoEvents();

            if (flgAlarm == 12)
            {
                updateLiplis();
            }
            else if (flgAlarm == 10)
            {
                //カウントダウンフェーズ
                onCountDown();
            }
            else if (flgAlarm == 11)
            {
                updateBatteryInfo();
                nextLiplis();
            }
            else
            {

            }

            //トゥルーエンドカウント
            if (!flgEnd){}else{cntEnd--;if(cntEnd < 0){onDelete();}}
        }
        #endregion


        /// <summary>
        /// 1秒置きのプロセス処理
        /// 100msでやるまでもないことをこのプロセスで行う
        /// (処理が冗長だったり、1秒で十分なこと)
        /// </summary>
        #region onProcess
        protected void onProcess(object o)
        {
            try
            {
                Application.DoEvents();

                //時報チェック
                onTimeSignal();

                //サーフェス
                Invoke(
                    (MethodInvoker)delegate()
                    {
                        checkSurface();
                    }
                );

            }
            catch
            {
                Console.WriteLine("");
            }

        }
        #endregion
        
        ///====================================================================
        ///
        ///                          onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// WndProc
        /// メッセージ制御
        /// </summary>
        #region WndProc
        protected override void WndProc(ref Message m)
        {
            try
            {
                //--- サイズ変更の制御 ---
                if (m.Msg == LpsWindowsApiDefine.WM_SIZE)
                {
                    //WParamの値を評価
                    switch ((int)m.WParam)
                    {
                        case LpsWindowsApiDefine.SIZE_RESTORED:
                            //normalized();
                            //flgMinimized = false;
                            break;
                        case LpsWindowsApiDefine.SIZE_MINIMIZED:
                            //minimized();
                            //flgMinimized = true;
                            break;
                        case LpsWindowsApiDefine.SIZE_MAXIMIZED:     //最大化はリターン
                            return;
                        case LpsWindowsApiDefine.SIZE_MAXSHOW:
                            break;
                        case LpsWindowsApiDefine.SIZE_MAXHIDE:
                            break;
                    }
                }
                //アクティブ状態の変化
                if (m.Msg == LpsWindowsApiDefine.WM_ACTIVATE)
                {
                    switch ((int)m.WParam)
                    {
                        case LpsWindowsApiDefine.WA_INACTIVE:

                            //rcm.hideAllWindow();
                            //rcm.hideWindow();
                            break;
                    }
                }
                base.WndProc(ref m);
            }
            catch
            {
                //2011/11/06 おしゃべり停止対策
                flgChatting = false;
                //endApplication();
            }
        }
        #endregion

        /// <summary>
        /// OnPaint
        /// オンペイントのオーバーライド
        /// →paintObject
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            onPaint(e);
        }
        #endregion

        /// <summary>
        /// 
        /// イベントハンドラ
        /// </summary>
        #region イベントハンドラ

        /// <summary>
        /// リプリスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_Click
        private void Liplis_Click(object sender, EventArgs e)
        {
            checkClick(1);
        }
        #endregion

        /// <summary>
        /// Liplis_DoubleClick
        /// ダブルクリック
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        #region Liplis_DoubleClick
        protected void Liplis_DoubleClick(object sender, EventArgs e)
        {
            checkClick(2);
        }
        #endregion

        /// <summary>
        /// Liplis_DragDrop
        /// ドラッグドロップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_DragDrop
        protected void Liplis_DragDrop(object sender, DragEventArgs e)
        {
            Invoke(new LpsDelegate.dlgListStrToVoid(ar.dropDataCheckLiplis), FctDragData.getDropTextList(e));
        }
        #endregion

        /// <summary>
        /// Liplis_DragEnter
        /// ドラッグエンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_DragEnter
        protected void Liplis_DragEnter(object sender, DragEventArgs e)
        {
            FctDragData.getDrag(e);
        }
        #endregion

        /// <summary>
        /// Liplis_FormClosing
        /// フォームクロージング
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_FormClosing
        protected void Liplis_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flgEnd)
            {

            }
            else
            {
                e.Cancel = true;
                onEnd();
            }
        }
        #endregion

        /// <summary>
        /// Liplis_KeyDown
        /// キーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_KeyDown
        protected void Liplis_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// Liplis_KeyUp
        /// キーアップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_KeyUp
        protected void Liplis_KeyUp(object sender, KeyEventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// Liplis_MouseEnter
        /// マウスエンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_MouseEnter
        protected void Liplis_MouseEnter(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// Liplis_MouseLeave
        /// マウスリーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_MouseLeave
        protected void Liplis_MouseLeave(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// Liplis_MouseUp
        /// マウスアップ
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_MouseUp
        protected void Liplis_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUp(sender, e);
        }
        #endregion

        /// <summary>
        /// Liplis_MouseDown
        /// マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_MouseDown
        public void Liplis_MouseDown(object sender, MouseEventArgs e)
        {
            onMouseDown(sender, e);
        }
        #endregion

        /// <summary>
        /// Liplis_MouseMove
        /// マウスムーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_MouseMove
        public void Liplis_MouseMove(object sender, MouseEventArgs e)
        {
            onMouseMove(sender, e);
        }
        #endregion

        /// <summary>
        /// サイズ変更時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Liplis_Resize
        protected void Liplis_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.flgMinimize = false;
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.flgMinimize = true; ;
            }
        }
        #endregion


        #endregion

        /// <summary>
        /// updateFirst
        /// ロードイベント
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        #region Liplis_Load
        protected void Liplis_Load(object sender, EventArgs e)
        {
            //まずアラームフラグを0に退避
            flgAlarm = 0;

            //リプリスの初期化
            initLiplis();

            //音声管理クラスの初期化
            initVoiceRoid();

            //フラグの初期化
            initFlg();

            //インターネット接続チェック
            flgConnect = ObjNetwork.checkInterNetConnection();

            //更新タイマースタート
            initTimer();

            //オパシティ1
            this.Opacity = 1;

            //アイコン表示
            initIcoAndTaskBar();

            //アクティビティを召還
            callActivityTalk();

            //チャットインフォの初期化
            initChatInfo();

            //挨拶
            greet();

            //nowLoadBody = obl.getBody(0,0);

            //バックグラウンド設定
            //this.BackgroundImage = nowLoadBody.getBody(0,0);
        }
        #endregion

        /// <summary>
        /// initActivityTalk
        /// アクティビティの表示
        /// ☆Miniオーバーライド
        /// </summary>
        #region callActivityTalk
        protected virtual void callActivityTalk()
        {
            //ロケーションの設定
            at.setLocation(this.Location.X, this.Location.Y, this.Width, this.Height, this.nowDirection);
            ap.setLocation(this.Location.X, this.Location.Y, this.Width, this.Height, at.Width, at.Height, this.nowDirection);
            anb.setLocation(this.Location.X, this.Location.Y, this.Width, this.Height, at.Width, at.Height, this.nowDirection);

            //ショウする
            at.Show();
            anb.Show();
        }
        #endregion

        /// <summary>
        /// onRecive
        /// メッセージ処理
        /// </summary>
        /// <param name="action">アクションコマンド</param>
        /// <param name="param">パラメータ</param>
        /// <returns></returns>
        #region onRecive
        public string onRecive(int action, List<string> param)
        {
            string result = "COMMAND_OK";

            switch (action)
            {
                case LiplisDefine.LM_NEXT:
                    onNextClick();
                    break;
                case LiplisDefine.LM_LOG:
                    callLogWindow();
                    break;
                case LiplisDefine.LM_SLEEP:
                    onSleep();
                    break;
                case LiplisDefine.LM_MINIMIZE:
                    onMiniNormal(param[0]);
                    break;
                case LiplisDefine.LM_NORMALIZE:
                    onMiniNormal("");
                    break;
                case LiplisDefine.LM_RSS:
                    callRssWindow();
                    break;
                case LiplisDefine.LM_TWITTER:
                    callTwitterWindow();
                    break;
                case LiplisDefine.LM_FILTER:
                    callFilterWindow();
                    break;
                case LiplisDefine.LM_CHAR:
                    callCharWindow();
                    break;
                //case LiplisDefine.LM_WIDGET:
                //    callWidgetWindow();
                //	break;
                case LiplisDefine.LM_SETTING:
                    callSettingWindow();
                    break;
                case LiplisDefine.LM_END:
                    onEnd();
                    break;
                case LiplisDefine.LM_CHANGE:
                    onChange(param[0]);
                    break;
                case LiplisDefine.LM_LOAD_SETTING:
                    loadSetting();
                    break;
                case LiplisDefine.LM_TWT_ACT:
                    callTwitterActivation();
                    break;     
                case LiplisDefine.LM_RELOAD_VOICELOID:
                    initVoiceRoid();
                    break;
                case LiplisDefine.LM_BATTERY:
                    talkBatteryInfo();
                    break;
                case LiplisDefine.LM_OUTRANGE_RECOVERY:
                    outRangeRecovery();
                    break;
                case LiplisDefine.LM_TWEET:
                    tweet(param[0]);
                    break;

                //ウインドウズ実行コマンド
                #region ウインドウズ実行コマンド
                case LiplisDefine.LM_WIN_FONTS: reqProcess("fonts");break;
                case LiplisDefine.LM_WIN_IEXPLORE: reqProcess("iexplore");break;
                case LiplisDefine.LM_WIN_WUPDMGR: reqProcess("wupdmgr");break;
                case LiplisDefine.LM_WIN_WSCUI_CPL: reqProcess("wscui.cpl");break;
                case LiplisDefine.LM_WIN_EXCEL: reqProcess("excel");break;
                case LiplisDefine.LM_WIN_CONTROL_ADMINTOOLS: reqProcess("controladmintools"); break;
                case LiplisDefine.LM_WIN_CMD: reqProcess("cmd");break;
                case LiplisDefine.LM_WIN_CONTROL: reqProcess("control");break;
                case LiplisDefine.LM_WIN_SERVICES_MSC: reqProcess("services.msc");break;
                case LiplisDefine.LM_WIN_MSCONFIG: reqProcess("msconfig");break;
                case LiplisDefine.LM_WIN_DFRG_MSC: reqProcess("dfrg.msc");break;
                case LiplisDefine.LM_WIN_DEVMGMT_MSC: reqProcess("devmgmt.msc");break;
                case LiplisDefine.LM_WIN_CALC: reqProcess("calc");break;
                case LiplisDefine.LM_WIN_POWERPNT: reqProcess("powerpnt");break;
                case LiplisDefine.LM_WIN_TIMEDATE_CPL: reqProcess("timedate.cpl");break;
                case LiplisDefine.LM_WIN_APPWIZ_CPL: reqProcess("appwiz.cpl");break;
                case LiplisDefine.LM_WIN_NOTEPAD: reqProcess("notepad");break;
                case LiplisDefine.LM_WIN_REGEDIT: reqProcess("regedit");break;
                case LiplisDefine.LM_WIN_WINWORD: reqProcess("winword");break;
                case LiplisDefine.LM_WIN_WRITE: reqProcess("write");break;
                case LiplisDefine.LM_WIN_MOVIEMK: reqProcess("moviemk");break;
                case LiplisDefine.LM_WIN_MSIMN: reqProcess("msimn");break;
                case LiplisDefine.LM_WIN_NUSRMGR_CPL: reqProcess("nusrmgr.cpl");break;
                case LiplisDefine.LM_WIN_WMPLAYER: reqProcess("wmplayer");break;
                case LiplisDefine.LM_WIN_MSMSGS: reqProcess("msmsgs");break;
                case LiplisDefine.LM_WIN_EXPLORER: reqProcess("explorer");break;
                case LiplisDefine.LM_WIN_CONTROL_DESKTOP: reqProcess("controldesktop"); break;
                case LiplisDefine.LM_WIN_DESK_CPL: reqProcess("desk.cpl");break;
                case LiplisDefine.LM_WIN_COMPMGMT_MSC: reqProcess("compmgmt.msc");break;
                case LiplisDefine.LM_WIN_DCOMCNFG: reqProcess("dcomcnfg");break;
                case LiplisDefine.LM_WIN_MMSYS_CPL: reqProcess("mmsys.cpl");break;
                case LiplisDefine.LM_WIN_SYSDM_CPL: reqProcess("sysdm.cpl");break;
                case LiplisDefine.LM_WIN_WUAUCPL_CPL: reqProcess("wuaucpl.cpl");break;
                case LiplisDefine.LM_WIN_TASKMGR: reqProcess("taskmgr");break;
                case LiplisDefine.LM_WIN_DISKMGMT_MSC: reqProcess("diskmgmt.msc");break;
                case LiplisDefine.LM_WIN_POWERCFG_CPL: reqProcess("powercfg.cpl");break;
                case LiplisDefine.LM_WIN_PERFMON_MSC: reqProcess("perfmon.msc");break;
                case LiplisDefine.LM_WIN_PERFMON: reqProcess("perfmon");break;
                case LiplisDefine.LM_WIN_FIREWALL_CPL: reqProcess("firewall.cpl");break;
                case LiplisDefine.LM_WIN_CONTROL_FOLDERS: reqProcess("controlfolders"); break;
                case LiplisDefine.LM_WIN_CONTROL_PRINTERS: reqProcess("controlprinters"); break;
                case LiplisDefine.LM_WIN_HDWWIZ_CPL: reqProcess("hdwwiz.cpl");break;
                case LiplisDefine.LM_WIN_FSQUIRT: reqProcess("fsquirt");break;
                case LiplisDefine.LM_WIN_DDESHARE: reqProcess("ddeshare");break;
                case LiplisDefine.LM_WIN_DXDIAG: reqProcess("dxdiag");break;
                case LiplisDefine.LM_WIN_DISKPART: reqProcess("diskpart");break;
                case LiplisDefine.LM_WIN_VERIFIER: reqProcess("verifier");break;
                case LiplisDefine.LM_WIN_IEXPRESS: reqProcess("iexpress");break;
                case LiplisDefine.LM_WIN_LOGOFF: reqProcess("logoff");break;
                case LiplisDefine.LM_WIN_MRT: reqProcess("mrt");break;
                case LiplisDefine.LM_WIN_CONF: reqProcess("conf");break;
                case LiplisDefine.LM_WIN_ODBCCP32_CPL: reqProcess("odbccp32.cpl");break;
                case LiplisDefine.LM_WIN_PASSWORD_CPL: reqProcess("password.cpl");break;
                case LiplisDefine.LM_WIN_PRINTERS: reqProcess("printers");break;
                case LiplisDefine.LM_WIN_NTMSMGR_MSC: reqProcess("ntmsmgr.msc");break;
                case LiplisDefine.LM_WIN_STICPL_CPL: reqProcess("sticpl.cpl");break;
                case LiplisDefine.LM_WIN_CLICONFG: reqProcess("cliconfg");break;
                case LiplisDefine.LM_WIN_TCPTEST: reqProcess("tcptest");break;
                case LiplisDefine.LM_WIN_TELNET: reqProcess("telnet");break;
                case LiplisDefine.LM_WIN_WMIMGMT_MSC: reqProcess("wmimgmt.msc");break;
                case LiplisDefine.LM_WIN_SYSKEY: reqProcess("syskey");break;
                case LiplisDefine.LM_WIN_TOURSTART: reqProcess("tourstart");break;
                case LiplisDefine.LM_WIN_DRWTSN32: reqProcess("drwtsn32");break;
                case LiplisDefine.LM_WIN_WINVER: reqProcess("winver");break;
                case LiplisDefine.LM_WIN_WAB: reqProcess("wab");break;
                case LiplisDefine.LM_WIN_WABMIG: reqProcess("wabmig");break;
                case LiplisDefine.LM_WIN_EVENTVWR_MSC: reqProcess("eventvwr.msc");break;
                case LiplisDefine.LM_WIN_ICWCONN1: reqProcess("icwconn1");break;
                case LiplisDefine.LM_WIN_INETCPL_CPL: reqProcess("inetcpl.cpl");break;
                case LiplisDefine.LM_WIN_CIADV_MSC: reqProcess("ciadv.msc");break;
                case LiplisDefine.LM_WIN_PACKAGER: reqProcess("packager");break;
                case LiplisDefine.LM_WIN_MAGNIFY: reqProcess("magnify");break;
                case LiplisDefine.LM_WIN_CONTROL_KEYBOARD: reqProcess("controlkeyboard"); break;
                case LiplisDefine.LM_WIN_FSMGMT_MSC: reqProcess("fsmgmt.msc");break;
                case LiplisDefine.LM_WIN_CLIPBRD: reqProcess("clipbrd");break;
                case LiplisDefine.LM_WIN_GPEDIT_MSC: reqProcess("gpedit.msc");break;
                case LiplisDefine.LM_WIN_JOY_CPL: reqProcess("joy.cpl");break;
                case LiplisDefine.LM_WIN_SYSEDIT: reqProcess("sysedit");break;
                case LiplisDefine.LM_WIN_MSINFO32: reqProcess("msinfo32");break;
                case LiplisDefine.LM_WIN_CERTMGR_MSC: reqProcess("certmgr.msc");break;
                case LiplisDefine.LM_WIN_OSK: reqProcess("osk");break;
                case LiplisDefine.LM_WIN_SPIDER: reqProcess("spider");break;
                case LiplisDefine.LM_WIN_DIALER: reqProcess("dialer");break;
                case LiplisDefine.LM_WIN_CONTROL_SCHEDTASKS: reqProcess("controlschedtasks"); break;
                case LiplisDefine.LM_WIN_INTL_CPL: reqProcess("intl.cpl");break;
                case LiplisDefine.LM_WIN_CHKDSK: reqProcess("chkdsk");break;
                case LiplisDefine.LM_WIN_WINCHAT: reqProcess("winchat");break;
                case LiplisDefine.LM_WIN_CLEANMGR: reqProcess("cleanmgr");break;
                case LiplisDefine.LM_WIN_RASPHONE: reqProcess("rasphone");break;
                case LiplisDefine.LM_WIN_TELEPHON_CPL: reqProcess("telephon.cpl");break;
                case LiplisDefine.LM_WIN_MOBSYNC: reqProcess("mobsync");break;
                case LiplisDefine.LM_WIN_CONTROL_NETCONNECTIONS: reqProcess("controlnetconnections"); break;
                case LiplisDefine.LM_WIN_NCPA_CPL: reqProcess("ncpa.cpl");break;
                case LiplisDefine.LM_WIN_MSHEARTS: reqProcess("mshearts");break;
                case LiplisDefine.LM_WIN_HYPERTRM: reqProcess("hypertrm");break;
                case LiplisDefine.LM_WIN_PINBALL: reqProcess("pinball");break;
                case LiplisDefine.LM_WIN_MIGWIZ: reqProcess("migwiz");break;
                case LiplisDefine.LM_WIN_SIGVERIF: reqProcess("sigverif");break;
                case LiplisDefine.LM_WIN_CONTROL_FONTS: reqProcess("controlfonts"); break;
                case LiplisDefine.LM_WIN_FREECELL: reqProcess("freecell");break;
                case LiplisDefine.LM_WIN_MSPAINT: reqProcess("mspaint");break;
                case LiplisDefine.LM_WIN_PBRUSH: reqProcess("pbrush");break;
                case LiplisDefine.LM_WIN_HELPCTR: reqProcess("helpctr");break;
                case LiplisDefine.LM_WIN_RSOP_MSC: reqProcess("rsop.msc");break;
                case LiplisDefine.LM_WIN_WINMINE: reqProcess("winmine");break;
                case LiplisDefine.LM_WIN_CONTROL_MOUSE: reqProcess("controlmouse"); break;
                case LiplisDefine.LM_WIN_MAIN_CPL: reqProcess("main.cpl");break;
                case LiplisDefine.LM_WIN_CHARMAP: reqProcess("charmap");break;
                case LiplisDefine.LM_WIN_ACCESS_CPL: reqProcess("access.cpl");break;
                case LiplisDefine.LM_WIN_ACCWIZ: reqProcess("accwiz");break;
                case LiplisDefine.LM_WIN_UTILMAN: reqProcess("utilman");break;
                case LiplisDefine.LM_WIN_NTMSOPRQ_MSC: reqProcess("ntmsoprq.msc");break;
                case LiplisDefine.LM_WIN_MSTSC: reqProcess("mstsc");break;
                case LiplisDefine.LM_WIN_REGEDIT32: reqProcess("regedit32");break;
                case LiplisDefine.LM_WIN_SECPOL_MSC: reqProcess("secpol.msc");break;
                case LiplisDefine.LM_WIN_LUSRMGR_MSC: reqProcess("lusrmgr.msc");break;
                case LiplisDefine.LM_WIN_NETSETUP_CPL: reqProcess("netsetup.cpl");break;
                #endregion

                //感情制御コマンド
                #region 感情制御コマンド
                //case "exTestJoy1": setEmotion(ComDefine.EXPRESSION_JOY1, "喜び1"); break;
                //case "exTestJoy2": setEmotion(ComDefine.EXPRESSION_JOY2, "喜び2"); break;
                //case "exTestJoy3": setEmotion(ComDefine.EXPRESSION_JOY3, "喜び3"); break;
                //case "exTestJoy-1": setEmotion(ComDefine.EXPRESSION_JOY_1, "喜び-1"); break;
                //case "exTestJoy-2": setEmotion(ComDefine.EXPRESSION_JOY_2, "喜び-2"); break;
                //case "exTestJoy-3": setEmotion(ComDefine.EXPRESSION_JOY_3, "喜び-3"); break;

                //case "exTestAdmiration1": setEmotion(ComDefine.EXPRESSION_ADMIRATION1, "好意1"); break;
                //case "exTestAdmiration2": setEmotion(ComDefine.EXPRESSION_ADMIRATION2, "好意2"); break;
                //case "exTestAdmiration3": setEmotion(ComDefine.EXPRESSION_ADMIRATION3, "好意3"); break;
                //case "exTestAdmiration-1": setEmotion(ComDefine.EXPRESSION_ADMIRATION_1, "好意-1"); break;
                //case "exTestAdmiration-2": setEmotion(ComDefine.EXPRESSION_ADMIRATION_2, "好意-2"); break;
                //case "exTestAdmiration-3": setEmotion(ComDefine.EXPRESSION_ADMIRATION_3, "好意-3"); break;

                //case "exTestPeace1": setEmotion(ComDefine.EXPRESSION_PEACE1, "安心1"); break;
                //case "exTestPeace2": setEmotion(ComDefine.EXPRESSION_PEACE2, "安心2"); break;
                //case "exTestPeace3": setEmotion(ComDefine.EXPRESSION_PEACE3, "安心3"); break;
                //case "exTestPeace-1": setEmotion(ComDefine.EXPRESSION_PEACE_1, "安心-1"); break;
                //case "exTestPeace-2": setEmotion(ComDefine.EXPRESSION_PEACE_2, "安心-2"); break;
                //case "exTestPeace-3": setEmotion(ComDefine.EXPRESSION_PEACE_3, "安心-3"); break;

                //case "exTestExstasy1": setEmotion(ComDefine.EXPRESSION_ECSTASY1, "快感1"); break;
                //case "exTestExstasy2": setEmotion(ComDefine.EXPRESSION_ECSTASY2, "快感2"); break;
                //case "exTestExstasy3": setEmotion(ComDefine.EXPRESSION_ECSTASY3, "快感3"); break;
                //case "exTestExstasy-1": setEmotion(ComDefine.EXPRESSION_ECSTASY_1, "快感-1"); break;
                //case "exTestExstasy-2": setEmotion(ComDefine.EXPRESSION_ECSTASY_2, "快感-2"); break;
                //case "exTestExstasy-3": setEmotion(ComDefine.EXPRESSION_ECSTASY_3, "快感-3"); break;

                //case "exTestAmazement1": setEmotion(ComDefine.EXPRESSION_AMAZEMENT1, "驚き1"); break;
                //case "exTestAmazement2": setEmotion(ComDefine.EXPRESSION_AMAZEMENT2, "驚き2"); break;
                //case "exTestAmazement3": setEmotion(ComDefine.EXPRESSION_AMAZEMENT3, "驚き3"); break;
                //case "exTestAmazement-1": setEmotion(ComDefine.EXPRESSION_AMAZEMENT_1, "驚き-1"); break;
                //case "exTestAmazement-2": setEmotion(ComDefine.EXPRESSION_AMAZEMENT_2, "驚き-2"); break;
                //case "exTestAmazement-3": setEmotion(ComDefine.EXPRESSION_AMAZEMENT_3, "驚き-3"); break;

                //case "exTestRage1": setEmotion(ComDefine.EXPRESSION_RAGE1, "怒り1"); break;
                //case "exTestRage2": setEmotion(ComDefine.EXPRESSION_RAGE2, "怒り2"); break;
                //case "exTestRage3": setEmotion(ComDefine.EXPRESSION_RAGE3, "怒り3"); break;
                //case "exTestRage-1": setEmotion(ComDefine.EXPRESSION_RAGE_1, "怒り-1"); break;
                //case "exTestRage-2": setEmotion(ComDefine.EXPRESSION_RAGE_2, "怒り-2"); break;
                //case "exTestRage-3": setEmotion(ComDefine.EXPRESSION_RAGE_3, "怒り-3"); break;

                //case "exTestInterest1": setEmotion(ComDefine.EXPRESSION_INTEREST1, "興味1"); break;
                //case "exTestInterest2": setEmotion(ComDefine.EXPRESSION_INTEREST2, "興味2"); break;
                //case "exTestInterest3": setEmotion(ComDefine.EXPRESSION_INTEREST3, "興味3"); break;
                //case "exTestInterest-1": setEmotion(ComDefine.EXPRESSION_INTEREST_1, "興味-1"); break;
                //case "exTestInterest-2": setEmotion(ComDefine.EXPRESSION_INTEREST_2, "興味-2"); break;
                //case "exTestInterest-3": setEmotion(ComDefine.EXPRESSION_INTEREST_3, "興味-3"); break;

                //case "exTestRespect1": setEmotion(ComDefine.EXPRESSION_RESPECT1, "尊敬1"); break;
                //case "exTestRespect2": setEmotion(ComDefine.EXPRESSION_RESPECT2, "尊敬2"); break;
                //case "exTestRespect3": setEmotion(ComDefine.EXPRESSION_RESPECT3, "尊敬3"); break;
                //case "exTestRespect-1": setEmotion(ComDefine.EXPRESSION_RESPECT_1, "尊敬-1"); break;
                //case "exTestRespect-2": setEmotion(ComDefine.EXPRESSION_RESPECT_2, "尊敬-2"); break;
                //case "exTestRespect-3": setEmotion(ComDefine.EXPRESSION_RESPECT_3, "尊敬-3"); break;

                //case "exTestCalmly1": setEmotion(ComDefine.EXPRESSION_CALMLY1, "冷静1"); break;
                //case "exTestCalmly2": setEmotion(ComDefine.EXPRESSION_CALMLY2, "冷静2"); break;
                //case "exTestCalmly3": setEmotion(ComDefine.EXPRESSION_CALMLY3, "冷静3"); break;
                //case "exTestCalmly-1": setEmotion(ComDefine.EXPRESSION_CALMLY_1, "冷静-1"); break;
                //case "exTestCalmly-2": setEmotion(ComDefine.EXPRESSION_CALMLY_2, "冷静-2"); break;
                //case "exTestCalmly-3": setEmotion(ComDefine.EXPRESSION_CALMLY_3, "冷静-3"); break;

                //case "exTestProud1": setEmotion(ComDefine.EXPRESSION_PROUD1, "誇り1"); break;
                //case "exTestProud2": setEmotion(ComDefine.EXPRESSION_PROUD2, "誇り2"); break;
                //case "exTestProud3": setEmotion(ComDefine.EXPRESSION_PROUD3, "誇り3"); break;
                //case "exTestProud-1": setEmotion(ComDefine.EXPRESSION_PROUD_1, "誇り-1"); break;
                //case "exTestProud-2": setEmotion(ComDefine.EXPRESSION_PROUD_2, "誇り-2"); break;
                //case "exTestProud-3": setEmotion(ComDefine.EXPRESSION_PROUD_3, "誇り-3"); break;
#endregion
                default:
                    result = "COMMAND_NG";
                    break;
            }

            return result;
        }
        public string onRecive(int action, string pParam)
        {
            List<string> param = new List<string>();
            param.Add(pParam);
            return onRecive(action, param);
        }
        #endregion

        /// <summary>
        /// onNextClick
        /// ネクストアイコンクリック
        /// </summary>
        #region onNextClick
        protected void onNextClick()
        {
            //チャット中チェック
            if (!flgChatting)
            {
                nextLiplis();
            }
            else
            {
                flgSkip = true;
            }
        }
        #endregion

        /// <summary>
        /// onEnd
        /// 終了処理
        /// </summary>
        #region onEnd
        protected void onEnd()
        {
            //既に終了処理が走っている場合は無効
            if (flgEnd) { return; }

            flgEnd = true;

            //アップデートカウントをほぼ無限に設定
            cntUpdate = 99999;

            //チャットを停止しておく
            chatStop();

            //お別れの挨拶セット
            goodBay();
        }
        #endregion


        /// <summary>
        /// onChange
        /// 交代処理
        /// </summary>
        #region onChange
        protected void onChange(string nextSkin)
        {
            //既に終了処理が走っている場合は無効
            if (flgEnd) { return; }

            //2014/01/08 ver3.2.1 acを消しておく
            ac.Hide();

            flgEnd = true; flgRestart = true;

            //アップデートカウントをほぼ無限に設定
            cntUpdate = 99999;

            //チャットを停止しておく
            chatStop();

            //ロードスキンを更新する
            os.loadSkin = nextSkin;
            os.setPreferenceData();

            //お別れの挨拶セット
            goodChange();
        }
        #endregion

        /// <summary>
        /// onSleep
        /// おやすみ処理
        /// </summary>
        #region onSleep
        protected void onSleep()
        {
            //最小化中は回避
            if (flgMinimize) { return; }

            if (flgSitdown)
            {
                //おやすみ→ウェイクアップ
                flgSitdown = false;
                standUp();
            }
            else
            {
                //ウェイクアップ→おやすみ
                flgSitdown = true;
                sitDown();
            }
        }
        #endregion

        /// <summary>
        /// 最小化処理メソッド
        /// </summary>
        #region minimized
        public void onMiniNormal(string param)
        {
            if (this.flgMinimize)
            {
                this.onNormalized();
            }
            else
            {
                this.onMinimized("");
            }
        }
        #endregion

        /// <summary>
        /// 最小化処理メソッド
        /// ☆Miniオーバーライド
        /// </summary>
        #region minimized
        public virtual void onMinimized(string param)
        {
            try
            {
                this.flgMinimize = true;
                this.WindowState = FormWindowState.Minimized;
                if (at != null)  { Invoke(new LpsDelegate.dlgVoidToVoid(at.onMinimize)); }
                if (ap != null)  { Invoke(new LpsDelegate.dlgVoidToVoid(ap.onMinimize)); }
                if (li != null)  { Invoke(new LpsDelegate.dlgVoidToVoid(li.onMinimize)); }
                if (ar != null)  { Invoke(new LpsDelegate.dlgVoidToVoid(ar.onMinimize)); }
                if (al != null)  { Invoke(new LpsDelegate.dlgVoidToVoid(al.onMinimize)); }
                if (ac != null)  { Invoke(new LpsDelegate.dlgVoidToVoid(ac.onMinimize)); }
                if (ast != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ast.onMinimize)); }

                if (ltb != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ltb.onMinimize)); }               
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 最小化復帰時メソッド
        /// ☆Miniオーバーライド
        /// </summary>
        #region normalized
        public virtual void onNormalized()
        {
            try
            {
                this.Show();
                this.flgMinimize = false;
                this.WindowState = FormWindowState.Normal;
                if (at != null){Invoke(new LpsDelegate.dlgVoidToVoid(at.onNormalize));}
                if (ap != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ap.onNormalize)); }
                if (li != null) { Invoke(new LpsDelegate.dlgVoidToVoid(li.onNormalize)); }
                if (ar != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ar.onNormalize)); }
                if (al != null) { Invoke(new LpsDelegate.dlgVoidToVoid(al.onNormalize)); }
                if (ac != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ac.onNormalize)); }
                if (ast != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ast.onNormalize)); }

                //再カウント
                reSetUpdateCount();
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                            Liplis制御
        ///                         
        ///====================================================================

        /// <summary>
        /// onPaint
        /// オンペイント
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        #region onPaint
        protected void onPaint(PaintEventArgs e)
        {
            try
            {
                Thread.Sleep(10);

                if (paintBuffBmp != null)
                {
                    paintBuffBmp.Dispose();
                    paintBuffBmp = null;
                }

                paintBuffBmp = (Bitmap)ob.getBody(nowBlink, cntMouth, nowDirection).Clone();

                e.Graphics.DrawImage(paintBuffBmp, obl.locX, obl.locY, obl.width, obl.height);
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// onCountDown
        /// カウントダウンイベント
        /// </summary>
        #region onCountDown
        protected void onCountDown()
        {
            try
            {
                if (flgEnd) { return; }

                Thread.Sleep(10);

                //カウントダウン
                cntUpdate--;

                //チャットフェーズに以降
                if (cntUpdate <= 0)
                {
                    flgAlarm = 11;
                }
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// マウスダウン時処理
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">MouseEventArgs</param>
        #region onMouseDown
        protected void onMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (os.mouseCtrl == 1)
                    {
                        if (ctrlKeyFlg)
                        {
                            mousePoint = new Point(e.X, e.Y);
                        }
                    }
                    else
                    {
                        //位置を記憶する
                        mousePoint = new Point(e.X, e.Y);

                    }

                    //右クリックメニューを閉じる
                    closeMenu();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    //位置を記憶する
                    mousePoint = new Point(this.Location.X + e.X, this.Location.Y + e.Y);
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// マウスムーブ時処理
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">MouseEventArgs</param>
        #region onMouseMove
        protected void onMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    if (os.mouseCtrl == 1)
                    {
                        //コントロールロックチェック
                        if (ctrlKeyFlg)
                        {
                            this.Left += e.X - mousePoint.X;
                            this.Top += e.Y - mousePoint.Y;
                            this.li.setSurface(this.Left, this.Top, this.Width, this.Height);
                        }
                    }
                    else
                    {
                        //位置を記憶する
                        this.Left += e.X - mousePoint.X;
                        this.Top += e.Y - mousePoint.Y;
                        this.li.setSurface(this.Left, this.Top, this.Width, this.Height);
                    }
                }

                //Liplis4.0.2 指定矩形チェック
                checkTouch(e.X, e.Y);

            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// マウスアップ時処理
        /// ☆Miniオーバーライド
        /// </summary>
        #region mouseUp
        public virtual void mouseUp(object sender, MouseEventArgs e)
        {
            os.locationX = this.Left;
            os.locationY = this.Top;
            os.setPreferenceDataSettingOnly();

            //方向チェック
            setDirection();

            //自動復帰
            outRangeRecoveryAuto();

            //ウインドウ表示領域の再計算
            at.setLocation(this.Location.X, this.Location.Y, this.Width, this.Height, this.nowDirection);
            ap.setLocation(this.Location.X, this.Location.Y, this.Width, this.Height, at.Width, at.Height, this.nowDirection);
            anb.setLocation(this.Location.X, this.Location.Y, this.Width, this.Height, at.Width, at.Height, this.nowDirection);

            //右クリックなら、CMPを出現させる
            if (e.Button == MouseButtons.Right)
            {
                onRightClick(); 
            }
        }
        #endregion

        /// <summary>
        /// 右クリック時処理
        /// </summary>
        #region onRightClick
        protected void onRightClick()
        {
            try
            {
                //メニュータイミング 1: 右クリックのとき
                if (os.menuTiming == 1)
                {
                    if (os.menuType == 0)
                    {
                        if (cmp.flgActive)
                        {
                            closeMenuCircle();
                        }
                        else
                        {
                            showMenuCircle();
                        }
                    }
                    else
                    {
                        if (li.flgActive)
                        {
                            closeMenuBox();
                        }
                        else
                        {
                            showMenuBox();
                        }
                    }
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 右クリックメニュー閉じる
        /// </summary>
        #region closeRcm
        public void closeMenu()
        {
            try
            {
                //メニュータイミング 1: 右クリックのとき
                if (os.menuTiming == 1)
                {
                    if (os.menuType == 0)
                    {
                        if (cmp.flgActive)
                        {
                            closeMenuCircle();
                        }
                    }
                    else
                    {
                        if (li.flgActive)
                        {
                            closeMenuBox();
                        }
                    }
                }
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion 

        /// <summary>
        /// 時報チェック
        /// </summary>
        #region onTimeSignal
        protected void onTimeSignal()
        {
            //現在時刻取得
            DateTime dt = DateTime.Now;

            //時報チェック
            if (dt.Minute == 0 && dt.Second == 0)
            {
                timeSignal(dt.Hour);
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                            メニュー制御
        ///                         
        ///====================================================================

        /// <summary>
        /// サーフェスチェック情報更新
        /// </summary>
        #region checkSurface
        protected void checkSurface()
        {
            try
            {
                //タイミング設定0 : マウスオンの場合
                if (os.menuTiming == 0)
                {
                    if (!flgMinimize)
                    {
                        //タイプ0サークル
                        if (os.menuType == 0)
                        {
                            if (!cmp.flgActive)
                            {
                                if (checkRect(MousePosition.X, MousePosition.Y))
                                {
                                    showMenuCircle();
                                }
                            }
                            else
                            {
                                if (!checkRect(MousePosition.X, MousePosition.Y))
                                {
                                    closeMenuCircle();
                                }
                            }
                        }
                        //タイプ1ボックス
                        else
                        {
                            if (!li.flgActive)
                            {
                                if (checkRect(MousePosition.X, MousePosition.Y))
                                {
                                    showMenuBox();
                                }
                            }
                            else
                            {
                                if (!checkRect(MousePosition.X, MousePosition.Y))
                                {
                                    closeMenuBox();
                                }
                            }
                        }
                    }
                }
                //タイミング設定0 右クリックの場合
                else
                {
                    if (!flgMinimize)
                    {
                        //タイプ0サークル
                        if (os.menuType == 0)
                        {
                            if (cmp.flgActive)
                            {
                                if (!checkRect(MousePosition.X, MousePosition.Y))
                                {
                                    closeMenuCircle();
                                }
                            }
                        }
                        //タイプ1ボックス
                        else
                        {
                            if (li.flgActive)
                            {
                                if (!checkRect(MousePosition.X, MousePosition.Y))
                                {
                                    closeMenuBox();
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName :  updateSleepIcon
        /// スリープアイコンの更新
        /// </summary>
        #region updateSleepIcon
        protected bool updateSleepIcon()
        {
            try
            {
                if (flgSitdown) { Invoke(new LpsDelegate.dlgVoidToVoid(li.setWaikUpIcon)); }    //ベルアイコンに変更
                else { Invoke(new LpsDelegate.dlgVoidToVoid(li.setSleepIcon)); }                //zzzアイコンに変更

                initCmp(LpsLiplisUtil.boolToBit(flgSitdown));
                this.Refresh();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// メニューを開く
        /// </summary>
        #region showMenuBox
        protected void showMenuBox()
        {
            li.Show();
            li.faidIn();
            li.flgActive = true;
        }
        #endregion

        /// <summary>
        /// メニューを閉じる
        /// </summary>
        #region closeMenuBox
        protected void closeMenuBox()
        {
            li.faidOut();
            li.flgActive = false;
        }
        #endregion

        /// <summary>
        /// サークルメニューを開く
        /// </summary>
        #region showMenuCircle
        protected virtual void showMenuCircle()
        {
            cmp.flgActive = true;
            cmp.setLocation(this, this.Width / 2, 100);
            Invoke(new LpsDelegate.dlgVoidToVoid(cmp.Popup));
        }
        #endregion

        /// <summary>
        /// サークルメニューを閉じる
        /// </summary>
        #region closeMenuCircle
        protected void closeMenuCircle()
        {
            cmp.flgActive = false;
            Invoke(new LpsDelegate.dlgVoidToVoid(cmp.PopupComplete));
        }
        #endregion

        /// <summary>
        /// CMPの初期化
        /// </summary>
        /// <param name="sleepMode"></param>
        #region initCmp
        private void initCmp(int sleepMode)
        {
            if (this.cmp != null)
            {
                closeMenuCircle();
                this.cmp.Dispose();
                this.cmp = null;
            }

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Liplis));
            this.cmp = new CircularMenu.CircularMenuPopup();
            this.cmp.ClosingAnimation.FrameImageEffect = ((CircularMenu.IFrameModifier)(resources.GetObject("resource.FrameImageEffect")));
            this.cmp.ClosingAnimation.LayoutAnimator = ((CircularMenu.IFrameLayoutManager)(resources.GetObject("resource.LayoutAnimator")));
            this.cmp.flgActive = false;
            this.cmp.OpeningAnimation.FrameImageEffect = ((CircularMenu.IFrameModifier)(resources.GetObject("resource.FrameImageEffect1")));
            this.cmp.OpeningAnimation.LayoutAnimator = ((CircularMenu.IFrameLayoutManager)(resources.GetObject("resource.LayoutAnimator1")));
            this.cmp.Radius = 120;
            this.cmp.ToolTip.BackgroundColor = System.Drawing.SystemColors.Info;
            this.cmp.ToolTip.BackgroundOpacity = ((byte)(175));
            this.cmp.ToolTip.BorderColor = System.Drawing.SystemColors.InfoText;
            this.cmp.ToolTip.BorderOpacity = ((byte)(255));
            this.cmp.ToolTip.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.cmp.ToolTip.ForegroundColor = System.Drawing.SystemColors.InfoText;
            this.cmp.ToolTip.ForegroundOpacity = ((byte)(255));
            this.cmp.ToolTipOverride = null;

            //testcode
            lpi.Dispose();
            lpi = null;
            lpi = new LiplisPopIcon(this, sleepMode);

        }
        #endregion
        

        ///====================================================================
        ///
        ///                          状態取得
        ///                         
        ///====================================================================

        /// <summary>
        /// Liplisの表示座標を返す
        /// </summary>
        #region getRect
        protected Rectangle getRect()
        {
            return new Rectangle(this.Left, this.Top, obl.width, obl.height);
        }
        #endregion

        /// <summary>
        /// Liplisの表示座標内かどうかチェックする
        #region いらないかもしれない説明
        /// xはLiplisDefine.LPS_ICON_DIFで定義(一番最初は56を採用(12 32 12))
        /// top - x  →  |             
        ///          　　|ここにアイコン表示
        /// top      →　|
        ///          　　| 
        ///          　　|ここにLiplis表示
        /// left     →  | -----------------------------  |  ← left + width
        ///          　　|
        ///          　　|
        ///top + height
        /// 
        #endregion
        /// </summary>
        #region checkRect
        protected bool checkRect(int x, int y)
        {
            try
            {
                //アイコンの高さを考慮(+LPS_ICON_DIF)いらないかもしれな説明を読めば明快
                return ((x >= Left && x <= this.Left + this.Width) && (y >= Top - LiplisDefine.LPS_ICON_DIF && y <= this.Top + this.Height));
            }
            catch(Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
            
        }
        #endregion

        /// <summary>
        /// Liplisの表示座標を返す
        /// </summary>
        #region getScreanRect
        protected Rectangle getScreanRect()
        {
            return Screen.PrimaryScreen.Bounds;
        }
        #endregion


        /// <summary>
        /// リプリスの真横の座標を返す
        /// </summary>
        #region getWindowPoint
        public Point getWindowPoint()
        {
            Point result = new Point();

            try
            {
                if (nowDirection == 1)
                {
                    result.Y = this.Top;
                    result.X = this.Left + this.Width;
                }
                else
                {
                    result.Y = this.Top;
                    result.X = this.Left - this.Width;
                }

                return result;
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return new Point(0, 0);
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                          Liplis制御依頼
        ///                  かならずデリゲート経由で操作依頼
        ///                         
        ///====================================================================

        /// <summary>
        /// リフレッシュ処理
        /// </summary>
        #region dlgReflesh
        protected void dlgReflesh()
        {
            try
            {
                this.Refresh();
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                            右クリック制御(廃止)
        ///                         
        ///====================================================================


        /// <summary>
        /// rcmを生成する
        /// </summary>
        #region createRcm
        protected void createRcm()
        {
            try
            {
                //rcm = new winPopList(this, ComPathController.getSkinPath() + so.loadSkinName + "\\" + ComDefine.SETTING_DIR_RCM + "\\" + ComDefine.SETTING_RCM_XML_FILE, "menu");
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// rcmを閉じる
        /// </summary>
        #region deleteRcm
        protected void deleteRcm()
        {
            try
            {
                //rcm.disposePopWindow();
                //rcm.Close();
                //rcm.Dispose();
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 右クリックメニューを呼び出す
        /// </summary>
        #region callRcm
        public void callRcm(int x, int y)
        {
            try
            {
                //if (!this.flgRightClickMenu)
                //{
                //    rcm.Show();
                //    rcm.setLocation(x, y);
                //    rcm.checkDirection();
                //    flgRightClickMenu = true;
                //}
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 右クリックメニュー閉じる
        /// </summary>
        #region closeRcm
        public void closeRcm()
        {
            //rcm.hideAllWindow();
            //rcm.hideWindow();
            //flgRightClickMenu = false;
        }
        #endregion 

        ///====================================================================
        ///
        ///                          チャット関連
        ///                         
        ///====================================================================
        
        /// <summary>
        /// greet
        /// 挨拶する
        /// </summary>
        #region greet
        protected void greet()
        {
            //挨拶の選定
            liplisNowTopic = olc.getGreetMessage(LiplisDefine.CHAT_DEF_GREET);

            //空だったらろーでぃんぐなう♪
            if (liplisNowTopic.getMessage().Equals(""))
            {
                liplisNowTopic = new MsgShortNews("ろーでぃんぐなう♪", 0, 0);
            }

            //チャット情報の初期化
            initChatInfo();

            //トークアクティビティの初期化
            initActivityTalk();
            
            //おしゃべりスレッドスタート
            chatStart();
        }
        #endregion

        /// <summary>
        /// goodBay
        /// さようなら
        /// </summary>
        #region goodBay
        protected void goodBay()
        {
            //挨拶の選定
            liplisNowTopic = olc.getGreetMessage(LiplisDefine.CHAT_DEF_GOODBYE);

            //空だったらろーでぃんぐなう♪
            if (liplisNowTopic.getMessage().Equals(""))
            {
                liplisNowTopic = new MsgShortNews("終了いたします。", 0, 0);
            }

            //チャット情報の初期化
            initChatInfo();

            //トークアクティビティの初期化
            initActivityTalk();

            //おしゃべりスレッドスタート
            chatStart();
        }
        #endregion

        /// <summary>
        /// goodChange
        /// よき交代を
        /// </summary>
        #region goodChange
        protected void goodChange()
        {
            //挨拶の選定
            liplisNowTopic = olc.getGreetMessage(LiplisDefine.CHAT_DEF_CHANGE);

            //空だったらろーでぃんぐなう♪
            if (liplisNowTopic.getMessage().Equals(""))
            {
                liplisNowTopic = new MsgShortNews("交代します。", 0, 0);
            }

            //チャット情報の初期化
            initChatInfo();

            //トークアクティビティの初期化
            initActivityTalk();

            //おしゃべりスレッドスタート
            chatStart();
        }
        #endregion

        /// <summary>
        /// talkBatteryInfo
        /// バッテリー情報をおしゃべりする
        /// </summary>
        #region talkBatteryInfo
        protected void talkBatteryInfo()
        {
            try
            {
                //チャットを停止させておく
                chatStop();

                //バッテリーのセリフを取得
                liplisNowTopic = olc.getBatteryInfo(liplisBatteryLevel, obtry.batteryExists);

                //チャット情報の初期化
                initChatInfo();

                //トークアクティビティの初期化
                initActivityTalk();

                //おしゃべりスレッドスタート
                chatStart();

                LpsLogControllerCus.d("コンバーティッド * " + liplisNowTopic.result);
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }



        }
        #endregion

        /// <summary>
        /// talk
        /// おしゃべりする
        /// </summary>
        /// <param name="talkString">おしゃべり内容</param>
        /// <param name="emotion">+-ありのエモーション</param>
        #region talk
        protected void talk(string talkString, int emotion)
        {
            try
            {
                //チャットを停止させておく
                chatStop();

                //なうトピックを上書き
                liplisNowTopic = new MsgShortNews(talkString, emotion, emotion);

                //チャット情報の初期化
                initChatInfo();

                //トークアクティビティの初期化
                initActivityTalk();

                //おしゃべりスレッドスタート
                chatStart();
                }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        protected void talk(MsgShortNews talkMessage)
        {
            try
            {
                //チャットを停止させておく
                chatStop();

                //なうトピックを上書き
                liplisNowTopic = talkMessage;

                //チャット情報の初期化
                initChatInfo();

                //トークアクティビティの初期化
                initActivityTalk();

                //おしゃべりスレッドスタート
                chatStart();
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// nextLiplis
        /// 次の話題を取得する
        /// 
        /// 2014/01/07 ver3.2.1 話題が尽きた時の挙動の修正 booleanの結果によりリターン
        /// </summary>
        #region nextLiplis
        protected void nextLiplis()
        {
            try
            {
                //話題取得フェーズ終了まで0に設定
                flgAlarm = 0;
                //チャット中かつなうトピックがNullでなければ回避
                //2011/11/06 ナウトピックがヌルの場合は既にチャットが終了しているとみなす。
                if (flgChatting && liplisNowTopic != null) { return; }
                //座り中なら回避
                if (flgSitdown)  { return; }
                //スキップ中なら回避
                if (flgSkipping) { return; }
                //最小化中なら回避
                if (flgMinimize) { return; }
                //話題を取得する
                if (!getTopic()) { reSetUpdateCount(); return; }    //2014/01/07 ver3.2.1 話題が尽きた時の挙動の修正 booleanの結果によりリターン
                //チャット情報の初期化
                initChatInfo();
                //トークアクティビティの初期化
                initActivityTalk();
                //アップデートカウントの更新
                chatStart();
                //発言数のインクリメント
                talkCnt++;

                LpsLogControllerCus.d("コンバーティッド * " + liplisNowTopic.result);
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// バッテリー情報の更新
        /// </summary>
        #region updateBatteryInfo
        protected void updateBatteryInfo()
        {
            liplisBatteryLevel = (int)(ps.BatteryLifePercent * 100);

            Console.WriteLine(liplisBatteryLevel);

            if (prevBatteryLevel != liplisBatteryLevel)
            {
                Console.WriteLine("prv:" + prevBatteryLevel + "now:" + liplisBatteryLevel);

                obtry.getBatteryImage(liplisBatteryLevel);


                //10の位が変化したらバッテリー情報を更新して、アイコンを更新する。
                if (obtry.batteryStatusChange)
                {
                    Invoke(new LpsDelegate.dlgVoidToVoid(li.setBatteryIcon));
                    initCmp(LpsLiplisUtil.boolToBit(flgSitdown));
                }
            }


            //前回値をセット
            prevBatteryLevel = liplisBatteryLevel;
        }
        #endregion

        /// <summary>
        /// 時報
        /// </summary>
        #region timeSignal
        protected void timeSignal(int hour)
        {
            talk(this.olc.getTimeSignal(hour));
        }
        #endregion
        
        
        ///====================================================================
        ///
        ///                              話題収集
        ///                         
        ///====================================================================


        /// <summary>
        //  MethodType : child
        /// MethodName : getTopic
        /// トピックを取得する
        /// 
        /// //2014/01/07 ver3.2.1 話題が尽きた時の挙動の修正。戻りをbooleanに変更
        /// </summary>
        #region getTopic
        protected bool getTopic()
        {
            try
            {
                flgThinking = true;
                updateThinkingIcon();

                //ショートニュース取得
                liplisNowTopic = otp.getTopic();

                //ver4.0.1
                //ツイッター内容控え
                liplisTweetMessege = liplisNowTopic.sorce;
                liplisTweetMessegeTitle = liplisNowTopic.title;

                flgThinking = false;
                updateThinkingIcon();

                //話題取得の可否を返す
                return liplisNowTopic != null;
            }
            catch (Exception err)
            {
                flgThinking = false;
                updateThinkingIcon();
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());

                return false;
            }
        }
        #endregion



        ///====================================================================
        ///
        ///                          チャット制御
        ///                         
        ///====================================================================

        /// <summary>
        //  MethodType : child
        /// MethodName : createTimer
        /// チャットの開始
        /// </summary>
        #region chatStart
        protected void chatStart()
        {
            try
            {
                //フラグ
                flgChatting = true;

                //現在表示文字の初期化
                liplisChatText = "";

                //総合エモーションの初期化
                sumEmotion.init();

                //トークウインドウを開く
                Invoke(new LpsDelegate.dlgVoidToVoid(at.Show));

                //即表示判定
                if (os.speed == 3)
                {
                    immediatelyReflesh();
                }
                else
                {
                    //2013/05/28 ver2.2.1 デバッグ出力
                    if (flgDebug)
                    {
                        LpsLogControllerCus.writingTestLog(liplisNowTopic.sorce);
                    }

                    /// 2013/05/01 ver2.2.0 おしゃべり対応
                    if (os.lpsVoiceOn == 1)
                    {
                        speechText();
                    }
                    //else
                    //{
                    //    lvr.stopThread();
                    //}

                    //おしゃべりフェーズに移行
                    flgAlarm = 12;
                }
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion


        /// <summary>
        //  MethodType : child
        /// MethodName : chatStop
        /// チャットストップ
        /// </summary>
        #region chatStop
        protected void chatStop()
        {
            try{
                //スルーカウントに移行
                flgAlarm = 0;

                //カウントの初期化
                reSetUpdateCount();
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
            finally
            {
                flgChatting = false;
                flgSkip = false;
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                          チャット処理
        ///                         
        ///====================================================================

        /// <summary>
        /// アップデイトリプリス
        /// </summary>
        #region updateLiplis
        protected void updateLiplis()
        {
            try
            {
                switch (liplisRefresh)
                {
                    case 0:
                        if (flgAlarm != 0) { refleshLiplis(); }
                        Thread.Sleep(33);
                        if (flgAlarm != 0) { refleshLiplis(); }
                        Thread.Sleep(33);
                        if (flgAlarm != 0) { refleshLiplis(); }
                        break;
                    case 1:
                        refleshLiplis();
                        break;
                    case 2:
                        if (cntSlow >= 1)
                        {
                            refleshLiplis();
                            cntSlow = 0;
                        }
                        else
                        {
                            cntSlow++;
                        }
                        break;
                    case 3:
                        //瞬間表示
                        immediatelyReflesh();
                        break;
                    default:
                        immediatelyReflesh();
                        break;
                }
            }
            catch
            {
                checkEnd();
            }
        }
        #endregion

        /// <summary>
        /// refleshLiplis
        /// リフレッシュリプリス
        /// 終了していたらfalseを返す
        /// </summary>
        #region refleshLiplis
        protected bool refleshLiplis()
        {
            try
            {
                Thread.Sleep(10);

                //--- キャンセルフェーズ --------------------
                if (checkMsg()) { return true; }

                //すわりチェック
                if (checkSitdown()) { return true; }

                //最小化チェック
                if (checkMinimize()) { return true; }

                if (flgTagCheck) { return true; }

                //if (liplisNextText == LiplisDefine.endChat){
                //    checkEnd(); 
                //}

                //スキップチェック
                if (checkSkip())
                {
                    return false;
                }

                //--- ナウワード取得・ナウテキスト設定フェーズ --------------------
                if (setText()) { return true; }

                //--- 描画フェーズ --------------------
                updateText();

                //ボディの更新
                updateBody();

                return true;
            }
            catch
            {
                checkEnd();
                return false;
            }
        }
        #endregion

        /// <summary>
        /// リフレッシュリプリス
        /// </summary>
        #region immediatelyReflesh
        protected void immediatelyReflesh()
        {
            LpsLogControllerCus.d("refleshLiplis" + cntLct);
            try
            {
                //--- キャンセルフェーズ --------------------
                if (checkMsg()) { return; }

                //すわりチェック
                if (checkSitdown()) { return; }

                //最小化チェック
                if (checkMinimize()) { return; }

                //--- 即表示フェーズ --------------------
                //スキップ
                skipLiplis();

                //--- 描画フェーズ --------------------
                updateText();

                //ボディの更新
                updateBody();

                //終了
                checkEnd();
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : checkMsg
        /// メッセージチェック
        ///	１バッチ終わったときにメッセージをヌルにしている。
        ///	次が読み込まれるまではヌルでアイドル状態なので、抜ける。
        /// </summary>
        #region checkMsg
        protected bool checkMsg()
        {
            try
            {
                 if(liplisNowTopic == null)
                 {
                    reSetUpdateCount();
                    return true;
                 }
                 return false;
            }
            catch(Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
       
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : checkSkip
        /// スキップチェック
        /// </summary>
        #region checkSkip
        protected bool checkSkip()
        {
            bool result = false;
            try
            {
                if (flgSkip)
                {
                    if (!flgSkipping)
                    {
                        flgSkipping = true;
                        chatStop();
                        result = skipLiplis();
                        flgSkipping = false;
                    }
                    return true;
                }
                return result;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
            finally
            {
                flgSkipping = false;
            }
        
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : checkSkip
        /// スキップチェック
        /// </summary>
        #region skipLiplis
        protected bool skipLiplis()
        {
            try
            {			
                if (liplisNowTopic != null && !liplisNowTopic.result.Equals(""))
                {
                    skipText();

                    //終端設定
                    cntLnw = liplisNowTopic.nameList.Count;
                    cntLct = liplisNowWord.Length;

                    //チャットを中断する
                    checkEnd();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            
                //終端設定
                cntLnw = liplisNowTopic.nameList.Count;
                cntLct = liplisNowWord.Length;
            
                return false;
            }
        

        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : skipLiplis_old
        /// スキップチェック
        /// </summary>
        #region skipLiplis_old
        //protected bool skipLiplis_old()
        //{
        //    try
        //    {
        //        liplisChatText = "";

        //        for (int idx = 0; idx < liplisNowTopic.nameList.Count; idx++)
        //        {
        //            //--- ワードセット、感情チェックフェーズ --------------------
        //            //送りワード文字数チェック
        //            if (idx != 0)
        //            {
        //                //なうワードの初期化
        //                liplisNowWord = liplisNowTopic.nameList[idx];

        //                //プレブエモーションセット
        //                prvEmotion = nowEmotion;

        //                //なうエモーションの取得
        //                nowEmotion = liplisNowTopic.emotionList[idx];

        //                //なうポイントの取得
        //                nowPoint = liplisNowTopic.pointList[idx];
        //            }
        //            //初回ワードチェック
        //            else if (idx == 0)
        //            {

        //                liplisNowWord = liplisNowTopic.nameList[idx];

        //                //空だったら、空じゃなくなるまで繰り返す
        //                if (liplisNowWord.Equals(""))
        //                {
        //                    do
        //                    {
        //                        //次ワード遷移
        //                        idx++;

        //                        //終了チェック
        //                        if (idx > liplisNowTopic.nameList[idx].Length) { break; }

        //                        //ナウワードの初期化
        //                        liplisNowWord = liplisNowTopic.nameList[idx];

        //                    } while (liplisNowWord.Equals(""));
        //                }
        //            }
        //            //おしゃべり中は何もしない
        //            else
        //            {

        //            }

        //            for (int kdx = 0; kdx < liplisNowWord.Length; kdx++)
        //            {
        //                //おしゃべり
        //                liplisChatText = liplisChatText + liplisNowWord.Substring(kdx, 1);
        //            }

        //            //終端設定
        //            cntLnw = liplisNowTopic.nameList.Count;
        //            cntLct = liplisNowWord.Length;

        //        }
        //        return true;
        //    }
        //    catch (Exception err)
        //    {
        //        LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());

        //        //終端設定
        //        cntLnw = liplisNowTopic.nameList.Count;
        //        cntLct = liplisNowWord.Length;

        //        return false;
        //    }


        //}
        #endregion


        /// <summary>
        //  MethodType : child
        /// MethodName : checkSitdown
        /// すわりチェック
        /// </summary>
        #region checkSitdown
        protected bool checkSitdown()
        {
            try
            {
                if(flgSitdown)
                {
                    liplisNowTopic = null;
                    updateBodySitDown();
                    return true;
                }
                return false;
            }
            catch(Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                 return false;
            }
        
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : checkMinimize
        /// すわりチェック
        /// </summary>
        #region checkMinimize
        protected bool checkMinimize()
        {
            try
            {
                if(flgMinimize)
                {
                    reSetUpdateCount();
                    liplisNowTopic = null;
                    checkEnd();
                    return true;
                }
                return false;
            }
            catch(Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                 return false;
            }
        
        }
        #endregion
        

        /// <summary>
        //  MethodType : child
        /// MethodName : checkEnd
        /// 終了チェック
        /// </summary>
        #region checkEnd
        protected bool checkEnd()
        {
            try
            {
                chatStop();
                flgSkipping = false;
                herfEyeCheck();
                if (liplisNowTopic != null) { appendLog(); }
                liplisNowTopic = null;
                return true;
            }
            catch(Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        protected bool checkEndError()
        {
            try
            {
                chatStop();
                liplisNowTopic = null;
                return true;
            }
            catch(Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : getSetText
        /// ナウワード設定とテキスト設定を行う
        /// </summary>
        #region setText
        protected bool setText()
        {
            int checkResult = 0;
            try
            {
                //最大文字数に達していたらエンド
                if (liplisNowTopic.result.Length > cntLct)
                {
                    //タグ検知
                    checkTag();

                    //ネームリストインデックスが最大の場合は、それ以上増えないので、何もしない
                    if (liplisNowTopic.nameList.Count > cntLnw)
                    {
                        //ネームの変化チェック
                        checkResult = checkName(liplisNowTopic.nameList[cntLnw], liplisNowTopic.result.Substring(cntLct, liplisNowTopic.result.Length - cntLct));

                        //現在ワードチェック
                        if (checkResult == 1 || checkResult == 9)
                        {
                            //2014/04/17 Liplis4.0
                            //エモーションのセット
                            setEmotion();

                            //インデックスインクリメント
                            cntLnw++;
                        }
                    }

                    //文字列形成
                    liplisChatText = liplisChatText + getLiplisChatText();
                    cntLct++;
                }
                else
                {
                    //チャットを中断する
                    checkEnd();
                }

                return false;
            }
            catch(Exception err)
            {
                //終端設定
                cntLnw = liplisNowTopic.nameList.Count;
                cntLct = liplisNowWord.Length;
            
                //チャットを中断する
                checkEnd();

                //ログの送信
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString() + Environment.NewLine + cntLnw + ":" + cntLct);
                return true ;
            }

        }
        #endregion

        /// <summary>
        /// 2014/04/17 
        /// エモーションセットを関数化
        /// ここで、エモーション変化の判断はこのメソッドで行う
        /// 修正方針 
        /// 　ある程度感情モードを保持する
        /// 　あまりにも表情変化が速いとヘンなのでその点を考慮する
        /// </summary>
        #region setEmotion
        protected void setEmotion()
        {
            //
            int bufEmotion = liplisNowTopic.emotionList[cntLnw];

            //プレブエモーションセット
            prvEmotion = nowEmotion;

            //変化があった場合にのみセット
            if (prvEmotion != bufEmotion)
            {
                if (bufEmotion != 0)
                {
                    //なうエモーションの取得
                    nowEmotion = bufEmotion; 
                }
            }

            //なうポイントの取得
            nowPoint = liplisNowTopic.pointList[cntLnw];

            //なう品詞を取得する
            nowPos = liplisNowTopic.pointList[cntLnw];

            //エモーション値の積算
            sumEmotion.set(nowEmotion, nowPoint);

            Console.WriteLine("emotion prv:" + prvEmotion + " now:" + nowEmotion + "," + nowPoint);
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : getLiplisChatText/getLiplisNextText
        /// 次のテキストを取得する
        /// </summary>
        #region getLiplisChatText/getLiplisNextText
        protected string getLiplisChatText()
        {
            try
            {
                string str = liplisNowTopic.result.Substring(cntLct, 1);

                if (str.Equals("@"))
                {
                    str = "<br><br>";
                }

                return str;
            }
            catch (Exception er)
            {
                throw er;
            }
        }
        protected string getLiplisNextText()
        {
            try
            {
                if (liplisNowTopic.result.Length > cntLct + 1)
                {
                    return liplisNowTopic.result.Substring(cntLct + 1, 1);
                }
                else
                {
                    return LiplisDefine.endChat;
                }
                
            }
            catch (Exception er)
            {
                throw er;
            }
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : checkTag
        /// タグかどうかチェックする
        /// タグだった場合はタグ分をすすめる
        /// </summary>
        #region checkTag
        protected void checkTag()
        {
            try
            {
                //タグチェック
                if (liplisNowTopic.result.Substring(cntLct, 1).Equals("<"))
                {
                    flgTagCheck = true;

                    //開始タグ
                    string tag = "";
                    int endIdx = -1;

                    //終端タグのチェック
                    if (!liplisNowTopic.result.Substring(++cntLct, 1).Equals("/"))
                    {
                        //終端タグまで取得
                        endIdx = liplisNowTopic.result.IndexOf(">", cntLct);

                        //タグ内容取得「km」
                        tag = liplisNowTopic.result.Substring(cntLct-1, endIdx + 2 - cntLct);

                        //リプリスチャットテキスト
                        liplisChatText = liplisChatText + tag;

                        //開始タグの終端までシーク
                        cntLct = cntLct + tag.Length - 1;
                    }
                    else
                    {
                        //終端タグまで取得
                        endIdx = liplisNowTopic.result.IndexOf(">", cntLct);

                        tag = liplisNowTopic.result.Substring(cntLct - 1, endIdx + 2 - cntLct);

                        liplisChatText = liplisChatText + tag;

                        //終端タグ
                        //終端なら、3プラスして終了
                        cntLct = endIdx + 1;
                    }

                    flgTagCheck = false;
                }
                else
                {
                    //ダイナリじゃなければ何もしない
                    return;
                }
            }
            catch
            {

            }
            finally
            {
                flgTagCheck = false;
            }

        }
        #endregion
        
        /// <summary>
        //  MethodType : child
        /// MethodName : checkName
        /// 0:始まらないが存在 1:始まる 9:始まらない、存在しない
        /// </summary>
        #region checkName
        protected int checkName(string word, string source)
        {
            //対象のワードで始まらないが、後ろに存在する
            if (!source.StartsWith(word) && source.IndexOf(word) > 0)
            {
                return 0;
            }
            //対象のワードで始まる
            else if (source.StartsWith(word))
            {
                return 1;
            }
            //対象のワードで始まらないし、存在もしない
            else
            {
                return 9;
            }
        }
        #endregion

        // <summary>
        //  MethodType : child
        /// MethodName : speechText
        /// 音声出力
        /// </summary>
        #region speechText
        protected void speechText()
        {
            if (liplisNowTopic != null)
            {
                lvr.addMessage(liplisNowTopic.sorce);
            }
        }  
        #endregion

        ///====================================================================
        ///
        ///                          フォーム処理
        ///                         
        ///====================================================================
        
        /// <summary>
        //  MethodType : child
        /// MethodName : updateBodySitDown
        /// 座り更新
        /// </summary>
        #region updateBodySitDown
        protected bool updateBodySitDown()
        {
            try
            {
                ob = obl.getLiplisBody(100, 0);

                //ボディの更新
                Invoke(reqReflesh);
                return true;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : updateBody
        /// おてんば(フル動作)
        /// </summary>
        #region updateBody
        protected bool updateBody()
        {
            try
            {
                //ボディ変化チェック
                setObjectBody();

                //--- 口パク --------------------
                //口パクカウント
                if (flgChatting)
                {
                    if (cntMouth == 1) { cntMouth = 2; }
                    else { cntMouth = 1; }
                    flgBodyChencge = true;
                }
                else
                {
                    cntMouth = 1;
                }

                //--- 目パチ --------------------
                //目パチカウント
                if (cntBlink == 0) { cntBlink = getBlinkCnt(); }
                else 
                { 
                    //カウント減らす
                    cntBlink--;

                    //目パチ状態の取得
                    prvBlink = nowBlink;
                    nowBlink = getBlinkState();

                    //目パチ変化チェック
                    if (prvBlink != nowBlink) { flgBodyChencge = true; }
                }

                //--- 描画 --------------------
                //描画変化チェック
                if (flgBodyChencge)
                {
                    flgBodyChencge = false;
                    Invoke(reqReflesh);
                }

                return true;
            }
            catch (Exception err)
            {
                ob = obl.getLiplisBody(0, 0);
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion


        /// <summary>
        //  MethodType : child
        /// MethodName : setObjectBody
        /// おてんば(フル動作)
        /// </summary>
        #region setObjectBody
        protected bool setObjectBody()
        {
            try
            {
                //感情変化
                if (nowEmotion != prvEmotion && flgChatting)
                {
                    if (os.lpsHelth == 1 && liplisBatteryLevel < 75)
                    {
                        //ヘルス設定ONでバッテリー残量75%以下なら、小破以下の画像取得
                        ob = obl.getLiplisBodyHelth(liplisBatteryLevel, nowEmotion, nowPoint);
                    }
                    else
                    {
                        ob = obl.getLiplisBody(nowEmotion, nowPoint);
                        Console.WriteLine("c"+ prvEmotion + " : " + nowEmotion);
                    }

                    flgBodyChencge = true;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
         #endregion

        /// <summary>
        /// MethodType : child
        /// MethodName : updateText
        /// テキストの更新
        /// </summary>
        #region updateText
        protected bool updateText()
        {
            try
            {
                if (liplisChatText.Equals(""))
                {
                    return true;
                }

                //テキスト出力
                if (liplisNowTopic.result.Length > cntLct - 1 )
                {
                    Invoke(new LpsDelegate.dlgS1ToVoid(at.setTextTotalkWindow), liplisChatText);
                    Invoke(new LpsDelegate.dlgI2ToVoid(at.setEmotionWindow), nowEmotion, nowPoint);
                }
                
                return true;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion

        /// <summary>
        /// MethodType : child
        /// MethodName : skipText
        /// テキストの更新
        /// </summary>
        #region skipText
        protected bool skipText()
        {
            try
            {
                //送り
                while (liplisNowTopic.result.Length > cntLct)
                {
                    setText();
                }

                //描画
                updateText();

                return true;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion

        /// <summary>
        /// MethodType : child
        /// MethodName : updateTextInstantly
        /// テキストの更新
        /// なうトピックの走査はしない
        /// </summary>
        #region updateText
        protected bool updateTextInstantly()
        {
            try
            {
                if (liplisChatText.Equals(""))
                {
                    return true;
                }

                Invoke(new LpsDelegate.dlgS1ToVoid(at.setTextTotalkWindow), liplisChatText);

                return true;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion

        /// <summary>
        /// アイコンを更新する
        /// </summary>
        #region updateThinkingIcon
        protected void updateThinkingIcon()
        {

        }
        #endregion

        /// <summary>
        /// ウインドウを変更する
        /// </summary>
        #region changeWindow
        public void changeWindow()
        {
            at.setBackGround();
        }
        #endregion

        /// <summary>
        /// メインウインドウに復帰する
        /// </summary>
        #region outRangeRecovery
        public void outRangeRecoveryAuto()
        {
            if (os.lpsOutRangeRecovery == 1)
            {
                outRangeRecovery();
            }
        }
        #endregion

        /// <summary>
        /// メインウインドウに復帰する
        /// </summary>
        #region outRangeRecovery
        public void outRangeRecovery()
        {
            int h = Screen.PrimaryScreen.Bounds.Height;     //ディスプレイの高さ
            int w = Screen.PrimaryScreen.Bounds.Width;      //ディスプレイの幅

            if (this.Top > h)
            {
                this.Top = h - this.Height;
            }
            if (this.Top < 0)
            {
                this.Top = 0;
            }

            if (this.Left > w)
            {
                this.Left = w - this.Width;
            }

            if (this.Left < 0)
            {
                this.Left = 0;
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                       ボディ更新詳細処理
        ///                         
        ///====================================================================

        //  MethodType : child
        /// MethodName : getBlinkCnt
        /// まばたきカウントの取得
        /// </summary>
        #region getBlinkCnt
        protected int getBlinkCnt()
        {
            try
            {
                Random rnd = new Random();
                return rnd.Next(17) + 17;
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return 10;
            }
        }
        #endregion

        /// <summary>
        /// getBlinkCnt
        /// まばたきカウントの取得
        /// </summary>
        #region getBlinkState
        protected int getBlinkState()
        {
            try
            {
                switch (cntBlink)
                {
                    case 0:
                        return 1;
                    case 1:
                        return 2;
                    case 2:
                        return 3;
                    case 3:
                        return 2;
                    default:
                        return 1;
                }
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return 1;
            }
        }
        #endregion

        /// <summary>
        /// getBlinkCnt
        /// まばたきカウントの取得
        /// </summary>
        #region herfEyeCheck
        protected void herfEyeCheck()
        {
            if (cntBlink == 1 || cntBlink == 3)
            {
                cntBlink = 0;
                updateBody();
            }
        }
        #endregion

        /// <summary>
        /// setEmotion
        /// 方向をセットする
        /// </summary>
        #region setDirection
        public void setDirection()
        {
            //方向チェック
            directionCheck();

            //前回値チェック
            if (nowDirection != prvDirection)
            {
                //方向が変わっていたらリフレッシュをかける
                Invoke(reqReflesh);
            }

            //前回値セット
            prvDirection = nowDirection;
        }
        #endregion

        /// <summary>
        /// 方向チェック
        /// </summary>
        #region directionCheck
        protected bool directionCheck()
        {
            int wid = 0;
            try
            {
                wid = Screen.PrimaryScreen.Bounds.Width;
                if (this.Left < wid / 2) { nowDirection = 1; }
                else { nowDirection = 0; }
                return true;
            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
                return false;
            }
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : sidDown
        /// すわり
        /// ☆Miniオーバーライド
        /// </summary>
        #region sitDown
        protected virtual bool sitDown()
        {
            try
            {
                //すわり有効、おしゃべり中ならおしゃべりメソッド内で処理
                flgSitdown = true;

                //おしゃべり中でなければ座りモーション
                //おしゃべり中はれフレッシュメソッド内で処理
                if (!flgChatting)
                {
                    //座りモーション
                    updateBodySitDown();
                }

                //アイコン変更
                updateSleepIcon();

                //チャットの初期化
                initChatInfo();
                //トークアクティビティの初期化
                initActivityTalk();

                //ヴォイスロイドストップ
                if (os.lpsVoiceOn == 1)
                {
                    lvr.callStopButtonDown();
                }

                //テキストの更新
                at.setWindowMode(LiplisDefine.WIN_MODE_TEXT_ONLY);
                liplisChatText = LiplisDefine.SAY_ZZZ;
                ap.Hide();
                anb.Hide();
                updateTextInstantly();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : standUp
        /// 立ち上がり
        /// </summary>
        #region standUp
        protected bool standUp()
        {
            try
            {
                flgSitdown = false;

                //アイコン変更
                updateSleepIcon();

                //あいさつ
                greet();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                        タッチ関連処理
        ///                         
        ///====================================================================


        /// <summary>
        /// タッチチェック
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void checkTouch(int x, int y)
        {
            objTouchResult result = olt.checkTouch(x, y, ob.getLstTouch());

            if (result.result == 2)
            {
                this.Cursor = System.Windows.Forms.Cursors.Hand;

                //チャットタイプを取得
                MsgShortNews chatType = olc.getChatWord("touch" , result.obj.chatSelected);

                if (chatType.nameList.Count > 0)
                {
                    //おしゃべり
                    talk(chatType);
                }
            }
            else if (result.result == 1)
            {
                this.Cursor = System.Windows.Forms.Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// タッチチェック
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void checkClick(int mode)
        {
            //X座標を取得する
            int x = System.Windows.Forms.Cursor.Position.X - this.Left;
            //Y座標を取得する
            int y = System.Windows.Forms.Cursor.Position.Y - this.Top;

            objTouchResult result = olt.checkClick(x, y, ob.getLstTouch(), mode);

            if (result.result == mode)
            {
                this.Cursor = System.Windows.Forms.Cursors.Hand;

                //チャットタイプを取得
                MsgShortNews chatType = olc.getChatWord("touch", result.obj.chatSelected);

                if (chatType.nameList.Count > 0)
                {
                    //おしゃべり
                    talk(chatType);
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }


        ///====================================================================
        ///
        ///                      他アクティビティの呼び出し
        ///                         
        ///====================================================================
        
        /// <summary>
        /// callRssWindow
        /// RSS設定ウインドウを呼び出す
        /// </summary>
        #region callRssWindow
        protected void callRssWindow()
        {
            Invoke(new LpsDelegate.dlgVoidToVoid(ar.loadRss));
            ar.Show();
        }
        #endregion

        /// <summary>
        /// callTwitterWindow
        /// RSS設定ウインドウを呼び出す
        /// </summary>
        #region callTwitterWindow
        protected void callTwitterWindow()
        {
            Invoke(new LpsDelegate.dlgVoidToVoid(ar.loadTwitter));
            ar.Show();
        }
        #endregion

        /// <summary>
        /// callFilterWindow
        /// RSS設定ウインドウを呼び出す
        /// </summary>
        #region callFilterWindow
        protected void callFilterWindow()
        {
            Invoke(new LpsDelegate.dlgVoidToVoid(ar.loadFilter));
            ar.Show();
        }
        #endregion

        /// <summary>
        /// callLogWindow
        /// ログウインドウを呼び出す
        /// </summary>
        #region callLogWindow
        protected void callLogWindow()
        {
            al.Show();
        }
        #endregion

        /// <summary>
        /// callCharWindow
        /// キャラウインドウを呼び出す
        /// </summary>
        #region callCharWindow
        protected void callCharWindow()
        {
            ac.Show();
        }
        #endregion

        /// <summary>
        /// callSettingWindow
        /// 設定ウインドウを呼び出す
        /// </summary>
        #region callSettingWindow
        protected void callSettingWindow()
        {
            ast.Show();
        }
        #endregion

        /// <summary>
        /// callWidgetWindow
        /// ウィジェットウインドウを呼び出す
        /// </summary>
        #region callWidgetWindow
        //protected void callWidgetWindow()
        //{
        //    awd.Show();
        //}
        #endregion

        /// <summary>
        /// calldownWindow
        /// ダウンロードウインドウを呼び出す
        /// </summary>
        #region callDownWindow
        //protected void callDownWindow()
        //{
        //    adl.Show();
        //}
        #endregion
        
        /// <summary>
        /// callRssBrwWindow
        /// RSSブラウザウインドウを呼び出す
        /// </summary>
        #region callRssBrwWindow
        //protected void callRssBrwWindow()
        //{
        //    arb.Show();
        //}
        #endregion
        
        /// <summary>
        /// callTwitterActivation
        /// ツイッターアクティベーションウインドウを呼び出す
        /// 2014/02/02 ver3.2.4 ツイッターの認証方式変更
        /// </summary>
        #region callTwitterActivation
        //protected void callTwitterActivation()
        //{
        //    TwitterService tokenGetObject = new TwitterService(LiplisDefine.TWITTER_OAUTH_CONSUMERKEY, LiplisDefine.TWITTER_OAUTH_CONSUMERSECRET);
        //    OAuthRequestToken reqToken = tokenGetObject.GetRequestToken();
        //    Uri uri = tokenGetObject.GetAuthenticationUrl(reqToken);
        //    Process.Start(uri.ToString());

        //    //ピンコード入力画面を表示する
        //    using (ActivityTwitterActivation ftip = new ActivityTwitterActivation(this, tokenGetObject, reqToken))
        //    {
        //        //画面表示
        //        ftip.ShowDialog();
        //    }
        //}
        protected void callTwitterActivation()
        {
            LiplisTwitterOAuth.lpsWitterOAuth(this);
        }
        #endregion
        



        ///====================================================================
        ///
        ///                          他スレッド操作
        ///                         
        ///====================================================================

        /// <summary>
        //  MethodType : child
        /// MethodName : appendLog
        /// ログを送る
        /// </summary>
        #region appendLog
        protected void appendLog()
        {
            Bitmap charBody = new Bitmap(75, 75);
            using (Graphics g = Graphics.FromImage(charBody))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(obl.getLiplisBody(liplisNowTopic.newsEmotion, liplisNowTopic.newsPoint).getBody(0, 0, 0), 0, 0, 75, 75);
            }
            Invoke(new LpsDelegate.dlgMsnToVoid(al.addLog), liplisNowTopic, charBody);
        }
        #endregion

        ///====================================================================
        ///
        ///                          その他処理
        ///                         
        ///====================================================================

        /// <summary>
        /// プロセス実行
        /// </summary>
        #region dlgProcess
        protected void dlgProcess(string command)
        {
            try
            {
                Process.Start(command);
            }
            catch
            {
                talk(LiplisDefine.err_Commnand, -1);
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                       ツイッター認証処理
        ///                         
        ///====================================================================

        /// <summary>
        /// registerTwitterInfo
        /// ツイッター情報を登録する
        /// </summary>
        #region registerTwitterInfo
        public void registerTwitterInfo(string token, string secret, string userId, string screanName)
        {
            //API呼び出し
            ResLpsRegisterTwitterInfoRespons res = LiplisApiCus.twitterRegister(os.uid, token, secret, userId, screanName);

            if (res.responseCode == "0")
            {
                //OKなら登録済みフラグを設定に格納
                os.twitterActivate = 1;
            }
            if (res.responseCode == "1")
            {
                //OKなら登録済みフラグを設定に格納
                os.twitterActivate = 1;
            }
            else
            {
                //NGなら0設定
                os.twitterActivate = 0;
            }

            //セーブ
            os.setPreferenceData();
        }
        #endregion

        /// <summary>
        /// tweet
        /// ツイートする
        /// </summary>
        #region tweet
        //現在の話題のツイート
        private void tweet()
        {
            if (!liplisTweetMessege.Equals(""))
            {
                //ここでツイート確認メッセージを表示するか?
                string fixmessage = FctLiplisTweet.createLiplisTweet(liplisTweetMessegeTitle, liplisTweetMessege);

                ///ツイート
                FctLiplisTweet.tweet(this.os.uid, fixmessage);
            }
        }
        //メッセージ指定のツイート
        private void tweet(string msg)
        {
            if (!msg.Equals(""))
            {
                //ここでツイート確認メッセージを表示するか?
                string fixmessage = FctLiplisTweet.createLiplisTweet(msg);

                ///ツイート
                FctLiplisTweet.tweet(this.os.uid, fixmessage);
            }
        }
        #endregion



    }
}
