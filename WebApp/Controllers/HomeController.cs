using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _service;

        public HomeController(ILogger<HomeController> logger, IHomeService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            //Car car = new Car();
            //car.Brand = "BMW";
            //car.Model = "e46";
            //car.Year = 2020;
            //Car.list.Add(car);

            //Car car1 = new Car();
            //car1.Brand = "BMW";
            //car1.Model = "e46";
            //car1.Year = 2020;
            //Car.list.Add(car1);

            return View(Car.list);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Car car)
        {
            _service.AddToList(car);
            return RedirectToAction("Index");
        }
        public IActionResult Delete()
        {
            _service.RemoveToList(0);
            return RedirectToAction("Index");
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
