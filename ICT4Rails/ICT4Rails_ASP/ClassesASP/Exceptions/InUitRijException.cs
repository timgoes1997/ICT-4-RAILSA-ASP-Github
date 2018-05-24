using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class InUitRijException : Exception
    {
        public InUitRijException()
        {
        }

        public InUitRijException(string message)
        : base(message)
        {
        }

        public InUitRijException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}