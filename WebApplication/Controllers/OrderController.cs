using Microsoft.AspNetCore.Mvc;
using WebApplication.ViewModels;
namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrderViewModel model)
        {

            if (ModelState.IsValid)
            {
                return Content($"Замовлення прийнято! Товар: {model.ProductName}, Кількість: {model.Quantity}");
            }

            return View(model);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmail(string customerEmail)
        {

            if (customerEmail.ToLower() == "admin@example.com")
            {
                return Json($"Email {customerEmail} вже зайнятий (для тесту).");
            }

            return Json(true);
        }

    }
}