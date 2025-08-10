using MediatR;
using Microsoft.AspNetCore.Identity;
using MyResturants.Domain.Constants;
using OrdersManagement.Application.Common.Responses;
using OrdersManagement.Application.Common.Services;
using OrdersManagement.Application.Helpers;
using OrdersManagement.Domain.Entities.User_Module;
using System.Net;

namespace MyResturants.Application.Users.Commands.Register;

public class RegisterCommandHandler
    (ILoggerHelper<RegisterCommandHandler> logger,
    UserManager<User> userManager,
    IJwtTokenService jwtTokenService) : IRequestHandler<RegisterCommand, CustomResultDTO<RegisterResponse>>
{
    public async Task<CustomResultDTO<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Registering new user with email: {Email}", request.Email);

        try
        {
            // Check if user already exists
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                logger.LogWarning("Registration failed: User with email {Email} already exists", request.Email);
                return CustomResultDTO<RegisterResponse>.Failure(
                    message: "User with this email already exists",
                    statusCode: HttpStatusCode.Conflict,
                    errors: new[] { "User with this email already exists" }
                );
            }

            // Create new user
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Nationality = request.Nationality,
                EmailConfirmed = true // Auto-confirm for demo purposes
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Assign default Customer role
                await userManager.AddToRoleAsync(user, UserRoles.Customer);
                
                // Get user roles (including the newly assigned Customer role)
                var userRoles = await userManager.GetRolesAsync(user);
                
                // Generate JWT token using unified service
                var tokenDTO = jwtTokenService.CreateTokenDto(user, userRoles);

                var response = new RegisterResponse
                {
                    UserId = user.Id.ToString(),
                    Email = user.Email!,
                    UserName = $"{user.FirstName} {user.LastName}"
                };

                logger.LogInformation("User registered successfully: {Email} with ID {UserId}", request.Email, user.Id);
                
                return CustomResultDTO<RegisterResponse>.Success(
                    statusCode : HttpStatusCode.Created,
                    message: "User registered successfully",
                    data: response,
                    tokenResult: tokenDTO
                );
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                logger.LogWarning("Registration failed for {Email}: {Errors}", request.Email, string.Join(", ", errors));
                
                return CustomResultDTO<RegisterResponse>.Failure(
                    message: "Registration failed",
                    statusCode: HttpStatusCode.BadRequest,
                    errors: errors
                );
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during user registration for {Email}", request.Email);
            
            return CustomResultDTO<RegisterResponse>.Failure(
                message: "An error occurred during registration",
                statusCode: HttpStatusCode.InternalServerError,
                errors: new[] { "An unexpected error occurred" }
            );
        }
    }


}
