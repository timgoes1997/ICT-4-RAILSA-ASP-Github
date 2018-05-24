using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    public abstract class Onderhoud
    {
        private int id;
        private Tram tram;
        private string opmerking;
        private DateTime datum;
        private bool bevestigd;

        public int ID { get { return id; } set { value = id; } }
        public Tram Tram { get { return tram; } set { value = tram; } }
        public string Opmerking { get { return opmerking; } set { value = opmerking; } }
        public DateTime Datum { get { return datum; } set { value = datum; } }
        public bool Bevestigd { get { return bevestigd; } set { value = bevestigd; } }

        public Onderhoud(Tram tram, string opmerking, DateTime datum, bool bevestigd)
        {
            this.tram = tram;
            this.opmerking = opmerking;
            this.datum = datum;
            this.bevestigd = bevestigd;
        }

        public Onderhoud(int id, Tram tram, string opmerking, DateTime datum, bool bevestigd)
        {
            
            this.id = id;
            this.tram = tram;
            this.opmerking = opmerking;
            this.datum = datum;
            this.bevestigd = bevestigd;
        }
    }
}
