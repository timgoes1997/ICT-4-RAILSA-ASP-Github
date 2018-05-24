using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICT4Rails_ASP.Classes;

namespace ICT4Rails_ASP.ClassesASP
{
    public class Gebruiker
    {
        private string employeeID;
        private string email;
        private string gebruikersnaam;

        private string voornaam;
        private string achternaam;
        private groepen groep;

        public string EmployeeID { get { return employeeID; } set { employeeID = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Voornaam { get { return voornaam; } set { voornaam = value; } }
        public string Achternaam { get { return achternaam; } set { achternaam = value; } }
        public string Gebruikersnaam { get { return gebruikersnaam; } set { gebruikersnaam = value; } }
        public groepen Groep { get { return groep; } set { groep = value; } }

        public Gebruiker(string employeeID, string email, string voornaam, string achternaam, groepen groep, string gebruikersnaam)
        {
            this.employeeID = employeeID;
            this.email = email;
            this.voornaam = voornaam;
            this.achternaam = achternaam;
            this.groep = groep;
            this.gebruikersnaam = gebruikersnaam;
        }
    }
}