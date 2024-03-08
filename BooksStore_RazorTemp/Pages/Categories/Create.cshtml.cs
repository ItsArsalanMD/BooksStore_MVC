using BooksStore_RazorTemp.Data;
using BooksStore_RazorTemp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksStore_RazorTemp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _data;

        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(AppDbContext data)
        {
            this._data = data;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(Category category)
        {
            _data.Categories.Add(category);
            _data.SaveChanges();
            return RedirectToPage("Category");
        }
    }
}
