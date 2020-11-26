using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Drazebni_databaze
{
    public class UzivatelDAO
    {
        public Uzivatel GetById(int id)
        {
            Uzivatel uzivatel = null;
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM uzivatel WHERE id = @Id", conn))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    uzivatel = new Uzivatel(
                        jmeno: reader[1].ToString(),
                        heslo: reader[2].ToString(),
                        adresa: reader[3].ToString(),
                        telefon: reader[4].ToString(),
                        email: reader[5].ToString());
                    uzivatel.Id = Int32.Parse(reader[0].ToString());
                }
                reader.Close();
                return uzivatel;
            }
        }

        public int UzivatelID(Uzivatel u)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();
            int id = 0;
            using (SqlCommand command = new SqlCommand("SELECT * FROM uzivatel where jmeno=@jmeno", conn))
            {
                command.Parameters.Add(new SqlParameter("@jmeno", u.Jmeno));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = Int32.Parse(reader[0].ToString());

                }
                reader.Close();
            }
            return id;
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

        public void Remove(int id)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM uzivatel WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id",id));
                command.ExecuteNonQuery();
            }
        }

        public void Create(Uzivatel uzivatel)
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

    }
}
