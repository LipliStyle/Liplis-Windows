//=======================================================================
//  ClassName : BaseEntity
//  �T�v      : �x�[�X�G���e�B�e�B
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
        /// �v���p�e�B
        protected string stConnectionString = string.Empty;

        ///=============================
        /// �g�����U�N�V�����������x��
        #region �g�����U�N�V�����������x��
        protected const string READ_UNCOMMITTED = "READ UNCOMMITTED";
        protected const string READ_COMMITTED   = "READ COMMITTED";
        protected const string REPEATABLE_READ  = "REPEATABLE READ";
        protected const string SERIALIZABLE     = "SERIALIZABLE";
        #endregion

        /// <summary>
        /// �f�B�X�|�[�Y
        /// </summary>
        #region dispose
        public virtual void dispose()
        {

        }
        #endregion

        /// <summary>
        /// �R�l�N�V�����X�g�����O��ݒ肷��
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
        /// �R�l�N�V�����X�g�����O��ݒ肷��
        /// </summary>
        #region setConnectionString
        public void setConnectionString(string stConnectionString)
        {
            this.stConnectionString = stConnectionString;
        }
        #endregion


        /// <summary>
        /// �R�l�N�V�����X�g�����O��ݒ肷��
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
        /// �f�[�^�x�[�X�R�l�N�V�������J��
        /// </summary>
        /// <returns>�쐬�����R�l�N�V����</returns>
        #region openDb
        protected SqlConnection openDb()
        {
            SqlConnection cSqlConnection = (new SqlConnection(stConnectionString));
            cSqlConnection.Open();

            return cSqlConnection;
        }
        #endregion

        /// <summary>
        /// �f�[�^�x�[�X�R�l�N�V�������J��
        /// </summary>
        /// <returns>�쐬�����R�l�N�V����</returns>
        #region openDb
        protected SqlConnection openDb(string conStr)
        {
            SqlConnection cSqlConnection = (new SqlConnection(conStr));
            cSqlConnection.Open();

            return cSqlConnection;
        }
        #endregion

        /// <summary>
        /// �쐬�����R�l�N�V������j������
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
        /// �L�[���݃`�F�b�N
        /// </summary>
        #region keyExist
        public bool keyExist(SqlConnection con, string sqlStr)
        {
            bool result;
            try
            {
                //�J�E���^�[�Q�b�g
                // cSqlConnection ���� SqlCommand �̃C���X�^���X�𐶐�����
                SqlCommand hCommand = con.CreateCommand();

                // ���s���� SQL �R�}���h��ݒ肷��
                hCommand.CommandText = sqlStr;

                // �w�肵�� SQL �R�}���h�����s���� SqlDataReader ���\�z����
                SqlDataReader cReader = hCommand.ExecuteReader();

                //���ʂ̎擾
                result = cReader.HasRows;

                // cReader �����
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
        /// �L�[���݃`�F�b�N
        /// </summary>
        #region keyExist
        public bool keyExist(SqlCommand hCommand ,string sqlStr)
        {
            bool result;
            try
            {
                // ���s���� SQL �R�}���h��ݒ肷��
                hCommand.CommandText = sqlStr;

                // �w�肵�� SQL �R�}���h�����s���� SqlDataReader ���\�z����
                SqlDataReader cReader = hCommand.ExecuteReader();

                //���ʂ̎擾
                result = cReader.HasRows;

                // cReader �����
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
        /// �L�[���݃`�F�b�N
        /// </summary>
        #region keyExist
        public bool keyExist(string sqlStr)
        {
            bool result;
            try
            {
                // SqlConnection �̐V�����C���X�^���X�𐶐�
                using (SqlConnection cSqlConnection = openDb())
                {
                    //�J�E���^�[�Q�b�g
                    // cSqlConnection ���� SqlCommand �̃C���X�^���X�𐶐�����
                    using (SqlCommand hCommand = cSqlConnection.CreateCommand())
                    {
                        // ���s���� SQL �R�}���h��ݒ肷��
                        hCommand.CommandText = sqlStr;

                        // �w�肵�� SQL �R�}���h�����s���� SqlDataReader ���\�z����
                        using (SqlDataReader cReader = hCommand.ExecuteReader())
                        {
                            //���ʂ̎擾
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
        /// SQL�������s����(�m���N�G��)
        /// CREATE�AUPDATE������
        #region executeNonQuery
        protected bool executeNonQuery(SqlCommand hCommand, string pSqlStr)
        {
            try
            {
                // ���s���� SQL �R�}���h��ݒ肷��
                hCommand.CommandText = pSqlStr;

                // SQL �R�}���h�����s���A�e�����󂯂��s��Ԃ�
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
                // SQL �R�}���h�����s���A�e�����󂯂��s��Ԃ�
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
        /// SQL�������s����(�m���N�G��)
        /// CREATE�AUPDATE������
        #region executeNonQuery
        protected bool executeNonQuery(SqlConnection cSqlConnection, string pSqlStr)
        {
            try
            {
                // cSqlConnection ���� SqlCommand �̃C���X�^���X�𐶐�����
                using (SqlCommand hCommand = cSqlConnection.CreateCommand())
                {
                    // ���s���� SQL �R�}���h��ݒ肷��
                    hCommand.CommandText = pSqlStr;

                    // SQL �R�}���h�����s���A�e�����󂯂��s��Ԃ�
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
        /// SQL�������s����(�m���N�G��)
        /// CREATE�AUPDATE������
        #region executeNonQuery
        protected bool executeNonQueryOC(string pSqlStr)
        {

            try
            {
                // SqlConnection �̐V�����C���X�^���X�𐶐�
                using (SqlConnection cSqlConnection = openDb())
                {
                    // cSqlConnection ���� SqlCommand �̃C���X�^���X�𐶐�����
                    using (SqlCommand hCommand = cSqlConnection.CreateCommand())
                    {
                        // ���s���� SQL �R�}���h��ݒ肷��
                        hCommand.CommandText = pSqlStr;

                        // SQL �R�}���h�����s���A�e�����󂯂��s��Ԃ�
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
        /// �g�����U�N�V�����������x����ݒ肷��B
        #region ����
        /// �������x�����w�肷��ƁA�ʂ̕������x����ݒ肵�Ȃ�����A�Z�b�V�����̏I�����܂ŕύX����Ȃ��B
        /// ���̃Z�b�V�����̕������x���́ASELECT �X�e�[�g�����g�Ńe�[�u�����x���̃��b�N �q���g���w�肷�邱�Ƃɂ���Ė��������邱�Ƃ��ł��邪�A
        /// �e�[�u�����x���̃��b�N �q���g���w�肵�Ă��A�Z�b�V�������̂ق��̃X�e�[�g�����g�ɂ͉e����^���Ȃ��B
        /// 
        /// �������x��	�������x��	�_�[�e�B���[�h	�����s�\�ǂݎ��	�t�@���g��
        ///  ��	READ UNCOMMITTED	��	            ��	                ��
        /// ��	READ COMMITTED	    �s��	        ��	                ��
        /// ��	REPEATABLE READ	    �s��	        �s��	            ��
        ///  ��	SERIALIZABLE	    �s��	        �s��	            �s�� 
        #endregion
        /// 
        /// 
        /// </summary>
        /// <param name="option"></param>
        #region setTransactionIsolationLevel
        protected void setTransactionIsolationLevel(string option)
        {
            //�g�����U�N�V�����������x���ݒ�
            executeNonQueryOC("SET TRANSACTION ISOLATION LEVEL " + option);
        }
        #endregion


        ///====================================================================
        ///
        ///                             ���R�[�h�Z�b�g����
        ///                         
        ///====================================================================


        /// <summary>
        /// getRsString
        /// ���R�[�h�Z�b�g���猋�ʂ��X�g�����O�Ŏ擾����
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
        /// ���R�[�h�Z�b�g���猋�ʂ��C���g�Ŏ擾����
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
        /// ���R�[�h�Z�b�g���猋�ʂ��C���g�Ŏ擾����
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
        /// ���R�[�h�Z�b�g���猋�ʂ��f�C�g�Ŏ擾����
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
        /// ���R�[�h�Z�b�g���猋�ʂ��C���g�Ŏ擾����
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