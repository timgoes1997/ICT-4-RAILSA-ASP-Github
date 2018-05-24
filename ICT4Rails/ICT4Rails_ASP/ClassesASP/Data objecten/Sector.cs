using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class Sector
    {
        private int id;
        private Tram tram;
        private int nummer;
        private bool beschikbaar;     
        private bool geblokkeerd;
        private int spoorID;

        public int ID { get { return id; } set { id = value; } }
        public Tram Tram { get { return tram; } set { tram = value; } }
        public int SpoorID { get { return spoorID; } set { spoorID = value; } }
        public int Nummer { get { return nummer; } set { nummer = value; } }
        public bool Beschikbaar { get { return beschikbaar; } set { beschikbaar = value; } }
        public bool Geblokkeerd { get { return geblokkeerd; } set { geblokkeerd = value; } }

        public Sector(int id, Tram tram, int spoorID, int nummer, bool beschikbaar, bool geblokkeerd)
        {
            this.id = id;
            this.tram = tram;
            this.spoorID = spoorID;
            this.nummer = nummer;
            this.beschikbaar = beschikbaar;
            this.geblokkeerd = geblokkeerd;
            
        }

        public void PlaatsTram(Tram tram)
        {
            this.tram = tram;
            tram.Beschikbaar = true;
            beschikbaar = false;
        }

        public void VerwijderTram()
        {
            this.tram = null;
            beschikbaar = true;
        }
    }
}