using Microsoft.AspNetCore.Mvc;
using WebApplication.ViewModels;
using Microsoft.Extensions.Localization;
namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IStringLocalizer<OrderViewModel> _localizer;

        public OrderController(IStringLocalizer<OrderViewModel> localizer)
        {
            _localizer = localizer;
        }

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
                    return Json(_localizer["PromoCodeAmountError", promoCode, totalAmount].Value);
                }
            }
            else
            {
                return Json(_localizer["PromoCodeInvalidError", promoCode].Value);
            }

            return Json(true);
        }

    }
}