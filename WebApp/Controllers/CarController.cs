using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _carService.GetAll());
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Car car) 
        {
            await _carService.Add(car);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carService.Get(id);
            if (car != null)
                return View(car);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Car car) 
        {
            await _carService.Update(car);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _carService.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.Get(id);
            if (car != null)
                return View(car);

            return RedirectToAction("Index");
        }
    }
}
