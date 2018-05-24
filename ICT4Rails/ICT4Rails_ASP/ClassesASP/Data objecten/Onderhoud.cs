using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class Onderhoud
    {
        private int id;
        private int adid; //Active directory account ID
        private Tram tram;
        private DateTime tijdstip;
        private DateTime beschikbaarDatum;
        private TypeOnderhoud typeOnderhoud;

        public int ID { get { return id; } set { id = value; } }
        public int AdID { get { return adid; } set { adid = value; } }
        public Tram Tram { get { return tram; } set { tram = value; } }
        public DateTime Tijdstip { get { return tijdstip; } set { tijdstip = value; } }
        public DateTime BeschikbaarDatum { get { return beschikbaarDatum; } set { beschikbaarDatum = value; } }
        public TypeOnderhoud TypeOnderhoud
        {
            get { return typeOnderhoud; }
            set { typeOnderhoud = value; }
        }

        public Onderhoud(int adID, Tram tram, DateTime tijdstip, DateTime beschikbaarDatum, TypeOnderhoud typeOnderhoud)
        {
            this.adid = adID;
            this.tram = tram;
            this.tijdstip = tijdstip;
            this.beschikbaarDatum = beschikbaarDatum;
            this.typeOnderhoud = typeOnderhoud;
        }

        public Onderhoud(int id, int adid, Tram tram, DateTime tijdstip, DateTime beschikbaarDatum, TypeOnderhoud typeOnderhoud)
        {
            this.id = id;
            this.adid = adid;
            this.tram = tram;
            this.tijdstip = tijdstip;
            this.beschikbaarDatum = beschikbaarDatum;
            this.typeOnderhoud = typeOnderhoud;
        }
    }
}