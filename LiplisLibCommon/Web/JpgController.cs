//=======================================================================
//  ClassName : objJpg
//  概要      : jpgオブジェクト
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using Liplis.Common;
using Liplis.Xml;


namespace Liplis.Web
{
    public class JpgController : XmlReadList, IDisposable
    {

        public JpgController()
        {
            lc = new LpsLogController();
        }

        /// <summary>
        /// サムネイルを取得する
        /// </summary>
        /// <returns></returns>
        public Bitmap getThumbnail(string thumbnail_url, string dlPath)
        {
            string fileName = "";
            try
            {
                if (!checkFileExist(dlPath))
                {
                    fileName = dlPath + getJpgFileName(thumbnail_url);
                    downLoad(thumbnail_url, fileName);
                    return new Bitmap(fileName);
                }

                return new Bitmap(0,0);
            }
            catch (System.Exception err)
            {
                lc.writingLog("objJpg : getThumbnail \n" + err);
                return new Bitmap(0,0);
            }
        }

        /// <summary>
        /// サムネイルをダウンロードし、名前を付けて保存する.
        /// ダウンロードしたパスを返す。
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cacheFilePath"></param>
        public string downLoadthumb(string uri, string cacheFilePath)
        {
            string fileName = "";
            try
            {
                if (!checkFileExist(cacheFilePath))
                {
                    fileName = cacheFilePath + getJpgFileName(uri);
                    downLoad(uri, fileName);
                }
                return fileName;
            }
            catch (System.Exception err)
            {
                lc.writingLog("objJpg : getThumbnail \n" + err);
                return fileName;
            }
        }

        /// <summary>
        /// サムネイルをダウンロードする
        /// </summary>
        public void downLoad(string uri, string cacheFilePath)
        {
            WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(uri, cacheFilePath);
            }
            catch (System.Net.WebException)
            {
            }
            catch (System.Exception err)
            {
                lc.writingLog("objJpg : downLoadthumb \n" + err);
            }
            finally
            {
                wc.Dispose();
                wc = null;
            }
        }

        /// <summary>
        /// jpgファイル名を取得する
        /// </summary>
        /// <returns></returns>
        private string getJpgFileName(string jpgUrl)
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
            catch (System.Exception err)
            {
                lc.writingLog("objJpg : downLoadthumb \n" + err);
                return DateTime.Now.ToString("yyyyMMddHHmmss") + LpsLiplisUtil.getName(5) + ".jpg";
            }
            finally
            {
                r = null;
                mc = null;
            }
        }

        public void Dispose()
        {
            lc = null;
        }

        /// <summary>
        /// ファイル存在チェック
        /// </summary>
        /// <returns></returns>
        public static bool checkFileExist(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}
