﻿using System;
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
            Produkty produktTest = _repository.GetById(4);
            Assert.That(produktTest.Nazwa, Is.EqualTo("Salata"));
            Assert.That(produktTest.Ilosc, Is.EqualTo(10));
            Assert.That(produktTest.Cena, Is.EqualTo(10));
        }

        [Test]
        public void CanUpdateProdukt()
        {
            Produkty produktTest = _repository.GetById(1);
            produktTest.Nazwa = "BananLepszy";
            _repository.Update(produktTest);
            produktTest = _repository.GetById(1);
            Assert.That(produktTest.Nazwa, Is.EqualTo("BananLepszy"));
            produktTest.Nazwa = "Banan";
            _repository.Update(produktTest);
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
            Assert.That(produkt.Count, Is.EqualTo(1));

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

        [Test]
        public void CanGetAllDostepneProdukty()
        {
            var products = _repository.GetAllDostepneProdukty();
            Assert.That(products.Count, Is.EqualTo(4));
        }

    }
}
