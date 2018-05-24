using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    public class Reparatie : Onderhoud
    {
        private ReparatieType reparatieType;

        public ReparatieType ReparatieType { get { return reparatieType; } set { value = reparatieType; } }

        public Reparatie(ReparatieType reparatieType, Tram tram, string opmerking, DateTime datum, bool bevestigd) 
            : base(tram, opmerking, datum, bevestigd)
        {
            this.reparatieType = reparatieType;
            this.Tram = tram;
            this.Opmerking = opmerking;
            this.Datum = datum;
            this.Bevestigd = bevestigd;
        }

    }

    public enum ReparatieType { klein = 0, middel = 1, groot = 2};
}
