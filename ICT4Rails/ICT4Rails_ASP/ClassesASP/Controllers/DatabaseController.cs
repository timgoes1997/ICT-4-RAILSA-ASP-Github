using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using ICT4Rails.Scripts;
using System.Globalization;
using System.Web.Caching;

namespace ICT4Rails_ASP.ClassesASP
{
    public class DatabaseController
    {
        private string pcn = "ADMIN";
        private string wachtwoord = "ADMIN";

        private OracleConnection verbinding;

        public DatabaseController()
        {
            verbinding = new OracleConnection
            {
                ConnectionString =
                    "User Id=" + pcn + ";Password=" + wachtwoord + ";Data Source=" + "//172.20.128.1:1521/xe;"
            };
        }

        /// <summary>
        /// Retourneert een instantie van OracleCommand met
        /// this.Verbinding & .CommandType.Text
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public OracleCommand MaakOracleCommand(string sql)
        {
            OracleCommand command = new OracleCommand(sql, verbinding);
            command.CommandType = System.Data.CommandType.Text;

            return command;
        }

        /// <summary>
        /// Voert de query uit van meegegeven OracleCommand.
        /// Deze OracleCommand moet gemaakt zijn door MaakOracleCommand() en parameters moeten al ingesteld zijn.
        /// De teruggegeven lijst bevat voor elke rij een OracleDataReader.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OracleDataReader VoerQueryUit(OracleCommand command)
        {
            try
            {
                if (verbinding.State == ConnectionState.Closed)
                {
                    verbinding.Open();
                }

                OracleDataReader reader = command.ExecuteReader();

                return reader;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Voert de query uit van meegegeven OracleCommand
        /// Deze OracleCommand moet gemaakt zijn door MaakOracleCommand() en parameters moeten al ingesteld zijn.
        /// De output is of de query gelukt is.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool VoerNonQueryUit(OracleCommand command)
        {
            try
            {
                if (verbinding.State == ConnectionState.Closed)
                {
                    verbinding.Open();
                }

                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Import
        /// <summary>
        /// Deze methode verkrijgt een lijst met alle sporen, van ieder spoor krijgen we ook de sectoren. 
        /// </summary>
        /// <returns></returns>
        public List<Spoor> GetAllSporen(Remise r)
        {
            try
            {
                List<Spoor> sporen = new List<Spoor>();
                List<Sector> sectoren = GetAllSectoren(r);

                string sql = "SELECT * FROM SPOOR WHERE REMISE_ID = :Remise_ID";

                OracleCommand cmd = MaakOracleCommand(sql);

                cmd.Parameters.Add(":Remise_ID", r.ID);

                OracleDataReader reader = VoerQueryUit(cmd);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    int nummer = Convert.ToInt32(reader.GetValue(2));
                    int lengte = Convert.ToInt32(reader.GetValue(3));
                    bool beschikbaar = Convert.ToBoolean(reader.GetValue(4));
                    SpoorType type = (SpoorType)Convert.ToInt32(reader.GetValue(5));

                    Spoor spoor = new Spoor(id, r, nummer, lengte, beschikbaar, type);
                    foreach (Sector s in sectoren)
                    {
                        if (spoor.ID == s.SpoorID)
                        {
                            spoor.SectorToevoegen(s);
                        }
                    }

                    sporen.Add(spoor);
                }

                return sporen;
            }
            catch
            {
                throw new NoSporenFoundException("Geen sporen gevonden in DatabaseController: GetAllSporen");
            }
            finally
            {
                verbinding.Close();
            }
        }

        /// <summary>
        /// Verkrijgt alle trams uit de database en stopt die allemaal in een lijst bestaand uit het object Tram.
        /// </summary>
        /// <returns></returns>
        public List<Tram> GetAllTrams(Remise r)
        {
            try
            {
                List<Tram> result = new List<Tram>();

                string sql = "SELECT * FROM TRAM WHERE REMISE_ID_STANDPLAATS = :Remise_ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":Remise_ID", r.ID);

                OracleDataReader reader = VoerQueryUit(command);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    int remise = Convert.ToInt32(reader.GetValue(1));
                    TramType tramtype = (TramType)Convert.ToInt32(reader.GetValue(2));
                    DateTime vertrektijd;
                    if (reader.GetValue(3) != DBNull.Value)
                    {
                        vertrektijd = Convert.ToDateTime(reader.GetValue(3));
                    }
                    else
                    {
                        vertrektijd = DateTime.Now;
                    }
                    int tramnummer = Convert.ToInt32(reader.GetValue(4));
                    int lengte = Convert.ToInt32(reader.GetValue(5));
                    string opmerking = reader.GetValue(6).ToString();
                    bool vervuild = Convert.ToBoolean(reader.GetValue(7));
                    bool defect = Convert.ToBoolean(reader.GetValue(8));
                    bool conducteurGeschikt = Convert.ToBoolean(reader.GetValue(9));
                    bool beschikbaar = Convert.ToBoolean(reader.GetValue(10));

                    result.Add(new Tram(id, r, tramtype, vertrektijd, tramnummer, lengte, opmerking, vervuild, defect, conducteurGeschikt, beschikbaar));
                }

                return result;
            }
            catch
            {
                return null;
            }
            finally
            {
                verbinding.Close();
            }
        }

        public List<Remise> GetAllRemise()
        {
            try
            {
                List<Remise> r = new List<Remise>();
                string sql = "SELECT * FROM REMISE";

                OracleCommand command = MaakOracleCommand(sql);

                OracleDataReader reader = VoerQueryUit(command);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    string naam = Convert.ToString(reader.GetValue(1));
                    int groteServiceBeurtenPerDag = Convert.ToInt32(reader.GetValue(2));
                    int kleineServiceBeurtenPerDag = Convert.ToInt32(reader.GetValue(3));
                    int groteSchoonmaakBeurtenPerDag = Convert.ToInt32(reader.GetValue(4));
                    int kleineSchoonmaakBeurtenPerDag = Convert.ToInt32(reader.GetValue(5));

                    r.Add(new Remise(id, naam, kleineSchoonmaakBeurtenPerDag, groteSchoonmaakBeurtenPerDag, kleineServiceBeurtenPerDag, 0, groteServiceBeurtenPerDag));
                }

                return r;
            }
            catch
            {
                return null;
            }
            finally
            {
                verbinding.Close();
            }
        }

        public Tram VerkrijgTram(List<Tram> trams, int id)
        {
            foreach (Tram tram in trams)
            {
                if (tram.ID == id)
                {
                    return tram;
                }
            }
            return null;
        }

        public Sector VerkrijgSector(List<Sector> sectoren, int id)
        {
            foreach (Sector sector in sectoren)
            {
                if (sector.ID == id)
                {
                    return sector;
                }
            }
            return null;
        }

        public Spoor VerkrijgSpoor(List<Spoor> sporen, int id)
        {
            foreach (Spoor s in sporen)
            {
                if (s.ID == id)
                {
                    return s;
                }
            }
            return null;
        }

        public List<Reservering> GetAllReservering(Remise r)
        {
            try
            {
            List<Reservering> reserveringen = new List<Reservering>();
                string sql = "SELECT * FROM RESERVERING";

                List<Tram> trams = GetAllTrams(r);
                List<Spoor> sporen = GetAllSporen(r);

                OracleCommand cmd = MaakOracleCommand(sql);
                OracleDataReader reader = VoerQueryUit(cmd);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    Tram tram = VerkrijgTram(trams, Convert.ToInt32(reader.GetValue(1)));
                    Spoor spoor = VerkrijgSpoor(sporen, Convert.ToInt32(reader.GetValue(2)));

                    Reservering reservering = new Reservering(id, tram, spoor);
                    reserveringen.Add(reservering);
                }

                return reserveringen;
            }
            catch
            {
                return null;
            }
            finally
            {
                verbinding.Close();
            }
        }

        public List<Sector> GetAllSectoren(Remise r) //TODO: Innerjoin met spoor om te kijken bij welke remise deze sector hoort.
        {
            try
            {
                List<Sector> sectoren = new List<Sector>();
                List<Tram> trams = GetAllTrams(r);
                string sql = "SELECT * FROM SECTOR";

                OracleCommand cmd = MaakOracleCommand(sql);
                OracleDataReader reader = VoerQueryUit(cmd);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    int spoorNummer = Convert.ToInt32(reader.GetValue(1));
                    Tram tram;
                    if (reader.GetValue(2) != DBNull.Value)
                    {
                        tram = VerkrijgTram(trams, Convert.ToInt32(reader.GetValue(2)));
                    }
                    else
                    {
                        tram = null;
                    }
                    int nummer = Convert.ToInt32(reader.GetValue(3));
                    bool beschikbaar = Convert.ToBoolean(reader.GetValue(4));
                    bool geblokkeerd = Convert.ToBoolean(reader.GetValue(5));

                    Sector sector = new Sector(id, tram, spoorNummer, nummer, beschikbaar, geblokkeerd);
                    sectoren.Add(sector);
                }

                return sectoren;
            }
            catch
            {
                return null;
            }
            finally
            {
                verbinding.Close();
            }
        }

        public List<Onderhoud> GetAllOnderhoud(Remise r)
        {
            try
            {
                List<Onderhoud> onderhoudList = new List<Onderhoud>();
                List<Tram> trams = GetAllTrams(r);
                string sql = "SELECT * FROM TRAM_ONDERHOUD"; // Moet nog worden ingevuld.

                OracleCommand cmd = MaakOracleCommand(sql);
                OracleDataReader reader = VoerQueryUit(cmd);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    int adid = Convert.ToInt32(reader.GetValue(1));
                    Tram tram = VerkrijgTram(trams, Convert.ToInt32(reader.GetValue(2)));
                    DateTime tijdstip = Convert.ToDateTime(reader.GetValue(3));
                    DateTime beschikbaarDatum = Convert.ToDateTime(reader.GetValue(4));
                    TypeOnderhoud typeOnderhoud = (TypeOnderhoud)Convert.ToInt32(reader.GetValue(5));

                    Onderhoud o = new Onderhoud(id, adid, tram, tijdstip, beschikbaarDatum, typeOnderhoud);
                    onderhoudList.Add(o);

                }

                return onderhoudList;
            }
            finally
            {
                verbinding.Close();
            }
        }

        public List<Lijn> GetAllLijnen(Remise r)
        {
            try
            {
                List<Lijn> lijnen = new List<Lijn>();
                string sql = "SELECT * FROM LIJN WHERE REMISE_ID = :Remise_ID";

                OracleCommand cmd = MaakOracleCommand(sql);

                cmd.Parameters.Add(":Remise_ID", r.ID);

                OracleDataReader reader = VoerQueryUit(cmd);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    int nummer = Convert.ToInt32(reader.GetValue(2));
                    bool conducteurRijdtMee = Convert.ToBoolean(reader.GetValue(3));
                    List<Tram> trams = null;
                    //GetAllTramsFromLijn(id);

                    Lijn lijn = new Lijn(id, r, nummer, conducteurRijdtMee, trams);
                    lijnen.Add(lijn);
                }

                foreach (Lijn l in lijnen)
                {
                    l.Trams = GetAllTramsFromLijn(l.ID, r);
                }

                return lijnen;
            }
            catch
            {
                return null;
            }
            finally
            {
                verbinding.Close();
            }
        }

        public List<Tram> GetAllTramsFromLijn(int lijnID, Remise r)
        {
            try
            {
                List<Tram> trams = new List<Tram>();
                List<int> tramIds = new List<int>();

                string sql = "SELECT TRAM_ID FROM TRAM_LIJN WHERE LIJN_ID = :ID";
                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":ID", lijnID);

                OracleDataReader reader = VoerQueryUit(command);

                while (reader.Read())
                {
                    int tramid = Convert.ToInt32(reader.GetValue(0));
                    tramIds.Add(tramid);
                }

                List<Tram> allTrams = GetAllTrams(r);
                foreach (int id in tramIds)
                {
                    Tram tram = VerkrijgTram(allTrams, id);
                    trams.Add(tram);
                }

                return trams;
            }
            catch
            {
                return null;
            }
            finally
            {
                verbinding.Close();
            }
        }

        public List<Verbinding> GetAllVerbindingen(Remise r)
        {
            try
            {
                List<Verbinding> verbindingen = new List<Verbinding>();
                string sql = "SELECT * FROM VERBINDING";

                OracleCommand cmd = MaakOracleCommand(sql);
                OracleDataReader reader = VoerQueryUit(cmd);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    Sector van = VerkrijgSector(GetAllSectoren(r), Convert.ToInt32(reader.GetValue(1)));
                    Sector naar = VerkrijgSector(GetAllSectoren(r), Convert.ToInt32(reader.GetValue(2)));

                    Verbinding lijnverbinding = new Verbinding(id, van, naar);
                    verbindingen.Add(lijnverbinding);
                }

                return verbindingen;
            }
            catch
            {
                return null;
            }
            finally
            {
                verbinding.Close();
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Update alle tram waarden in de database voor het gegeven tram id.
        /// </summary>
        /// <param name="tram">De tram die moet worden geüpdate</param>
        /// <returns></returns>
        public bool UpdateTram(Tram tram)
        {
            try
            {
                string sql = "UPDATE TRAM SET Remise_ID_Standplaats = :Remise_ID, Tramtype_ID = :Tramtype_ID, Vertrektijd = :Vertrektijd, Nummer = :Nummer,"
                    + " Lengte = :Lengte, Status = :Status, Vervuild = :Vervuild, Defect = :Defect, ConducteurGeschikt = :ConducteurGeschikt, Beschikbaar = :Beschikbaar"
                    + " WHERE ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":Remise_ID", tram.Remise.ID);
                command.Parameters.Add(":Tramtype_ID", Convert.ToInt32(tram.TramType));
                command.Parameters.Add(":Vertrektijd", Convert.ToDateTime(tram.Vertrektijd));
                command.Parameters.Add(":Nummer", tram.TramNummer);
                command.Parameters.Add(":Lengte", tram.Lengte);
                command.Parameters.Add(":Status", tram.Opmerking);
                command.Parameters.Add(":Vervuild", Convert.ToInt32(tram.Vervuild));
                command.Parameters.Add(":Defect", Convert.ToInt32(tram.Defect));
                command.Parameters.Add(":ConducteurGeschikt", Convert.ToInt32(tram.ConducteurGeschikt));
                command.Parameters.Add(":Beschikbaar", Convert.ToInt32(tram.Beschikbaar));
                command.Parameters.Add(":ID", tram.ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        /// <summary>
        /// Update alle spoor waarden in de database voor het gegeven spoor id.
        /// </summary>
        /// <param name="spoor">Het spoor dat moet worden geüpdate</param>
        /// <returns></returns>
        public bool UpdateSpoor(Spoor spoor)
        {
            try
            {
                string sql = "UPDATE SPOOR SET Remise_ID = :Remise_ID, Nummer = :Nummer, Lengte = :Lengte, Beschikbaar = :Beschikbaar, InUitRijspoor = :InUitRijspoor"
                    + " WHERE ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":Remise_ID", spoor.Remise.ID);
                command.Parameters.Add(":Nummer", spoor.Nummer);
                command.Parameters.Add(":Lengte", spoor.Lengte);
                command.Parameters.Add(":Beschikbaar", Convert.ToInt32(spoor.Beschikbaar));
                command.Parameters.Add(":InUitRijspoor", Convert.ToInt32(spoor.SpoorType));
                command.Parameters.Add(":ID", spoor.ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        /// <summary>
        /// Update alle sector waarden in de database voor het gegeven sector id.
        /// </summary>
        /// <param name="sector">Het sector dat moet worden geüpdate</param>
        /// <returns></returns>
        public bool UpdateSector(Sector sector, Remise r)
        {
            try
            {
                string sql = "UPDATE SECTOR SET Spoor_ID = :Spoor_ID, Tram_ID = :Tram_ID, Nummer = :Nummer, Beschikbaar = :Beschikbaar, Blokkade = :Blokkade"
                    + " WHERE ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":Spoor_ID", VerkrijgSpoorSector(GetAllSporen(r), sector).ID);
                if (sector.Tram != null)
                {
                    command.Parameters.Add(":Tram_ID", sector.Tram.ID);
                }
                else
                {
                    command.Parameters.Add(":Tram_ID", DBNull.Value);
                }
                command.Parameters.Add(":Nummer", sector.Nummer);
                command.Parameters.Add(":Beschikbaar", Convert.ToInt32(sector.Beschikbaar));
                command.Parameters.Add(":Blokkade", Convert.ToInt32(sector.Geblokkeerd));
                command.Parameters.Add(":ID", sector.ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        /// <summary>
        /// Update de lijn en de huidige trams op de lijn.
        /// </summary>
        /// <param name="lijn"></param>
        /// <returns></returns>
        public bool UpdateLijn(Lijn lijn)
        {
            try
            {
                string sql = "UPDATE LIJN SET Remise_ID = :Remise_ID, Nummer = :Nummer, ConducteurRijdtMee = :ConducteurRijdtMee"
                    + " WHERE ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":Remise_ID", lijn.Remise.ID);
                command.Parameters.Add(":Nummer", lijn.Nummer);
                command.Parameters.Add(":ConducteurRijdtMee", lijn.ConducteurRijdtMee);
                command.Parameters.Add(":ID", lijn.ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        public Spoor VerkrijgSpoorSector(List<Spoor> sporen, Sector sector)
        {
            foreach (Spoor spoor in sporen)
            {
                foreach (Sector sec in spoor.Sectoren)
                {
                    if (sec.ID == sector.ID)
                    {
                        return spoor;
                    }
                }
            }
            return null;
        }
        #endregion

        #region Insert

        /// <summary>
        /// Voegt een nieuwe tram toe aan de database.
        /// </summary>
        /// <param name="tram">De tram die aan de database moet worden toegevoegd</param>
        /// <returns>Of de tram is toegevoegd of niet</returns>
        public bool AddTram(Tram tram)
        {
            try
            {
                string sql = "INSERT INTO TRAM (Remise_ID_Standplaats, Tramtype_ID, Vertrektijd, Nummer, Lengte, Status, Vervuild, Defect, ConducteurGeschikt, Beschikbaar) "
                    + "VALUES (:Remise_ID, :Tramtype_ID, :Vertrektijd, :Nummer, :Lengte, :Status, :Vervuild, :Defect, :ConducteurGeschikt, :Beschikbaar)";

                OracleCommand command = MaakOracleCommand(sql);
                
                command.Parameters.Add(":Remise_ID", tram.Remise.ID);
                command.Parameters.Add(":Tramtype_ID", (Convert.ToInt32(tram.TramType) + 1));
                command.Parameters.Add(":Vertrektijd", DateTime.Now); //tram.Vertrektijd
                command.Parameters.Add(":Nummer", tram.TramNummer);
                command.Parameters.Add(":Lengte", tram.Lengte);
                command.Parameters.Add(":Status", tram.Opmerking);
                command.Parameters.Add(":Vervuild", Convert.ToInt32(tram.Vervuild));
                command.Parameters.Add(":Defect", Convert.ToInt32(tram.Defect));
                command.Parameters.Add(":ConducteurGeschikt", Convert.ToInt32(tram.ConducteurGeschikt));
                command.Parameters.Add(":Beschikbaar", Convert.ToInt32(tram.Beschikbaar));

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        
}

        /// <summary>
        /// Voegt een reservering toe aan de database.
        /// </summary>
        /// <param name="res">De reservering die moet worden toegevoegd</param>
        /// <returns>Of de reservering is toegevoegd of niet</returns>
        public bool AddReservering(Reservering res)
        {
            try
            {
                //Todo nextval?
                string sql = "INSERT INTO RESERVERING ( Tram_ID, Spoor_ID ) VALUES ( :Tram_ID, :Spoor_ID )";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":Tram_ID", res.Tram.ID);
                command.Parameters.Add(":Spoor_ID", res.Spoor.ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        /// <summary>
        /// Voegt een onderhoudsbeurt toe aan de database
        /// </summary>
        /// <param name="sch">De onderhoud die moet worden toegevoegd</param>
        /// <returns>Of onderhoud is toegevoegt aan de database of niet.</returns>
        public bool AddOnderhoud(Onderhoud ond)
        {
            try
            {
                //Todo nextval?
                string sql = "INSERT INTO TRAM_ONDERHOUD ( Medewerker_ID, Tram_ID, DatumTijdstip, BeschikbaarDatum, TypeOnderhoud ) VALUES ( :Medewerker_ID, :Tram_ID, :DatumTijdstip, :BeschikbaarDatum, :TypeOnderhoud )";

                OracleCommand command = MaakOracleCommand(sql);
                
                command.Parameters.Add(":Medewerker_ID", ond.AdID);
                command.Parameters.Add(":Tram_ID", ond.Tram.ID);
                command.Parameters.Add(":DatumTijdstip", ond.Tijdstip);
                command.Parameters.Add(":BeschikbaarDatum", ond.BeschikbaarDatum);
                command.Parameters.Add(":TypeOnderhoud", Convert.ToInt32(ond.TypeOnderhoud));

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        /// <summary>
        /// Voegt een lijn toe aan de database
        /// </summary>
        /// <param name="sch">De lijn die moet worden toegevoegd</param>
        /// <returns>Of lijn is toegevoegt aan de database of niet.</returns>
        public bool AddLijn(Lijn lijn)
        {
            try
            {
                string sql = "INSERT INTO LIJN ( Remise_ID, Nummer, ConducteurRijdtMee ) VALUES ( :Remise_ID, :Nummer, :ConducteurRijdtMee )";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":Remise_ID", lijn.Remise.ID);
                command.Parameters.Add(":Nummer", lijn.Nummer);
                command.Parameters.Add(":ConducteurRijdtMee", lijn.ConducteurRijdtMee);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        public bool AddTramLijn(Lijn lijn, Tram tram)
        {
            try
            {
                string sql = "INSERT INTO TRAM_LIJN ( Tram_ID, Lijn_ID ) VALUES ( :Tram_ID, :Lijn_ID )";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":Tram_ID", tram.ID);
                command.Parameters.Add(":Lijn_ID", lijn.ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }
        #endregion

        #region Delete

        /// <summary>
        /// Verwijdert een tram uit de database
        /// </summary>
        /// <param name="ID">Nummer of identifier van het tram object.</param>
        /// <returns>Of de verwijdering goed is uitgevoerd</returns>
        public bool DeleteTram(Tram tram)
        {
            try
            {
                string sql = "DELETE FROM TRAM WHERE ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":ID", tram.ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        /// <summary>
        /// Verwijdert onderhoud uit de datbase
        /// </summary>
        /// <param name="ID">Nummer of identifier van het onderhoud object.</param>
        /// <returns>Of de verwijdering goed is uitgevoerd</returns>
        public bool DeleteOnderhoud(int ID)
        {
            try
            {
                string sql = "DELETE FROM TRAM_ONDERHOUD WHERE ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":ID", ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        /// <summary>
        /// Verwijdert reservering uit de datbase
        /// </summary>
        /// <param name="ID">Nummer of identifier van het reservering object.</param>
        /// <returns>Of de verwijdering goed is uitgevoerd</returns>
        public bool DeleteReservering(int ID)
        {
            try
            {
                string sql = "DELETE FROM RESERVERING WHERE ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":ID", ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        /// <summary>
        /// Verwijdert lijn uit de datbase
        /// </summary>
        /// <param name="ID">Nummer of identifier van het lijn object.</param>
        /// <returns>Of de verwijdering goed is uitgevoerd</returns>
        public bool DeleteLijn(int ID)
        {
            try
            {
                string sql = "DELETE FROM LIJN WHERE ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":ID", ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }

        /// <summary>
        /// Verwijdert een tram van een lijn uit de database
        /// </summary>
        /// <param name="ID">Nummer of identifier van het lijn object.</param>
        /// <returns>Of de verwijdering goed is uitgevoerd</returns>
        public bool DeleteTramLijn(int ID)
        {
            try
            {
                string sql = "DELETE FROM TRAM_LIJN WHERE TL_ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":ID", ID);

                return VoerNonQueryUit(command);
            }
            catch
            {
                return false;
            }
            finally
            {
                verbinding.Close();
            }
        }
        #endregion

    }
}