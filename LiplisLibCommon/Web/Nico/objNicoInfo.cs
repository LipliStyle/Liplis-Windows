//=======================================================================
//  ClassName : objNicoInfo
//  概要      : ニコインフォオブジェクト
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin.Sachin
//=======================================================================
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Xml;
using Liplis.Common;
using Liplis.Xml;

namespace Liplis.Web.Nico
{
    public class objNicoInfo : XmlReadList
    {
        ///===========================================
        /// 設定値
        public string   video_id        { get; set; }   //ニコビデオID
        public string   title           { get; set; }   //タイトル
        public string   description     { get; set; }   //動画の説明
        public string   thumbnail_url   { get; set; }   //サムネイルURL
        public string   first_retrieve  { get; set; }   //掲載日
        public string   length          { get; set; }   //長さ
        public long     view_counter    { get; set; }   //再生数
        public long     comment_num     { get; set; }   //コメント数      
        public long     mylist_counter  { get; set; }   //マイリス数
        public string   last_res_body   { get; set; }   //コメント
        public string   watch_url       { get; set; }   //URL
        public string   thumb_type      { get; set; }   //サムネイルタイプ
        public string   embeddable      { get; set; }   //embeddable

        public const string NICO_DOMAIN = @"http://www.nicovideo.jp/";
        public const string NICO_INFO = "http://ext.nicovideo.jp/api/getthumbinfo/";

        ///===========================================
        /// タグリスト
        public List<string> tagList { get; set; }   //タグリスト

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region objNicoInfo
        public objNicoInfo(string nicoVideoId)
        {
            try
            {
                //インスタンス化
                xmlDoc = new XmlDocument();

                //キャッシュファイルパスの指定
                xmlFilePath = createNicoInfoUri(nicoVideoId);

                //xmlの読込
                readXml();
                readResult();
            }
            catch (System.Exception err)
            {
                lc.writingLog("NicoInfoObject : コンストラクター : " + err);
                createDefault();
            }
        }

        public objNicoInfo(string id, string title)
        {
            setKara();
            this.video_id = id;
            this.title = title;
        }

        public objNicoInfo()
        {
            setKara();
        }
        #endregion

        /// <summary>
        /// readResult
        /// readXmlで読み込んだ結果を自変数に読み込む
        /// </summary>
        #region readResult
        public void readResult()
        {
            try
            {
                //メイン設定の読込
                video_id = rXLMSStr(xmlDoc.SelectNodes(nicxm.VIDEO_ID));        //名前
                title = rXLMSStr(xmlDoc.SelectNodes(nicxm.TITLE));           //タイトル
                description = rXLMSStr(xmlDoc.SelectNodes(nicxm.DISCRIPTION));     //説明
                thumbnail_url = rXLMSStr(xmlDoc.SelectNodes(nicxm.THUMBNAIL_URL));   //サムネイルURL
                first_retrieve = rXLMSStr(xmlDoc.SelectNodes(nicxm.FIRST_RETRIEVE));  //掲載日
                length = rXLMSStr(xmlDoc.SelectNodes(nicxm.LENGTH));          //ビデオの長さ
                view_counter = rXLMSInt(xmlDoc.SelectNodes(nicxm.VIEW_COUNTER));    //再生数
                comment_num = rXLMSInt(xmlDoc.SelectNodes(nicxm.COMMENT_NUM));     //コメント数
                mylist_counter = rXLMSInt(xmlDoc.SelectNodes(nicxm.MYLIST_COUNTER));  //マイリスト登録数
                last_res_body = rXLMSStr(xmlDoc.SelectNodes(nicxm.LAST_RES_BODY));   //最終レス内容
                watch_url = rXLMSStr(xmlDoc.SelectNodes(nicxm.WATCH_URL));       //URL
                thumb_type = rXLMSStr(xmlDoc.SelectNodes(nicxm.THUMB_TYPE));      //サムネイルタイプ
                embeddable = rXLMSStr(xmlDoc.SelectNodes(nicxm.EMBEDDABLE));      //外部プレイヤーから再生できるか
            }
            catch (System.Exception err)
            {
                lc.writingLog("NicoInfoObject : readResult:設定の読込失敗\n" + err);

            }

        }
        #endregion

        /// <summary>
        /// setKara
        /// 空で初期化
        /// </summary>
        #region setKara
        private void setKara()
        {
            video_id = "";
            title = "";
            description = "";
            thumbnail_url = "";
            first_retrieve = "";
            length = "";
            view_counter = 0;
            comment_num = 0;
            mylist_counter = 0;
            last_res_body = "";
            watch_url = "";
            thumb_type = "";
            embeddable = "";
        }
        #endregion

        /// <summary>
        /// saveMonoSettings
        /// 現在自クラスにセットされている値を設定ファイルに書き込む
        /// </summary>
        #region saveSettings
        public void saveSettings()
        {
            //読み取り専用
        }
        #endregion

        /// <summary>
        /// createDefault
        /// デフォルトファイルの作成
        /// </summary>
        #region createDefault
        public void createDefault()
        {
            //読み取り専用
        }
        #endregion


        /// <summary>
        /// ニコインフォのURIを生成する
        /// </summary>
        #region createNicoInfoUri
        private string createNicoInfoUri(string nicoVideoId)
        {
            try
            {
                return NICO_INFO + nicoVideoId;
            }
            catch (System.Exception err)
            {
                lc.writingLog("NicoInfoObject : createNicoInfoUri:設定の読込失敗\n" + err);
                //読み込み失敗時、定義クラスから拾ってみる
                return NICO_DOMAIN + nicoVideoId;
            }
        }
        #endregion


        /// <summary>
        /// getThumbnail
        /// サムネイルを取得する
        /// </summary>
        /// <returns></returns>
        #region getThumbnail
        public Bitmap getThumbnail(string dlPath)
        {
            try
            {
                if (!LpsPathController.checkFileExist(dlPath))
                {
                    downLoadthumb(this.thumbnail_url, dlPath);
                }

                return new Bitmap(dlPath);
            }
            catch (System.Exception err)
            {
                lc.writingLog("NicoInfoObject : getThumbnail \n" + err);
                return null;
            }
        }
        #endregion


        /// <summary>
        /// サムネイルをダウンロードする
        /// </summary>
        #region downLoadthumb
        private void downLoadthumb(string uri, string cacheFilePath)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadFile(uri, cacheFilePath);
            }
            catch (System.Exception err)
            {
                lc.writingLog("NicoInfoObject : downLoadthumb \n" + err);
            }
        }
        #endregion

    }

    /// <summary>
    /// ReadSettingXpathMapList
    /// XPathの設定
    /// </summary>
    #region nicxm
    class nicxm
    {
        //メイン設定
        public const string VIDEO_ID = "/nicovideo_thumb_response/thumb/video_id";
        public const string TITLE = "/nicovideo_thumb_response/thumb/title";
        public const string DISCRIPTION = "/nicovideo_thumb_response/thumb/description";
        public const string THUMBNAIL_URL = "/nicovideo_thumb_response/thumb/thumbnail_url";
        public const string FIRST_RETRIEVE = "/nicovideo_thumb_response/thumb/first_retrieve";
        public const string LENGTH = "/nicovideo_thumb_response/thumb/length";
        public const string VIEW_COUNTER = "/nicovideo_thumb_response/thumb/view_counter";
        public const string COMMENT_NUM = "/nicovideo_thumb_response/thumb/comment_num";
        public const string MYLIST_COUNTER = "/nicovideo_thumb_response/thumb/mylist_counter";
        public const string LAST_RES_BODY = "/nicovideo_thumb_response/thumb/last_res_body";
        public const string WATCH_URL = "/nicovideo_thumb_response/thumb/watch_url";
        public const string THUMB_TYPE = "/nicovideo_thumb_response/thumb/thumb_type";
        public const string EMBEDDABLE = "/nicovideo_thumb_response/thumb/embeddable";

        public const string TAG = "/nicovideo_thumb_response/thumb/tags/tag";
    }
    #endregion

}
