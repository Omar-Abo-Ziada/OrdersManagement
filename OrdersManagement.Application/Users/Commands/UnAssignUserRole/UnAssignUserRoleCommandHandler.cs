using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Exceptions;

namespace MyResturants.Application.Users.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommandHandler
     (ILogger<UnAssignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<UnAssignUserRoleCommand>
{
    public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("UnAssigning Role from user : {@request}", request);

        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new CustomNotFoundException(nameof(User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.UserRole)
            ?? throw new CustomNotFoundException(nameof(IdentityRole), request.UserRole);

        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}   