using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazaDanych.Entities
{
    public class ZamowieniaKoszyk
    {
        public virtual int Id { get; private set; }

        public virtual Zamowienie Zamowienie { get; set; }
        public virtual Produkty Produkt { get; set; }
        public virtual int Ilosc { get; set; }
    }
}
