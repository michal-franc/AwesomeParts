using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BazaDanych.Entities;

namespace BazaDanych.Mappings
{
    class ProduktyMap : ClassMap<Produkty>
    {
        public ProduktyMap()
        {
            Id(x => x.Id);

            Map(x => x.Nazwa);
            Map(x => x.Cena);
            Map(x => x.DocelowaIlosc);
            Map(x => x.Ilosc);
            Map(x => x.Dostepny);
               

            References(x => x.Producent).LazyLoad(Laziness.False).Cascade.SaveUpdate();
        }
    }
}
