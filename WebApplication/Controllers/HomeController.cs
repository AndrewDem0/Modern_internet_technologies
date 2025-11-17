using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data.Models;
using WebApplication.Data.Interfaces;
using WebApplication.Configuration;
using Microsoft.AspNetCore.Authorization;

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
        [AllowAnonymous]
        public Task<IActionResult> Index()
        {
            var users = _repository.All<ApplicationUser>();

            ViewData["ApplicationName"] = _config.ApplicationName;
            ViewData["ApiUrl"] = _config.MySpecificSetting?.ApiUrl;

            return Task.FromResult<IActionResult>(View(users));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Policy = "ArchiveAccessPolicy")]
        public IActionResult Archive()
        {
            return View();
        }

        [Authorize(Policy = "PremiumAccess")]
        public IActionResult Premium()
        {
            return View();
        }
    }
}