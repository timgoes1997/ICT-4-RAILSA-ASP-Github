using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Rails_ASP.ClassesASP;
using ICT4Rails_ASP.Classes;

namespace ICT4Rails_ASP
{
    public partial class Reparatie : System.Web.UI.Page
    {
        #region Fields
        private OnderhoudController oc;
        private DataTable schoonmaakLijstenDt;
        private Gebruiker gebruiker;
        #endregion
        #region Properties

        #endregion
        #region Constructors
        protected void Page_Load(object sender, EventArgs e)
        {
            //    try
            //    {
            gebruiker = (Gebruiker)Session["gebruiker"];
            if (gebruiker == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (gebruiker.Groep == groepen.Schoonmaker || gebruiker.Groep == groepen.Bestuurder || gebruiker.Groep == groepen.WagenparkBeheerder)
            {
                if (gebruiker.Groep == groepen.Schoonmaker)
                {
                    Response.Redirect("Schoonmaak.aspx");
                }
                else
                {
                    Response.Redirect("Beheer.aspx");
                }
            }
            oc = new OnderhoudController();
            FillLists();
            DataTable dtOnderhoud = GetReaparatieDT(new OnderhoudController().GetAllReparatie());

            gvReparatieLijsten.DataSource = dtOnderhoud;
            gvReparatieLijsten.DataBind();
            //schoonmaakLijstenDt = onderhoudController.GetOnderhoudDataTable();
            //gvSchoonmaakLijsten.DataSource = schoonmaakLijstenDt;
            //gvSchoonmaakLijsten.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message);
            //}
        }
        #endregion

        #region Methode
        private DataTable GetReaparatieDT(List<Onderhoud> input)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Tramnummer", typeof(int));
            result.Columns.Add("Tijdstip", typeof(DateTime));
            result.Columns.Add("Beschikbaar datum", typeof(DateTime));
            result.Columns.Add("Type", typeof(String));

            foreach (Onderhoud o in input)
            {
                DataRow d = result.NewRow();
                d["Tramnummer"] = o.Tram.TramNummer;
                d["Tijdstip"] = o.Tijdstip;
                d["Beschikbaar datum"] = o.BeschikbaarDatum;
                d["Type"] = o.TypeOnderhoud.ToString();
                result.Rows.Add(d);
            }

            return result;
        }
        public void FillLists()
        {
            foreach (Tram t in oc.GetNietDefecteTrams())
            {
                ddlServicebeurtToevoegenTramNummer.Items.Add(t.TramNummer.ToString());
            }
            foreach (Tram t in oc.GetDefecteTrams())
            {
                ddlReparatieAfronden.Items.Add(t.TramNummer.ToString());
            }
        }

        #endregion

        protected void btnServicebeurtToevoegen_Click(object sender, EventArgs e)
        {
            try
            {
                int tramnummer = Convert.ToInt32(ddlServicebeurtToevoegenTramNummer.Text);
                String typeString = ddlServicebeurtToevoegenType.Text;
                TypeOnderhoud type = (TypeOnderhoud)Enum.Parse(typeof(TypeOnderhoud), typeString);
                int adid = 1; // todo; moet nog gemaakt worden

                BeheerController bc = new BeheerController();
                Tram t = bc.GetTramByNummer(tramnummer);
                t.Defect = false;
                new DatabaseController().UpdateTram(t);

                Onderhoud ond = new Onderhoud(adid, t, DateTime.Now, DateTime.Now, type);
                oc.AddOnderhoud(ond);
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                string script = "alert(\"    " + ex.Message + "\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            //dummy
        }

        protected void btnReparatieAfronden_Click(object sender, EventArgs e)
        {
            try
            {
                BeheerController bc = new BeheerController();
                Tram t = bc.GetTramByNummer(Convert.ToInt32(ddlReparatieAfronden.Text));
                Onderhoud ond = oc.GetOnderhoud(t);
                oc.OnderhoudAfronden(ond.ID);
                t.Defect = false;
                new DatabaseController().UpdateTram(t);
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