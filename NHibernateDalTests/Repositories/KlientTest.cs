using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BazaDanych.Repositories;
using BazaDanych.Entities;

namespace Tests.Repositories
{
    [TestFixture]
    public class KlientTest
    {
        KlientRepository _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new KlientRepository();
        }

        [Test]
        public void CanAddAndDeleteKlient()
        {
            Klient klient = new Klient() { Imie = "Stefan" };
            int beforeCounter = _repository.GetCount();
            _repository.Add(klient);
            Assert.That(_repository.GetCount(), Is.EqualTo(beforeCounter+1));
            _repository.Remove(klient);
            Assert.That(_repository.GetCount(), Is.EqualTo(beforeCounter));
        }

        [Test]
        public void CanGetKlientByID()
        {
            var testedKlient = _repository.GetById(1);
            Assert.That(testedKlient.Imie, Is.EqualTo("Michal"));

            testedKlient = _repository.GetById(2);
            Assert.That(testedKlient.Imie, Is.EqualTo("Kamil"));

            testedKlient = _repository.GetById(3);
            Assert.That(testedKlient.Imie, Is.EqualTo("Stefan"));
        }

        [Test]
        public void CanUpdateKlient()
        {
            var testedKlient = _repository.GetById(1);
            testedKlient.Imie = "Stefcio";
            _repository.Update(testedKlient);
            testedKlient = _repository.GetById(1);
            Assert.That(testedKlient.Imie, Is.EqualTo("Stefcio"));
            testedKlient.Imie = "Michal";
            _repository.Update(testedKlient);
            testedKlient = _repository.GetById(1);
            Assert.That(testedKlient.Imie, Is.EqualTo("Michal"));
        }

        [Test]
        public void CanDeleteKlient()
        {
            Klient klient = new Klient() { Imie = "Stefan" };
            var klienciBeforeAdd = _repository.GetCount();

            _repository.Add(klient);

            var klienciAfterUpdate = _repository.GetCount();
            Assert.That(klienciAfterUpdate, Is.EqualTo(klienciBeforeAdd+1));

            _repository.Remove(klient);

            var klienciAfterDelete = _repository.GetCount();
            Assert.That(klienciAfterDelete, Is.EqualTo(klienciBeforeAdd));
        }


        [Test]
        public void CanFilterByCity()
        {
            var testedKlients = _repository.GetByFilter("Miasto","Wroclaw");
            Assert.That(testedKlients.Count, Is.EqualTo(0));
            testedKlients = _repository.GetByFilter("Miasto", "Twardogora");
            Assert.That(testedKlients.Count, Is.EqualTo(3));
            testedKlients = _repository.GetByFilter("Miasto", "Pcim Dolny");
            Assert.That(testedKlients.Count, Is.EqualTo(0));
        }

        [Test]
        public void CanGetByImieNazwisko()
        {
            var klient = _repository.GetByImieNazwisko("Michal","Franc");

            Assert.That(klient.Imie,Is.EqualTo("Michal"));
            Assert.That(klient.Nazwisko, Is.EqualTo("Franc"));

             klient = _repository.GetByImieNazwisko("Kamil","Minda");

             Assert.That(klient.Imie, Is.EqualTo("Kamil"));
             Assert.That(klient.Nazwisko, Is.EqualTo("Minda"));

             klient = _repository.GetByImieNazwisko("Stefan","Romanski");

             Assert.That(klient.Imie, Is.EqualTo("Stefan"));
             Assert.That(klient.Nazwisko, Is.EqualTo("Romanski"));
        }

        [Test]
        public void CanGetByCompanyName()
        {
            var testedKlients = _repository.GetByFilter("Firma", "Contium");
            Assert.That(testedKlients.Count, Is.EqualTo(1));
            testedKlients = _repository.GetByFilter("Firma", "PWR");
            Assert.That(testedKlients.Count, Is.EqualTo(1));
            testedKlients = _repository.GetByFilter("Firma", "IBM");
            Assert.That(testedKlients.Count, Is.EqualTo(1));
        }


        [Test]
        public void HasAccesToZamowienia()
        {
            var klient = _repository.GetByImieNazwisko("Michal", "Franc");

            Assert.That(klient.Zamowienia,Is.Not.Null);
            Assert.That(klient.Zamowienia.Count,Is.EqualTo(2));

            klient = _repository.GetByImieNazwisko("Kamil", "Minda");

            Assert.That(klient.Zamowienia, Is.Not.Null);
            Assert.That(klient.Zamowienia.Count, Is.EqualTo(1));
        }

        [Test]
        public void HasAccesToZamowieniaZrealizowane()
        {
            var klient = _repository.GetByImieNazwisko("Michal", "Franc");

            Assert.That(klient.ZamowieniaZrealizowane, Is.Not.Null);
            Assert.That(klient.ZamowieniaZrealizowane.Count, Is.EqualTo(1));

            klient = _repository.GetByImieNazwisko("Kamil", "Minda");

            Assert.That(klient.ZamowieniaZrealizowane, Is.Not.Null);
            Assert.That(klient.ZamowieniaZrealizowane.Count, Is.EqualTo(0));
        }

        [Test]
        public void HasAccesToZamowieniaNieZrealizowane()
        {
            var klient = _repository.GetByImieNazwisko("Michal", "Franc");

            Assert.That(klient.ZamowieniaNieZrealizowane, Is.Not.Null);
            Assert.That(klient.ZamowieniaNieZrealizowane.Count, Is.EqualTo(1));

            klient = _repository.GetByImieNazwisko("Kamil", "Minda");

            Assert.That(klient.ZamowieniaNieZrealizowane, Is.Not.Null);
            Assert.That(klient.ZamowieniaNieZrealizowane.Count, Is.EqualTo(1));
        }
    }

}
