using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyResturants.Application.Users.Commands.AssignUserRole;
using MyResturants.Application.Users.Commands.UnAssignUserRole;
using MyResturants.Application.Users.Commands.UpdateUserDetails;
using MyResturants.Domain.Constants;

namespace MyResturants.Presentaion.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand updateUserDetailsCommand)
    {
        await mediator.Send(updateUserDetailsCommand);
        return NoContent();
    }

    [HttpPatch("AssignUserRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("UnAssignUserRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UnAssignUserRole(UnAssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}