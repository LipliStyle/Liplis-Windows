//=======================================================================
//  ClassName : BaseEntity
//  概要      : ベースエンティティ
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin. All Rights Reserved. 
//=======================================================================
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System;

namespace Liplis.Sql
{
    public class BaseEntity
    {
        ///=============================
        /// プロパティ
        protected string stConnectionString = string.Empty;

        ///=============================
        /// トランザクション分離レベル
        #region トランザクション分離レベル
        protected const string READ_UNCOMMITTED = "READ UNCOMMITTED";
        protected const string READ_COMMITTED   = "READ COMMITTED";
        protected const string REPEATABLE_READ  = "REPEATABLE READ";
        protected const string SERIALIZABLE     = "SERIALIZABLE";
        #endregion

        /// <summary>
        /// ディスポーズ
        /// </summary>
        #region dispose
        public virtual void dispose()
        {

        }
        #endregion

        /// <summary>
        /// コネクションストリングを設定する
        /// </summary>
        #region setConnectionString
        protected void setConnectionString(string dataSource, string databaseName)
        {
            stConnectionString += "Data Source         = " + dataSource + "; ";
            stConnectionString += "Initial Catalog     = " + databaseName + "; ";
            stConnectionString += "Integrated Security = SSPI; ";
        }
        #endregion

        /// <summary>
        /// コネクションストリングを設定する
        /// </summary>
        #region setConnectionString
        public void setConnectionString(string stConnectionString)
        {
            this.stConnectionString = stConnectionString;
        }
        #endregion


        /// <summary>
        /// コネクションストリングを設定する
        /// </summary>
        #region getConnectionString
        protected string getConnectionString(string dataSource, string databaseName)
        {
            StringBuilder result = new StringBuilder();
            result.Append("Data Source         = " + dataSource + "; ");
            result.Append("Initial Catalog     = " + databaseName + "; ");
            result.Append("Integrated Security = SSPI; ");

            return result.ToString();
        }
        #endregion

        /// <summary>
        /// データベースコネクションを開く
        /// </summary>
        /// <returns>作成したコネクション</returns>
        #region openDb
        protected SqlConnection openDb()
        {
            SqlConnection cSqlConnection = (new SqlConnection(stConnectionString));
            cSqlConnection.Open();

            return cSqlConnection;
        }
        #endregion

        /// <summary>
        /// データベースコネクションを開く
        /// </summary>
        /// <returns>作成したコネクション</returns>
        #region openDb
        protected SqlConnection openDb(string conStr)
        {
            SqlConnection cSqlConnection = (new SqlConnection(conStr));
            cSqlConnection.Open();

            return cSqlConnection;
        }
        #endregion

        /// <summary>
        /// 作成したコネクションを破棄する
        /// </summary>
        /// <param name="cSqlConnection"></param>
        protected void closeDb(SqlConnection cSqlConnection)
        {
            try
            {
                if(cSqlConnection != null)
                {
                    if (cSqlConnection.State == ConnectionState.Open)
                    {
                        cSqlConnection.Close();
                        cSqlConnection.Dispose();
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// キー存在チェック
        /// </summary>
        #region keyExist
        public bool keyExist(SqlConnection con, string sqlStr)
        {
            bool result;
            try
            {
                //カウンターゲット
                // cSqlConnection から SqlCommand のインスタンスを生成する
                SqlCommand hCommand = con.CreateCommand();

                // 実行する SQL コマンドを設定する
                hCommand.CommandText = sqlStr;

                // 指定した SQL コマンドを実行して SqlDataReader を構築する
                SqlDataReader cReader = hCommand.ExecuteReader();

                //結果の取得
                result = cReader.HasRows;

                // cReader を閉じる
                cReader.Close();

                return result;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// キー存在チェック
        /// </summary>
        #region keyExist
        public bool keyExist(SqlCommand hCommand ,string sqlStr)
        {
            bool result;
            try
            {
                // 実行する SQL コマンドを設定する
                hCommand.CommandText = sqlStr;

                // 指定した SQL コマンドを実行して SqlDataReader を構築する
                SqlDataReader cReader = hCommand.ExecuteReader();

                //結果の取得
                result = cReader.HasRows;

                // cReader を閉じる
                cReader.Close();

                return result;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// キー存在チェック
        /// </summary>
        #region keyExist
        public bool keyExist(string sqlStr)
        {
            bool result;
            try
            {
                // SqlConnection の新しいインスタンスを生成
                using (SqlConnection cSqlConnection = openDb())
                {
                    //カウンターゲット
                    // cSqlConnection から SqlCommand のインスタンスを生成する
                    using (SqlCommand hCommand = cSqlConnection.CreateCommand())
                    {
                        // 実行する SQL コマンドを設定する
                        hCommand.CommandText = sqlStr;

                        // 指定した SQL コマンドを実行して SqlDataReader を構築する
                        using (SqlDataReader cReader = hCommand.ExecuteReader())
                        {
                            //結果の取得
                            result = cReader.HasRows;
                        }
                    }
                }

                return result;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        #endregion


        /// <summary>
        /// SQL文を実行する(ノンクエリ)
        /// CREATE、UPDATE等向け
        #region executeNonQuery
        protected bool executeNonQuery(SqlCommand hCommand, string pSqlStr)
        {
            try
            {
                // 実行する SQL コマンドを設定する
                hCommand.CommandText = pSqlStr;

                // SQL コマンドを実行し、影響を受けた行を返す
                int iResult = hCommand.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        protected bool executeNonQuery(SqlCommand hCommand)
        {
            try
            {
                // SQL コマンドを実行し、影響を受けた行を返す
                int iResult = hCommand.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// SQL文を実行する(ノンクエリ)
        /// CREATE、UPDATE等向け
        #region executeNonQuery
        protected bool executeNonQuery(SqlConnection cSqlConnection, string pSqlStr)
        {
            try
            {
                // cSqlConnection から SqlCommand のインスタンスを生成する
                using (SqlCommand hCommand = cSqlConnection.CreateCommand())
                {
                    // 実行する SQL コマンドを設定する
                    hCommand.CommandText = pSqlStr;

                    // SQL コマンドを実行し、影響を受けた行を返す
                    int iResult = hCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// SQL文を実行する(ノンクエリ)
        /// CREATE、UPDATE等向け
        #region executeNonQuery
        protected bool executeNonQueryOC(string pSqlStr)
        {

            try
            {
                // SqlConnection の新しいインスタンスを生成
                using (SqlConnection cSqlConnection = openDb())
                {
                    // cSqlConnection から SqlCommand のインスタンスを生成する
                    using (SqlCommand hCommand = cSqlConnection.CreateCommand())
                    {
                        // 実行する SQL コマンドを設定する
                        hCommand.CommandText = pSqlStr;

                        // SQL コマンドを実行し、影響を受けた行を返す
                        int iResult = hCommand.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// setTransactionIsolationLevel
        /// トランザクション分離レベルを設定する。
        #region 説明
        /// 分離レベルを指定すると、別の分離レベルを設定しない限り、セッションの終了時まで変更されない。
        /// 他のセッションの分離レベルは、SELECT ステートメントでテーブルレベルのロック ヒントを指定することによって無効化することができるが、
        /// テーブルレベルのロック ヒントを指定しても、セッション内のほかのステートメントには影響を与えない。
        /// 
        /// 分離レベル	分離レベル	ダーティリード	反復不可能読み取り	ファントム
        ///  低	READ UNCOMMITTED	可	            可	                可
        /// ↑	READ COMMITTED	    不可	        可	                可
        /// ↓	REPEATABLE READ	    不可	        不可	            可
        ///  高	SERIALIZABLE	    不可	        不可	            不可 
        #endregion
        /// 
        /// 
        /// </summary>
        /// <param name="option"></param>
        #region setTransactionIsolationLevel
        protected void setTransactionIsolationLevel(string option)
        {
            //トランザクション分離レベル設定
            executeNonQueryOC("SET TRANSACTION ISOLATION LEVEL " + option);
        }
        #endregion


        ///====================================================================
        ///
        ///                             レコードセット処理
        ///                         
        ///====================================================================


        /// <summary>
        /// getRsString
        /// レコードセットから結果をストリングで取得する
        /// </summary>
        /// <param name="cReader">cReader</param>
        /// <param name="idx">idx</param>
        /// <returns></returns>
        #region getRsString
        protected string getRsString(SqlDataReader cReader, int idx)
        {
            try
            {
                if (cReader[idx] != null)
                {
                    return cReader.GetString(idx);
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        #endregion

        /// <summary>
        /// getRsString
        /// レコードセットから結果をイントで取得する
        /// </summary>
        /// <param name="cReader">cReader</param>
        /// <param name="idx">idx</param>
        /// <returns></returns>
        #region getRsInt32
        protected Int32 getRsInt32(SqlDataReader cReader, int idx)
        {
            try
            {
                if (cReader[idx] != null)
                {
                    return cReader.GetInt32(idx);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// getRsString
        /// レコードセットから結果をイントで取得する
        /// </summary>
        /// <param name="cReader">cReader</param>
        /// <param name="idx">idx</param>
        /// <returns></returns>
        #region getRsInt64
        protected Int64 getRsInt64(SqlDataReader cReader, int idx)
        {
            try
            {
                if (cReader[idx] != null)
                {
                    return cReader.GetInt64(idx);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// getRsDateTime
        /// レコードセットから結果をデイトで取得する
        /// </summary>
        /// <param name="cReader">cReader</param>
        /// <param name="idx">idx</param>
        /// <returns></returns>
        #region getRsDateTime
        protected DateTime getRsDateTime(SqlDataReader cReader, int idx)
        {
            try
            {
                if (cReader[idx] != null)
                {
                    return cReader.GetDateTime(idx);
                }
                else
                {
                    return new DateTime(0);
                }
            }
            catch
            {
                return new DateTime(0);
            }
        }
        protected string getRsDateTimeStr(SqlDataReader cReader, int idx)
        {
            try
            {
                if (cReader[idx] != null)
                {
                    return cReader.GetDateTime(idx).ToString();
                }
                else
                {
                    return new DateTime(0).ToString();
                }
            }
            catch
            {
                return new DateTime(0).ToString();
            }
        }
        #endregion

        /// <summary>
        /// getRsString
        /// レコードセットから結果をイントで取得する
        /// </summary>
        /// <param name="cReader">cReader</param>
        /// <param name="idx">idx</param>
        /// <returns></returns>
        #region getRsBool
        protected bool getRsBool(SqlDataReader cReader, int idx)
        {
            try
            {
                if (cReader[idx] != null)
                {
                    return cReader.GetInt32(idx) == 1;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}