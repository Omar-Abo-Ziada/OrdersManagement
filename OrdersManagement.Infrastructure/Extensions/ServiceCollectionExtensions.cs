using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyResturants.Infrastructure.Authorization;
using MyResturants.Infrastructure.Authorization.Requirements;
using MyResturants.Infrastructure.Seeders;
using OrdersManagement.Domain.Entities.User_Module;
using OrdersManagement.Infrastructure.Presistance;
using System.Diagnostics;

namespace MyResturants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.EnableSensitiveDataLogging();
            options.LogTo(log => Debug.WriteLine(log), LogLevel.Information);
        });

        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<int>>()
            .AddClaimsPrincipalFactory<ResturantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<IResturantSeeder, ResturantSeeder>();

        //services.AddScoped<IResturantRepository, ResturantRepositoy>();
        //services.AddScoped<IDishRepository, DishRepository>();

        services.AddAuthorizationBuilder()
            //.AddPolicy("HasNationality" ,builder => builder.RequireClaim("Nationality")); // will restrict to only users who has nationality in their tokens claims
            .AddPolicy(PolicyNames.HasNationality,
             builder => builder.RequireClaim(AppClaimTypes.Nationality, ["American", "French"])) // will restrict to only users who has nationality with those values in their tokens claims
           
           .AddPolicy(PolicyNames.AtLeast20,
             builder => builder.AddRequirements(new MinimumAgeRequirement(20))); // will restrict to only users who has nationality with those values in their tokens claims

        services.AddScoped<IAuthorizationHandler , MinimumAgeRequirementHandler>();

        return services;
    }
}