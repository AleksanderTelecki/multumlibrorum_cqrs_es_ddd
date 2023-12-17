using CQRS.Communication.Abstract;
using CQRS.Communication.Controllers;
using CQRS.Communication.Options;
using CQRS.Communication.Services;

namespace CQRS.Communication.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRestQueryController(this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.Configure<RestDispatcherOptions>(builderConfiguration.GetSection(nameof(RestDispatcherOptions)));
        services.AddControllers().AddApplicationPart(typeof(RestQueryController).Assembly);
        services.AddHttpClient();
        
        services.AddScoped<IRestDispatcher, RestDispatcher>();
        
        return services;
    }
}