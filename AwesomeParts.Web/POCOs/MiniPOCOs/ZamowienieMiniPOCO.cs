using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AwesomeParts.Web.POCOs.MiniPOCOs
{
    public class ZamowienieMiniPOCO
    {
        [Key]
        public int Id { get; set; }

        public Nullable<DateTime> DataZlozenia { get; set; }
        public bool Zrealizowano { get; set; }
    }
}