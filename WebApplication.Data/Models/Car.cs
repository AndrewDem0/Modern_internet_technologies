using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data.Models 
{
    public enum EngineVolume
    {
        [Display(Name = "1.3 L")]
        V1_3,
        [Display(Name = "1.8 L")]
        V1_8,
        [Display(Name = "2.0 L")]
        V2_0
    }

    public enum CarClass
    {
        [Display(Name = "Economy")]
        Economy,
        [Display(Name = "Business")]
        Business,
        [Display(Name = "Premium")]
        Premium
    }

    public enum CarCondition
    {
        [Display(Name = "New")]
        New,
        [Display(Name = "Used")]
        Used
    }

    public enum CarType
    {
        [Display(Name = "Sedan")]
        Sedan,
        [Display(Name = "Station Wagon")]
        StationWagon,
        [Display(Name = "Hatchback")]
        Hatchback,
        [Display(Name = "SUV")]
        SUV
    }

    public class Car
    {
        public int Id { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "RequiredField")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "StringLengthError")]
        public string Model { get; set; }

        [Display(Name = "Year")]
        [Required(ErrorMessage = "RequiredField")]
        [Range(1990, 2100, ErrorMessage = "RangeError")]
        public int Year { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "RequiredField")]
        [Range(100, 10000000, ErrorMessage = "PriceRangeError")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Engine Volume")]
        public EngineVolume EngineVolume { get; set; }

        [Display(Name = "Car Class")]
        public CarClass Class { get; set; }

        [Display(Name = "Condition")]
        public CarCondition Condition { get; set; }

        [Display(Name = "Mileage")]
        [Range(0, 1000000, ErrorMessage = "RangeError")]
        public int Mileage { get; set; }

        [Display(Name = "Body Type")]
        public CarType Type { get; set; }

        [Display(Name = "Photo URL")]
        [Required(ErrorMessage = "RequiredField")]
        [Url(ErrorMessage = "UrlError")]
        public string ImageUrl { get; set; }
    }
}