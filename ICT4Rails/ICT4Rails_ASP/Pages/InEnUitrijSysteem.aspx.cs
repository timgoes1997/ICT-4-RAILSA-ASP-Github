using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Rails_ASP.ClassesASP;

namespace ICT4Rails_ASP
{
    public partial class InEnUitrijSysteem : System.Web.UI.Page
    {
        private BeheerController bc;

        protected void Page_Load(object sender, EventArgs e)
        {
            bc = new BeheerController();
            bindGvRemise();

            FillLists();
        }

        #region Method

        private void bindGvRemise()
        {
            gvTrams.DataSource = getDataTable();
            gvTrams.DataBind();
        }

        private DataTable getDataTable()
        {
            DataTable result = new DataTable();
            BeheerController bc = new BeheerController();

            List<Tram> trams = bc.GetAllTrams();

            result.Columns.Add("Spoornummer");
            result.Columns.Add("Sectornummer");
            result.Columns.Add("Tramnummer");
            result.Columns.Add("Vertrektijd tram");
            //result.Columns.Add("Tramstatus");

            int rowCounter = 0;

            DataRow row;
            foreach (Tram t in trams)
            {
                if (!t.Beschikbaar)
                {
                    continue;
                }

                result.Rows.Add();

                //spoorNummer
                //sectorNummer
                //tramnummer
                //vertrektijd van de tram
                //tramstatus

                row = result.Rows[rowCounter];
                row[0] = bc.GetSpoorByTram(t).Nummer;
                row[1] = bc.GetSectorByTram(t).Nummer;
                row[2] = t.TramNummer;
                row[3] = t.Vertrektijd.Hour + ":" + t.Vertrektijd.Minute;
                //if (t.Defect)
                //{
                //    row[4] = "Defect";
                //}
                //else if (t.Vervuild)
                //{
                //    row[4] = "Vervuild";
                //}
                //else
                //{
                //    row[4] = "Beschikbaar";
                //}


                rowCounter++;
            }
            return result;

        }

        public void FillLists()
        {
            foreach (Tram t in bc.GetAllTrams())
            {
                ddlTramstatusAanpassenTramnummer.Items.Add(t.TramNummer.ToString());
            }
            foreach (Tram t in bc.GetNietBeschikbareTrams())
            {
                ddlTramInvoerenTramnummer.Items.Add(t.TramNummer.ToString());
            }
        }

        #endregion

        protected void btnTramstatusAanpassen_Click(object sender, EventArgs e)
        {
            try
            {
                int tramnummer = Convert.ToInt32(ddlTramstatusAanpassenTramnummer.Text);
                string status = ddlTramstatusAanpassenStatus.Text;

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

        protected void btnTramInvoeren_Click(object sender, EventArgs e)
        {
            try
            {
                int tramnummer = Convert.ToInt32(ddlTramInvoerenTramnummer.Text);

                Tram tram = bc.GetTramByNummer(tramnummer);

                Int32.TryParse(ddlTramInvoerenTramnummer.Text, out tramnummer);
                if (tram != null)
                {
                    if (!tram.Beschikbaar)
                    {
                        IUcontroller.DeelTramInBeter(tram);
                    }
                }
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                string script = "alert(\"    " + ex.Message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }
    }
}