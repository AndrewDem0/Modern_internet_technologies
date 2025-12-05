using System.ComponentModel.DataAnnotations;

namespace WebApplication.Data.Models 
{
    public enum EngineVolume
    {
        [Display(Name = "1.3 л")]
        V1_3,
        [Display(Name = "1.8 л")]
        V1_8,
        [Display(Name = "2.0 л")]
        V2_0
    }

    public enum CarClass
    {
        [Display(Name = "Економ")]
        Economy,
        [Display(Name = "Бізнес")]
        Business,
        [Display(Name = "Преміум")]
        Premium
    }

    public enum CarCondition
    {
        [Display(Name = "Нова")]
        New,
        [Display(Name = "Б/В")]
        Used
    }

    public enum CarType
    {
        [Display(Name = "Седан")]
        Sedan,
        [Display(Name = "Універсал")]
        StationWagon,
        [Display(Name = "Хетчбек")]
        Hatchback,
        [Display(Name = "Позашляховик")]
        SUV
    }

    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введіть модель")]
        [Display(Name = "Модель")]
        public string Model { get; set; }

        [Display(Name = "Рік випуску")]
        public int Year { get; set; }

        [Display(Name = "Об'єм двигуна")]
        public EngineVolume EngineVolume { get; set; }

        [Display(Name = "Ціна ($)")]
        [Range(0, double.MaxValue, ErrorMessage = "Ціна має бути більше 0")]
        public decimal Price { get; set; }

        [Display(Name = "Клас авто")]
        public CarClass Class { get; set; }

        [Display(Name = "Стан")]
        public CarCondition Condition { get; set; }

        [Display(Name = "Пробіг (км)")]
        public int Mileage { get; set; }

        [Display(Name = "Тип кузова")]
        public CarType Type { get; set; }

        [Display(Name = "Посилання на фото")]
        public string ImageUrl { get; set; }
    }
}