using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Notes
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder UseConnectedConfig(this IConfigurationBuilder config)
        {
            return config;
        }

        public static IServiceCollection AddConnectedServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
