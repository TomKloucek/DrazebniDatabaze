using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Drazebni_databaze
{
    public class DatabazeUzivatelu
    {
        private static DatabazeUzivatelu instance = null;
        public UzivatelDAO dao = new UzivatelDAO();

        private DatabazeUzivatelu()
        {
            this.uzivatele = new List<Uzivatel>();
        }

        public static DatabazeUzivatelu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabazeUzivatelu();
                }
                return instance;
            }
        }

        public List<Uzivatel> uzivatele;

        public void AddUzivatel(Uzivatel novyUzivatel)
        {
            try
            {
                if (uzivatele.Contains(novyUzivatel) || novyUzivatel.Jmeno.Length <= 1)
                {
                    Console.WriteLine($"Uzivatel se jmenem: {novyUzivatel.Jmeno} uz existuje, nebo nesmi mit prazdne jmeno");
                }
                else
                {
                    uzivatele.Add(novyUzivatel);
                    this.Save(novyUzivatel);
                    Console.WriteLine($"Uzivatel: {novyUzivatel.Jmeno} byl pridan");
                }
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Jmeno uzivatele nesmi byt null");
                Console.ResetColor();
            }
            
        }

        public bool Contains(Uzivatel uzivatel)
        {
            if (uzivatele.Contains(uzivatel))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void NactiUzivatele()
        {
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM uzivatel", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Uzivatel uzivatel = new Uzivatel(
                        jmeno: reader[1].ToString(),
                        heslo: reader[2].ToString(),
                        adresa: reader[3].ToString(),
                        telefon: reader[4].ToString(),
                        email: reader[5].ToString());

                    if (uzivatele.Contains(uzivatel) || uzivatel.Jmeno.Length <= 1)
                    {
                        Console.WriteLine($"Uzivatel se jmenem: {uzivatel.Jmeno} uz existuje, nebo nesmi mit prazdne jmeno");
                    }
                    else
                    {
                        uzivatele.Add(uzivatel);
                        Console.WriteLine($"Uzivatel: {uzivatel.Jmeno} byl pridan");
                    }
                }
                reader.Close();
            }

        }
        public void Save(Uzivatel uzivatel)
        {
            dao.Create(uzivatel);
        }

        
        public void Update(Uzivatel uzivatel)
        {
            dao.Update(uzivatel);
        }

        public void Remove(string jmeno)
        {
            dao.Remove(jmeno);
        }

        public void VypisUzivatelu()
        {
            Console.WriteLine("\n");
            foreach (var uzivatel in uzivatele)
            {
                Console.WriteLine(uzivatel);
            }
        }

        public Uzivatel GetById(int id)
        {
            return dao.GetById(id);
        }
    }
}
