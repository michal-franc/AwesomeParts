using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BazaDanych.Entities;

namespace BazaDanych.Mappings
{
    class PracownikUmowaMap :ClassMap<PracownikUmowa>
    {
        public PracownikUmowaMap()
        {
            Id(x => x.Id);
            Map(x => x.Placa);
            Map(x => x.DataPodpisania);
            Map(x => x.DataWygasniecia);
            Map(x => x.Uwagi);

            References(x => x.Pracownik).LazyLoad(Laziness.False).Cascade.SaveUpdate();
        }
    }
}
