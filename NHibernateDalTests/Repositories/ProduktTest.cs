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
    public class ProduktTest
    {
        private Repository<Produkty> _repository;
        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new Repository<Produkty>();
        }

        [Test]
        public void CanGetProdukt()
        {
            var count = _repository.GetCount() - 1;
            Produkty produktTest = _repository.GetById(count);
            Assert.That(produktTest.Nazwa, Is.EqualTo("Banan"));
            Assert.That(produktTest.Ilosc, Is.EqualTo(100));
            Assert.That(produktTest.Cena, Is.EqualTo(10));
        }

        [Test]
        public void CanUpdateProdukt()
        {
            var count = _repository.GetCount()-1;
            Produkty produktTest = _repository.GetById(count);
            produktTest.Nazwa = "BananLepszy";
            _repository.Update(produktTest);
            produktTest = _repository.GetById(count);
            Assert.That(produktTest.Nazwa, Is.EqualTo("BananLepszy"));
        }

        [Test]
        public void CanDeleteProdukt()
        {
            int countBefore = _repository.GetCount();
            Produkty prod = new Produkty() { };
            _repository.Add(prod);
            int countAfter = _repository.GetCount();
            Assert.That(countAfter, Is.EqualTo(countBefore + 1));

            _repository.Remove(prod);
            int countAfterDelete = _repository.GetCount();
            Assert.That(countAfterDelete, Is.EqualTo(countBefore));
        }

        [Test]
        public void CanGetProduktById()
        {
            var count = _repository.GetCount();
            Produkty produktTest = _repository.GetById(count);
            Assert.That(produktTest.Nazwa, Is.EqualTo("Truskawka"));
            Assert.That(produktTest.Ilosc, Is.EqualTo(50));
            Assert.That(produktTest.Cena, Is.EqualTo(5));
        }

        [Test]
        public void CanGetProduktProducent()
        {
            var count = _repository.GetCount();
            Produkty produktTestA = _repository.GetById(count-1);
            Produkty produktTestB = _repository.GetById(count);

            Assert.That(produktTestA.Producent.Nazwa, Is.EqualTo("A"));
            Assert.That(produktTestB.Producent.Nazwa, Is.EqualTo("B"));
        }
    }
}
