using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICT4Rails_ASP.ClassesASP;

namespace ICT4Rails_ASP
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["gebruiker"] != null)
            {
                Response.Redirect("http://172.20.128.1/Pages/Beheer.aspx");
            } 
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbGebruikersnaam.Text;
            string password = tbWachtwoord.Text;
            InlogController ic = new InlogController();
            PrincipalContext pc = ic.GetPrincipalContext();
            if (pc != null)
            {
                Response.Write("AD omgeving opgehaald");
            }
            else
            {
                Response.Write("Error: kon AD omgeving niet ophalen");
                return;
            }

            if (ic.BestaatUser(username))
            {
                Response.Write("Gebruiker " + username + " bestaat.");
            }
            else
            {
                Response.Write("Error: gebruikersnaam " + username + " bestaat niet.");
                return;
            }

            if (ic.CheckLogin(username, password))
            {
                Gebruiker g = ic.GetGebruiker(username);
                Session["gebruiker"] = g;
                Response.Redirect("http://172.20.128.1/Pages/Beheer.aspx");
            }
            else
            {
                Response.Write("Error: gebruikersnaam " + username + " met ingevoerde wachtwoord komen niet overeen met die van Active Directory.");
                return;
            }
        }
    }
}