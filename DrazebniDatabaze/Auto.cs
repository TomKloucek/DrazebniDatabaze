using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Drazebni_databaze
{
    public enum Skupina
    {
        A, B, C, D, E, F
    }
    public class Auto : IComparable<Auto>
    {
        private int vykon;
        private float delka;
        private string jmeno;
        private Skupina skupina;
        private DateTime datumVydani;

        public int Vykon
        {
            get { return vykon; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("nesmi byt zaporny");
                }
                vykon = value;
            }
        }

        public float Delka
        {
            get { return delka; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("nesmi byt zaporny");
                }
                delka = value;
            }
        }
        public string Jmeno
        {
            get { return jmeno; }
            set { jmeno = value; }
        }

        public Skupina Skupina
        {
            get { return skupina; }
        }

        public DateTime DatumVydani
        {
            get { return datumVydani; }
        }

        public Auto(string jmeno, Skupina skupina, DateTime datumVydani, int vykon, float delka)
        {
            Jmeno = jmeno;
            this.skupina = skupina;
            this.datumVydani = datumVydani;
            Vykon = vykon;
            Delka = delka;
        }

        public override string ToString()
        {
            return jmeno;
        }

        public int CompareTo(Auto other)
        {
            return this.jmeno.CompareTo(other.jmeno);
        }

        public Auto getByID(int id)
        {
            AutoDAO dao = new AutoDAO();
            return dao.getByID(id);
        }

        public void Create(Auto a)
        {

        }

        


    }
}