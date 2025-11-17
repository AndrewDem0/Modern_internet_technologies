using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApplication.Authorization
{
    public class ForumAccessHandler : AuthorizationHandler<ForumAccessRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ForumAccessRequirement requirement)
        {

            if (context.User.HasClaim(c => c.Type == "IsMentor") ||
                context.User.HasClaim(c => c.Type == "IsVerifiedUser") ||
                context.User.HasClaim(c => c.Type == "HasForumAccess"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}