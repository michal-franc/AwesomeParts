using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BazaDanych.Entities;

namespace AwesomeParts.Web.POCOs
{
    public class KlientPOCO
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string Imie { get; set; }
        public virtual string Nazwisko { get; set; }
        public virtual string Login { get; set; }
        public virtual string Haslo { get; set; }
        public virtual string Telefon { get; set; }
        public virtual string Firma { get; set; }
        public virtual string NIP { get; set; }
        public virtual string Ulica { get; set; }
        public virtual string Numer { get; set; }
        public virtual string Miasto { get; set; }
        public virtual string KodPocztowy { get; set; }
        public virtual string Kraj { get; set; }
        public virtual KlientRodzajPOCO Rodzaj { get; set; }
        public virtual LoginRolaPOCO LoginRola { get; set; }
    }
}