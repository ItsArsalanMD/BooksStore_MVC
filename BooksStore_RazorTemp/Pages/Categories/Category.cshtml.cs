using BooksStore_RazorTemp.Data;
using BooksStore_RazorTemp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksStore_RazorTemp.Pages.Categories
{
    public class CategoryModel : PageModel
    {
        private readonly AppDbContext _data;

        public CategoryModel(AppDbContext data)
        {
            this._data = data;
        }

        public List<Category> categories { get; set; }

        public void OnGet()
        {
            categories = _data.Categories.ToList();
        }

        public void OnPost()
        {

        }

    }
}
