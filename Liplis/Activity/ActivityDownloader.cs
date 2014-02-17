//=======================================================================
//  ClassName : ActivityDownloader
//  概要      : ダウンローダー
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Control;
using Liplis.Fct;
using Liplis.MainSystem;
using Liplis.Msg;
using Liplis.Web.Nico;

namespace Liplis.Activity
{
    public partial class ActivityDownloader : BaseSystem
    {
        ///=====================================
        /// リプリスオブジェクト
        private Liplis.MainSystem.Liplis lips;

        ///=====================================
        /// オブジェクト
        private ObjSetting os;

        ///=====================================
        /// ニコニコ検索
        private string opt = LpsDefineMost.NICO_SEARCH_OPT_WORD;
        private Thread nicoSearchThread;

        ///=====================================
        /// フラグ
        private bool flgEnd = false;

        ///=====================================
        /// 選択インデックス
        private int sortSelectedIdx = 2;

        ///=====================================
        /// URL
        private string url = "";

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region ActivityDownloader
        public ActivityDownloader(Liplis.MainSystem.Liplis lips, ObjSetting os)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.lips = lips;
            this.os = os;
            this.initSettingWindow();
            this.initDownloader();
        }
        #endregion

        /// <summary>
        /// initSettingWindow
        /// initSettingWindowの初期化
        /// </summary>
        #region initSettingWindow
        private void initSettingWindow()
        {
            //オパシティ1
            this.Opacity = 1;

            //初期選択
            rdoKey.Checked = true;
            opt = LpsDefineMost.NICO_SEARCH_OPT_WORD;
            cboSort.SelectedIndex = 2;
        }
        #endregion

        /// <summary>
        /// initDownloader
        /// ダウンローダーの初期化
        /// </summary>
        #region initDownloader
        private void initDownloader()
        {
            //列が自動的に作成されないようにする
            dgvDownloader.AutoGenerateColumns = false;

            //DataGridViewTextBoxColumn列を作成する
            CusCtlDataGridViewProgressBarColumn prgColumn = new CusCtlDataGridViewProgressBarColumn();
            prgColumn.DataPropertyName = "prg";
            prgColumn.Name = "prg";
            prgColumn.HeaderText = "ダウン進捗";
            prgColumn.Width = 100;
            prgColumn.Maximum = 100;
            prgColumn.Mimimum = 0;
            //列を追加する
            dgvDownloader.Columns.Add(prgColumn);
        }
        #endregion

        ///====================================================================
        ///
        ///                             OnRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// btnNicoCheck_Click
        /// ニコチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnNicoCheck_Click
        private void btnNicoCheck_Click(object sender, EventArgs e)
        {
            if (LpsLiplisUtil.domainCheck(txtNicoUrl.Text, LpsDefineMost.URL_NICO_DOMAIN) || LpsRegularEx.chekcNicoVideoId(txtNicoUrl.Text))
            {
                createNicoInfo(txtNicoUrl.Text);
            }
            else
            {
                //どちらにも該当しない場合はエラー
                lblNicoMessage.Text = "URLが不正か、動画が見つかりません。";
                return;
            }
        }
        #endregion

        /// <summary>
        /// ブラウザクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnBrowser_Click
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (LpsLiplisUtil.domainCheck(txtNicoUrl.Text, LpsDefineMost.URL_NICO_DOMAIN)) 
            {
                url = txtNicoUrl.Text;
            }
            else if(LpsRegularEx.chekcNicoVideoId(txtNicoUrl.Text))
            {
                url = LpsDefineMost.URL_NICO_VIDEO + "/" + txtNicoUrl.Text;
            }
            else
            {
                //どちらにも該当しない場合はエラー
                lblNicoMessage.Text = "URLが不正か、動画が見つかりません。";
                return;
            }

            callBrowser();
        }
        #endregion


        /// <summary>
        /// btnNicoDl_Click
        /// 動画ダウンロードクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnNicoDl_Click
        private void btnNicoDl_Click(object sender, EventArgs e)
        {
            fileDonwload(10);
        }
        #endregion

        /// <summary>
        /// btnNicoMp3_Click
        /// MP3ダウンロードクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnNicoMp3_Click
        private void btnNicoMp3_Click(object sender, EventArgs e)
        {
            fileDonwload(11);
        }
        #endregion

        /// <summary>
        /// fileDonwload
        /// ファイルダウンロード
        /// </summary>
        /// <param name="code"></param>
        #region fileDonwload
        private void fileDonwload(int code)
        {
            string url = "";
            if (LpsLiplisUtil.domainCheck(txtNicoUrl.Text, LpsDefineMost.URL_NICO_DOMAIN))
            {
                string[] splitStr = txtNicoUrl.Text.Split('/');
                url = LpsDefineMost.URL_NICO_VIDEO + splitStr[splitStr.Length - 1]; ;
            }
            else if (LpsRegularEx.chekcNicoVideoId(txtNicoUrl.Text))
            {
                url = LpsDefineMost.URL_NICO_VIDEO + txtNicoUrl.Text;
            }
            else
            {
                //どちらにも該当しない場合はエラー
                lblNicoMessage.Text = "URLが不正か、動画が見つかりません。";
                return;
            }
            objNicoInfo n = new objNicoInfo(LpsRegularEx.getNicoId(url));
            string downPath = LpsPathController.getSaveFileName(LpsDefineMost.LPS_EXTENSION_FLV, os.downPath, n.title, os.downNotice);
            lips.addDownload(new ObjDownloadFile(url, n.title, downPath, 0.0, code, 0, 0));
        }
        #endregion
        
        /// <summary>
        /// btnNicoSearchWord_Click
        /// ワード検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnNicoSearchWord_Click
        private void btnNicoSearchWord_Click(object sender, EventArgs e)
        {

            opt = LpsDefineMost.NICO_SEARCH_OPT_WORD;
            sortSelectedIdx = cboSort.SelectedIndex;
            
            nicoSearchWord();
        }
        #endregion

        /// <summary>
        /// btnStop_Click
        /// 検索中止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnStop_Click
        private void btnStop_Click(object sender, EventArgs e)
        {
            nicoSearchAbort();
        }
        #endregion
        
        /// <summary>
        /// dgvNicoSearch_CellContentClick
        /// DGV選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region dgvNicoSearch_CellContentClick
        private void dgvNicoSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtNicoUrl.Text = (string)dgvNicoSearch[0, e.RowIndex].Value;

                if (LpsLiplisUtil.domainCheck(txtNicoUrl.Text, LpsDefineMost.URL_NICO_DOMAIN) || LpsRegularEx.chekcNicoVideoId(txtNicoUrl.Text))
                {
                    createNicoInfo(txtNicoUrl.Text);

                    if (LpsLiplisUtil.domainCheck(txtNicoUrl.Text, LpsDefineMost.URL_NICO_DOMAIN))
                    {
                        url = txtNicoUrl.Text;
                    }
                    else if (LpsRegularEx.chekcNicoVideoId(txtNicoUrl.Text))
                    {
                        url = LpsDefineMost.URL_NICO_VIDEO + "/" + txtNicoUrl.Text;
                    }

                    lblNicoMessage.Text = url;
                }
                else
                {
                    //どちらにも該当しない場合はエラー
                    lblNicoMessage.Text = "URLが不正か、動画が見つかりません。";
                    return;
                }
            }
            catch
            {

            }
        }
        #endregion


        /// <summary>
        /// 検索方法の選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region 検索方法を選択する
        private void rdoKey_CheckedChanged(object sender, EventArgs e)
        {
            opt = LpsDefineMost.NICO_SEARCH_OPT_WORD;
        }
        private void rdoTag_CheckedChanged(object sender, EventArgs e)
        {
            opt = LpsDefineMost.NICO_SEARCH_OPT_TAG;
        }
        private void rdoAll_CheckedChanged(object sender, EventArgs e)
        {
            opt = LpsDefineMost.NICO_SEARCH_OPT_BOTH;
        }
        #endregion

        /// <summary>
        /// コンボ選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region コンボ選択
        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        #endregion

        /// <summary>
        /// csmハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region cms
        private void tsmiDoga_Click(object sender, EventArgs e)
        {
            fileDonwload(10);
        }

        private void tsmiMp3_Click(object sender, EventArgs e)
        {
            fileDonwload(11);
        }

        private void tsmiBrw_Click(object sender, EventArgs e)
        {
            if (LpsLiplisUtil.domainCheck(txtNicoUrl.Text, LpsDefineMost.URL_NICO_DOMAIN))
            {
                url = txtNicoUrl.Text;
            }
            else if (LpsRegularEx.chekcNicoVideoId(txtNicoUrl.Text))
            {
                url = LpsDefineMost.URL_NICO_VIDEO + "/" + txtNicoUrl.Text;
            }
            else
            {
                //どちらにも該当しない場合はエラー
                lblNicoMessage.Text = "URLが不正か、動画が見つかりません。";
                return;
            }

            callBrowser();
        }
        #endregion


        ///====================================================================
        ///
        ///                              onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// onDelete
        /// </summary>
        #region Dispose
        public void dispose()
        {
            flgEnd = true;
            this.Close();
        }
        #endregion

        /// <summary>
        /// ActivityDownloader_FormClosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityDownloader_FormClosing
        private void ActivityDownloader_FormClosing(object sender, FormClosingEventArgs e)
        {
            //エンドフラグが有効でなければ、ハイドさせる
            if (!flgEnd)
            {
                e.Cancel = true;
                Invoke(new LpsDelegate.dlgVoidToVoid(this.Hide));
            }
        }
        #endregion
        
        ///====================================================================
        ///
        ///                     ダウンロード関連
        ///                         
        ///====================================================================

        /// <summary>
        /// addDownload
        /// ダウンロードに追加する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region addDownload
        public DataGridViewRow addDownload(ObjDownloadFile item)
        {
            try
            {
                DataGridViewProgressBarCell ccdgcpb = new DataGridViewProgressBarCell();
                ccdgcpb.Maximum = 100;
                ccdgcpb.Mimimum = 0;
                ccdgcpb.Value = 0;

                dgvDownloader.Rows.Add(new object[] { FctCreateFromResource.getIconExtention(convertIcoCd(item.kbn)), item.title, item.fileSize, ccdgcpb });
                return dgvDownloader.Rows[dgvDownloader.Rows.Count - 1];
            }
            catch
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// convertIcoCd
        /// アイコンIDコンバート
        /// </summary>
        /// <param name="kbn"></param>
        /// <returns></returns>
        #region convertIcoCd
        private int convertIcoCd(int kbn)
        {
            switch(kbn)
            {
                case 10:
                    return LiplisDefine.EXTENTION_FLV;
                case 11:
                    return LiplisDefine.EXTENTION_MP3;
                default:
                    return 0;
            }
        }
        #endregion
        

        /// <summary>
        /// delDgvDownload
        /// ダウンロードディージーブイから削除する
        /// </summary>
        /// <param name="d"></param>
        #region delDgvDownload
        public void delDgvDownload(DataGridViewRow d)
        {
            try
            {
                dgvDownloader.Rows.Remove(d);
            }
            catch
            {
                return;
            }
        }
        #endregion

        /// <summary>
        /// クリエイトニコインフォ
        /// </summary>
        #region createNicoInfo
        public void createNicoInfo(string url)
        {
            string htmlStr = "";
            string fixFileName;
            string[] splitStr;

            splitStr = url.Split('/');
            url = splitStr[splitStr.Length - 1];

            fixFileName = LpsPathController.getTempPath() + "nicoDl.html";

            //ファイル読込
            using (StreamWriter fixFile = new StreamWriter(@fixFileName, false, System.Text.Encoding.GetEncoding(932)))
            {
                //HTML作成
                htmlStr = htmlStr + "<HTML>\n";
                htmlStr = htmlStr + "<HEAD>\n";
                htmlStr = htmlStr + "</HEAD>\n";
                htmlStr = htmlStr + "	<BODY>\n";
                htmlStr = htmlStr + "   <iframe src=http://www.nicovideo.jp/thumb?v=" + url + " width=300 height=180  scrolling=no border=0 frameborder=0></iframe>\n";
                htmlStr = htmlStr + "   </BODY>\n";
                htmlStr = htmlStr + "</HTML>\n";

                fixFile.Write(htmlStr);
            }

            wbNico.Navigate(fixFileName);
            wbNico.Refresh();
        }
        #endregion

        /// <summary>
        /// nicoSearch
        /// ニコニコ検索
        /// </summary>
        #region nicoSearch
        private void nicoSearch()
        {
            //宣言
            List<string> nicoVideoIdList = new List<string>();
            string nicoVideoId;
            string word = txtNicoSearchWord.Text;

            //デリゲート
            LpsDelegate.dlgS2ToVoid d = new LpsDelegate.dlgS2ToVoid(addNicoDgv);
            LpsDelegate.dlgVoidToVoid c = new LpsDelegate.dlgVoidToVoid(clearNicoDgv);

            //クリア
            Invoke(c);

            //ダウンロード
            NicoSearch nico = new NicoSearch(os.nicoId, os.nicoPass);

            //ログインチェック
            if (!nico._isLogin)
            {
                //ログインし直す
                nico = new NicoSearch(LiplisDefine.NICO_DEFAULT_ID, LiplisDefine.NICO_DEFAULT_PASS);
                //ログインチェック
                if (!nico._isLogin)
                {
                    //検索失敗
                    MessageBox.Show("ニコニコ動画へのログインに失敗しました。", "Liplis");
                    return;
                }
            }

            //ページ
            for (int page = 1; page <= 50; page++)
            {
                List<string> urlList = nico.getUrlList(word, opt, sortSelectedIdx, page);

                foreach (string url in urlList)
                {
                    nicoVideoId = LpsRegularEx.getNicoId(url);

                    if (nicoVideoIdList.IndexOf(nicoVideoId) < 0)
                    {
                        nicoVideoIdList.Add(nicoVideoId);
                        objNicoInfo o = new objNicoInfo(nicoVideoId);
                        Invoke(d, nicoVideoId, o.title);
                        Application.DoEvents();
                        Thread.Sleep(5);
                    }
                }
            }
        }
        private void nicoSearchWord()
        {
            //スレッドチェック
            nicoSearchAbort();

            //画像作成するスレッドを生成
            nicoSearchThread = new Thread(new ThreadStart(nicoSearch));

            //WebBrowserはシングルスレッドアパートメントモードでのみ実行可能なのでスレッドのモードを設定して実行する
            nicoSearchThread.SetApartmentState(ApartmentState.STA);

            //スレッドスタート
            nicoSearchThread.Start();
        }
        private void nicoSearchAbort()
        {
            //スレッドチェック
            if (nicoSearchThread != null && nicoSearchThread.ThreadState == ThreadState.Running)
            {
                nicoSearchThread.Abort();
            }
        }
        private void addNicoDgv(string nicoVideoId, string title)
        {
            try
            {
                dgvNicoSearch.Rows.Add(new object[] { nicoVideoId, title });
                this.Refresh();
            }
            catch
            {
                return;
            }
        }
        private void clearNicoDgv()
        {
            dgvNicoSearch.Rows.Clear();
        }
        #endregion

        /// <summary>
        /// setWindowMode
        /// ウインドウモードのセット
        /// </summary>
        #region callBrowser
        public void callBrowser()
        {
            if (LpsLiplisUtil.domainCheck(txtNicoUrl.Text, LpsDefineMost.URL_NICO_DOMAIN) || LpsRegularEx.chekcNicoVideoId(txtNicoUrl.Text))
            {
                new LpsDelegate.dlgVoidToVoid(doCallBrowzer).BeginInvoke(null, null);
            }
            else
            {
                //どちらにも該当しない場合はエラー
                lblNicoMessage.Text = "URLが不正か、動画が見つかりません。";
                return;
            }
        }
        #endregion

        /// <summary>
        /// プロセス起動のスレッド
        /// </summary>
        #region doCallBrowzer
        private void doCallBrowzer()
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (System.ComponentModel.Win32Exception fileNotFoundErr)
            {
                Console.Write(fileNotFoundErr);
            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
        }
        #endregion







    }
}
