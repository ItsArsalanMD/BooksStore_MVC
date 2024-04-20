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
    public class ProductRepository : Repository<Product>, IProductRepo
    {
        private readonly AppDbContext _data;

        public ProductRepository(AppDbContext data) : base(data)
        {
            this._data = data;
        }

        public void Update(Product product)
        {
            var productFromDb = _data.Products.FirstOrDefault(u=> u.Id == product.Id);

            if(productFromDb != null)
            {
                productFromDb.Title = product.Title;
                productFromDb.ISBN = product.ISBN;
                productFromDb.Price = product.Price;
                productFromDb.Price50 = product.Price50;
                productFromDb.Price100 = product.Price100;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Description = product.Description;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.Author = product.Author;
                if(product.ImageUrl != null)
                {
                    productFromDb.ImageUrl = product.ImageUrl;
                }

            }
        }
    }
}
