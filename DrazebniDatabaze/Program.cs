using System;

namespace Drazebni_databaze
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabazeUzivatelu db = DatabazeUzivatelu.Instance;
            DrazebniDatabaze test = new DrazebniDatabaze();
            try
            {
                db.NactiUzivatele();
                foreach (var uzivatel in db.uzivatele)
                {
                   Console.WriteLine(uzivatel);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

             Uzivatel u1 = db.GetById(1);
             Uzivatel u2 = db.GetById(2);
             Uzivatel u3 = db.GetById(3);

             Nabidka n1 = new Nabidka(u1, 25);
             Nabidka n2 = new Nabidka(u1, 15);
             Nabidka n3 = new Nabidka(u2, 35);

            Auto bmw = new Auto("bmw", Skupina.A, DateTime.Now, 75, 4);
            Auto skoda = new Auto("Skoda", Skupina.B, DateTime.Now, 85, 6);

            Drazba d = new Drazba(bmw, "sdffffffffffwsefwefwef");
            d.pridej(n1);
            d.pridej(n2);
            d.pridej(n3);

            Drazba d2 = new Drazba(skoda, "adwaduadhqaudhhudli");
            d2.pridej(n1);
            d2.pridej(n2);
            d2.pridej(n3);

            test.PridejDrazbu(d);
            test.PridejDrazbu(d2);


            Console.WriteLine(n1.PrihazujiciID());
            Console.WriteLine(n3.PrihazujiciID());

            Console.WriteLine(test.AktualniDrazba);
            test.DrazbaSkoncila();
            Console.WriteLine(test.AktualniDrazba);
            test.DrazbaSkoncila();

        }

    }
}