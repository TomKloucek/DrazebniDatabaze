using System;
using System.Collections.Generic;
using System.Text;

namespace Drazebni_databaze
{
    class UzivatelProxy
    {
        private Dictionary<int,Uzivatel> uzivatele = new Dictionary<int,Uzivatel>();
        private UzivatelDAO dao = new UzivatelDAO();

        public Uzivatel getByID(int id)
        {
            if (!uzivatele.ContainsKey(id))
            {
                uzivatele[id] = dao.GetById(id);
            }
            return uzivatele[id];
        }

        public int getID(Uzivatel u)
        {
        int id = dao.UzivatelID(u);
            uzivatele[id] = u;
            return id;
        }

        public void Update(Uzivatel u)
        {
            uzivatele.Remove(u.Id);
        }

        public void Remove(int id)
        {
            uzivatele.Remove(id);
        }

        public void Create(Uzivatel u)
        {
            dao.Create(u);
        }
    }
}
