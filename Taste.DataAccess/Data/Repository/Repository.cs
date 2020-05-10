using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly  DbContext  Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = Context.Set<T>();
        }
        public void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return this.dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[]  { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            }

            if (orderBy !=null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();

        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, 
            string includeProperties = null)
        {

            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            }

            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            var e = this.dbSet.Find(id);
            this.dbSet.Remove(e);
        }

        public void Remove(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> enity)
        {
            this.dbSet.RemoveRange(enity);
        }
    }
}
