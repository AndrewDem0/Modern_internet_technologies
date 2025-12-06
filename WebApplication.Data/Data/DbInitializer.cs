using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApplication.Data.Data;
using WebApplication.Data.Models;

namespace WebApplication.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            context.Database.EnsureCreated();

            var adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, "Admin@123");
            }
            if (context.Cars.Any())
            {
                context.Cars.RemoveRange(context.Cars);
                context.SaveChanges();
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
                    // Реальне фото білої Toyota Corolla Sedan
                    ImageUrl = "https://cdn2.riastatic.com/photos/ir/new/auto/photo/toyota_corolla__624647577-620x415x70.jpg"
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
                    // Реальне фото синього Honda Civic Hatchback
                    ImageUrl = "https://cdn.shopify.com/s/files/1/2452/9929/files/350114454_6537828239618872_2212741164459001736_n_1024x1024.jpg?v=1685328945"
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
                    // Реальне фото сріблястого Nissan GT-R
                    ImageUrl = "https://motorcar.com.ua/wp-content/uploads/2024/06/nissan-gt-r-r35-final-editions-2.jpg"
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
                    // Реальне фото червоної Mazda MX-5
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e2/Mazda_Roadster_ND.jpg/1200px-Mazda_Roadster_ND.jpg"
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
                    // Реальне фото Subaru Impreza Hatchback
                    ImageUrl = "https://p.turbosquid.com/ts-thumb/xq/4yWEhv/pk/subaru_impreza_sti_wrc_2006_0000/jpg/1709225106/600x600/fit_q87/7aadef1ea165ceaa6b721920b8948819b72762cd/subaru_impreza_sti_wrc_2006_0000.jpg"
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
                    // Реальне фото Mitsubishi Outlander
                    ImageUrl = "https://i.guim.co.uk/img/media/712a11c56758e939fa9303c13a9cf6b1c37cb207/1568_1239_3853_2311/master/3853.jpg?width=1200&height=900&quality=85&auto=format&fit=crop&s=8dba5b39d4baea9c562b407dfb42d523"
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
                    // Реальне фото Lexus RX
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTZiwZWcruYyMys_096NF6ABA30LdbnJBayzA&s"
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
                    // Реальне фото червоного Suzuki Swift
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTZfSCgOj-gt7ox_MrTOeV3MRnE9-Wjyh_q1g&s"
                }
            };

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }
    }
}