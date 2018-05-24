using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    public class Sector
    {
        //fields
        private int id;
        //private int tramId;
        private int spoorNummer;
        private Tram tram;
        private SectorStatus status;
        private bool isGereserveerd;

        //properties
        public int Id { get { return id; } set { id = value; } }
        //public int TramId { get { return tramId; } set { tramId = value; } }
        public int SpoorNummer { get { return spoorNummer; } set { spoorNummer = value; } }
        public Tram Tram { get { return tram; } set { tram = value; } }
        public SectorStatus Status { get { return status; } set { status = value; } }
        public bool IsGereserveerd { get { return isGereserveerd; } set { isGereserveerd = value; } }

        //constructor
        public Sector(int id, Tram tram, int spoornummer, SectorStatus status)
        {
            this.id = id;
            spoorNummer = spoornummer;
            this.tram = tram;
            this.status = status;
        }

        public bool IsGereserveerdCheck(List<Reservering> reserveringen)
        {
            foreach (Reservering reservering in reserveringen)
            {
                if(this.Id == reservering.Sector.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public enum SectorStatus { bezet, leeg, geblokeerd};
}
