using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ICT4Rails_ASP.ClassesASP;
using System.Collections.Generic;

namespace UnitTestDatabaseTestProject
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void VerkrijgAlleLijsten()
        {
            DatabaseController db = new DatabaseController();
            List<Remise> remise = db.GetAllRemise();
            List<Spoor> sporen = db.GetAllSporen(remise[0]);
            List<Tram> trams = db.GetAllTrams(remise[0]);
            List<Tram> tramsLijn = db.GetAllTramsFromLijn(1, remise[0]);
            List<Lijn> lijn = db.GetAllLijnen(remise[0]);

            //nog geen dummy data, dus nog niet volledig getest.
            List<Reservering> reservering = db.GetAllReservering(remise[0]);
            List<Sector> sectoren = db.GetAllSectoren(remise[0]);
            List<Onderhoud> onderhoud = db.GetAllOnderhoud(remise[0]);
            List<Verbinding> transfers = db.GetAllVerbindingen(remise[0]);
        }

        
        [TestMethod]
        public void LijstBeheercontroller()
        {
            BeheerController bc = new BeheerController();
            OnderhoudController oc = new OnderhoudController(bc.HuidigeRemise);

            List<Sector> geblokkeerd = bc.GetAllGeblokkeerdeSectoren();
            List<Sector> nietGeblokkeerd = bc.GetAllNietGeblokkeerdeSectoren();
            List<Tram> viezeTrams = oc.GetViezeTrams();
            List<Tram> defecteTrams = oc.GetDefecteTrams();
            //List<Tram> tram = bc.BeschikbareTrams();
            List<Tram> trams = bc.GetAllTrams();


        //    List<Sector> sectoren = db.GetAllSectoren();
        //    List<Onderhoud> onderhoud = db.GetAllOnderhoud();
        //    List<Verbinding> transfers = db.GetAllVerbindingen();
        }

        [TestMethod]
        public void UpdateTestMethode()
        {
            BeheerController bc = new BeheerController();
            List<Tram> trams = bc.GetAllTrams();
            trams[1].Defect = !trams[1].Defect;
            //bc.TramStatusAanpassen(trams[1]);

            List<Spoor> sporen = bc.GetAllSporen();
            sporen[0].Sectoren[0].Tram = trams[1];
            DatabaseController dc = new DatabaseController();
            dc.UpdateSector(sporen[0].Sectoren[0], bc.HuidigeRemise);

            //    List<Sector> sectoren = db.GetAllSectoren();
            //    List<Onderhoud> onderhoud = db.GetAllOnderhoud();
            //    List<Verbinding> transfers = db.GetAllVerbindingen();
        }
    }
}
