using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Drazebni_databaze
{
    /// <summary>
    /// Trida vytvari objekt DAO ktery slouzi pro operace na databazi
    /// s objekty typu auto
    /// </summary>
    public class AutoDao
    {
        /// <summary>
        /// Fuknce na ziskani id zadaneho auta
        /// </summary>
        /// <param name="a">Objekt auto u ktereho hledame id</param>
        /// <returns>Pokud nalezne dotazovane auto v db, pak vrati jeho id, v pripade ze ne vrati 0</returns>
        public int GetID(Auto a)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM car WHERE jmeno = @jmeno AND vykon = @vykon AND delka = @delka", conn))
            {
                command.Parameters.Add(new SqlParameter("@jmeno", a.Jmeno));
                command.Parameters.Add(new SqlParameter("@vykon", a.Vykon));
                command.Parameters.Add(new SqlParameter("@delka", a.Delka));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return Int32.Parse(reader[0].ToString());
                }
                reader.Close();
                return 0;
            }
        }

        /// <summary>
        /// Vraci auto s odpovidajicim id v databazi
        /// </summary>
        /// <param name="id">id auta ktere hledame v db</param>
        /// <returns>Vraci nalezene auto v db</returns>
        public Auto getByID(int id)
        {
            Auto auto = null;
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM car WHERE id = @Id", conn))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    auto = new Auto(
                        jmeno: reader[1].ToString(),
                        vykon: Int32.Parse(reader[2].ToString()),
                        delka: float.Parse(reader[3].ToString()),
                        datumVydani: DateTime.Parse(reader[4].ToString()),
                        skupina: (Skupina)Enum.Parse(typeof(Skupina), reader[5].ToString()));
                }
                reader.Close();
                return auto;
            }
        }

        /// <summary>
        /// Updatne auto na serveru podle atributu zadaneho auta
        /// </summary>
        /// <param name="a">Auto ktere chceme aktualizovat</param>
        public void Update(Auto a)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();
            SqlCommand command = null;

            using (command = new SqlCommand("UPDATE car SET jmeno=@jmeno,vykon=@vykon,delka=@delka,datum=@datum,skupina=@skupina where jmeno = @jmeno and delka = @delka and @vykon = vykon", conn))
            {
                command.Parameters.Add(new SqlParameter("@jmeno", a.Jmeno));
                command.Parameters.Add(new SqlParameter("@vykon", a.Vykon));
                command.Parameters.Add(new SqlParameter("@delka", a.Delka));
                command.Parameters.Add(new SqlParameter("@datum", a.DatumVydani));
                command.Parameters.Add(new SqlParameter("@skupina",a.Skupina));
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Odebere zadane auto z db
        /// </summary>
        /// <param name="a">Auto ktere chceme z db smazat</param>
        public void Remove(Auto a)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM car WHERE id = @id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", this.GetID(a)));
                command.ExecuteNonQuery();
            }
        }

        public void Create(Auto a)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();
            SqlCommand command = null;
            using (command = new SqlCommand("INSERT INTO car(jmeno,vykon,delka,datum,skupina) VALUES (@jmeno,@vykon,@delka,@datum,@skupina)", conn))
            {

                command.Parameters.Add(new SqlParameter("@jmeno", a.Jmeno));
                command.Parameters.Add(new SqlParameter("@vykon", a.Vykon));
                command.Parameters.Add(new SqlParameter("@delka", a.Delka));
                command.Parameters.Add(new SqlParameter("@datum", a.DatumVydani));
                command.Parameters.Add(new SqlParameter("@skupina", a.Skupina));
                command.ExecuteNonQuery();
                command.CommandText = "Select @@Identity";
                a.ID = Convert.ToInt32(command.ExecuteScalar());
            }
        }


    }
}
