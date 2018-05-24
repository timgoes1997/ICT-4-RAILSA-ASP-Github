using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class TramToevoegException : Exception
    {
        public TramToevoegException()
        {
        }

        public TramToevoegException(string message)
        : base(message)
        {
        }

        public TramToevoegException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}