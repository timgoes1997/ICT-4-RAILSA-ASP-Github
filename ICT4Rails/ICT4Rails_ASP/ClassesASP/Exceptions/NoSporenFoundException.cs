using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class NoSporenFoundException : Exception
    {
        public NoSporenFoundException()
        {
        }

        public NoSporenFoundException(string message)
        : base(message)
        {
        }

        public NoSporenFoundException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}