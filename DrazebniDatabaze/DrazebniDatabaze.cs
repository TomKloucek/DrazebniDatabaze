using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Drazebni_databaze
{
    public class DrazebniDatabaze
    {
        public SortedSet<Auto> list;
        public Drazba aktualniDrazba;
        public Queue<Drazba> frontaDrazeb;
        public LinkedList<Drazba> ukonceneDrazby;

        public Queue<Drazba> FrontaDrazeb { get => frontaDrazeb; set => frontaDrazeb = value; }
        public LinkedList<Drazba> UkonceneDrazby { get => ukonceneDrazby; set => ukonceneDrazby = value; }
        public Drazba AktualniDrazba { get => aktualniDrazba; set => aktualniDrazba = value; }

        public DrazebniDatabaze()
        {
            this.list = new SortedSet<Auto>();
            this.FrontaDrazeb = new Queue<Drazba>();
            this.AktualniDrazba = null;
            this.UkonceneDrazby = new LinkedList<Drazba>();
        }

        public void Pridej(Auto auto)
        {
            list.Add(auto);
        }

        public void Vypis()
        {
            foreach (Auto auto in list)
            {
                Console.WriteLine(auto);
            }

        }

        public Auto HledejPodleJmena(string jmeno)
        {
            foreach (Auto auto in list)
            {
                if (auto.Jmeno == jmeno)
                {
                    return auto;
                }
                
            }
            return null;
      
        }

        public void PridejDrazbu(Drazba d)
        {
            if (FrontaDrazeb.Count == 0)
            {
                AktualniDrazba = d;
            }
            FrontaDrazeb.Enqueue(d);
            Save(d);
        }

        public void DrazbaSkoncila()
        {
            Console.WriteLine($"Vec: {AktualniDrazba.drazeneAuto} vyhrava {AktualniDrazba.prihozy.Peek().prihazujici}");
            AktualniDrazba.drazbaBezi = false;
            Update(AktualniDrazba);
            FrontaDrazeb.Dequeue();
            UkonceneDrazby.AddLast(AktualniDrazba);
            try
            {
                AktualniDrazba = FrontaDrazeb.Peek();
            }catch(Exception err)
            {
                Console.WriteLine("Drazby jsou aktualne prazdne");
            }
        }

        public Drazba GetByID(int id)
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
                        drazeneAuto: auto.getByID(Int32.Parse(reader[3].ToString())),
                        popis: reader[1].ToString());
                }
                reader.Close();
                return drazba;
            }
        }

        public void RemoveDrazba(string popis)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();

            using (SqlCommand command = new SqlCommand("DELETE FROM drazba WHERE popis = @popis", conn))
            {
                command.Parameters.Add(new SqlParameter("@jmeno", popis)); // Uvazujeme-li ze kazda drazba ma unikatni popis
                command.ExecuteNonQuery();
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
                    command.Parameters.Add(new SqlParameter("@id",drazba.ID));
                    command.Parameters.Add(new SqlParameter("@popis", drazba.Popis));
                    int autoId = GetID(drazba.drazeneAuto);
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
                catch(Exception err)
                {
                    Console.WriteLine(err.Message);
                }
                command.ExecuteNonQuery();
            }
        }

        public void Save(Drazba drazba)
        {
            SqlConnection conn = DatabaseConnection.GetInstance();
            SqlCommand command = null;
            using (command = new SqlCommand("INSERT INTO drazba(popis,drazbaBezi,car_id) VALUES (@popis,@drazbaBezi,@car_id)", conn))
            {
                int autoId = GetID(drazba.drazeneAuto);
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

        public int GetID(Auto a)
       {
            AutoDAO dao = new AutoDAO();
            return dao.GetID(a);

        }


    }
}