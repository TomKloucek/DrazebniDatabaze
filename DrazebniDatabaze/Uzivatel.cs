using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Drazebni_databaze
{
    public class Uzivatel
    {
        private int id;
        private string jmeno;
        private string heslo;
        //private IValidator<string> validName = SimpleSetValidator.GetInstance(DatabazeUzivatelu.Instance.uzivatele);
        private string adresa;
        private string telefon;
        private string email;

        public int Id
        {
            get => id;
            set { id = value; }
        }

        public string Jmeno { 
            get => jmeno;
            set {
                try
                {
                    //    validName.Validate(value);
                    DatabazeUzivatelu db = DatabazeUzivatelu.Instance;
                    if (!db.Contains(this)) { jmeno = value; }
                    else { throw new Exception("Toto jmeno uz v databazi je"); }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } 
        }

        public string Heslo
        {
            get => heslo;
            set
            {
                try
                {
                    if (strongPassword(value))
                    {
                        heslo = value;
                    }
                }
                catch
                {
                    Console.WriteLine($"Heslo je slabe u uzivatele {this.Jmeno}");
                }
            }
        }

        public string Email
        {
            get => email;
            set
            {
                try
                {
                    if (isEmail(value))
                    {
                        email = value;
                    }
                }
                catch
                {
                    Console.WriteLine($"Zadana emailova adresa u uzivatele {this.Jmeno} neni platna");
                }
            }
        }

        public string Telefon
        {
            get => telefon;
            set
            {
                try
                {
                    if (isTelephone(value))
                    {
                        telefon = value;
                    }
                }
                catch
                {
                    Console.WriteLine($"Zadany telefon u uzivatele {this.Jmeno} neodpovida formatu XXX XXX XXX");
                }
            }
        }

        public string Adresa
        {
            get => adresa;
            set
            {
                try
                {
                    if (isAdresa(value))
                    {
                        adresa = value;
                    }
                }
                catch
                {
                    Console.WriteLine($"Adresa u uzivatele {this.Jmeno} neodpovida formatu adresy");
                }
            }
        }


        public bool strongPassword(string heslo)
        {
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
            Regex match = new Regex(pattern);
            if (match.IsMatch(heslo))
            {
                return true;
            }
            else
            {
                throw new Exception("Slabe heslo");
            }
        }

        public bool isEmail(string mail)
        {
            string pattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
            Regex match = new Regex(pattern);
            if (match.IsMatch(mail))
            {
                return true;
            }
            else
            {
                throw new Exception("Nezadal jsi email");
            }
        }

        public bool isTelephone(string phone)
        {
            string pattern = @"^(\+?420)? ?[0-9]{3} ?[0-9]{3} ?[0-9]{3}$";
            Regex match = new Regex(pattern);
            if (match.IsMatch(phone))
            {
                return true;
            }
            else
            {
                throw new Exception("Nezadal jsi tel cislo ve spravnem formatu");
            }
        }

        public bool isAdresa(string adresa)
        {
            string pattern = @"^[A-z]+\s\d{2}$";
         
                     Regex match = new Regex(pattern);
            if (match.IsMatch(adresa))
            {
                return true;
            }
            else
            {
                throw new Exception("Nezadal jsi adresu ve spravnem formatu");
            }
        }

        public Uzivatel(string jmeno, string heslo, string email, string adresa, string telefon)
        {
            this.Jmeno = jmeno;
            this.Heslo = heslo;
            this.Email = email;
            this.Adresa = adresa;
            this.Telefon = telefon;
           
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Uzivatel u = (Uzivatel)obj;
                return (this.jmeno == u.jmeno);
            }
        }

        public override string ToString()
        {
            return $"Jmeno: {this.Jmeno}, Email:{this.Email}";
        }

    }
}
