using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    class ReparatieController
    {
        private List<Reparatie> reparatie;

        public List<Reparatie> Reparatie { get { return reparatie; } set { value = reparatie; } }



        public ReparatieController()
        {
            DatabaseController dc = new DatabaseController();
            reparatie = dc.GetAllReparatie();
        }

        /// <summary>
        /// Verkrijgt alle reparaties vanuit de database.
        /// </summary>
        /// <returns>Een lijst met reparaties</returns>
        public List<Reparatie> GetAllReparatie()
        {
            DatabaseController dc = new DatabaseController();
            reparatie = dc.GetAllReparatie();
            return reparatie;
        }

        /// <summary>
        /// Kijkt of een tram nog gerepareerd moet worden.
        /// </summary>
        /// <param name="tram"></param>
        /// <returns>Of een tram wel of niet moet worden gerepareerd</returns>
        public bool TramControleerReparatie(Tram tram)
        {
            DatabaseController dc = new DatabaseController();
            reparatie = dc.GetAllReparatie();
            foreach (Reparatie r in reparatie)
            {
                if (r.Tram.ID == tram.ID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Haalt een lijst op van alle trams die in reparatie zijn.
        /// </summary>
        /// <returns></returns>
        public List<Tram> GetKapotteTrams()
        {
            DatabaseController dc = new DatabaseController();
            List<Tram> trams = dc.GetAllTrams();
            foreach(Reparatie r in reparatie)
            {
                foreach(Tram tram in trams.ToList())
                {
                    if (r.Tram.TramNummer == tram.TramNummer)
                    {
                        trams.Remove(tram);
                    }
                }
            }
            return trams;

        }
        /// <summary>
        /// Rond reparatie af/ verwijderd tram uit reparatie in database
        /// </summary>
        /// <param name="nummer"></param>
        /// <returns></returns>
        public string ReparatieAfronden(int nummer)
        {
            DatabaseController dc = new DatabaseController();
            if (dc.DeleteSchoonmaak(nummer))
            {
                return "Reparatie " + nummer + " succesvol afgerond!";
            }
            else
            {

                return "Kon Reparatie " + nummer + " niet afronden. Probeer het later opnieuw";
            }
        }

        /// <summary>
        /// Voegt een reparatie toe aan de database.
        /// </summary>
        /// <param name="reparatie"></param>
        /// <param name="tram"></param>
        /// <param name="opmerking"></param>
        /// <param name="date"></param>
        /// <param name="bevestigd"></param>
        /// <returns></returns>
        public string AddReparatie(ReparatieType reparatie, Tram tram, string opmerking, DateTime date, bool bevestigd)
        {
            DatabaseController dc = new DatabaseController();
            if (dc.AddReparatie(new Reparatie(reparatie, tram, opmerking, date, false)))
            {
                return "Reparatie " + tram.TramNummer + " succesvol toegevoegd!";
            }
            else
            {
                return "Kon Reparatie " + tram.TramNummer + " niet toevoegen. Probeer het later opnieuw";
            }
        }
    }
}
