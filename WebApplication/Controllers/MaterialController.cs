// Файл: WebApplication/Controllers/MaterialController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication.Data.Interfaces;
using WebApplication.Data.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        private readonly IWebAppRepository _repository;
        private readonly IAuthorizationService _authService;

        private readonly UserManager<ApplicationUser> _userManager;

        public MaterialController(IWebAppRepository repository,
                                IAuthorizationService authService,
                                UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _authService = authService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = _userManager.GetUserId(User);

            var materials = await _repository.ReadWhere<Material>(m => m.AuthorId == currentUserId)
                                           .ToListAsync();
            return View(materials);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var material = await _repository.ReadSingleAsync<Material>(m => m.Id == id);

            if (material == null) return NotFound();

            var authorizationResult = await _authService.AuthorizeAsync(
                User, material, "CanManageMaterial");

            if (!authorizationResult.Succeeded)
            {

                return Forbid();
            }

            return View(material);
        }

        public async Task<IActionResult> CreateTestMaterial()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var newMaterial = new Material
            {
                Title = $"Матеріал, створений {currentUser.Email}",
                Content = "Цей матеріал створив я.",
                AuthorId = currentUser.Id
            };

            await _repository.AddAsync(newMaterial);

            return Ok($"Створено матеріал з ID: {newMaterial.Id}");
        }
    }
}