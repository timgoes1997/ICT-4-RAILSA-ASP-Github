using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class OnderhoudToevoegException : Exception
    {
        public OnderhoudToevoegException()
        {
        }

        public OnderhoudToevoegException(string message)
        : base(message)
        {
        }

        public OnderhoudToevoegException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}