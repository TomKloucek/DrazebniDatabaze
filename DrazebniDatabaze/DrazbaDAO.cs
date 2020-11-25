using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Drazebni_databaze
{
    /// <summary>
    /// Trida vytvari objekt DAO ktery slouzi pro operace na databazi
    /// s objekty typu drazba
    /// </summary>
    public class DrazbaDAO
    {
        /// <summary>
        /// Instance AutoDAO, protoze ji pouzivame v programu casteji
        /// </summary>
        public AutoDAO dao = new AutoDAO();
        /// <summary>
        /// Fuknce na ziskani id zadanehe drazby
        /// </summary>
        /// <param name="d">Objekt drazba u ktereho hledame id</param>
        /// <returns>Pokud nalezne dotazovanou drazbu v db, pak vrati jeji id, v pripade ze ne vrati 0</returns>
        public int GetID(Drazba d)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT id FROM drazba WHERE id=@id", conn))
            {
                command.Parameters.Add(new SqlParameter("@id", d.ID));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return Int32.Parse(reader[0].ToString());
                }
                reader.Close();
                return 0;
            }
        }

        public void Create(Drazba drazba)
        {
            
            SqlConnection conn = DatabaseConnection.GetInstance();
            SqlCommand command = null;
            using (command = new SqlCommand("INSERT INTO drazba(popis,drazbaBezi,car_id) VALUES (@popis,@drazbaBezi,@car_id)", conn))
            {
                int autoId = dao.GetID(drazba.drazeneAuto);
                if (autoId == 0)
                {
                    DBNull id = DBNull.Value;
                    command.Parameters.Add(new SqlParameter("@car_id", id));

                }
                else
                {
                    int id = autoId;
                    command.Parameters.Add(new SqlParameter("@car_id", id));

                }
                command.Parameters.Add(new SqlParameter("@popis", drazba.Popis));
                command.Parameters.Add(new SqlParameter("@drazbaBezi", drazba.drazbaBezi));
                command.ExecuteNonQuery();
                command.CommandText = "Select @@Identity";
                drazba.ID = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public Drazba Read(int id)
        {
            Drazba drazba = null;
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("SELECT * FROM drazba WHERE id = @Id", conn))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = id;

                command.Parameters.Add(param);
                SqlDataReader reader = command.ExecuteReader();
                Auto auto = null;
                while (reader.Read())
                {
                    drazba = new Drazba(
                        drazeneAuto: dao.getByID(Int32.Parse(reader[3].ToString())),
                        popis: reader[1].ToString());
                }
                reader.Close();
                return drazba;
            }
        }

        public void Update(Drazba drazba)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();
            SqlCommand command = null;

            using (command = new SqlCommand("UPDATE drazba SET popis=@popis,drazbaBezi=@drazbaBezi,car_id=@car_id where id = @id", conn))
            {
                try
                {
                    command.Parameters.Add(new SqlParameter("@id", drazba.ID));
                    command.Parameters.Add(new SqlParameter("@popis", drazba.Popis));
                    int autoId = dao.GetID(drazba.drazeneAuto);
                    if (autoId == 0)
                    {
                        DBNull id = DBNull.Value;
                        command.Parameters.Add(new SqlParameter("@car_id", id));

                    }
                    else
                    {
                        int id = autoId;
                        command.Parameters.Add(new SqlParameter("@car_id", id));

                    }
                    command.Parameters.Add(new SqlParameter("@drazbaBezi", drazba.drazbaBezi));
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
                command.ExecuteNonQuery();
            }
        }

        public void Delete(string popis)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM drazba WHERE popis = @popis", conn))
            {
                command.Parameters.Add(new SqlParameter("@jmeno", popis)); // Uvazujeme-li ze kazda drazba ma unikatni popis
                command.ExecuteNonQuery();
            }
        }

        
    }
}
