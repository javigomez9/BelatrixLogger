using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BelatrixLog
{
    public class SqlAdapter
    {
        private static string conectionString;
        
        private static string getConnectionString()
        {
            if (conectionString == null || conectionString.Equals(string.Empty))
            {
                return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
            }
            else
            {
                return conectionString;
            }
        }

        public static int sqlExecuteNonQuery(string sqlScript)
        {
            System.Data.SqlClient.SqlConnection connection = null;
            try
            {
                int rowsAffected;
                connection = new System.Data.SqlClient.SqlConnection(getConnectionString());
                connection.Open();
                System.Data.SqlClient.SqlCommand command
                    = new System.Data.SqlClient.SqlCommand(sqlScript); 
                command.Connection = connection;
                rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Sql Execute Exception", sqlScript, ex);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

        }

        
    }
}
