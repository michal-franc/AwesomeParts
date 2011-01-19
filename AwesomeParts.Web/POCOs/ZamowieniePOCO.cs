using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BazaDanych.Entities;
using System.ServiceModel.DomainServices.Server;

namespace AwesomeParts.Web.POCOs
{
    public class ZamowieniePOCO
    {
        [Key]
        public int Id { get; set; }

        public Nullable<DateTime> DataZlozenia { get; set; }
        public Nullable<DateTime> DataZrealizowania { get; set; }
        public bool Zrealizowano { get; set; }
        public string KlientNazwa { get; set; }
        public string KlientFirma { get; set; }
        public int KlientID { get; set; }
        public int PracownikID { get; set; }

        [Include]
        [Association("ZamowienieKlient", "KlientID", "Id", IsForeignKey = true)]
        public KlientPOCO Klient { get; set; }

        [Include]
        [Association("ZamowieniePracownik", "PracownikID", "Id", IsForeignKey = true)]
        public PracownikPOCO Pracownik { get; set; }

        #region Wykorzystywane do Chartow
        public int RokZrealizowania
        {
            get
            {
                if (DataZrealizowania.HasValue)
                {
                    return DataZrealizowania.Value.Year;
                }
                else return 0;
            }
        }

        public int MiesiacZrealizowania
        {
            get
            {
                if (DataZrealizowania.HasValue)
                {
                    return DataZrealizowania.Value.Month;
                }
                else return 0;
            }
        }
        #endregion

    }
}