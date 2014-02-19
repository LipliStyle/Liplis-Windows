//=======================================================================
//  ClassName : SqlUtilMySql
//  概要      : エスキューエルユーティル(マイエスキューエル)
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Liplis.Sql
{
    class SqlUtilMySql
    {
                //コネクション
        private MySqlConnection sqlCon;
        private string conStr;

        //コンストラクター
        public SqlUtilMySql(string serverAddress, string userName, string pass, string dataBaseName)
        {
            //コネクションが存在していたら閉じる
            if (sqlCon != null)
            {
                sqlCon.Close();
            }
            //接続文字列の設定
            //conStr = String.Format("server=" + serverAddress + " user id=" + userName + " password=" + pass + " database=aviutlautotranse; pooling=false");
            conStr = String.Format("server={0};user id={1}; password={2}; database=aviutlautotranse; pooling=false", serverAddress, userName, pass);
            try
            {
                sqlCon = new MySqlConnection(conStr);
                sqlCon.Open();
            }
            catch
            {
                MessageBox.Show("接続失敗");
            }
        }

        public string test()
        {
            string result = "";
            string sqlString = "select Anime_Name from animedatabase where Anime_ID='00001'";
            //sqlString = "insert animedatabase (Anime_ID,Anime_Name) values ('00065','あああ')";
            MySqlDataReader reader = null;
            MySqlCommand cmd = new MySqlCommand(sqlString, sqlCon);


            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //result = reader.GetString(0); // ←これで内容を取りだせる。
                    result = ConvertEncoding(reader.GetString(0)
                         , System.Text.Encoding.GetEncoding("Shift-Jis")
                         , System.Text.Encoding.UTF8);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Failed to populate database list: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }

            return result;
        }

        public string ConvertEncoding(string _src
                                   , System.Text.Encoding _srcEncoding
                                   , System.Text.Encoding _destEncoding)
        {
            byte[] srcBytes = _srcEncoding.GetBytes(_src);
            return _destEncoding.GetString(srcBytes);
        }
    }
}
