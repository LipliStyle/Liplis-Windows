//=======================================================================
//  ClassName : mailSetting
//  概要      : メール設定
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin.Sachin
//=======================================================================

using System.Collections.Generic;
using System.Xml;
using Liplis.Common;
namespace Liplis.Xml
{
    public class mailSetting : XmlReadList
    {
        public string fromAddress;
        public string fromName;
        public List<string> toAddress;
        public string smtp;
        public string domain;
        public string account;
        public string pass;

                /// <summary>
        /// コンストラクター
        /// </summary>
        public mailSetting(string path)
        {
            try
            {
                //インスタンス化
                xmlDoc = new XmlDocument();

                //キャッシュファイルパスの指定
                xmlFilePath = path;

                //xmlの読込
                readXml();
                readResult();
            }
            catch
            {
                //設定ファイルの読み込みエラーの旨、異常位置を知らせるウインドウを出すべき
                lc.writingLog("メール送信エラー");
                createDefault();
            }
        }


        /// <summary>
        /// readResult
        /// readXmlで読み込んだ結果を自変数に読み込む
        /// </summary>
        public void readResult()
        {
            try
            {
                toAddress = new List<string>();
                fromAddress = rXLMSStr(xmlDoc.SelectNodes(smxml.FROM_ADDRESS));
                fromName = rXLMSStr(xmlDoc.SelectNodes(smxml.FROM_NAME));
                readXmlList(xmlDoc.SelectNodes(smxml.TO_ADDRESS),toAddress);
                smtp = rXLMSStr(xmlDoc.SelectNodes(smxml.SMTP));
                domain = rXLMSStr(xmlDoc.SelectNodes(smxml.DOMAIN));
                account = rXLMSStr(xmlDoc.SelectNodes(smxml.ACCOUNT));
                pass = rXLMSStr(xmlDoc.SelectNodes(smxml.PASS));
            }
            catch (System.Exception err)
            {
                lc.writingLog("mailSetting : readResult:設定の読込失敗\n" + err);
                createDefault();
            }

        }
        /// <summary>
        /// readResult
        /// readXmlで読み込んだ結果を自変数に読み込む
        /// </summary>
        public void createDefault()
        {
            try
            {
                fromAddress = "";
                fromName = "";
                toAddress = new List<string>();
                smtp = "";
                domain = "";
                account = "";
                pass = "";
            }
            catch (System.Exception err)
            {
                lc.writingLog("mailSetting : readResult:設定の読込失敗\n" + err);
            }

        }


    }

        /// <summary>
    /// SettingXpathMapList
    /// XPathの設定
    /// </summary>
    #region smxml
    class smxml
    {
        public const  string FROM_ADDRESS = "/mailSetting/fromAddress";
        public const string FROM_NAME = "/mailSetting/fromName";
        public const string TO_ADDRESS = "/mailSetting/toAddress";
        public const string SMTP = "/mailSetting/smtp";
        public const string DOMAIN = "/mailSetting/domain";
        public const string ACCOUNT = "/mailSetting/account";
        public const string PASS = "/mailSetting/pass";
    }
    #endregion 
}
