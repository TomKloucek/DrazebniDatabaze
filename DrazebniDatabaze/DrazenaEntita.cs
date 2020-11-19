using System;
using System.Collections.Generic;
using System.Text;

namespace Drazebni_databaze
{
    class DrazenaEntita
    {
        private string jmeno;
        private int cena;

        public int Cena { get => cena; set => cena = value; }
        public string Jmeno { get => jmeno; set => jmeno = value; }

        public DrazenaEntita(string jmeno, int cena)
        {
            this.Jmeno = jmeno;
            this.Cena = cena;
        }

        public override string ToString()
        {
            return $"Predmet: {this.Jmeno}\nCena: {this.Cena}Kc";
        }

    }
}
