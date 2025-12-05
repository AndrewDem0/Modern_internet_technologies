using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Потрібен для FirstOrDefaultAsync
using WebApplication.Data.Interfaces;
using WebApplication.Data.Models;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class CarsController : Controller
    {
        private readonly IWebAppRepository _repository;

        public CarsController(IWebAppRepository repository)
        {
            _repository = repository;
        }

        // GET: Cars
        public async Task<IActionResult> Index(int? pageNumber)
        {
            // Використовуємо ваш репозиторій
            var carsQuery = _repository.ReadAll<Car>();

            // Для сітки по 3 авто в ряд краще брати число, кратне 3 (наприклад, 6)
            int pageSize = 6 ;

            var paginatedCars = await PaginatedList<Car>.CreateAsync(carsQuery, pageNumber ?? 1, pageSize);

            return View(paginatedCars);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Шукаємо авто через ReadAll + фільтр
            var car = await _repository.ReadAll<Car>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Знаходимо авто, яке хочемо видалити
            var car = await _repository.ReadAll<Car>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")] // Важливо: вказуємо, що це дія Delete
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Шукаємо авто за ID
            var car = await _repository.ReadAll<Car>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car != null)
            {
                // Викликаємо метод видалення з репозиторію
                await _repository.DeleteAsync(car);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}