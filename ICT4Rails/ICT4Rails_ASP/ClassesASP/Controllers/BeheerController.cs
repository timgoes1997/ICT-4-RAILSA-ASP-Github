using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public class BeheerController
    {
        private Remise huidigeRemise;
        public Remise HuidigeRemise { get { return huidigeRemise; } set { huidigeRemise = value; } }
        
        public BeheerController()
        {
            DatabaseController dc = new DatabaseController();
            huidigeRemise = dc.GetAllRemise()[0];
        }

        /// <summary>
        /// Verkrijgt alle geblokkeerde sectoren.
        /// </summary>
        /// <returns>Retourneert een lijst met geblokkeerde sectoren.</returns>
        public List<Sector> GetAllGeblokkeerdeSectoren()
        {
            List<Sector> geblokkeerdeSectoren = new List<Sector>();
            foreach (Spoor spoor in GetAllSporen())
            {
                foreach (Sector s in spoor.Sectoren)
                {
                    if (s.Geblokkeerd)
                    {
                        geblokkeerdeSectoren.Add(s);
                    }
                }
            }
            return geblokkeerdeSectoren;
        }

        /// <summary>
        /// Verkrijgt alle niet Geblokkeerde sectoren.
        /// </summary>
        /// <returns>Retourneert een lijst met niet geblokkeerde sectoren.</returns>
        public List<Sector> GetAllNietGeblokkeerdeSectoren()
        {
            List<Sector> nietGeblokkeerdeSectoren = new List<Sector>();
            foreach (Spoor spoor in GetAllSporen())
            {
                foreach (Sector s in spoor.Sectoren)
                {
                    if (!s.Geblokkeerd)
                    {
                        nietGeblokkeerdeSectoren.Add(s);
                    }
                }
            }
            return nietGeblokkeerdeSectoren;
        }


        /// <summary>
        /// Kijkt of er nog een paar trams beschikbaar zijn.
        /// </summary>
        /// <returns>Retourneert false wanneer er geen trams meer in dienst zijn.</returns>
        public bool TramsNietBeschikbaar()
        {
            foreach (Tram tram in GetAllTrams())
            {
                if (!tram.Beschikbaar)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Controleert of een tram een reservering heeft.
        /// </summary>
        /// <param name="tram"></param>
        /// <returns></returns>
        public bool TramControleerReservering(Tram tram)
        {
            foreach (Reservering reservering in GetAllReserveringen())
            {
                if (reservering.Tram.TramNummer == tram.TramNummer)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Verkrijgt de reservering van een tram
        /// </summary>
        /// <param name="tram"></param>
        /// <returns></returns>
        public Reservering GetReservering(Tram tram)
        {
            foreach (Reservering reservering in GetAllReserveringen())
            {
                if (reservering.Tram.ID == tram.ID)
                {
                    return reservering;
                }
            }
            return null;
        }

        /// <summary>
        /// Verkrijgt de eerste beschikbare tram.
        /// </summary>
        /// <returns>Eerste beschikbare tram</returns>
        public Tram VerkrijgEersteNietBeschikbareTram()
        {
            foreach (Tram tram in GetAllTrams())
            {
                if (!tram.Beschikbaar)
                {
                    return tram;
                }
            }
            return null;
        }

        /// <summary>
        /// Verkrijgt een willekeurige niet beschikbare tram.
        /// </summary>
        /// <returns>Willekeurige niet beschikbare tram</returns>
        public Tram GetWillekeurigeNietBeschikbareTram()
        {
            List<Tram> trams = GetNietBeschikbareTrams(); //Vraagt een lijst op van NietBeschikbare Trams.
            int random = new Random().Next(0, trams.Count); //Pakt een willekeurig getal afhankelijk van de grootte van de lijst.
            return trams[random]; //Pakt een willekeurige tram uit de lijst met beschikbare trams.
            //foreach (Tram t in trams.ToList())
            //{
            //    if (t.Defect || t.Vervuild)
            //    {
            //        trams.Remove(t);
            //    }
            //}

            //if (trams[random] != null)
            //{
            //    if (!trams[random].Defect && !trams[random].Vervuild)
            //    {
            //    }
            //}
            //return null;
        }

        /// <summary>
        /// Verkrijgt alle trams die in dienst zijn.
        /// </summary>
        /// <returns>Retourneert een lijst met trams die in dienst zijn.</returns>
        public List<Tram> GetNietBeschikbareTrams()
        {
            List<Tram> tramsBeschikbaar = new List<Tram>();

            foreach (Tram tram in GetAllTrams())
            {
                if (!tram.Beschikbaar)
                {
                    tramsBeschikbaar.Add(tram);
                }
            }

            return tramsBeschikbaar;
        }

        /// <summary>
        /// Voegt een tram toe aan de database
        /// </summary>
        /// <param name="tramNummer">Het nummer van de tram die je wilt toevoegen</param>
        /// <param name="vertrektijd">De vertrektijd van de tram die je wilt toevoegen</param>
        /// <param name="type">Het soort tram dat je wilt toevoegen</param>
        public bool TramToevoegen(int tramNummer, DateTime vertrektijd, TramType type)
        {
            DatabaseController dc = new DatabaseController();
            Tram tram = new Tram(huidigeRemise, type , vertrektijd, tramNummer, 1, "", false, false, true, false);
            if (dc.AddTram(tram))
            {
                return true;
            }
            else
            {
                throw new TramToevoegException();
            }
        }

        /// <summary>
        /// Verwijdert een tram van de database
        /// </summary>
        /// <param name="tramNummer">Welke tram verwijdert moet worden.</param>
        public bool TramVerwijderen(Tram tram)
        {
            DatabaseController dc = new DatabaseController();
            if (dc.DeleteTram(tram))
            {
                return true;
            }
            else
            {
                throw new TramVerwijderException();
            }
        }

        /// <summary>
        /// Reserveert een sector voor een tram.
        /// </summary>
        /// <param name="sector">De sector die gereserveerd moet worden.</param>
        /// <param name="tram">De tram waarvoor de reservering wordt gemaakt.</param>
        /// <returns>Een string met het bericht of iets is gelukt of niet.</returns>
        public void SpoorReserveren(Spoor spoor, Tram tram)
        {
            DatabaseController dc = new DatabaseController();
            int aantalReserveringen = 0;
            foreach (Reservering r in GetAllReserveringen())
            {
                if (r.Spoor.ID == spoor.ID)
                {
                    aantalReserveringen++;
                    if (r.Tram.ID == tram.ID)
                    {
                        throw new SpoorReserveerException("Tram is al gereserveerd!");
                    }
                }
            }

            if (aantalReserveringen < spoor.Sectoren.Count)
            {
                Reservering res = new Reservering(tram, spoor);
                if (!dc.AddReservering(res))
                {
                    throw new SpoorReserveerException("De reservering kon niet worden toegevoegd aan de database!");
                }
            }
            else
            {
                throw new SpoorReserveerException("Er zijn al teveel reserveringen voor dit spoor waardoor er geen sectoren meer over zijn!");
            }
        }

        /// <summary>
        /// Verplaats de tram naar een ander spoor of sector
        /// </summary>
        /// <param name="tram">De tram die verplaatst moet worden</param>
        /// <param name="spoorVan">Het spoor waar die tram op staat</param>
        /// <param name="sectorVan">De sector waar die tram op staat</param>
        /// <param name="spoorNaar">Het spoor waar de tram naar toe gaat</param>
        /// <param name="sectorNaar">De sector waar de tram naar toe gaat.</param>
        /// <returns>Retourneert een fout string</returns>
        public bool TramVerplaatsen(Tram tram, Spoor spoorVan, Sector sectorVan, Spoor spoorNaar, Sector sectorNaar) //TODO: Exception geven wanneer het niet lukt. Dit in het form afhandelen.
        {
            foreach (Sector sector in spoorVan.Sectoren)
            {
                if(sector.Tram.ID == tram.ID)
                {
                    foreach (Sector naarSector in spoorNaar.Sectoren)
                    {
                        if(naarSector.ID == sectorNaar.ID)
                        {
                            sector.VerwijderTram();
                            naarSector.VerwijderTram();
                            DatabaseController dc = new DatabaseController();
                            if (dc.UpdateSector(sector, huidigeRemise))
                            {
                                if (dc.UpdateSector(naarSector, huidigeRemise))
                                {
                                    return true;
                                }
                                else
                                {
                                    throw new TramVerplaatsException("De sector waar de tram naar verplaatst wordt kon niet worden geüpdate in de database!");
                                }
                            }
                            else
                            {
                                throw new TramVerplaatsException("De sector waarvan de tram word verplaats kon niet worden geüpdate in de database!");
                            }
                        }
                    }
                }
            }

            throw new TramVerplaatsException("De sector waarvan de tram word verplaats of de sector waarnaar de tram wordt verplaats kon niet gevonden worden!" );
        }

        /// <summary>
        /// Blokeert/deblokkeert een sector.
        /// </summary>
        /// <param name="spoorNummer"></param>
        /// <param name="sectorNummer"></param>
        public void ToggleSector(Sector sector) //TODO: Exception geven wanneer het niet lukt. Dit in het form afhandelen.
        {
            if (sector.Tram != null)
            {
                throw new ToggleSectorException("Er staat nog een tram op de sector.");
            }
            sector.Geblokkeerd = !sector.Geblokkeerd;
            sector.Beschikbaar = !sector.Beschikbaar;
            DatabaseController dc = new DatabaseController();
            if (!dc.UpdateSector(sector, huidigeRemise))
            {
                throw new ToggleSectorException("De sector kon niet worden geblokkeerd/gedeblokkeerd!");
            }
        }

        /// <summary>
        /// Veranderd de status van de tram
        /// </summary>
        /// <param name="tram">De tram die van status moet worden veranderd</param>
        /// <param name="tramStatus">De status waar de tram naar moet worden veranderd</param>
        public void TramStatusAanpassen(Tram tram, bool vervuild, bool defect) //TODO: Exception geven wanneer het niet lukt. Dit in het form afhandelen.
        {
            tram.Vervuild = vervuild;
            tram.Defect = defect;
            DatabaseController dc = new DatabaseController();
            if (!dc.UpdateTram(tram))
            {
                throw new TramStatusException("De status van de tram kont niet worden veranderd.");
            }
        }

        /// <summary>
        /// Verkrijgt een tram door met tramnummer
        /// </summary>
        /// <param name="tramNummer">Nummer van tram die je nodig hebt</param>
        /// <returns>Tram die je nodig hebt</returns>
        public Tram GetTramByNummer(int tramNummer)
        {
            foreach (Tram tram in GetAllTrams())
            {
                if (tram.TramNummer == tramNummer)
                {
                    return tram;
                }
            }
            return null;
        }

        /// <summary>
        /// Verkrijgt een spoor met behulp van ID
        /// </summary>
        /// <param name="id">Het ID van het spoor dat je nodig hebt</param>
        /// <returns>Retourneert een spoor</returns>
        public Spoor GetSpoorByNummer(int nummer)
        {
            foreach (Spoor sp in GetAllSporen())
            {
                if (sp.Nummer == nummer)
                {
                    return sp;
                }
            }
            return null;
        }

        /// <summary>
        /// Verkrijgt het spoor waar een tram op staat
        /// </summary>
        /// <param name="tram">De tram die mogelijk op een spoor staat</param>
        /// <returns>Het spoor waar de meegegeven tram op staat</returns>
        public Spoor GetSpoorByTram(Tram tram)
        {
            foreach (Spoor spoor in GetAllSporen())
            {
                foreach (Sector s in spoor.Sectoren)
                {
                    if (s.Tram != null)
                    {
                        if (s.Tram.ID == tram.ID)
                        {
                            return spoor;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Verkrijgt de sector waar mogelijk een tram op staat.
        /// </summary>
        /// <param name="tram">De tram waarmee je de sector zoekt.</param>
        /// <returns>Retourneert de sector waar de meegegeven tram op staat</returns>
        public Sector GetSectorByTram(Tram tram)
        {
            foreach (Spoor spoor in GetAllSporen())
            {
                foreach (Sector s in spoor.Sectoren)
                {
                    if (s.Tram != null)
                    {
                        if (s.Tram.ID == tram.ID)
                        {
                            return s;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Verkrijgt een sector van een specifiek spoor.
        /// </summary>
        /// <param name="spoor">Het spoor waar de sector zich in moet bevinden</param>
        /// <param name="sectorID">Het id van de sector die gevonden moet worden</param>
        /// <returns>Retourneert een sector wanneer die gevonden is op het gegeven spoor.</returns>
        public Sector GetSectorByID(Spoor spoor, int sectorID)
        {
            foreach (Sector s in spoor.Sectoren)
            {
                if (s.ID == sectorID)
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// Get all reservering objects from db.
        /// </summary>
        /// <returns>Retourneert een lijst met reserveringen</returns>
        public List<Reservering> GetAllReserveringen()
        {
            return new DatabaseController().GetAllReservering(huidigeRemise);
        }

        /// <summary>
        /// Verkrijgt alle trams van de DB
        /// </summary>
        /// <returns></returns>
        public List<Tram> GetAllTrams()
        {
            return new DatabaseController().GetAllTrams(huidigeRemise);
        }

        /// <summary>
        /// Verkrijt alle sporen van de DB
        /// </summary>
        /// <returns></returns>
        public List<Spoor> GetAllSporen()
        {
            return new DatabaseController().GetAllSporen(huidigeRemise);
        }

        /// <summary>
        /// Verkrijgt alle lijnen van de DB
        /// </summary>
        /// <returns></returns>
        public List<Lijn> GetAllLijnen()
        {
            return new DatabaseController().GetAllLijnen(HuidigeRemise);
        }

        /// <summary>
        /// Verkrijgt alle verbindingen van de DB
        /// </summary>
        /// <returns></returns>
        public List<Verbinding> GetAllVerbinding()
        {
            return new DatabaseController().GetAllVerbindingen(HuidigeRemise);
        }

        /// <summary>
        /// Verkrijgt alle remise van de DB.
        /// </summary>
        /// <returns></returns>
        public List<Remise> GetAllRemise()
        {
            return new DatabaseController().GetAllRemise();
        }
    }
}