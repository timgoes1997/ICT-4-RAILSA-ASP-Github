using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    public class Spoor
    {
        //fields
        private int spoorNummer;
        private SpoorStatus spoorStatus;
        public SpoorType spoorType;
        private List<Sector> sectoren;

        //properties
        public int SpoorNummer { get { return spoorNummer; } set { spoorNummer = value; } }
        public SpoorStatus SpoorStatus { get { return spoorStatus; } set { spoorStatus = value; } }
        public List<Sector> Sectoren { get { return sectoren; } set { sectoren = value; } }
        public SpoorType SpoorType { get { return spoorType; } set { spoorType = value; } }

        //constructor
        public Spoor(int spoornummer, int spoorstatus, SpoorType spoortype)
        {
            this.spoorNummer = spoornummer;
            sectoren = new List<Sector>();
            if (spoorstatus == 0)
            {
                this.spoorStatus = Scripts.SpoorStatus.InGebruik;
            }
            else
            {
                this.spoorStatus = Scripts.SpoorStatus.Leeg;
            }
            this.spoorType = spoortype;
        }

        /// <summary>
        /// Voegt een sectoor toe aan het spoor.
        /// </summary>
        /// <param name="s">Voegt een sector toe aan dit spoor</param>
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
            foreach (Sector s in sectoren)
            {
                if (s.Status == SectorStatus.geblokeerd)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Verkrijgt de eerste legesector van dit spoor.
        /// </summary>
        /// <returns>Verkrijgt de eerst volgende lege sector binnen dit spoor</returns>
        public Sector VerkrijgEerstLegeSector()
        {
            for (int i = 0; i < sectoren.Count; i++)
            {
                if (sectoren[i].Status == SectorStatus.leeg)
                {
                    return sectoren[i];
                }
            }
            return null;
        }
        
        /// <summary>
        /// Veranderd de status van een sector in dit spoor.
        /// </summary>
        /// <param name="sector">Sector die van status moet worden veranderd</param>
        /// <param name="status">Naar welke status de sector moet worden veranderd</param>
        /// <returns>true wanneer een sector binnen dit spoor van status is veranderd.</returns>
        public bool VeranderSectorStatus(Sector sector, SectorStatus status)
        {
            foreach(Sector s in sectoren)
            {
                if(s.Id == sector.Id)
                {
                    s.Status = status;
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
            foreach(Sector s in sectoren)
            {
                if(s.Id == sector.Id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Verandert de spoorstatus naar leeg indien er geen sectoren meer worden gebruikt.
        /// </summary>
        public void ZetSpoorStatus()
        {
            spoorStatus = SpoorStatus.Leeg;

            foreach (Sector sector in sectoren)
            {
                if (sector != null)
                {
                    if (sector.Status == SectorStatus.bezet)
                    {
                        spoorStatus = SpoorStatus.InGebruik;
                    }
                }
            }
        }

        /// <summary>
        /// Kijkt of een spoor nog vrijesectoren heeft.
        /// </summary>
        /// <returns>True = vrije sectoren of false = geen sectoren</returns>
        public bool VrijeSectoren()
        {
            foreach (Sector s in sectoren)
            {
                if (s.Status == SectorStatus.leeg)
                {
                    return true;
                }
            }
            return false;
        }

    }
    public enum SpoorType { Normaal, Schoonmaak, Reparatie };

    public enum SpoorStatus { InGebruik, Leeg};
}
