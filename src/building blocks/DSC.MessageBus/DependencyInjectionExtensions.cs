using Microsoft.Extensions.DependencyInjection;
using System;

namespace DSC.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

            services.AddSingleton<IMessageBusService>(new MessageBusService(connection));

            return services;
        }
    }
}