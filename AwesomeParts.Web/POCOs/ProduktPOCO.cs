using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;

namespace AwesomeParts.Web.POCOs
{
    public class ProduktPOCO
    {
        [Key]
        public int Id { get; set; }

        public string Nazwa { get; set; }
        public decimal Cena { get; set; }
        public int Ilosc { get; set; }
        public int DocelowaIlosc { get; set; }

        [Include]
        [Association("ProduktProducent", "ProducentID", "Id", IsForeignKey = true)]
        public ProduktProducentPOCO Producent { get; set; }
        public int ProducentID { get; set; }
    }
}