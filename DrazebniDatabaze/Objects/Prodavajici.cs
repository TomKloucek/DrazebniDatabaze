using System;
using System.Collections.Generic;
using System.Text;

namespace Drazebni_databaze
{
    class Prodavajici
    {
        private string jmeno;
        private string prijmeni;
        private string email;
        private string telefon;

        public Prodavajici(string jmeno, string prijmeni, string email, string telefon)
        {
            this.Jmeno = jmeno;
            this.Prijmeni = prijmeni;
            this.Email = email;
            this.Telefon = telefon;
        }

        public string Telefon { get => telefon; set => telefon = value; }
        public string Email { get => email; set => email = value; }
        public string Prijmeni { get => prijmeni; set => prijmeni = value; }
        public string Jmeno { get => jmeno; set => jmeno = value; }

        public override string ToString()
        {
            return $"Jmeno: {Jmeno}\nPrijmeni: {Prijmeni}\nEmail: {Email}\nTelefon: {Telefon}";
        }
    }
}
