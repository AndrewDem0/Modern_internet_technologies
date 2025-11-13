using Microsoft.EntityFrameworkCore;
using WebApplication.Data.Data;
using WebApplication.Data.Interfaces;
using WebApplication.Data.Models;

namespace WebApplication.Data.Repositories
{

    public class WebAppRepository : BaseSqlServerRepository<ApplicationDbContext>, IWebAppRepository
    {

        public WebAppRepository(ApplicationDbContext db) : base(db)
        {
   
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            
            return await ReadAll<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
