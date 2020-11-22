using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace Drazebni_databaze
{
    public class Drazba
    {
        public Auto drazeneAuto;
        public Stack<Nabidka> prihozy;
        public bool drazbaBezi = true;
        private string popis;
        public int id;

        public Int32 ID
        {
            get => id;
            set { id = value; }
        }

        public string Popis
        {
            get 
            {
                return popis;
            }

            set
            {

                if (validDescription(value).Count == 0)
                {
                    popis = value;
                }
                else
                {
                    Console.WriteLine("Neplatny popis duvody nize:");
                    foreach (var chyba in validDescription(value))
                    {
                        if (chyba != null){
                            Console.WriteLine($"Popis obsahuje {chyba}");
                        }
                    }
                }
                    
            }
        }
        

        public Drazba(Auto drazeneAuto,string popis)
        {
            this.drazeneAuto = drazeneAuto;
            this.prihozy = new Stack<Nabidka>();
            this.Popis = popis;
        }

        public List<string> validDescription(string popis)
        {
            Dictionary<string,string> patterny = new Dictionary<string, string>();
            patterny.Add("email", @"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)");
            patterny.Add("adresu", @"[A-z]+\d{1,3}");
            patterny.Add("telefon", @"[0-9]{9}");
            patterny.Add("webovou stranku", @"(www|http:|https:)+[^\s]+[\w]");

            List<string> nesplnene = new List<string>();
            

            foreach (KeyValuePair<string, string> entry in patterny)
            {
                Regex match = new Regex(entry.Value);
                if (match.IsMatch(popis))
                {
                    nesplnene.Add(entry.Key);
                }
                
            }

            return nesplnene;
        }

        public void pridej(Nabidka n)
        {
            NabidkaDAO dao = new NabidkaDAO();
            if (prihozy.Count == 0)
            {
                prihozy.Push(n);
            }
            else {
                if (n.castka > prihozy.Peek().castka)
                {
                    prihozy.Push(n);
                    dao.Create(n, this);
                }
                else
                {
                    Console.WriteLine("Tato nabidka je mensi");
                }
            }
            
        }


        public void konecDrazby()
        {
            this.drazbaBezi = false;
        }

        public override string ToString()
        {
            return $"{drazeneAuto}";
        }

        
    }
}
