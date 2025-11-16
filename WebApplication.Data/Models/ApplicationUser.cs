using Microsoft.AspNetCore.Identity;

namespace WebApplication.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}