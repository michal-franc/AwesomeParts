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
    public class PracownikTest
    {
        PracownikRepository _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new PracownikRepository();
        }

        [Test]
        public void CanGetPracownik()
        {
            Pracownik pracownikTest = _repository.GetById(1);
            Assert.That(pracownikTest.Imie, Is.EqualTo("Michał"));
            Assert.That(pracownikTest.Nazwisko, Is.EqualTo("Franc"));
            Assert.That(pracownikTest.Login, Is.EqualTo("LaM"));
        }


        [Test]
        public void CanUpdatePracownik()
        {
            Pracownik pracownikTest = _repository.GetById(1);
            pracownikTest.Imie = "Michal";
            _repository.Update(pracownikTest);
            pracownikTest = _repository.GetById(1);
            Assert.That(pracownikTest.Imie, Is.EqualTo("Michal"));
        }

        [Test]
        public void CanAddAndDeletePracownik()
        {
            int countBefore = _repository.GetCount();
            Pracownik prac = new Pracownik() { };
            _repository.Add(prac);
            int countAfter = _repository.GetCount();
            Assert.That(countAfter, Is.EqualTo(countBefore+1));

            _repository.Remove(prac);
            int countAfterDelete = _repository.GetCount();
            Assert.That(countAfterDelete, Is.EqualTo(countBefore));
        }

        [Test]
        public void CanGetPracownikById()
        {
            Pracownik pracownikTest = _repository.GetById(2);
            Assert.That(pracownikTest.Imie, Is.EqualTo("Stefan"));
            Assert.That(pracownikTest.Nazwisko, Is.EqualTo("Romański"));
            Assert.That(pracownikTest.Login, Is.EqualTo("stefcio"));
        }

        [Test]
        public void CanGetPracownikRola()
        {
            Pracownik pracownikTest0 = _repository.GetById(1);
            Pracownik pracownikTest1 = _repository.GetById(2);

            Assert.That(pracownikTest0.LoginRola.Rola, Is.EqualTo("Uzytkownik"));
            Assert.That(pracownikTest1.LoginRola.Rola, Is.EqualTo("Administrator"));
        }

        [Test]
        public void CanGetPracownikRodzaj()
        {
            Pracownik pracownikTest0 = _repository.GetById(1);
            Pracownik pracownikTest1 = _repository.GetById(2);

            Assert.That(pracownikTest0.Rodzaj.Rodzaj, Is.EqualTo("Magazynier"));
            Assert.That(pracownikTest1.Rodzaj.Rodzaj, Is.EqualTo("Sekretarka"));
        }

        [Test]
        public void CanGetPracownikStatus()
        {
            Pracownik pracownikTest0 = _repository.GetById(1);
            Pracownik pracownikTest1 = _repository.GetById(2);

            Assert.That(pracownikTest0.Status.Status, Is.EqualTo("Urlop"));
            Assert.That(pracownikTest1.Status.Status, Is.EqualTo("Zatrudniony"));
        }

        [Test]
        public void CanFilterByPracownikStatus()
        {
            Assert.Fail();
        }

        [Test]
        public void CanFilterByPracownikRodzaj()
        {
            Assert.Fail();
        }

        [Test]
        public void CanFilterByPracownikLoginRola()
        {
            //LoginRola rolaAdministrator = new LoginRola() { Rola = "Administrator" };
            //LoginRola rolaUzytkownik = new LoginRola() { Rola = "Użytkownik" };
            //LoginRola rolaSprzedawca = new LoginRola() { Rola = "Sprzedawca" };

            var pracownicy = _repository.GetByRola("Administrator");
            Assert.That(pracownicy.Count, Is.EqualTo(1));

            pracownicy = _repository.GetByRola("Uzytkownik");
            Assert.That(pracownicy.Count, Is.EqualTo(1));

            pracownicy = _repository.GetByRola("Sprzedawca");
            Assert.That(pracownicy.Count, Is.EqualTo(2));

            pracownicy = _repository.GetByRola("NieznanaRola");
            Assert.That(pracownicy.Count, Is.EqualTo(0));

        }


        [Test]
        public void CanGetByPracownikImieNazwisko()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetAktualnaUmowa()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetArchiwalneUmowy()
        {
            Assert.Fail();
        }

        [Test]
        public void CanFilterByBeetwenDataZatrdnienia()
        {
            Assert.Fail();
        }

        [Test]
        public void CanFilterByBeetwenDataZwolnienia()
        {
            Assert.Fail();
        }

        [Test]
        public void HasAccesToObslugiwaneZamowienia()
        {
            Assert.Fail();
        }
    }
}
