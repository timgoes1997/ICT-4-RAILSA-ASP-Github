using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class ToggleSectorException : Exception
    {
        public ToggleSectorException()
        {
        }

        public ToggleSectorException(string message)
        : base(message)
        {
        }

        public ToggleSectorException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}