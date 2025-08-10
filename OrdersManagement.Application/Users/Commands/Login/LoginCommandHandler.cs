using MediatR;
using Microsoft.AspNetCore.Identity;
using OrdersManagement.Application.Common.Responses;
using OrdersManagement.Application.Common.Services;
using OrdersManagement.Application.Helpers;
using OrdersManagement.Domain.Entities.User_Module;
using System.Net;

namespace MyResturants.Application.Users.Commands.Login;

public class LoginCommandHandler
    (ILoggerHelper<LoginCommandHandler> logger,
    UserManager<User> userManager,
    IJwtTokenService jwtTokenService) : IRequestHandler<LoginCommand, CustomResultDTO<LoginResponse>>
{
    public async Task<CustomResultDTO<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Login attempt for user: {Email}", request.Email);

        try
        {
            // Find user by email
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                logger.LogWarning("Login failed: User not found with email {Email}", request.Email);
                return CustomResultDTO<LoginResponse>.Failure(
                    message: "Invalid email or password",
                    statusCode: HttpStatusCode.Unauthorized,
                    errors: new[] { "Invalid email or password" }
                );
            }

            // Check password
            var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                logger.LogWarning("Login failed: Invalid password for user {Email}", request.Email);
                return CustomResultDTO<LoginResponse>.Failure(
                    message: "Invalid email or password",
                    statusCode: HttpStatusCode.Unauthorized,
                    errors: new[] { "Invalid email or password" }
                );
            }

            // Check if email is confirmed (if required)
            if (!user.EmailConfirmed)
            {
                logger.LogWarning("Login failed: Email not confirmed for user {Email}", request.Email);
                return CustomResultDTO<LoginResponse>.Failure(
                    message: "Email not confirmed",
                    statusCode: HttpStatusCode.Forbidden,
                    errors: new[] { "Email not confirmed" }
                );
            }

            // Get user roles
            var userRoles = await userManager.GetRolesAsync(user);
            
            // Generate JWT token using unified service
            var tokenDTO = jwtTokenService.CreateTokenDto(user, userRoles);

            var response = new LoginResponse
            {
                UserId = user.Id.ToString(),
                UserName = $"{user.FirstName} {user.LastName}",
                Email = user.Email!
            };

            logger.LogInformation("User logged in successfully: {Email} with ID {UserId}", request.Email, user.Id);
            
            return CustomResultDTO<LoginResponse>.Success(
                message: "Login successful",
                data: response,
                tokenResult: tokenDTO
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during user login for {Email}", request.Email);
            
            return CustomResultDTO<LoginResponse>.Failure(
                message: "An error occurred during login",
                statusCode: HttpStatusCode.InternalServerError,
                errors: new[] { "An unexpected error occurred" }
            );
        }
    }


}
