using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModels
{
    public class OrderViewModel
    {
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "RequiredError")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "StringLengthError")]
        public string ProductName { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "RequiredError")]
        [Range(1, 100, ErrorMessage = "RangeError")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "RequiredError")]
        [Range(0.01, 10000.00, ErrorMessage = "PriceRangeError")]
        public decimal Price { get; set; }

        [Display(Name = "Customer Email")]
        [Required(ErrorMessage = "RequiredError")]
        [EmailAddress(ErrorMessage = "EmailError")]
        [Remote(action: "VerifyEmail", controller: "Order")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Confirm Email")]
        [Required(ErrorMessage = "RequiredError")]
        [Compare("CustomerEmail", ErrorMessage = "CompareError")]
        public string ConfirmCustomerEmail { get; set; }

        [Display(Name = "Promo Code")]
        [Remote(action: "VerifyPromoCode", controller: "Order", AdditionalFields = "Price,Quantity")]
        public string? PromoCode { get; set; }
    }
}