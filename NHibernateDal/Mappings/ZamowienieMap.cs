using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BazaDanych.Entities;

namespace BazaDanych.Mappings
{
    class ZamowienieMap : ClassMap<Zamowienie>
    {
        public ZamowienieMap()
        {
            Id(x => x.Id);

            Map(x => x.Zrealizowano);
            Map(x => x.DataZlozenia).Nullable();
            Map(x => x.DataZrealizowania).Nullable();

            References(x => x.Klient).LazyLoad(Laziness.False);
            References(x => x.Pracownik).LazyLoad(Laziness.False);

            //HasManyToMany(x => x.ZamowioneProdukty).Table("ZamowieniaKoszyk").ChildKeyColumn("Produkt_id").ParentKeyColumn("Zamowienie_id").Not.LazyLoad();
        }
    }
}
