using CQRS.Core.Commands;
using CQRS.Core.Commands.Abstract;
using CQRS.Core.Events;
using CQRS.Core.Events.Abstract;
using CQRS.Core.Queries;
using CQRS.Core.Queries.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Extensions
{
    public static class ServiceProvider
    {
        public static IServiceCollection ConfigureCQRS<TSearchedTypeAssembly>(this IServiceCollection services)
        {
            // TODO: ADD Handlers Chaching
            services.TryAddScoped<ICommandDispatcher, CommandDispatcher>();
            services.TryAddScoped<IQueryDispatcher, QueryDispatcher>();
            services.TryAddScoped<IEventDispatcher, EventDispatcher>();

            services.Scan(selector =>
            {
                selector.FromAssembliesOf(typeof(TSearchedTypeAssembly))
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(IQueryHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });
            services.Scan(selector =>
            {
                selector.FromAssembliesOf(typeof(TSearchedTypeAssembly))
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(ICommandHandler<>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });
            services.Scan(selector =>
            {
                selector.FromAssembliesOf(typeof(TSearchedTypeAssembly))
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(IEventHandler<>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });

            return services;
        }
    }
}
