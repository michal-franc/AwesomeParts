using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Mocks;
using BazaDanych;
using BazaDanych.Repositories;
using BazaDanych.Entities;

namespace Tests
{
    [TestFixture]
    class NHibernate
    {

        [Test]
        public void CanGenerateNewSchema()
        {
            SessionFactory.ResetSchema();
        }


        [Test]
        public void CanUpdateShemaAndFillDatabaseWithSampleData()
        {
            //TestData

            #region Pracownicy

            PracownikRepository pracRepository = new PracownikRepository();
        
            #region PracownikRodzaj
            Repository<PracownikRodzaj> repositoryPracownikRodzaj = new Repository<PracownikRodzaj>();
            
            PracownikRodzaj pracownikRodzajMagazynier = new PracownikRodzaj() { Rodzaj = "Magazynier" };
            PracownikRodzaj pracownikRodzajSekretarka = new PracownikRodzaj() { Rodzaj = "Sekretarka" };
            PracownikRodzaj pracownikRodzajPrezes = new PracownikRodzaj() { Rodzaj = "Prezes" };
            PracownikRodzaj pracownikRodzajKierownik = new PracownikRodzaj() { Rodzaj = "Kierownik" };

            repositoryPracownikRodzaj.Add(pracownikRodzajMagazynier);
            repositoryPracownikRodzaj.Add(pracownikRodzajSekretarka);
            repositoryPracownikRodzaj.Add(pracownikRodzajPrezes);
            repositoryPracownikRodzaj.Add(pracownikRodzajKierownik);         
            #endregion

            #region PracownikStatus
            Repository<PracownikStatus> repositoryPracownikStatus = new Repository<PracownikStatus>();
            
            PracownikStatus pracownikStatusUrlop = new PracownikStatus() { Status = "Urlop" };
            PracownikStatus pracownikStatusZatrudniony = new PracownikStatus() { Status = "Zatrudniony" };
            PracownikStatus pracownikStatusChorobowe = new PracownikStatus() { Status = "Chorobowe" };

            repositoryPracownikStatus.Add(pracownikStatusUrlop);
            repositoryPracownikStatus.Add(pracownikStatusZatrudniony);
            repositoryPracownikStatus.Add(pracownikStatusChorobowe);        
            #endregion

            #region LoginRola
            Repository<LoginRola> repositoryLoginRola = new Repository<LoginRola>();

            LoginRola loginRolaAdministrator = new LoginRola() { Rola = "Administrator" };
            LoginRola loginRolaZarzad = new LoginRola() { Rola = "Zarzad" };
            LoginRola loginRolaUzytkownik = new LoginRola() { Rola = "Uzytkownik" };
            LoginRola loginRolaSprzedawca = new LoginRola() { Rola = "Sprzedawca" };
            LoginRola loginRolaKontrahent = new LoginRola() { Rola = "Kontrahent" };

            repositoryLoginRola.Add(loginRolaAdministrator);
            repositoryLoginRola.Add(loginRolaZarzad);
            repositoryLoginRola.Add(loginRolaUzytkownik);
            repositoryLoginRola.Add(loginRolaSprzedawca);
            repositoryLoginRola.Add(loginRolaKontrahent);
            #endregion


            Pracownik pracownikMichalFranc = new Pracownik() { Imie = "Michal", Nazwisko = "Franc", Login = "LaM", Status = pracownikStatusUrlop, LoginRola = loginRolaUzytkownik, Rodzaj = pracownikRodzajMagazynier };
            Pracownik pracownikStefanRomanski = new Pracownik() { Imie = "Stefan", Nazwisko = "Romanski", Login = "stefcio", Status = pracownikStatusZatrudniony, LoginRola = loginRolaAdministrator, Rodzaj = pracownikRodzajSekretarka };
            Pracownik pracownikWojtekKorycki = new Pracownik() { Imie = "Wojciech", Nazwisko = "Korycki", Login = "wojtek", Status = pracownikStatusZatrudniony, LoginRola = loginRolaSprzedawca, Rodzaj = pracownikRodzajSekretarka };
            Pracownik pracownikKamilMinda = new Pracownik() { Imie = "Kamil", Nazwisko = "Minda", Login = "kminda", Status = pracownikStatusChorobowe, LoginRola = loginRolaSprzedawca, Rodzaj = pracownikRodzajSekretarka };
            Pracownik pracownikIwonaKarczoch = new Pracownik() { Imie = "Iwona", Nazwisko = "Karczoch", Login = "ikarczoch", Status = pracownikStatusZatrudniony, LoginRola = loginRolaZarzad, Rodzaj = pracownikRodzajPrezes };

            pracRepository.Add(pracownikMichalFranc);
            pracRepository.Add(pracownikStefanRomanski);
            pracRepository.Add(pracownikWojtekKorycki);
            pracRepository.Add(pracownikKamilMinda);
            pracRepository.Add(pracownikIwonaKarczoch);

            #endregion

            #region PracownikUmowa

            Repository<PracownikUmowa> repositoryUmowa = new Repository<PracownikUmowa>();

            PracownikUmowa umowaMichalFranc = new PracownikUmowa() 
            { 
                DataPodpisania=new DateTime(2010,1,1),
                DataWygasniecia=null,  
                Aktualna=true, 
                Placa=1000, 
                Pracownik = pracownikMichalFranc,
                Uwagi="Testowa umowa"
            };

            PracownikUmowa umowaMichalFrancArchiw0 = new PracownikUmowa()
            {
                DataPodpisania = new DateTime(2009, 1, 1),
                DataWygasniecia = new DateTime(2010, 1, 1),
                Aktualna = false,
                Placa = 1000,
                Pracownik = pracownikMichalFranc,
                Uwagi = "Testowa umowa"
            };

            PracownikUmowa umowaMichalFrancArchiw1 = new PracownikUmowa()
            {
                DataPodpisania = new DateTime(2008, 6, 1),
                DataWygasniecia = new DateTime(2009, 1, 1),
                Aktualna = false,
                Placa = 1500,
                Pracownik = pracownikMichalFranc,
                Uwagi = "Testowa umowa"
            };

            PracownikUmowa umowaMichalFrancArchiw2 = new PracownikUmowa()
            {
                DataPodpisania = new DateTime(2008, 5, 1),
                DataWygasniecia = new DateTime(2008, 6, 1),
                Aktualna = false,
                Placa = 1000,
                Pracownik = pracownikMichalFranc,
                Uwagi = "Praktyki"
            };

            PracownikUmowa umowaStefanRomanski = new PracownikUmowa() 
            {
                DataPodpisania = new DateTime(2007, 1, 1),
                DataWygasniecia=null,
                Aktualna = true,
                Pracownik = pracownikStefanRomanski,
                Placa=2000
            };

            PracownikUmowa umowaWojtekKorycki = new PracownikUmowa() 
            {
                DataPodpisania = new DateTime(2006, 1, 1),
                DataWygasniecia=null,
                Aktualna = true,
                Pracownik = pracownikWojtekKorycki,
                Placa=1500
            };

            PracownikUmowa umowaKamilMinda = new PracownikUmowa()
            {
                DataPodpisania = new DateTime(2005, 1, 1),
                DataWygasniecia = null,
                Aktualna = true,
                Pracownik = pracownikKamilMinda,
                Placa = 1500
            };

            PracownikUmowa umowaIwonaKarczoch = new PracownikUmowa()
            {
                DataPodpisania = new DateTime(2006, 1, 1),
                DataWygasniecia = null,
                Aktualna = true,
                Pracownik = pracownikIwonaKarczoch,
                Placa = 1500
            };

            repositoryUmowa.Add(umowaMichalFranc);
            repositoryUmowa.Add(umowaStefanRomanski);
            repositoryUmowa.Add(umowaWojtekKorycki);
            repositoryUmowa.Add(umowaKamilMinda);
            repositoryUmowa.Add(umowaIwonaKarczoch);
            repositoryUmowa.Add(umowaMichalFrancArchiw0);
            repositoryUmowa.Add(umowaMichalFrancArchiw1);
            repositoryUmowa.Add(umowaMichalFrancArchiw2);

            #endregion

            #region Klienci
            Repository<Klient> repositoryKlient = new Repository<Klient>();

            #region   KlientRodzaj

            Repository<KlientRodzaj> repositoryKlientRodzaj = new Repository<KlientRodzaj>();

            KlientRodzaj klientRodzajodzajKupujacy = new KlientRodzaj() { Rodzaj = "Kupujacy" };
            KlientRodzaj klientRodzajSprzedajacy = new KlientRodzaj() { Rodzaj = "Sprzedajacy" };

            repositoryKlientRodzaj.Add(klientRodzajodzajKupujacy);
            repositoryKlientRodzaj.Add(klientRodzajSprzedajacy);

            #endregion

            Klient klientMichalFrancKupujacy = new Klient()
            {
                        Imie = "Michal" , 
                        Nazwisko="Franc" , 
                        Firma="Contium" , 
                        KodPocztowy="56-416", 
                        Miasto="Twardogora",
                        Kraj="Polska", 
                        Numer="2a" , 
                        Ulica="Brzozowa" , 
                        Telefon="661943509", 
                        NIP="55641", 
                        Login="LaM",
                        Rodzaj= klientRodzajodzajKupujacy,
                        LoginRola = loginRolaKontrahent
            };

            Klient klientKamilMindaKupujacy =  new Klient()
                    {
                        Imie = "Kamil" , 
                        Nazwisko="Minda" , 
                        Firma="PWR" , 
                        KodPocztowy="56-416", 
                        Miasto="Twardogora",
                        Kraj="Polska", 
                        Numer="28a" , 
                        Ulica="Magiczna" , 
                        Telefon="123456789", 
                        NIP="55651", 
                        Login="Emol",
                        Rodzaj= klientRodzajodzajKupujacy,
                        LoginRola = loginRolaKontrahent
                    };
            Klient klientStefanRomanskiSprzedajacy =  new Klient()
                    {
                        Imie = "Stefan" , 
                        Nazwisko="Romanski" , 
                        Firma="IBM" , 
                        KodPocztowy="56-416", 
                        Miasto="Twardogora",
                        Kraj="Polska", 
                        Numer="20" , 
                        Ulica="Lipowa" , 
                        Telefon="09876543", 
                        NIP="55000", 
                        Login="Grzywa",
                        Rodzaj= klientRodzajSprzedajacy,
                        LoginRola = loginRolaKontrahent
                    };

            repositoryKlient.Add(klientMichalFrancKupujacy);
            repositoryKlient.Add(klientKamilMindaKupujacy);
            repositoryKlient.Add(klientStefanRomanskiSprzedajacy);
            #endregion

            #region Produkt

            #region ProduktProducent
            ProduktProducent producentPolmosSA = new ProduktProducent() { Nazwa = "Polmos" };
            ProduktProducent producentKFC = new ProduktProducent() { Nazwa = "KFC" };

            Repository<ProduktProducent> produktProducentRepository = new Repository<ProduktProducent>();

            produktProducentRepository.Add(producentPolmosSA);
            produktProducentRepository.Add(producentKFC);

            #endregion
            Repository<Produkty> produktyRepository = new Repository<Produkty>();

            Produkty produktBanan = new Produkty() { Nazwa = "Banan", Ilosc = 100, Cena = 10 , DocelowaIlosc=200 ,Producent= producentPolmosSA};
            Produkty produktTruskawka = new Produkty() { Nazwa = "Truskawka", Ilosc = 50, Cena = 5, DocelowaIlosc = 50, Producent = producentKFC };
            Produkty produktJajka = new Produkty() { Nazwa = "Jajka", Ilosc = 50, Cena = 5, DocelowaIlosc = 76, Producent = producentKFC };
            Produkty produktySalata = new Produkty() { Nazwa = "Salata",  Ilosc = 10, Cena = 10 , DocelowaIlosc=200 ,Producent= producentPolmosSA};
            Produkty produktyPomidor = new Produkty() { Nazwa = "Pomidor",  Ilosc = 150, Cena = 25 , DocelowaIlosc=250 ,Producent= producentPolmosSA};

            produktyRepository.Add(produktBanan);
            produktyRepository.Add(produktJajka);
            produktyRepository.Add(produktTruskawka);
            produktyRepository.Add(produktySalata);
            produktyRepository.Add(produktyPomidor);

            #endregion

            #region Zamowienia

            ZamowieniaRepository repositoryZamowienia = new ZamowieniaRepository();

            Zamowienie zamowienieMichalFrancZrealizowane = new Zamowienie()
            {
                DataZlozenia = new DateTime(2009, 10, 10),
                DataZrealizowania = new DateTime(2009, 10, 20),
                Klient = klientMichalFrancKupujacy,
                Pracownik = pracownikKamilMinda,
                Zrealizowano = true
            };

            Zamowienie zamowienieKamilMindaNieZrealizowane = new Zamowienie()
            {
                DataZlozenia=new DateTime(2010,10,10),
                DataZrealizowania=null,
                Klient = klientKamilMindaKupujacy,
                Pracownik = pracownikWojtekKorycki ,
                Zrealizowano=false
            };

            Zamowienie zamowienieMichalFrancNieZrealizowane = new Zamowienie()
            {
                DataZlozenia=new DateTime(2010,6,10),
                DataZrealizowania=null,
                Klient = klientMichalFrancKupujacy,
                Pracownik = pracownikWojtekKorycki ,
                Zrealizowano=false
            };

            Zamowienie zamowienieNieZrealizowane = new Zamowienie()
            {
                DataZlozenia=new DateTime(2010,6,10),
                DataZrealizowania=null,
                Klient = null,
                Pracownik = null,
                Zrealizowano=false
            };

            repositoryZamowienia.Add(zamowienieMichalFrancZrealizowane);
            repositoryZamowienia.Add(zamowienieKamilMindaNieZrealizowane);
            repositoryZamowienia.Add(zamowienieMichalFrancNieZrealizowane);
            repositoryZamowienia.Add(zamowienieNieZrealizowane);

            #endregion

            #region Koszyk

            Repository<ZamowieniaKoszyk> repositoryKoszyk = new Repository<ZamowieniaKoszyk>();

            List<ZamowieniaKoszyk> koszyki = new List<ZamowieniaKoszyk>()
            {
                new ZamowieniaKoszyk(){Ilosc=10, Produkt=produktyPomidor, Zamowienie = zamowienieMichalFrancZrealizowane},
                new ZamowieniaKoszyk(){Ilosc=15, Produkt=produktySalata,Zamowienie=zamowienieKamilMindaNieZrealizowane},
                new ZamowieniaKoszyk(){Ilosc=5, Produkt=produktBanan, Zamowienie = zamowienieKamilMindaNieZrealizowane},
                new ZamowieniaKoszyk(){Ilosc=1, Produkt=produktJajka,Zamowienie=zamowienieKamilMindaNieZrealizowane}
            };

            foreach(ZamowieniaKoszyk koszyk in koszyki)    
            {
                repositoryKoszyk.Add(koszyk);
            }

            #endregion
        }
    }
}
