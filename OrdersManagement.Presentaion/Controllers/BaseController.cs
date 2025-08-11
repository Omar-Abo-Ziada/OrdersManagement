using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrdersManagement.Application.Common.Responses;
using OrdersManagement.Application.Helpers;
using System.Net;

namespace MyResturants.Presentaion.Controllers;

[ApiController]
public abstract class BaseController(IMediator mediator , IMapperHelper mapper) : ControllerBase
{
    protected readonly IMediator _mediator = mediator;
    protected readonly IMapperHelper _mapper = mapper;

    /// <summary>
    /// Returns a successful response with data
    /// </summary>
    /// <typeparam name="T">Type of the response data</typeparam>
    /// <param name="data">Response data</param>
    /// <param name="message">Success message</param>
    /// <param name="statusCode">HTTP status code (default: 200 OK)</param>
    /// <returns>Success response</returns>
    protected IActionResult Success<T>(T data, string message = "Operation successful", HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var result = CustomResultDTO<T>.Success(message, data, statusCode);
        return StatusCode((int)result.StatusCode, result);
    }

    /// <summary>
    /// Returns a successful response with data and token
    /// </summary>
    /// <typeparam name="T">Type of the response data</typeparam>
    /// <param name="data">Response data</param>
    /// <param name="token">JWT token information</param>
    /// <param name="message">Success message</param>
    /// <param name="statusCode">HTTP status code (default: 200 OK)</param>
    /// <returns>Success response with token</returns>
    protected IActionResult Success<T>(T data, TokenDTO token, string message = "Operation successful", HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var result = CustomResultDTO<T>.Success(message, data, statusCode, token);
        return StatusCode((int)result.StatusCode, result);
    }

    /// <summary>
    /// Returns a failure response
    /// </summary>
    /// <typeparam name="T">Type of the response data</typeparam>
    /// <param name="message">Error message</param>
    /// <param name="data">Response data (optional)</param>
    /// <param name="statusCode">HTTP status code (default: 400 Bad Request)</param>
    /// <param name="errors">List of validation errors</param>
    /// <returns>Failure response</returns>
    protected IActionResult Failure<T>(string message = "Operation failed", T? data = default, HttpStatusCode statusCode = HttpStatusCode.BadRequest, IEnumerable<string>? errors = null)
    {
        var result = CustomResultDTO<T>.Failure(message, data, statusCode, errors);
        return StatusCode((int)result.StatusCode, result);
    }

    /// <summary>
    /// Returns a 404 Not Found response
    /// </summary>
    /// <typeparam name="T">Type of the response data</typeparam>
    /// <param name="message">Not found message</param>
    /// <param name="data">Response data (optional)</param>
    /// <returns>404 Not Found response</returns>
    protected IActionResult NotFound<T>(string message = "Resource not found", T? data = default)
    {
        return Failure(message, data, HttpStatusCode.NotFound);
    }

    /// <summary>
    /// Returns a 401 Unauthorized response
    /// </summary>
    /// <typeparam name="T">Type of the response data</typeparam>
    /// <param name="message">Unauthorized message</param>
    /// <param name="data">Response data (optional)</param>
    /// <returns>401 Unauthorized response</returns>
    protected IActionResult Unauthorized<T>(string message = "Unauthorized", T? data = default)
    {
        return Failure(message, data, HttpStatusCode.Unauthorized);
    }

    /// <summary>
    /// Returns a 403 Forbidden response
    /// </summary>
    /// <typeparam name="T">Type of the response data</typeparam>
    /// <param name="message">Forbidden message</param>
    /// <param name="data">Response data (optional)</param>
    /// <returns>403 Forbidden response</returns>
    protected IActionResult Forbidden<T>(string message = "Forbidden", T? data = default)
    {
        return Failure(message, data, HttpStatusCode.Forbidden);
    }

    /// <summary>
    /// Returns a 500 Internal Server Error response
    /// </summary>
    /// <typeparam name="T">Type of the response data</typeparam>
    /// <param name="message">Internal server error message</param>
    /// <param name="data">Response data (optional)</param>
    /// <returns>500 Internal Server Error response</returns>
    protected IActionResult InternalServerError<T>(string message = "Internal server error", T? data = default)
    {
        return Failure(message, data, HttpStatusCode.InternalServerError);
    }

    /// <summary>
    /// Returns a 409 Conflict response
    /// </summary>
    /// <typeparam name="T">Type of the response data</typeparam>
    /// <param name="message">Conflict message</param>
    /// <param name="data">Response data (optional)</param>
    /// <returns>409 Conflict response</returns>
    protected IActionResult Conflict<T>(string message = "Conflict", T? data = default)
    {
        return Failure(message, data, HttpStatusCode.Conflict);
    }

    protected IActionResult HandleResultAsync<TResponse>(
    CustomResultDTO<TResponse> result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.OK => Success(result.Data!, result.TokenDTO, result.Message, result.StatusCode),
            HttpStatusCode.Created => Success(result.Data!, result.TokenDTO, result.Message, result.StatusCode),
            HttpStatusCode.BadRequest => Failure(result.Message, result.Data, result.StatusCode, result.Errors),
            HttpStatusCode.Unauthorized => Unauthorized(result.Message, result.Data),
            HttpStatusCode.Forbidden => Forbidden(result.Message, result.Data),
            HttpStatusCode.Conflict => Conflict(result.Message, result.Data),
            HttpStatusCode.InternalServerError => InternalServerError(result.Message, result.Data),
            _ => Failure(result.Message, result.Data, result.StatusCode, result.Errors)
        };
    }

}
