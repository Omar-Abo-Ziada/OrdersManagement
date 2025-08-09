
using MyResturants.Domain.Exceptions;
using OrdersManagement.Application.Exceptions;
using System.Net;

namespace MyResturants.Presentaion.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (CustomNotFoundException notFound)
        {
            logger.LogWarning(notFound, notFound.Message);
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(notFound.Message);
        }
        catch (BusinessException businessEx)
        {
            logger.LogWarning(businessEx, businessEx.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest; 
            await context.Response.WriteAsJsonAsync(new
            {
                type = businessEx.Type.ToString(),
                error = businessEx.Message
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync("Internal Server Error");
        }
    }
}
