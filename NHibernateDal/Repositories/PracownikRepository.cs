using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BazaDanych.Entities;

namespace BazaDanych.Repositories
{
    public  class PracownikRepository : Repository<Pracownik>
    {    
        public IList<Pracownik> GetByRola(string rola)
        {
            return GetByQuery(String.Format("from Pracownik p where p.LoginRola.Rola = '{0}'",rola));
        }
    }
}
