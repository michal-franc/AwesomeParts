using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazaDanych.Entities
{
    public class PracownikUmowa
    {
        public virtual int Id { get; private set; }


        public virtual int Placa { get; set; }

        public virtual Nullable<DateTime> DataPodpisania { get; set; }

        public virtual Nullable<DateTime> DataWygasniecia { get; set; }

        public virtual bool Aktualna { get; set; }

        public virtual string Uwagi { get; set; }


        public virtual Pracownik Pracownik { get; set; }
    }
}
