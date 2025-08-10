using MediatR;
using OrdersManagement.Application.Common.Responses;

namespace MyResturants.Application.Users.Commands.Login;

public class LoginCommand : IRequest<CustomResultDTO<LoginResponse>>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class LoginResponse
{
    public string UserId { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
