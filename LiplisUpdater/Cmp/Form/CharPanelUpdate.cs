//=======================================================================
//  ClassName : CharPanel
//  概要      : キャラクターパネル
//
//  Liplis2.3
//  2013/06/20 Liplis2.3.0 UI変更
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Ionic.Zip;
using Liplis.Common;
using Liplis.Control;
using Liplis.Fct;
using Liplis.Msg;
using Liplis.Web;
using LiplisUpdater;

namespace Liplis.Cmp.Form
{
    public class CharPanelUpdate : Panel
    {
        ///=====================================
        /// スキンインスタンス
        private frmMain main;
        private ObjSkinSetting oss;
        private ObjLiplisVersion nowOlv;
        private ObjLiplisVersion newOlv;

        ///=====================================
        /// 構成要素
        private Label lblText;
        private Label lnkLbl;
        private CusCtlPictureBox pic;
        private Button btnUpdate;
        private Color baseColor;

        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region CharPanel
        public CharPanelUpdate(frmMain main, ObjSkinSetting oss)
        {
            this.main = main;
            this.oss  = oss;
            this.nowOlv = new ObjLiplisVersion(oss.charName);
            initDataPanel();
            checkVersion();
        }
        #endregion


        /// <summary>
        /// キャラパネルの初期化
        /// </summary>
        #region initDataPanel
        private void initDataPanel()
        {
            ObjBodyList obl = new ObjBodyList(oss.charName);

            //初期化
            this.lblText = new System.Windows.Forms.Label();
            this.lnkLbl = new System.Windows.Forms.Label();
            this.pic = new CusCtlPictureBox();
            this.btnUpdate = new System.Windows.Forms.Button();

            // 
            // panel
            // 
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lnkLbl);
            this.Controls.Add(this.pic);
            

            this.Location = new System.Drawing.Point(3, 3);
            this.Name = "panel";
            this.Size = new System.Drawing.Size(380, 80);
            this.TabIndex = 0;
            this.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.MouseLeave += new System.EventHandler(this.mouseLeave);

            this.BackColor = Color.Azure;

            // 
            // lnkLbl
            // 
            this.lnkLbl.Location = new System.Drawing.Point(108, 17);
            this.lnkLbl.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lnkLbl.Name = "lnkLbl";
            this.lnkLbl.Size = new System.Drawing.Size(439, 16);
            this.lnkLbl.TabIndex = 1;
            this.lnkLbl.Text = oss.charName;
            this.lnkLbl.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.lnkLbl.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // lblText
            // 
            this.lblText.Location = new System.Drawing.Point(108, 33);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(454, 48);
            this.lblText.Text = oss.charIntroduction.Replace("@",Environment.NewLine);
            this.lblText.TabIndex = 2;
            this.lblText.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.lblText.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(2, 2);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(75, 75);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.SizeMode = PictureBoxSizeMode.Zoom;
            this.pic.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.pic.MouseLeave += new System.EventHandler(this.mouseLeave);

            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(300, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "アップデート";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Enabled = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);



            //イメージ
            if (LpsPathControllerCus.checkFileExist(LpsPathControllerCus.getSkinPath() + oss.charName + "\\window\\icon.png"))
            {
                this.pic.Image = new Bitmap(new Bitmap(LpsPathControllerCus.getSkinPath() + oss.charName + "\\window\\icon.png"), new Size(100, 100));
            }
            else
            {
                this.pic.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.TRANSE);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        #region checkVersion
        private void checkVersion()
        {
            //バージョンファイルチェック
            if (!nowOlv.skinVersion.Equals(""))
            {
                newOlv = new ObjLiplisVersion(oss.charName, nowOlv.versionUrl);

                if (!newOlv.skinVersion.Equals("") && nowOlv.skinVersion != newOlv.skinVersion)
                {
                    this.btnUpdate.Enabled = true;
                    baseColor = Color.FromArgb(255, 228, 96);
                    this.BackColor = baseColor;
                }
                else
                {
                    baseColor = Color.Azure;
                    this.BackColor = baseColor;
                    this.btnUpdate.Enabled = false;
                }  
            }
            else
            {
                baseColor = Color.Azure;
                this.BackColor = baseColor;
                this.btnUpdate.Enabled = false;
            }
        }
        #endregion

        /// <summary>
        /// スキンバージョンファイル再読み込み
        /// </summary>
        #region 
        private void reload()
        {
            this.nowOlv = new ObjLiplisVersion(oss.charName);
            checkVersion();
        }
        #endregion

        /// <summary>
        /// アイコンビットマップを取得する
        /// </summary>
        /// <param name="obl"></param>
        /// <param name="bmp"></param>
        /// <returns></returns>
        #region getIconBmp
        private Bitmap getIconBmp(ObjBodyList obl, Bitmap bmp)
        {
            try
            {
                using (Bitmap dest = new Bitmap(32, 32))
                {
                    using (Graphics g = Graphics.FromImage(dest))
                    {
                        using (Bitmap b = new Bitmap(obl.getLiplisBody(0, 1).getBody11()).Clone(new Rectangle(0, 0, obl.width, obl.width), dest.PixelFormat))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(b, 0, 0, 32, 32);
                            return (Bitmap)dest.Clone();
                        }
                    }
                }
            }
            catch
            {
                return FctCreateFromResource.getTranse();
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                           onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// mouseEnter
        /// マウスエンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region mouseEnter
        private void mouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightSkyBlue;
        }
        #endregion

        /// <summary>
        /// mouseLeave
        /// マウスリーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region mouseLeave
        private void mouseLeave(object sender, EventArgs e)
        {
            this.BackColor = baseColor;
        }
        #endregion

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

        ///====================================================================
        ///
        ///                           onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// 破棄処理
        /// </summary>
        #region dispose
        public void dispose()
        {
            lblText.Dispose();
            lnkLbl.Dispose();
            pic.Image.Dispose();
            pic.Image = null;
            pic.Dispose();
        }
        #endregion

        ///====================================================================
        ///
        ///                           一般処理
        ///                         
        ///====================================================================
        #region 一般処理
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

        /// <summary>
        /// update
        /// アップデートする。
        /// </summary>
        private bool update()
        {
            try
            {
                Invoke(new LpsDelegate.dlgVoidToVoid(main.grpOn));
                Invoke(new LpsDelegate.dlgS1ToVoid(main.setTitleLbl), oss.charName);
                Invoke(new LpsDelegate.dlgI1ToVoid(main.initPrg),100);

                string url = nowOlv.skinUrl;

                string liplisPath = LpsPathControllerCus.getAppPath();
                string archivePath = liplisPath + "\\temp\\" + oss.charName + ".zip";
                string unZipPathRoot = liplisPath + "\\temp\\";
                string unZipPath = liplisPath + "\\temp\\" + oss.charName;
                string skinPath = liplisPath + "\\skin\\" + oss.charName;

                Invoke(new LpsDelegate.dlgI1ToVoid(main.setPrgVal), 10);
                //パッチをダウンロードする
                if (downLoadNewLiplis(url, archivePath))
                {
                    Invoke(new LpsDelegate.dlgI1ToVoid(main.setPrgVal), 20);
                    //パッチの解凍
                    if (unZip(archivePath, unZipPathRoot))
                    {
                        Invoke(new LpsDelegate.dlgI1ToVoid(main.setPrgVal), 30);
                        //ファイルの更新
                        if (doPatch(skinPath, unZipPath))
                        {
                            Invoke(new LpsDelegate.dlgS1ToVoid(main.setFileLbl), "完了");
                            reload();
                            btnUpdate.Enabled = false;
                            CenterMessageBox.Show(this, "最新バージョンのスキンが適用されました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            CenterMessageBox.Show(this, "スキンの適用に失敗しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Invoke(new LpsDelegate.dlgS1ToVoid(main.setFileLbl), "スキンの適用に失敗しました。");
                        }
                    }
                    else
                    {
                        CenterMessageBox.Show(this, "スキンの解凍に失敗しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Invoke(new LpsDelegate.dlgS1ToVoid(main.setFileLbl), "スキンの解凍に失敗しました。");
                    }
                }
                else
                {
                    CenterMessageBox.Show(this, "スキンのダウンロードに失敗しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Invoke(new LpsDelegate.dlgS1ToVoid(main.setFileLbl), "スキンのダウンロードに失敗しました。");
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Invoke(new LpsDelegate.dlgVoidToVoid(main.grpOff));
                Invoke(new LpsDelegate.dlgS1ToVoid(main.setTitleLbl), "");
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
                Invoke(new LpsDelegate.dlgS1ToVoid(main.setFileLbl), "パッチダウンロード中");
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
        /// 解凍する
        /// </summary>
        /// <param name="archivePath"></param>
        /// <param name="unZipPath"></param>
        /// <returns></returns>
        private bool unZip(string archivePath, string unZipPath)
        {
            try
            {
                Invoke(new LpsDelegate.dlgS1ToVoid(main.setFileLbl), "パッチ展開中");

                Application.DoEvents();
                using (ZipFile zip = ZipFile.Read(archivePath, getReadOptions()))
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
        /// getReadOptions
        /// エンコーディングをsift-jisにしないと文字化けするため、オプション指定
        /// </summary>
        /// <returns></returns>
        private ReadOptions getReadOptions()
        {
            ReadOptions ro = new ReadOptions();

            ro.Encoding = Encoding.GetEncoding("shift_jis");

            return ro;
        }

        /// <summary>
        /// パッチを適用する
        /// </summary>
        /// <param name="liplisPath"></param>
        /// <param name="unZipPath"></param>
        /// <returns></returns>
        private bool doPatch(string liplisPath, string unZipPath)
        {
            int cnt = 0;
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
                Invoke(new LpsDelegate.dlgI1ToVoid(main.initPrg), files.Length);

                //パッチ適用
                foreach (string file in files)
                {
                    Application.DoEvents();
                    Invoke(new LpsDelegate.dlgI1ToVoid(main.setPrgVal), cnt++);
                    Invoke(new LpsDelegate.dlgS1ToVoid(main.setFileLbl), Path.GetFileName(file));
                    File.Copy(file, liplisPath + Path.GetFileName(file), true);
                }

                //フォルダが存在する場合はフォルダも適用
                string[] dirs = Directory.GetDirectories(unZipPath);
                foreach (string dir in dirs)
                {
                    doPatch(liplisPath + Path.GetFileName(dir),dir);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }



        #endregion



    }
}
