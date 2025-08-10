using System.Net;

namespace OrdersManagement.Application.Common.Responses;

public class CustomResultDTO<T>
{
    public bool IsSuccess { get; }
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; }
    public IEnumerable<string>? Errors { get; set; }
    public T? Data { get; }
    public TokenDTO? TokenDTO { get; set; }

    private CustomResultDTO(bool isSuccess, HttpStatusCode statusCode, string? message = null,
        T? data = default, IEnumerable<string>? errors = null, TokenDTO? tokenDTO = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
        Data = data;
        Errors = errors;
        TokenDTO = tokenDTO;
    }

    public static CustomResultDTO<T> Success(string? message = "Operation successful.", T? data = default,
        HttpStatusCode statusCode = HttpStatusCode.OK, TokenDTO? tokenResult = null)
     => new(isSuccess: true, statusCode: statusCode, message: message, data: data, tokenDTO: tokenResult);

    public static CustomResultDTO<T> Failure(string? message = "Operation failed.", T? data = default,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest, IEnumerable<string>? errors = null)
        => new(isSuccess: false, statusCode: statusCode, message: message, data: data, errors: errors);
}
