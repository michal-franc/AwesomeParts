
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


    // TODO: Create methods containing your application logic.
    [EnableClientAccess()]
    public class ProduktyService : DomainService
    {
        private IRepository<Produkty> _context = new Repository<Produkty>();

        [Update]
        public void UpdateProdukt(ProduktPOCO produkt)
        {
            _context.Add(new Produkty
            {
                Nazwa = produkt.Nazwa,
                Ilosc = produkt.Ilosc,
                Cena = produkt.Cena,
                DocelowaIlosc = produkt.DocelowaIlosc,
                Producent = new ProduktProducent
                {
                    Nazwa = produkt.ProducentNazwa
                }
            });
        }

        [Query]
        public IQueryable<ProduktPOCO> GetProdukty()
        {
            return (
                from r in this._context.GetAll().AsQueryable<Produkty>()
                orderby r.Nazwa
                select new ProduktPOCO
                {
                    Id = r.Id,
                    Nazwa = r.Nazwa,
                    Cena = r.Cena,
                    ProducentNazwa = r.Producent.Nazwa,
                    Ilosc = r.Ilosc,
                    DocelowaIlosc = r.DocelowaIlosc
                });
        }

        
    }
}


