using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Interfaces;
using WebApplication.Data.Models;

namespace WebApplication.Controllers
{
    public class CarsController : Controller
    {
        private readonly IWebAppRepository _repository;

        public CarsController(IWebAppRepository repository)
        {
            _repository = repository;
        }


        public async Task<IActionResult> Index(int? pageNumber)
        {

            var carsQuery = _repository.ReadAll<Car>();

            int pageSize = 3;

            var paginatedCars = await PaginatedList<Car>.CreateAsync(carsQuery, pageNumber ?? 1, pageSize);

            return View(paginatedCars);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }
    }
}