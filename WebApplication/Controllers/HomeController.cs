using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
using WebApplication.Data.Interfaces;
using WebApplication.Configuration;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWebAppRepository _repository;
        private readonly WebAppConfiguration _config;

        public HomeController(IWebAppRepository repository, WebAppConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var users = _repository.All<ApplicationUser>();

            // Example of accessing a specific configuration value
            var currentApiKey = _config.MySpecificSetting?.ApiKey;

            // Access configuration settings
            ViewData["ApplicationName"] = _config.ApplicationName;
            ViewData["ApiUrl"] = _config.MySpecificSetting?.ApiUrl;

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