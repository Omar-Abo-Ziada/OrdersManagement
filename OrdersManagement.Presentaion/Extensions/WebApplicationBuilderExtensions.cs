using Microsoft.OpenApi.Models;
using MyResturants.Presentaion.Middlewares;

namespace MyResturants.Presentaion.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentaion(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Resturants API", Version = "v1" });

            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                    },
                    []

                    }
                });
        });

        builder.Services.AddScoped<ErrorHandlingMiddleware>();
    }
}