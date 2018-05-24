using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class TramVerwijderException : Exception
    {
        public TramVerwijderException()
        {
        }

        public TramVerwijderException(string message)
        : base(message)
        {
        }

        public TramVerwijderException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}