using Books.DataAccess.Repository.IRepository;
using Books.Models;
using BooksWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepo
    {
        private readonly AppDbContext _data;

        public CategoryRepository(AppDbContext data) : base(data)
        {
            this._data = data;
        }

        public void Update(Category category)
        { 
            _data.Category.Update(category);
        }
    }
}
