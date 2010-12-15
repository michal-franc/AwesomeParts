using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BazaDanych.Entities;

namespace BazaDanych.Mappings
{
    class PracownikRodzajMap : ClassMap<PracownikRodzaj>
    {
        public PracownikRodzajMap()
        {
            Id(x => x.Id);
            Map(x => x.Rodzaj);
        }
    }
}
