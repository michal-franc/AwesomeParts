using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazaDanych.Entities
{
    public class Zamowienie
    {
        public virtual int Id { get; private set; }

        public virtual Klient Klient { get; set; }

        /// <summary>
        /// Pracownik firmy przydzielony do obsugi zamowienia
        /// </summary>
        public virtual Pracownik Pracownik { get; set; }
        public virtual Nullable<DateTime> DataZlozenia { get; set; }
        public virtual Nullable<DateTime> DataZrealizowania { get; set; }
        
        /// <summary>
        /// Wskaznik oznaczajacy czy zamowienie zostalo zrealizowane
        /// </summary>
        public virtual bool Zrealizowano { get; set; }
    }
}
