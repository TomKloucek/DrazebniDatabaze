using NUnit.Framework;
using Drazebni_databaze;
using System;

namespace Unit_Test
{
    public class TestsUzivatel
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void TestUzivatelKonstruktor()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");

            Assert.AreEqual("karel", u.Jmeno);
            Assert.AreEqual("Lilecek1", u.Heslo);
            Assert.AreEqual("spatny@frajer.com", u.Email);
            Assert.AreEqual("Pri 25", u.Adresa);
            Assert.AreEqual("605 897 123", u.Telefon);

        }
        [Test]
        public void TestUzivatelPasswordKratke()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");

            try
            {
                u.strongPassword("karel");
            }
            catch
            {
                Assert.Pass();
            }               

        }
        [Test]
        public void TestUzivatelPasswordIdealni()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");

            try
            {
                u.strongPassword("Louposlav123");
            }
            catch
            {
                Assert.Fail();
            }

        }

        [Test]
        public void TestUzivatelAdresaSpatna()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");

            try
            {
                u.isAdresa("Pri25");
                Assert.Fail();
            }
            catch
            {
            }

        }

        [Test]
        public void TestUzivatelAdresaSpravna()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");

            try
            {
                u.isAdresa("Pribramska 12");
            }
            catch
            {
                Assert.Fail();
            }

        }

        [Test]
        public void TestUzivatelEquals()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");
            Uzivatel u1 = new Uzivatel("karel", "heslo", setValidator, "spatny@email.com", "Adresa 1", "605 258 789");
            Assert.AreEqual(true, u1.Equals(u));
        }
        [Test]
        public void TestTelephoneGood()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");

            try
            {
                u.isTelephone("265 265 265");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestTelephoneBad()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");

            try
            {
                u.isTelephone("265 265 26");
            }
            catch
            {
                Assert.Pass();
            }
        }

        [Test]
        public void TestEmailGood()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");

            try
            {
                u.isEmail("loupak@mail.vz");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestEmailBad()
        {
            DatabazeUzivatelu db = new DatabazeUzivatelu();
            var setValidator = new SimpleSetValidator(db.uzivatele);
            Uzivatel u = new Uzivatel("karel", "Lilecek1", setValidator, "spatny@frajer.com", "Pri 25", "605 897 123");

            try
            {
                u.isEmail("loupakmail.vz");
            }
            catch
            {
                Assert.Pass();
            }
        }
    }
}
