using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Drazebni_databaze
{
    public class DatabazeUzivatelu
    {
        private static DatabazeUzivatelu instance = null;

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
            SqlConnection conn = DatabaseConnection.GetInstance();
            SqlCommand command = null;

                using (command = new SqlCommand("INSERT INTO uzivatel(jmeno,heslo,adresa,telefon,email) VALUES (@jmeno,@heslo,@adresa,@telefon,@email)", conn))
                {

                    command.Parameters.Add(new SqlParameter("@jmeno", uzivatel.Jmeno));
                    command.Parameters.Add(new SqlParameter("@heslo", uzivatel.Heslo));
                    command.Parameters.Add(new SqlParameter("@adresa", uzivatel.Adresa));
                    command.Parameters.Add(new SqlParameter("@telefon", uzivatel.Telefon));
                    command.Parameters.Add(new SqlParameter("@email", uzivatel.Email));
                    command.ExecuteNonQuery();
                    command.CommandText = "Select @@Identity";
                    uzivatel.Id = Convert.ToInt32(command.ExecuteScalar());
                }
           
        }

        
        public void Update(Uzivatel uzivatel)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();
            SqlCommand command = null;

            using (command = new SqlCommand("UPDATE uzivatel SET jmeno=@jmeno,heslo=@heslo,adresa=@adresa,telefon=@telefon,email=@email where id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", uzivatel.Id));
                command.Parameters.Add(new SqlParameter("@jmeno", uzivatel.Jmeno));
                command.Parameters.Add(new SqlParameter("@heslo", uzivatel.Heslo));
                command.Parameters.Add(new SqlParameter("@adresa", uzivatel.Adresa));
                command.Parameters.Add(new SqlParameter("@telefon", uzivatel.Telefon));
                command.Parameters.Add(new SqlParameter("@email", uzivatel.Email));
                command.ExecuteNonQuery();
            }
        }

        public void Remove(string jmeno)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM uzivatel WHERE jmeno = @jmeno", conn))
            {
                command.Parameters.Add(new SqlParameter("@jmeno", jmeno));
                command.ExecuteNonQuery();
            }
        }

        public void VypisUzivatelu()
        {
            Console.WriteLine("\n");
            foreach (var uzivatel in this.uzivatele)
            {
                Console.WriteLine(uzivatel);
            }
        }

        public Uzivatel GetById(int id)
        {
            UzivatelDAO dao = new UzivatelDAO();
            return dao.GetById(id);
        }
    }
}
