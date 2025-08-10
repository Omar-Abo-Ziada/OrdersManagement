using MyResturants.Application.Extensions;
using MyResturants.Infrastructure.Extensions;
using MyResturants.Infrastructure.Seeders;
using MyResturants.Presentaion.Extensions;
using MyResturants.Presentaion.Middlewares;
using OrdersManagement.Presentaion.Middlewares;
using Serilog;
using Serilog.Events;
using System.Reflection;

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

            // Register middleware services
            //builder.Services.AddScoped<TransactionMiddleware>();
            //builder.Services.AddScoped<ErrorHandlingMiddleware>();

            // Configure Swagger with XML documentation
            //builder.Services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            //    {
            //        Title = "Orders Management API",
            //        Version = "v1",
            //        Description = "A comprehensive API for managing orders, restaurants, and dishes using CQRS pattern",
            //        Contact = new Microsoft.OpenApi.Models.OpenApiContact
            //        {
            //            Name = "API Support",
            //            Email = "support@ordersmanagement.com"
            //        }
            //    });

            //    // Include XML comments
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    if (File.Exists(xmlPath))
            //    {
            //        c.IncludeXmlComments(xmlPath);
            //    }

            //    // Add JWT Bearer authentication
            //    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            //    {
            //        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            //        Name = "Authorization",
            //        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            //        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer"
            //    });

            //    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            //    {
            //        {
            //            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            //            {
            //                Reference = new Microsoft.OpenApi.Models.OpenApiReference
            //                {
            //                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
            //                    Id = "Bearer"
            //                }
            //            },
            //            Array.Empty<string>()
            //        }
            //    });
            //});

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
                    //c.RoutePrefix = string.Empty; // Serve Swagger UI at root
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
