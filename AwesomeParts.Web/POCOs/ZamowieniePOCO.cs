using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BazaDanych.Entities;

namespace AwesomeParts.Web.POCOs
{
    public class ZamowieniePOCO
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual Klient Klient { get; set; }
        public virtual Pracownik Pracownik { get; set; }
        public virtual Nullable<DateTime> DataZlozenia { get; set; }
        public virtual Nullable<DateTime> DataZrealizowania { get; set; }
        public virtual bool Zrealizowano { get; set; }
    }
}