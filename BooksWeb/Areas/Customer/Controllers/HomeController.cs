
using Books.DataAccess.Repository.IRepository;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BooksWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unit;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unit)
        {
            _logger = logger;
            _unit = unit;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unit.ProductRepo.GetAll(includeProps: "Category");

            return View(productList);
        }

        public IActionResult Details(int id)
        {
            Product product = _unit.ProductRepo.Get(u=> u.Id == id ,includeProps: "Category");

            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}