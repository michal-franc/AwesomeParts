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
        public int Id { get; set; }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Telefon { get; set; }
        public string Firma { get; set; }
        public string NIP { get; set; }
        public string Ulica { get; set; }
        public string Numer { get; set; }
        public string Miasto { get; set; }
        public string KodPocztowy { get; set; }
        public string Kraj { get; set; }
        public KlientRodzajPOCO Rodzaj { get; set; }
    }
}