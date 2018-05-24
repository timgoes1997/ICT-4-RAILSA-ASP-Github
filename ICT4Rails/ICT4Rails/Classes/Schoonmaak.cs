using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    public class Schoonmaak : Onderhoud
    {
        private SchoonmaakType schoonmaakType;

        public SchoonmaakType SchoonmaakType { get { return schoonmaakType; } set { value = schoonmaakType; } }

        public Schoonmaak(Tram tram, string opmerking, DateTime datum, bool bevestigd, SchoonmaakType schoonmaakType) 
            : base(tram, opmerking, datum, bevestigd)
        {
            this.Tram = tram;
            this.Opmerking = opmerking;
            this.Datum = datum;
            this.Bevestigd = bevestigd;
            this.schoonmaakType = schoonmaakType;
        }

        public Schoonmaak(int id, Tram tram, string opmerking, DateTime datum, bool bevestigd, SchoonmaakType schoonmaakType)
            : base(id, tram, opmerking, datum, bevestigd)
        {
            /*
            this.ID = id;
            this.Tram = tram;
            this.Opmerking = opmerking;
            this.Datum = datum;
            this.Bevestigd = bevestigd;*/
            this.schoonmaakType = schoonmaakType;
        }

    }

    public enum SchoonmaakType { klein = 1, groot = 2}
}
