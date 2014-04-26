//=======================================================================
//  ClassName : VersionXml
// ■概要     : バージョンXMLインスタンス
//
//■ Liplis4.0
//　2014/04/26 Liplis4.0 アップデート機能
// Copyright(c) 2014 LipliStyle さちん MITライセンス
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Liplis.Common;
using Liplis.Msg;

namespace Liplis.Xml
{
    public class VersionXml : XmlReadList
    {
        ///=============================
        /// 定数
        public const string VERSION = "/liplis/version";
        public const string FILE = "/liplis/file";
        public const string TARGET_VERSION = "/liplis/target/version";
        public const string TARGET_ENABLE = "/liplis/target/enable";
        public const string TARGET_OPTION = "/liplis/target/option";

        ///=============================
        /// キーバリューリスト
        public string version { get; set; }
        public string file { get; set; }
        public List<msgTargetVersion> lstTarget;

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region VersionXml
        public VersionXml(string url)
        {
            try
            {
                //インスタンス化
                version = "";
                lstTarget = new List<msgTargetVersion>();
                xmlDoc = new XmlDocument();

                //キャッシュファイルパスの指定
                xmlFilePath = url;

                //xmlの読込
                readXml();
                readResult();
            }
            catch (System.Exception err)
            {
                createDefault();
            }
        }
        #endregion

        /// <summary>
        /// 設定読込
        /// </summary>
        private void readResult()
        {
            List<string> lstVersion = new List<string>();
            List<string> lstEnable = new List<string>();
            List<string> lstOption = new List<string>();

            readXmlList(xmlDoc.SelectNodes(TARGET_VERSION), lstVersion);
            readXmlList(xmlDoc.SelectNodes(TARGET_ENABLE), lstEnable);
            readXmlList(xmlDoc.SelectNodes(TARGET_OPTION), lstOption);

            int idx = 0;

            //取得したバージョンデータを回してリストを作成する
            foreach (string ver in lstVersion)
            {
                lstTarget.Add(new msgTargetVersion(ver, lstEnable[idx], lstOption[idx]));
                idx++;
            }

            //バージョン、ファイルの取得
            version = rXLMSStr(xmlDoc.SelectNodes(VERSION));
            file = rXLMSStr(xmlDoc.SelectNodes(FILE));
        }

        /// <summary>
        /// デフォルトの読み込み
        /// </summary>
        private void createDefault()
        {
            version = "";
            lstTarget = new List<msgTargetVersion>();
        }

        /// <summary>
        /// 同じバージョンのメッセージインスタンスを取得する
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public msgTargetVersion getSameVersion(string version)
        {
            foreach (msgTargetVersion msg in lstTarget)
            {
                if (msg.version.Equals(version))
                {
                    return msg;
                }
            }

            return new msgTargetVersion("","","");
        }

    }
}
