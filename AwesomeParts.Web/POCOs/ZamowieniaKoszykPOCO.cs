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

        public string Nazwa { get; set; }
        public string Producent { get; set; }
        public int Ilosc { get; set; }
        public decimal CenaJednostkowa { get; set; }
        public decimal CenaCalosciowa { get; set; }
        public int ProduktID { get; set; }
        public int ZamowienieID { get; set; }
    }
}