using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_6.Utils
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;
        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.strConnection);
        }
        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }
        public DataTable ExecuteSPQuery(string sp)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException)
            {
                dt = null;
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return dt;
        }
        public int ExecuteSPInsert(string sp)
        {
            int result = 0;
            try
            {
                _connection.Open();
                SqlCommand cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                result = cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                result = 0;
            }
            finally
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return result;
        }
        public SqlConnection GetConnection()
        {
            return _connection;
        }
    }
}
