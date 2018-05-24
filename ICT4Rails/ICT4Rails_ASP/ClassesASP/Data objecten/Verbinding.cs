using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class Verbinding
    {
        private int id;
        private Sector van;
        private Sector naar;

        public int ID { get { return id; } set { id = value; } }
        public Sector Van { get { return van; } set { van = value; } }
        public Sector Naar { get { return naar; } set { naar = value; } }

        public Verbinding(int id, Sector van, Sector naar)
        {
            this.id = id;
            this.van = van;
            this.naar = naar;
        }
    }
}