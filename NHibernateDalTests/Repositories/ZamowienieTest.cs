using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BazaDanych.Entities;
using BazaDanych.Repositories;

namespace Tests.Repositories
{
    [TestFixture]
    public class ZamowienieTest
    {
        ZamowieniaRepository _repository;
        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new ZamowieniaRepository();
        }

        [Test]
        public void CanGetZamowienie()
        {
            Zamowienie zamowienie = _repository.GetById(1);
            Assert.That(zamowienie.Zrealizowano, Is.True);
            Assert.That(zamowienie.Pracownik.Imie, Is.EqualTo("Kamil"));
            Assert.That(zamowienie.Klient.Imie, Is.EqualTo("Michal"));
        }

        [Test]
        public void CanUpdateZamowienie()
        {
            var count = _repository.GetCount();
            Zamowienie zamowienie = _repository.GetById(count);
            zamowienie.Zrealizowano = true;
            _repository.Update(zamowienie);
            zamowienie = _repository.GetById(count);
            Assert.That(zamowienie.Zrealizowano, Is.True);
            zamowienie.Zrealizowano = false;
            _repository.Update(zamowienie);
            Assert.That(zamowienie.Zrealizowano, Is.False);
        }

        [Test]
        public void CanAddAndDeleteZamowienie()
        {
            int countBefore = _repository.GetCount();
            Zamowienie zam = new Zamowienie() { DataZlozenia=DateTime.Now,DataZrealizowania=DateTime.Now };
            _repository.Add(zam);
            int countAfter = _repository.GetCount();
            Assert.That(countAfter, Is.EqualTo(countBefore + 1));

            _repository.Remove(zam);
            int countAfterDelete = _repository.GetCount();
            Assert.That(countAfterDelete, Is.EqualTo(countBefore));
        }

        [Test]
        public void CanFilterZamowieniaByZrealizowane()
        {
            var zamowienia = _repository.GetByFilter("Zrealizowano", true);
            Assert.That(zamowienia.Count, Is.EqualTo(1));
            zamowienia = _repository.GetByFilter("Zrealizowano", false);
            Assert.That(zamowienia.Count, Is.EqualTo(3));
        }


        [Test]
        public void CanFilterZamowieniaByZrealizowaneAndPracownikFirstAndLastName()
        {
            var zamowienia = _repository.GetByZrealizowaneAndPracownikName("Kamil", "Minda");
            Assert.That(zamowienia.Count, Is.EqualTo(1));

            zamowienia = _repository.GetByZrealizowaneAndPracownikName("Wojtek", "Korycki");
            Assert.That(zamowienia.Count, Is.EqualTo(0));

        }

        [Test]
        public void CanFilterZamowieniaByZrealizowaneAndKlientFirstAndLastName()
        {
            var  zamowienia = _repository.GetByZrealizowaneAndKlientName("Jan", "Kowalski");
            Assert.That(zamowienia.Count, Is.EqualTo(0));

            zamowienia = _repository.GetByZrealizowaneAndKlientName("Michal", "Franc");
            Assert.That(zamowienia.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanFilterZamowieniaByPracownikFirstAndLastName()
        {
            var zamowienia = _repository.GetByPracownikName("Wojciech", "Korycki");
            Assert.That(zamowienia.Count, Is.EqualTo(2));

            zamowienia = _repository.GetByPracownikName("Kamil", "Minda");
            Assert.That(zamowienia.Count, Is.EqualTo(1));

            zamowienia = _repository.GetByPracownikName("Grzegorz", "Brzeczyszczykiewicz");
            Assert.That(zamowienia.Count, Is.EqualTo(0));
        }

        [Test]
        public void CanFilterZamowieniaByKlientFirstAndLastName()
        {
            var zamowienia = _repository.GetByKlientName("Michal", "Franc");
            Assert.That(zamowienia.Count, Is.EqualTo(2));
            zamowienia = _repository.GetByKlientName("Kamil", "Minda");
            Assert.That(zamowienia.Count, Is.EqualTo(1));

            zamowienia = _repository.GetByKlientName("Grzegorz", "Brzeczyszczykiewicz");
            Assert.That(zamowienia.Count, Is.EqualTo(0));
        }

        [Test]
        public void CanFilterZamowieniaByPracownikNull()
        {
            var zamowienia = _repository.GetByPracownikNull();
            Assert.That(zamowienia.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanFilterZamowieniaByDataZlozenia()
        {
            //10.06.2010
            //10.10.2009
            //10.10.2010
            //10.06.2010

            var zamowienia = _repository.GetByDataZlozenia(new DateTime(2008, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(0));

            zamowienia = _repository.GetByDataZlozenia(new DateTime(2010, 6, 10));
            Assert.That(zamowienia.Count, Is.EqualTo(2));

            zamowienia = _repository.GetByDataZlozenia(new DateTime(2009, 10, 10));
            Assert.That(zamowienia.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanFilterZamowieniaByBeetwenDataZlozenia()
        {
             //10.06.2010
             //10.10.2009
             //10.10.2010
             //10.06.2010

            var zamowienia = _repository.GetByBeetwenDataZlozenia(new DateTime(2008, 1, 1), new DateTime(2011, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(4));

            zamowienia = _repository.GetByBeetwenDataZlozenia(new DateTime(2008, 1, 1), new DateTime(2009, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(0));

            zamowienia = _repository.GetByBeetwenDataZlozenia(new DateTime(2008, 1, 1), new DateTime(2010, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(1));

            zamowienia = _repository.GetByBeetwenDataZlozenia(new DateTime(2010, 1, 1), new DateTime(2011, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(3));

            zamowienia = _repository.GetByBeetwenDataZlozenia(new DateTime(2010, 6, 1), new DateTime(2010, 6, 12));
            Assert.That(zamowienia.Count, Is.EqualTo(2));

            zamowienia = _repository.GetByBeetwenDataZlozenia(new DateTime(2010, 6,9), new DateTime(2010, 6, 11));
            Assert.That(zamowienia.Count, Is.EqualTo(2));

            zamowienia = _repository.GetByBeetwenDataZlozenia(new DateTime(2010, 6, 12), new DateTime(2010, 6, 15));
            Assert.That(zamowienia.Count, Is.EqualTo(0));

        }

        [Test]
        public void CanFilterZamowieniaByGreaterThanDataZlozenia()
        {
            //10.06.2010
            //10.10.2009
            //10.10.2010
            //10.06.2010

            var zamowienia = _repository.GetByGreaterThanDataZlozenia(new DateTime(2008, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(4));

            zamowienia = _repository.GetByGreaterThanDataZlozenia(new DateTime(2009, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(4));

            zamowienia = _repository.GetByGreaterThanDataZlozenia(new DateTime(2010, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(3));

            zamowienia = _repository.GetByGreaterThanDataZlozenia(new DateTime(2010, 6, 10));
            Assert.That(zamowienia.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanFilterZamowieniaByLessThanDataZlozenia()
        {
            var zamowienia = _repository.GetByLessThanDataZlozenia(new DateTime(2008, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(0));

            zamowienia = _repository.GetByLessThanDataZlozenia(new DateTime(2009, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(0));

            zamowienia = _repository.GetByLessThanDataZlozenia(new DateTime(2010, 1, 1));
            Assert.That(zamowienia.Count, Is.EqualTo(1));

            zamowienia = _repository.GetByLessThanDataZlozenia(new DateTime(2010, 6, 12));
            Assert.That(zamowienia.Count, Is.EqualTo(3));
        }

        [Test]
        public void CanFilterZamowieniaByDataZrealizowania()
        {
            //new DateTime(2009,10,20)
            var zamowienia = _repository.GetByDataZrealizowania(new DateTime(2009, 10, 20));
            Assert.That(zamowienia.Count, Is.EqualTo(1));

            zamowienia = _repository.GetByDataZrealizowania(new DateTime(2010, 10, 20));
            Assert.That(zamowienia.Count, Is.EqualTo(0));
        }

        [Test]
        public void CanFilterZamowieniaByBeetwenDataZrealizowania()
        {
            //new DateTime(2009,10,20)
            var zamowienia = _repository.GetByBeetwenDataZrealizowania(new DateTime(2009, 10, 10), new DateTime(2009, 10, 25));
            Assert.That(zamowienia.Count, Is.EqualTo(1));

            zamowienia = _repository.GetByBeetwenDataZrealizowania(new DateTime(2010, 10, 21), new DateTime(2010, 10, 29));
            Assert.That(zamowienia.Count, Is.EqualTo(0));
        }

        [Test]
        public void CanFilterZamowieniaByGreaterThanDataZrealizowania()
        {
            //new DateTime(2009,10,20)
            var zamowienia = _repository.GetByGreaterThanDataZrealizowania(new DateTime(2009, 10, 10));
            Assert.That(zamowienia.Count, Is.EqualTo(1));

            zamowienia = _repository.GetByDataZrealizowania(new DateTime(2010, 10, 25));
            Assert.That(zamowienia.Count, Is.EqualTo(0));
        }

        [Test]
        public void CanFilterZamowieniaByLessThanDataZrealizowania()
        {
            //new DateTime(2009,10,20)
            var zamowienia = _repository.GetByLessThanDataZrealizowania(new DateTime(2009, 10, 25));
            Assert.That(zamowienia.Count, Is.EqualTo(1));

            zamowienia = _repository.GetByDataZrealizowania(new DateTime(2010, 10, 10));
            Assert.That(zamowienia.Count, Is.EqualTo(0));
        }


        [Test]
        public void CanSortZamowienia()
        {
            //10.06.2010
            //10.10.2009
            //10.10.2010
            //10.06.2010

            var zamowienia = _repository.GetByBeetwenDataZlozenia(new DateTime(2007, 10, 25), new DateTime(2012, 10, 25), "asc");
            Assert.That(zamowienia[0].DataZlozenia, Is.EqualTo(new DateTime(2009,10,10)));
            Assert.That(zamowienia[1].DataZlozenia, Is.EqualTo(new DateTime(2010, 6, 10)));
            Assert.That(zamowienia[2].DataZlozenia, Is.EqualTo(new DateTime(2010, 6, 10)));
            Assert.That(zamowienia[3].DataZlozenia, Is.EqualTo(new DateTime(2010, 10, 10)));

            zamowienia = _repository.GetByBeetwenDataZlozenia(new DateTime(2007, 10, 25),new DateTime(2012, 10, 25), "desc");
            Assert.That(zamowienia[0].DataZlozenia, Is.EqualTo(new DateTime(2010, 10, 10)));
            Assert.That(zamowienia[1].DataZlozenia, Is.EqualTo(new DateTime(2010, 6, 10)));
            Assert.That(zamowienia[2].DataZlozenia, Is.EqualTo(new DateTime(2010, 6, 10)));          
            Assert.That(zamowienia[3].DataZlozenia, Is.EqualTo(new DateTime(2009, 10, 10)));
        }

        [Test]
        public void HasAccesToZamowienieProducts()
        {
            var zamowienia = _repository.GetByPracownikName("Wojciech", "Korycki");
            var produkty = _repository.GetProductsByZamowienie(zamowienia[0]);
            Assert.That(produkty.Count, Is.EqualTo(3));
            Assert.That(produkty[0].Nazwa, Is.EqualTo("BananLepszy"));
            Assert.That(produkty[1].Nazwa, Is.EqualTo("Banan"));
            Assert.That(produkty[2].Nazwa, Is.EqualTo("Jajka"));

        }
    }
}
