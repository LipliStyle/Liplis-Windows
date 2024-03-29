﻿//=======================================================================
//  ClassName : LiplisMini
//  概要      : LiplisMiniのメインクラス
//
//  Liplis3.0.5
//  2013/06/23 Liplis3.0.1 LiplisMini作成
//  2013/06/23 Liplis3.0.4 Liplis本体側の修正
//  2013/08/31 Liplis3.0.5 ATにOSSを渡すように変更。フォントカラー適用
//  2014/08/07 Liplis4.0.4 LiplisMiniのトークウインドウ表示位置を調整
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Reflection;
using Liplis.Activity;
using Liplis.Common;
using Liplis.Msg;
using System.Windows.Forms;
using System.Drawing;

namespace Liplis.MainSystem
{
    public partial class LiplisMini : MainSystem.Liplis
    {
        ///====================================================================
        ///
        ///                          独自メソッド 
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region LiplisMini
        public LiplisMini() : base()
        {
        }
        #endregion

        /// <summary>
        /// ミニサイズに合わせる
        /// </summary>
        #region fitMiniSize
        private void fitMiniSize()
        {
            this.Width = this.obl.width;
            this.Height = this.obl.height;
            this.cmp.Radius = 100;

            this.StartPosition = FormStartPosition.Manual;

            //横位置の調整
            //fixLocationX();
        }
        #endregion

        //private void fixLocationX()
        //{
        //    if (this.Width == 200)
        //    {
        //        this.Location = new System.Drawing.Point(0,0);
        //    }
        //    else if (this.Width > 200)
        //    {
        //        this.Location = new System.Drawing.Point(this.Width - 200, 0);
        //    }
        //    else
        //    {
        //        this.Location = new System.Drawing.Point(200 - this.Width, 0);
        //    }

        //}
        

        ///====================================================================
        ///
        ///                      オーバーライドメソッド
        ///                         
        ///====================================================================

        /// <summary>
        /// initActivity
        /// アクティビティの初期化
        /// </summary>
        #region initActivity
        protected override bool initActivity()
        {
            try
            {
                //トークアクティビティの初期化
                at = new ActivityTalkMini(this, os,oss);

                //以下は親クラスと同様 2013/06/23 ver3.0.1現在
                ar = new ActivityTopicRegist(this, this.os, this.owf);
                al = new ActivityLog(this, this.os, obr, this.owf);
                ac = new ActivityChar(this, ossList, this.os.loadSkin, this.owf);
                ast = new ActivitySetting(this, this.os, this.owf);

                //オーナーフォーム登録
                this.AddOwnedForm(at);
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
        /// Liplisの初期化
        /// 主にクラスの初期化
        /// </summary>
        #region initObject
        protected override void initObject()
        {
            ps         = SystemInformation.PowerStatus;
            os         = new ObjSetting();
            ossList    = new ObjSkinSettingList();
            oss        = ossList.loadTargetSkin(os.loadSkin);
            obl        = new ObjBodyListMini(os.loadSkin); this.fitMiniSize();             //ミニオーバーライド + サイズフィット
            ob         = obl.getLiplisBody(0, 0);
            olc        = new ObjLiplisChat(os.loadSkin);
            olt        = new ObjLiplisTouch(os.loadSkin);
            owf        = new ObjWindowFile(os.loadSkin);
            obtry      = new ObjBattery(os.loadSkin, this.ps);
            otp        = new ObjTopicMini(os, oss);
            li         = new LiplisIconMini(this);
            lpi        = new LiplisPopIcon(this, 0);
            ltb        = new LiplisTaskBar(this);
            obr        = new ObjBroom();
            sumEmotion = new MsgEmotion();
            this.AddOwnedForm(li);
        }
        #endregion

        /// <summary>
        /// initActivityTalk
        /// アクティビティの表示
        /// </summary>
        #region callActivityTalk
        protected override void callActivityTalk()
        {
            //ロケーションの設定
            at.setLocation(this.Location.X, this.Location.Y, this.Width, this.Height, this.nowDirection);

            //ショウする
            at.Show();
        }
        #endregion

        /// <summary>
        /// 最小化処理メソッド
        /// </summary>
        #region minimized
        public override void onMinimized(string param)
        {
            try
            {
                this.flgMinimize = true;
                this.WindowState = FormWindowState.Minimized;
                if (at != null) { Invoke(new LpsDelegate.dlgVoidToVoid(at.onMinimize)); }
                if (li != null) { Invoke(new LpsDelegate.dlgVoidToVoid(li.onMinimize)); }
                if (ar != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ar.onMinimize)); }
                if (al != null) { Invoke(new LpsDelegate.dlgVoidToVoid(al.onMinimize)); }
                if (ac != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ac.onMinimize)); }
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
        /// </summary>
        #region normalized
        public override void onNormalized()
        {
            try
            {
                this.Show();
                this.flgMinimize = false;
                this.WindowState = FormWindowState.Normal;
                if (at != null) { Invoke(new LpsDelegate.dlgVoidToVoid(at.onNormalize)); }
                if (li != null) { Invoke(new LpsDelegate.dlgVoidToVoid(li.onNormalize)); }
                if (ar != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ar.onNormalize)); }
                if (al != null) { Invoke(new LpsDelegate.dlgVoidToVoid(al.onNormalize)); }
                if (ac != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ac.onNormalize)); }
                if (ast != null) { Invoke(new LpsDelegate.dlgVoidToVoid(ast.onNormalize)); }

                reSetUpdateCount();
            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// マウスアップ時処理
        /// </summary>
        #region mouseUp
        public override void mouseUp(object sender, MouseEventArgs e)
        {
            os.locationX = this.Left;
            os.locationY = this.Top;
            os.setPreferenceData();

            //方向チェック
            setDirection();

            //自動復帰
            outRangeRecoveryAuto();

            //ウインドウ表示領域の再計算
            at.setLocation(this.Location.X, this.Location.Y, this.Width, this.Height, this.nowDirection);

            //右クリックなら、CMPを出現させる
            if (e.Button == MouseButtons.Right)
            {
                onRightClick();
            }
            else if (e.Button == MouseButtons.Left)
            {
                checkClick(1);
            }
        }
        #endregion


        /// <summary>
        /// マウスムーブ時処理
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">MouseEventArgs</param>
        #region onMouseMove
        protected override void onMouseMove(object sender, MouseEventArgs e)
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
                checkTouch(e.X * 3 / 2, e.Y * 3 / 2);

            }
            catch (System.Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        /// <summary>
        //  MethodType : child
        /// MethodName : sidDown
        /// すわり
        /// </summary>
        #region sitDown
        protected override bool sitDown()
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
        /// onDelete
        /// 終了
        /// </summary>
        #region onDelete
        protected override void onDelete()
        {
            try
            {
                //まずはタイマーをとめる
                flgAlarm = 0;               //フラグ0
                timRefresh.Dispose();       //タイマーの破棄

                //ボイスロイドスレッドの終了
                lvr.Dispose(); lvr = null;

                //セーブをしておく
                if(os != null)os.setPreferenceData();

                //おそうじ
                if (obr != null) obr.deleteAllTempFile();

                //アクティビティの破棄
                if (at != null) Invoke(new LpsDelegate.dlgVoidToVoid(at.dispose));
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

        /// <summary>
        /// initActivityTalk
        /// トークアクティビティを初期化する
        /// ap,anbを使用しない
        /// </summary>
        #region initActivityTalk
        protected override void initActivityTalk()
        {
            try
            {
                if (!liplisNowTopic.url.Equals(""))
                {
                    at.url = liplisNowTopic.url;
                    at.title = liplisNowTopic.title;
                    at.setWindowMode(LiplisDefine.WIN_MODE_WITH_URL);

                    Invoke(new LpsDelegate.dlgVoidToVoid(at.activityInit));

                }//ウインドウモードをウィズURLモードに設定
                else
                {
                    at.setWindowMode(LiplisDefine.WIN_MODE_TEXT_ONLY);
                }//ウインドウモードをテキストモードに設定


            }
            catch (Exception err)
            {
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, err.ToString());
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                        タッチ関連処理
        ///                         
        ///====================================================================
        #region タッチ関連処理
        /// <summary>
        /// タッチチェック
        /// ☆Miniオーバーライド
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected override void checkTouch(int x, int y)
        {
            objTouchResult result = olt.checkTouch(x, y, ob.getLstTouch());

            if (result.result == 2)
            {
                this.Cursor = Cursors.Hand;

                //チャットタイプを取得
                MsgShortNews chatType = olc.getChatWord("touch", result.obj.chatSelected);

                if (chatType.nameList.Count > 0)
                {
                    //おしゃべり
                    talk(chatType);
                }
            }
            else if (result.result == 1)
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// タッチチェック
        /// ☆Miniオーバーライド
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected override void checkClick(int mode)
        {
            //X座標を取得する
            int x = System.Windows.Forms.Cursor.Position.X - this.Left;
            //Y座標を取得する
            int y = System.Windows.Forms.Cursor.Position.Y - this.Top;

            objTouchResult result = olt.checkClick(x, y, ob.getLstTouch(), mode);

            if (result.result == mode)
            {
                this.Cursor = Cursors.Hand;

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
        #endregion



    }
}


