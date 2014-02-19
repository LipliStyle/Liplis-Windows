//=======================================================================
//  ClassName : BaseTextFactoryBcp
//  概要      : データベース書込用BCPファイル生成クラス
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin.Sachin
//=======================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using Liplis.Common;
using System.IO;
using System.Text;

namespace Liplis.Fct
{
    public class BaseTextFactoryBcp : BaseTextFactory
    {
        ///=============================
        /// 書き込み先テーブル名
        protected string dataBaseName = "";
        protected string tableName = "";

        ///=============================
        /// 結果リスト
        protected SynchronizedCollection<String> bcpList;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public BaseTextFactoryBcp(string entName, string dataBaseName, string tableName)
            : base(entName)
        {
            //DB名取得
            this.dataBaseName = dataBaseName;

            //テーブル名取得
            this.tableName = tableName;


            //リスト作成
            this.bcpList = new SynchronizedCollection<string>();
        }

        /// <summary>
        /// addDataBcp
        /// bcpコマンドを追加する
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        #region addDataBcp
        protected void addDataBcp(string res)
        {
            bcpList.Add(res);
        }
        #endregion

        /// <summary>
        /// cleanDataBcp
        /// bcpリストをクリーニングする
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        #region cleanDataBcp
        protected void cleanDataBcp()
        {
            bcpList.Clear();
        }
        #endregion

        /// <summary>
        /// BCPファイルを作成する
        /// </summary>
        #region 
        public void createBcpFiles()
        {
            //ファイル名作成
            string fileNmae = LpsLiplisUtil.getName(10);

            //司令ファイル名作成
            string ctrlFileName = fileNmae + ".bcpcon";

            //bcpファイル名作成
            string bcpFileName = fileNmae + ".bcp";

            //司令ファイルの作成
            createBcpControlFile(ctrlFileName, bcpFileName);

            //bcpファイルの作成
            createBcpCommandFile(bcpFileName);
        }
        #endregion

        /// <summary>
        /// createBcpControlFile
        /// BCPコントロールファイルを作成する
        /// </summary>
        #region createBcpControlFile
        protected bool createBcpControlFile(string ctrlFileName, string bcpFileName)
        {
            try
            {
                string filePath = LpsPathController.getAppPath() + "\\temp\\" + ctrlFileName;

                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
                {
                    w.WriteLine(this.tableName);
                    w.WriteLine(LpsPathController.getAppPath() + "\\temp\\" + bcpFileName);
                    w.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// createBcpCommandFile
        /// BCPコマンドファイルを作成する
        /// </summary>
        #region createBcpCommandFile
        protected bool createBcpCommandFile(string bcpFileName)
        {
            try
            {
                string filePath = LpsPathController.getAppPath() + "\\temp\\" + bcpFileName;

                //DB登録
                bcpList.Add(this.tableName);

                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
                {
                    foreach (string str in bcpList)
                    {
                        w.WriteLine(str);
                    }

                    w.Close();
                }

                //クエリーのクリア
                bcpList.Clear();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// createBcpCommandFile
        /// BCPコマンドファイルを作成する
        /// </summary>
        protected virtual bool createBcpCommandFile()
        {
            try
            {
                string filePath = LpsPathController.getAppPath() + "\\tempBcp\\" + entName + "_" + LpsLiplisUtil.getName(10) + "." + entName + "bcp";

                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
                {
                    foreach (string str in bcpList)
                    {
                        w.WriteLine(str);
                    }

                    w.Close();
                }

                //クエリーのクリア
                bcpList.Clear();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion


        /// <summary>
        /// convertBcpDateFormat
        /// 時刻をBCPのフォーマットに変換する。
        /// </summary>
        #region convertBcpDateFormat
        protected string convertBcpDateFormat(string strDateTime)
        {
            return strDateTime.Replace("/", "-") + ".000";
        }
        #endregion

        /// <summary>
        /// doBcp
        /// BCPを実行する
        /// </summary>
        #region doBcp
        public virtual void doBcp()
        {
            //ファイルの読み込み
            List<string> bcpList = LpsLiplisUtil.getFileList(LpsPathController.getAppPath() + "\\tempBcp", "*." + entName + "bcp");

            //SQL文リストを作成する。
            foreach (string sqlFile in bcpList)
            {
                using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
                {
                    proc.StartInfo.FileName = "bcp";
                    proc.StartInfo.Arguments = dataBaseName + ".dbo." + tableName + " in \"" + sqlFile + "\" -c -T -S LPS-DB";
                    proc.Start();
                    proc.WaitForExit();
                }

                //ファイル削除
                LpsLiplisUtil.DeleteFile(sqlFile);
            }
        }
        #endregion

    }
}
