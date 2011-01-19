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
            Assert.That(pracownikTest.Imie, Is.EqualTo("Michal"));
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
            Assert.That(pracownikTest.Nazwisko, Is.EqualTo("Romanski"));
            Assert.That(pracownikTest.Login, Is.EqualTo("stefcio"));
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
            //PracownikStatus pracownikStatusUrlop = new PracownikStatus() { Status = "Urlop" };
            //PracownikStatus pracownikStatusZatrudniony = new PracownikStatus() { Status = "Zatrudniony" };
            //PracownikStatus pracownikStatusChorobowe = new PracownikStatus() { Status = "Chorobowe" };

            var pracownicy = _repository.GetByStatus("Urlop");
            Assert.That(pracownicy.Count,Is.EqualTo(1));
            pracownicy = _repository.GetByStatus("Zatrudniony");
            Assert.That(pracownicy.Count, Is.EqualTo(3));
            pracownicy = _repository.GetByStatus("Chorobowe");
            Assert.That(pracownicy.Count, Is.EqualTo(1));


        }



        [Test]
        public void CanGetByPracownikImieNazwisko()
        {
            //Pracownik pracownikMichalFranc;
            //Pracownik pracownikStefanRomanski;
            //Pracownik pracownikWojtekKorycki;
            //Pracownik pracownikKamilMinda;
            //Pracownik pracownikIwonaKarczoch;


            var pracownik = _repository.GetByImieNazwisko("Michal","Franc");
            Assert.That(pracownik.Imie, Is.EqualTo("Michal"));
            Assert.That(pracownik.Nazwisko, Is.EqualTo("Franc"));

            pracownik = _repository.GetByImieNazwisko("Kamil", "Minda");
            Assert.That(pracownik.Imie, Is.EqualTo("Kamil"));
            Assert.That(pracownik.Nazwisko, Is.EqualTo("Minda"))
                ;
            pracownik = _repository.GetByImieNazwisko("Wojciech", "Korycki");
            Assert.That(pracownik.Imie, Is.EqualTo("Wojciech"));
            Assert.That(pracownik.Nazwisko, Is.EqualTo("Korycki"));

            pracownik = _repository.GetByImieNazwisko("Stefan", "Romanski");
            Assert.That(pracownik.Imie, Is.EqualTo("Stefan"));
            Assert.That(pracownik.Nazwisko, Is.EqualTo("Romanski"));

            pracownik = _repository.GetByImieNazwisko("Iwona", "Karczoch");
            Assert.That(pracownik.Imie, Is.EqualTo("Iwona"));
            Assert.That(pracownik.Nazwisko, Is.EqualTo("Karczoch"));

            pracownik = _repository.GetByImieNazwisko("Grzegorz", "Brzeczyszczykiewicz");
            Assert.That(pracownik, Is.Null);

        }

        [Test]
        public void CanGetAktualnaUmowa()
        {
            var pracownik = _repository.GetByImieNazwisko("Michal", "Franc");
            Assert.That(pracownik.Imie, Is.EqualTo("Michal"));
            Assert.That(pracownik.Nazwisko, Is.EqualTo("Franc"));
            Assert.That(pracownik.AktualnaUmowa,Is.Not.Null);
            Assert.That(pracownik.AktualnaUmowa.Aktualna, Is.True);
            Assert.That(pracownik.AktualnaUmowa.DataPodpisania, Is.EqualTo(new DateTime(2010, 1, 1)));
        }

        [Test]
        public void CanGetArchiwalneUmowy()
        {
            var pracownik = _repository.GetByImieNazwisko("Michal", "Franc");
            Assert.That(pracownik.Imie, Is.EqualTo("Michal"));
            Assert.That(pracownik.Nazwisko, Is.EqualTo("Franc"));
            Assert.That(pracownik.UmowyArchiwalne.Count,Is.EqualTo(3));
        }

        [Test]
        public void CanFilterByBeetwenDataZatrudnienia()
        {
            //DateTime(2010,1,1)
            //DateTime(2009, 1, 1),    
            //DateTime(2008, 6, 1
            //DateTime(2008, 5, 1)
            //DateTime(2007, 1, 1)
            //DateTime(2006, 1, 1)
            //DateTime(2006, 1, 1)
            //DateTime(2005, 1, 1)

            var pracownicy = _repository.CanGetByDataPodpisaniaUmowy(new DateTime(2009,1,1),new DateTime(2011,1,1));
            Assert.That(pracownicy.Count, Is.EqualTo(1));

            pracownicy = _repository.CanGetByDataPodpisaniaUmowy(new DateTime(2000, 1, 1), new DateTime(2001, 1, 1));
            Assert.That(pracownicy.Count, Is.EqualTo(0));

            pracownicy = _repository.CanGetByDataPodpisaniaUmowy(new DateTime(2000, 1, 1), new DateTime(2012, 1, 1));
            Assert.That(pracownicy.Count, Is.EqualTo(5));

            pracownicy = _repository.CanGetByDataPodpisaniaUmowy(new DateTime(2006, 1, 1), new DateTime(2006, 5, 1));
            Assert.That(pracownicy.Count, Is.EqualTo(2));

            pracownicy = _repository.CanGetByDataPodpisaniaUmowy(new DateTime(2008, 5, 10), new DateTime(2008, 6, 10));
            Assert.That(pracownicy.Count, Is.EqualTo(0));
        }

        [Test]
        public void HasAccesToObslugiwaneZamowienia()
        {
            var pracownik = _repository.GetByImieNazwisko("Michal", "Franc");
            Assert.That(pracownik.Zamowienia, Is.Not.Null);
            Assert.That(pracownik.Zamowienia.Count, Is.EqualTo(0));

            pracownik = _repository.GetByImieNazwisko("Wojciech", "Korycki");
            Assert.That(pracownik.Zamowienia, Is.Not.Null);
            Assert.That(pracownik.Zamowienia.Count, Is.EqualTo(2));

            pracownik = _repository.GetByImieNazwisko("Kamil", "Minda");
            Assert.That(pracownik.Zamowienia, Is.Not.Null);
            Assert.That(pracownik.Zamowienia.Count, Is.EqualTo(1));


        }
    }
}
