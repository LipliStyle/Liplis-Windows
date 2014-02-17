using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Liplis.Web
{
    public static class HttpResponseCheck
    {
        /// <summary>
        /// ターゲットのURLのレスポンスコードを取得する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getHttpStatus
        public static HttpStatusCode getHttpStatus(string url)
        {
            try
            {
                //WebRequestの作成
                HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(url);
                webreq.Timeout = 10000;

                HttpWebResponse webres = null;
                try
                {
                    //サーバーからの応答を受信するためのWebResponseを取得
                    webres = (HttpWebResponse)webreq.GetResponse();

                    //応答ステータスコードを表示する
                    return webres.StatusCode;
                }
                catch (System.Net.WebException ex)
                {
                    //HTTPプロトコルエラーかどうか調べる
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        //HttpWebResponseを取得
                        HttpWebResponse errres = (HttpWebResponse)ex.Response;
 
                        return errres.StatusCode;
                    }
                    else
                    {
                        return HttpStatusCode.NotFound;
                    }

                }
                finally
                {
                    //閉じる
                    if (webres != null){webres.Close();} 
                }
            }
            catch
            {
                return HttpStatusCode.NotFound;
            }
        }
        #endregion

        /// <summary>
        /// ターゲットのURLの生存チェック
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region chekcLiveUrl
        public static bool chekcLiveUrl(string url)
        {
            if (getHttpStatus(url) == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
