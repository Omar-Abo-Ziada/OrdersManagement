using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MyResturants.Application.Users;

namespace MyResturants.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler
    (ILogger<MinimumAgeRequirementHandler> logger , IUserContext userContext)
    : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("User : {Email} , date of birth {dateOfBirth} - Handling MinimumAgeRequirement"
            , currentUser?.Email , currentUser?.DateOfBirth);

        if(currentUser?.DateOfBirth is null)
        {
            logger.LogWarning("User Date Of Birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if(currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Now))
        {
            logger.LogInformation("Authorziation Succeeded");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }



        throw new NotImplementedException();
    }
}
