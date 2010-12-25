using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BazaDanych.Repositories
{
    public interface IRepository<T>
    {
        IList<T> GetByFilter(string parameterName,object value);
        T GetById(int id);

        IList<T> GetAll();
        int GetCount();
        int Add(T item);
        void AddById(T item, int Id);
        void Remove(T item);
        void Update(T item);
        void UpdateById(T item,int Id);
    }
}
