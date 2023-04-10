using Microsoft.AspNetCore.Mvc;
using PricePilot.Models;
using PricePilot.Models.Models;
using System.Diagnostics;

namespace PricePilot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Privacy()
        {
            List<Product> products = await Scraper.Init();
            List<Product> productsScrapper2 = await Scraper2.Init();
            return ProductView(products, productsScrapper2);
        }

        private ActionResult ProductView(List<Product> products, List<Product> productsScrapper2)
        {
            return View("Privacy", Tuple.Create(products, productsScrapper2));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}