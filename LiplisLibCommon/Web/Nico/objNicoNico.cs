//=======================================================================
//  ClassName : objNicoNico
//  概要      : ニコニコ動画オブジェクト
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace Liplis.Web.Nico
{
    public class objNicoNico
    {
        private bool _isLogin;
        private CookieContainer cookie_login;
        private const string REGEX_PTN_sm_uri = "^http://www.nicovideo.jp/watch/sm(?<videoid>[0-9]+)$";
        private const string URI_getflv = "http://www.nicovideo.jp/api/getflv?v=sm";
        private const string URI_Login = "https://secure.nicovideo.jp/secure/login?site=niconico";

        public objNicoNico(string username, string password)
        {
            this.login(username, password);
        }

        public XmlTextReader getCommentXML(string URI, int commentCount)
        {
            Hashtable hashtable = this.getMovieInfo(URI);
            if (hashtable == null)
            {
                return null;
            }
            string url = hashtable["ms"] as string;
            if (url == null)
            {
                return null;
            }
            string str2 = hashtable["thread_id"] as string;
            if (str2 == null)
            {
                return null;
            }
            string arg = string.Format("<thread res_from=\"-{0}\" version=\"20061206\" thread=\"{1}\" />", commentCount, str2);
            string s = this.HttpPost(url, arg, ref this.cookie_login);
            return new XmlTextReader(new MemoryStream(Encoding.UTF8.GetBytes(s), false));
        }

        public Stream getMovie(string URI, out string fileName, out long fileSize)
        {
            fileSize = 0L;
            string[] strArray = URI.Split(new char[] { '/' });
            fileName = strArray[strArray.Length - 1] + ".flv";
            Hashtable hashtable = this.getMovieInfo(URI);
            if (hashtable == null)
            {
                return null;
            }
            string url = hashtable["url"] as string;
            if (url == null)
            {
                return null;
            }
            this.HttpGet(URI, ref this.cookie_login);
            return this.HttpGetMovie(url, ref this.cookie_login, out fileSize);
        }

        public Hashtable getMovieInfo(string URI)
        {
            string str = "";
            if (this._isLogin)
            {
                if (this.cookie_login == null)
                {
                    return null;
                }
                Match match = new Regex("^http://www.nicovideo.jp/watch/sm(?<videoid>[0-9]+)$").Match(URI);
                if (match.Success)
                {
                    str = match.Groups["videoid"].ToString();
                    string url = "http://www.nicovideo.jp/api/getflv?v=sm" + str;
                    string str3 = this.HttpGet(url, ref this.cookie_login);
                    Hashtable hashtable = new Hashtable();
                    foreach (string str5 in HttpUtility.UrlDecode(str3, Encoding.UTF8).Split(new char[] { '&' }))
                    {
                        System.Windows.Forms.Application.DoEvents();
                        string[] strArray2 = str5.Split(new char[] { '=' });
                        if (strArray2.Length != 2)
                        {
                            hashtable[strArray2[0]] = strArray2[1] + "=" + strArray2[2];
                        }
                        else
                        {
                            hashtable[strArray2[0]] = strArray2[1];
                        }
                    }
                    return hashtable;
                }
            }
            return null;
        }

        private string HttpGet(string url, ref CookieContainer cc)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cc;
            Stream responseStream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            string str = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();
            return str;
        }

        private Stream HttpGetMovie(string url, ref CookieContainer cc, out long fileSize)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cc;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            fileSize = response.ContentLength;
            return responseStream;
        }

        private string HttpPost(string url, Hashtable vals, ref CookieContainer cc)
        {
            string s = "";
            foreach (string str2 in vals.Keys)
            {
                s = s + string.Format("{0}={1}&", str2, vals[str2]);
            }
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            request.CookieContainer = cc;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            Stream responseStream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            string str3 = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();
            return str3;
        }

        private string HttpPost(string url, string arg, ref CookieContainer cc)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(arg);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            request.CookieContainer = cc;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            Stream responseStream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            string str = reader.ReadToEnd();
            reader.Close();
            responseStream.Close();
            return str;
        }

        public bool login(string username, string password)
        {
            //事前チェック
            if (username.Equals("") || password.Equals(""))
            {
                this._isLogin = false;
                return false;
            }

            Hashtable vals = new Hashtable(3);
            vals["mail"] = username;
            vals["password"] = password;
            vals["next_url"] = "";
            this.cookie_login = new CookieContainer();
            if (this.HttpPost("https://secure.nicovideo.jp/secure/login?site=niconico", vals, ref this.cookie_login).IndexOf("パスワードが間違っています") != -1)
            {
                this.cookie_login = null;
                this._isLogin = false;
                return false;
            }
            this._isLogin = true;
            return true;
        }

        public bool isLogin
        {
            get
            {
                return this._isLogin;
            }
        }
    }
}
