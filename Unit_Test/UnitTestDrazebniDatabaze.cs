using NUnit.Framework;
using Drazebni_databaze;
using System;

namespace Unit_Test
{
    class UnitTestDrazebniDatabaze
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestPridej()
        {
            DrazebniDatabaze d = new DrazebniDatabaze();
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);

            d.Pridej(a);

            if (!d.list.Contains(a))
            {
                Assert.Fail();
            }
            if (d.list.Count != 1)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestHledej()
        {
            DrazebniDatabaze d = new DrazebniDatabaze();
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);

            d.Pridej(a);

            Assert.AreEqual(a, d.HledejPodleJmena("bmw"));
        }

        [Test]
        public void TestFungovaniDrazeb()
        {
            DrazebniDatabaze d = new DrazebniDatabaze();
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);
            Auto a1 = new Auto("skoda", Skupina.A, datum, 650, 3);

            Drazba d1 = new Drazba(a, "Popis");

            d.PridejDrazbu(d1);

            if (d.aktualniDrazba != d1)
            {
                Assert.Fail();
            }

            Drazba d2 = new Drazba(a1, "popis");

            d.PridejDrazbu(d2);

            if (d.frontaDrazeb.Count != 2)
            {
                Assert.Fail();
            }

            if (!d.frontaDrazeb.Contains(d2))
            {
                Assert.Fail();
            }

            d.DrazbaSkoncila();

            Assert.AreEqual(d2, d.aktualniDrazba);

            if (!d.ukonceneDrazby.Contains(d1))
            {
                Assert.Fail();
            }
            
        }
    }
}
