using BooksStore_RazorTemp.Data;
using BooksStore_RazorTemp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksStore_RazorTemp.Pages.Categories
{
    
    public class EditModel : PageModel
    {
        private readonly AppDbContext _data;

        public Category Category { get; set; }

        public EditModel(AppDbContext data)
        {
            this._data = data;
        }

        public void OnGet(int id)
        {
            if(id !=null && id != 0)
            {
                Category = _data.Categories.Find(id);
            }
        }

        public IActionResult OnPost(Category category)
        {
            if(ModelState.IsValid)
            {
                _data.Categories.Update(category);
                _data.SaveChanges();
                return RedirectToPage("Category");
            }
            return Page();
        }
    }
}
