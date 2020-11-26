using NUnit.Framework;
using Drazebni_databaze;
using System;

namespace Unit_Test
{
    class UnitTestDrazba
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestDrazbaKonstruktor()
        {
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);
            Drazba d = new Drazba(a,"popis");

            Assert.AreEqual(a, d.drazeneAuto);
            Assert.IsTrue(d.drazbaBezi);
        }

        [Test]
        public void TestPrihazovani()
        {
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);
            Drazba d = new Drazba(a, "popis");

            DatabazeUzivatelu db =  DatabazeUzivatelu.Instance;
            //var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", "spatny@frajer.com", "Pri 25", "605 897 123");
            Nabidka n = new Nabidka(u, 50);

            d.pridej(n);

            Assert.AreEqual(n, d.prihozy.Peek());

            Nabidka mensi = new Nabidka(u, 15);

            d.pridej(mensi);

            Assert.AreEqual(n, d.prihozy.Peek());
            Assert.AreEqual(1, d.prihozy.Count);

            Nabidka vetsi = new Nabidka(u, 150);

            d.pridej(vetsi);

            Assert.AreEqual(vetsi, d.prihozy.Peek());
            Assert.AreEqual(2, d.prihozy.Count);
        }

        [Test]
        public void TestKonecDrazby()
        {
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);
            Drazba d = new Drazba(a, "popis");

            d.konecDrazby();

            Assert.IsFalse(d.drazbaBezi);
        }
    }
}
