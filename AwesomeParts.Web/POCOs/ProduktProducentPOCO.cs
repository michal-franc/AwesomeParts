using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AwesomeParts.Web.POCOs
{
    public class ProduktProducentPOCO
    {
        [Key]
        public virtual int Id { get; private set; }

        public virtual string Nazwa { get; set; }
    }
}