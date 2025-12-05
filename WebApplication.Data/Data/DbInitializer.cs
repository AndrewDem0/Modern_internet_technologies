using System.Linq;
using WebApplication.Data.Models;

namespace WebApplication.Data.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {

            if (context.Cars.Any())
            {
                return; 
            }

            var cars = new Car[]
            {
                new Car { Brand = "Toyota", Model = "Corolla", Year = 2022, Price = 21500 },
                new Car { Brand = "Honda", Model = "Civic", Year = 2023, Price = 24600 },
                new Car { Brand = "Nissan", Model = "GT-R", Year = 2021, Price = 113500 },
                new Car { Brand = "Mazda", Model = "MX-5 Miata", Year = 2023, Price = 28000 },
                new Car { Brand = "Subaru", Model = "Impreza", Year = 2022, Price = 19700 },
                new Car { Brand = "Mitsubishi", Model = "Outlander", Year = 2023, Price = 27500 },
                new Car { Brand = "Lexus", Model = "RX 350", Year = 2023, Price = 48500 },
                new Car { Brand = "Suzuki", Model = "Swift", Year = 2022, Price = 16500 },
                new Car { Brand = "Infiniti", Model = "Q50", Year = 2023, Price = 42600 },
                new Car { Brand = "Acura", Model = "Integra", Year = 2024, Price = 31500 }
            };

            context.Cars.AddRange(cars);

            context.SaveChanges();
        }
    }
}