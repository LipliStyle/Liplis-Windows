//=======================================================================
//  ClassName : LiplisWedFileDownLoadercs
//  概要      : ウェブファイルダウンローダー
//
//  Liplis2.0
//  Copyright(c) 2010-2011 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liplis.Common;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace Liplis.Web
{
    public static class LiplisWedFileDownLoader
    {
        /// <summary>
        /// サムネイルをダウンロードし、名前を付けて保存する.
        /// ダウンロードしたパスを返す。
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cacheFilePath"></param>
        #region downLoadthumb
        public static string downLoadthumb(string uri)
        {
            string fileName = "";
            try
            {
                fileName = LpsPathController.getTempPath() + getJpgFileName(uri);
                downLoad(uri, fileName);

                return fileName;
            }
            catch 
            {
                return fileName;
            }
        }
        #endregion

        /// <summary>
        /// ファイルをダウンロードする
        /// </summary>
        #region downLoad
        public static void downLoad(string uri, string cacheFilePath)
        {
            HttpWebRequest Req;
            HttpWebResponse Res;
            Stream st = null;
            FileStream fs = new FileStream(cacheFilePath,FileMode.Create,FileAccess.Write);
 
            try
            {
                Req = (HttpWebRequest)WebRequest.Create(uri);
                //タイムアウト時間設定
                Req.Timeout = 10000;
                Res = (HttpWebResponse)Req.GetResponse();
                st = Res.GetResponseStream();

                //応答データをファイルに書き込む
                byte[] readData = new byte[10240];
                int readSize = 0;

                //このフォウには意味があるようです。
                for (; ; )
                {
                    readSize = st.Read(readData, 0, readData.Length);
                    if (readSize == 0){ break; }
                    fs.Write(readData, 0, readSize);
                }

                //readSize = st.Read(readData, 0, readData.Length);
                //if (readSize != 0)
                //{
                //    fs.Write(readData, 0, readSize);
                //}
                

                //閉じる
               
            }
            catch (System.Net.WebException)
            {

            }
            catch 
            {

            }
            finally
            {
                if (fs != null) { fs.Close(); }
                if (st != null) { st.Close(); }
            }
        }
        #endregion

        /// <summary>
        /// ファイルをダウンロードする
        /// </summary>
        #region downLoad
        public static void downLoad_old(string uri, string cacheFilePath)
        {
            WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(uri, cacheFilePath);
            }
            catch (System.Net.WebException)
            {
            }
            catch
            {

            }
            finally
            {
                wc.Dispose();
                wc = null;
            }
        }
        #endregion

        /// <summary>
        /// jpgファイル名を取得する
        /// </summary>
        /// <returns></returns>
        #region getJpgFileName
        private static string getJpgFileName(string jpgUrl)
        {
            Regex r = new Regex(@"/[-_.!~*'()a-zA-Z0-9;?:@&=+$,%#]+jpg", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //TextBox1.Text内で正規表現と一致する対象をすべて検索
            MatchCollection mc = r.Matches(jpgUrl);
            try
            {
                foreach (System.Text.RegularExpressions.Match m in mc)
                {
                    //正規表現に一致したグループと位置を表示
                    return DateTime.Now.ToString("yyyyMMddHHmmss") + m.Groups[0].ToString().Substring(1);
                }
                return DateTime.Now.ToString("yyyyMMddHHmmss") + LpsLiplisUtil.getName(5) + ".jpg";
            }
            catch
            {
                return DateTime.Now.ToString("yyyyMMddHHmmss") + LpsLiplisUtil.getName(5) + ".jpg";
            }
            finally
            {
                r = null;
                mc = null;
            }
        }
        #endregion
    }
}
