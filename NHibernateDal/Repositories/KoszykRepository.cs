using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BazaDanych.Entities;

namespace BazaDanych.Repositories
{
    public class KoszykRepository : Repository<ZamowieniaKoszyk>
    {
        public int GetProductsSoldByYear(int year, string produktNazwa)
        {
            int counter =0;
            foreach (ZamowieniaKoszyk koszyk in this.GetByQuery(String.Format("from ZamowieniaKoszyk k where k.Produkt.Nazwa = '{0}' ", produktNazwa)))
            {
                if (koszyk.Zamowienie.DataZrealizowania.HasValue && koszyk.Zamowienie.DataZrealizowania.Value.Year == year)
                {
                    counter += koszyk.Ilosc;
                }
            }

            return counter;

        }

        public int GetProductsSoldByMonth(int month,int year,string produktNazwa)
        {
            int counter = 0;
            foreach (ZamowieniaKoszyk koszyk in this.GetByQuery(String.Format("from ZamowieniaKoszyk k where k.Produkt.Nazwa = '{0}' ", produktNazwa, month, year)))
            {
                if (koszyk.Zamowienie.DataZrealizowania.HasValue && koszyk.Zamowienie.DataZrealizowania.Value.Month == month && koszyk.Zamowienie.DataZrealizowania.Value.Year == year)
                {
                    counter += koszyk.Ilosc;
                }
            }
            return counter;
        }

        public int GetAllProductsSoldByYear(int year)
        {
            int counter = 0;
            foreach (ZamowieniaKoszyk koszyk in this.GetAll())
            {
                if (koszyk.Zamowienie.DataZrealizowania.HasValue && koszyk.Zamowienie.DataZrealizowania.Value.Year ==year)
                {
                    counter += koszyk.Ilosc;
                }
            }

            return counter;

        }

        public int GetAllProductsSoldByYearAndMonth(int year, int month)
        {
            int counter = 0;
            foreach (ZamowieniaKoszyk koszyk in this.GetAll())
            {
                if (koszyk.Zamowienie.DataZrealizowania.HasValue && koszyk.Zamowienie.DataZrealizowania.Value.Year == year && koszyk.Zamowienie.DataZrealizowania.Value.Month ==month)
                {
                    counter += koszyk.Ilosc;
                }
            }
            return counter;
        }
    }
}
