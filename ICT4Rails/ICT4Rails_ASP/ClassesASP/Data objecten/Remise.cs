using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class Remise
    {
        private int id;
        private string naam;

        //Moeten nog worden geïmplementeerd.
        private int kleineSchoonmaakbeurtenPerDag;
        private int groteSchoonmaakbeurtenPerDag;
        private int kleineOnderhoudsbeurtenPerDag;
        private int middelOnderhoudsbeurtenPerDag;
        private int groteOnderhoudsbeurtenPerDag;

        public int KleineSchoonmaakbeurtenPerDag
        {
            get { return kleineSchoonmaakbeurtenPerDag; }
            set { kleineSchoonmaakbeurtenPerDag = value; }
        }

        public int GroteSchoonmaakbeurtenPerDag
        {
            get { return groteSchoonmaakbeurtenPerDag; }
            set { groteSchoonmaakbeurtenPerDag = value; }
        }

        public int KleineOnderhoudsbeurtenPerDag
        {
            get { return kleineOnderhoudsbeurtenPerDag; }
            set { kleineOnderhoudsbeurtenPerDag = value; }
        }

        public int MiddelOnderhoudsbeurtenPerDag
        {
            get { return middelOnderhoudsbeurtenPerDag; }
            set { middelOnderhoudsbeurtenPerDag = value; }
        }

        public int GroteOnderhoudsbeurtenPerDag
        {
            get { return groteOnderhoudsbeurtenPerDag; }
            set { groteOnderhoudsbeurtenPerDag = value; }
        }

        public int ID { get { return id; } set { id = value; } }
        public string Naam { get { return naam; } set { naam = value; } }

        public Remise(int id, string naam, int kleineSchoonmaakbeurtenPerDag, int groteSchoonmaakbeurtenPerDag, int kleineOnderhoudsbeurtenPerDag, int middelOnderhoudsbeurtenPerDag, int groteOnderhoudsbeurtenPerDag)
        {
            this.id = id;
            this.naam = naam;
            this.kleineSchoonmaakbeurtenPerDag = kleineSchoonmaakbeurtenPerDag;
            this.groteSchoonmaakbeurtenPerDag = groteSchoonmaakbeurtenPerDag;
            this.kleineOnderhoudsbeurtenPerDag = kleineOnderhoudsbeurtenPerDag;
            this.middelOnderhoudsbeurtenPerDag = middelOnderhoudsbeurtenPerDag;
            this.groteOnderhoudsbeurtenPerDag = groteOnderhoudsbeurtenPerDag;
        }
        public Remise(int id, string naam)
        {
            this.id = id;
            this.naam = naam;
        }
    }
}