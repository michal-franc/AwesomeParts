using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazaDanych.Entities
{
    public class PracownikStatus
    {
        public virtual int Id { get; private set; }

        public virtual string Status { get; set; }
    }
}
