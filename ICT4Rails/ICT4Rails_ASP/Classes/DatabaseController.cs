using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using ICT4Rails.Scripts;

namespace ICT4Rails.Scripts
{
    public class DatabaseController
    {
        private string pcn = "dbi324201";
        private string wachtwoord = "WczifwDM2x";

        private OracleConnection verbinding;

        public DatabaseController()
        {
            verbinding = new OracleConnection
            {
                ConnectionString =
                    "User Id=" + pcn + ";Password=" + wachtwoord + ";Data Source=" + "//192.168.15.50:1521/fhictora;"
            };


            // Testen of dm werkt
            /*
            OracleCommand command = MaakOracleCommand("SELECT * FROM DUAL");

            OracleDataReader reader = VoerQueryUit(command);

            string s = reader[0].ToString();
            */
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

        #region ImportQueries

        /// <summary>
        /// Deze methode verkrijgt een lijst met alle sporen, van ieder spoor krijgen we ook de sectoren. 
        /// </summary>
        /// <returns></returns>
        public List<Spoor> GetAllSporen()
        {
            try
            {
                List<Spoor> sporen = new List<Spoor>();

                string sql = "SELECT * FROM SPOOR";

                OracleCommand command = MaakOracleCommand(sql);

                OracleDataReader reader = VoerQueryUit(command);

                while (reader.Read())
                {
                    int spoornummer = Convert.ToInt32(reader.GetValue(0));
                    int spoorstatus = Convert.ToInt32(reader.GetValue(1));
                    SpoorType spoortype = (SpoorType)Convert.ToInt32(reader.GetValue(2));

                    sporen.Add(new Spoor(spoornummer, spoorstatus, spoortype));
                }

                List<Sector> sectoren = GetAllSectoren();

                //Voegt alle sectoren toe aan het juiste spoor.
                foreach (Sector sector in sectoren)
                {
                    foreach (Spoor spoor in sporen)
                    {
                        if (sector.SpoorNummer == spoor.SpoorNummer)
                        {
                            spoor.SectorToevoegen(sector);
                        }
                    }
                }

                return sporen;
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


        /// <summary>
        /// Verkrijgt alle sectoren vanuit de database en stopt die uit een lijst bestaand uit het object Sector.
        /// </summary>
        /// <returns></returns>
        private List<Sector> GetAllSectoren()
        {
            try
            {
                List<Tram> trams = GetAllTrams();
                List<Sector> result = new List<Sector>();

                string sql = "SELECT * FROM SECTOR";

                OracleCommand command = MaakOracleCommand(sql);

                OracleDataReader reader = VoerQueryUit(command);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));

                    //Verkrijgt het tramID, indien deze null is wordt het tramid naar 0 gezet.
                    Tram tram;
                    if (reader.GetValue(1) == DBNull.Value)
                    {
                        tram = null;
                    }
                    else
                    {
                        tram = VerkrijgTram(trams, Convert.ToInt32(reader.GetValue(1)));
                    }

                    int spoornummer = Convert.ToInt32(reader.GetValue(2));
                    SectorStatus status = (SectorStatus)Convert.ToInt32(reader.GetValue(3));

                    result.Add(new Sector(id, tram, spoornummer, status));
                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Verkrijgt alle trams uit de database en stopt die allemaal in een lijst bestaand uit het object Tram.
        /// </summary>
        /// <returns></returns>
        public List<Tram> GetAllTrams()
        {
            try
            {
                List<Tram> result = new List<Tram>();

                string sql = "SELECT * FROM TRAM";

                OracleCommand command = MaakOracleCommand(sql);

                OracleDataReader reader = VoerQueryUit(command);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    TramType tramtype = (TramType)Convert.ToInt32(reader.GetValue(1));
                    int tramnummer = Convert.ToInt32(reader.GetValue(2));
                    string vertrektijd = reader.GetValue(3).ToString();
                    TramStatus status = (TramStatus)Convert.ToInt32(reader.GetValue(4));
                    string status_opmerking = reader.GetValue(5).ToString();

                    result.Add(new Tram(id, tramtype, tramnummer, vertrektijd, status, status_opmerking));
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

        /// <summary>
        /// Verkrijgt een lijst met alle trams die moeten worden schoongemaakt.
        /// </summary>
        public List<Schoonmaak> GetAllSchoonmaak()
        {
            try
            {
                List<Tram> trams = GetAllTrams();
                List<Schoonmaak> schoonmaak = new List<Schoonmaak>();

                string sql = "SELECT * FROM ONDERHOUD WHERE TypeOnderhoud = '" + "Schoonmaak " + "'";

                OracleCommand command = MaakOracleCommand(sql);

                OracleDataReader reader = VoerQueryUit(command);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    Tram tram = VerkrijgTram(trams, Convert.ToInt32(reader.GetValue(1)));
                    SchoonmaakType s = (SchoonmaakType)Convert.ToInt32(reader.GetValue(3));
                    string opmerking = Convert.ToString(reader.GetValue(4));
                    DateTime invoerDatum = DateTime.Now; 
                    //Convert.ToDateTime(reader.GetValue(5));
                    bool bevestigd = Convert.ToBoolean(reader.GetValue(6));

                    schoonmaak.Add(new Schoonmaak(id, tram, opmerking, invoerDatum, bevestigd, s));
                }

                return schoonmaak;
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

        public List<Reparatie> GetAllReparatie()
        {
            try
            {
                List<Tram> trams = GetAllTrams();
                List<Reparatie> reparatie = new List<Reparatie>();

                string sql = "SELECT * FROM ONDERHOUD WHERE SOORT = 1";

                OracleCommand command = MaakOracleCommand(sql);

                OracleDataReader reader = VoerQueryUit(command);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    Tram tram = VerkrijgTram(trams, Convert.ToInt32(reader.GetValue(1)));
                    ReparatieType s = (ReparatieType)Convert.ToInt32(reader.GetValue(3));
                    string opmerking = Convert.ToString(reader.GetValue(4));
                    DateTime invoerDatum = DateTime.Now;
                    //Convert.ToDateTime(reader.GetValue(5));
                    bool bevestigd = Convert.ToBoolean(reader.GetValue(6));

                    reparatie.Add(new Reparatie(s, tram, opmerking, invoerDatum, bevestigd));
                }

                return reparatie;
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

        /// <summary>
        /// Verkrijgt een tram uit een lijst met trams.
        /// </summary>
        /// <param name="trams">Een lijst met trams.</param>
        /// <param name="tramNummer">Het tramnummer van de tram die je nodig hebt.</param>
        /// <returns></returns>
        public Tram VerkrijgTram(List<Tram> trams, int tramNummer)
        {
            foreach (Tram t in trams)
            {
                if (t.ID == tramNummer)
                {
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// Verkrijgt alle reserveringen.
        /// </summary>
        /// <returns>Retourneert een lijst met reserveringen</returns>
        public List<Reservering> GetAllReservering()
        {
            try
            {
                List<Tram> trams = GetAllTrams();
                List<Sector> sectoren = GetAllSectoren();
                List<Reservering> reserveringen = new List<Reservering>();

                string sql = "SELECT * FROM RESERVERING";

                OracleCommand command = MaakOracleCommand(sql);

                OracleDataReader reader = VoerQueryUit(command);

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader.GetValue(0));
                    Tram tram = VerkrijgTram(trams, Convert.ToInt32(reader.GetValue(1)));
                    Sector sector = VerkrijgSector(sectoren, Convert.ToInt32(reader.GetValue(2)));

                    reserveringen.Add(new Reservering(id, tram, sector));
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

        /// <summary>
        /// Verkrijgt een tram uit een lijst met trams.
        /// </summary>
        /// <param name="trams">Een lijst met trams.</param>
        /// <param name="tramNummer">Het tramnummer van de tram die je nodig hebt.</param>
        /// <returns></returns>
        public Sector VerkrijgSector(List<Sector> sectoren, int sectorID)
        {
            foreach (Sector s in sectoren)
            {
                if (s.Id == sectorID)
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// Kijkt of de logingegevens in de database juist zijn.
        /// </summary>
        /// <param name="username">De gegeven gebruikersnaam die gecontroleerd wordt.</param>
        /// <param name="password">Het wachtwoord dat bij de gegeven gebruikersnaam hoort</param>
        /// <returns></returns>
        public bool IsLoginCorrect(string username, string password)
        {
            try
            {
                bool result = false;

                string query = "SELECT COUNT(*) FROM PERSONEEL WHERE GEBRUIKERSNAAM = :username AND WACHTWOORD = :password";

                OracleCommand command = MaakOracleCommand(query);

                command.Parameters.Add(":username", username);
                command.Parameters.Add(":password", password);

                OracleDataReader reader = VoerQueryUit(command);

                while (reader.Read())
                {
                    if (Convert.ToInt32(reader.GetValue(0)) == 1)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }

                return result;
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
        /// Voorbeeld query methode
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool TestFunctie(string data)
        {
            try
            {
                string sql = "SELECT :Data FROM DATA";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":Data", data);

                OracleDataReader reader = VoerQueryUit(command);

                return Convert.ToBoolean(reader["Data"]);
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

        #region UpdateQueries

        /// <summary>
        /// Update alle tram waarden in de database voor het gegeven tram id.
        /// </summary>
        /// <param name="tram">De tram die moet worden geüpdate</param>
        /// <returns></returns>
        public bool UpdateTram(Tram tram) //Moet nog worden getest
        {
            try
            {
                string sql = "UPDATE TRAM SET TRA_TYPE = :tramType, TRAMNUMMER = :tramNummer, VERTREKTIJD = :vertrekTijd, STATUS = :status, STATUS_OPMERKING = :statusOpmerking"
                            + " WHERE ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":tramType", Convert.ToInt32(tram.Type));
                command.Parameters.Add(":tramNummer", tram.TramNummer);
                command.Parameters.Add(":vertrekTijd", tram.VertrekTijd);
                command.Parameters.Add(":status", Convert.ToInt32(tram.Status));
                command.Parameters.Add(":statusOpmerking", tram.StatusOpmerking);
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
        public bool UpdateSpoor(Spoor spoor) //Moet nog worden getest
        {
            try
            {
                string sql = "UPDATE SPOOR SET STATUS = :status, TYPE = :type"
                            + " WHERE NUMMER = :nummer";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":status", Convert.ToInt32(spoor.SpoorStatus));
                command.Parameters.Add(":tramType", Convert.ToInt32(spoor.SpoorType));
                command.Parameters.Add(":nummer", spoor.SpoorNummer);

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
        public bool UpdateSector(Sector sector) //Moet nog worden getest
        {
            try
            {
                string sql = "UPDATE SECTOR SET TRA_ID = :tramID, SPO_NUMMER = :spoorNummer, STATUS = :status"
                            + " WHERE ID = :ID";

                OracleCommand command = MaakOracleCommand(sql);

                if (sector.Tram != null)
                {
                    command.Parameters.Add(":tramID", sector.Tram.ID);
                } else
                {
                    command.Parameters.Add(":tramID", DBNull.Value);
                }
                
                command.Parameters.Add(":spoorNummer", sector.SpoorNummer);
                command.Parameters.Add(":status", Convert.ToInt32(sector.Status));
                command.Parameters.Add(":ID", sector.Id);

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

        #region Insert

        public bool AddTram(Tram tram)
        {
            try
            {
                /*
                string sql = "INSERT INTO RESERVERING ( ID, TRA_ID, SPO_NUMMER) VALUES ( RESERVERING_SEQ, :tramID, :spoorNummer )";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":tramID", res.TramID);
                command.Parameters.Add(":spoorNummer", res.SpoorNummer);

                return VoerNonQueryUit(command); */
                return true;
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
        /// <returns></returns>
        public bool AddReservering(Reservering res) //moet nog worden getest
        {
            try
            {
                string sql = "INSERT INTO RESERVERING ( ID, TRA_ID, SEC_ID ) VALUES ( RESERVERING_SEQ.nextval, :tramID, :sectorID )";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":tramID", res.Tram.ID);
                command.Parameters.Add(":sectorID", res.Sector.Id);

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
        /// Voegt een schoonmaak toe aan de database
        /// </summary>
        /// <param name="sch">De schoonmaak die moet worden toegevoegd</param>
        /// <returns></returns>
        public bool AddSchoonmaak(Schoonmaak sch) //moet nog worden getest
        {
            // 2 is schoonmaak
            try
            {
                string sql = "INSERT INTO ONDERHOUD ( ID, TRA_ID, SOORT, TYPE, OPMERKING, INVOER_DATUM, BEVESTIGD) VALUES ( ONDERHOUD_SEQ.nextval, :tramID, :soort, :type, :opmerking, :invoerDatum, :bevestigd )";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":tramID", sch.Tram.ID);
                command.Parameters.Add(":soort", 2);
                command.Parameters.Add(":type", Convert.ToInt32(sch.SchoonmaakType));
                command.Parameters.Add(":opmerking", sch.Opmerking);
                command.Parameters.Add(":invoerDatum", sch.Datum.ToString());
                command.Parameters.Add(":bevestigd", Convert.ToInt32(sch.Bevestigd));

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
        /// Voegt een reparatie toe aan de database
        /// </summary>
        /// <param name="rep">De reparatie die moet worden toegevoegd.</param>
        /// <returns></returns>
        public bool AddReparatie(Reparatie rep)
        {
            //1 is reparatie
            try
            {
                string sql = "INSERT INTO ONDERHOUD ( ID, TRA_ID, SOORT, TYPE, OPMERKING, INVOER_DATUM, BEVESTIGD) VALUES ( ONDERHOUD_SEQ.nextval, :tramID, :soort, :type, :opmerking, :invoerDatum, :bevestigd )";

                OracleCommand command = MaakOracleCommand(sql);


                command.Parameters.Add(":tramID", rep.Tram.ID);
                command.Parameters.Add(":soort", 1);
                command.Parameters.Add(":type", Convert.ToInt32(rep.ReparatieType));
                command.Parameters.Add(":opmerking", rep.Opmerking);
                command.Parameters.Add(":invoerDatum", rep.Datum.ToString());
                command.Parameters.Add(":bevestigd", Convert.ToInt32(rep.Bevestigd));

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

        #region DeleteQueries
        public bool DeleteTram(int tramNummer)
        {
            try
            {
                string sql = "DELETE FROM TRAM WHERE TRAMNUMMER = :tramNummer";

                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":tramNummer", tramNummer);

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

        public bool DeleteSchoonmaak(int nummer)
        {
            try
            {
                string sql = "DELETE FROM ONDERHOUD WHERE tra_id = (SELECT t.ID FROM TRAM t WHERE t.TRAMNUMMER = :nummer)";
                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":nummer", nummer);

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

        public bool DeleteReservering(int nummer)
        {
            try
            {
                string sql = "DELETE FROM RESERVERING WHERE id = :nummer";
                OracleCommand command = MaakOracleCommand(sql);

                command.Parameters.Add(":nummer", nummer);

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
