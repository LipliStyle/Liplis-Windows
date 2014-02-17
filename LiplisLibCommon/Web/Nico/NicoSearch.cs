//=======================================================================
//  ClassName : NicoSearch
//  概要      : にこさーち
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin. All Rights Reserved. 
//=======================================================================
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Liplis.Common;
using System;

namespace Liplis.Web.Nico
{
    public class NicoSearch
    {
        public bool _isLogin { get; set; }
        private string mail;
        private string password;
        private Encoding encode;
        private WebClient wc;

        public List<string> nicoVideoIdList {get;set;}
        public List<objNicoInfo> nicoInfoList { get; set; }

        /// <summary>
        /// 動画リスト
        /// </summary>
        public objNicoSearchMovieList lmov { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="smail">メールアドレス</param>
        /// <param name="spass">パスワード</param>
        #region NicoSearch
        public NicoSearch(string smail, string spass)
        {
            this.encode = Encoding.UTF8;
            this.mail = smail;
            this.password = spass;
            lmov = new objNicoSearchMovieList();
            _isLogin = login();
        }
        #endregion


        /// <summary>
        /// ニコニコ動画にログインする
        /// </summary>
        /// <returns>Top page HTML</returns>
        #region login
        public bool login()
        {
            try
            {
                wc = new WebClient();
                string str;
                NameValueCollection nvc = new NameValueCollection();

                nvc.Add("mail", this.mail);
                nvc.Add("password", this.password);
                wc.Encoding = this.encode;

                str = this.encode.GetString(wc.UploadValues(LpsDefineMost.URL_NICO_LOGIN_PAGE_URL, nvc));
                if (str.IndexOf("ニコニコ動画　ログインフォーム") >= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion


        /// <summary>
        /// search
        /// ニコニコ検索
        /// </summary>
        /// <param name="tag">検索するタグ名</param>
        /// <returns>タグ検索したHTML</returns>
        #region search
        public void search(string tag, string opt, int sortCd)
        {
            string nicoVideoId = "";
            //ニコニコURLリストの抽出
            
            nicoVideoIdList = new List<string>();
            nicoInfoList = new List<objNicoInfo>();

            //sort=fは投稿日が新しいもの順
            for (int page = 1; page <= 50; page++)
            {
                List<string> urlList = LpsRegularEx.getNicoVideoUrl(wc.DownloadString(LpsDefineMost.URL_NICO_DOMAIN + opt + tag + getSortCode(sortCd) + "&page=" + page));

                foreach (string url in urlList)
                {
                    nicoVideoId = LpsRegularEx.getNicoId(url);

                    if (nicoVideoIdList.IndexOf(nicoVideoId) < 0)
                    {
                        nicoVideoIdList.Add(nicoVideoId);
                        nicoInfoList.Add(new objNicoInfo(nicoVideoId));
                        Console.WriteLine(nicoInfoList[nicoInfoList.Count-1].title);
                    }
                }
            }


            //検索情報の取得
            this.lmov.GetMovieData(LpsDefineMost.URL_NICO_DOMAIN + opt + tag + getSortCode(sortCd));
        }
        public void tagSearch(string tag, int sortCd)
        {
            search(tag, LpsDefineMost.NICO_SEARCH_OPT_TAG, sortCd);
        }

        public void wordSearch(string word, int sortCd)
        {
            search(word, LpsDefineMost.NICO_SEARCH_OPT_WORD, sortCd);
        }
        #endregion

        /// <summary>
        /// getSource
        /// ソースコードを取得する
        /// </summary>
        /// <param name="tag">検索するタグ名</param>
        /// <returns>タグ検索したHTML</returns>
        #region getUrlList
        public List<string> getUrlList(string tag, string opt, int sortCd, int page)
        {
            return LpsRegularEx.getNicoVideoUrl(wc.DownloadString(LpsDefineMost.URL_NICO_DOMAIN + opt + tag + getSortCode(sortCd) + "&page=" + page));
        }
        #endregion

        /// <summary>
        /// getSortCode
        /// ソートコードの取得
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        #region getSortCode
        public string getSortCode(int cd)
        {
            switch (cd)
            {
                case 0:
                    return LpsDefineMost.NICO_SEARCH_SORT_NEW_D;
                case 1:
                    return LpsDefineMost.NICO_SEARCH_SORT_NEW_A;
                case 2:
                    return LpsDefineMost.NICO_SEARCH_SORT_SAISEI_D;
                case 3:
                    return LpsDefineMost.NICO_SEARCH_SORT_SAISEI_A;
                case 4:
                    return LpsDefineMost.NICO_SEARCH_SORT_COMMENT_D;
                case 5:
                    return LpsDefineMost.NICO_SEARCH_SORT_COMMENT_A;
                case 6:
                    return LpsDefineMost.NICO_SEARCH_SORT_MYLIST_D;
                case 7:
                    return LpsDefineMost.NICO_SEARCH_SORT_MYLIST_A;
                case 8:
                    return LpsDefineMost.NICO_SEARCH_SORT_TOKO_D;
                case 9:
                    return LpsDefineMost.NICO_SEARCH_SORT_TOKO_A;
                case 10:
                    return LpsDefineMost.NICO_SEARCH_SORT_LONG_D;
                case 11:
                    return LpsDefineMost.NICO_SEARCH_SORT_LONG_A;
                default:
                    return "";
            }
        }
        #endregion
    }
}
