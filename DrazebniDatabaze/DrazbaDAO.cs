using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Drazebni_databaze
{
    class DrazbaDAO
    {
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
    }
}
