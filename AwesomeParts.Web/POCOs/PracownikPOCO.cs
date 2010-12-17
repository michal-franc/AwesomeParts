using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BazaDanych.Entities;

namespace AwesomeParts.Web.POCOs
{
    public class PracownikPOCO
    {
        [Key]
        public virtual int Id { get; private set; }

        public virtual string Login { get; set; }
        public virtual string Haslo { get; set; }
        public virtual string Imie { get; set; }
        public virtual string Nazwisko { get; set; }
        public virtual string Pesel { get; set; }
        public virtual string UwagiDoStatusu { get; set; }
        public virtual PracownikRodzajPOCO Rodzaj { get; set; }
        public virtual PracownikStatusPOCO Status { get; set; }
        public virtual LoginRolaPOCO LoginRola { get; set; }
        public virtual IList<ZamowieniaKoszykPOCO> Zamowienia { get; set; }
    }
}