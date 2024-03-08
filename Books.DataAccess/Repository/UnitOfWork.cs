using Books.DataAccess.Repository.IRepository;
using BooksWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _data;

        public ICategoryRepo CategoryRepo { get; private set; }

        public UnitOfWork(AppDbContext data)
        {
            this._data = data;
            CategoryRepo = new CategoryRepository(_data);
        }

        public void Save()
        {
            _data.SaveChanges();
        }
    }
}
