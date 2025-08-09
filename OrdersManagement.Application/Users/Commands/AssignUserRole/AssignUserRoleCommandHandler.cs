using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyResturants.Domain.Entities;
using MyResturants.Domain.Exceptions;

namespace MyResturants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler
    (ILogger<AssignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Assign Role to user : {@request}", request);

        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new CustomNotFoundException(nameof(User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.UserRole)
            ?? throw new CustomNotFoundException(nameof(IdentityRole), request.UserRole);

        await userManager.AddToRoleAsync(user, role.Name!);
    }
}