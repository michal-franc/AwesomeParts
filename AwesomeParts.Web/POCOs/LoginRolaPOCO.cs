using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AwesomeParts.Web.POCOs
{
    public class LoginRolaPOCO
    {
        [Key]
        public virtual int ID { get; set; }

        public virtual string Rola { get; set; }
    }
}