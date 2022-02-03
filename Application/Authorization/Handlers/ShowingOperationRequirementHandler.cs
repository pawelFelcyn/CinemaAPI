using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Application.Authorization;

internal class ShowingOperationRequirementHandler : AuthorizationHandler<ShowingOperationRequirement, Showing>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShowingOperationRequirement requirement, Showing showing)
    {
        if (context.User is null || context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) is null)
        {
            return Task.CompletedTask;
        }


        var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        if (showing.CreatedById == userId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
