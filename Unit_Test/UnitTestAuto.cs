using NUnit.Framework;
using Drazebni_databaze;
using System;

namespace Unit_Test
{
    public class TestsAuto
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestAutoKonstruktor()
        {
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);
            Assert.AreEqual("bmw",a.Jmeno);
            Assert.AreEqual(3, a.Delka);
            Assert.AreEqual(650,a.Vykon);
            Assert.AreEqual(Skupina.A, a.Skupina);
            Assert.AreEqual(datum, a.DatumVydani);

        }

        [Test]
        public void TestAutoZapornyVykon()
        {
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);
            try
            {
                a.Vykon = -5;
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void TestAutoZapornaDelka()
        {
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);
            try
            {
                a.Delka = -5;
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void TestToString()
        {
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);
            Assert.AreEqual("bmw", a.ToString());
        }

        [Test]
        public void TestCompareTo()
        {
            DateTime datum = DateTime.Now;
            Auto a = new Auto("bmw", Skupina.A, datum, 650, 3);
            Auto b = new Auto("bmw", Skupina.B, datum, 720, 5);
            Auto c = a;
            Auto d = new Auto("skoda", Skupina.B, datum, 720, 5);

            Assert.AreEqual(0,a.CompareTo(b));
            Assert.AreEqual(0, a.CompareTo(c));
            Assert.AreEqual(-1, a.CompareTo(d));
        }


    }
}