using OrdersManagement.Application.Common.Responses;
using OrdersManagement.Domain.Entities.User_Module;

namespace OrdersManagement.Application.Common.Services;

public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JWT token for the specified user with roles
    /// </summary>
    /// <param name="user">The user to generate token for</param>
    /// <param name="userRoles">The roles assigned to the user</param>
    /// <returns>JWT token string</returns>
    string GenerateJwtToken(User user, IList<string> userRoles);

    /// <summary>
    /// Creates a complete TokenDTO with all necessary information including roles
    /// </summary>
    /// <param name="user">The user to create token for</param>
    /// <param name="userRoles">The roles assigned to the user</param>
    /// <returns>Complete TokenDTO object</returns>
    TokenDTO CreateTokenDto(User user, IList<string> userRoles);
}
