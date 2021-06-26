using DSC.Auth.API.Application.Messages.ConsumersBus;
using DSC.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DSC.Auth.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetConnectionString("RabbitMQCs")).AddHostedService<CreateUserGuardianConsumer>();
        }
    }
}