using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelatrixLog
{
    public class FileWriter
    {

        private static string filePad;
        
        private static string getFilePad()
        {
            
            filePad = System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"];
            if (filePad.Equals(string.Empty))
                filePad = AppDomain.CurrentDomain.BaseDirectory; 
            return filePad;
        }


        public void WriteMessageToTXT(string logMessage)
        {
            string fileCompletePad = getFilePad() + "\\LogFile_" + DateTime.Now.ToString("yyMMdd") + ".txt";
            
            if (!System.IO.File.Exists(fileCompletePad))
            {
                System.IO.File.WriteAllText(fileCompletePad, logMessage + Environment.NewLine);
            }
            else
            {
                System.IO.File.AppendAllText(fileCompletePad, logMessage + Environment.NewLine);
            }
        }  
      
        public void LogMessageOnFile(string pLogMessage, MessageType pMessageType, bool LogError, bool LogWarning, bool LogInformation)
        {
            string logMessageToFile = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " - ";
            if (MessageType.Error == pMessageType && LogError)
            {
                logMessageToFile += "ERROR: ";
            }
            else if (MessageType.Warning == pMessageType && LogWarning)
            {
                logMessageToFile += "WARNING: ";
            }
            else if (MessageType.Information == pMessageType && LogInformation)
            {
                logMessageToFile += "MESSAGE: ";
            }
            logMessageToFile += pLogMessage;

            WriteMessageToTXT(logMessageToFile);
        }
    }
}
