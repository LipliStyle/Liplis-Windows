//=======================================================================
//  ClassName : LiplisNicoDownLoader
//  概要      : ニコニコのダウンロードを請け負う
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.IO;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Msg;
using Liplis.Web.Nico;

namespace Liplis.Web
{
    public class LiplisNicoDownLoader
    {
        ///=============================
        /// プロパティ
        private ObjDownloadFile item;
        private DataGridViewRow dgv;
        private objNicoNico nico;

        private string nicoId;
        private string nicoPass;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dgv"></param>
        #region LiplisNicoDownLoader
        public LiplisNicoDownLoader(ObjDownloadFile item, DataGridViewRow dgv, string nicoId, string nicoPass)
        {
            this.item     = item;
            this.dgv      = dgv;
            this.nicoId   = nicoId;
            this.nicoPass = nicoPass;
        }
        #endregion

        /// <summary>
        /// 代表メソッド
        /// </summary>
        #region movieDownload
        public void movieDownload()
        {
            movieDown();
        }
        #endregion

        /// <summary>
        /// 代表メソッド
        /// </summary>
        #region mp3Download
        public void mp3Download()
        {
            movieDown();
            convertMp3();
        }
        #endregion

        /// <summary>
        /// 動画ダウンロード
        /// </summary>
        #region movieDown
        private void movieDown()
        {
            string pFineLname = "";
            string pTitle;
            long pFileSize = 0;
            Stream st = null;

            //ダウンロード
            nico = new objNicoNico(this.nicoId, this.nicoPass);

            //ログインチェック
            if (!nico.isLogin)
            {
                //ログインし直す
                nico = new objNicoNico(LiplisDefine.NICO_DEFAULT_ID, LiplisDefine.NICO_DEFAULT_PASS);
                if (!nico.isLogin)
                {
                    //タイトルの設定
                    dgv.Cells[1].Value = item.title;
                    dgv.Cells[2].Value = "ログイン失敗";
                    return;
                }
            }

            //ストリームの取得
            try
            {
                st = nico.getMovie(item.url, out pFineLname, out pFileSize);
            }
            catch
            {
                st = null;
            }
           

            //ファイルサイズの取得
            item.fileSize = pFileSize;

            if (st != null)
            {
                //ファイル名補正
                pFineLname = item.dlPath;
                pTitle = item.title;

                //タイトルの設定
                dgv.Cells[1].Value = item.title;
                dgv.Cells[2].Value = item.fileSize;
            }
            else
            {
                //タイトルの設定
                dgv.Cells[1].Value = item.title;
                dgv.Cells[2].Value = "ダウンロード失敗";

                return;
            }

            //ダウンロードフェーズの開始
            BinaryReader reader = null; //バイナリーリーダー
            BinaryWriter writer = null; //バイナリーライター
            double dlSize       = 0.0;        //ダウンロードサイズ
            double rate         = 0.0;        //ダウンロードサイズ
            byte[] buffer;              //バッファ

            try
            {
                reader = new BinaryReader(st);
                try
                {
                    writer = new BinaryWriter(File.Open(item.dlPath, FileMode.Create));
                }
                catch (ArgumentException)
                {
                    item.dlPath = LpsPathController.getDownPath() + pFineLname;
                    writer = new BinaryWriter(File.Open(item.dlPath, FileMode.Create));
                }
                

                //ダウンロード
                while ((buffer = reader.ReadBytes(0x1000)).Length > 0)
                {
                    writer.Write(buffer);
                    dlSize += buffer.Length;
                    rate = (dlSize / (double)item.fileSize * 100.0);
                    this.dgv.Cells[3].Value = (int)rate;
                    Application.DoEvents();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                //クローズ
                if (reader != null) { reader.Close(); }
                if (writer != null) { writer.Close(); }
            }
        }
        #endregion

        /// <summary>
        /// MP3変換
        /// </summary>
        #region movieDown
        private void convertMp3()
        {
            LpsBaseMovieConverter lbmc = new LpsBaseMovieConverter();
            lbmc.convertFlvToMP3High(item.dlPath,item.dlPath.Replace(LpsDefineMost.LPS_EXTENSION_FLV,LpsDefineMost.LPS_EXTENSION_MP3));
        }
        #endregion
    }
}
