using Books.Models;
using Books.DataAccess.Repository.IRepository;
using BooksWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unit;

        public CategoryController(IUnitOfWork data)
        {
            this._unit = data;
        }

        public IActionResult Index()
        {
            List<Category> catlist = _unit.CategoryRepo.GetAll().ToList();
            return View(catlist);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateCategory(Category category)
        {
            if(category.Name == category.DisplayOrders.ToString())
            {
                ModelState.AddModelError("name", "Category Name and Display order cannot be same");
            }

            if(ModelState.IsValid)
            {
                _unit.CategoryRepo.Add(category);
                _unit.Save();
                TempData["success"] = "Category Created Successfuly";
                return RedirectToAction("index");
            }

            return View(category);
            
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }

            Category? category = _unit.CategoryRepo.Get(u => u.Id == Id);

            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                _unit.CategoryRepo.Update(category);
                _unit.Save();
                TempData["success"] = "Category Updated Successfuly";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            Category? category = _unit.CategoryRepo.Get(i => i.Id == Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Daletepost(int? Id)
        {
            Category? category = _unit.CategoryRepo.Get(i => i.Id == Id);
            if (Id == null || Id == 0)
            {
                return NotFound();   
            }

            _unit.CategoryRepo.Remove(category);
            _unit.Save();

            TempData["success"] = "Category Deleted Successfuly";
            return RedirectToAction("Index");
        }
    }
}
