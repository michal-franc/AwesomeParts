using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BazaDanych.Entities;
using System.ServiceModel.DomainServices.Server;
using AwesomeParts.Web.POCOs.MiniPOCOs;

namespace AwesomeParts.Web.POCOs
{
    public class PracownikPOCO
    {
        [Key]
        public virtual int Id { get; set; }

        public int UserID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public string UwagiDoStatusu { get; set; }
        public int RodzajID { get; set; }
        public int StatusID { get; set; }

        [Include]
        [Association("PracownikRodzaj", "RodzajID", "Id", IsForeignKey = true)]
        public PracownikRodzajPOCO Rodzaj { get; set; }

        [Include]
        [Association("PracownikStatus", "RodzajID", "Id", IsForeignKey = true)]
        public PracownikStatusPOCO Status { get; set; }

        //[Include]
        //public IList<ZamowienieMiniPOCO> Zamowienia { get; set; }

        //[Include]
        //public IList<PracownikUmowaMiniPOCO> UmowyArchiwalne { get; set; }

        //[Include]
        //public PracownikUmowaMiniPOCO AktualnaUmowa { get; set; }
    }
}