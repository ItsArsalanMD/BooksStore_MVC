using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Books.Models.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly IWebHostEnvironment _webHost;

        public ProductController(IUnitOfWork unit, IWebHostEnvironment webHost)
        {
            this._unit = unit;
            this._webHost = webHost;
        }

        public IActionResult Index()
        {
            List<Product> product = _unit.ProductRepo.GetAll(includeProps:"Category").ToList();
            
            return View(product);
        }

        [HttpGet]
        public IActionResult Upsert(int? id) //updateInsert = Upsert
        { 
            IEnumerable<SelectListItem> categoryList = _unit.CategoryRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            //ViewBag.CategoryList = categoryList;
            //passing category data to product view model for creating products and selecting the category
            ProductVM productVM = new()
            {
                categoryList = categoryList,
                Product = new Product()
            };

            if(id == null || id == 0)
            {
                //create product
                return View(productVM);
            }
            else
            {
                //update product
                productVM.Product = _unit.ProductRepo.Get(u => u.Id == id);
                return View(productVM);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productvm, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHost.WebRootPath; // this variable gives us wwwroot folder path.

                if (file != null)
                {
                    // upload the file and save that in root folder.

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // giving random name to the file and extension ad file name using guid and store that in filename variable.

                    string productPath = Path.Combine(wwwRootPath, @"images\product"); // navigate to product folder path.

                    if(!string.IsNullOrEmpty(productvm.Product.ImageUrl))
                    {
                        //deleting the old image while updating
                        var oldImagePath = Path.Combine(wwwRootPath, productvm.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // saving the image to the given path.
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        // copy the file to the given location - product path variabel.
                        file.CopyTo(fileStream);
                    }

                    // and also save image in product modal in imageUrl property.
                    productvm.Product.ImageUrl = @"\images\product\" + fileName;
                }
                
                // add or update the product
                if(productvm.Product.Id == 0)
                {
                    _unit.ProductRepo.Add(productvm.Product);
                    TempData["success"] = "Product Created Successfuly";
                }
                else
                {
                    _unit.ProductRepo.Update(productvm.Product);
                    TempData["success"] = "Product Updated Successfuly";
                }

                _unit.Save();

                return RedirectToAction("index");
            }
            else
            {
                productvm.categoryList = _unit.CategoryRepo.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productvm);
            }
        }


        #region API CALLS,

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> product = _unit.ProductRepo.GetAll(includeProps:"Category").ToList();
            return Json(new { data = product });
        }

        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            Product? productDelete = _unit.ProductRepo.Get(i => i.Id == Id);
            if (productDelete == null)
            {
                return Json(new { success = false, message = "Eror while deleting" });
            }

            var oldImagePath = Path.Combine(_webHost.WebRootPath, productDelete.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            
            _unit.ProductRepo.Remove(productDelete);
            _unit.Save();

            return RedirectToAction("Index","Product");
        }
        #endregion
    }
}
