using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyResturants.Application.Resturants;
using MyResturants.Application.Users;
using OrdersManagement.Application.Helpers;
using Serilog;

namespace MyResturants.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddScoped<IResturantsService, ResturantsService>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration) 
            // From json to make it editable through differnt envirnments
            // Manual Config
            //.WriteTo.Seq("http://localhost:5341/")
            //.WriteTo.Console(outputTemplate:"[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] | {SourceContext} | {NewLine} {Message:lj}{NewLine}{Exception}")
            //.MinimumLevel.Override("Microsoft" , Serilog.Events.LogEventLevel.Warning)
            //.MinimumLevel.Override("Microsoft.EntityFrameworkCore" , Serilog.Events.LogEventLevel.Information)
            .CreateLogger();
            
        services.AddSerilog();

        services.AddAutoMapper(assembly);

        services.AddMapster(); 

        //MapsterConfig.Configure();
        
        services.AddScoped<IObjectMapper, MapsterAdapter>();

        services.AddValidatorsFromAssembly(assembly)
            .AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();

        return services;
    }
}