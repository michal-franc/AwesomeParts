using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BazaDanych.Entities;

namespace BazaDanych.Mappings
{
    class KlientMap : ClassMap<Klient>
    {
        public KlientMap()
        {
            Id(x => x.Id);

            Map(x => x.User_id);
            Map(x => x.Imie);
            Map(x => x.Nazwisko);
            Map(x => x.Login);
            Map(x => x.Haslo);
            Map(x => x.Email);
            Map(x => x.Telefon);
            Map(x => x.Firma);
            Map(x => x.NIP);
            Map(x => x.Ulica);
            Map(x => x.Numer);
            Map(x => x.Miasto);
            Map(x => x.KodPocztowy);
            Map(x => x.Kraj);

            HasMany(x => x.Zamowienia).Not.LazyLoad().Cascade.SaveUpdate();
        }
    }
}
