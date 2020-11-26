using System.Collections.Generic;

namespace Drazebni_databaze
{
    /// <summary>
    /// Autor: Tomas Kloucek
    /// Trida vyuziva navrhovy vzor proxy, ma cache uzivatelu
    /// Aby se zbytecne nedotazovala na Server vicekrat
    /// </summary>
    class UzivatelProxy
    {
        private Dictionary<int,Uzivatel> uzivatele = new Dictionary<int,Uzivatel>();
        private UzivatelDao dao = new UzivatelDao();

        /// <summary>
        /// Metoda vyuziva tridu UzivatelDAO a jeji metodu GetById
        /// Pokud jeste uzivatel neni v cache tak se dotaze a nasledne ulozi do cache
        /// </summary>
        /// <param name="id">Id uzivatele ktereho hledame</param>
        /// <returns>Vraci ziskaneho uzivatele, nebo uzivatele ktery uz je ulozen v cache</returns>
        public Uzivatel GetById(int id)
        {
            if (!uzivatele.ContainsKey(id))
            {
                uzivatele[id] = dao.GetById(id);
            }
            return uzivatele[id];
        }

        /// <summary>
        /// Metoda vyuziva tridu UzivatelDAO a jeji metodu GetId
        /// Ziska id uzivatele a rovnou ho ulozi s klicem[id] do cache
        /// </summary>
        /// <param name="u">Uzivatel ktereho id chceme zjistit</param>
        /// <returns>Vraci id uzivatele</returns>
        public int GetId(Uzivatel u)
        {
        int id = dao.UzivatelID(u);
            uzivatele[id] = u;
            return id;
        }

        /// <summary>
        /// Metoda vyuziva tridu UzivatelDAO a jeji metodu Update
        /// Aktualizuje uzivatele a smaze jeho zaznam z cache, protoze neni aktualni
        /// </summary>
        /// <param name="u">Uzivatel ktereho chceme aktualizovat</param>
        public void Update(Uzivatel u)
        {
            uzivatele.Remove(u.Id);
            dao.Update(u);
        }

        /// <summary>
        /// Metoda vyuziva tridu UzivatelDAO a jeji metodu Remove
        /// Smaze uzivatele z databaze i jeho zaznam z cache pameti
        /// </summary>
        /// <param name="id">Id podle ktereho budeme uzivatele mazat</param>
        public void Remove(int id)
        {
            uzivatele.Remove(id);
            dao.Remove(id);
        }

        /// <summary>
        /// Metoda vyuziva tridu UzivatelDAO a jeji metodu Create
        /// Slouzi k vytvoreni uzivatele na Server, do cache se nic nepridava
        /// </summary>
        /// <param name="u">Uzivatel ktereho pridame do db</param>
        public void Create(Uzivatel u)
        {
            dao.Create(u);
        }
    }
}
