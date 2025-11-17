using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApplication.Data.Models; 

namespace WebApplication.Authorization
{
    public class IsAuthorHandler : AuthorizationHandler<IsAuthorRequirement, Material>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IsAuthorRequirement requirement,
            Material resource) 
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null && resource.AuthorId == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}