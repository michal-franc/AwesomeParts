﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AwesomeParts.Web.POCOs
{
    public class PracownikStatusPOCO
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string Status { get; set; }
    }
}