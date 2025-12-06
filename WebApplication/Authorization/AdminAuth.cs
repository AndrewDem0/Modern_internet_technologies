using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication.Authorization
{
    public class AdminEmailRequirement : IAuthorizationRequirement
    {
        public string AdminEmail { get; }

        public AdminEmailRequirement(string adminEmail)
        {
            AdminEmail = adminEmail;
        }
    }

    public class AdminEmailHandler : AuthorizationHandler<AdminEmailRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminEmailRequirement requirement)
        {
            if (context.User.Identity?.IsAuthenticated == true &&
                context.User.Identity.Name == requirement.AdminEmail)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}