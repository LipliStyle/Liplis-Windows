//=======================================================================
//  ClassName : ProxyController
//  概要      : プロキシーコントローラー
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Liplis.Common;

namespace Liplis.Web
{
    public class ProxyController
    {
        //プロキシリスト
        public LstShufflableList<string> resultList;

        private int cnt;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="path">パス</param>
        public ProxyController(string path)
        {
            //結果リスト
            resultList = new LstShufflableList<string>();

            //プロキシィリストを読み込む
            loadProxyList(path, Encoding.GetEncoding("sjis"));

            //カウント
            cnt = 0;
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="path">パス</param>
        /// <param name="enc">エンコード</param>
        public ProxyController(string path, Encoding enc)
        {
            //結果リスト
            resultList = new LstShufflableList<string>();

            //プロキシィリストを読み込む
            loadProxyList(path, enc);

            //カウント
            cnt = 0;
        }

        /// <summary>
        /// プロキシを読み込む
        /// </summary>
        /// <param name="path"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public List<string> loadProxyList(string path,Encoding enc)
        {
            string line = "";

            //テキストファイル読み込み
            StreamReader sr = new StreamReader(path, enc);

            //処理
            while ((line = sr.ReadLine()) != null)
            {
                resultList.Add(line);
            }
            sr.Close();

            //結果を返す
            return resultList;
        }

        /// <summary>
        /// 次のプロキシィを取得する
        /// </summary>
        /// <returns></returns>
        public WebProxy getNextProxy()
        {
            try
            {
                //カウントが1以下なら塗るを返す
                if (resultList.Count < 1)
                {
                    return null;
                }

                //カウントのリセット
                if (cnt > resultList.Count)
                {
                    cnt = 0;
                }

                string[] buf = resultList[cnt++].Split(':');

                return new WebProxy(buf[0], int.Parse(buf[1]));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 現在のプロキシ設定を返す
        /// </summary>
        /// <returns></returns>
        public string getNowProxy()
        {
            try
            {
                return resultList[cnt];
            }
            catch
            {
                return null;
            }
        }
    }
}
