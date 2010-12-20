using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazaDanych.Entities
{
    public class Produkty
    {
        public virtual int Id { get; private set; }

        public virtual string Nazwa { get; set; }

        public virtual decimal Cena { get; set;}
        /// <summary>
        /// Dostepna ilosc danego produkty w sztukach
        /// </summary>
        public virtual int Ilosc { get; set; }
        /// <summary>
        /// Przewidywana ilosc danego produktu w sztukach. Zawarta w niezrealizowanych zamowieniach.
        /// </summary>
        public virtual int DocelowaIlosc { get; set; }

        public virtual bool Dostepny { get; set; }

        public virtual ProduktProducent Producent { get; set; }


    }
}
