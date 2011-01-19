using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BazaDanych.Entities;

namespace BazaDanych.Mappings
{
    class PracownikMap : ClassMap<Pracownik>
    {
        public PracownikMap()
        {
            Id(x => x.Id);
            Map(x => x.User_id);
            Map(x => x.Imie);
            Map(x => x.Nazwisko);
            Map(x => x.Pesel);
            Map(x => x.Login);
            Map(x => x.Haslo);
            Map(x => x.UwagiDoStatusu);

            References(x => x.Rodzaj).LazyLoad(Laziness.False).Cascade.SaveUpdate();
            References(x => x.Status).LazyLoad(Laziness.False).Cascade.SaveUpdate();

            HasMany(x => x.Zamowienia).Not.LazyLoad().Cascade.SaveUpdate();
            HasMany(x => x.Umowy).Not.LazyLoad().Cascade.SaveUpdate();
            
        }
    }
}
