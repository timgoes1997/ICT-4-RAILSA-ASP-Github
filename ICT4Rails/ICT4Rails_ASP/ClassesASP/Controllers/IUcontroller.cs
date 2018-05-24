using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICT4Rails_ASP.ClassesASP
{
    public delegate void DeelTramIn(Tram tram, DatabaseController dc);

    public class IUcontroller
    {
        private static Remise huidigeRemise;
        public static Remise HuidigeRemise { get { return huidigeRemise; } set { huidigeRemise = value; } }

        private static List<Spoor> alleSporen;

        public static DeelTramIn[] IndelingMethodes =
        {
            DeelTramInOpOnderhoudsSpoor,
            DeelTramInOpReservering,
            DeelTramInVolgensAlgoritme
        };

        static IUcontroller()
        {
            DatabaseController dc = new DatabaseController();
            huidigeRemise = dc.GetAllRemise()[0];
        }

        /// <summary>
        /// Deelt de tram in op het spoor.
        /// </summary>
        /// <param name="tram"></param>
        public static void DeelTramInBeter(Tram tram)
        {
            if (tram == null)
            {
                throw new InUitRijException("U probeert een leeg tram object in te delen");
            }
            DatabaseController dc = new DatabaseController(); //Maakt een verbinding met de database via de databaseklasse.
            alleSporen = dc.GetAllSporen(huidigeRemise); //Verkrijgt alle sporen.

            DeelTramIn deelTramIn = IndelingMethodes[ControleerTram(tram)]; //Voegt een methode toe aan de delegate afhankelijk van de stauts van de tram.
            
            deelTramIn(tram, dc); //Voert DeelTramInOpOnderhoudsSpoor of DeelTramInVolgensAlgoritme afhankelijk van de status van de Tram.
        }

        /// <summary>
        /// Deelt de tram in op een vrij onderhoudsspoor.
        /// </summary>
        /// <param name="tram"></param>
        /// <param name="dc"></param>
        public static void DeelTramInOpOnderhoudsSpoor(Tram tram, DatabaseController dc) //ToDo: Rekening houden met verbinding.
        {
            OnderhoudController oc = new OnderhoudController(huidigeRemise);
            Onderhoud onderhoud = oc.GetOnderhoud(tram);
            Spoor spoor = oc.GetVrijOnderhoudsSpoor();
            if (spoor != null)
            {
                Sector sector = spoor.GetEersteSector();
                sector.PlaatsTram(tram);

                if (sector.ID == spoor.Sectoren[(spoor.Sectoren.Count - 1)].ID)
                {
                    spoor.Beschikbaar = false;
                }

                dc.UpdateSpoor(spoor);
                dc.UpdateSector(sector, huidigeRemise);
                dc.UpdateTram(sector.Tram);
            }
            else
            {
                DeelTramInVolgensAlgoritme(tram, dc);
            }
        }

        /// <summary>
        /// Deelt de tram in op een vrij onderhoudsspoor.
        /// </summary>
        /// <param name="tram"></param>
        /// <param name="dc"></param>
        public static void DeelTramInOpReservering(Tram tram, DatabaseController dc) //ToDo: Rekening houden met verbinding.
        {
            BeheerController bc = new BeheerController();
            Reservering reservering = bc.GetReservering(tram);

            foreach (Spoor spoor in alleSporen)
            {
                if (reservering.Spoor.ID == spoor.ID)
                {
                    Sector sector = spoor.GetEersteSector();
                    if (sector == null)
                    {
                        DeelTramInVolgensAlgoritme(tram, dc);
                        return;
                    }
                    else
                    {
                        sector.PlaatsTram(tram);
                        if (sector.ID == spoor.Sectoren[(spoor.Sectoren.Count - 1)].ID)
                        {
                            spoor.Beschikbaar = false;
                        }
                        tram.Beschikbaar = true;
                        dc.UpdateSpoor(spoor);
                        dc.UpdateTram(tram);
                        dc.UpdateSector(sector, huidigeRemise);
                        dc.DeleteReservering(reservering.ID);
                    }
                }
            }
        }

        /// <summary>
        /// Deelt de tram in volgens het normaal algrotime.
        /// </summary>
        /// <param name="tram"></param>
        /// <param name="dc"></param>
        public static void DeelTramInVolgensAlgoritme(Tram tram, DatabaseController dc) //ToDo: Rekening houden met verbinding.
        {
            for (int x = 0; x < alleSporen.Count; x++)
            {
                if (alleSporen[x].Beschikbaar && alleSporen[x].SpoorType == SpoorType.Normaal) //TODO: spoor op niet beschikbaar zetten wanneer de laatste sector word gewijzigt naar niet beschikbaar of geblokkeerd.
                {
                    for (int i = 0; i < alleSporen[x].Sectoren.Count; i++)
                    {
                        if (alleSporen[x].Sectoren[i].Beschikbaar && !alleSporen[x].Sectoren[i].Geblokkeerd && alleSporen[x].GenoegVrijeSporen())
                        {
                            //Kijken of vertrektijd met vorige kan
                            //Kijken of vertrektijd met reservering kan
                            if (i - 1 != -1 && alleSporen[x].Sectoren[i - 1].Tram != null)
                            {
                                if (alleSporen[x].Sectoren[i - 1].Tram.Vertrektijd <= tram.Vertrektijd)
                                {
                                    //plaats tram, TODO: Aparte MEthode volgende stuk
                                    alleSporen[x].Sectoren[i].PlaatsTram(tram);
                                    if (alleSporen[x].Sectoren[i].ID == alleSporen[x].Sectoren[(alleSporen[x].Sectoren.Count - 1)].ID)
                                    {
                                        alleSporen[x].Beschikbaar = false;
                                    }
                                    dc.UpdateSpoor(alleSporen[x]);
                                    dc.UpdateSector(alleSporen[x].Sectoren[i], huidigeRemise);
                                    dc.UpdateTram(alleSporen[x].Sectoren[i].Tram);
                                    return;
                                }
                            }
                            else if (i - 1 == -1)
                            {
                                //plaats tram, TODO: Aparte MEthode volgende stuk
                                alleSporen[x].Sectoren[i].PlaatsTram(tram);
                                if (alleSporen[x].Sectoren[i].ID == alleSporen[x].Sectoren[(alleSporen[x].Sectoren.Count - 1)].ID)
                                {
                                    alleSporen[x].Beschikbaar = false;
                                }
                                dc.UpdateSpoor(alleSporen[x]);
                                dc.UpdateSector(alleSporen[x].Sectoren[i], huidigeRemise);
                                dc.UpdateTram(alleSporen[x].Sectoren[i].Tram);
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Controleer of de tram een onderhoudsbeurt nodig heeft.
        /// </summary>
        /// <param name="tram">De tram die wordt gecontroleerd</param>
        /// <returns></returns>
        public static int ControleerTram(Tram tram)
        {
            OnderhoudController oc = new OnderhoudController(huidigeRemise);
            BeheerController bc = new BeheerController();

            if (oc.TramControleerOnderhoud(tram))
            {
                return 0;
            }
            else if (bc.TramControleerReservering(tram))
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}