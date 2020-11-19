using System;
using System.Collections.Generic;
using System.Text;

namespace Drazebni_databaze
{
    public class Nabidka
    {
        public Uzivatel prihazujici;
        public int castka;

        public Nabidka(Uzivatel prihazujici, int castka)
        {
            this.prihazujici = prihazujici;
            this.castka = castka;
        }

        public override string ToString()
        {
            return $"Uzivatel {prihazujici.Jmeno} prihazuje {castka}";
        }
    }
}
