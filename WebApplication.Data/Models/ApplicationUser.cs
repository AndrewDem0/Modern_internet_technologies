using Microsoft.AspNetCore.Identity;

namespace WebApplication.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Ось ваші два нові поля з завдання
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}