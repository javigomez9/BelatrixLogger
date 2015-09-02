using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelatrixLog
{
    public class CommandWriter
    {
        public void LogToCommandWindow(string pLogMessage, MessageType pMessagetype, bool LogError, bool LogWarning, bool LogInformation)
        {
            if (MessageType.Error == pMessagetype && LogError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (MessageType.Warning == pMessagetype && LogWarning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (MessageType.Information == pMessagetype && LogInformation)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(DateTime.Now.ToShortDateString() + " - " + pLogMessage);

        }
    }
}
