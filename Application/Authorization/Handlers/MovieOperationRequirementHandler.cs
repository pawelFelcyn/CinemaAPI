using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Application.Authorization;

internal class MovieOperationRequirementHandler : AuthorizationHandler<MovieOperationRequirement, Movie>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MovieOperationRequirement requirement, Movie movie)
    {
        if (context.User is null || context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) is null)
        {
            return Task.CompletedTask;
        }


        var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        if (movie.CreatedById == userId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
