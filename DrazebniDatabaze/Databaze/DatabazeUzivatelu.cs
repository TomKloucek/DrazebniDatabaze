using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Drazebni_databaze
{
 
    /// <summary>
    /// Autor: Tomas Kloucek 
    /// Trida je singleton a obstarava unikatnost uzivatelu v ni
    ///  Poskytuje veskere metody pro operace s uzivateli
    /// </summary>
    public class DatabazeUzivatelu
    {
        private UzivatelProxy proxy = new UzivatelProxy();
        private static DatabazeUzivatelu _instance = null;

        /// <summary>
        /// Konstruktor na vytvoreni databaze uzivatelu
        /// </summary>
        private DatabazeUzivatelu()
        {
            this.uzivatele = new List<Uzivatel>();
        }
        
        /// <summary>
        /// Metoda navrhoveho vzoru Singleton, muze byt pouze jedna instance
        /// </summary>
        public static DatabazeUzivatelu Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabazeUzivatelu();
                }
                return _instance;
            }
        }
        
        /// <summary>
        /// List uzivatelu kteri jsou aktualne ulozeni
        /// </summary>
        public List<Uzivatel> uzivatele;
        
        /// <summary>
        /// Pridava uzivatele do listu uzivatelu ve tride, zaroven ulozi uzivatele na SQL Server
        /// </summary>
        /// <param name="novyUzivatel">Instance pridavaneho uzivatele</param>     
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
            catch(Exception err)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Jmeno uzivatele nesmi byt null");
                Console.ResetColor();
            }
            
        }
        
        /// <summary>
        ///  Metoda pro overeni, zda je uzivatel aktualne v listu
        /// </summary>
        /// <param name="uzivatel">Uzivatel u ktereho chceme zjistit zda v listu je</param>
        /// <returns>Vraci true pokud obsahuje, vraci false pokud neobsahuje</returns>
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
        
        /// <summary>
        /// Metoda nacteni uzivatelu, kteri jsou ulozeni na SQL Serveru
        /// </summary>
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
        
        /// <summary>
        /// Metoda na ulozeni uzivatele na SQL Server, vyuziva tridu proxy
        /// </summary>
        /// <param name="uzivatel"></param>
        public void Save(Uzivatel uzivatel)
        {
            proxy.Create(uzivatel);
        }
        
        /// <summary>
        /// Metoda na aktualizaci uzivatele, vyuziva tridu proxy
        /// </summary>
        /// <param name="uzivatel">Uzivatel ktereho chceme na serveru aktualizovat</param>
        public void Update(Uzivatel uzivatel)
        {
            proxy.Update(uzivatel);
        }

        /// <summary>
        /// Metoda na odebrani uzivatele ze serveru, vyuziva tridu proxy
        /// </summary>
        /// <param name="id">id uzivatele ktereho chceme ze serveru smazat</param>
        public void Remove(int id)
        {
            proxy.Remove(id);
        }

        /// <summary>
        /// Vypise vsechny uzivatele z listu uzivatelu
        /// </summary>
        public void VypisUzivatelu()
        {
            Console.WriteLine("\n");
            foreach (var uzivatel in uzivatele)
            {
                Console.WriteLine(uzivatel);
            }
        }

        /// <summary>
        /// Metoda na ziskani uzivatele podle id z databaze
        /// </summary>
        /// <param name="id">Id uzivatele na serveru ktereho chceme ziskat</param>
        /// <returns>Vraci uzivatele ktereho ziskame ze serveru</returns>
        public Uzivatel GetById(int id)
        {
            return proxy.GetById(id);
        }
    }
}
