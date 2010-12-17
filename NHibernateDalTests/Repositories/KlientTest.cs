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
        Repository<Klient> _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new Repository<Klient>();
        }

        [Test]
        public void CanAddKlient()
        {
            Klient klient = new Klient() { Imie = "Stefan" };
            Assert.That(_repository.GetCount(), Is.EqualTo(3));
            _repository.Add(klient);
            Assert.That(_repository.GetCount(), Is.EqualTo(4));
            var testedKlient = _repository.GetById(_repository.GetCount());
            Assert.That(testedKlient.Imie, Is.EqualTo("Stefan"));
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
        public void CanGetKlientRodzaj()
        {
            var testedKlient = _repository.GetById(1);
            Assert.That(testedKlient.Imie, Is.EqualTo("Michal"));
            Assert.That(testedKlient.Rodzaj.Rodzaj, Is.Not.Null);
            Assert.That(testedKlient.Rodzaj.Rodzaj, Is.EqualTo("Kupujacy"));

            testedKlient = _repository.GetById(2);
            Assert.That(testedKlient.Imie, Is.EqualTo("Kamil"));
            Assert.That(testedKlient.Rodzaj.Rodzaj, Is.Not.Null);
            Assert.That(testedKlient.Rodzaj.Rodzaj, Is.EqualTo("Kupujacy"));

            testedKlient = _repository.GetById(3);
            Assert.That(testedKlient.Imie, Is.EqualTo("Stefan"));
            Assert.That(testedKlient.Rodzaj.Rodzaj, Is.Not.Null);
            Assert.That(testedKlient.Rodzaj.Rodzaj, Is.EqualTo("Sprzedajacy"));
        }

        [Test]
        public void CanGetKlientRola()
        {
            var testedKlient = _repository.GetById(1);
            Assert.That(testedKlient.Imie, Is.EqualTo("Michal"));
            Assert.That(testedKlient.LoginRola.Rola, Is.Not.Null);
            Assert.That(testedKlient.LoginRola.Rola, Is.EqualTo("Administrator"));

            testedKlient = _repository.GetById(2);
            Assert.That(testedKlient.Imie, Is.EqualTo("Kamil"));
            Assert.That(testedKlient.LoginRola.Rola, Is.Not.Null);
            Assert.That(testedKlient.LoginRola.Rola, Is.EqualTo("Uzytkownik"));

            testedKlient = _repository.GetById(3);
            Assert.That(testedKlient.Imie, Is.EqualTo("Stefan"));
            Assert.That(testedKlient.LoginRola.Rola, Is.Not.Null);
            Assert.That(testedKlient.LoginRola.Rola, Is.EqualTo("Uzytkownik"));
        }

        [Test]
        public void CanFilterByCity()
        {
            var testedKlients = _repository.GetByFilter("Miasto","Wrocław");
            Assert.That(testedKlients.Count, Is.EqualTo(1));
            testedKlients = _repository.GetByFilter("Miasto", "Twardogóra");
            Assert.That(testedKlients.Count, Is.EqualTo(3));
            testedKlients = _repository.GetByFilter("Miasto", "Pcim Dolny");
            Assert.That(testedKlients.Count, Is.EqualTo(0));
        }

    }

}
