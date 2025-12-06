using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Потрібен для FirstOrDefaultAsync
using System.Threading.Tasks;
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

            int pageSize = 6 ;

            var paginatedCars = await PaginatedList<Car>.CreateAsync(carsQuery, pageNumber ?? 1, pageSize);

            return View(paginatedCars);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _repository.ReadAll<Car>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _repository.ReadAll<Car>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _repository.ReadAll<Car>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car != null)
            {

                await _repository.DeleteAsync(car);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}