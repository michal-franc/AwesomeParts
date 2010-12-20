using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BazaDanych.Entities;

namespace BazaDanych.Repositories
{
    public  class PracownikRepository : Repository<Pracownik>
    {    
        public IList<Pracownik> GetByRola(string rola)
        {
            return GetByQuery(String.Format("from Pracownik p where p.LoginRola.Rola = '{0}'",rola));
        }

        public IList<Pracownik> GetByStatus(string status)
        {
            return GetByQuery(String.Format("from Pracownik p where p.Status.Status = '{0}'", status));
        }

        public IList<Pracownik> GetByRodzaj(string rodzaj)
        {
            return GetByQuery(String.Format("from Pracownik p where p.Rodzaj.Rodzaj= '{0}'", rodzaj));
        }

        public Pracownik GetByImieNazwisko(string imie, string nazwisko)
        {
            return GetByQuery(String.Format("from Pracownik p where p.Imie= '{0}' and p.Nazwisko = '{1}'", imie,nazwisko)).FirstOrDefault();
        }

        public IList<Pracownik> CanGetByDataPodpisaniaUmowy(DateTime dateTime, DateTime dateTime_2, string sort = "asc")
        {
            return GetAll().Where(x => x.AktualnaUmowa.DataPodpisania >= dateTime && x.AktualnaUmowa.DataPodpisania <= dateTime_2).ToList();
        }
    }
}
