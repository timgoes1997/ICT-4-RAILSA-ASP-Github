using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class TramStatusException : Exception
    {
        public TramStatusException()
        {
        }

        public TramStatusException(string message)
        : base(message)
        {
        }

        public TramStatusException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}