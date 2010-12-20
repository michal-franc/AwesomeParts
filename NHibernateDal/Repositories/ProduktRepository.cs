using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BazaDanych.Entities;

namespace BazaDanych.Repositories
{
    public  class ProduktRepository :Repository<Produkty>
    {
        public IList<Produkty> GetByNazwaLike(string nazwa)
        {
            return GetByQuery(String.Format("from Produkty p where p.Nazwa like  '%{0}%' ", nazwa)).ToList();
        }

        public IList<Produkty> GetInsufficentProducts()
        {
            return GetByQuery(String.Format("from Produkty p where p.Ilosc < p.DocelowaIlosc")).ToList();

        }

        public IList<Produkty> GetByProducent(string producent)
        {
            return GetByQuery(String.Format("from Produkty p where p.Producent.Nazwa = '{0}'",producent)).ToList();
        }

        public IList<Produkty> GetAllDostepneProdukty()
        {
            return GetByQuery(String.Format("from Produkty p where p.Dostepny = 1")).ToList();
        }

    }
}
