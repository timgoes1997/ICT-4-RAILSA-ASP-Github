using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class Reservering
    {
        // Fields
        private int id;
        private Tram tram;
        private Spoor spoor;
        // Properties
        public int ID { get { return id; } set { id = value; } }
        public Tram Tram { get { return tram; } set { tram = value; } }
        public Spoor Spoor { get { return spoor; } set { spoor = value; } }

        // Constructors
        public Reservering(Tram tram, Spoor spoor)
        {
            this.tram = tram;
            this.spoor = spoor;
        }

        public Reservering(int id, Tram tram, Spoor spoor)
        {
            this.id = id;
            this.tram = tram;
            this.spoor = spoor;
        }

        // Methods
        public override string ToString()
        {
          //  string info = tram.TramNummer + " " + sector.Id;
          //  return info;
          return "";
        }
    }
}