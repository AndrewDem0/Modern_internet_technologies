using Microsoft.EntityFrameworkCore;
using WebApplication.Data.Data;
using WebApplication.Data.Interfaces;
using WebApplication.Data.Models;

namespace WebApplication.Data.Repositories
{
    public class WebAppRepository : BaseSqlServerRepository<ApplicationDbContext>, IWebAppRepository
    {
        // 1. Оголошуємо змінну для контексту в цьому класі
        private readonly ApplicationDbContext _context;

        public WebAppRepository(ApplicationDbContext db) : base(db)
        {
            // 2. Зберігаємо отриманий db у нашу змінну _context
            _context = db;
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await ReadAll<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // Тепер цей метод бачить _context
        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}