using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazaDanych.Entities
{
    public class Pracownik
    {
        public virtual int Id { get; private set; }

        public virtual string Login { get; set; }

        public virtual string Haslo{ get; set; }

        public virtual string Imie { get; set; }

        public virtual string Nazwisko { get; set; }

        public virtual string Pesel { get; set; }

        public virtual string UwagiDoStatusu { get; set; }

        public virtual PracownikRodzaj Rodzaj { get; set; }

        public virtual PracownikStatus Status { get; set; }
        public virtual LoginRola LoginRola { get; set; }

        public virtual IList<Zamowienie> Zamowienia { get; set; }
        public virtual IList<PracownikUmowa> Umowy { get; set; }


        public virtual IList<PracownikUmowa> UmowyArchiwalne
        {
            get
            {
                return Umowy.Where(x => x.Aktualna == false).ToList();
            }
        }

        public virtual PracownikUmowa AktualnaUmowa
        {
            get
            {
                return Umowy.Where(x => x.Aktualna == true).FirstOrDefault();
            }
        }
    }
}
