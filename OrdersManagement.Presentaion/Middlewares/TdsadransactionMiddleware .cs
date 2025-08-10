//using Microsoft.EntityFrameworkCore;

//namespace OrdersManagement.Presentaion.Middlewares;

//public class TransactionMiddleware
//{
//    private readonly DbContext _dbContext;

//    public TransactionMiddleware(DbContext dbContext)
//    {
//        _dbContext = dbContext;
//    }

//    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//    {
//        // Only start transaction for HTTP methods that modify data
//        if (context.Request.Method is not ("POST" or "PUT" or "PATCH" or "DELETE"))
//        {
//            await next(context);
//            return;
//        }

//        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

//        try
//        {
//            await next(context);

//            // Only commit if status code is successful
//            if (context.Response.StatusCode is >= 200 and < 300)
//            {
//                await _dbContext.SaveChangesAsync();
//                await transaction.CommitAsync();
//            }
//            else
//            {
//                await transaction.RollbackAsync();
//            }
//        }
//        catch
//        {
//            await transaction.RollbackAsync();
//            throw;
//        }
//    }
//}
