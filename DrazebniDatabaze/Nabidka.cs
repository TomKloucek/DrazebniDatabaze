using System;
using System.Collections.Generic;
using System.Text;

namespace Drazebni_databaze
{
    public class Nabidka
    {
        public int id;
        public Uzivatel prihazujici;
        public int castka;

        public Int32 ID
        {
            get => id;
            set { id = value; }
        }

        public Nabidka(Uzivatel prihazujici, int castka)
        {
            this.prihazujici = prihazujici;
            this.castka = castka;
        }

        public override string ToString()
        {
            return $"Uzivatel {prihazujici.Jmeno} prihazuje {castka}";
        }

        public int PrihazujiciID()
        {
            UzivatelDAO dao = new UzivatelDAO();
            return dao.UzivatelID(this.prihazujici);
        }

    }
}
