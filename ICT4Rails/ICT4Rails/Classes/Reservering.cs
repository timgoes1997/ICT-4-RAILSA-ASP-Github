using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICT4Rails.Scripts;

namespace ICT4Rails.Scripts
{
    public class Reservering
    {
        // Fields
        private int id;
        private Tram tram;
        private Sector sector;

        // Properties
        public int ID { get { return id; } set { id = value; } }
        public Tram Tram { get { return tram; } set { tram = value; } }
        public Sector Sector { get { return sector; } set { sector = value; } }

        // Constructors
        public Reservering(Tram tram, Sector sector)
        {
            this.tram = tram;
            this.sector = sector;
        }

        public Reservering(int id, Tram tram, Sector sector)
        {
            this.id = id;
            this.tram = tram;
            this.sector = sector;
        }

        // Methods
        public override string ToString()
        {
            string info = tram.TramNummer + " " + sector.Id;
            return info;
        }

    }
}
