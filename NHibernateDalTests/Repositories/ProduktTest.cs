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
        private ProduktRepository _repository;
        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new ProduktRepository();
        }

        [Test]
        public void CanGetProdukt()
        {
            var count = _repository.GetCount() - 1;
            Produkty produktTest = _repository.GetById(count);
            Assert.That(produktTest.Nazwa, Is.EqualTo("BananLepszy"));
            Assert.That(produktTest.Ilosc, Is.EqualTo(10));
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
            Assert.That(produktTest.Nazwa, Is.EqualTo("Pomidor"));
            Assert.That(produktTest.Ilosc, Is.EqualTo(150));
            Assert.That(produktTest.Cena, Is.EqualTo(25));
        }

        [Test]
        public void CanGetProduktProducent()
        {
            var count = _repository.GetCount();
            Produkty produktTestA = _repository.GetById(1);
            Produkty produktTestB = _repository.GetById(2);

            Assert.That(produktTestA.Producent.Nazwa, Is.EqualTo("Polmos"));
            Assert.That(produktTestB.Producent.Nazwa, Is.EqualTo("KFC"));
        }

        [Test]
        public void CanGetProduktByName()
        {
            var produkt = _repository.GetByNazwaLike("Banan");
            Assert.That(produkt.Count, Is.EqualTo(2));

            produkt = _repository.GetByNazwaLike("Jajka");
            Assert.That(produkt.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanGetProduktByProducent()
        {
            var products = _repository.GetByProducent("KFC");
            Assert.That(products.Count, Is.EqualTo(2));

            products = _repository.GetByProducent("Polmos");
            Assert.That(products.Count, Is.EqualTo(3));
        }

        [Test]
        public void CanGetInsufficentQuantityProducts()
        {
            var products = _repository.GetInsufficentProducts();
            Assert.That(products.Count, Is.EqualTo(4));
        }


    }
}
