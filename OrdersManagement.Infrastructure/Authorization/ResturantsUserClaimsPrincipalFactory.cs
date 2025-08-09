using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OrdersManagement.Domain.Entities.User_Module;
using System.Security.Claims;

namespace MyResturants.Infrastructure.Authorization;

public class ResturantsUserClaimsPrincipalFactory(UserManager<User> userManager,
    RoleManager<IdentityRole<int>> roleManager,
    IOptions<IdentityOptions> options)
    : UserClaimsPrincipalFactory<User, IdentityRole<int>>(userManager, roleManager, options)
{
    public async override Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);

        if (user.Nationality is not null)
            id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));

        if (user.DateOfBirth is not null)
            id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));

        return new ClaimsPrincipal(id);
    }
}