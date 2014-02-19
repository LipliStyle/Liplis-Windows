//=======================================================================
//  ClassName : objNicoSearchMovieList
//  概要      : ニコサーチ動画りすとオブジェクト
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;

namespace Liplis.Web.Nico
{
    /// <summary>
    /// 検索結果＆ページ＆動画リスト
    /// </summary>
    public class objNicoSearchMovieList
    {
        public List<objNicoInfo> lmovie { get; set; }
        public objNicoSearchInfo sinfo { get; set; }

        public objNicoSearchMovieList()
        {
            lmovie = new List<objNicoInfo>();
            sinfo = new objNicoSearchInfo();
        }

        /// <summary>
        /// ページ情報取得
        /// </summary>
        /// <param name="html">HTMLページ</param>
        public void GetMovieData(string html)
        {
            int start = html.IndexOf("searchInfo");
            //int end = 0;
            if (start >= 0) //Exist
            {
                GetSearchInfo(html);
                //GetMovieInfo(html, start, end);
            }
        }
        /// <summary>
        /// 動画情報取得
        /// </summary>
        /// <param name="html">HTMLページ</param>
        /// <param name="start">先頭</param>
        /// <param name="end">後尾</param>
        private void GetMovieInfo(string html, int start, int end)
        {
            if (GetStringPosition(html, "id:\"", "\"", ref start, ref end) == false)
            {
                return;
            }
            string id = html.Substring(start, end - start);

            GetStringPosition(html, "title:\"", "\"", ref start, ref end);
            string title = html.Substring(start, end - start);

            this.lmovie.Add(new objNicoInfo(id, title));

            GetMovieInfo(html, start, end);
        }

        /// <summary>
        /// 検索結果情報
        /// </summary>
        /// <param name="html">HTMLページ</param>
        private void GetSearchInfo(string html)
        {
            int start = 0;
            int end = 0;

            start = html.IndexOf("page:");

            GetStringPosition(html, "page: ", ",", ref start, ref end);
            sinfo.page = html.Substring(start, end - start);
            GetStringPosition(html, "query: ", ",", ref start, ref end);
            sinfo.query = html.Substring(start, end - start);
            GetStringPosition(html, "words: ", ",", ref start, ref end);
            sinfo.words = html.Substring(start, end - start);
            GetStringPosition(html, "total: ", ",", ref start, ref end);
            sinfo.total = Convert.ToInt32(html.Substring(start, end - start));
            GetStringPosition(html, "offset: ", ",", ref start, ref end);
            sinfo.offset = Convert.ToInt32(html.Substring(start, end - start));
            GetStringPosition(html, "length: ", ",", ref start, ref end);
            sinfo.length = Convert.ToInt32(html.Substring(start, end - start));

        }

        /// <summary>
        /// 文字列切り出し位置の取得
        /// </summary>
        /// <param name="html">参照するhtmlページ</param>
        /// <param name="startterm">検索する前の文字</param>
        /// <param name="endterm">検索する後の文字</param>
        /// <param name="start">参照型　検索位置最初</param>
        /// <param name="end">参照型　検索位置最後</param>
        /// <returns></returns>
        private bool GetStringPosition(string html, string startterm, string endterm, ref int start, ref int end)
        {
            start = html.IndexOf(startterm, start);
            if (start == -1) return false;
            start += (startterm.Length);
            end = html.IndexOf(endterm, start);
            return true;
        }
    }
}
