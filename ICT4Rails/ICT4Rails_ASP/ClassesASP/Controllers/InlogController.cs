using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using ICT4Rails_ASP.Classes;
using System.DirectoryServices.AccountManagement;

namespace ICT4Rails_ASP.ClassesASP
{
    public class InlogController
    {
        #region Fields
        private string sDomain = "Server.ict4rails.com";
        private string sDefaultOU = "DC=ict4rails,DC=com";
        private string sDefaultRootOU = "DC=ict4rails,DC=com";
        private string sServiceUser = "CN=Administrator,CN=Users,DC=ict4rails,DC=com";
        private string sServicePassword = "Qwerty123!";


        private const string url = "LDAP://CN=Users,DC=ict4rails,DC=com";
        private const string loginaam = "Administrator";
        private const string password = "Qwerty123!";
        #endregion

        /// <summary>
        /// Kijkt of de gebruiker bestaat en zijn credentials valide zijn.
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public bool CheckLogin(string gebruikersnaam, string wachtwoord)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();
            return oPrincipalContext.ValidateCredentials(gebruikersnaam, wachtwoord);
        }

        public Gebruiker GetGebruiker(string gebruikersnaam)
        {
            UserPrincipal uc = GetUser(gebruikersnaam);

            foreach (groepen groep in Enum.GetValues(typeof(groepen)))
            {
                if (IsUserGroupMember(gebruikersnaam, groep.ToString()))
                {
                    Gebruiker gebruiker = new Gebruiker(uc.EmployeeId, uc.EmailAddress, uc.Surname, uc.GivenName, groep, gebruikersnaam);
                    return gebruiker;
                }
            }

            return null;
        }

        public bool IsUserGroupMember(string sUserName, string sGroupName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            GroupPrincipal oGroupPrincipal = GetGroup(sGroupName);

            if (oUserPrincipal != null && oGroupPrincipal != null)
            {
                return oGroupPrincipal.Members.Contains(oUserPrincipal);
            }
            else
            {
                return false;
            }
        }

        public GroupPrincipal GetGroup(string sGroupName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            GroupPrincipal oGroupPrincipal = GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);
            return oGroupPrincipal;
        }

        /// <summary>
        /// Pakt de AD omgeving erbij in een C# klasse
        /// </summary>
        /// <returns>Retruns the PrincipalContext object</returns>
        public PrincipalContext GetPrincipalContext()
        {
            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Domain, sDomain, sDefaultOU, ContextOptions.SimpleBind, sServiceUser, sServicePassword);
            return oPrincipalContext;
        }

        /// <summary>
        /// Checkt of de username bestaat
        /// </summary>
        /// <param name="sUserName">The username to check</param>
        /// <returns>Returns true if username Exists</returns>
        public bool BestaatUser(string sUserName)
        {
            return GetUser(sUserName) != null;
        }


        /// <summary>
        /// Haalt een user op
        /// </summary>
        /// <param name="sUserName">The username to get</param>
        /// <returns>Returns the UserPrincipal Object</returns>
        public UserPrincipal GetUser(string sUserName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            UserPrincipal oUserPrincipal = UserPrincipal.FindByIdentity(oPrincipalContext, sUserName);
            return oUserPrincipal;
        }
    }
}