using OrdersManagement.Infrastructure.Presistance;

namespace OrdersManagement.Presentaion.Middlewares;

public class TransactionMiddleware : IMiddleware
{
    private readonly ApplicationDbContext _context;
    public TransactionMiddleware(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string method = context.Request.Method.ToUpper();

        if (method == "POST" || method == "PUT" || method == "DELETE")
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await next(context);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        else
        {
            await next(context);
        }
    }
}
// Extension method used to add the middleware to the HTTP request pipeline.
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseTransactionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TransactionMiddleware>();
    }
}
