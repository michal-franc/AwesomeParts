using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AwesomeParts.Web.POCOs
{
    public class PracownikRodzajPOCO
    {
        [Key]
        public int Id { get; set; }

        public string Rodzaj { get; set; }
    }
}