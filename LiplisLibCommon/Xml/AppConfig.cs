//=======================================================================
//  ClassName : AppConfig
//  概要      : 設定クラス
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Liplis.Common;

namespace Liplis.Xml
{
    public class AppConfig : XmlReadList
    {
        public Dictionary<string, string> keyValueList;


        ///====================================================================
        ///
        ///                             初期処理
        ///                         
        ///====================================================================


        /// <summary>
        /// コンストラクター
        /// </summary>
        #region AppConfig
        public AppConfig(string path)
        {
            initAppConfig(path);
        }
        public AppConfig()
        {
            initAppConfig(LpsPathController.getAppPath() + "\\App.config");
        }
        #endregion


        /// <summary>
        /// Appconfigの初期化
        /// </summary>
        /// <param name="path"></param>
        #region initAppConfig(
        private void initAppConfig(string path)
        {
            try
            {
                //インスタンス化
                keyValueList = new Dictionary<string, string>();
                xmlDoc = new XmlDocument();

                //キャッシュファイルパスの指定
                xmlFilePath = path;

                //xmlの読込
                readXml();
                readResult();
            }
            catch (System.Exception err)
            {
                //設定ファイルの読み込みエラーの旨、異常位置を知らせるウインドウを出すべき
                lc.writingLog("SettingController : コンストラクター : 設定ファイルが存在しないため作成します\n" + err);
                createDefault();
            }
        }
        #endregion


        /// <summary>
        /// readResult
        /// readXmlで読み込んだ結果を自変数に読み込む
        /// </summary>
        #region readResult
        public void readResult()
        {
            int idx = 0;
            try
            {
                //読み込んだノードリストを対象のリストに格納する
                foreach (XmlNode node in xmlDoc.SelectNodes(sxml.ADD))
                {
                    if (node.Attributes.Count == 2)
                    {
                        keyValueList.Add(node.Attributes[0].InnerText, node.Attributes[1].InnerText);
                    }
                    else if (node.Attributes.Count == 1)
                    {
                        keyValueList.Add(node.Attributes[0].InnerText, "");
                    }
                    else
                    {
                        keyValueList.Add("notFoundKey" + idx, "");
                    }
                    idx++;
                }
            }
            catch (System.Exception err)
            {
                lc.writingLog("SettingController : readResult:設定の読込失敗\n" + err);
                createDefault();
            }

        }
        #endregion

        ///====================================================================
        ///
        ///                             データ操作
        ///                         
        ///====================================================================

        /// <summary>
        /// 該当キーの値を読み込む
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        #region getValueString
        public string getValueString(string key)
        {
            try
            {
                return keyValueList[key];
            }
            catch
            {
                keyValueList[key] = "";
                return "notFound";
            }
        }
        #endregion


        /// <summary>
        /// 該当キーの値を読み込む
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        #region getValueInt
        public int getValueInt(string key)
        {
            try
            {
                return int.Parse(keyValueList[key]);
            }
            catch
            {
                keyValueList[key] = "0";
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// 該当キーの値を読み込む
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        #region getValueBool
        public bool getValueBool(string key)
        {
            try
            {
                return int.Parse(keyValueList[key]) == 1;
            }
            catch
            {
                keyValueList[key] = "0";
                return false;
            }
        }
        #endregion


        /// <summary>
        /// 該当キーの値をセットする
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        #region setValueString
        public void setValueString(string key, string value)
        {
            try
            {
                keyValueList[key] = value;
            }
            catch
            {
                keyValueList.Add(key, value);
            }
        }

        #endregion

        /// <summary>
        /// 該当キーの値をセットする
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        #region setValueInt
        public void setValueInt(string key, int value)
        {
            try
            {
                keyValueList[key] = value.ToString();
            }
            catch
            {
                keyValueList.Add(key, value.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 該当キーの値をセットする
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        #region setValueBool
        public void setValueBool(string key, bool value)
        {
            try
            {
                if (value)
                {
                    keyValueList[key] = "1";
                }
                else
                {
                    keyValueList[key] = "0";
                }
               
            }
            catch
            {
                keyValueList[key] = "0";
            }
        }
        #endregion


        /// <summary>
        /// 設定を初期化する(すべての設定情報をクリア)
        /// </summary>
        #region clearSetting
        public void clearSetting()
        {
            keyValueList.Clear();
        }
        #endregion

        ///====================================================================
        ///
        ///                             保存処理
        ///                         
        ///====================================================================

        /// <summary>
        /// saveMonoSettings
        /// 現在自クラスにセットされている値を設定ファイルに書き込む
        /// </summary>
        #region saveSettings
        public void saveSettings()
        {
            //XMLライタの宣言
            XmlWriter writer = null;
            XmlWriterSettings st = new System.Xml.XmlWriterSettings();
            st.Encoding = Encoding.UTF8;    //文字コードを指定する
            st.Indent = true;                           //インデントを指定する
            st.IndentChars = ("\t");                    //インデントにタブを指定

            //エラー処理は後で考えよう
            try
            {
                writer = XmlWriter.Create(xmlFilePath, st);
                writer.WriteStartDocument(true);

                //XMLの定義
                writer.WriteStartElement("configuration");
                writer.WriteStartElement("appSettings");
                foreach (string key in keyValueList.Keys)
                {
                    writer.WriteStartElement("add");
                    writer.WriteAttributeString("key", key);
                    writer.WriteAttributeString("value", keyValueList[key]);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();

            }
            catch (System.Exception err)
            {
                Console.Write(err);
            }
            finally
            {
            }
        }
        #endregion

        /// <summary>
        /// デフォルトファイルの作成
        /// </summary>
        #region createDefault
        public void createDefault()
        {
            saveSettings();
        }
        #endregion
    }
    
    class sxml
    {
        //メイン設定
        public const string ADD = "/configuration/appSettings/add";
    }
}