using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelatrixLog
{
    public class DataAccessException : Exception
    {
        private string message;
        private string exception;

        public DataAccessException(string ex, string pMessage, Exception InnerEx) : base(ex, InnerEx )
        {
            message = pMessage;
        }
    }
}
