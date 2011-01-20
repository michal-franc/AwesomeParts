
namespace AwesomeParts.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using AwesomeParts.Web.POCOs;
    using BazaDanych.Repositories;
    using BazaDanych.Entities;
    using AwesomeParts.Web.POCOs.MiniPOCOs;
    using System.Web.Security;
    using AwesomeParts.Web.Models;


    // TODO: Create methods containing your application logic.
    [EnableClientAccess()]
    public class AwesomePartsService : DomainService
    {
        #region contexts

        private Repository<Produkty> _produktyContext = new Repository<Produkty>();
        private IRepository<ProduktProducent> _producentContext = new Repository<ProduktProducent>();
        private ZamowieniaRepository _zamowieniaContext = new ZamowieniaRepository();
        private KlientRepository _klienciContext = new KlientRepository();
        private PracownikRepository _pracownicyContext = new PracownikRepository();
        private IRepository<PracownikRodzaj> _pracownikRodzajeContext = new Repository<PracownikRodzaj>();
        private IRepository<PracownikStatus> _pracownikStatusyContext = new Repository<PracownikStatus>();
        private IRepository<PracownikUmowa> _pracownikUmowyContext = new Repository<PracownikUmowa>();
        private KoszykRepository _koszykContext = new KoszykRepository();

        #endregion contexts

        #region CRUDs
        
        #region Produkty CRUD

        [Insert()]
        public void InsertProdukt(ProduktPOCO produkt)
        {
            _produktyContext.Add(new Produkty
            {
                Nazwa = produkt.Nazwa,
                Ilosc = produkt.Ilosc,
                Cena = produkt.Cena,
                DocelowaIlosc = produkt.DocelowaIlosc,
                Producent = _producentContext.GetById(produkt.ProducentID)
            });
        }

        [Update()]
        public void UpdateProdukt(ProduktPOCO produkt)
        {
            _produktyContext.UpdateById(new Produkty
            {
                Nazwa = produkt.Nazwa,
                Ilosc = produkt.Ilosc,
                Cena = produkt.Cena,
                DocelowaIlosc = produkt.DocelowaIlosc,
                Producent = _producentContext.GetById(produkt.ProducentID)
            }, produkt.Id);
        }

        [Delete()]
        public void DeleteProdukt(ProduktPOCO produkt)
        {
            //_context.Remove(new Produkty
            //{
            //    Nazwa = produkt.Nazwa,
            //    Ilosc = produkt.Ilosc,
            //    Cena = produkt.Cena,
            //    DocelowaIlosc = produkt.DocelowaIlosc,
            //    Producent = new ProduktProducent
            //    {
            //        Nazwa = produkt.ProducentNazwa
            //    }
            //});
            _produktyContext.Remove(_produktyContext.GetById(produkt.Id));
        }

        [Query()]
        public IQueryable<ProduktPOCO> GetProdukty()
        {
            return (
                from r in this._produktyContext.GetAll().AsQueryable<Produkty>()
                select POCOHelpers.MapProduktToPOCO(r));
        }

        [Query()]
        public IQueryable<ProduktProducentPOCO> GetProducenci()
        {
            return (
                from r in this._producentContext.GetAll().AsQueryable<ProduktProducent>()
                select new ProduktProducentPOCO
                {
                    Id = r.Id,
                    Nazwa = r.Nazwa
                });
        }

        [Query()]
        public IQueryable<ProductSellPOCO> GetCountProductsSoldByYearRange(int startYear,int endYear)
        {
            int counter = 0;
            List<ProductSellPOCO> list = new List<ProductSellPOCO>();
            for(int i = startYear;i<endYear;i++)
            {
                counter++;
                list.Add(new ProductSellPOCO(){Id=counter,Ilosc=_koszykContext.GetAllProductsSoldByYear(i), Year=i});
            }
             return list.AsQueryable<ProductSellPOCO>();
        }

        [Query()]
        public IQueryable<ProductSellPOCO> GetCountProductsSoldByYear(int year)
        {
            int counter = 0;
            List<ProductSellPOCO> list = new List<ProductSellPOCO>();
            for (int i = 1; i <= 12; i++)
            {
                counter++;
                list.Add(new ProductSellPOCO() { Id = counter, Ilosc = _koszykContext.GetAllProductsSoldByYearAndMonth(year, i), Year = year, Month = counter });
            }
            return list.AsQueryable<ProductSellPOCO>();
        }

        [Query()]
        public IQueryable<ProductSellPOCO> GetProductsSoldByYear(int year)
        {
            int counter = 0;
            List<ProductSellPOCO> list = new List<ProductSellPOCO>();
            foreach (Produkty prod in _produktyContext.GetAll())
            {
                counter++;
                list.Add( new ProductSellPOCO() { Id=counter, Nazwa = prod.Nazwa, Ilosc = _koszykContext.GetProductsSoldByYear(year, prod.Nazwa) });
            }
            return list.AsQueryable<ProductSellPOCO>();
        }

        [Query()]
        public IQueryable<ProductSellPOCO> GetProductsSoldByMonthAndYear(int month, int year)
        {
            int counter = 0;

            List<ProductSellPOCO> list = new List<ProductSellPOCO>();
            foreach (Produkty prod in _produktyContext.GetAll())
            {
                counter++;

                list.Add(new ProductSellPOCO() { Id = counter, Nazwa = prod.Nazwa, Ilosc = _koszykContext.GetProductsSoldByMonth(month, year, prod.Nazwa) });
            }
            return list.AsQueryable<ProductSellPOCO>(); 
        }

        #endregion Produkty CRUD

        #region Pracownicy CRUD

        [Insert()]
        public void InsertPracownik(PracownikPOCO pracownik)
        {
            PracownikStatus ps = _pracownikStatusyContext.GetById(pracownik.StatusID);
            PracownikRodzaj pr = _pracownikRodzajeContext.GetById(pracownik.RodzajID);

            int pracownikID = _pracownicyContext.Add(new Pracownik
            {
                Imie = pracownik.Imie,
                Nazwisko = pracownik.Nazwisko,
                Pesel = pracownik.Pesel,
                Status = ps,
                Rodzaj = pr,
                UwagiDoStatusu = pracownik.UwagiDoStatusu,
                
            });

            if (pracownikID == 0) throw new Exception();

            InsertEmptyUmowa(pracownikID);
        }

        [Update()]
        public void UpdatePracownik(PracownikPOCO pracownik)
        {
            Pracownik p = _pracownicyContext.GetById(pracownik.Id);
            PracownikStatus ps = _pracownikStatusyContext.GetById(pracownik.StatusID);
            PracownikRodzaj pr = _pracownikRodzajeContext.GetById(pracownik.RodzajID);

            p.Imie = pracownik.Imie;
            p.Nazwisko = pracownik.Nazwisko;
            p.Pesel = pracownik.Pesel;
            p.Status = ps;
            p.Rodzaj = pr;
            p.UwagiDoStatusu = pracownik.UwagiDoStatusu;

            _pracownicyContext.Update(p);
        }

        [Update]
        public void UpdateUmowa(PracownikUmowaPOCO umowa)
        {
            PracownikUmowa u = _pracownikUmowyContext.GetById(umowa.Id);

            u.DataPodpisania = umowa.DataPodpisania;
            u.DataWygasniecia = umowa.DataWygasniecia;
            u.Aktualna = umowa.Aktualna;
            u.Placa = umowa.Placa;
            u.Uwagi = umowa.Uwagi;

            _pracownikUmowyContext.Update(u);
        }

        [Insert]
        public void InsertUmowa(PracownikUmowaPOCO umowa)
        {
            Pracownik p = _pracownicyContext.GetById(umowa.PracownikID);
            _pracownikUmowyContext.Add(new PracownikUmowa
            {
                DataPodpisania = umowa.DataPodpisania,
                DataWygasniecia = umowa.DataWygasniecia,
                Aktualna = umowa.Aktualna,
                Placa = umowa.Placa,
                Uwagi = umowa.Uwagi,
                Pracownik = p
            });
        }

        [Ignore]
        private void InsertEmptyUmowa(int pracownikID)
        {
            InsertUmowa(new PracownikUmowaPOCO
            {
                PracownikID = pracownikID,
                Uwagi = "Wypelnij szczegoly umowy!",
                Aktualna = true
            });
        }

        //[Delete()]
        //public void DeletePracownik(PracownikPOCO produkt)
        //{
        //    //_pracownikContext.Remove(new Pracowniky
        //    //{
        //    //    Nazwa = produkt.Nazwa,
        //    //    Ilosc = produkt.Ilosc,
        //    //    Cena = produkt.Cena,
        //    //    DocelowaIlosc = produkt.DocelowaIlosc,
        //    //    Producent = new PracownikProducent
        //    //    {
        //    //        Nazwa = produkt.ProducentNazwa
        //    //    }
        //    //});
        //    _pracownikContext.Remove(_pracownikContext.GetById(produkt.Id));
        //}

        [Query]
        public IQueryable<PracownikPOCO> GetPracownicy()
        {
            return (
                from r in this._pracownicyContext.GetAll().AsQueryable<Pracownik>()
                select POCOHelpers.MapPracownikToPOCO(r));
        }

        [Query]
        public IQueryable<PracownikRodzajPOCO> GetPracownkRodzaje()
        {
            return (
                from r in this._pracownikRodzajeContext.GetAll().AsQueryable<PracownikRodzaj>()
                select new PracownikRodzajPOCO
                {
                    Id = r.Id,
                    Rodzaj = r.Rodzaj
                });
        }

        [Query]
        public IQueryable<PracownikStatusPOCO> GetPracownkStatusy()
        {
            return (
                from r in this._pracownikStatusyContext.GetAll().AsQueryable<PracownikStatus>()
                select new PracownikStatusPOCO
                {
                    Id = r.Id,
                    Status = r.Status
                });
        }

        [Query]
        public IQueryable<PracownikUmowaPOCO> GetUmowaAktualnaByPracownikId(int pracownikID)
        {
            return new List<PracownikUmowaPOCO> 
            {
                POCOHelpers.MapPracownikUmowaToPOCO(this._pracownicyContext.GetById(pracownikID).AktualnaUmowa)
            }.AsQueryable();
        }

        [Query]
        public IQueryable<PracownikUmowaPOCO> GetUmowyNieaktualneByPracownikId(int pracownikID)
        {
            return (
                from r in this._pracownicyContext.GetById(pracownikID).UmowyArchiwalne.AsQueryable()
                select POCOHelpers.MapPracownikUmowaToPOCO(r));
        }

        [Query]
        public IQueryable<PracownikUmowaPOCO> GetUmowyByPracownikId(int pracownikID)
        {
            if (pracownikID > 0)
            {
                return (
                    from r in this._pracownicyContext.GetById(pracownikID).Umowy.AsQueryable()
                    select POCOHelpers.MapPracownikUmowaToPOCO(r));
            }
            else
                return null;
        }


        #endregion Pracownicy CRUD

        #region Klienci CRUD

        public IQueryable<KlientPOCO> GetKlienci()
        {
            return (
                from r in this._klienciContext.GetAll().AsQueryable<Klient>()
                orderby r.Nazwisko
                select POCOHelpers.MapKlientToPOCO(r));
        }

        #endregion Klienci CRUD

        #region Zamowienia CRUD

        [Insert()]
        public void InsertZamowienie(ZamowieniePOCO zamowienie)
        {
            _zamowieniaContext.Add(new Zamowienie
            {
                Klient = _klienciContext.GetById(zamowienie.KlientID),
                DataZlozenia = null,
                Pracownik = null,
                DataZrealizowania = null,
                Zrealizowano = false
            });
        }

        [Update()]
        public void UpdateZamowienie(ZamowieniePOCO zamowienie)
        {
            Zamowienie z = _zamowieniaContext.GetById(zamowienie.Id);

            if (zamowienie.PracownikID > 0)
                z.Pracownik = _pracownicyContext.GetById(zamowienie.PracownikID);
            else
                z.Pracownik = null;

            z.DataZlozenia = zamowienie.DataZlozenia;
            z.DataZrealizowania = zamowienie.DataZrealizowania;
            z.Zrealizowano = zamowienie.Zrealizowano;

            _zamowieniaContext.Update(z);
        }

        [Delete()]
        public void DeleteZamowienie(ZamowieniePOCO zamowienie)
        {
            //_context.Remove(new Zamowieniey
            //{
            //    Nazwa = Zamowienie.Nazwa,
            //    Ilosc = Zamowienie.Ilosc,
            //    Cena = Zamowienie.Cena,
            //    DocelowaIlosc = Zamowienie.DocelowaIlosc,
            //    Producent = new ZamowienieProducent
            //    {
            //        Nazwa = Zamowienie.ProducentNazwa
            //    }
            //});
            _zamowieniaContext.Remove(_zamowieniaContext.GetById(zamowienie.Id));
        }

        [Query()]
        public IQueryable<ZamowieniePOCO> GetZamowienia()
        {
            return (
                from r in this._zamowieniaContext.GetAll().AsQueryable()
                where r.DataZlozenia != null
                select POCOHelpers.MapZamowienieToPOCO(r));
        }

        [Query()]
        public IQueryable<ZamowieniePOCO> GetZamowieniaNieprzydzielone()
        {
            return (
                from r in this._zamowieniaContext.GetAll().AsQueryable()
                where r.Pracownik == null
                select POCOHelpers.MapZamowienieToPOCO(r));
        }

        [Query()]
        public IQueryable<ZamowieniePOCO> GetZamowieniaByPracownikId(int pracownikID)
        {
            return (
                from r in this._zamowieniaContext.GetAll().AsQueryable()
                where r.Pracownik != null && r.Pracownik.Id == pracownikID
                select POCOHelpers.MapZamowienieToPOCO(r));
        }

        [Query()]
        public IQueryable<ZamowieniePOCO> GetZamowieniaByKlientId(int klientID)
        {
            return (
                from r in this._zamowieniaContext.GetAll().AsQueryable()
                where r.Klient.Id == klientID && r.DataZlozenia != null
                select POCOHelpers.MapZamowienieToPOCO(r));
        }

        [Query()]
        public IQueryable<ZamowieniePOCO> GetAktualneZamowienieByKlientId(int klientID)
        {
            return (
                from r in this._zamowieniaContext.GetAll().AsQueryable()
                where r.Klient.Id == klientID && r.DataZlozenia == null
                select POCOHelpers.MapZamowienieToPOCO(r));
        }

        #endregion Zamowienia CRUD

        #region Koszyk CRUD

        [Insert()]
        public void InsertZamowienieKoszyk(ZamowieniaKoszykPOCO zamowienieKoszyk)
        {
            Produkty p = _produktyContext.GetById(zamowienieKoszyk.ProduktID);
            Zamowienie z = _zamowieniaContext.GetById(zamowienieKoszyk.ZamowienieID);

            _koszykContext.Add(new ZamowieniaKoszyk
            {
                Ilosc = zamowienieKoszyk.Ilosc,
                Produkt = p,
                Zamowienie = z
            });
        }

        [Query()]
        public IQueryable<ZamowieniaKoszykPOCO> GetKoszykByZamowienieId(int zamowienieID)
        {
            return (
                from r in this._koszykContext.GetAll().AsQueryable()
                where r.Zamowienie.Id == zamowienieID
                select POCOHelpers.MapKoszykToPOCO(r));
        }


        [Query()]
        public IQueryable<KoszykPOCO> GetKoszyki()
        {
            return (
                from r in this._koszykContext.GetAll().AsQueryable() orderby r.Zamowienie.DataZrealizowania ascending
                select POCOHelpers.MapKoszyk1ToPOCO(r));
        }


        #endregion Koszyk CRUD

        #region Helper Queries

        

        #endregion Helper Queries
        
        #endregion CRUDs
    }
}


