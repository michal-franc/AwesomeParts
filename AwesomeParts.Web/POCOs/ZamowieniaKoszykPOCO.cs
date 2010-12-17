using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BazaDanych.Entities;

namespace AwesomeParts.Web.POCOs
{
    public class ZamowieniaKoszykPOCO
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual Zamowienie Zamowienie { get; set; }
        public virtual Produkty Produkt { get; set; }
        public virtual int Ilosc { get; set; }
    }
}