using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyResturants.Presentaion.Middlewares;
using OrdersManagement.Application.Common.Configuration;
using OrdersManagement.Application.Common.Responses;
using OrdersManagement.Domain.Entities.User_Module;
using OrdersManagement.Infrastructure.Presistance;
using OrdersManagement.Presentaion.Middlewares;
using Serilog;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MyResturants.Presentaion.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentaion(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddControllers();

        ConfigureLogs(builder);
        ConfigureJWT(builder);
        ConfigureSwagger(builder);

        // Register middleware services
        builder.Services.AddScoped<TransactionMiddleware>();
        builder.Services.AddScoped<ErrorHandlingMiddleware>();

        builder.Services.AddEndpointsApiExplorer();
        // Swagger configuration moved to Program.cs to avoid duplicates
    }

    private static void ConfigureLogs(WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(builder.Configuration)
           .Enrich.FromLogContext()
           .CreateLogger();
        builder.Host.UseSerilog();
    }

    static void ConfigureJWT(WebApplicationBuilder builder)
    {
        var jwtSettingsSection = builder.Configuration.GetSection(nameof(JWTSettings));
        builder.Services.Configure<JWTSettings>(jwtSettingsSection);
        var jwtSettings = jwtSettingsSection.Get<JWTSettings>();

        builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireDigit = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuers = jwtSettings?.ValidIssuers,
                ValidAudiences = jwtSettings?.ValidAudiences,
                IssuerSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(jwtSettings!.SecretKey)),
                ClockSkew = TimeSpan.Zero
            };

            // Customizing unauthorized response
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse(); // Prevent the default 401 challenge

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(
                        CustomResultDTO<string>.Failure(
                            errors: ["Unauthorized access"],
                            statusCode: HttpStatusCode.Unauthorized
                        )
                    );

                    return context.Response.WriteAsync(result);
                },

                // Optionally handle forbidden response (403)
                OnForbidden = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(
                        CustomResultDTO<string>.Failure(
                            errors: ["Forbidden access"],
                            statusCode: HttpStatusCode.Forbidden
                        )
                    );

                    return context.Response.WriteAsync(result);
                }
            };
        });

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ " +
                "أبجدهوزحطيكلمنسعفصقرشتثخذضظغ";
            options.User.RequireUniqueEmail = true;
        });
    }

    static void ConfigureSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Orders Managemnet API v1",
                Description = "ASP.NET Core 9 Web API with JWT Auth"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
      {
          new OpenApiSecurityScheme
          {
              Reference = new OpenApiReference
              {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
              }
          },
          Array.Empty<string>()
      }
  });
        });
    }
}