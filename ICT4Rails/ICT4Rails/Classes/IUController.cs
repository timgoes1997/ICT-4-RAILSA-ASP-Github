using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    public class IUController
    {
        private List<Spoor> alleSporen;
        private List<Tram> alleTrams;
        //private Tram binnenrijdendeTram;


       /* public void deelTramIn(Tram tram)
        {
            DatabaseController dc = new DatabaseController();

            alleSporen = dc.GetAllSporen();

            int spoorCounter = 0;

            foreach (Spoor spoor in alleSporen)
            {
                foreach (Sector sector in spoor.Sectoren)
                {
                    if(sector.Status == SectorStatus.bezet)
                    {
                        // Als een spoor bezet is dan +1
                        spoorCounter += 1;
                    }
                }
            }

            if(spoorCounter == 0)
            {
                //Alle sporen zijn leeg
                tram.Status = 2;
                dc.UpdateTram(tram);
                alleSporen.First().Sectoren.First().Tram = tram;
                alleSporen.First().Sectoren.First().Status = SectorStatus.bezet;
                dc.UpdateSector(alleSporen.First().Sectoren.First());
                alleSporen.First().SpoorStatus = true;
                dc.UpdateSpoor(alleSporen.First());
                return;
            }
            else
            {
                foreach (Spoor spoor in alleSporen)
                {
                    //spoorStatus = true, spoor is bezet
                    // TODO: er zijn geen sporen die bezet zijn waar de tram op geplaatst kan worden, plaats m op een leeg spoor.
                    if(spoor.SpoorStatus == true)
                    {
                        foreach (Sector sector in spoor.Sectoren)
                        {
                            int sectorIndex = spoor.Sectoren.IndexOf(sector);
                            if(sector.Status == SectorStatus.bezet)
                            {
                                if (Convert.ToInt32(sector.Tram.VertrekTijd) >= Convert.ToInt32(tram.VertrekTijd))
                                {
                                    //De tram vertrekt eerder en kan er dus voor geplaatst worden
                                    if (spoor.Sectoren.Count > (sectorIndex + 1))
                                    {
                                        if (spoor.Sectoren[sectorIndex + 1].Status != SectorStatus.bezet)
                                        {
                                            tram.Status = 2;
                                            dc.UpdateTram(tram);
                                            spoor.Sectoren[sectorIndex + 1].Tram = tram;
                                            spoor.Sectoren[sectorIndex + 1].Status = SectorStatus.bezet;
                                            dc.UpdateSector(spoor.Sectoren[sectorIndex + 1]);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        sectorIndex = 0;
                                    }
                                }
                                //De tram vertrekt later dan de tram die er al staat, zoek een ander spoor
                            }
                        }
                    }
                }

                foreach (Spoor spoor in alleSporen)
                {
                        if(spoor.SpoorStatus == false)
                        {
                            spoor.Sectoren.First().Status = SectorStatus.bezet;
                            tram.Status = 2;
                            dc.UpdateTram(tram);
                            spoor.Sectoren.First().Tram = tram;
                            dc.UpdateSector(spoor.Sectoren.First());
                            spoor.SpoorStatus = true;
                            dc.UpdateSpoor(spoor);
                            return;
                        }                
                }
            }
        }*/

        /// <summary>
        /// Deelt de tram in op het spoor.
        /// </summary>
        /// <param name="tram"></param>
        public void DeelTramInBeter(Tram tram)
        {
            DatabaseController dc = new DatabaseController(); //Maakt een verbinding met de database via de databaseklasse.
            alleSporen = dc.GetAllSporen(); //Verkrijgt alle sporen.

            int controleer = ControleerTram(tram); //Controlleert of de tram nog moet worden schoongemaakt of gerepareerd.
            if (controleer == 2) //indien de tram nog moet worden gerepareert deel de tram dan in op het repartatiespoor.
            {
                DeelTramInOpReparatieSpoor(tram, dc); 
            }
            else if (controleer == 1) //indien de tram nog moet worden schoongemaakt deel de tram dan in op een schoonmaakspoor.
            {
                DeelTramInOpSchoonmaakSpoor(tram, dc);
            }
            else if(controleer == 0) //Deelt de tram in op het spoor volgens ons indelingsalgoritme.
            {
                DeelTramInVolgensAlgoritme(tram, dc);
            }
            else if(controleer == 3)
            {
                DeelTramInOpReservering(tram, dc);
            }
        }

        /// <summary>
        /// Deelt de tram in op een leeg reparatiespoor.
        /// </summary>
        /// <param name="tram">De tram die ingedeelt moet worden.</param>
        /// <param name="dc">De databasecontroller die verbinding maakt met de database</param>
        public void DeelTramInOpReparatieSpoor(Tram tram, DatabaseController dc)
        {
            Spoor repSpoor = VerkrijgVrijReparatieSpoor(); //Verkrijgt een leeg reparatie spoor.
            Sector legeSector = repSpoor.VerkrijgEerstLegeSector(); //Verkrijgt een lege sector op het verkregen schoonmaakspoor.
            if (legeSector != null) //indien legesector bestaat deel de tram dan in op die sector.           
            {
                legeSector.Tram = tram;
                legeSector.Status = SectorStatus.bezet;
                dc.UpdateSector(legeSector);
                tram.Status = TramStatus.Geparkeerd;
                dc.UpdateTram(tram);
            }
            else //wanneer legesector niet bestaat deel de tram dan in volgens het normaal indelingsalgoritme.
            {
                DeelTramInVolgensAlgoritme(tram, dc);
            }
        }

        /// <summary>
        /// Deelt de tram in op een leeg schoonmaakspoor.
        /// </summary>
        /// <param name="tram">De tram die ingedeeld moet worden</param>
        /// <param name="dc">De databasecontroller</param>
        public void DeelTramInOpSchoonmaakSpoor(Tram tram, DatabaseController dc)
        {
            Spoor repSpoor = VerkrijgVrijSchoonmaakSpoor(); //Verkrijgt een leeg schoonmaakspoor
            Sector legeSector = repSpoor.VerkrijgEerstLegeSector(); //Verkrijgt een lege sector op het verkregen schoonmaakspoor.
            if (legeSector != null) //indien legesector bestaat deel de tram dan in op die sector.           
            {
                legeSector.Tram = tram;
                legeSector.Status = SectorStatus.bezet;
                dc.UpdateSector(legeSector);
                repSpoor.SpoorStatus = SpoorStatus.InGebruik;
                dc.UpdateSpoor(repSpoor);
                tram.Status = TramStatus.Geparkeerd;
                dc.UpdateTram(tram);
            }
            else //wanneer legesector niet bestaat deel de tram dan in volgens het normaal indelingsalgoritme.
            {
                DeelTramInVolgensAlgoritme(tram, dc);
            }
        }

        /// <summary>
        /// Deelt de tram in op een spoor dat voldoet aan de eisen van ons algoritme.
        /// </summary>
        /// <param name="tram">De tram die ingedeelt moet worden</param>
        /// <param name="dc">De databasecontroller die aangemaakt wordt in de DeelTramBeterIn methode</param>
        public void DeelTramInVolgensAlgoritme(Tram tram, DatabaseController dc)
        {
            for (int x = 0; x < alleSporen.Count; x++) //doorloopt alle sporen.
            {
                if (alleSporen[x].SpoorStatus == SpoorStatus.InGebruik) //wanneer het spoor in gebruik is.
                {
                    for (int i = 0; i < alleSporen[x].Sectoren.Count; i++) //doorloopt alle sectoren van het spoor.
                    {

                        if (alleSporen[x].Sectoren[i].Status == SectorStatus.leeg && alleSporen[x].Sectoren[i].IsGereserveerdCheck(dc.GetAllReservering()) == false) //indien er nog een sector vrij is.
                        {
                            if (i - 1 != -1 && alleSporen[x].Sectoren[i - 1].Tram != null)
                            {
                                if (Convert.ToInt32(alleSporen[x].Sectoren[i - 1].Tram.VertrekTijd) <= Convert.ToInt32(tram.VertrekTijd)) //indien de vertrektijd van de laatste tram op het spoor vroeger is dan van de parameter tram.
                                {
                                    alleSporen[x].Sectoren[i].Status = SectorStatus.bezet;
                                    alleSporen[x].Sectoren[i].Tram = tram;
                                    dc.UpdateSector(alleSporen[x].Sectoren[i]); //de sector wordt geÜpdate
                                    tram.Status = TramStatus.Geparkeerd;
                                    dc.UpdateTram(tram); //de tram word geüpdate.
                                    return;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    alleSporen[x].SpoorStatus = SpoorStatus.InGebruik;
                    dc.UpdateSpoor(alleSporen[x]); //spoor wordt geüpdate.
                    alleSporen[x].Sectoren.First().Status = SectorStatus.bezet;
                    alleSporen[x].Sectoren.First().Tram = tram;
                    dc.UpdateSector(alleSporen[x].Sectoren.First()); //de sector wordt geÜpdate
                    tram.Status = TramStatus.Geparkeerd;
                    dc.UpdateTram(tram); //de tram word geüpdate.
                    return;
                }
            }
        }

        public void DeelTramInOpReservering(Tram tram, DatabaseController dc)
        {
            foreach (Reservering reservering in dc.GetAllReservering())
            {
                if(reservering.Tram.TramNummer == tram.TramNummer)
                {
                    foreach (Spoor spoor in alleSporen)
                    {
                        if(reservering.Sector.SpoorNummer == spoor.SpoorNummer && reservering.Sector.Status == SectorStatus.leeg)
                        {
                            spoor.SpoorStatus = SpoorStatus.InGebruik;
                            dc.UpdateSpoor(spoor);
                            reservering.Sector.Status = SectorStatus.bezet;
                            reservering.Sector.Tram = tram;
                            dc.UpdateSector(reservering.Sector);
                            tram.Status = TramStatus.Geparkeerd;
                            dc.UpdateTram(tram);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// verkrijgt een vrij spoor waar onderhoud voor een tram kan op gevoerd.
        /// </summary>
        /// <returns>Een vrij onderhoudsspoor</returns>
        public Spoor VerkrijgVrijReparatieSpoor()
        {
            DatabaseController dc = new DatabaseController();
            foreach (Spoor s in dc.GetAllSporen())
            {
                if (s.SpoorStatus == SpoorStatus.Leeg && s.spoorType == SpoorType.Reparatie && !s.SectorGeblokkeerd()) //Wanneer een spoor leeg is, er geen sector op geblokeerd wordt en het spoor van het type reparatie is, retourneer dan dat spoor.
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// Verkrijgt een vrij spoor waar een tram op schoongemaakt kan worden.
        /// </summary>
        /// <returns>Retourneert een leeg schoonmaak spoor</returns>
        public Spoor VerkrijgVrijSchoonmaakSpoor()
        {
            DatabaseController dc = new DatabaseController();
            foreach (Spoor s in dc.GetAllSporen())
            {
                if (s.SpoorStatus == SpoorStatus.Leeg && s.spoorType == SpoorType.Schoonmaak && !s.SectorGeblokkeerd()) //Wanneer een spoor leeg is, er geen sector op geblokeerd wordt en het spoor van het type schoonmaak is, retourneer dan dat spoor.
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// Controleer of de tram een onderhoudsbeurt nodig heeft.
        /// </summary>
        /// <param name="tram">De tram die wordt gecontroleerd</param>
        /// <returns></returns>
        public int ControleerTram(Tram tram)
        {
            ReparatieController rc = new ReparatieController();
            SchoonmaakController sc = new SchoonmaakController();
            BeheerController bc = new BeheerController();

            if (rc.TramControleerReparatie(tram)) //indien er nog een tram moet worden gerepareerd retourneer dan 2.
            {
                return 2;
                /* 
                 * if (sc.TramControleerSchoonmaak(tram))
                 * {
                 *     return 3;
                 * }*/
            }
            else if (sc.TramControleerSchoonmaak(tram)) //wanneer er nog een tram moet worden schoongemaakt retourneer dan 1.
            {
                return 1;
            }
            else if(bc.TramControleerReservering(tram))
            {
                return 3;
            }
            else //wanneer de tram geen onderhousbeurt nodig heeft retourneer dan 0.
            {
                return 0;
            }
        }
    }
}
