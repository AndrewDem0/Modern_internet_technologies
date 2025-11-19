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

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPromoCode(string promoCode, decimal price, int quantity)
        {
            if (string.Equals(promoCode, "LNU2025", StringComparison.OrdinalIgnoreCase))
            {
                var totalAmount = price * quantity;

                if (totalAmount < 100)
                {
                    return Json($"Промокод '{promoCode}' діє лише для замовлень від 100 грн. Ваша сума: {totalAmount} грн.");
                }
            }
            else
            {

                return Json($"Промокод '{promoCode}' не знайдено.");
            }

            return Json(true);
        }

    }
}