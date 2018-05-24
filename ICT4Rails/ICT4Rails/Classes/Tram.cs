using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    public class Tram
    {
        //fields
        private int id;
        private TramType type;
        private int tramNummer;
        private string vertrekTijd;
        private TramStatus status;
        private string statusOpmerking;

        public int ID { get { return id; } set { id = value; } }
        public TramType Type { get { return type; } set { type = value; } }
        public int TramNummer { get { return tramNummer;} set { tramNummer = value; } }
        public string VertrekTijd { get { return vertrekTijd;} set { vertrekTijd = value; } }
        public TramStatus Status { get { return status;} set { status = value; } }
        public string StatusOpmerking { get {  return statusOpmerking;} set { statusOpmerking = value; } }

        // Tram ophalen VANUIT de database
        public Tram(int id, TramType type, int tramnummer, string vertrektijd, TramStatus status, string statusopmerking)
        {
            this.id = id;
            this.tramNummer = tramnummer;
            this.type = type;
            this.vertrekTijd = vertrektijd;
            this.status = status;
            this.statusOpmerking = statusopmerking;
        }

        // Tram aanmaken in de software
        public Tram(TramType type, int tramnummer, string vertrektijd, TramStatus status, string statusopmerking)
        {
            this.tramNummer = tramnummer;
            this.type = type;
            this.vertrekTijd = vertrektijd;
            this.status = status;
            this.statusOpmerking = statusopmerking;
        }
    }

    public enum TramType { Combino = 0, elfg = 1, dubbelkopcombino = 2, twaalfg = 3, opleidingstram = 4};

    public enum TramStatus { Onderhoud, InDienst, Geparkeerd};
}
