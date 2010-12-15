using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using BazaDanych.Entities;
using BazaDanych.Mappings;

namespace BazaDanych
{
    public static class SessionFactory
    {

        public static ISession OpenSession()
        {
            return GetSessionFactory().OpenSession();
        }

        private static ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                _sessionFactory = CreateSessionFactory(UpdateSchema);
            }
            return _sessionFactory;
        }
        private static ISessionFactory _sessionFactory;

        public static void ResetSchema()
        {
            CreateSessionFactory(ResetSchema);
        }

        private static ISessionFactory CreateSessionFactory(Action<Configuration> func)
        {
            return Fluently.Configure().
                    Database(MsSqlConfiguration.MsSql2008.ConnectionString
                    ("Data Source=LAM-PC\\SQLDEV;Initial Catalog=AwesomeParts;Integrated Security=SSPI;"))
                    .Mappings(m => m.FluentMappings.Add<ZamowieniaKoszykMap>())
                    .Mappings(m => m.FluentMappings.Add<KlientMap>())
                    .Mappings(m => m.FluentMappings.Add<ProduktyMap>())
                    .Mappings(m => m.FluentMappings.Add<KlientRodzajMap>())
                    .Mappings(m => m.FluentMappings.Add<PracownikMap>())
                    .Mappings(m => m.FluentMappings.Add<PracownikRodzajMap>())
                    .Mappings(m => m.FluentMappings.Add<PracownikStatusMap>())
                    .Mappings(m => m.FluentMappings.Add<PracownikUmowaMap>())
                    .Mappings(m => m.FluentMappings.Add<ProduktProducentMap>())
                    .Mappings(m => m.FluentMappings.Add<ZamowienieMap>())
                    .Mappings(m => m.FluentMappings.Add<LoginRolaMap>())
                    .ExposeConfiguration(func)
                    .BuildSessionFactory();
        }

        private static void UpdateSchema(Configuration config)
        {
            new SchemaUpdate(config).Execute(true, true);
        }

        public static void ResetSchema(Configuration config)
        {
             new SchemaExport(config).Create(true,true);
        }
    }
}
