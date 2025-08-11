using MyResturants.Application.Extensions;
using MyResturants.Infrastructure.Extensions;
using MyResturants.Infrastructure.Seeders;
using MyResturants.Presentaion.Extensions;
using MyResturants.Presentaion.Middlewares;
using OrdersManagement.Presentaion.Middlewares;
using Serilog;

namespace MyResturants.Presentaion
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddPresentaion();
            builder.Services
                .AddInfrastructure(builder.Configuration)
                .AddApplication(builder.Configuration);
       
            var app = builder.Build();

            var scope = app.Services.CreateScope();
            
            // Seed roles first
            var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
            await roleSeeder.Seed();
            
            // Then seed other data
            var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
            await seeder.Seed();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseMiddleware<TransactionMiddleware>();

            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders Management API v1");
                });
            }

            app.UseHttpsRedirection();

            //app.MapGroup("api/identity")
            //    .WithTags("Identity")
            //    .MapIdentityApi<User>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}