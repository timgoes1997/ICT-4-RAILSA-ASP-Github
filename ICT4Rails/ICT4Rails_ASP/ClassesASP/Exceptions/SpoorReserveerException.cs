using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class SpoorReserveerException : Exception
    {
        public SpoorReserveerException()
        {
        }

        public SpoorReserveerException(string message)
        : base(message)
        {
        }

        public SpoorReserveerException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}