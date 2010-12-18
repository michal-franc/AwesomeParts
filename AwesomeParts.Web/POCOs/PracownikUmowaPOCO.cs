using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BazaDanych.Entities;

namespace AwesomeParts.Web.POCOs
{
    public class PracownikUmowaPOCO
    {
        [Key]
        public virtual int Id { get; private set; }

        public virtual int Placa { get; set; }
        public virtual Nullable<DateTime> DataPodpisania { get; set; }
        public virtual Nullable<DateTime> DataWygasniecia { get; set; }
        public virtual bool Aktualna { get; set; }
        public virtual string Uwagi { get; set; }
        public virtual PracownikPOCO Pracownik { get; set; }
    }
}