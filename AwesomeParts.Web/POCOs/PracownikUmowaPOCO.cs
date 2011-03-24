using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BazaDanych.Entities;
using System.ServiceModel.DomainServices.Server;
using AwesomeParts.Web.Resources;

namespace AwesomeParts.Web.POCOs
{
    public class PracownikUmowaPOCO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Pole placa musi się składać tylko z cyfr")]
        public int Placa { get; set; }

        [Required(ErrorMessageResourceName = "ValidationErrorRequiredField", ErrorMessageResourceType = typeof(ValidationErrorResources))]
        [Display(Name = "Data podpisania", Description = "Data podpisania umowy")]
        public Nullable<DateTime> DataPodpisania { get; set; }

        [Display(Name = "Data wygasniecia", Description = "Data wygaśnięcia umowy")]
        public Nullable<DateTime> DataWygasniecia { get; set; }
        public bool Aktualna { get; set; }

        [StringLength(255, MinimumLength = 0, ErrorMessage = "Pole uwagi maksymalna ilość znaków 20.")]
        public string Uwagi { get; set; }

        public int PracownikID { get; set; }

        [Include]
        [Association("UmowaPracownik", "PracownikID", "Id", IsForeignKey = true)]
        public PracownikPOCO Pracownik { get; set; }
    }
}