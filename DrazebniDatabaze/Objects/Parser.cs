using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Drazebni_databaze
{
    class Parser
    {
        public Prodavajici ParseProdavajici(string path)
        {
            string email = null,telefon = null,jmeno = null,prijmeni = null;
            List<string> data = new List<string>(); 
            System.IO.StreamReader file = new System.IO.StreamReader(@path);
            string[] rawData = file.ReadToEnd().Split(",");

            Regex regex = new Regex(@"\w+@[a-zA-Z]+?\.[a-zA-Z]{2,3}");
            Regex regex_2 = new Regex(@"(\+?420)? ?[0-9]{3} ?[0-9]{3} ?[0-9]{3}");

            // is this condition true ? yes : no
            foreach (string cast in rawData)
            {
                Match match_email = regex.Match(cast);
                if (match_email.Success)
                {
                   email = match_email.Value;
                }
                Match match_phone = regex_2.Match(cast);
                if (match_phone.Success)
                {
                    telefon = match_phone.Value.Trim();
                }
                data.Add(cast);
            }      

            jmeno = data[0].Split(":")[1].Split(" ")[0];
            prijmeni = data[0].Split(":")[1].Split(" ")[1];
          
            file.Close();

            return new Prodavajici(jmeno, prijmeni, email, telefon);
        }

        public DrazenaEntita ParseEntita(string path)
        {
            int cena = 0;
            string nazev = null;
            List<string> data = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(@path);
            string[] rawData = file.ReadToEnd().Split(",");

            foreach(var item in rawData)
            {
                data.Add(item);
            }

            nazev = data[1].Trim().Split(':')[1];
            cena = Int32.Parse(data[2].Trim().Split(':')[1].Substring(0, data[2].Trim().Split(':')[1].Length-2));

            return new DrazenaEntita(nazev, cena);
        }
    }
}
