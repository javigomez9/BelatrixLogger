using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BelatrixLog;

namespace MessageTest
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        public void JobLogger_LogInformationMessages()
        {
            new JobLogger(true, true, true, true, true, true);            
            JobLogger.LogMessage("Hola mundo", MessageType.Information );
        }

        [TestMethod]
        public void JobLogger_LogInformationMessages_Sininicializar()
        {            
            JobLogger.LogMessage("Hola mundo", MessageType.Information);
        }

        [TestMethod]
        public void JobLogger_LogOnlyFile()
        {
            new JobLogger(true, false, false, true, true, true);
            JobLogger.LogMessage("Log only message", MessageType.Information);
        }
        [TestMethod]
        public void JobLogger_LogOnlyConsole()
        {
            new JobLogger(false, true, false, true, true, true);
            JobLogger.LogMessage("Log only Warning", MessageType.Warning);
        }
        [TestMethod]
        public void JobLogger_LogOnlyDataBase()
        {
            new JobLogger(false, false, true, true, true, true);
            JobLogger.LogMessage("Log only Error", MessageType.Error);
        }

        //[TestMethod]
        //[ExpectedException(typeof(Exception))]
        //public void JobLogger_WithOutType()
        //{
        //    new JobLogger(true, true, true, true, true, true);
        //    JobLogger.LogMessage("NO LOG", MessageType.);
        //}

        [TestMethod]        
        public void JobLogger_LogNotEnable()
        {
            new JobLogger(false, false, false, true, true, true);
            JobLogger.LogMessage("No escribir este log", MessageType.Error);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void JobLogger_NoMessage()
        {
            new JobLogger(true, true, true, true, true, true);
            JobLogger.LogMessage(string.Empty, MessageType.Information);
        }

        [TestMethod]
        public void JobLogger_NotLogMessage_InformationMessage()
        {
            new JobLogger(true, true, true, false, true, true);
            JobLogger.LogMessage("Information Message", MessageType.Information);
        }


    }
}
