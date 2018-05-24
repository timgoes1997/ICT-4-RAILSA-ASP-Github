using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class TramVerplaatsException : Exception
    {
        public TramVerplaatsException()
        {
        }

        public TramVerplaatsException(string message)
        : base(message)
        {
        }

        public TramVerplaatsException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}