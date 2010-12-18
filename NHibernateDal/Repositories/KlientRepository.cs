using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BazaDanych.Entities;

namespace BazaDanych.Repositories
{
    public class KlientRepository : Repository<Klient>
    {

        public Klient GetByImieNazwisko(string imie, string nazwisko)
        {
            return GetByQuery(String.Format("from Klient k where k.Imie = '{0}' and k.Nazwisko = '{1}'",imie,nazwisko)).FirstOrDefault();
        }

        public IList<Klient> GetByRodzaj(string rodzaj)
        {
            return GetByQuery(String.Format("from Klient k where k.Rodzaj.Rodzaj = '{0}' ", rodzaj)).ToList();

        }
    }
}
