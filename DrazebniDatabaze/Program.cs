using System;

namespace Drazebni_databaze
{
    class Program
    {
        static void Main(string[] args)
        {
            
            DrazebniDatabaze drazebniDatabaze = new DrazebniDatabaze();
            Auto bmw = new Auto("bmw", Skupina.A, DateTime.Now, 75, 4);
            Auto skoda = new Auto("Skoda", Skupina.B, DateTime.Now, 85, 6);

            Console.WriteLine(Convert.ToBoolean(bmw.CompareTo(skoda)));
            drazebniDatabaze.Pridej(bmw);
            drazebniDatabaze.Pridej(bmw);
            drazebniDatabaze.Pridej(skoda);
            drazebniDatabaze.Vypis();
            Console.WriteLine(drazebniDatabaze.HledejPodleJmena("bmw"));

            DatabazeUzivatelu db = DatabazeUzivatelu.Instance;
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


            //    db.AddUzivatel(u1);
            //    db.AddUzivatel(u2);
            //   db.AddUzivatel(u3);
            //  Uzivatel test = new Uzivatel("Test", "ffjjEds5D", "zbytecny@gmail.com", "Ulice 54", "456789789");
            //db.AddUzivatel(test);

            /* foreach (var uzivatel in db.uzivatele)
             {
             try
             {
                 db.Save(uzivatel);
             }
             catch(Exception e)
             {
                 Console.WriteLine($"Uzivatel: {uzivatel.Jmeno} uz je ulozen na serveru");
             }
             }
             */







            string popisSpravne = "Bmw is a long established fact that a reader will be distracted by the readable " +
                "content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less " +
                "normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English." +
                " Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text," +
                " and a search for 'lorem ipsum' will uncover many web sites still in their infancy." +
                " Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).";

            string popisSpatne = "Bmw is a long established fact that a reader will be distracted by the readable " +
                "content of a page when looking at its layout. 123456789 of using Lorem Ipsum is that it has a more-or-less " +
                "normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English." +
                " Karlova5 desktop publishing packages and web page editors@chudaci.com now use Lorem Ipsum as their default model text," +
                " and a search for 'lorem ipsum' www.jecnak.cz uncover many web sites still in their infancy." +
                " Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).";
            
                        Drazba d = new Drazba(bmw, popisSpravne);
                        d.pridej(n1);
                        d.pridej(n2);
                        d.pridej(n3);

                        Drazba d2 = new Drazba(skoda, popisSpravne);
                        d2.pridej(n1);
                        d2.pridej(n2);
                        d2.pridej(n3);

                        drazebniDatabaze.PridejDrazbu(d);
                        drazebniDatabaze.PridejDrazbu(d2);

                        
                        Console.WriteLine(drazebniDatabaze.AktualniDrazba);
                        drazebniDatabaze.DrazbaSkoncila();
                        Console.WriteLine(drazebniDatabaze.AktualniDrazba);
                        drazebniDatabaze.DrazbaSkoncila();


        }

    }
}