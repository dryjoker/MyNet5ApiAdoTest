using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyNet5ApiAdoTest.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyNet5ApiAdoTest.Utility
{
    public static class MSSQLHelper
    {
        public static readonly string connectionString = AppConfigurationService.Configuration.GetConnectionString("NewsDb");

        public static int ExecuteNonQuery(string strSQL, Hashtable htParams = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandText = strSQL;
                sqlCommand.CommandType = CommandType.Text;
                PrepareParams(htParams, sqlCommand);
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public static SqlDataReader GetSqlDataReader(string strSQL, Hashtable htParams = null)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand sqlCommand = conn.CreateCommand();
            sqlCommand.CommandText = strSQL;
            sqlCommand.CommandType = CommandType.Text;
            PrepareParams(htParams, sqlCommand);
            return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static object ExecuteScalar(string strSQL, Hashtable htParams = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sqlCommand = conn.CreateCommand();
                sqlCommand.CommandText = strSQL;
                sqlCommand.CommandType = CommandType.Text;
                PrepareParams(htParams, sqlCommand);
                return sqlCommand.ExecuteScalar();
            }
        }

        private static void PrepareParams(Hashtable htParams, SqlCommand sqlCommand)
        {
            if(htParams != null)
            {
                foreach (DictionaryEntry entry in htParams)
                {
                    sqlCommand.Parameters.AddWithValue(entry.Key.ToString(), entry.Value);
                }
            }            
        }
    }
}
