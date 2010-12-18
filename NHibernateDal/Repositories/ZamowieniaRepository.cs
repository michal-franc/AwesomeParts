using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BazaDanych.Entities;
using NHibernate.Criterion;
using System.Globalization;

namespace BazaDanych.Repositories
{
    public class ZamowieniaRepository : Repository<Zamowienie>
    {
        public IList<Zamowienie> GetByPracownikName(string firstName,string lastName)
        {
            return GetByQuery(String.Format("from Zamowienie z where z.Pracownik.Imie = '{0}' and z.Pracownik.Nazwisko = '{1}'", firstName, lastName));
        }

        public IList<Zamowienie> GetByZrealizowaneAndPracownikName(string firstName, string lastName)
        {
            return GetByQuery(String.Format("from Zamowienie z where z.Pracownik.Imie = '{0}' and z.Pracownik.Nazwisko = '{1}' and z.Zrealizowano = true", firstName, lastName));
        }

        public IList<Zamowienie> GetByZrealizowaneAndKlientName(string firstName, string lastName)
        {
            return GetByQuery(String.Format("from Zamowienie z where z.Klient.Imie = '{0}' and z.Klient.Nazwisko = '{1}' and z.Zrealizowano = true", firstName, lastName));
        }

        public IList<Zamowienie> GetByKlientName(string firstName, string lastName)
        {
            return GetByQuery(String.Format("from Zamowienie z where z.Klient.Imie  = '{0}' and z.Klient.Nazwisko = '{1}'", firstName, lastName));
        }

        public IList<Zamowienie> GetByPracownikNull()
        {
            return GetByQuery(String.Format("from Zamowienie z where z.Pracownik is null"));
        }

        public IList<Zamowienie> GetByBeetwenDataZlozenia(DateTime date1, DateTime date2,string sort = "asc")
        {
            return GetByQuery(String.Format("from Zamowienie z where z.DataZlozenia  >= '{0}' and z.DataZlozenia <='{1}' order by z.DataZlozenia  {2}", date1.ToShortDateString(), date2.ToShortDateString(), sort));
        }

        public IList<Zamowienie> GetByDataZlozenia(DateTime dateTime, string sort = "asc")
        {
            return GetByQuery(String.Format("from Zamowienie z where z.DataZlozenia  = '{0}' order by z.DataZlozenia  {1}", dateTime.ToShortDateString(), sort));

        }

        public IList<Zamowienie> GetByGreaterThanDataZlozenia(DateTime dateTime , string sort = "asc")
        {
            return GetByQuery(String.Format("from Zamowienie z where z.DataZlozenia  > '{0}' order by z.DataZlozenia  {1}", dateTime.ToShortDateString(), sort));

        }

        public IList<Zamowienie> GetByLessThanDataZlozenia(DateTime dateTime,string sort = "asc")
        {
            return GetByQuery(String.Format( "from Zamowienie z where z.DataZlozenia  < '{0}' order by z.DataZlozenia {1} ", dateTime.ToShortDateString(), sort));
        }

        public IList<Zamowienie> GetByBeetwenDataZrealizowania(DateTime date1, DateTime date2,string sort = "asc")
        {
            return GetByQuery(String.Format( "from Zamowienie z where z.DataZrealizowania  >= '{0}' and z.DataZrealizowania <='{1}' order by z.DataZrealizowania {2}", date1.ToShortDateString(), date2.ToShortDateString(), sort));
        }

        public IList<Zamowienie> GetByDataZrealizowania(DateTime dateTime,string sort = "asc")
        {
            return GetByQuery(String.Format("from Zamowienie z where z.DataZrealizowania  = '{0}' order by z.DataZrealizowania {1}", dateTime.ToShortDateString(), sort));

        }

        public IList<Zamowienie> GetByGreaterThanDataZrealizowania(DateTime dateTime,string sort = "asc")
        {
            return GetByQuery(String.Format("from Zamowienie z where z.DataZrealizowania  > '{0}' order by z.DataZrealizowania {1} ", dateTime.ToShortDateString(), sort));

        }

        public IList<Zamowienie> GetByLessThanDataZrealizowania(DateTime dateTime,string sort = "asc")
        {
            return GetByQuery(String.Format("from Zamowienie z where z.DataZrealizowania  < '{0}' {1}", dateTime.ToShortDateString(), sort));
        }

        public IList<Produkty> GetProductsByZamowienie(Zamowienie zamowienie)
        {
            IList<Produkty> returnedList = null;
            using (var session = SessionFactory.OpenSession())
            {
                returnedList = session.CreateQuery(String.Format("select p from Produkty p , ZamowieniaKoszyk z where z.Produkt = p and z.Zamowienie = '{0}'", zamowienie.Id)).List<Produkty>();
                session.Flush();
            }
            return returnedList;
        }
    }
}
