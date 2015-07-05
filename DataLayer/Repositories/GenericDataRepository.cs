using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        public virtual IList<T> GetAll()
        {
            List<T> list;
            using (var context = new BooksEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();


                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }

        public virtual IList<T> GetAll(Expression<Func<T, bool>> where)
        {
            List<T> list;
            using (var context = new BooksEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();


                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual T GetSingle(Expression<Func<T, bool>> where)
        {
            T item = null;
            using (var context = new BooksEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }


        public virtual int Add(params T[] items)
        {
            int res = 0;
            using (var context = new BooksEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Added;
                }
                res=context.SaveChanges();
            }
            return res;
        }

        public virtual int Update(params T[] items)
        {
            int res = 0;
            using (var context = new BooksEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                res=context.SaveChanges();
            }
            return res;
        }

        public virtual int Remove(params T[] items)
        {
            int res = 0;
            using (var context = new BooksEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    
                }
                res=context.SaveChanges();
            }

            return res;
        }

        public virtual int RemoveAll(IList<T> itemList )
        {
            int res = 0;
            using (var context = new BooksEntities())
            {

                context.Set<T>().RemoveRange(itemList);
                res = context.SaveChanges();
            }

            return res;
        }

        public virtual IList<T> GetLazyData(Expression<Func<T, bool>> where, Expression<Func<T, IComparable>> orderBy, int pageIndex)
        {
            int PageRows = 20;
            int from = PageRows * pageIndex;
            int to = PageRows;

            List<T> list;
            using (var context = new BooksEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                if (where != null)
                {
                    list = dbQuery
                    .AsNoTracking().OrderBy(orderBy)
                    .Where(where).Skip(from).Take(to)
                    .ToList<T>();
                }
                else
                {
                    list = dbQuery
                    .AsNoTracking().OrderBy(orderBy)
                    .Skip(from).Take(to)
                    .ToList<T>();
                }

            }
            return list;
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            int result = 0;
            using (var context = new BooksEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                result = dbQuery.Count(where);
            }
            return result;
        }
        
    }
}
