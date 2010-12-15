using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazaDanych.Entities
{
    public class LoginRola
    {
        public virtual int ID{ get; private set; }
        public virtual string Rola { get; set; }
    }
}
