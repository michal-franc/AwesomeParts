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
        public virtual int Id { get; set; }

        public int Placa { get; set; }
        public Nullable<DateTime> DataPodpisania { get; set; }
        public Nullable<DateTime> DataWygasniecia { get; set; }
        public bool Aktualna { get; set; }
        public string Uwagi { get; set; }
        public int PracownikID { get; set; }

        [Include]
        [Association("UmowaPracownik", "PracownikID", "Id", IsForeignKey = true)]
        public PracownikPOCO Pracownik { get; set; }
    }
}