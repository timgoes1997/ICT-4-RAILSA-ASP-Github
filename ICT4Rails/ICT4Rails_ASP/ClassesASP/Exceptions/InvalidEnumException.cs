using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class InvalidEnumException : Exception
    {
        public InvalidEnumException()
        {
        }

        public InvalidEnumException(string message)
        : base(message)
        {
        }

        public InvalidEnumException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}