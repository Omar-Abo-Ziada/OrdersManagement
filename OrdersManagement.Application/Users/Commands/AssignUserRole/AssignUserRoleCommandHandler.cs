using MediatR;
using Microsoft.AspNetCore.Identity;
using OrdersManagement.Application.Helpers;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Exceptions;

namespace MyResturants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler
    (ILoggerHelper<AssignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole<int>> roleManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assign Role to user : {@request}", request);

        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new CustomNotFoundException(nameof(User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.UserRole)
            ?? throw new CustomNotFoundException(nameof(IdentityRole<int>), request.UserRole);

        await userManager.AddToRoleAsync(user, role.Name!);
    }
}