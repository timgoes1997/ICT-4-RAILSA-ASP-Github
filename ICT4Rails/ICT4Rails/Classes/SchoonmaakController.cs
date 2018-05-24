using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    class SchoonmaakController
    {
        private List<Schoonmaak> schoonmaak;

        public List<Schoonmaak> Schoonmaak { get { return schoonmaak; } set { value = schoonmaak; } } //schoonmaaklijst


        public SchoonmaakController()
        {
            DatabaseController dc = new DatabaseController();
            schoonmaak = dc.GetAllSchoonmaak();
        }

        /// <summary>
        /// Haalt lijst van schoonmaak op uit de database
        /// </summary>
        /// <returns></returns>
        public List<Schoonmaak> GetAllSchoonmaak()
        {
            DatabaseController dc = new DatabaseController();
            schoonmaak = dc.GetAllSchoonmaak();
            return schoonmaak;
        }

        /// <summary>
        /// Haalt alle trams op die in schoonmaak zijn.
        /// </summary>
        /// <returns></returns>
        public List<Tram> GetViezeTrams()
        {
            DatabaseController dc = new DatabaseController();
            List<Tram> trams = dc.GetAllTrams();
            foreach (Schoonmaak sch in schoonmaak)
            {
                foreach (Tram tram in trams.ToList())
                {
                    if (sch.Tram.TramNummer == tram.TramNummer)
                    {
                        trams.Remove(tram);
                    }

                }
            }
            return trams;
        }

        /// <summary>
        /// Controleert of een tram schoongemaakt moet worden
        /// </summary>
        /// <param name="tram">Tram die gecontroleert moet worden</param>
        /// <returns></returns>
        public bool TramControleerSchoonmaak(Tram tram)
        {
            DatabaseController dc = new DatabaseController();
            schoonmaak = dc.GetAllSchoonmaak();
            foreach (Schoonmaak s in schoonmaak)
            {
                if (s.Tram.ID == tram.ID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Rond schoonmaak af/ verwijder schoonmaak uit de database.
        /// </summary>
        public string SchoonmaalAfronden(int nummer)
        {
            DatabaseController dc = new DatabaseController();
            if (dc.DeleteSchoonmaak(nummer))
            {
                return "Schoonmaak " + nummer + " succesvol afgerond!";
            }
            else
            {
                return "Kon Schoonmaak " + nummer + " niet afronden. Probeer het later opnieuw";
            }
        }

        /// <summary>
        /// Voegt schoonmaak toe aan de database.
        /// </summary>
        /// <param name="tram"></param>
        /// <param name="opmerking"></param>
        /// <param name="date"></param>
        /// <param name="bevestigd"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public string AddSchoonmaak(Tram tram, string opmerking, DateTime date, bool bevestigd, SchoonmaakType st)
        {
            DatabaseController dc = new DatabaseController();
            if (dc.AddSchoonmaak(new Schoonmaak(tram, opmerking, date, bevestigd, st)))
            {
                return "Schoonmaak " + tram.TramNummer + " succesvol toegevoegd!";
            }
            else
            {
                return "Kon Schoonmaak " + tram.TramNummer + " niet toevoegen. Probeer het later opnieuw";
            }
        }
    }
}
