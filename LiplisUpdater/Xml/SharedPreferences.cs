using System;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using Liplis.Common;
using System.Reflection;

namespace Liplis.Xml
{
    public class SharedPreferences : XmlReadList
    {
        ///=============================
        /// 定数
        public const string ADD = "/configuration/appSettings/add";

        ///=============================
        /// キーバリューリスト
        public Dictionary<string, string> keyValueList;

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region SharedPreferences
        public SharedPreferences()
        {
            try
            {
                //インスタンス化
                keyValueList = new Dictionary<string, string>();
                xmlDoc = new XmlDocument();

                //キャッシュファイルパスの指定
                xmlFilePath = LpsPathControllerCus.getAppPath() + "\\App.config";

                //xmlの読込
                readXml();
                readResult();
            }
            catch (System.Exception err)
            {
                //設定ファイルの読み込みエラーの旨、異常位置を知らせるウインドウを出すべき
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "設定ファイルが存在しないため作成します" + Environment.NewLine + err);
                createDefault();
            }
        }
        #endregion

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="path">設定ファイルパス</param>
        #region SharedPreferences
        public SharedPreferences(string path)
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
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " :  設定ファイルが存在しないため作成します" + Environment.NewLine + err);
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
                foreach (XmlNode node in xmlDoc.SelectNodes(ADD))
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
                LpsLogControllerCus.writingLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "設定の読込失敗" + Environment.NewLine + err);
                createDefault();
            }

        }
        #endregion

        /// <summary>
        /// 該当キーの値を読み込む
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="defStr">デフォルト文字</param> 
        /// <returns></returns>
        #region getString
        public string getString(string key, string defStr)
        {
            try
            {
                return keyValueList[key];
            }
            catch
            {
                keyValueList[key] = defStr;
                return defStr;
            }
        }
        #endregion

        /// <summary>
        /// 該当キーの値を読み込む
        /// </summary>
        /// <param name="key">キー</param>
        /// /// <param name="defVal">デフォルト値</param> 
        /// <returns>値</returns>
        #region getInt
        public int getInt(string key, int defVal)
        {
            try
            {
                return int.Parse(keyValueList[key]);
            }
            catch
            {
                keyValueList[key] = defVal.ToString();
                return defVal;
            }
        }
        #endregion

        /// <summary>
        /// 該当キーの値をセットする
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        #region setString
        public void setString(string key, string value)
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
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        #region setInt
        public void setInt(string key, int value)
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
        /// 設定を初期化する(すべての設定情報をクリア)
        /// </summary>
        #region clearSetting
        public void clearSetting()
        {
            keyValueList.Clear();
        }
        #endregion

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
            st.Encoding = Encoding.GetEncoding(932);    //文字コードを指定する
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
}