using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class OnderhoudController
    {
        #region Fields
        private Remise huidigeRemise;
        public Remise HuidigeRemise { get { return huidigeRemise; } set { huidigeRemise = value; } }

        #endregion
        #region Properties

        #endregion
        #region Constructors
        public OnderhoudController(Remise huidigeRemise)
        {
            this.huidigeRemise = huidigeRemise;
        }
        public OnderhoudController()
        {
            this.huidigeRemise = new DatabaseController().GetAllRemise()[0];
        }
        #endregion
        #region Methods

        /// <summary>
        /// Kijkt of een tram nog gerepareerd moet worden.
        /// </summary>
        /// <param name="tram"></param>
        /// <returns>Of een tram wel of niet moet worden gerepareerd</returns>
        public bool TramControleerOnderhoud(Tram tram)
        {
            foreach (Onderhoud o in GetAllOnderhoud())
            {
                if (o.Tram.ID == tram.ID)
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
        public List<Tram> GetDefecteTrams()
        {
            DatabaseController dc = new DatabaseController();
            List<Tram> trams = dc.GetAllTrams(huidigeRemise);
            List<Tram> defecteTrams = new List<Tram>();
            foreach (Onderhoud o in GetAllOnderhoud())
            {
                foreach (Tram tram in trams)
                {
                    if (o.Tram.TramNummer == tram.TramNummer && (o.TypeOnderhoud == TypeOnderhoud.KleineServiceBeurt || o.TypeOnderhoud == TypeOnderhoud.GroteServiceBeurt))
                    {
                        defecteTrams.Add(tram);
                    }
                }
            }
            return defecteTrams;
        }

        /// <summary>
        /// Haalt alle trams op die in schoonmaak zijn.
        /// </summary>
        /// <returns></returns>
        public List<Tram> GetViezeTrams()
        {
            DatabaseController dc = new DatabaseController();
            List<Tram> trams = dc.GetAllTrams(huidigeRemise);
            List<Tram> viezeTrams = new List<Tram>();
            foreach (Onderhoud o in GetAllOnderhoud())
            {
                foreach (Tram tram in trams)
                {
                    if (o.Tram.TramNummer == tram.TramNummer && (o.TypeOnderhoud == TypeOnderhoud.KleineSchoonmaakBeurt || o.TypeOnderhoud == TypeOnderhoud.GroteSchoonmaakBeurt))
                    {
                        viezeTrams.Add(tram);
                    }
                }
            }
            return viezeTrams;
        }

        /// <summary>
        /// Verkrijgt alle trams die niet schoongemaakt zijn.
        /// </summary>
        /// <returns></returns>
        public List<Tram> GetNietIngedeeldeTramsSchoonmaak()
        {
            DatabaseController dc = new DatabaseController();
            List<Tram> trams = dc.GetAllTrams(huidigeRemise);
            foreach (Onderhoud o in GetAllOnderhoud())
            {
                foreach (Tram tram in trams.ToList())
                {
                    if (o.Tram.TramNummer == tram.TramNummer && (o.TypeOnderhoud == TypeOnderhoud.KleineSchoonmaakBeurt || o.TypeOnderhoud == TypeOnderhoud.GroteSchoonmaakBeurt))
                    {
                        trams.Remove(tram);
                    }
                }
            }
            return trams;
        }


        /// <summary>
        /// Haalt een lijst op van alle trams die niet defect zijn.
        /// </summary>
        /// <returns>Retourneert een lijst met niet defecte trams.</returns>
        public List<Tram> GetNietDefecteTrams()
        {
            DatabaseController dc = new DatabaseController();
            List<Tram> trams = dc.GetAllTrams(huidigeRemise);
            foreach (Onderhoud o in GetAllOnderhoud())
            {
                foreach (Tram tram in trams.ToList())
                {
                    if (o.Tram.TramNummer == tram.TramNummer && (o.TypeOnderhoud == TypeOnderhoud.KleineServiceBeurt || o.TypeOnderhoud == TypeOnderhoud.GroteServiceBeurt))
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
        public void OnderhoudAfronden(int onderhoudsID)
        {
            DatabaseController dc = new DatabaseController();
            dc.DeleteOnderhoud(onderhoudsID);
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
        public void AddOnderhoud(Onderhoud onderhoud)
        {
            DatabaseController dc = new DatabaseController();
            if (!dc.AddOnderhoud(onderhoud))
            {
                throw new OnderhoudToevoegException("Onderhoud kon niet worden toegevoegd aan de database!");
            }
        }


        /// <summary>
        /// Verkrijgt alle onderhoud uit de database.
        /// </summary>
        /// <returns>Een lijst met reparaties</returns>
        private List<Onderhoud> GetAllOnderhoud()
        {
            return new DatabaseController().GetAllOnderhoud(huidigeRemise);
        }

        public List<Onderhoud> GetAllReparatie()
        {
            List<Onderhoud> result = new List<Onderhoud>();

            foreach (Onderhoud o in GetAllOnderhoud())
            {
                if (o.TypeOnderhoud == TypeOnderhoud.GroteServiceBeurt
                    || o.TypeOnderhoud == TypeOnderhoud.KleineServiceBeurt)
                {
                    result.Add(o);
                }
            }

            return result;
        }

        public List<Onderhoud> GetAllSchoonmaak()
        {
            List<Onderhoud> result = new List<Onderhoud>();

            foreach (Onderhoud o in GetAllOnderhoud())
            {

                if (o.TypeOnderhoud == TypeOnderhoud.GroteSchoonmaakBeurt
                    || o.TypeOnderhoud == TypeOnderhoud.KleineSchoonmaakBeurt)
                {
                    result.Add(o);
                }
            }

            return result;
        }

        public Onderhoud GetOnderhoud(Tram tram)
        {
            foreach (Onderhoud onderhoud in GetAllOnderhoud())
            {
                if (onderhoud.Tram.ID == tram.ID)
                {
                    return onderhoud;
                }
            }

            return null;
        }

        /// <summary>
        /// verkrijgt een vrij spoor waar onderhoud voor een tram kan op gevoerd.
        /// </summary>
        /// <returns>Een vrij onderhoudsspoor</returns>
        public Spoor GetVrijOnderhoudsSpoor()
        {
            DatabaseController dc = new DatabaseController();
            foreach (Spoor s in dc.GetAllSporen(huidigeRemise))
            {
                if (s.SpoorType == SpoorType.Onderhoud && s.Beschikbaar)
                {
                    return s;
                }
            }
            return null;
        }
        #endregion
    }
}