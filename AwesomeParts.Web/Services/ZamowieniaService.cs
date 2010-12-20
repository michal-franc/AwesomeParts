
namespace AwesomeParts.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using BazaDanych.Repositories;
    using BazaDanych.Entities;
    using AwesomeParts.Web.POCOs;


    // TODO: Create methods containing your application logic.
    [EnableClientAccess()]
    public class ZamowieniaService : DomainService
    {
        private ZamowieniaRepository _zamowieniaContext = new ZamowieniaRepository();
        private KlientRepository _klienciContext = new KlientRepository();
        private PracownikRepository _pracownicyContext = new PracownikRepository();

        [Insert()]
        public void InsertZamowienie(ZamowieniePOCO zamowienie)
        {
            _zamowieniaContext.Add(new Zamowienie
            {
                Klient = _klienciContext.GetById(zamowienie.Klient.Id),
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
            z.Pracownik = _pracownicyContext.GetById(zamowienie.Pracownik.Id);
            z.DataZrealizowania = zamowienie.DataZrealizowania;
            z.Zrealizowano = zamowienie.Zrealizowano;

            _zamowieniaContext.UpdateById(z, zamowienie.Id);
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
                from r in this._zamowieniaContext.GetAll().AsQueryable<Zamowienie>()
                select POCOHelpers.MapZamowienieToPOCO(r));
        }
    }
}


