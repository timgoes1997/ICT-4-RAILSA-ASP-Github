using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class Tram
    {
        private int id;
        private Remise remise;
        private TramType tramType;
        private DateTime vertrektijd;
        private int tramNummer;
        private int lengte;
        private string opmerking;
        private bool vervuild;
        private bool defect;
        private bool conducteurGeschikt;
        private bool beschikbaar;

        public int ID { get { return id; } set { id = value; } }
        public Remise Remise { get { return remise; } set { remise = value; } }
        public TramType TramType { get { return tramType; } set { tramType = value; } }
        public DateTime Vertrektijd { get { return vertrektijd; } set { vertrektijd = value; } }
        public int TramNummer { get { return tramNummer; } set { tramNummer = value; } }
        public int Lengte { get { return lengte; } set { lengte = value; } }
        public string Opmerking { get { return opmerking; } set { opmerking = value; } }
        public bool Vervuild { get { return vervuild; } set { vervuild = value; } }
        public bool Defect { get { return defect; } set { defect = value; } }
        public bool ConducteurGeschikt { get { return conducteurGeschikt; } set { conducteurGeschikt = value; } }
        public bool Beschikbaar { get { return beschikbaar; } set { beschikbaar = value; } }

        public Tram(int id, Remise remise, TramType tramType, DateTime vertrektijd, int tramNummer, int lengte, string opmerking, bool vervuild, bool defect, bool conducteurGeschikt, bool beschikbaar)
        {
            this.id = id;
            this.remise = remise;
            this.tramType = tramType;
            this.tramNummer = tramNummer;
            this.lengte = lengte;
            this.opmerking = opmerking;
            this.vervuild = vervuild;
            this.defect = defect;
            this.conducteurGeschikt = conducteurGeschikt;
            this.beschikbaar = beschikbaar;
            this.vertrektijd = vertrektijd;
        }

        public Tram(Remise remise, TramType tramType, DateTime vertrektijd, int tramNummer, int lengte, string opmerking, bool vervuild, bool defect, bool conducteurGeschikt, bool beschikbaar)
        {
            this.remise = remise;
            this.tramType = tramType;
            this.vertrektijd = vertrektijd;
            this.tramNummer = tramNummer;
            this.lengte = lengte;
            this.opmerking = opmerking;
            this.vervuild = vervuild;
            this.beschikbaar = beschikbaar;
            this.conducteurGeschikt = conducteurGeschikt;
            this.defect = defect;
        }
    }
}