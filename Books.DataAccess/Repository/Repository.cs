using Books.DataAccess.Repository.IRepository;
using BooksWeb.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _data;

        internal DbSet<T> dbSet;

        public Repository(AppDbContext data)
        {
            this._data = data;
            this.dbSet = _data.Set<T>();

            _data.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if(!string.IsNullOrEmpty(includeProps))
            {
                foreach(var incProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incProp);
                }
            }


            return query.FirstOrDefault();
        }

        //Category, CoverType comma separated values are getting.
        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            if(!string.IsNullOrEmpty(includeProps))
            {
                foreach(var incProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
