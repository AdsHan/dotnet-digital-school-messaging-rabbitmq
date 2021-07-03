using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RTO.Auth.API.Extensions;

namespace RTO.Auth.API.Configuration
{
    public static class SettingsConfig
    {
        public static IServiceCollection AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var tokenSettings = new TokenSettings();
            new ConfigureFromConfigurationOptions<TokenSettings>(configuration.GetSection("Token")).Configure(tokenSettings);

            services.AddSingleton(tokenSettings);

            return services;
        }

    }
}