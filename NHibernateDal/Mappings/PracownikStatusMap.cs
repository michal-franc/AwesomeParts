using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BazaDanych.Entities;

namespace BazaDanych.Mappings
{
    class PracownikStatusMap : ClassMap<PracownikStatus>
    {
        public PracownikStatusMap()
        {
            Id(x => x.Id);
            Map(x => x.Status);
        }
    }
}
