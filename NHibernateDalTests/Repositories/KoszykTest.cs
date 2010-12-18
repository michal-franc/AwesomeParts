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
    public class KoszykTest
    {
        Repository<ZamowieniaKoszyk> _repository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _repository = new Repository<ZamowieniaKoszyk>();
        }

        [Test]
        public void CanGetKoszyk()
        {
            var testedKoszyk = _repository.GetById(1);
            Assert.That(testedKoszyk.Ilosc, Is.EqualTo(10));

            testedKoszyk = _repository.GetById(2);
            Assert.That(testedKoszyk.Ilosc, Is.EqualTo(15));
        }


        [Test]
        public void CanAddKoszyk()
        {
            ZamowieniaKoszyk newKoszyk = new ZamowieniaKoszyk() { Ilosc = 20};

            var koszykiBeforeCount = _repository.GetCount();

            _repository.Add(newKoszyk);

            var koszykiCount = _repository.GetCount();
            Assert.That(koszykiCount, Is.EqualTo(koszykiBeforeCount+1));

            var testedKoszyk = _repository.GetById(koszykiCount);
            Assert.That(testedKoszyk.Ilosc, Is.EqualTo(20));
        }


        [Test]
        public void CanUpdateKoszyk()
        {
            var testedKoszyk = _repository.GetById(1);
            testedKoszyk.Ilosc = 30;

            _repository.Update(testedKoszyk);

            testedKoszyk = _repository.GetById(1);
            Assert.That(testedKoszyk.Ilosc, Is.EqualTo(30));
        }

        [Test]
        public void CanDeleteKoszyk()
        {
            ZamowieniaKoszyk newKoszyk = new ZamowieniaKoszyk() { Ilosc = 20 };

            var koszykiBeforeCount = _repository.GetCount();

            _repository.Add(newKoszyk);

            var koszykiAfterCount = _repository.GetCount();
            Assert.That(koszykiAfterCount,Is.EqualTo(koszykiBeforeCount+1));

            _repository.Remove(newKoszyk);

            var koszykiCount = _repository.GetCount();
            Assert.That(koszykiCount, Is.EqualTo(koszykiBeforeCount));
        }
    }
}
