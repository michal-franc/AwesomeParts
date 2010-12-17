
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
    using AwesomeParts.Web.POCOs;
    using BazaDanych.Entities;


    // TODO: Create methods containing your application logic.
    [EnableClientAccess()]
    public class KlientService : DomainService
    {
        private IRepository<Klient> _context = new Repository<Klient>();

        public IQueryable<KlientPOCO> GetKlienci()
        {
            return (
                from r in this._context.GetAll().AsQueryable<Klient>()
                orderby r.Nazwisko
                select new KlientPOCO
                {
                    Id = r.Id,
                    Imie = r.Imie,
                    Nazwisko = r.Nazwisko,
                    Login = r.Login,
                    Haslo = r.Haslo,
                    Telefon = r.Telefon,
                    Firma = r.Firma,
                    NIP = r.NIP,
                    Ulica = r.Ulica,
                    Numer = r.Numer,
                    Miasto = r.Miasto,
                    KodPocztowy = r.KodPocztowy,
                    Kraj = r.Kraj,
                    Rodzaj = new KlientRodzajPOCO
                    {
                        Id = r.Rodzaj.Id,
                        Rodzaj = r.Rodzaj.Rodzaj
                    },
                    LoginRola = new LoginRolaPOCO
                    {
                        ID = r.LoginRola.ID,
                        Rola = r.LoginRola.Rola
                    }
                });
        }
    }
}


