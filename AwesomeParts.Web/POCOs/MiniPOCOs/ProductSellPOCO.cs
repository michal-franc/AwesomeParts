using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AwesomeParts.Web.POCOs.MiniPOCOs
{
    public class ProductSellPOCO
    {
        [Key]
        public int Id { get; set; }

        public string Nazwa { get; set; }
        public int Ilosc { get; set; }
    }
}