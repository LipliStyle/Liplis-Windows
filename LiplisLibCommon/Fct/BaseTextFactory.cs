//=======================================================================
//  ClassName : BaseTextFactory
//  概要      : データベース書き込み用テキストファイル生成クラス
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Liplis.Common;

namespace Liplis.Fct
{
    public class BaseTextFactory
    {
        ///=============================
        /// エンティティネーム
        protected string entName;

        ///=============================
        /// 結果リスト
        protected List<string> resQuery;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public BaseTextFactory(string entName)
        {
            this.entName = entName;
            resQuery = new List<string>();
        }
        public BaseTextFactory()
        {
            this.entName = "";
            resQuery = new List<string>();
        }

        /// <summary>
        /// 結果ファイルに書き込む
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        #region addData
        protected void addData(string res)
        {
            resQuery.Add(res);
        }
        #endregion

        /// <summary>
        /// リストをクリーニングする
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        #region cleanData
        protected void cleanData()
        {
            resQuery.Clear();
        }
        #endregion

        /// <summary>
        /// エスケープ
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        #region fixCentense
        protected string fixCentense(string str)
        {
            return str.Replace("г", "").Replace("$", "＄");
        }
        #endregion

        /// <summary>
        /// 結果ファイルに書き込む
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        #region writeFile
        protected bool writeFile(string filePath, Encoding enc, string str)
        {
            try
            {
                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, enc))
                {
                    w.WriteLine(str);

                    w.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        protected bool writeFile(string filePath, Encoding enc, List<string> strList)
        {
            try
            {
                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, enc))
                {
                    foreach (string str in strList)
                    {
                        w.WriteLine(str);
                    }
                    
                    w.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        protected bool writeFile()
        {
            try
            {
                string filePath = LpsPathController.getAppPath() + "\\temp\\" + entName + "_" + LpsLiplisUtil.getName(10) + ".led";

                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
                {
                    foreach (string str in resQuery)
                    {
                        w.WriteLine(str);
                    }

                    w.Close();
                }

                //クエリーのクリア
                resQuery.Clear();

                return true;
            }
            catch
            {
                return false;
            }
        }
        protected bool writeFile(string filePath)
        {
            try
            {
                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
                {
                    foreach (string str in resQuery)
                    {
                        w.WriteLine(str);
                    }

                    w.Close();
                }

                //クエリーのクリア
                resQuery.Clear();

                return true;
            }
            catch
            {
                return false;
            }
        }
        protected bool writeFileSql()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(LpsPathController.getAppPath());

                string filePath = LpsPathController.getAppPath() + "\\temp\\" + entName + "_" + LpsLiplisUtil.getName(10) + ".sql";

                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
                {
                    foreach (string str in resQuery)
                    {
                        w.WriteLine(str);
                    }

                    w.Close();
                }

                //クエリーのクリア
                resQuery.Clear();

                return true;
            }
            catch
            {
                return false;
            }
        }
        protected bool writeFileSql(List<string> resQuery)
        {
            try
            {
                string filePath = LpsPathController.getAppPath() + "\\temp\\" + entName + "_" + LpsLiplisUtil.getName(10) + ".sql";

                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
                {
                    foreach (string str in resQuery)
                    {
                        w.WriteLine(str);
                    }

                    w.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        protected bool writeFileSql(string resQuery)
        {
            try
            {
                string filePath = LpsPathController.getAppPath() + "\\temp\\" + entName + "_" + LpsLiplisUtil.getName(10) + ".sql";

                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
                {
                    w.WriteLine(resQuery);
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
        /// 親フォルダに書き込む
        /// </summary>
        /// <returns></returns>
        #region writeFileParent
        protected bool writeFileParent()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(LpsPathController.getAppPath());

                string filePath = di.Parent.FullName + "\\temp\\" + entName + "_" + LpsLiplisUtil.getName(10) + ".sql";

                //結果をファイルに書き込む
                using (StreamWriter w = new StreamWriter(filePath, false, Encoding.GetEncoding(LpsDefineMost.ENCODING_SJIS)))
                {
                    foreach (string str in resQuery)
                    {
                        w.WriteLine(str);
                    }

                    w.Close();
                }

                //クエリーのクリア
                resQuery.Clear();

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
