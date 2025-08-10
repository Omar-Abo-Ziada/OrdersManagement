using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyResturants.Domain.Constants;

namespace MyResturants.Infrastructure.Seeders;

public class RoleSeeder : ISeeder
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly ILogger<RoleSeeder> _logger;

    public RoleSeeder(RoleManager<IdentityRole<int>> roleManager, ILogger<RoleSeeder> logger)
    {
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task Seed()
    {
        _logger.LogInformation("Starting role seeding...");

        var roles = GetRoles();

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role.Name!))
            {
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Role '{RoleName}' created successfully", role);
                }
                else
                {
                    _logger.LogError("Failed to create role '{RoleName}': {Errors}", 
                        role, string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                _logger.LogInformation("Role '{RoleName}' already exists", role);
            }
        }

        _logger.LogInformation("Role seeding completed");
    }


    private IEnumerable<IdentityRole<int>> GetRoles()
    {
        List<IdentityRole<int>> roles =
        [
            new (UserRoles.Admin){Name = UserRoles.Admin ,NormalizedName = UserRoles.Admin.ToUpper() },
            new (UserRoles.Customer){Name = UserRoles.Customer, NormalizedName = UserRoles.Customer.ToUpper() },
            new (UserRoles.Seller){Name = UserRoles.Seller, NormalizedName = UserRoles.Seller.ToUpper() },
            new (UserRoles.Rider){ Name = UserRoles.Rider,NormalizedName = UserRoles.Rider.ToUpper() },
        ];

        return roles;
    }
}
