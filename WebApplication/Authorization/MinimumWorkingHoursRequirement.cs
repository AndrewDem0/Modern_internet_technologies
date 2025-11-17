using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Authorization
{
    public class MinimumWorkingHoursRequirement : IAuthorizationRequirement
    {
        public int MinimumHours { get; }

        public MinimumWorkingHoursRequirement(int minimumHours)
        {
            MinimumHours = minimumHours;
        }
    }
}