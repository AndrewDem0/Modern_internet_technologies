using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Data.Models;

namespace WebApplication.Data.Interfaces
{
    public interface IWebAppRepository : IRepository
    {
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
    }
}
