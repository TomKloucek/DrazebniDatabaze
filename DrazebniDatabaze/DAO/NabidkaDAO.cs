using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Drazebni_databaze
{
    public class NabidkaDAO
    {
        public int GetID(Nabidka n)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM nabidka WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", n.ID));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return Int32.Parse(reader[0].ToString());
                }
                reader.Close();
                return 0;
            }
        }

        public void Create(Nabidka n,Drazba d)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();
            SqlCommand command = null;
            UzivatelDao dao = new UzivatelDao();
            DrazbaDAO daoD = new DrazbaDAO();

            using (command = new SqlCommand("INSERT INTO nabidka(castka,uzivatel_id,drazba_id) VALUES (@castka,@uzivatel_id,@drazba_id)", conn))
            {

                command.Parameters.Add(new SqlParameter("@castka", n.castka));
                command.Parameters.Add(new SqlParameter("@uzivatel_id", dao.UzivatelID(n.prihazujici)));
                command.Parameters.Add(new SqlParameter("@drazba_id", daoD.GetID(d)));
                command.ExecuteNonQuery();
                command.CommandText = "Select @@Identity";
                n.ID = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public Nabidka Read(int id)
        {
            UzivatelDao dao = new UzivatelDao();
            Nabidka n = null;
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM nabidka WHERE id = @Id", conn))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    n = new Nabidka(
                        castka: Int32.Parse(reader[1].ToString()),
                        prihazujici: dao.GetById(Int32.Parse(reader[2].ToString())));
                    n.ID = Int32.Parse(reader[0].ToString());
                }
                reader.Close();
                return n;
            }
        }

        public void Remove(int id)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM nabidka WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }

        public void Update(Nabidka n)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();
            SqlCommand command = null;
            UzivatelDao dao = new UzivatelDao();

            using (command = new SqlCommand("UPDATE uzivatel SET jmeno=@jmeno,heslo=@heslo,adresa=@adresa, where id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", n.ID));
                command.Parameters.Add(new SqlParameter("@castka", n.castka));
                command.Parameters.Add(new SqlParameter("@heslo", dao.UzivatelID(n.prihazujici)));
                command.ExecuteNonQuery();
            }
        }



    }
}
