using MediatR;

namespace MyResturants.Application.Users.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommand : IRequest
{
    public string UserEmail { get; set; } = default!;
    public string UserRole { get; set; } = default!;
}