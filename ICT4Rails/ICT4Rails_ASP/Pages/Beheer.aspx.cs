using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Rails_ASP.ClassesASP;
using ICT4Rails_ASP.Classes;

namespace ICT4Rails_ASP
{
    public partial class Beheer : System.Web.UI.Page
    {
        BeheerController bc;
        private DataTable dtTrams;
        private DataTable dtReserveringen;
        private int count;
        private Gebruiker gebruiker;

        protected void Page_Load(object sender, EventArgs e)
        {
            gebruiker = (Gebruiker)Session["gebruiker"];
            if(gebruiker == null)
            {
                Response.Redirect("Login.aspx");
            }
            if(gebruiker.Groep == groepen.Schoonmaker || gebruiker.Groep == groepen.Technicus)
            {
                if(gebruiker.Groep == groepen.Schoonmaker)
                {
                    Response.Redirect("Schoonmaak.aspx");
                }
                else
                {
                    Response.Redirect("Reparatie.aspx");
                }
            }

            bc = new BeheerController();
            //try
            //{
            lbSectorBestaatNiet.BackColor = Color.DimGray;
            lbSectorBestaatNiet.BorderColor = Color.Blue;

            lbSectorBezet.BackColor = Color.DeepSkyBlue;
            lbSectorBezet.BorderColor = Color.Blue;

            lbSectorGeblokkeerd.BackColor = Color.DarkRed;
            lbSectorGeblokkeerd.ForeColor = Color.White;
            lbSectorGeblokkeerd.BorderColor = Color.Blue;

            lbSectorLeeg.BackColor = Color.White;
            lbSectorLeeg.BorderColor = Color.Blue;

            lbSectorNietBeschikbaar.BackColor = Color.OrangeRed;
            lbSectorNietBeschikbaar.ForeColor = Color.White;
            lbSectorNietBeschikbaar.BorderColor = Color.Blue;

            lbSectorGereserveerd.BackColor = Color.Green;
            lbSectorGereserveerd.ForeColor = Color.White;
            lbSectorGereserveerd.BorderColor = Color.Blue;

            FillLists();
            FillDDLSporen();

            if (!IsPostBack)
            {
                timerTimer.Enabled = false;
            }

            bindGvRemise();
            bindGvReserveringen();

            if (ViewState["dtTrams"] != null)
            {
                dtTrams = (DataTable)ViewState["dtTrams"];
                count = (int)ViewState["count"];
            }
            else
            {
                dtTrams = new DataTable();
                count = 0;
            }

            if (ViewState["dtReserveringen"] != null)
            {
                dtReserveringen = (DataTable)ViewState["dtReserveringen"];
            }
            else
            {
                dtReserveringen = new DataTable();
            }

            ddlTramToevoegen.DataSource = Enum.GetValues(typeof(TramType));
            ddlTramToevoegen.DataBind();

            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
        }

        private void bindGvRemise()
        {
            gvRemise.DataSource = getDataTable();
            gvRemise.DataBind();

            gvRemise.Rows[0].Height = 50;

            foreach (GridViewRow row in gvRemise.Rows)
            {
                foreach (TableCell c in row.Cells)
                {
                    int lengte = c.Text.Length;

                    if (lengte < 6)
                    {
                        c.BackColor = Color.DeepSkyBlue;
                        continue;
                    }

                    switch (lengte)
                    {
                        case 7:
                            // De sector is geblokkeerd
                            c.Text = "";
                            c.BackColor = Color.DarkRed;
                            break;
                        case 12:
                            // De sector is niet beschikbaar
                            c.Text = "";
                            c.BackColor = Color.OrangeRed;
                            break;
                        case 14:
                            // De sector is niet beschikbaar
                            c.Text = "";
                            c.BackColor = Color.Green;
                            break;
                        case 8:
                            // Sector is beschikbaar
                            c.Text = "";
                            c.BackColor = Color.White;
                            break;
                        case 10:
                            // Error in getDataTable
                            c.BackColor = Color.DeepPink;
                            break;
                        default:
                            c.BackColor = Color.DimGray;
                            break;
                    }
                }
            }
        }

        private void bindGvReserveringen()
        {
            gvReserveringen.DataSource = getDataTableReserveringen();
            gvReserveringen.DataBind();

            gvRemise.Rows[0].Height = 50;
        }

        private void FillDDLSporen()
        {
            foreach (Spoor spoor in bc.GetAllSporen())
            {
                ddlSpoorBlokkeren.Items.Add(spoor.Nummer.ToString());
                ddlSpoorReserverenSpoor.Items.Add(spoor.Nummer.ToString());
                ddlSectorBlokkerenSpoor.Items.Add(spoor.Nummer.ToString());
                if (spoor.Beschikbaar == true)
                {
                    ddlTramVerplaatsenSpoor.Items.Add(spoor.Nummer.ToString());
                }
            }
        }

        private void FillLists()
        {
            foreach (Tram t in bc.GetAllTrams())
            {
                if (t.Beschikbaar)
                {
                    ddlTramVerplaatsenTram.Items.Add(t.TramNummer.ToString());
                }
                ddlSpoorReserverenTram.Items.Add(t.TramNummer.ToString());
                ddlTramVerwijderen.Items.Add(t.TramNummer.ToString());
                ddlTramStatusAanpassenTramnummer.Items.Add(t.TramNummer.ToString());
            }
        }

        private DataTable getDataTable()
        {
            DataTable result = new DataTable();

            List<Spoor> sporen = bc.GetAllSporen();

            for (int i = 0; i < 9; i++)
            {
                result.Rows.Add();
            }

            for (int i = 0; i < sporen.Count; i++)
            {
                result.Columns.Add("Spoor " + sporen[i].Nummer);
            }

            int rowCounter = 0;
            int cellCounter = 0;
            int testCounter = 0;
            bool spoorIsGereserveerd = false;

            DataRow row;
            foreach (Spoor spoor in sporen)
            {
                foreach (Reservering reservering in bc.GetAllReserveringen())
                {
                    if (reservering.Spoor.ID == spoor.ID)
                    {
                        spoorIsGereserveerd = true;
                    }
                }

                foreach (Sector sector in spoor.Sectoren)
                {
                    if (sector.Tram != null)
                    {
                        result.Rows[rowCounter].ItemArray[cellCounter] = sector.Tram.TramNummer;
                        row = result.Rows[rowCounter];
                        row[cellCounter] = sector.Tram.TramNummer;//3 of 4 (ong) = tramnummer zelf 
                    }
                    else if (sector.Geblokkeerd)
                    {
                        row = result.Rows[rowCounter];
                        row[cellCounter] = 7777777;//7 = geblokkeerd
                    }
                    else if (!sector.Beschikbaar)
                    {
                        row = result.Rows[rowCounter];
                        row[cellCounter] = 121212121212;//12 = niet beschikbaar
                    }
                    else if (spoorIsGereserveerd)
                    {
                        row = result.Rows[rowCounter];
                        row[cellCounter] = 14141414141414; //14 = spoor is gereserveerd
                    }
                    else if (sector.Beschikbaar && !sector.Geblokkeerd && sector.Tram == null)
                    {
                        row = result.Rows[rowCounter];
                        row[cellCounter] = 88888888;//8 = sector bestaat & is beschikbaar
                    }
                    else
                    {
                        row = result.Rows[rowCounter];
                        row[cellCounter] = 1010101010;//10 = sector bestaat niet
                    }
                    rowCounter++;
                    testCounter++;
                }
                spoorIsGereserveerd = false;

                cellCounter++;
                rowCounter = 0;
            }
            return result;
        }

        private DataTable getDataTableReserveringen()
        {
            DataTable result = new DataTable();

            List<Reservering> reserveringen = bc.GetAllReserveringen();

            result.Columns.Add("Id");
            result.Columns.Add("Tramnummer");
            result.Columns.Add("Spoornummer");

            int rowCounter = 0;

            DataRow row;
            foreach (Reservering res in reserveringen)
            {
                result.Rows.Add();

                row = result.Rows[rowCounter];
                row[0] = res.ID;
                row[1] = res.Tram.TramNummer;
                row[2] = res.Spoor.Nummer;

                rowCounter++;
            }
            return result;
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            ViewState.Add("dtTrams", dtTrams);
            ViewState.Add("count", count);
        }

        protected void indeelTimer_Tick(object sender, EventArgs e)
        {
            count += 1;
            lbTimerPlaceholder.Text = "Momenteel " + count + " trams ingereden.";

            IUcontroller.DeelTramInBeter(bc.GetWillekeurigeNietBeschikbareTram());
            //int tempInt = Convert.ToInt32(lbTimerPlaceholder.Text);
            //lbTimerPlaceholder.Text = (tempInt + 1).ToString();
        }


        protected void btnStopTimer_Click(object sender, EventArgs e)
        {
            timerTimer.Enabled = false;
        }

        protected void btnStartTimer_Click(object sender, EventArgs e)
        {
            timerTimer.Enabled = true;
            //List<Tram> trams = bc.GetAllTrams();

            //foreach (Tram tram in trams)
            //{
            //    IUcontroller.DeelTramInBeter(tram);
            //}
        }

        protected void gvRemise_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSectorReserverenSpoor_SelectedIndexChanged(object sender, EventArgs e)
        {

            int spoornummer = Convert.ToInt32(ddlSpoorReserverenSpoor.SelectedItem.Text);
            Spoor selectedSpoor = bc.GetSpoorByNummer(spoornummer);


            if (selectedSpoor == null)
            {
                Response.Write("Selectedspoor " + spoornummer + " is null");
                return;
            }
        }

        protected void ddlTramVerplaatsenSpoor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTramVerplaatsenSector.Items.Clear();

            Spoor selectedSpoor = null;
            foreach (Spoor spoor in bc.GetAllSporen())
            {
                if (spoor.Nummer == Convert.ToInt32(ddlTramVerplaatsenSpoor.SelectedItem.Text))
                {
                    selectedSpoor = spoor;
                }
            }
            if (selectedSpoor == null)
            {
                Response.Write("Selectedspoor is null");
                return;
            }

            foreach (Sector s in selectedSpoor.GetLegeSectoren())
            {
                ddlTramVerplaatsenSector.Items.Add(s.Nummer.ToString());
            }
        }

        protected void ddlSectorBlokkerenSpoor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //todo; sectoren ophalen die bij dit spoor horen
            ddlSectorBlokkerenSector.Items.Clear();

            Spoor selectedSpoor = null;
            foreach (Spoor spoor in bc.GetAllSporen())
            {
                if (spoor.Nummer == Convert.ToInt32(ddlSectorBlokkerenSpoor.SelectedItem.Text))
                {
                    selectedSpoor = spoor;
                }
            }
            if (selectedSpoor == null)
            {
                Response.Write("Selectedspoor is null");
                return;
            }

            foreach (Sector s in selectedSpoor.Sectoren)
            {
                ddlSectorBlokkerenSector.Items.Add(s.Nummer.ToString());
            }
        }

        protected void btnTramVerwijderen_Click(object sender, EventArgs e)
        {
            try
            {
                int tramnummer = Convert.ToInt32(ddlTramVerwijderen.SelectedItem.Text);
                Tram t = bc.GetTramByNummer(tramnummer);
                bc.TramVerwijderen(t);
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                string script = "alert(\"    " + ex.Message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnTramToevoegen_Click(object sender, EventArgs e)
        {
            try
            {
                BeheerController bc = new BeheerController();
                int tramnummer = Convert.ToInt32(tbTramToevoegen.Text);
                int year = 2015;
                int month = 11;
                int days = 11;
                int hours = Convert.ToInt32(tbTramToevoegenVertrektijdUren.Text);
                int minutes = Convert.ToInt32(tbTramToevoegenVertrektijdMinuten.Text);
                int seconds = 00;
                DateTime vertrektijd = new DateTime(year, month, days, hours, minutes, seconds);
                String typeString = ddlTramToevoegen.SelectedItem.Text;
                TramType type = (TramType)Enum.Parse(typeof(TramType), typeString);

                bc.TramToevoegen(tramnummer, vertrektijd, type);
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                string script = "alert(\"    " + ex.Message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnTramVerplaatsen_Click(object sender, EventArgs e)
        {
            try
            {
                int tramnummer = Convert.ToInt32(ddlTramVerplaatsenTram.SelectedItem.Text);
                int sectornaar = Convert.ToInt32(ddlTramVerplaatsenSector.SelectedItem.Text);
                int spoornaar = Convert.ToInt32(ddlTramVerplaatsenSpoor.SelectedItem.Text);
                Tram t = bc.GetTramByNummer(tramnummer);
                Spoor p = bc.GetSpoorByNummer(spoornaar);

                bc.TramVerplaatsen(t, bc.GetSpoorByTram(t), bc.GetSectorByTram(t), p, bc.GetSectorByID(p, sectornaar));
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                string script = "alert(\"    " + ex.Message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnSectorBlokkerenDeblokkeren_Click(object sender, EventArgs e)
        {

        }

        protected void btnSectorReserveren_Click(object sender, EventArgs e)
        {

        }

        protected void btnTramStatusAanpassen_Click(object sender, EventArgs e)
        {
            try
            {
                int tramnummer = Convert.ToInt32(ddlTramStatusAanpassenTramnummer.Text);
                string status = ddlTramStatusAanpassenStatus.Text;

                Tram tram = bc.GetTramByNummer(tramnummer);

                if (status == "vervuild")
                {
                    bc.TramStatusAanpassen(tram, true, tram.Defect);
                }
                else if (status == "defect")
                {
                    bc.TramStatusAanpassen(tram, tram.Vervuild, true);
                }
                else if (status == "beschikbaar")
                {
                    bc.TramStatusAanpassen(tram, false, false);
                }
                else
                {
                    // geef status aan.
                }

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                string script = "alert(\"    " + ex.Message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnSpoorReserveren_Click(object sender, EventArgs e)
        {
            try
            {
                bc = new BeheerController();

                int spoornummer = Convert.ToInt32(ddlSpoorReserverenSpoor.SelectedItem.Text);
                int tramnummer = Convert.ToInt32(ddlSpoorReserverenTram.SelectedItem.Text);

                Spoor s = bc.GetSpoorByNummer(spoornummer);
                Tram t = bc.GetTramByNummer(tramnummer);

                bc.SpoorReserveren(s, t);
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                string script = "alert(\"    " + ex.Message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        protected void btnSpoorBlokkerenDeblokkeren_Click(object sender, EventArgs e)
        {
            try
            {
                Spoor s = bc.GetSpoorByNummer(Convert.ToInt32(ddlSpoorBlokkeren.SelectedItem.Text));

                foreach (Sector sector in s.Sectoren)
                {
                    bc.ToggleSector(sector);
                }
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                string script = "alert(\"    " + ex.Message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }

        }

        protected void btnSectorBlokkerenDeblokkeren_Click1(object sender, EventArgs e)
        {
            try
            {
                Spoor s = bc.GetSpoorByNummer(Convert.ToInt32(ddlSectorBlokkerenSpoor.SelectedItem.Text));
                int sectorId = Convert.ToInt32(ddlSectorBlokkerenSector.SelectedItem.Text);
                Sector se = bc.GetSectorByID(s, sectorId);
                bc.ToggleSector(se);
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ");</script>");
            }
        }
    }
}