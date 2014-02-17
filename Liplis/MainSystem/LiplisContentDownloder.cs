using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Msg;
using Liplis.Ser;
using Liplis.Web;
using Liplis.Web.MhtGenerator;

namespace Liplis.MainSystem
{
    public class LiplisContentDownloder
    {
        private Liplis lips;
        private ObjSetting os;
        public ObjDownloadHst odh;
        public List<DataGridViewRow> dgvList;

        ///=============================
        /// ダウンロードスレッド
        Thread imgThread;

        /// <summary>
        /// LiplisContentDownloder
        /// コンストラクター
        /// </summary>
        #region LiplisContentDownloder
        public LiplisContentDownloder(Liplis lips, ObjSetting os)
        {
            this.lips = lips;
            this.os = os;
            this.odh = SerialLiplisDownloadHst.loadObject();
            initList();
        }
        #endregion

        /// <summary>
        /// initList
        /// リストの初期化
        /// </summary>
        #region initList
        private void initList()
        {
            dgvList = new List<DataGridViewRow>();
            foreach (ObjDownloadFile odf in odh.downList)
            {
                lips.addDownload(odf);
            }
        }
        #endregion


        /// <summary>
        /// 1件要素を追加する
        /// </summary>
        #region addList
        //public void addList(ObjDownloadFile item, DataGridViewRow dgv)
        //{
        //    //ODHダウンロードリストに追加する
        //    odh.downList.Insert(0, item);
            
        //    //DGVリストに追加する
        //    dgvList.Insert(0, dgv);

        //    //ダウンロード
        //    doDownload(item, dgv);

        //    //ダウロードリストのカウントチェック
        //    if (odh.downList.Count > 100)
        //    {
        //        try
        //        {
        //            //ODHから削除
        //            odh.downList.RemoveAt(100);
                    
        //            //DVGリストから削除
        //            lips.delDgvDownload(dgvList[100]);
        //        }
        //        catch
        //        {
        //            Console.WriteLine("ダウンロードログ削除エラー");
        //        }
        //    }

        //    return;
        //}
        #endregion

        /// <summary>
        /// ダウンロードを実行する
        /// </summary>
        #region doDownload
        private void doDownload(ObjDownloadFile item, DataGridViewRow dgv)
        {
            //フラグがオフならダウンロードする
            if (!item.flgEnd)
            {
                switch (item.kbn)
                {
                    case 0:
                        downloadHmt(item, dgv);
                        return;
                    case 10:
                        downloadDoga(item, dgv);
                        return;
                    case 11:
                        downloadMp3(item, dgv);
                        return;
                    default:
                        downloadHmt(item, dgv);
                        return;
                }
            }
            //フラグがオンなら履歴に追加する
            else
            {
                return;
            }
        }
        #endregion

        /// <summary>
        /// downloadHmt
        /// Hmtをダウンロードする
        /// </summary>
        #region downloadHmt
        private void downloadHmt(ObjDownloadFile item, DataGridViewRow dgv)
        {
            try
            {
                //ダウンロード
                new MhtDownloader(item.url).Write(LpsPathController.getSaveFileName(LpsDefineMost.LPS_EXTENSION_MHT, os.downPath, item.title, os.downNotice));

                item.flgEnd = true;
            }
            catch (Exception err)
            {
                Console.Write(err);
            }
        }
        #endregion

        /// <summary>
        /// downloadDoga
        /// 動画をダウンロードする
        /// </summary>
        #region downloadDoga
        private void downloadDoga(ObjDownloadFile item, DataGridViewRow dgv)
        {
            try
            {
                doInitThread(item,dgv);
            }
            catch (Exception err)
            {
                Console.Write(err);
                return;
            }
        }
        #endregion

        /// <summary>
        /// downloadMp3
        /// MP3をダウンロードする
        /// </summary>
        #region downloadMp3
        private void downloadMp3(ObjDownloadFile item, DataGridViewRow dgv)
        {
            try
            {
                doInitThreadMp3(item, dgv);
            }
            catch (Exception err)
            {
                Console.Write(err);
                return;
            }
        }
        #endregion



        /// <summary>
        /// doThread
        /// スレッドを実行する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region doInitThread
        public void doInitThread(ObjDownloadFile item, DataGridViewRow dgv)
        {
            LiplisNicoDownLoader lndl = new LiplisNicoDownLoader(item, dgv, os.nicoId, os.nicoPass);

            //画像作成するスレッドを生成
            imgThread = new Thread(new ThreadStart(lndl.movieDownload));

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

        /// <summary>
        /// doThread
        /// スレッドを実行する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region doInitThreadMp3
        public void doInitThreadMp3(ObjDownloadFile item, DataGridViewRow dgv)
        {
            LiplisNicoDownLoader lndl = new LiplisNicoDownLoader(item, dgv, os.nicoId, os.nicoPass);

            //画像作成するスレッドを生成
            imgThread = new Thread(new ThreadStart(lndl.mp3Download));

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
