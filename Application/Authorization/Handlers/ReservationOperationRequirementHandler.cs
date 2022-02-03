using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Application.Authorization;

internal class ReservationOperationRequirementHandler : AuthorizationHandler<ReservationOperationRequirement, Reservation>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReservationOperationRequirement requirement, Reservation reservation)
    {
        if (context.User is null || context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) is null)
        {
            return Task.CompletedTask;
        }

        var userId  = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

        if (reservation.ReservedById == userId || reservation.Showing.CreatedById == userId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
