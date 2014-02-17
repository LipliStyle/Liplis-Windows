//=======================================================================
//  ClassName : WidgetCpu02Base
//  概要      : こちらに透過しないオブジェクトを置く
//              親側からよば
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System.Windows.Forms;
using Liplis.Sys;
using System.Drawing;
using System;
using System.Collections.Generic;
using Liplis.Msg;
using Liplis.Control;
using Liplis.Common;
using System.IO;
using System.Threading;

namespace Liplis.Widget.WidDlg
{
    public partial class WidgetDlgBase : WidgetBaseBase
    {
        ///=============================
        ///クラス
        private ObjWidgetSetting o;
        private DataGridViewRow dgv;
        private Liplis.MainSystem.Liplis lips;

        ///=============================
        /// 定数
        private const int HONSU = 60;

        ///=============================
        /// ファイル情報
        private Stream stream;
        private string fileName;
        private string filePath;
        private long fileSize;

        ///=============================
        /// ダウンロードスレッド
        Thread imgThread;

        ///====================================================================
        ///
        ///                       onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// WidgetDlgBase
        /// 
        /// </summary>
        #region WidgetDlgBase
        public WidgetDlgBase(Liplis.MainSystem.Liplis lips, ObjWidgetSetting o, Stream stream, string fileName, long fileSize, DataGridViewRow dgv)
        {
            this.lips = lips;
            this.o = o;
            this.stream = stream;
            this.filePath = fileName;
            this.fileName = dgv.Cells[1].Value.ToString();
            this.fileSize = fileSize;
            this.dgv= dgv;
            InitializeComponent();
            initWindow();
        }
        #endregion

        /// <summary>
        /// initWindow
        /// ウインドウの初期化
        /// </summary>
        #region initWindow
        private void initWindow()
        {
            this.lblWidDlgTitle.ForeColor      = o.widgetTitleColor;
            this.lblWidDlgRate.ForeColor       = o.widgetForeColor;
            this.prg.Maximum                   = 100;
            this.prg.Minimum                   = 0;
            this.prg.Value                     = 0;
            this.lblWidDlgTitle.Text           = this.fileName;
            this.MouseDown                    += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.MouseMove                    += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);
            this.g.MouseDown                  += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseDown);
            this.g.MouseMove                  += new System.Windows.Forms.MouseEventHandler(this.lblWidCpuRate_MouseMove);

            this.g.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblWidDlgTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.prg.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);
            this.lblWidDlgRate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.endWidget);

        }
        #endregion

        /// <summary>
        /// endWidget
        /// エンドウィジェットのオーバーライド
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region endWidget
        protected override void endWidget(object sender, MouseEventArgs e)
        {
            //閉じてよいか聞く

            if (MessageBox.Show("ダウンロードをキャンセルしますか？", "Liplis", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                imgThread.Abort();
                f1.Close();
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                           onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// マウスムーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region マウスムーブ
        private void lblWidCpuRate_MouseDown(object sender, MouseEventArgs e) { mouseDown(e); }
        private void lblWidCpuRate_MouseMove(object sender, MouseEventArgs e) { mouseMoveWidget(e); }
        #endregion

        /// <summary>
        /// 動画ダウンロード
        /// </summary>
        #region MovieDownload
        private void MovieDownload()
        {
            BinaryReader reader = null; //バイナリーリーダー
            BinaryWriter writer = null; //バイナリーライター
            double dlSize = 0.0;        //ダウンロードサイズ
            byte[] buffer;              //バッファ

            LpsDelegate.dlgI1ToVoid d = new LpsDelegate.dlgI1ToVoid(updateWidget);      //アップデートウィジェット
            Invoke(new LpsDelegate.dlgVoidToVoid(initPrg));                             //プログレスバーの初期化

            try
            {
                reader = new BinaryReader(this.stream);
                writer = new BinaryWriter(File.Open(this.filePath, FileMode.Create));

                //ダウンロード
                while ((buffer = reader.ReadBytes(0x1000)).Length > 0)
                {
                    writer.Write(buffer);
                    dlSize += buffer.Length;
                    Application.DoEvents();
                    Invoke(d, (int)dlSize);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                //クローズ
                if (reader != null) { reader.Close(); }
                if (writer != null) { writer.Close(); }
                try
                {
                    //リプリスのadlist殻削除する
                    lips.delAdlist(f1);

                    //クローズインヴォーク
                    Invoke(new LpsDelegate.dlgVoidToVoid(f1.Close));
                }
                catch
                {

                }
            }
        }
        #endregion

        private void updateWidget(int dlSize)
        {
            prg.Value = dlSize;
            double rate = (dlSize / (double)fileSize * 100.0);
            lblWidDlgRate.Text = rate.ToString("0.00");

            //データグリッドを更新する
            this.dgv.Cells[3].Value = (int)rate;

            this.Refresh();
            Application.DoEvents();
        }

        private void initPrg()
        {
            //ゲージ初期化
            prg.Maximum = (int)fileSize;
            prg.Minimum = 0;
            prg.Value = 0;

        }

        /// <summary>
        /// doThread
        /// スレッドを実行する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region doInitThread
        public void doInitThread()
        {
            //画像作成するスレッドを生成
            imgThread = new Thread(new ThreadStart(MovieDownload));

            //WebBrowserはシングルスレッドアパートメントモードでのみ実行可能なのでスレッドのモードを設定して実行する
            imgThread.SetApartmentState(ApartmentState.STA);

            //スレッドスタート
            imgThread.Start();

            //スレッド終了を待つ
            //imgThread.Join();

            //スレッドを終了
            //imgThread.Abort();

            return;
        }
        #endregion
    }
}
