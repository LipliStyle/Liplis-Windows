//=======================================================================
//  ClassName : frmMain
// ■概要     : Liplisアップデートツール
//
//■ Liplis4.0
//　2014/04/26 Liplis4.0 アップデート機能
// Copyright(c) 2014 LipliStyle さちん MITライセンス
//=======================================================================
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Ionic.Zip;
using Liplis.Cmp.Form;
using Liplis.Common;
using Liplis.Control;
using Liplis.Msg;
using Liplis.Web;
using Liplis.Xml;

namespace LiplisUpdater
{
    public partial class frmMain : Form
    {
        ///=====================================
        /// クラス
        private VersionXml version;

        ///=====================================
        /// オブジェクト
        private ObjSkinSettingList ossList;

        ///=====================================
        /// フラグ
        private bool debug = false;

        ///====================================================================
        ///
        ///                    　　　   初期化処理
        ///                         
        ///====================================================================

        #region 初期化処理
        public frmMain()
        {
            InitializeComponent();
            getCommand();
            this.ossList = new ObjSkinSettingList();
            this.ossList.loadAllSkinFile(LpsPathControllerCus.getSkinPath());
            this.StartPosition = FormStartPosition.CenterScreen;
            initCharList();
        }

        /// <summary>
        /// initCharList
        /// キャラクターリストの初期化
        /// </summary>
        #region initCharList
        private void initCharList()
        {
            //OSSリストをまわしてパネルを作成する
            foreach (ObjSkinSetting oss in ossList.ossList)
            {
                addPanel(oss);
            }
        }
        #endregion

        /// <summary>
        /// ウインドウの初期化
        /// </summary>
        private void initWindow()
        {
            lblFile.Text = "";
            prg.Minimum = 0;
            prg.Maximum = 100;
            prg.Value = 0;
        }

        /// <summary>
        /// クラスの初期化
        /// </summary>
        private void getNewVersion()
        {
            version = new VersionXml(LiplisUpdaterDefine.getUrlVersion(debug));

            //バージョン表示
            if (!version.version.Equals(""))
            {
                lblNewVer.Text = version.version;

                if (lblNewVer.Text.Equals(lblNowVer.Text))
                {
                    CenterMessageBox.Show(this, "最新バージョンです。" + Environment.NewLine + "アップデートの必要はありません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnUpdate.Enabled = false;
                }
                else
                {
                    msgTargetVersion msg = version.getSameVersion(lblNowVer.Text);

                    if (msg.enable.Equals("true"))
                    {
                        CenterMessageBox.Show(this, "最新バージョンがあります。" + Environment.NewLine + "アップデートできます。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnUpdate.Enabled = true;
                    }
                    else
                    {
                        CenterMessageBox.Show(this, "アップデート可能なバージョンではありません。" + Environment.NewLine + "手動でアップデートして下さい。" + Environment.NewLine + "手順についてはLipliStyleのサイトを御覧ください。", this.Text,  MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnUpdate.Enabled = false;
                    }
                }
            }
            else
            {
                CenterMessageBox.Show(this, "最新バージョンが取得できません", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblNewVer.Text = "最新バージョン取得失敗";
                btnUpdate.Enabled = false;
            }
        }

        /// <summary>
        /// 現在のバージョンを取得する
        /// </summary>
        private void getNowVersion()
        {
            string liplisPath = LpsPathController.getAppPath() + "\\Liplis.exe";

            if (LpsPathController.checkFileExist(liplisPath))
            {
                FileVersionInfo vi = FileVersionInfo.GetVersionInfo(liplisPath);

                lblNowVer.Text = vi.FileVersion;
            }
            else
            {
                CenterMessageBox.Show(this, "Liplis.exeが見つかりません。" + Environment.NewLine + "インストールし直して下さい。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblNowVer.Text = "Liplis.exe取得失敗";
            }
        }

        /// <summary>
        /// コマンドライン取得
        /// </summary>
        private void getCommand()
        {
            //コマンドラインを配列で取得する
            string[] cmds;
            cmds = System.Environment.GetCommandLineArgs();
            //コマンドライン引数の表示
            foreach (string cmd in cmds)
            {
                if (cmd.Equals("/debug"))
                {
                    debug = true;
                }
            }
        }

        #endregion

        ///====================================================================
        ///
        ///                         イベントハンドラ
        ///                         
        ///====================================================================
       
        #region イベントハンドラ
        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            getNowVersion();
            getNewVersion();
            initWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!checkLiplisRun())
            {
                update();
            }
            else
            {
                CenterMessageBox.Show(this, "Liplisを終了してから実行して下さい。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            getNowVersion();
            getNewVersion();
        }

        /// <summary>
        /// 終了ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiVersion_Click(object sender, EventArgs e)
        {
            FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            CenterMessageBox.Show(this,  this.Text + Environment.NewLine + ver.FileVersion, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        ///====================================================================
        ///
        ///                    　　　      処理
        ///                         
        ///====================================================================
        #region 処理


        /// <summary>
        /// update
        /// アップデートする。
        /// </summary>
        private bool update()
        {
            try
            {
                lblUpdateTitle.Text = "Liplis";
                lblFile.Text = "アップデート開始";

                prg.Value = 0;
                prg.Maximum = 100;

                //フレームをロック
                grpOn();

                //URL取得
                string url = LiplisUpdaterDefine.getUrlRoot(debug) + version.file;

                //パスを取得
                string liplisPath = LpsPathControllerCus.getAppPath();
                string archivePath = liplisPath + "\\temp\\" + version.file;
                string unZipPathRoot = liplisPath + "\\temp\\";
                string unZipPath = liplisPath + "\\temp\\" + version.file.Substring(0, version.file.Length - 3);

                prg.Value = 10;
                //パッチをダウンロードする
                if (downLoadNewLiplis(url, archivePath))
                {
                    prg.Value = 20;
                    //パッチの解凍
                    if (unZip(archivePath, unZipPathRoot))
                    {
                        prg.Value = 30;
                        //ファイルの更新
                        if (doPatch(liplisPath, unZipPath))
                        {
                            lblFile.Text = "完了";
                            getNowVersion();
                            btnUpdate.Enabled = false;
                            CenterMessageBox.Show(this, "最新バージョンのパッチが適用されました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            CenterMessageBox.Show(this, "パッチの適用に失敗しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            lblFile.Text = "パッチの適用に失敗しました。";
                        }
                    }
                    else
                    {
                        CenterMessageBox.Show(this, "パッチの解凍に失敗しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblFile.Text = "パッチの解凍に失敗しました。";
                    }
                }
                else
                {
                    CenterMessageBox.Show(this, "パッチのダウンロードに失敗しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblFile.Text = "パッチのダウンロードに失敗しました。";
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                lblUpdateTitle.Text = "";
                grpOff();
            }
        }

        /// <summary>
        /// 解凍する
        /// </summary>
        /// <param name="archivePath"></param>
        /// <param name="unZipPath"></param>
        /// <returns></returns>
        private bool unZip(string archivePath, string unZipPath)
        {
            try
            {
                lblFile.Text = "パッチ展開中";
                Application.DoEvents();
                using (ZipFile zip = ZipFile.Read(archivePath))
                {
                    //(2)解凍時に既にファイルがあったら上書きする設定
                    zip.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                    //(3)全て解凍する
                    zip.ExtractAll(unZipPath);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// downLoadNewLiplis
        /// 最新のLiplisをダウンロードする
        /// </summary>
        private bool downLoadNewLiplis(string url, string dlPath)
        {
            try
            {
                lblFile.Text = "パッチダウンロード中";
                Application.DoEvents();
                LiplisWedFileDownLoader.downLoad(url, dlPath);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// パッチを適用する
        /// </summary>
        /// <param name="liplisPath"></param>
        /// <param name="unZipPath"></param>
        /// <returns></returns>
        private bool doPatch(string liplisPath, string unZipPath)
        {
            try
            {
                //ディレクトリ存在チェック
                if (!Directory.Exists(liplisPath))
                {
                    Directory.CreateDirectory(liplisPath);
                }

                //末尾円マークチェック
                if (liplisPath[liplisPath.Length - 1] != Path.DirectorySeparatorChar)
                {
                    liplisPath = liplisPath + Path.DirectorySeparatorChar;
                }
                    
                //ファイル取得
                string[] files = Directory.GetFiles(unZipPath);

                //ファイル数設定
                prg.Value = 0;
                prg.Maximum = files.Length;

                //パッチ適用
                foreach (string file in files)
                {
                    Application.DoEvents();
                    prg.Value += 1;
                    lblFile.Text = Path.GetFileName(file);
                    File.Copy(file, liplisPath + Path.GetFileName(file), true);
                }

                //フォルダが存在する場合はフォルダも適用
                string[] dirs = Directory.GetDirectories(unZipPath);
                foreach (string dir in dirs)
                {
                    doPatch(dir, liplisPath + Path.GetFileName(dir));
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// リプリス起動チェック
        /// </summary>
        /// <returns></returns>
        private bool checkLiplisRun()
        {
            if (Process.GetProcessesByName("Liplis").Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        #endregion

        ///====================================================================
        ///
        ///                          フォーム操作
        ///                         
        ///====================================================================

        #region addPanel

        /// <summary>
        /// addLog
        /// パネルの追加
        /// </summary>
        private void addPanel(ObjSkinSetting oss)
        {
            //新規要素の追加
            CharPanelUpdate d = new CharPanelUpdate(this,oss);
            flpChar.Controls.Add(d);
            this.Refresh();
        }
        
        /// <summary>
        /// グループをロックする
        /// </summary>
        public void  grpOn()
        {
            grpLiplis.Enabled = false;
            grpSkin.Enabled = false;
        }

        /// <summary>
        /// グループのロックを解除する
        /// </summary>
        public void grpOff()
        {
            grpLiplis.Enabled = true;
            grpSkin.Enabled = true;
        }

        /// <summary>
        /// タイトルラベル
        /// </summary>
        /// <param name="str"></param>
        public void setTitleLbl(string str)
        {
            lblUpdateTitle.Text = str;
        }

        /// <summary>
        /// ファイルラベル
        /// </summary>
        /// <param name="str"></param>
        public void setFileLbl(string str)
        {
            lblFile.Text = str;
        }

        /// <summary>
        /// PRGを初期化する
        /// </summary>
        /// <param name="max"></param>
        public void initPrg(int max)
        {
            prg.Value = 0;
            prg.Minimum = 0;
            prg.Maximum = max;
        }

        /// <summary>
        /// PRGをセットする
        /// </summary>
        /// <param name="val"></param>
        public void setPrgVal(int val)
        {
            prg.Value = val;
        }

        #endregion


    }
}


