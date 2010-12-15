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
    public class UmowaTest
    {
        Repository<PracownikUmowa> _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new Repository<PracownikUmowa>();
        }

        [Test]
        public void CanGetUmowa()
        {
            var count = _repository.GetCount();
            PracownikUmowa umowa1 = _repository.GetById(count-1);
            Assert.That(umowa1.Aktualna, Is.EqualTo(false));
            Assert.That(umowa1.Placa, Is.EqualTo(1000));
        }

        [Test]
        public void CanUpdateUmowa()
        {
            var count = _repository.GetCount();
            PracownikUmowa umowaTest = _repository.GetById(count-1);
            umowaTest.Placa= 2000;
            _repository.Update(umowaTest);
            umowaTest = _repository.GetById(count-1);
            Assert.That(umowaTest.Placa, Is.EqualTo(2000));
        }

        [Test]
        public void CanDeleteUmowa()
        {
            int countBefore = _repository.GetCount();
            PracownikUmowa umowa = new PracownikUmowa() { DataWygasniecia=DateTime.Now , DataPodpisania=DateTime.Now };
            _repository.Add(umowa);
            int countAfter = _repository.GetCount();
            Assert.That(countAfter, Is.EqualTo(countBefore + 1));

            _repository.Remove(umowa);
            int countAfterDelete = _repository.GetCount();
            Assert.That(countAfterDelete, Is.EqualTo(countBefore));
        }

        [Test]
        public void CanGetUmowaPracownik()
        {
            var count = _repository.GetCount();
            PracownikUmowa umowa = _repository.GetById(count-1);
            Assert.That(umowa.Pracownik.Imie, Is.EqualTo("Michal"));
        }
    }
}
