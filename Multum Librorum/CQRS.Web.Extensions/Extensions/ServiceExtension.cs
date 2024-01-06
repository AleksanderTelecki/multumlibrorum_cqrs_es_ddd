using CQRS.Communication.Abstract;
using CQRS.Communication.Options;
using CQRS.Communication.Services;
using CQRS.Web.Extensions.Controllers;

namespace CQRS.Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRestController(this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.Configure<RestDispatcherOptions>(builderConfiguration.GetSection(nameof(RestDispatcherOptions)));
        services.AddControllers().AddApplicationPart(typeof(RestQueryController).Assembly);
        services.AddControllers().AddApplicationPart(typeof(RestCommandController).Assembly);
        services.AddHttpClient();
        
        services.AddScoped<IRestDispatcher, RestDispatcher>();
        
        return services;
    }
}