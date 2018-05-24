using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class Lijn
    {
        private int id;
        private Remise remise;
        private int nummer;
        private bool conducteurRijdtMee;
        private List<Tram> trams;

        public int ID { get { return id; } set { id = value; } }
        public Remise Remise { get { return remise; } set { remise = value; } }
        public int Nummer { get { return nummer; } set { nummer = value; } }
        public bool ConducteurRijdtMee { get { return conducteurRijdtMee; } set { conducteurRijdtMee = value; } }
        public List<Tram> Trams { get { return trams; } set { trams = value; } }

        public Lijn(int id, Remise remise, int nummer, bool conducteurRijdtMee, List<Tram> trams)
        {
            this.id = id;
            this.remise = remise;
            this.nummer = nummer;
            this.conducteurRijdtMee = conducteurRijdtMee;
            this.trams = trams;
        }
    }
}