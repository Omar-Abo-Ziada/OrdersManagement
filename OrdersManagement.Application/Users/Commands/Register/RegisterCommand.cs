using MediatR;
using OrdersManagement.Application.Common.Responses;

namespace MyResturants.Application.Users.Commands.Register;

public class RegisterCommand : IRequest<CustomResultDTO<RegisterResponse>>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}

public class RegisterResponse
{
    public string UserId { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
}
