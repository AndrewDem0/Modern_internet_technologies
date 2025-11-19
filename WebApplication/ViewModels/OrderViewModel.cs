using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModels
{
    public class OrderViewModel
    {

        [Required(ErrorMessage = "Назва товару є обов'язковою.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Назва товару має бути від 3 до 50 символів.")]
        [Display(Name = "Назва товару")]
        public string ProductName { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Кількість має бути від 1 до 100.")]
        [Display(Name = "Кількість")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 10000.00, ErrorMessage = "Ціна повинна бути більше 0.")]
        [Display(Name = "Ціна за одиницю")]
        public decimal Price { get; set; }

        [Required]
        [Remote(action: "VerifyEmail", controller: "Order")]
        [EmailAddress(ErrorMessage = "Некоректний формат Email.")]
        [Display(Name = "Email замовника")]
        public string CustomerEmail { get; set; }

        [Required]
        [Compare("CustomerEmail", ErrorMessage = "Email адреси не співпадають.")]
        [Display(Name = "Підтвердіть Email")]
        public string ConfirmCustomerEmail { get; set; }


        [Display(Name = "Промокод")]
        [Remote(action: "VerifyPromoCode", controller: "Order", AdditionalFields = "Price,Quantity")]
        public string? PromoCode { get; set; }
    }
}