using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using BazaDanych.Entities;

namespace BazaDanych.Mappings
{
    public class LoginRolaMap : ClassMap<LoginRola>
    {

        public LoginRolaMap()
        {
            Id(x => x.ID);
            Map(x => x.Rola);
        }
    }
        
}
