using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using NHibernate;

namespace BazaDanych.Repositories
{
    public  class Repository<T> : IRepository<T>
    {
        public T GetById(int id)
        {
            T klient;

            klient = GetByFilter("Id",id).FirstOrDefault();

            return klient;
        }
        public void Add(T item)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(item);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        public void AddById(T item,int Id)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(item,Id);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        public void Remove(T item)
        {

            using (var session = SessionFactory.OpenSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        public void Update(T item)
        {

            using (var session = SessionFactory.OpenSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(item);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        public void UpdateById(T item,int Id)
        {

            using (var session = SessionFactory.OpenSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(item,Id);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        public int GetCount()
        {
            int count = 0;
            using (var session = SessionFactory.OpenSession())
            {
                 count = session.CreateCriteria(typeof(T))
                    .List<T>().Count;
                 session.Flush();
            }
            return count;
        }
        public IList<T> GetAll()
        {
            IList<T> returnedList = null;
            using (var session = SessionFactory.OpenSession())
            {
                returnedList = session.CreateCriteria(typeof(T)).List<T>();
                session.Flush();
            }
            return returnedList;
        }

        public IList<T> GetByFilter(string parameterName, object value)
        {
            IList<T> returnedList = null;
            using (var session = SessionFactory.OpenSession())
            {
                returnedList = session.CreateCriteria(typeof(T)).Add(Expression.Eq(parameterName, value)).List<T>();
                session.Flush();
            }
            return returnedList;
        }


        protected IList<T>  GetByCriteria(ICriterion criteria)
        {
            IList<T> returnedList = null;
            using (var session = SessionFactory.OpenSession())
            {
                returnedList = session.CreateCriteria(typeof(T)).Add(criteria).List<T>();
                session.Flush();
            }
            return returnedList;
        }

        protected IList<T> GetByQuery(string query)
        {
            IList<T> returnedList = null;
            using (var session = SessionFactory.OpenSession())
            {
                returnedList = session.CreateQuery(query).List<T>();
                session.Flush();
            }
            return returnedList;
        }
    }
}
