using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazaDanych.Entities
{
    public class KlientRodzaj
    {
        public virtual int Id { get; private set; }
        public virtual string Rodzaj { get; set; }
    }
}
