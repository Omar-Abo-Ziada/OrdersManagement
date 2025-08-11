using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyResturants.Application.Users.Commands.Login;
using MyResturants.Application.Users.Commands.Register;
using OrdersManagement.Application.Common.Responses;
using OrdersManagement.Application.Helpers;

namespace MyResturants.Presentaion.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class AuthController(IMediator mediator, IMapperHelper mapper)
    : BaseController(mediator, mapper)
{

    /// <summary>
    /// Register a new user account
    /// </summary>
    /// <param name="command">User registration details</param>
    /// <returns>Registration result with JWT token</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(CustomResultDTO<RegisterResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CustomResultDTO<RegisterResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomResultDTO<RegisterResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(CustomResultDTO<RegisterResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return HandleResultAsync<RegisterResponse>(result);
    }

    /// <summary>
    /// Authenticate user and get JWT token
    /// </summary>
    /// <param name="command">User login credentials</param>
    /// <returns>Authentication result with JWT token</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(CustomResultDTO<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResultDTO<LoginResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CustomResultDTO<LoginResponse>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(CustomResultDTO<LoginResponse>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(CustomResultDTO<LoginResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return HandleResultAsync<LoginResponse>(result);
    }
}
