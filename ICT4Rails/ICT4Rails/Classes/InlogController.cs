using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ICT4Rails.Scripts
{
    public class InlogController
    {
        /// <summary>
        /// Verkrijgt de Sha256 hash van het ingevoerde wachtwoord.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text); //Slaat het text op als UTF-8 byte code.
            SHA256Managed hashstring = new SHA256Managed(); //Verkrijgen van de SHA256 hashstring.
            byte[] hash = hashstring.ComputeHash(bytes); //Berekent de hashwaarde voor de specifieke bytes reeks van de array bytes.
            string hashString = string.Empty;
            foreach (byte x in hash) //Doorloopt iedere byte in de array hash.
            {
                hashString += String.Format("{0:x2}", x); //De byte in hash wordt gedecodeerd naar dat specifieke format.
            }
            return hashString;
        }
    }
}
