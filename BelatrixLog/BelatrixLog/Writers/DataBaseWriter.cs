using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelatrixLog
{
    public class DataBaseLogger : SqlAdapter
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLogMessage"></param>
        /// <param name="pMessagetype"></param>
        public static void logToDataBase(string pLogMessage, MessageType pMessagetype)
        {
            if (!ExistLogTable())
                CreateLogTable();
            SqlAdapter.sqlExecuteNonQuery("Insert into BelaLog Values('" + pLogMessage + "', " + ((int)pMessagetype).ToString() + ", DEFAULT )");
        }

        /// <summary>
        /// Crea la tabla de Logueo
        /// </summary>
        private static void CreateLogTable()
        {
            string scriptTableExist = "CREATE TABLE [dbo].[BelaLog] (" + Environment.NewLine +
                "[Id] INT IDENTITY (1, 1) NOT NULL," + Environment.NewLine +
                "[Message] VARCHAR (MAX) NOT NULL," + Environment.NewLine +
                "[Type]    TINYINT       NOT NULL," + Environment.NewLine +
                "[Date]    DATETIME      DEFAULT (getdate()) NULL," + Environment.NewLine +
                "PRIMARY KEY CLUSTERED ([Id] ASC)" + Environment.NewLine +
                ");";

            SqlAdapter.sqlExecuteNonQuery(scriptTableExist);
        }

        /// <summary>
        /// Verifica si existe la tabla de logueo
        /// </summary>
        /// <returns></returns>
        private static bool ExistLogTable()
        {
            string scriptTableExist = "SELECT * FROM BelaLog";
            try
            {
                SqlAdapter.sqlExecuteNonQuery(scriptTableExist);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
