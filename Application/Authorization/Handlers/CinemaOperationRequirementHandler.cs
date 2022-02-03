using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Application.Authorization;

internal class CinemaOperationRequirementHandler : AuthorizationHandler<CinemaOperationRequirement, Cinema>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CinemaOperationRequirement requirement, Cinema cinema)
    {
        if (context.User is null || context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) is null)
        {
            return Task.CompletedTask;
        }


        var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        if (cinema.CreatedById == userId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

