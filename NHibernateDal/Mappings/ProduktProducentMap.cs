using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BazaDanych.Entities;

namespace BazaDanych.Mappings
{
    class ProduktProducentMap : ClassMap<ProduktProducent>
    {
        public ProduktProducentMap()
        {
            Id(x => x.Id);
            Map(x => x.Nazwa);
        }
    }
}
