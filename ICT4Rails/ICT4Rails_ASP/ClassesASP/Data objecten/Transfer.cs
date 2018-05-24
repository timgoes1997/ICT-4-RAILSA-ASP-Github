using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class Transfer
    {
        private Remise remiseVan;
        private Remise remiseNaar;
        private int aantal;

        public Remise RemiseVan { get { return remiseVan; } set { remiseVan = value; } }
        public Remise RemiseNaar { get { return RemiseNaar; } set { RemiseNaar = value; } }
        public int Aantal { get { return aantal; } set { aantal = value; } }

        public Transfer(Remise remiseVan, Remise remiseNaar, int aantal)
        {
            this.remiseVan = remiseVan;
            this.remiseNaar = remiseNaar;
            this.aantal = aantal;
        }
    }
}