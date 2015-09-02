using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelatrixLog
{
    public class JobLogger
    {
        #region AtributosAndProperties
        private static bool _logToFile;

        public static bool LogToFile
        {
            get { return JobLogger._logToFile; }
            set { JobLogger._logToFile = value; }
        }
        private static bool _logToConsole;

        public static bool LogToConsole
        {
            get { return JobLogger._logToConsole; }
            set { JobLogger._logToConsole = value; }
        }
        private static bool _logInformation;

        public static bool LogInformation
        {
            get { return JobLogger._logInformation; }
            set { JobLogger._logInformation = value; }
        }
        private static bool _logWarning;

        public static bool LogWarning
        {
            get { return JobLogger._logWarning; }
            set { JobLogger._logWarning = value; }
        }
        private static bool _logError;

        public static bool LogError
        {
            get { return JobLogger._logError; }
            set { JobLogger._logError = value; }
        }
        private static bool _logToDatabase;

        public static bool LogToDatabase
        {
            get { return JobLogger._logToDatabase; }
            set { JobLogger._logToDatabase = value; }
        }
        private static bool _initialized;

        public static bool Initialized
        {
            get { return JobLogger._initialized; }
            set { JobLogger._initialized = value; }
        }

        #endregion

        /// <summary>
        /// Constructor, Inicializa la configuracion de logueo
        /// </summary>
        /// <param name="logToFile">Si se graba en archivo txt</param>
        /// <param name="logToConsole">Si se muestra por consola</param>
        /// <param name="logToDatabase">Si se graba en la base de datos</param>
        /// <param name="logInformation">Indica si esta habilitado loguear mensajes de Informacion</param>
        /// <param name="logWarning">Indica si esta habilitado loguear mensajes de Advertencia</param>
        /// <param name="logError">Indica si esta habilitado loguear mensajes de Error</param>
        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase, bool logInformation, bool logWarning, bool logError)
        {
            _logError = logError;
            _logInformation = logInformation;
            _logWarning = logWarning;
            _logToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
            _initialized = true;
        }

        /// <summary>
        /// Loguea un Mensaje enviado por Base de datos, Consola y archivo txt dependiendo de la configuracion
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="pMessagetype"></param>
        public static void LogMessage(string logMessage, MessageType pMessagetype) 
        {
            if (!_initialized)
                InitializeValuesFromConfig();

            logMessage.Trim();
            if (logMessage == null || logMessage.Length == 0)
            {
                throw new Exception("No Message to Log");                
            }
            if (!_logToConsole && !_logToFile && !_logToDatabase)
            {                
                return;
            }
            if (!_logError && !LogInformation && !_logWarning) 
            {
                throw new Exception("There is no message type configured to log.");
            }

            // Javier Gomez 14/06/15  - Si se puede loguear algo
            if ((MessageType.Error == pMessagetype && LogError) 
                || (MessageType.Warning == pMessagetype && LogWarning)
                || (MessageType.Information == pMessagetype && LogInformation))
            {
                if (LogToDatabase)
                {
                    DataBaseLogger.logToDataBase(logMessage, pMessagetype);
                }

                if (LogToFile )
                {
                    new FileWriter().LogMessageOnFile(logMessage, pMessagetype, LogError, LogWarning, LogInformation);
                }

                if (LogToConsole )
                {
                   new CommandWriter().LogToCommandWindow(logMessage, pMessagetype, LogError, LogWarning, LogInformation);
                }
            }
        }

        #region PrivateMethots
        /// <summary>
        /// Configura los valores de logueo segun el archivo de configuracion
        /// </summary>
        private static void InitializeValuesFromConfig()
        {
            LogError = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["logError"].ToString());
            LogInformation = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["logMessage"].ToString());
            LogWarning = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["logWarning"].ToString());
            LogToDatabase = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["logToDatabase"].ToString());
            LogToFile = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["logToFile"].ToString());
            LogToConsole = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["logToConsole"].ToString());
            Initialized = true;
        }

        

        

        #endregion
    }

    
}
