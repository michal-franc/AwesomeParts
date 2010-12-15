using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BazaDanych.Entities;


namespace BazaDanych.Mappings
{
    public class ZamowieniaKoszykMap : ClassMap<ZamowieniaKoszyk>
    {
        public ZamowieniaKoszykMap()
        {
            Id(x => x.Id);
            Map(x => x.Ilosc);

            References(x => x.Produkt).LazyLoad(Laziness.False).Cascade.SaveUpdate();
            References(x => x.Zamowienie).LazyLoad(Laziness.False).Cascade.SaveUpdate();
        }
    }
}
