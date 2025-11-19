using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using WebApplication.Configuration;
using WebApplication.Data.Interfaces;
using WebApplication.Data.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWebAppRepository _repository;
        private readonly WebAppConfiguration _config;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IWebAppRepository repository, WebAppConfiguration config, IStringLocalizer<HomeController> localizer)
        {
            _repository = repository;
            _config = config;
            _localizer = localizer;
        }

        [AllowAnonymous]
        public Task<IActionResult> Index()
        {
            var users = _repository.All<ApplicationUser>();

            ViewData["ApplicationName"] = _config.ApplicationName;
            ViewData["ApiUrl"] = _config.MySpecificSetting?.ApiUrl;
            ViewData["WelcomeMessage"] = _localizer["WelcomeMessage"];

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

        [Authorize(Policy = "ForumAccess")]
        public IActionResult Forum()
        {
            return View();
        }
    }
 }