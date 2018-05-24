using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    public class BeheerController
    {
        private List<Tram> trams; //trams
        private List<Spoor> sporen; //sporen

        public List<Tram> Trams { get { return trams; } set { value = trams; } } //trams
        public List<Spoor> Sporen { get { return sporen; } set { value = sporen; } } //sporen

        private DatabaseController dc;

        public BeheerController()
        {
            dc = new DatabaseController();
            trams = dc.GetAllTrams();
            sporen = GetAllSporen();
        }

        /// <summary>
        /// Update de lijsten in deze klasse, de trams en sporen.
        /// </summary>
        public void Update()
        {
            trams = dc.GetAllTrams();
            sporen = GetAllSporen();
        }

        /// <summary>
        /// Kijkt of er nog een paar trams de status in dienst hebben.
        /// </summary>
        /// <returns>Retourneert false wanneer er geen trams meer in dienst zijn.</returns>
        public bool TramInDienst()
        {
            foreach (Tram tram in trams)
            {
                if (tram.Status == TramStatus.InDienst)
                {
                    return true;
                }
            }
            return false;
        }

        public bool TramControleerReservering(Tram tram)
        {
            foreach (Reservering reservering in GetAllReserveringen())
            {
                if(reservering.Tram.TramNummer == tram.TramNummer)
                {
                    return true;
                }
            }

            return false;
        } 

        /// <summary>
        /// Verkrijgt alle trams die in dienst zijn.
        /// </summary>
        /// <returns>Retourneert een lijst met trams die in dienst zijn.</returns>
        public List<Tram> TramsInDienst()
        {
            List<Tram> tramsInDienst = new List<Tram>();

            foreach (Tram tram in trams)
            {
                if (tram.Status == TramStatus.InDienst)
                {
                    tramsInDienst.Add(tram);
                }
            }

            return tramsInDienst;
        }

        /// <summary>
        /// Voegt een tram toe aan de database
        /// </summary>
        /// <param name="tramNummer">Het nummer van de tram die je wilt toevoegen</param>
        /// <param name="vertrektijd">De vertrektijd van de tram die je wilt toevoegen</param>
        /// <param name="type">Het soort tram dat je wilt toevoegen</param>
        public string TramToevoegen(int tramNummer, string vertrektijd, TramType type)
        {
            if (dc.AddTram(new Tram(type, tramNummer, vertrektijd, TramStatus.InDienst, "")))
            {
                return "Tram " + tramNummer + " succesvol toegevoegd!";
            }
            else
            {
                return "Kon tram " + tramNummer + " niet toevoegen. Probeer het later opnieuw";
            }
        }

        /// <summary>
        /// Verwijdert een tram van de database
        /// </summary>
        /// <param name="tramNummer">Welke tram verwijdert moet worden.</param>
        public string TramVerwijderen(int tramNummer)
        {
            if (dc.DeleteTram(tramNummer))
            {
                return "Tram " + tramNummer + " succesvol verwijderd!";
            }
            else
            {
                return "Kon tram " + tramNummer + " niet verwijderen. Probeer het later opnieuw";
            }
        }

        /// <summary>
        /// Reserveert een sector voor een tram.
        /// </summary>
        /// <param name="sector">De sector die gereserveerd moet worden.</param>
        /// <param name="tram">De tram waarvoor de reservering wordt gemaakt.</param>
        /// <returns>Een string met het bericht of iets is gelukt of niet.</returns>
        public string SectorReserveren(Sector sector, Tram tram) //TODO optimaliseren/verkleinen, we hebben hiervoor al methodes in deze klassen zitten.
        {
            Reservering r = new Reservering(tram, sector);

            if (dc.AddReservering(r))
            {
                Spoor spoor = GetSpoorByNummer(sector.SpoorNummer);
                spoor.SpoorStatus = SpoorStatus.InGebruik;
                dc.UpdateSpoor(spoor);
                return "Reservering voor tramnummer " + tram.TramNummer + " toegevoegd op sector met ID " + sector.Id;
            }
            else
            {
                return "Kon reservering voor tramnummer " + tram.TramNummer + " niet toevoegen op sector met ID " +
                       sector.Id;
            }
        }

        /// <summary>
        /// Get all reservering objects from db.
        /// </summary>
        /// <returns>Retourneert een lijst met reserveringen</returns>
        public List<Reservering> GetAllReserveringen()
        {
            return new DatabaseController().GetAllReservering();
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
        public string TramVerplaatsen(Tram tram, Spoor spoorVan, Sector sectorVan, Spoor spoorNaar, Sector sectorNaar)
        {
            //Todo: Als spoorVan en sectorVan niet null zijn dan moet er nog worden gekeken of de tram überhaupt wel verplaatst mag worden.
            string result = " ";

            if (sectorVan != null && spoorVan != null)
            {
                if (sectorVan.Tram.TramNummer == tram.TramNummer)
                {
                    if (spoorVan.Sectoren[0].SpoorNummer == sectorVan.SpoorNummer)
                    {
                        spoorVan.VeranderSectorStatus(sectorVan, SectorStatus.leeg);
                        sectorVan.Status = SectorStatus.leeg;
                        spoorVan.ZetSpoorStatus();
                        dc.UpdateSector(sectorVan);
                        dc.UpdateSpoor(spoorVan);
                    }
                    else
                    {
                        return "tram kan niet verplaatst worden omdat die niet op de eerste sector staat!";
                    }
                }
            }

            if (spoorNaar != null && sectorNaar != null)
            {
                if (spoorNaar.HeeftSector(sectorNaar))
                {
                    sectorNaar.Status = SectorStatus.bezet;
                    sectorNaar.Tram = tram;
                    spoorNaar.SpoorStatus = SpoorStatus.InGebruik;
                    dc.UpdateSector(sectorNaar);
                    dc.UpdateSpoor(spoorNaar);
                }
                else
                {
                    return "Sector niet gevonden bij gegeven spoor!";
                }
            }

            return result;
        }

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tramNummer"></param>
        /// <param name="spoorNummer"></param>
        /// <param name="sectorNummer"></param>
        public string TramVerplaatsen(int tramnummer, int spoornummer, int sectorID_naar) //TODO optimaliseren/verkleinen, we hebben daarvoor al methodes in deze klassen zitten.
        {
            Sector van = null;
            Spoor spoorvan = null;

            foreach (Spoor spoor in sporen)
            {
                foreach (Sector sector in spoor.Sectoren)
                {
                    if (sector.Tram == null)
                    {
                        continue;//to next foreach loop, aka skip the next if-statement
                    }
                    if (sector.Tram.TramNummer == tramnummer)
                    {
                        van = sector;
                        spoorvan = spoor;
                        van.Status = SectorStatus.leeg;
                    }
                }
            }

            if (van == null)
            {
                return "Sector 'van' kon niet gevonden worden aan de hand van dit tramnummer.\r\nWaarschijnlijk staat deze tram niet op een sector momenteel.";
            }

            if (spoorvan == null)
            {
                return "Kon spoorvan niet vinden.";
            }

            Sector naar = null;

            foreach (Spoor spoor in sporen)
            {
                foreach (Sector sector in spoor.Sectoren)
                {
                    if (sector.Id == sectorID_naar)
                    {
                        naar = sector;
                        naar.Status = SectorStatus.bezet;
                    }
                }
            }

            if (naar == null)
            {
                return "Sector 'naar' kon niet gevonden worden.";
            }

            string result = "";


            van.Tram = null;
            if (dc.UpdateSector(van))
            {
                result += "Updaten van de oude sector is gelukt. ";
            }
            else
            {
                result += "Kon de oude sector niet updaten. ";
            }

            Tram t = null;
            foreach (Tram tram in trams)
            {
                if (tram.TramNummer == tramnummer)
                {
                    t = tram;
                }
            }
            if (t == null)
            {
                return "Kon tram met tramnummer " + tramnummer + " niet vinden.";
            }

            naar.Tram = t;
            if (dc.UpdateSector(naar))
            {
                result += "Updaten van de nieuwe sector is gelukt.";
            }
            else
            {
                result += "Kon de nieuwe sector niet updaten.";
            }

            Spoor spoor_naar = null;

            foreach (Spoor spoor in sporen)
            {
                if (spoor.SpoorNummer == spoornummer)
                {
                    spoor_naar = spoor;
                }
            }

            spoorvan.SpoorStatus = SpoorStatus.Leeg;

            foreach (Sector sector in spoorvan.Sectoren)
            {
                if (sector.Status == SectorStatus.bezet)
                {
                    spoorvan.SpoorStatus = SpoorStatus.InGebruik;
                }
            }

            spoor_naar.SpoorStatus = SpoorStatus.InGebruik;

            dc.UpdateSector(naar);
            dc.UpdateSector(van);
            dc.UpdateSpoor(spoorvan);
            dc.UpdateSpoor(spoor_naar);

            return result;
        }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spoorNummer"></param>
        public string ToggleSpoor(Spoor spoor) //TODO: kan nog geoptimaliseerd/verkleint worden. In plaats van spoornummer Spoor parameter zelf en de GetSpoorByID method aanroepen met tramnummer.
        {
            string p_result;

            // Als de sector niet gevonden is, return error
            if (spoor == null)
            {
                return "Error, kon sector niet vinden";
            }

            // Toggle het spoor
            if (spoor.SpoorStatus == SpoorStatus.InGebruik)
            {
                p_result = "leeg";
                spoor.SpoorStatus = SpoorStatus.Leeg;
            }
            else
            {
                p_result = "in gebruik";
                spoor.SpoorStatus = SpoorStatus.InGebruik;
            }

            if (dc.UpdateSpoor(spoor))
            {
                return "Spoorstatus is nu " + p_result;
            }
            else
            {
                return "Kon spoor niet updaten.";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spoorNummer"></param>
        /// <param name="sectorNummer"></param>
        public string ToggleSector(Sector sector) //TODO: kan nog geoptimaliseerd/verkleint worden. In plaats van spoornummer Spoor parameter zelf en de GetSpoorByID method aanroepen met tramnummer. Zelfde geld voor sector alleen moet daarvoor nog een methode voor worden aangemaakt.
        {
            #region Toggle sector naar geblokkeerd of leeg
            string s_result;
            // Update de status
            if (sector.Status == SectorStatus.bezet)
            {
                s_result = "leeg.";
                sector.Status = SectorStatus.leeg;
            }
            else
            {
                s_result = "bezet.";
                sector.Status = SectorStatus.bezet;
            }

            // Return het resultaat van het updaten
            if (dc.UpdateSector(sector))
            {
                return "Sector is nu " + s_result;
            }
            else
            {
                return "Kon sector niet updaten.";
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spoorNummer"></param>
        /// <param name="sectorNummer"></param>
        public void TramPlaatsen(Tram tram, Spoor spoor, Sector sector)
        {

        }

        /// <summary>
        /// Veranderd de status van de tram
        /// </summary>
        /// <param name="tram">De tram die van status moet worden veranderd</param>
        /// <param name="tramStatus">De status waar de tram naar moet worden veranderd</param>
        public string TramStatusAanpassen(Tram tram, TramStatus tramStatus) //TODO: wanneer de tram naar indienst wordt verzet en op het eerste spoor staat dan verdwijnt die uit de 
        {
            DatabaseController dc = new DatabaseController();
            if (tramStatus == TramStatus.InDienst)
            {
                Spoor spoor = GetSpoorByTram(tram);
                Sector sector = GetSectorByTram(tram);

                if (spoor.Sectoren[0].Id == sector.Id)
                {
                    sector.Tram = null;
                    sector.Status = SectorStatus.leeg;
                    dc.UpdateSector(sector);
                }
                else
                {
                    return "De tram wordt geblokkeerd door een andere tram op het spoor, hierdoorm kan de tram nog niet naar indienst worden gezet.";
                }
            }
            else
            {
                tram.Status = tramStatus; //TODO: tram naar onderhouds of normaal spoor sturen wanneer die van status is veranderd. (kan volgens mij met algoritme)
            }

            if (dc.UpdateTram(tram))
            {
                return "Gelukt! Tramstatus van tram " + tram.TramNummer + " is nu " + tramStatus.ToString() + ".";
            }
            else
            {
                return "Kon de tramstatus van tram " + tram.TramNummer + " niet aanpassen.";
            }

        }

        /// <summary>
        /// Verkrijgt alle sporen
        /// </summary>
        /// <returns>Een lijst met alle sporen</returns>
        public List<Spoor> GetAllSporen()
        {
            DatabaseController dc = new DatabaseController();
            sporen = dc.GetAllSporen();
            return sporen;
        }

        /// <summary>
        /// Verkrijgt alle trams
        /// </summary>
        /// <returns>Een lijst met alle trams</returns>
        public List<Tram> GetAllTrams()
        {
            DatabaseController dc = new DatabaseController();
            trams = dc.GetAllTrams();
            return trams;
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
                if (sp.SpoorNummer == nummer)
                {
                    return sp;
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
            Update();
            foreach (Sector s in spoor.Sectoren)
            {
                if (s.Id == sectorID)
                {
                    return s;
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
                        if (s.Tram.TramNummer == tram.TramNummer)
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
                        if (s.Tram.TramNummer == tram.TramNummer)
                        {
                            return s;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Verkrijgt alle trams die te verplaatsen zijn.
        /// </summary>
        /// <returns>Retourneert een lijst met trams die verplaats kunnen worden</returns>
        public List<Tram> VerplaatsbareTrams()
        {
            List<Tram> verplaatsbaar = new List<Tram>();

            foreach (Tram tram in GetAllTrams())
            {
                if (tram.Status == TramStatus.InDienst)
                {
                    verplaatsbaar.Add(tram);
                }
            }

            foreach (Spoor spoor in GetAllSporen())
            {
                if (spoor.Sectoren[0].Tram != null)
                {
                    verplaatsbaar.Add(spoor.Sectoren[0].Tram);
                }
            }

            verplaatsbaar = verplaatsbaar.OrderBy(x => x.TramNummer).ToList(); //sorteert de lijst op basis van tramnummer, van laag naar hoog.
            return verplaatsbaar;
        }

        /// <summary>
        /// Verkrijgt alle vrijesporen
        /// </summary>
        /// <returns>Retourneert een lijst met vrije sporen</returns>
        public List<Spoor> VrijeSporen()
        {
            List<Spoor> vrij = new List<Spoor>();

            foreach (Spoor spoor in GetAllSporen()) //doorloopt alle sporen
            {
                if (spoor.SpoorStatus == SpoorStatus.Leeg) //wanneer een spoor de spoorstatus leeg heeft.
                {
                    vrij.Add(spoor);
                }
                else if (spoor.VrijeSectoren()) //verkrijgt alle sporen met vrijesectoren
                {
                    vrij.Add(spoor);
                }
            }

            vrij = vrij.OrderBy(x => x.SpoorNummer).ToList(); //sorteert de lijst op basis van spoornummer, van laag naar hoog.
            return vrij;
        }

        public bool VerwijderReservering(Reservering r)
        {
            DatabaseController dc = new DatabaseController();
            return dc.DeleteReservering(r.ID);
        }
    }
}
