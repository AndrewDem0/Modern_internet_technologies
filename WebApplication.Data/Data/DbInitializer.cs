using System;
using System.Linq;
using WebApplication.Data.Data;
using WebApplication.Data.Models;

namespace WebApplication.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Якщо дані вже є - не додаємо нові
            if (context.Cars.Any())
            {
                return;
            }

            var cars = new Car[]
            {
                new Car
                {
                    Model = "Toyota Corolla",
                    Year = 2022,
                    Price = 21500,
                    EngineVolume = EngineVolume.V1_8,
                    Class = CarClass.Economy,
                    Condition = CarCondition.Used,
                    Mileage = 15000,
                    Type = CarType.Sedan,
                    // Білий седан
                    ImageUrl = "https://images.unsplash.com/photo-1590362891991-f776e747a588?auto=format&fit=crop&w=600&q=80"
                },
                new Car
                {
                    Model = "Honda Civic",
                    Year = 2023,
                    Price = 24600,
                    EngineVolume = EngineVolume.V2_0,
                    Class = CarClass.Economy,
                    Condition = CarCondition.New,
                    Mileage = 0,
                    Type = CarType.Hatchback,
                    // Синій автомобіль
                    ImageUrl = "https://images.unsplash.com/photo-1606152421811-aa9116c92563?auto=format&fit=crop&w=600&q=80"
                },
                new Car
                {
                    Model = "Nissan GT-R",
                    Year = 2021,
                    Price = 113500,
                    EngineVolume = EngineVolume.V2_0,
                    Class = CarClass.Premium,
                    Condition = CarCondition.Used,
                    Mileage = 5000,
                    Type = CarType.Sedan,
                    // Спорткар (вид ззаду/збоку)
                    ImageUrl = "https://images.unsplash.com/photo-1600712242805-5f78671b24da?auto=format&fit=crop&w=600&q=80"
                },
                new Car
                {
                    Model = "Mazda MX-5 Miata",
                    Year = 2023,
                    Price = 28000,
                    EngineVolume = EngineVolume.V2_0,
                    Class = CarClass.Business,
                    Condition = CarCondition.New,
                    Mileage = 10,
                    Type = CarType.Sedan,
                    // Червоний кабріолет
                    ImageUrl = "https://images.unsplash.com/photo-1552519507-da3b142c6e3d?auto=format&fit=crop&w=600&q=80"
                },
                new Car
                {
                    Model = "Subaru Impreza",
                    Year = 2022,
                    Price = 19700,
                    EngineVolume = EngineVolume.V2_0,
                    Class = CarClass.Economy,
                    Condition = CarCondition.Used,
                    Mileage = 22000,
                    Type = CarType.Hatchback,
                    // Сірий хетчбек
                    ImageUrl = "https://images.unsplash.com/photo-1617788138017-80ad40651399?auto=format&fit=crop&w=600&q=80"
                },
                new Car
                {
                    Model = "Mitsubishi Outlander",
                    Year = 2023,
                    Price = 27500,
                    EngineVolume = EngineVolume.V2_0,
                    Class = CarClass.Business,
                    Condition = CarCondition.New,
                    Mileage = 0,
                    Type = CarType.SUV,
                    // Білий позашляховик
                    ImageUrl = "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?auto=format&fit=crop&w=600&q=80"
                },
                new Car
                {
                    Model = "Lexus RX 350",
                    Year = 2023,
                    Price = 48500,
                    EngineVolume = EngineVolume.V2_0,
                    Class = CarClass.Premium,
                    Condition = CarCondition.New,
                    Mileage = 0,
                    Type = CarType.SUV,
                    // Преміум кросовер
                    ImageUrl = "https://images.unsplash.com/photo-1550355291-bbee04a92027?auto=format&fit=crop&w=600&q=80"
                },
                new Car
                {
                    Model = "Suzuki Swift",
                    Year = 2022,
                    Price = 16500,
                    EngineVolume = EngineVolume.V1_3,
                    Class = CarClass.Economy,
                    Condition = CarCondition.Used,
                    Mileage = 18000,
                    Type = CarType.Hatchback,
                    // Червоний компакт
                    ImageUrl = "https://images.unsplash.com/photo-1541899481282-d53bffe3c35d?auto=format&fit=crop&w=600&q=80"
                }
            };

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }
    }
}