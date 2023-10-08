using CQRS.Core.Commands;
using CQRS.Core.Events;
using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Core.Extensions
{
    public static class ServiceBuilderExtensions
    {
        public static void AddScopedCommandHandler(this IServiceCollection services, Type handlerType)
        {
            foreach (var interfaceType in handlerType.GetInterfaces()) 
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                {
                    services.AddScoped(interfaceType, handlerType);
                }
            }
        }

        public static void AddScopedEventHandler(this IServiceCollection services, Type handlerType)
        {
            foreach (var interfaceType in handlerType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                {
                    services.AddScoped(interfaceType, handlerType);
                }
            }
        }

        public static void AddScopedQueryHandler(this IServiceCollection services, Type handlerType)
        {
            foreach (var interfaceType in handlerType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                {
                    services.AddScoped(interfaceType, handlerType);
                }
            }
        }
    }
}
