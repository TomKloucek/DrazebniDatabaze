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

        public DrazbaDAO drazbaDAO = new DrazbaDAO();
        public NabidkaDAO nabidkaDAO = new NabidkaDAO();
        public AutoDao autoDAO = new AutoDao();

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
            return drazbaDAO.Read(id);
        }

        public void RemoveDrazba(string popis)
        {
            drazbaDAO.Delete(popis);
        }

        public void Update(Drazba drazba)
        {
            drazbaDAO.Update(drazba);
        }

        public void Save(Drazba drazba)
        {
            drazbaDAO.Create(drazba);
        }

       


    }
}