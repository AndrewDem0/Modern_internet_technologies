using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApplication.Authorization
{
    public class MinimumWorkingHoursHandler
        : AuthorizationHandler<MinimumWorkingHoursRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MinimumWorkingHoursRequirement requirement)
        {
            var workingHoursClaim = context.User.FindFirst("WorkingHours");

            if (workingHoursClaim == null)
            {
                return Task.CompletedTask;
            }

            if (int.TryParse(workingHoursClaim.Value, out int userHours))
            {
                if (userHours >= requirement.MinimumHours)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}