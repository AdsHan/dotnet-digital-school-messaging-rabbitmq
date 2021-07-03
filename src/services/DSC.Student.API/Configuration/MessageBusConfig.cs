using DSC.MessageBus;
using DSC.Student.API.Application.Messages.ConsumersBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DSC.Student.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetConnectionString("RabbitMQCs")).AddHostedService<UserCreatedOkConsumer>();

        }
    }
}