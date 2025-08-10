using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrdersManagement.Application.Common.Configuration;
using OrdersManagement.Application.Common.Responses;
using OrdersManagement.Domain.Entities.User_Module;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrdersManagement.Application.Common.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly JWTSettings _jwtSettings;

    public JwtTokenService(IOptions<JWTSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateJwtToken(User user, IList<string> userRoles)
    {
        // Validate JWT settings
        if (string.IsNullOrEmpty(_jwtSettings.SecretKey))
            throw new InvalidOperationException("JWT SecretKey is not configured");
        
        if (_jwtSettings.ValidIssuers?.Length == 0)
            throw new InvalidOperationException("JWT ValidIssuers are not configured");
            
        if (_jwtSettings.ValidAudiences?.Length == 0)
            throw new InvalidOperationException("JWT ValidAudiences are not configured");

        // Use the first issuer and audience from the arrays
        var issuer = _jwtSettings.ValidIssuers.FirstOrDefault() ?? throw new InvalidOperationException("No valid issuer found");
        var audience = _jwtSettings.ValidAudiences.FirstOrDefault() ?? throw new InvalidOperationException("No valid audience found");

        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
        var tokenHandler = new JwtSecurityTokenHandler();
        
        // Create base claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim("UserId", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            new Claim("Nationality", user.Nationality ?? ""),
            new Claim("DateOfBirth", user.DateOfBirth?.ToString("yyyy-MM-dd") ?? "")
        };

        // Add role claims
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpirationInDays),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public TokenDTO CreateTokenDto(User user, IList<string> userRoles)
    {
        var token = GenerateJwtToken(user, userRoles);
        
        return new TokenDTO
        {
            Token = token,
            UserId = user.Id.ToString(),
            UserName = $"{user.FirstName} {user.LastName}",
            Email = user.Email!,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.ExpirationInDays),
            Roles = userRoles
        };
    }
}
