using NUnit.Framework;
using Drazebni_databaze;
using System;

namespace Unit_Test
{
    class UnitTestNabidka
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestNabidkaKonstruktor()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");
            Nabidka n = new Nabidka(u, 50);

            Assert.AreEqual(u, n.prihazujici);
            Assert.AreEqual(50, n.castka);
        }

        [Test]
        public void TestNabidkaToString()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");
            Nabidka n = new Nabidka(u, 50);

            Assert.AreEqual("Uzivatel karel prihazuje 50", n.ToString());
        }
    }
}
