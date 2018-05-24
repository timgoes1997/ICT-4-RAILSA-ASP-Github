using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class Spoor
    {
        private int id;
        private Remise remise;
        private int nummer;
        private int lengte;
        private bool beschikbaar;
        private SpoorType spoorType; //In DB bool IUspoor
        private List<Sector> sectoren;

        public int ID { get { return id; } set { id = value; } }
        public Remise Remise { get { return remise; } set { remise = value; } }
        public int Nummer { get { return nummer; } set { nummer = value; } }
        public int Lengte { get { return lengte; } set { lengte = value; } }
        public bool Beschikbaar { get { return beschikbaar; } set { beschikbaar = value; } }
        public SpoorType SpoorType { get { return spoorType; } set { spoorType = value; } }
        public List<Sector> Sectoren { get { return sectoren; } set { sectoren = value; } }


        public Spoor(int id, Remise remise, int nummer, int lengte, bool beschikbaar, SpoorType spoorType)
        {
            this.id = id;
            this.remise = remise;
            this.nummer = nummer;
            this.lengte = lengte;
            this.beschikbaar = beschikbaar;
            this.spoorType = spoorType;
            this.sectoren = new List<Sector>();
        }

        /// <summary>
        /// Voegt een sector aan dit spoor.
        /// </summary>
        /// <param name="s"></param>
        public void SectorToevoegen(Sector s)
        {
            sectoren.Add(s);
        }

        /// <summary>
        /// Kijkt of er een sector binnen het spoor is geblokkeerd.
        /// </summary>
        /// <returns>Kijkt of er een sector binnen dit spoor is geblokkeerd</returns>
        public bool SectorGeblokkeerd()
        {
            //werkt dit??
            foreach (Sector s in sectoren)
            {
                if (s.Geblokkeerd)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Kijkt of een sector binnen dit spoor bestaat.
        /// </summary>
        /// <param name="sector">Welke sector in dit spoor moet zitten.</param>
        /// <returns>True waneer de sector zich binnen dit spoor bevind.</returns>
        public bool HeeftSector(Sector sector)
        {
            foreach (Sector s in sectoren)
            {
                if (s.ID == sector.ID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Verkrijgt alle legeSectoren van een spoor.
        /// </summary>
        /// <returns>Een lijst met lege sectoren</returns>
        public List<Sector> GetLegeSectoren()
        {
            List<Sector> vrijeSectoren = new List<Sector>();
            foreach (Sector s in sectoren)
            {
                if (s.Beschikbaar && !s.Geblokkeerd)
                {
                    vrijeSectoren.Add(s);
                }
            }
            return vrijeSectoren;
        }

        public bool GenoegVrijeSporen()
        {
            List<Reservering> reserveringen = new DatabaseController().GetAllReservering(remise);
            int aantalReserveringen = 0;
            foreach (Reservering r in reserveringen)
            {
                if (r.Spoor.ID == ID)
                {
                    aantalReserveringen++;
                }
            }

            if (GetLegeSectoren().Count > aantalReserveringen)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verkrijgt de eerste lege sector van dit spoor.
        /// </summary>
        /// <returns>Retourneert een lege sector.</returns>
        public Sector GetEersteSector()
        {
            for (int i = 0; i < sectoren.Count; i++)
            {
                if (sectoren[i].Beschikbaar && !sectoren[i].Geblokkeerd)
                {
                    if (i == sectoren.Count)
                    {
                        beschikbaar = false;
                    }
                    return sectoren[i];
                }
            }

            return null;
        }
    }
}