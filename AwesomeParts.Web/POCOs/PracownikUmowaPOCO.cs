using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BazaDanych.Entities;
using System.ServiceModel.DomainServices.Server;

namespace AwesomeParts.Web.POCOs
{
    public class PracownikUmowaPOCO
    {
        [Key]
        public int Id { get; set; }

        public int Placa { get; set; }

        [Display(Name = "Data podpisania", Description = "Data podpisania umowy")]
        public Nullable<DateTime> DataPodpisania { get; set; }

        [Display(Name = "Data wygasniecia", Description = "Data wygaśnięcia umowy")]
        public Nullable<DateTime> DataWygasniecia { get; set; }
        public bool Aktualna { get; set; }
        public string Uwagi { get; set; }
        public int PracownikID { get; set; }

        [Include]
        [Association("UmowaPracownik", "PracownikID", "Id", IsForeignKey = true)]
        public PracownikPOCO Pracownik { get; set; }
    }
}