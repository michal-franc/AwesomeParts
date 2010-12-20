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
        public int Id { get; set; }

        public ZamowieniePOCO Zamowienie { get; set; }
        public ProduktPOCO Produkt { get; set; }
        public int Ilosc { get; set; }
    }
}