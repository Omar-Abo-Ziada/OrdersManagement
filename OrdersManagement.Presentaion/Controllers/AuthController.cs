using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyResturants.Application.Users.Commands.Login;
using MyResturants.Application.Users.Commands.Register;
using OrdersManagement.Application.Common.Responses;
using System.Net;

namespace MyResturants.Presentaion.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

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
        
        return result.StatusCode switch
        {
            HttpStatusCode.Created => Success(result.Data!, result.TokenDTO, result.Message, result.StatusCode),
            HttpStatusCode.BadRequest => Failure(result.Message, result.Data, result.StatusCode, result.Errors),
            HttpStatusCode.Conflict => Conflict(result.Message, result.Data),
            HttpStatusCode.InternalServerError => InternalServerError(result.Message, result.Data),
            _ => Failure(result.Message, result.Data, result.StatusCode, result.Errors)
        };
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
        
        return result.StatusCode switch
        {
            HttpStatusCode.OK => Success(result.Data!, result.TokenDTO, result.Message, result.StatusCode),
            HttpStatusCode.BadRequest => Failure(result.Message, result.Data, result.StatusCode, result.Errors),
            HttpStatusCode.Unauthorized => Unauthorized(result.Message, result.Data),
            HttpStatusCode.Forbidden => Forbidden(result.Message, result.Data),
            HttpStatusCode.InternalServerError => InternalServerError(result.Message, result.Data),
            _ => Failure(result.Message, result.Data, result.StatusCode, result.Errors)
        };
    }
}
