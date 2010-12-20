using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AwesomeParts.Web.POCOs.MiniPOCOs
{
    public class PracownikUmowaMiniPOCO
    {
        [Key]
        public virtual int Id { get; set; }

        public Nullable<DateTime> DataPodpisania { get; set; }
        public bool Aktualna { get; set; }
    }
}