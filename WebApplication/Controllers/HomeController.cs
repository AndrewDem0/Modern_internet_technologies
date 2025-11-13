using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
using WebApplication.Data.Interfaces;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWebAppRepository _repository;

        public HomeController(IWebAppRepository repository)
        {
            _repository = repository;
        }

        public Task<IActionResult> Index()
        {
            var users = _repository.All<ApplicationUser>();

            return View(users); 
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